using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace CppBrowser.Core
{
    public class State
    {
        public State()
        {
            try
            {
                LoadState();
            }
            catch (Exception)
            {

            }
        }

        private void LoadState()
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(State.RegKey);
            if (null != regKey)
            {
                try
                {
                    ShowParams = Convert.ToBoolean(regKey.GetValue(Reg_ShowParamsKey));
                    ShowNamespace = Convert.ToBoolean(regKey.GetValue(Reg_ShowNamespaceKey));
                    SortByName = Convert.ToBoolean(regKey.GetValue(Reg_SortByNameKey));
                    LockFindbar = Convert.ToBoolean(regKey.GetValue(Reg_LockFindbar));
                    CaseSensitiveFind = Convert.ToBoolean(regKey.GetValue(Reg_CaseSensitiveFind));
                    SelectCurrentItem = Convert.ToBoolean(regKey.GetValue(Reg_SelectCurrentItem));
                    FindOnlyName = Convert.ToBoolean(regKey.GetValue(Reg_FindOnlyName));

                    {
                        object color = regKey.GetValue(Reg_BackColor1);
                        BackColor1 = (null == color) ? defaultBackColor1 : Color.FromArgb(Convert.ToInt32(color));
                        color = regKey.GetValue(Reg_BackColor2);
                        BackColor2 = (null == color) ? defaultBackColor2 : Color.FromArgb(Convert.ToInt32(color));
                        color = regKey.GetValue(Reg_SelectedColor);
                        SelectionColor = (null == color) ? defaultSelectionColor : Color.FromArgb(Convert.ToInt32(color));
                        color = regKey.GetValue(Reg_FindColor);
                        FindColor = (null == color) ? defaultFindColor : Color.FromArgb(Convert.ToInt32(color));
                    }
                    {
                        object font = regKey.GetValue(Reg_Font);
                        Font = (null == font) ? defaultFont : (Font)TypeDescriptor.GetConverter(typeof(Font)).ConvertFromString(font.ToString());
                    }
                    {
                        object window = regKey.GetValue(Reg_WindowLocation);
                        WindowLocation = (null == window) ? ListBoxEx.ListBoxLocation.Mouse : (ListBoxEx.ListBoxLocation)Convert.ToInt16(window);

                        window = regKey.GetValue(Reg_WindowPos);
                        WindowPos = (null == window) ? new Point(0, 0) : (Point)TypeDescriptor.GetConverter(typeof(Point)).ConvertFromString(window.ToString());
                    }

                    {
                        ItemsOrder = new List<int>(ItemOrderCount);
                        for (int i = 0; i < ItemOrderCount; ++i)
                            ItemsOrder.Add(i);

                        string loadedOrder = Convert.ToString(regKey.GetValue(Reg_ItemsOrder));
                        string[] Items = new System.Text.RegularExpressions.Regex(";").Split(loadedOrder);

                        if (Items.Length >= ItemOrderCount)
                        {
                            for (int i = 0; i < ItemOrderCount; i++)
                                ItemsOrder[i] = Convert.ToInt32(Items[i]);
                        }
                    }

                    {
                        string loadedDict = Convert.ToString(regKey.GetValue(Reg_DictInfo));
                        int size = Enum.GetValues(typeof(Element.IdentifiersIDs)).Length;
                        m_ItemsInfo = new Dictionary<Element.IdentifiersIDs, KeyValuePair<bool, Color>>(size);
                        System.Text.RegularExpressions.Regex regexSemiColon = new System.Text.RegularExpressions.Regex(";");
                        System.Text.RegularExpressions.Regex regexComma = new System.Text.RegularExpressions.Regex(",");
                        string[] MainValues = regexSemiColon.Split(loadedDict);
                        foreach (string mainValue in MainValues)
                        {
                            if (0 == mainValue.Length)
                                continue;

                            Element.IdentifiersIDs id = Element.IdentifiersIDs.Class;
                            bool bEnabled = false;
                            Color color = Color.Black;

                            string[] subValues = regexComma.Split(mainValue);
                            for (int i = 0; i < subValues.Length; ++i)
                            {
                                switch (i)
                                {
                                    case 0:
                                        id = (Element.IdentifiersIDs)Convert.ToInt32(subValues[i]);
                                        break;
                                    case 1:
                                        bEnabled = Convert.ToBoolean(subValues[i]);
                                        break;
                                    case 2:
                                        color = Color.FromArgb(Convert.ToInt32(subValues[i]));
                                        break;
                                }
                            }

                            m_ItemsInfo.Add(id, new KeyValuePair<bool, Color>(bEnabled, color));
                        }

                        //just make sure that everything was loaded
                        foreach (Element.IdentifiersIDs id in Enum.GetValues(typeof(Element.IdentifiersIDs)))
                        {
                            if (!m_ItemsInfo.ContainsKey(id))
                            {
                                Color color = Color.Black;

                                switch (id)
                                {
                                    case Element.IdentifiersIDs.Member_Function:
                                        color = Color.DarkBlue;
                                        break;
                                    case Element.IdentifiersIDs.Function:
                                        color = Color.Black;
                                        break;
                                    /*case Element.IdentifiersIDs.Macro_define:
                                        color = Color.Blue;
                                        break;*/
                                }

                                m_ItemsInfo.Add(id, new KeyValuePair<bool, Color>(true, color));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }
            }
        }

        public void SaveState()
        {
            try
            {
                RegistryKey regKey = Registry.CurrentUser;
                regKey = regKey.CreateSubKey(State.RegKey);
                if (null != regKey)
                {
                    regKey.SetValue(Reg_ShowParamsKey, ShowParams);
                    regKey.SetValue(Reg_ShowNamespaceKey, ShowNamespace);
                    regKey.SetValue(Reg_SortByNameKey, SortByName);
                    regKey.SetValue(Reg_LockFindbar, LockFindbar);
                    regKey.SetValue(Reg_DictInfo, MakeDictionaryString(m_ItemsInfo));
                    regKey.SetValue(Reg_BackColor1, BackColor1.ToArgb());
                    regKey.SetValue(Reg_BackColor2, BackColor2.ToArgb());
                    regKey.SetValue(Reg_SelectedColor, SelectionColor.ToArgb());
                    regKey.SetValue(Reg_FindColor, FindColor.ToArgb());
                    regKey.SetValue(Reg_Font, TypeDescriptor.GetConverter(typeof(Font)).ConvertToString(Font));
                    regKey.SetValue(Reg_CaseSensitiveFind, CaseSensitiveFind);
                    regKey.SetValue(Reg_SelectCurrentItem, SelectCurrentItem);
                    regKey.SetValue(Reg_FindOnlyName, FindOnlyName);
                    regKey.SetValue(Reg_WindowLocation, Convert.ToInt16(WindowLocation));
                    regKey.SetValue(Reg_WindowPos, TypeDescriptor.GetConverter(typeof(Point)).ConvertToString(WindowPos));
                    regKey.SetValue(Reg_Version, 100);
                    regKey.SetValue(Reg_ItemsOrder, MakeListString(ItemsOrder));
                }
            }
            catch (Exception)
            {

            }
        }

        private static string MakeListString(List<int> list)
        {
            string retVal = string.Empty;

            foreach (int item in list)
            {
                if (0 < retVal.Length)
                    retVal += ";";

                retVal += item.ToString();
            }

            return retVal;
        }

        private static string MakeDictionaryString(Dictionary<Element.IdentifiersIDs, KeyValuePair<bool, Color>> dict)
        {
            string value = string.Empty;

            foreach (KeyValuePair<Element.IdentifiersIDs, KeyValuePair<bool, Color>> dictItem in dict)
                value += Convert.ToInt32(dictItem.Key).ToString() + ',' + dictItem.Value.Key.ToString() + ',' + dictItem.Value.Value.ToArgb().ToString() + ';';

            System.Diagnostics.Trace.WriteLine(value);

            return value;
        }

        public Color BackColor1 { get; set; }
        public Color BackColor2 { get; set; }
        public Color SelectionColor { get; set; }
        public Color FindColor { get; set; }

        public Font Font { get; set; }

        public bool ShowParams { get; set; }
        public bool ShowNamespace { get; set; }
        public bool SortByName { get; set; }

        public bool LockFindbar { get; set; }
        public bool RememberLastSearch { get; set; }
        public bool CaseSensitiveFind { get; set; }
        public bool SelectCurrentItem { get; set; }
        public bool FindOnlyName { get; set; }

        public Point WindowPos { get; set; }
        public ListBoxEx.ListBoxLocation WindowLocation { get; set; }

        public Dictionary<Element.IdentifiersIDs, KeyValuePair<bool, Color>> ItemsInfo { get { return m_ItemsInfo; } }

        private Dictionary<Element.IdentifiersIDs, KeyValuePair<bool, Color>> m_ItemsInfo;
        public List<int> ItemsOrder { get; set; }

        private string Reg_ShowParamsKey = "ShowParams";
        private string Reg_ShowNamespaceKey = "ShowNamespace";
        private string Reg_SortByNameKey = "SortByName";
        private string Reg_LockFindbar = "ShowFindbar";
        private string Reg_DictInfo = "DictInfo";
        private string Reg_BackColor1 = "BackColor1";
        private string Reg_BackColor2 = "BackColor2";
        private string Reg_SelectedColor = "SelectedColor";
        private string Reg_FindColor = "FindColor";
        private string Reg_Font = "Font";
        private string Reg_CaseSensitiveFind = "CaseSensitiveFind";
        private string Reg_SelectCurrentItem = "SelectCurrentItem";
        private string Reg_FindOnlyName = "FindOnlyName";
        private string Reg_WindowPos = "WindowPos";
        private string Reg_WindowLocation = "WindowLocation";
        private string Reg_Version = "Version";
        private string Reg_ItemsOrder = "ItemsOrder";

        private static int ItemOrderCount = 6;

        public static Color defaultBackColor1 = SystemColors.Control;
        public static Color defaultBackColor2 = SystemColors.Control;
        public static Color defaultSelectionColor = SystemColors.Highlight;
        public static Color defaultFindColor = Color.Yellow;
        public static Font defaultFont = new Font("Consolas", 10);
        public static string RegKey = "Software\\SauliusZ\\CppBrowser";
    }
}
