using System;
using System.Collections.Generic;
using System.Data.Common;

namespace CppBrowser.Core
{
    class InfoGrabber
    {
        class FindHelper
        {
            public FindHelper(string name, long parentID)
            {
                Parent = string.Empty;
                Name = name;
                ParentID = parentID;
            }

            public string GetFullName()
            {
                if (0 < Parent.Length)
                    return Parent + "::" + Name;
                else
                    return Name;
            }

            public string Parent { get; set; }
            public string Name { get; set; }
            public long ParentID { get; set; }
            public int Index { get; set; }
        }

        public InfoGrabber()
        {
            m_RealID_To_Identifier = new Dictionary<short, Element.IdentifiersIDs>(Enum.GetValues(typeof(Element.IdentifiersIDs)).Length);
        }

        public void ResetIDs()
        {
            m_RealID_To_Identifier.Clear();
        }

        private void CollectIDs(DB.DB db)
        {
            if (0 < m_RealID_To_Identifier.Count)
                return;

            DbDataReader reader = null;

            try
            {
                reader = db.ExecuteQuery("SELECT id, name FROM code_item_kinds");

                Dictionary<string, Element.IdentifiersIDs> dictionary = Element.GetDictionaryOfCodeItems();

                while (reader.Read())
                {
                    short nId = reader.GetInt16(0);
                    string name = reader.GetString(1);

                    if (dictionary.ContainsKey(name))
                        m_RealID_To_Identifier[nId] = dictionary[name];
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (null != reader)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
        }

        public List<Element> GetInfoForFile(DB.DB db, string filePath)
        {
            if (null == db)
                return null;

            CollectIDs(db);

            DbDataReader reader = null;
            List<Element> array = null;
            try
            {

                reader = db.ExecuteQuery("SELECT id FROM files WHERE LOWER(name) LIKE '" + filePath.ToLower() + "'");

                long nFileID = -1;
                while (reader.Read())
                {
                    nFileID = reader.GetInt64(0);
                }
                reader.Close();
                reader.Dispose();

                string sql = "SELECT code_items.name, code_items_join.name, code_items.start_line, code_items.kind, code_items.id, code_items.type, code_items.parent_id, code_items.end_line FROM code_items ";
                sql += "LEFT OUTER JOIN code_items AS code_items_join ON code_items.parent_id = code_items_join.id ";
                sql += "WHERE code_items.file_id = " + nFileID.ToString() + " AND code_items.kind IN( ";

                {
                    string kinds = string.Empty;
                    foreach (short kind in m_RealID_To_Identifier.Keys)
                    {
                        if (0 == kind)
                            continue;

                        if (0 < kinds.Length)
                            kinds += ", ";


                        kinds += kind.ToString();
                    }
                    sql += kinds + ")";
                }

                reader = db.ExecuteQuery(sql);

                array = new List<Element>();
                Dictionary<long, int> idsForParameters = new Dictionary<long, int>();
                Dictionary<long, List<Element>> idsForParents = new Dictionary<long, List<Element>>();
                Dictionary<long, FindHelper> parents = new Dictionary<long, FindHelper>();
                while (reader.Read())
                {
                    Element info = new Element();
                    info.Name = reader.GetString(0);
                    info.Parent = (reader.IsDBNull(1) ? string.Empty : reader.GetString(1));
                    info.LineBegin = reader.GetInt32(2);
                    info.LineEnd = reader.GetInt32(7);
                    short kind = reader.GetInt16(3);

                    info.ID = m_RealID_To_Identifier[kind];

                    switch (info.ID)
                    {
                        /*case Element.IdentifiersIDs.Typedef:
                            info.Type = reader.IsDBNull(5) ? string.Empty : reader.GetString(5).Trim((char)1).Trim();
                            break;*/
                        case Element.IdentifiersIDs.Function:
                            if(0 < info.Parent.Length)
                                info.ID = Element.IdentifiersIDs.Member_Function;
                            break;

                    }

                    switch (info.ID)
                    {
                        case Element.IdentifiersIDs.Function:
                        case Element.IdentifiersIDs.Member_Function:
                        //case Element.IdentifiersIDs.Macro_define:
                            idsForParameters.Add(reader.GetInt64(4), array.Count);
                            break;
                    }

                    {
                        long nParentID = reader.IsDBNull(6) ? 0 : reader.GetInt64(6);
                        switch (info.ID)
                        {
                            case Element.IdentifiersIDs.Class:
                            case Element.IdentifiersIDs.Struct:
                            case Element.IdentifiersIDs.Namespace:
                            case Element.IdentifiersIDs.Enum:
                            case Element.IdentifiersIDs.Enumerator:
                            //case Element.IdentifiersIDs.Typedef:
                                parents.Add(reader.GetInt64(4), new FindHelper(info.Name, nParentID));
                                break;
                        }

                        if (0 < nParentID)
                        {
                            if (!idsForParents.ContainsKey(nParentID))
                                idsForParents.Add(nParentID, new List<Element>(1));

                            idsForParents[nParentID].Add(info);
                        }
                    }

                    array.Add(info);
                }

                reader.Close();
                reader.Dispose();

                UpdateParents(ref parents, ref idsForParents);

                sql = "SELECT parent_id, type, name FROM code_items ";
                sql += "WHERE file_id = " + nFileID.ToString();

                reader = db.ExecuteQuery(sql);
                while (reader.Read())
                {
                    long parentId = reader.GetInt64(0);
                    if (idsForParameters.ContainsKey(parentId))
                    {
                        Element info = array[idsForParameters[parentId]];
                        if (info.Value.Length > 0)
                            info.Value += ", ";

                        if (!reader.IsDBNull(1) && !reader.IsDBNull(2))
                        {
                            string value = reader.GetString(1);
                            string charToReplace = ((char)1).ToString();
                            int count = value.Length - value.Replace(charToReplace, "").Length;
                            if (1 == count)
                            {
                                info.Value += value.Replace(charToReplace, reader.GetString(2));
                            }
                            else
                            {
                                info.Value += reader.GetString(1)
                                                    .Trim((char)1)
                                                    .Replace((char)1, ' ')
                                                    .Trim() + " " + reader.GetString(2);
                            }
                        }
                        else if (reader.IsDBNull(1) && !reader.IsDBNull(2))
                            info.Value += reader.GetString(2);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (null != reader)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }

            return (null == array || 0 == array.Count ? null : array);
        }

        private void UpdateParents(ref Dictionary<long, FindHelper> parents, ref Dictionary<long, List<Element>> parentsToUpdate)
        {
            foreach (KeyValuePair<long, FindHelper> pair in parents)
            {
                long nParentID = pair.Value.ParentID;
                while (0 != nParentID)
                {
                    if (parents.ContainsKey(nParentID))
                    {
                        FindHelper helper = parents[nParentID];
                        if (0 < pair.Value.Parent.Length)
                            pair.Value.Parent = pair.Value.Parent.Insert(0, helper.Name + "::");
                        else
                            pair.Value.Parent = helper.Name;

                        nParentID = helper.ParentID;
                    }
                    else
                    {
                        nParentID = 0;
                    }
                }
            }

            foreach (KeyValuePair<long, FindHelper> pair in parents)
            {
                if (0 != pair.Key && parentsToUpdate.ContainsKey(pair.Key))
                {
                    foreach (Element element in parentsToUpdate[pair.Key])
                    {
                        element.Parent = pair.Value.GetFullName();
                    }
                }

            }

        }

        private Dictionary<short, Element.IdentifiersIDs> m_RealID_To_Identifier;
    }
}
