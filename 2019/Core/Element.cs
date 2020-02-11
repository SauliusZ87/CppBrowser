using System;
using System.Collections.Generic;

namespace CppBrowser.Core
{
    public class Element
    {
        public enum IdentifiersIDs
        {
            Class = 0,
            Struct,
            Enum,
            Member_Function,
            Enumerator,
            Namespace,
            Function,
            /*Macro_define,
            Macro_undef,
            Pound_include,
            Pound_import,
            Pound_ifdef,
            Pound_ifndef,
            Typedef,*/
        }

        public static Dictionary<string, IdentifiersIDs> GetDictionaryOfCodeItems()
        {
            Dictionary<string, IdentifiersIDs> dictionary = new Dictionary<string, IdentifiersIDs>(Enum.GetValues(typeof(IdentifiersIDs)).Length);

            foreach (IdentifiersIDs id in Enum.GetValues(typeof(IdentifiersIDs)))
            {
                dictionary.Add(GetIdentifierString(id), id);
            }

            return dictionary;
        }

        public static string GetIdentifierString(IdentifiersIDs id)
        {
            switch (id)
            {
                case IdentifiersIDs.Class:
                    return "class";
                case IdentifiersIDs.Struct:
                    return "struct";
                case IdentifiersIDs.Enum:
                    return "enum";
                case IdentifiersIDs.Member_Function:
                    return "member_function";
                case IdentifiersIDs.Enumerator:
                    return "enumerator";
                case IdentifiersIDs.Namespace:
                    return "namespace";
                case IdentifiersIDs.Function:
                    return "function";
                /*case IdentifiersIDs.Macro_define:
                    return "macro_define";
                case IdentifiersIDs.Macro_undef:
                    return "macro_undef";
                case IdentifiersIDs.Pound_include:
                    return "pound_include";
                case IdentifiersIDs.Pound_import:
                    return "pound_import";
                case IdentifiersIDs.Pound_ifdef:
                    return "pound_ifdef";
                case IdentifiersIDs.Pound_ifndef:
                    return "pound_ifndef";
                case IdentifiersIDs.Typedef:
                    return "typedef";*/
                default:
                    System.Diagnostics.Debug.Assert(false);
                    return string.Empty;
            }
        }

        private string FormatNameWithPrefix(string prefix, bool bParent)
        {
            if (bParent && 0 < Parent.Length)
                return prefix + Parent + "::" + Name;
            else
                return prefix + Name;
        }

        public string FormatName(bool bParams, bool bParent)
        {
            switch (ID)
            {
                case IdentifiersIDs.Class:
                    return FormatNameWithPrefix("class ", bParent);

                case IdentifiersIDs.Struct:
                    return FormatNameWithPrefix("struct ", bParent);

                case IdentifiersIDs.Enum:
                    return FormatNameWithPrefix("enum ", bParent);

                case IdentifiersIDs.Enumerator:
                    return FormatNameWithPrefix(string.Empty, bParent);

                case IdentifiersIDs.Namespace:
                    return FormatNameWithPrefix("namespace ", bParent);

                /*case IdentifiersIDs.Macro_define:
                    string val = Name;
                    if (bParams && 0 < Value.Length)
                    {
                        int nPos = Name.IndexOf(' ');
                        if (-1 < nPos)
                        {
                            val = Name.Insert(nPos, "(" + Value + ") ");
                        }
                    }
                    return "#define " + val;

                case IdentifiersIDs.Macro_undef:
                    return "#undef " + Name;

                case IdentifiersIDs.Pound_include:
                    return "#include " + Name;

                case IdentifiersIDs.Pound_import:
                    return "#import " + Name;

                case IdentifiersIDs.Pound_ifdef:
                    return "#ifdef " + Name;

                case IdentifiersIDs.Pound_ifndef:
                    return "#ifndef " + Name;

                case IdentifiersIDs.Typedef:
                    return "typedef " + (bParams ? (Type + " ") : string.Empty) + (bParent ? (Parent + "::") : string.Empty) + Name;*/

                case IdentifiersIDs.Member_Function:
                case IdentifiersIDs.Function:
                    {
                        string FormatedName = string.Empty;
                        if (bParent && 0 < Parent.Length)
                            FormatedName += Parent + "::";

                        FormatedName += Name;

                        if (bParams)
                            FormatedName += "(" + Value + ")";

                        return FormatedName;
                    }
                default:
                    System.Diagnostics.Debug.Assert(false);
                    return string.Empty;
            }
        }

        public Element()
        {
            Name = string.Empty;
            Value = string.Empty;
            Parent = string.Empty;
            Type = string.Empty;
            LineBegin = -1;
            LineEnd = -1;
            ID = IdentifiersIDs.Class;
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public string Parent { get; set; }
        public string Type { get; set; }
        public int LineBegin { get; set; }
        public int LineEnd { get; set; }
        public IdentifiersIDs ID { get; set; }
    }
}
