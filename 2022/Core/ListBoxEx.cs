using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using EnvDTE;
using System.Collections;

namespace CppBrowser.Core
{
    public partial class ListBoxEx : Form
    {
        public enum ListBoxLocation { Mouse, CenterScreen, Manual }

        public static FormStartPosition ListBoxLocationToStartPosition(ListBoxLocation location)
        {
            switch(location)
            {
                case ListBoxLocation.CenterScreen:
                    return FormStartPosition.CenterScreen;
                default:
                    return FormStartPosition.Manual;
            }
        }

        class CompareByLine : IComparer<Element>
        {
            private bool m_bShowParams;
            private bool m_bShowNameSpace;
            public CompareByLine(bool bShowParams, bool bShowNameSpace)
            {
                m_bShowParams = bShowParams;
                m_bShowNameSpace = bShowNameSpace;
            }
            // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
            int IComparer<Element>.Compare(Element info1, Element info2)
            {
                return (info1.LineBegin.CompareTo(info2.LineBegin));
            }
        }

        class CompareByName : IComparer<Element>
        {
            private bool m_bShowParams;
            private bool m_bShowNameSpace;
            public CompareByName(bool bShowParams, bool bShowNameSpace)
            {
                m_bShowParams = bShowParams;
                m_bShowNameSpace = bShowNameSpace;
            }
            // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
            int IComparer<Element>.Compare(Element info1, Element info2)
            {
                if (m_bShowNameSpace)
                {
                    if (info1.Parent.Length == 0 && info2.Parent.Length != 0)
                        return -1;
                    else if (info1.Parent.Length != 0 && info2.Parent.Length == 0)
                        return 1;
                    else
                    {
                        int count1 = info1.Parent.Length - info1.Parent.Replace("::", "").Length;
                        int count2 = info2.Parent.Length - info2.Parent.Replace("::", "").Length;
                        if (count1 != count2)
                            return count1.CompareTo(count2);
                    }
                }
                return (new CaseInsensitiveComparer().Compare(info1.FormatName(m_bShowParams, m_bShowNameSpace),
                        info2.FormatName(m_bShowParams, m_bShowNameSpace)));
            }
        }

        public ListBoxEx(System.Collections.Generic.List<Element> items, EnvDTE.Document document, State state)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            InitializeComponent();
            
            MouseWheel += new MouseEventHandler(ListBoxEx_MouseWheel);

            m_items = items;
            m_activeDocument = document;

            m_state = state;

            tiParameters.Checked = m_state.ShowParams;
            tiNamespace.Checked = m_state.ShowNamespace;
            tiSort.Checked = m_state.SortByName;

            this.StartPosition = ListBoxLocationToStartPosition(m_state.WindowLocation);
            this.Location = new Point(System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y);

            UpdateListboxFont();
            UpdateTooltipsForToolstripItems();
            if (m_state.LockFindbar)
            {
                PerformFindButtonClick();
                int nIndex = toolStripFind.Items.IndexOfKey("btnLockFindBar");
                if (0 <= nIndex)
                    toolStripFind.Items[nIndex].PerformClick();
            }

            UpdateItemsPosition();

            if (m_state.RememberLastSearch)
                btnRememberLastSearch.Checked = true;

            PopulateList();
            listBox1.Update();
        }

        private void UpdateItemsPosition()
        {
            Dictionary<int, ToolStripItem> dict = new Dictionary<int, ToolStripItem>(toolStripMain.Items.Count);

            foreach (ToolStripItem item in toolStripMain.Items)
                dict.Add(Convert.ToInt32(item.Tag), item);

            for (int i = toolStripMain.Items.Count - 1; i >= 0 ; --i)
                toolStripMain.Items.RemoveAt(i);

            for (int i = 0; i < dict.Count; ++i)
                toolStripMain.Items.Insert(i, dict[m_state.ItemsOrder[i]]);
        }

        private void RememberItemsPosition()
        {
            m_state.ItemsOrder.RemoveRange(0, m_state.ItemsOrder.Count);
            foreach (ToolStripItem item in toolStripMain.Items)
                m_state.ItemsOrder.Add(Convert.ToInt32(item.Tag));
        }

