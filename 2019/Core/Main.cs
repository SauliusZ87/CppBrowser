using EnvDTE;
using EnvDTE80;
using System;

namespace CppBrowser.Core
{
    public class Main
    {
        public Main(DTE2 dte)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            try
            {
                _applicationObject = dte;

                solutionEvents = _applicationObject.Events.SolutionEvents;
                solutionEvents.AfterClosing += new _dispSolutionEvents_AfterClosingEventHandler(SolutionEvents_Closing);
                solutionEvents.ProjectAdded += new _dispSolutionEvents_ProjectAddedEventHandler(SolutionEvents_ProjectAdded);

                m_state = new State();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        public void ShowElements()
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            if (_applicationObject.ActiveDocument != null && 0 < _applicationObject.ActiveDocument.FullName.Length)
            {
                System.Collections.Generic.List<Element> array = null;

                string dbfile = System.IO.Path.ChangeExtension(_applicationObject.Solution.FullName, "sdf");
                if (System.IO.File.Exists(dbfile))
                {
                    DB.DBSqlCe db = new DB.DBSqlCe(dbfile);
                    array = infoGrabber.GetInfoForFile(db, _applicationObject.ActiveDocument.FullName);
                    db.Dispose();
                }
                else
                {
                    dbfile = GetNewDBPath();
                    if (System.IO.File.Exists(dbfile))
                    {
                        string copy = "";
                        try
                        {
                            copy = dbfile + "_back";
                            System.IO.File.Copy(dbfile, copy);
                        }
                        catch(Exception)
                        {
                            System.IO.File.Delete(copy);
                        }

                        DB.DBSqlLite db = null;
                        try
                        { 
                            db = new DB.DBSqlLite(copy);
                            array = infoGrabber.GetInfoForFile(db, _applicationObject.ActiveDocument.FullName);
                        }
                        finally
                        {
                            db.Dispose();
                            System.IO.File.Delete(copy);
                        }
                    }
                }

                if (null == array && IsCppFileExtension(_applicationObject.ActiveDocument.Name))
                {
                    array = new CLang().ParseFile(_applicationObject.ActiveDocument.FullName);
                    if (null == array || 0 == array.Count)
                        array = new CppParser().ParseFile(_applicationObject.ActiveDocument.FullName);
                }

                if (null != array)
                    new ListBoxEx(array, _applicationObject.ActiveDocument, m_state).Show();
            }
        }

        private string GetNewDBPath()
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            string dbfile = System.IO.Path.ChangeExtension(_applicationObject.Solution.FullName, "VC.db");
            if (System.IO.File.Exists(dbfile))
                return dbfile;

            dbfile = System.IO.Path.GetDirectoryName(_applicationObject.Solution.FullName);
            dbfile = System.IO.Path.Combine(dbfile, ".vs", System.IO.Path.GetFileNameWithoutExtension(_applicationObject.Solution.FileName), "v15");
            dbfile = System.IO.Path.Combine(dbfile, "Browse.VC.db");
            return dbfile;
        }

        private bool IsCppFileExtension(string file)
        {
            foreach (string extension in m_validExtensions)
            {
                if (file.EndsWith(extension))
                    return true;
            }

            return false;
        }

        private void SolutionEvents_Closing()
        {
            SolutionChanged();
        }

        private void SolutionEvents_ProjectAdded(Project project)
        {
            SolutionChanged();
        }

        private void SolutionChanged()
        {
            infoGrabber.ResetIDs();
        }

        private DTE2 _applicationObject = null;
        private SolutionEvents solutionEvents = null;
        private State m_state = null;
        private InfoGrabber infoGrabber = new InfoGrabber();

        private readonly System.Collections.Generic.List<string> m_validExtensions = new System.Collections.Generic.List<string>() { ".c", ".cpp", ".cxx", ".cc", ".h", ".hxx", ".hh", ".inl" };
    }
}
