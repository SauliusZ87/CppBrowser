using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CppBrowser.Core
{
    class CLang
    {
        private string location;
        private string outputFile;
        private string programFile;
        public CLang()
        {
            location = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembl‌​y().Location);
            outputFile = Path.Combine(location, "clang_output");
            programFile = Path.Combine(location, "clang.exe");
        }
        public List<Element> ParseFile(string filePath)
        {
            List<Element> elements = new List<Element>();

            if(RunCLang(filePath))
            {
                StreamReader file = new StreamReader(outputFile);
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] items = line.Split((char)1);
                    if (6 == items.GetLength(0))
                    {
                        Element element = new Element();
                        element.ID = (Element.IdentifiersIDs)int.Parse(items[0]);
                        element.Name = items[1];
                        element.Parent = items[2];
                        element.LineBegin = int.Parse(items[3]);
                        element.LineEnd = int.Parse(items[4]);
                        element.Value = items[5];

                        elements.Add(element);
                    }
                }
            }

            return elements;
        }

        private bool RunCLang(string filePath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = programFile;
            startInfo.Arguments = "\"" + filePath + "\" \"" + outputFile + "\"";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process processTemp = new Process();
            processTemp.StartInfo = startInfo;
            processTemp.EnableRaisingEvents = true;
            try
            {
                processTemp.Start();
                if (processTemp.WaitForExit(3000) && 0 == processTemp.ExitCode && File.Exists(outputFile))
                    return true;
            }
            catch (System.Exception)
            {
            }

            return false;
        }
    }
}