        private void UpdateListboxFont()
        {
            listBox1.Font = m_state.Font;
            Graphics g = listBox1.CreateGraphics();
            SizeF size = g.MeasureString("A", listBox1.Font, 40, StringFormat.GenericTypographic);
            listBox1.ItemHeight = (int)Math.Ceiling(size.Height);
        }

        private void ListBox_Deactivate(object sender, EventArgs e)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            if (!m_bSettingsViewOpened)
            {
                RememberItemsPosition();
                Close();
                m_activeDocument.ActiveWindow.Activate();
            }
        }

        private void ListBox_Shown(object sender, EventArgs e)
        {
            if (toolStripFind.Visible)
                findBox.Focus();
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            MoveToCurrentSelectedItem();
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (toolStripFind.Visible && !m_state.LockFindbar)
                        PerformFindButtonClick();
                    else
                        Close();
                    e.Handled = true;
                    break;
                case Keys.Enter:
                    MoveToCurrentSelectedItem();
                    e.Handled = true;
                    break;
                default:
                    e.Handled = ProcessShortCuts(e.KeyCode, e.Modifiers);
                    break;
            }
        }

        private bool ProcessShortCuts(Keys key, Keys modifier)
        {
            if (Keys.Alt == modifier)
            {
                int nIndex = -1;
                switch (key)
                {
                    case Keys.D1:
                    case Keys.NumPad1:
                        nIndex = 0;
                        break;
                    case Keys.D2:
                    case Keys.NumPad2:
                        nIndex = 1;
                        break;
                    case Keys.D3:
                    case Keys.NumPad3:
                        nIndex = 2;
                        break;
                    case Keys.D4:
                    case Keys.NumPad4:
                        nIndex = 3;
                        break;
                    case Keys.D5:
                    case Keys.NumPad5:
                        nIndex = 4;
                        break;
                    case Keys.D6:
                    case Keys.NumPad6:
                        nIndex = 5;
                        break;
                    default:
                        return false;
                }

                if (0 <= nIndex && toolStripMain.Items.Count > nIndex)
                {
                    toolStripMain.Items[nIndex].PerformClick();
                    return true;
                }
            }
            else if (Keys.Control == modifier)
            {
                if (Keys.F == key)
                {
                    if (toolStripFind.Visible || m_state.LockFindbar)
                        findBox.Focus();
                    else
                        PerformFindButtonClick();
                    return true;
                }
            }
             
            return false;
        }

        private void UpdateTooltipsForToolstripItems()
        {
            for (int i = 0; i < toolStripMain.Items.Count; ++i)
            {
                ToolStripItem item = toolStripMain.Items[i];
                switch (item.Name)
                {
                    case "tiNamespace":
                        item.ToolTipText = "Show qualified names";
                        break;
                    case "tiParameters":
                        item.ToolTipText = "Show parameters";
                        break;
                    case "tiSort":
                        item.ToolTipText = "Sort by name";
                        break;
                    case "tiFind":
                        item.ToolTipText = "Show find dialog (CTRL+F)";
                        break;
                    case "tiSettings":
                        item.ToolTipText = "Show settings dialog";
                        break;
                }

                item.ToolTipText += " (ALT+" + (i + 1).ToString() + ")";
            }
        }

        private void PopulateList()
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            listBox1.Items.Clear();

            if (0 < m_items.Count)
            {
                if (tiSort.Checked)
                    m_items.Sort(new CompareByName(tiParameters.Checked, tiNamespace.Checked));
                else
                    m_items.Sort(new CompareByLine(tiParameters.Checked, tiNamespace.Checked));

                int nMaxLength = 0;
                int nMaxLengthIndex = 0;
                for (int i = 0; i < m_items.Count; ++i)
                {
                    Element info = m_items[i];
                    KeyValuePair<bool, Color> pair = m_state.ItemsInfo[info.ID];
                    if (pair.Key)
                    {
                        string name = info.FormatName(tiParameters.Checked, tiNamespace.Checked).Trim();

                        if ((m_state.LockFindbar || toolStripFind.Visible) && 0 < m_matchString.Length)
                        {
                            if (m_state.FindOnlyName && m_state.ShowNamespace)
                            {
                                if (-1 == info.Name.Trim().IndexOf(m_matchString, m_state.CaseSensitiveFind ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase))
                                    continue;
                            }
                            else if (-1 == name.IndexOf(m_matchString, m_state.CaseSensitiveFind ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase))
                                continue;
                        }

                        listBox1.Items.Add(new ListBoxExItem(name, true, pair.Value, i));

                        if (nMaxLength < name.Length)
                        {
                            nMaxLength = name.Length;
                            nMaxLengthIndex = i;
                        }
                    }
                }

                Graphics graphics = this.CreateGraphics();
                int width = (int)graphics.MeasureString(m_items[nMaxLengthIndex].FormatName(tiParameters.Checked, tiNamespace.Checked), listBox1.Font).Width;
                int height = listBox1.ItemHeight * listBox1.Items.Count;

                Size sizeDiff = this.Size - listBox1.Size;
                this.Size = new Size(width, height) + sizeDiff;
            }

            switch (m_state.WindowLocation)
            {
                case ListBoxLocation.Manual:
                    this.Location = m_state.WindowPos;
                    goto case ListBoxLocation.Mouse;
                case ListBoxLocation.Mouse:
                    Point newLocation = new Point();
                    newLocation.X = this.Size.Width + this.Location.X;
                    newLocation.Y = this.Size.Height + this.Location.Y;

                    Rectangle screenSize = System.Windows.Forms.Screen.GetWorkingArea(this.Location);

                    // second monitor connected
                    if (this.Location.X > screenSize.Width)
                        screenSize.Width += screenSize.Left;

                    // second monitor connected
                    if (this.Location.Y > screenSize.Height)
                        screenSize.Height += screenSize.Top;

                    Point updatedLocation = this.Location;
                    if (newLocation.X > screenSize.Width)
                        updatedLocation.X = this.Location.X - (newLocation.X - screenSize.Width);

                    if (newLocation.Y > screenSize.Height)
                        updatedLocation.Y = this.Location.Y - (newLocation.Y - screenSize.Height);

                    if (0 > updatedLocation.X)
                        updatedLocation.X = 0;

                    if (0 > updatedLocation.Y)
                        updatedLocation.Y = 0;

                    this.Location = updatedLocation;

                    break;
            }

            if (m_state.SelectCurrentItem)
                SelectCurrentItem();
        }

        private void SelectCurrentItem()
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            TextSelection selection = (TextSelection)m_activeDocument.Selection;
            if(0 <= selection.CurrentLine)
            {
                for (int i = 0; i < listBox1.Items.Count; ++i )
                {
                    Element element = m_items[((ListBoxExItem)listBox1.Items[i]).Index];
                    if (selection.CurrentLine >= element.LineBegin && selection.CurrentLine <= element.LineEnd)
                    {
                        listBox1.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void MoveToCurrentSelectedItem()
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            if (0 <= listBox1.SelectedIndex)
            {
                object obj = listBox1.Items[listBox1.SelectedIndex];
                if (obj is ListBoxExItem)
                {
                    TextSelection selection = (TextSelection)m_activeDocument.Selection;
                    selection.GotoLine(m_items[((ListBoxExItem)obj).Index].LineBegin);
                }
            }
            Close();
        }

        private void tiNamespace_Click(object sender, EventArgs e)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            m_state.ShowNamespace = tiNamespace.Checked;
            PopulateList();
            listBox1.Update();
        }

        private void tiParameters_Click(object sender, EventArgs e)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            m_state.ShowParams = tiParameters.Checked;
            PopulateList();
            listBox1.Update();
        }
        
        private void tiSort_Click(object sender, EventArgs e)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            m_state.SortByName = tiSort.Checked;
            PopulateList();
            listBox1.Update();
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (0 > e.Index)
                return;

            string fullstring = null;
            Brush textBrush = null;

            object obj = ((System.Windows.Forms.ListBox)sender).Items[e.Index];
            if (obj is ListBoxExItem)
            {
                ListBoxExItem item = (ListBoxExItem)obj;
                fullstring = item.Text;
                textBrush = new SolidBrush(item.Color);
            }
            else
            {
                fullstring = obj.ToString();
                textBrush =  Brushes.Black;
            }

            Brush backgroundBrush = null;

            if (DrawItemState.Selected == (e.State & DrawItemState.Selected))
                backgroundBrush = new SolidBrush(m_state.SelectionColor);
            else if(e.Index % 2 == 0)
                backgroundBrush = new SolidBrush(m_state.BackColor1);
            else
                backgroundBrush = new SolidBrush(m_state.BackColor2);

            if((m_state.LockFindbar || toolStripFind.Visible) && 0 < m_matchString.Length)
            {
                int nBeginSearchFrom = (m_state.FindOnlyName && m_state.ShowNamespace ? fullstring.LastIndexOf("::") : 0);
                if (-1 == nBeginSearchFrom)
                    nBeginSearchFrom = 0;

                float whiteSpaceSize = e.Graphics.MeasureString("_ _", listBox1.Font, e.Bounds.Size, StringFormat.GenericTypographic).Width -
                                       2F * e.Graphics.MeasureString("_", listBox1.Font, e.Bounds.Size, StringFormat.GenericTypographic).Width;
                                       

                int nIndexOfMatch = fullstring.IndexOf(m_matchString, nBeginSearchFrom, m_state.CaseSensitiveFind ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase);
                if (-1 != nIndexOfMatch)
                {
                    string firstPart = fullstring.Substring(0, nIndexOfMatch);
                    string matchPart = fullstring.Substring(nIndexOfMatch, m_matchString.Length);

                    SizeF firstSize = e.Graphics.MeasureString(firstPart, listBox1.Font, e.Bounds.Size, StringFormat.GenericTypographic);
                    CompensateWhiteSpace(ref firstSize, firstPart, whiteSpaceSize);
                    SizeF matchSize = e.Graphics.MeasureString(matchPart, listBox1.Font, e.Bounds.Size, StringFormat.GenericTypographic);
                    CompensateWhiteSpace(ref matchSize, matchPart, whiteSpaceSize);

                    e.Graphics.FillRectangle(backgroundBrush, new RectangleF(e.Bounds.Left, e.Bounds.Top, firstSize.Width, e.Bounds.Height));
                    e.Graphics.FillRectangle(new SolidBrush(m_state.FindColor), new RectangleF(e.Bounds.Left + firstSize.Width, e.Bounds.Top, matchSize.Width, e.Bounds.Height));
                    e.Graphics.FillRectangle(backgroundBrush, new RectangleF(e.Bounds.Left + firstSize.Width + matchSize.Width,
                                                                             e.Bounds.Top,
                                                                             e.Bounds.Width - firstSize.Width - matchSize.Width,
                                                                             e.Bounds.Height));
                }
            }
            else
                e.Graphics.FillRectangle(backgroundBrush, e.Bounds);



            e.Graphics.DrawString(fullstring, e.Font, textBrush, e.Bounds, StringFormat.GenericTypographic);
            e.DrawFocusRectangle();
        }

        private void CompensateWhiteSpace(ref SizeF size, string text, float whitespaceSize)
        {
            if(0 > size.Width)
                size.Width = 0F;

            int nWhiteSpacesInBack = 0;
            while (true)
            {
                int nIndex = text.Length - 1 - nWhiteSpacesInBack;
                if (0 <= nIndex && ' ' == text[nIndex])
                    ++nWhiteSpacesInBack;
                else
                    break;
            }

            if (0 < nWhiteSpacesInBack)
                size.Width += whitespaceSize * nWhiteSpacesInBack;
        }

        private void tiSettings_Click(object sender, EventArgs e)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            m_bSettingsViewOpened = true;
            Settings settings = new Settings(m_state);
            settings.Location = this.Location;
            if (DialogResult.OK == settings.ShowDialog(this))
            {
                UpdateListboxFont();
                PopulateList();
                listBox1.Update();
            }
            m_bSettingsViewOpened = false;
        }

        private void toolStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            m_LastPoint = new Point(e.X, e.Y);

            foreach (ToolStripItem item in toolStripMain.Items)
            {
                if (item.Bounds.Contains(m_LastPoint))
                {
                    CanMove = false;
                    return;
                }
            }
            CanMove = true;
        }

        private void toolStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            if (CanMove)
            {
                this.Left += e.X - m_LastPoint.X;
                this.Top += e.Y - m_LastPoint.Y;
            }
        }

        private void toolStripMain_MouseUp(object sender, MouseEventArgs e)
        {
            CanMove = false;
        }

        private void toolStripMain_MouseLeave(object sender, EventArgs e)
        {
            CanMove = false;
        }

        private void toolStripMain_LayoutCompleted(object sender, EventArgs e)
        {
            UpdateTooltipsForToolstripItems();
        }

        private void closeFind_Click(object sender, EventArgs e)
        {
            PerformFindButtonClick();
        }

        private void PerformFindButtonClick()
        {
            int nIndex = toolStripMain.Items.IndexOfKey("tiFind");
            if (0 <= nIndex)
                toolStripMain.Items[nIndex].PerformClick();
        }

        private void findBox_EscapePressed(object sender)
        {
            listBox1.Focus();
        }

        private void findBox_EnterPressed(object sender)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            MoveToCurrentSelectedItem();
        }

        private void findBox_ArrowPressed(object sender, Keys keyData)
        {
            if (0 < listBox1.Items.Count)
            {
                switch (keyData)
                {
                    case Keys.Up:
                        if (0 < listBox1.SelectedIndex)
                            listBox1.SelectedIndex--;
                        break;
                    case Keys.Down:
                        if (listBox1.Items.Count - 1 > listBox1.SelectedIndex)
                            listBox1.SelectedIndex++;
                        break;
                    case Keys.Home:
                        listBox1.SelectedIndex = 0;
                        break;
                    case Keys.End:
                        if (0 < listBox1.Items.Count)
                            listBox1.SelectedIndex = listBox1.Items.Count - 1;
                        break;
                }
            }
        }

        private void findBox_AltNumPressed(object sender, Keys keyData)
        {
            ProcessShortCuts(keyData, Keys.Alt);
        }

        private void tiFind_Click(object sender, EventArgs e)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            bool bShow = tiFind.Checked;

            if (toolStripFind.Visible != bShow)
            {
                toolStripFind.Enabled = bShow;
                toolStripFind.Visible = bShow;

                if(!m_state.RememberLastSearch)
                    m_matchString = string.Empty;

                if (!bShow)
                    findBox.Text = string.Empty;

                PopulateList();
                listBox1.Update();

                Size listboxSize = listBox1.Size;
                if (bShow)
                    listboxSize.Height -= toolStripFind.Size.Height;
                else
                    listboxSize.Height += toolStripFind.Size.Height;
                listBox1.Size = listboxSize;
            }

            if (bShow)
            {
                if (m_state.RememberLastSearch)
                    findBox.Text = m_matchString;
                else
                    findBox.Text = string.Empty;

                findBox.Focus();
            }
        }

        private void findBox_TextChanged(object sender, EventArgs e)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

            if (toolStripFind.Visible)
            {
                m_matchString = findBox.Text;
                PopulateList();
                listBox1.Update();
                if (0 < listBox1.Items.Count)
                    listBox1.SelectedIndex = 0;
            }
        }

        private void btnRememberLastSearch_Click(object sender, EventArgs e)
        {
            m_state.RememberLastSearch = btnRememberLastSearch.Checked;
        }

        private void btnLockFindBar_Click(object sender, EventArgs e)
        {
            m_state.LockFindbar = btnLockFindBar.Checked;
            tiFind.Enabled = btnCloseFind.Enabled = !btnLockFindBar.Checked;
        }

        class Win32
        {
            [System.Runtime.InteropServices.DllImport("User32.Dll")]
            public static extern bool SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
            public const int WM_MOUSEWHEEL = 0x020A;
        }

        void ListBoxEx_MouseWheel(object sender, MouseEventArgs e)
        {
            int scrollInfo = (0 & 0xFFFF) + ((e.Delta & 0xFFFF) << 16);
            Win32.SendMessage(listBox1.Handle, Win32.WM_MOUSEWHEEL, scrollInfo, 0);
        }

        private static string m_matchString = string.Empty;

        private Point m_LastPoint;

        private State m_state;

        private bool m_bSettingsViewOpened = false;
        private System.Collections.Generic.List<Element> m_items;
        private EnvDTE.Document m_activeDocument;

        private bool CanMove { get; set; }
    }
}
