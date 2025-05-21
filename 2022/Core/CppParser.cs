using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace CppBrowser.Core
{
    using StringInt = System.Collections.Generic.KeyValuePair<string, int>;
    using StringIntList = System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, int>>;

    public class PeekableStreamReaderAdapter
    {
        private StreamReader Underlying;
        private Queue<string> BufferedLines;

        public PeekableStreamReaderAdapter(StreamReader underlying)
        {
            Underlying = underlying;
            BufferedLines = new Queue<string>();
        }

        public string PeekLine()
        {
            string line = Underlying.ReadLine();
            if (line == null)
                return null;
            BufferedLines.Enqueue(line);
            return line;
        }


        public string ReadLine()
        {
            if (BufferedLines.Count > 0)
                return BufferedLines.Dequeue();
            return Underlying.ReadLine();
        }

        public void Close()
        {
            Underlying.Close();
        }
    }

    class CppParser
    {
        private string m_AdditionalNamespaces;
        private int m_nCurrentLine;
        private int m_nOpenNamespacesCount;

        public CppParser() { m_AdditionalNamespaces = string.Empty; m_nCurrentLine = 0; m_nOpenNamespacesCount = 0; }

        public System.Collections.Generic.List<Element> ParseFile(string filePath)
        {
            try
            {
                System.Collections.Generic.List<Element> array = new System.Collections.Generic.List<Element>();

                PeekableStreamReaderAdapter file = new PeekableStreamReaderAdapter(File.OpenText(filePath));

                Element info = null;
                while (FindFunctionDeclaration(ref file, ref info))
                {
                    if (null != info)
                        array.Add(info);
                }
                file.Close();

                return (array.Count > 0 ? array : null);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }

            return null;
        }

        // if returned true further inspection is needed
        private bool HandleCommentsAndStringLiterals(ref string input, ref bool bSmartComment)
        {
            if (0 == input.Length)
                return false;

            if (!bSmartComment && input.StartsWith("#"))
            {
                input = string.Empty;
                return false;
            }

            int nSimpleComment = input.IndexOf("//");
            if (0 == nSimpleComment && !bSmartComment)
            {
                input = string.Empty;
                return false;
            }

            if (0 == input.IndexOf("IMPLEMENT_"))
            {
                input = string.Empty;
                return false;
            }


            int nSmartCommentBegin = input.IndexOf("/*");
            int nStringLiteral = input.IndexOf("\"");

            if (!bSmartComment && -1 == nSimpleComment && -1 == nSmartCommentBegin && -1 == nStringLiteral)
                return false;

            if (-1 == nSimpleComment)
                nSimpleComment = int.MaxValue;

            if (-1 == nSmartCommentBegin)
                nSmartCommentBegin = int.MaxValue;

            if (-1 == nStringLiteral)
                nStringLiteral = int.MaxValue;

            if (!bSmartComment)
            {
                if (nSmartCommentBegin < nSimpleComment && nSmartCommentBegin < nStringLiteral)
                {
                    string temp = input.Substring(0, nSmartCommentBegin);
                    input = input.Substring(nSmartCommentBegin);
                    int nSmartCommentEnd = input.IndexOf("*/");
                    if (-1 == nSmartCommentEnd)
                    {
                        bSmartComment = true;
                        input = temp;
                        return false;
                    }
                    else
                    {
                        bSmartComment = false;
                        input = input.Substring(nSmartCommentEnd + 2);
                        input = temp + input;
                        return true;
                    }

                }
                else if (nSimpleComment < nSmartCommentBegin && nSimpleComment < nStringLiteral)
                {
                    input = input.Substring(0, nSimpleComment);
                    return false;
                }
                else if (nStringLiteral != int.MaxValue)
                {
                    string BeginingOfString = input.Substring(0, nStringLiteral);
                    input = input.Substring(nStringLiteral + 1);
                    int nEndOfLiteral = 0;
                    while (true)
                    {
                        nEndOfLiteral = input.IndexOf("\"", nEndOfLiteral > 0 ? nEndOfLiteral + 1 : nEndOfLiteral);
                        if (0 >= nEndOfLiteral)
                            break;
                        else if (1 == nEndOfLiteral)
                        {
                            if ('\\' != input[0])
                                break;
                        }
                        else if ('\\' == input[nEndOfLiteral - 2] && '\\' == input[nEndOfLiteral - 1] || '\\' != input[nEndOfLiteral - 1])
                        {
                            break;
                        }
                    }

                    if (-1 != nEndOfLiteral)
                        input = BeginingOfString + input.Substring(nEndOfLiteral + 1);
                    else
                        input = BeginingOfString;

                    return true; // might be that there is something after the string
                }
            }
            else
            {
                int nSmartCommentEnd = input.IndexOf("*/");

                if (-1 == nSmartCommentEnd)
                {
                    input = string.Empty;
                    return false;
                }
                else
                {
                    bSmartComment = false;
                    input = input.Substring(nSmartCommentEnd + 2);
                    return true;
                }
            }

            return false;
        }

        private bool FindFunctionDeclaration(ref PeekableStreamReaderAdapter file, ref Element info)
        {
            string input = null;
            StringIntList lines = new StringIntList();
            int nOpenBrackets = 0;
            bool bSmartComment = false;
            while ((input = file.ReadLine()) != null)
            {
                ++m_nCurrentLine;

                while (HandleCommentsAndStringLiterals(ref input, ref bSmartComment)) { }

                if (0 == input.Length)
                    continue;

                //remove all multiple spaces and tabs
                //so that we could detect comments
                input = new Regex("\\s*::\\s*").Replace(new Regex("\\s+").Replace(input, " "), "::");

                string strNamespace = FindNamespace(ref file, input);
                if (null != strNamespace)
                {
                    m_AdditionalNamespaces += strNamespace + "::";
                    ++m_nOpenNamespacesCount;
                    --nOpenBrackets;
                    continue;
                }

                lines.Add(new StringInt(input, m_nCurrentLine));

                if (input.Contains('{') || input.Contains("BEGIN_MESSAGE_MAP"))
                    ++nOpenBrackets;

                if (input.Contains('}') || input.Contains("END_MESSAGE_MAP"))
                {
                    --nOpenBrackets;
                    if (0 > nOpenBrackets)
                    {
                        nOpenBrackets = 0;
                        --m_nOpenNamespacesCount;
                        if (0 == m_nOpenNamespacesCount)
                        {
                            m_AdditionalNamespaces = string.Empty;
                            lines.Clear();
                        }
                        else
                        {
                            m_AdditionalNamespaces = m_AdditionalNamespaces.TrimEnd(':');
                            int nPos = m_AdditionalNamespaces.LastIndexOf("::");
                            if (-1 != nPos)
                            {
                                m_AdditionalNamespaces = m_AdditionalNamespaces.Substring(0, nPos + 2);
                            }
                        }
                    }
                    else if (0 == nOpenBrackets)
                    {
                        // we found end of function declaration
                        // now we must find beginning
                        info = ParseBlock(lines);
                        return true;
                    }
                }
            }

            return false;
        }

        private string FindNamespace(ref PeekableStreamReaderAdapter file, string line)
        {
            Regex namespacePresent = new Regex("namespace\\s+.+|class\\s+.+");
            Match match = namespacePresent.Match(line);
            if (match.Success)
            {
                while (true)
                {
                    string nextLine = file.PeekLine();
                    if (null == nextLine)
                    {
                        return null;
                    }

                    if (nextLine.StartsWith("#") || nextLine.StartsWith("//"))
                    {
                        nextLine = string.Empty;
                        continue;
                    }
                    else if (nextLine.Contains('{'))
                    {
                        line = line.Trim();
                        int nPos = line.IndexOf(' ');
                        if (-1 != nPos)
                        {
                            line = line.Substring(nPos + 1);
                            nPos = line.IndexOf(' ');
                            if (-1 != nPos)
                            {
                                line = line.Substring(0, nPos);
                                line = line.Trim();
                            }

                            return line;
                        }
                    }
                }
            }

            return null;
        }

        private Element ParseBlock(StringIntList lines)
        {
            string function = string.Empty;

            Regex removeMultipleSpaces = new Regex("\\s+");
            Regex fixOperator = new Regex("operator\\s+");
            Regex functionName = new Regex("[^;}\\s]+\\s*\\([^;]*\\)\\s*{");
            Regex fixColon = new Regex("public:|protected:|private:");
            foreach (StringInt strLine in lines)
            {
                function += fixColon.Replace(fixOperator.Replace(removeMultipleSpaces.Replace(strLine.Key, " "), "operator"), "");

                Match match = functionName.Match(function);
                if (match.Success)
                {
                    function = match.Value;
                    break;
                }
            }

            int nIndex = function.IndexOf('(');
            if (0 < nIndex)
            {
                Element info = new Element();
                info.ID = Core.Element.IdentifiersIDs.Function;
                {
                    // find function parameters
                    int nEnd = function.IndexOf(')');
                    if (0 < nEnd && 1 < (nEnd - nIndex))
                    {
                        string parameters = function.Substring(nIndex + 1, nEnd - nIndex - 1);
                        parameters = parameters.Trim();
                        if (0 < parameters.Length)
                            info.Value = parameters;
                    }
                }

                {
                    // find function name and namespace
                    function = function.Substring(0, nIndex);

                    info.Parent = m_AdditionalNamespaces;
                    int nNameSpace = function.IndexOf("::");
                    if (0 < nNameSpace)
                    {
                        info.ID = Core.Element.IdentifiersIDs.Member_Function;
                        info.Parent += function.Substring(0, nNameSpace).Trim().TrimStart('*');
                        function = function.Substring(nNameSpace + 2);
                    }
                    else
                    {
                        info.Parent = info.Parent.TrimEnd(':');
                    }

                    info.Name = function.Trim().TrimStart('*');
                }

                Regex matchFunction = new Regex(info.Name + "\\s*\\(");
                foreach (StringInt strLine in lines)
                {
                    if (matchFunction.Match(strLine.Key).Success)
                    {
                        info.LineBegin = strLine.Value;
                        break;
                    }
                }
                return info;
            }
            return null;
        }
    }
}
