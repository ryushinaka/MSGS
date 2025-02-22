using System.Collections.Generic;
using System.Data;
using MiniScript.MSGS.Extensions;
using UnityEngine;

namespace MiniScript.MSGS.Data
{
    public static class DataStoreWarehouse
    {
        #region private
        //static GameModification currentMod;

        //the List of "data tables" in memory currently
        static object lock_datastores;
        static DataSet datastores;
        #endregion

        #region Events
        public delegate void OnDataWarehouseEventA();
        public delegate void OnDataWarehouseEventB(string msg);
        public delegate void OnDataWarehouseEventC(string msg, ValMap row);
        public static OnDataWarehouseEventB OnDataWarehouseGameModLoad;
        public static OnDataWarehouseEventB OnDataWarehouseGameModUnload;
        public static OnDataWarehouseEventB OnDataWarehouseStoreCreated;
        public static OnDataWarehouseEventB OnDataWarehouseStoreDeleted;
        public static OnDataWarehouseEventB OnDataWarehouseStoreModified;
        public static OnDataWarehouseEventB OnDataWarehouseStoreLoaded;
        public static OnDataWarehouseEventB OnDataWarehouseStoreUnloaded;
        public static OnDataWarehouseEventC OnDataWarehouseStoreRowAdd;
        public static OnDataWarehouseEventC OnDataWarehouseStoreRowRemoved;
        public static OnDataWarehouseEventC OnDataWarehouseStoreRowModified;
        public static OnDataWarehouseEventA OnDataWarehouseInitialized;
        public static OnDataWarehouseEventB OnDataWarehouseStateSaved;
        public static OnDataWarehouseEventB OnDataWarehouseStateLoaded;
        public static OnDataWarehouseEventB OnDataWarehouseStateDeleted;
        public static OnDataWarehouseEventA OnDataWarehouseAutoSaved;
        #endregion

        public static void UpdateInstance(string table, ref ValMap map)
        {
            for (int i = 0; i < datastores.Tables[table].Rows.Count; i++)
            {
                string rowpid = datastores.Tables[table].Rows[i]["PrimaryKey"].ToString();
                string mappid = map["PrimaryKey"].ToString();
                if (rowpid.Equals(mappid))
                {
                    foreach (DataColumn dc in datastores.Tables[table].Columns)
                    {
                        if (dc.GetType() == typeof(double))
                        {
                            datastores.Tables[table].Rows[i][dc] = map[dc.ColumnName].DoubleValue();
                        }
                        else if (dc.GetType() == typeof(string))
                        {
                            datastores.Tables[table].Rows[i][dc] = map[dc.ColumnName].ToString();
                        }
                    }
                    i = datastores.Tables[table].Rows.Count;
                }
            }
        }

        public static void UnloadMod()
        {
            datastores.Clear();
        }

        static bool IsDataType(ref DataSet set)
        {
            if (set.Tables.Contains("Config"))
            {
                if (set.Tables["Config"].Columns.Contains("Type")
                    && set.Tables["Config"].Columns.Contains("Name")
                    && set.Tables["Config"].Rows.Count == 1)
                {
                    if (set.Tables["Config"].Rows[0]["Type"].ToString() == "datatype" &&
                        set.Tables.Count == 2) { return true; }
                }
            }

            return false;
        }

        static bool IsSaveState(ref DataSet set)
        {
            if (set.Tables.Contains("Config"))
            {
                if (set.Tables["Config"].Columns.Contains("Type") && set.Tables["Config"].Columns.Contains("Name") && set.Tables["Config"].Rows.Count == 1)
                {
                    DataRow dr = set.Tables["Config"].Rows[0];
                    if (dr["Type"].ToString() == "savestate")
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static DataTable GetTable(ref DataSet set)
        {
            foreach (DataTable dt in set.Tables)
            {
                if (dt.TableName != "Config") { return dt; }
            }

            return null;
        }

        public static bool Contains(string label)
        {
            if (datastores.Tables.Contains(label)) { return true; }
            return false;
        }

        public static ValList Select(string label, string prop, string value)
        {
            if (Contains(label))
            {
                if (datastores.Tables[label].Columns.Contains(prop))
                {
                    ValList list = new ValList();
                    if (datastores.Tables[label].Columns[prop].DataType == typeof(string))
                    {
                        //var rows = datastores.Tables[label].Select(prop + "='" + value + "'");
                        foreach (DataRow dr in datastores.Tables[label].Rows) {
                            if ((string)dr[prop] == value) { list.values.Add(dr.ToValMap()); }
                        }
                    }
                    else if (datastores.Tables[label].Columns[prop].DataType == typeof(double))
                    {
                        //var rows = datastores.Tables[label].Select(prop + "=" + value.ToString());
                        foreach (DataRow dr in datastores.Tables[label].Rows) {
                            if ((double)dr[prop] == double.Parse(value)) { list.values.Add(dr.ToValMap()); }
                        }
                    }

                    return list;
                }
            }
            return new ValList();
        }

        public static ValList GetStates()
        {
            throw new System.NotImplementedException("");
            //ValList rst = new ValList();
            //string[] filenames = System.IO.Directory.GetFiles(System.IO.Path.Combine(new string[] {
            //    new ApplicationConfig().Path, currentMod.ModHashID, "saves" }));
            //foreach (string kv in filenames)
            //{
            //    rst.values.Add(new ValString(kv.Substring(0, kv.Length - 4)));
            //}

            //return rst;
        }

        public static void StateSave(string label)
        {
            //if (string.Equals(label.ToLower(), "autosave"))
            //{
            //    MiniScriptSingleton.LogWarning("A save state can not be named 'autosave' as it is a reserved keyword. Did you mean to call CreateAutosave?");
            //    return;
            //}

            //string path = string.Empty;

            ////ensure that the saves folder exists
            //path = System.IO.Path.Combine(new string[]
            //{
            //    new ApplicationConfig().Path,
            //    ModManagementSingleton.Instance.CurrentMod.ModHashID,
            //    "saves"});
            //if (!System.IO.Directory.Exists(path)) { System.IO.Directory.CreateDirectory(path); }

            ////create full path with filename+extension
            //path = System.IO.Path.Combine(new string[] { path, label + ".xml" });

            ////create the DataSet for everything currently loaded into memory
            //DataSet master = new DataSet(label); //the DataSetName will be the name of the savestate & filename
            //#region config table
            //DataTable ddz = new DataTable("Config");
            //DataColumn ddc = new DataColumn("Type", typeof(string)); ddz.Columns.Add(ddc);
            //ddc = new DataColumn("Name", typeof(string)); ddz.Columns.Add(ddc);
            //ddz.AcceptChanges();
            //var zz = ddz.NewRow(); zz["Type"] = "savestate"; zz["Name"] = label;
            //ddz.Rows.Add(zz); //add the config data row to the table
            //master.Tables.Add(ddz); //add the config table to the dataset
            //#endregion

            //lock (lock_datastores)
            //{
            //    for (int i = 0; i < datastores.Tables.Count; i++)
            //    {
            //        master.Tables.Add(datastores.Tables[i].Copy());
            //    }
            //}

            //master.AcceptChanges();
            ////write the savestate to the file
            //master.WriteXml(path, XmlWriteMode.WriteSchema);

            ////notify any handlers that the state was saved
            //if (OnDataWarehouseStateSaved != null) { OnDataWarehouseStateSaved(label); }
        }

        public static void StateLoad(string label)
        {
            //string path;

            //path = System.IO.Path.Combine(new string[] {
            //    new ApplicationConfig().Path,
            //    ModManagementSingleton.Instance.CurrentMod.ModHashID,
            //    "states",
            //    label + ".xml" });


            //bool found = System.IO.File.Exists(path);

            //if (found)
            //{
            //    DataSet set = new DataSet();
            //    set.ReadXml(path, XmlReadMode.ReadSchema);
            //    if (IsSaveState(ref set))
            //    {
            //        lock (lock_datastores)
            //        {
            //            datastores.Clear();
            //            for (int i = 0; i < set.Tables.Count; i++)
            //            {
            //                if (set.Tables[i].TableName != "Config")
            //                {
            //                    datastores.Tables.Add(set.Tables[i].Copy());
            //                }
            //            }
            //        }
            //        if (OnDataWarehouseStoreLoaded != null) { OnDataWarehouseStoreLoaded(label); }
            //    }
            //    else
            //    {
            //        MiniScriptSingleton.LogError("StateLoad() could not be completed for '" + label + "' as it was not a recognized format.");
            //    }
            //}
            //else
            //{
            //    MiniScriptSingleton.LogWarning("StateLoad() was attempted on a label that was not found: '" + label + "'");
            //}
        }

        public static void StateDelete(string label)
        {
            //string path;
            //if (ModManagementSingleton.Instance.CurrentMod == null)
            //{
            //    path = System.IO.Path.Combine(new string[] { Application.persistentDataPath, label, ".xml" });
            //}
            //else
            //{
            //    //ensure that the saves folder exists
            //    path = System.IO.Path.Combine(new string[] {
            //    new ApplicationConfig().Path,
            //    ModManagementSingleton.Instance.CurrentMod.ModHashID,
            //    "states",
            //    label + ".xml" });
            //}

            //bool found = System.IO.File.Exists(path);
            //if (found)
            //{
            //    System.IO.File.Delete(path);
            //    if (OnDataWarehouseStateDeleted != null) { OnDataWarehouseStateDeleted(label); }
            //}
            //else
            //{
            //    MiniScriptSingleton.LogWarning("StateDelete() on label '" + label + "' could not be found to delete.");
            //}
        }

        public static void StateAutoSave()
        {
            //string label = "autosave";

            //string path;
            //if (ModManagementSingleton.Instance.CurrentMod == null)
            //{
            //    path = System.IO.Path.Combine(new string[] { Application.persistentDataPath, label, ".xml" });
            //}
            //else
            //{
            //    path = System.IO.Path.Combine(new string[]
            //    {
            //    new ApplicationConfig().Path,
            //    ModManagementSingleton.Instance.CurrentMod.ModHashID,
            //    "saves", label, ".xml" });
            //}

            ////create the DataSet for everything currently loaded into memory
            //DataSet master = new DataSet(label); //the DataSetName will be the name of the savestate & filename
            //#region config table
            //DataTable ddz = new DataTable("Config");
            //DataColumn ddc = new DataColumn("Type", typeof(string)); ddz.Columns.Add(ddc);
            //ddc = new DataColumn("Name", typeof(string)); ddz.Columns.Add(ddc);
            //ddz.AcceptChanges();
            //var zz = ddz.NewRow(); zz["Type"] = "savestate"; zz["Name"] = label;
            //ddz.Rows.Add(zz); //add the config data row to the table
            //master.Tables.Add(ddz); //add the config table to the dataset
            //#endregion

            //lock (lock_datastores)
            //{
            //    foreach (DataTable zzdt in datastores.Tables)
            //    {
            //        master.Tables.Add(zzdt.Copy());
            //    }
            //}

            //master.AcceptChanges();
            ////write the savestate to the file
            //master.WriteXml(path, XmlWriteMode.WriteSchema);
            //if (OnDataWarehouseAutoSaved != null) { OnDataWarehouseAutoSaved(); }
        }

        public static void DataStoreUnload(string label, bool save)
        {
            if (Contains(label))
            {
                if (save) { DataStoreSave(label); }

                var dt = datastores.Tables[label];
                lock (lock_datastores) { datastores.Tables.Remove(dt); }
                if (OnDataWarehouseStoreUnloaded != null) { OnDataWarehouseStoreUnloaded(label); }
            }
        }

        public static void DataStoreLoad(string label, bool replace)
        {
            //string path = System.IO.Path.Combine(new string[] {
            //    new ApplicationConfig().Path,
            //    ModManagementSingleton.Instance.CurrentMod.ModHashID,
            //    "data",
            //    label + ".xml"
            //    });
            //bool found = System.IO.File.Exists(path);

            //if (found)
            //{
            //    DataSet set = new DataSet();
            //    set.ReadXml(path, XmlReadMode.ReadSchema);
            //    if (IsDataType(ref set))
            //    {
            //        if (Contains(label))
            //        {
            //            if (replace)
            //            {
            //                var replacement = new DataTable();
            //                foreach (DataTable dt in set.Tables)
            //                {
            //                    if (dt.TableName != "Config")
            //                    {
            //                        replacement = dt.Copy();
            //                        break;
            //                    }
            //                }

            //                lock (lock_datastores)
            //                {
            //                    var dt = datastores.Tables[label];
            //                    datastores.Tables.Remove(dt);
            //                    datastores.Tables.Add(replacement);
            //                }
            //            }
            //            else
            //            {
            //                MiniScriptSingleton.LogWarning("DataStoreLoad called on '" + label + "' which is already in memory. The 'replace' bool was false.");
            //            }
            //        }
            //        else
            //        {
            //            foreach (DataTable dt in set.Tables)
            //            {
            //                if (dt.TableName != "Config")
            //                {
            //                    lock (lock_datastores)
            //                    {
            //                        datastores.Tables.Add(dt.Copy());
            //                    }
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        MiniScriptSingleton.LogError("DataStoreLoad could not find a matching data type of '" + label + "'");
            //    }
            //}
        }

        public static void DataStoreCreate(string label)
        {
            if (string.Equals(label.ToLower(), "autosave"))
            {
                MiniScriptSingleton.LogWarning("A DataStore can not be named 'autosave' as it is a reserved keyword.");
                return;
            }

            if (!Contains(label))
            {
                DataTable blah = new DataTable(label);
                DataColumn col = new DataColumn("PrimaryKey", typeof(string));
                blah.Columns.Add(col); blah.AcceptChanges();
                lock (lock_datastores) { datastores.Tables.Add(blah); }
            }
            else
            {
                MiniScriptSingleton.LogError("CreateDataStore() was given a 'typename' argument of \"" +
                        label + "\", matching one that already exists.");
            }
        }

        public static void DataStoreCreate(ref DataSet set)
        {
            throw new System.NotImplementedException("Datastorewarehouse.Datastorecreate");
        }

        public static void DataStoreDelete(string label)
        {
            throw new System.NotImplementedException("");

            //string path = System.IO.Path.Combine(new string[] {
            //    new ApplicationConfig().Path,
            //    "data",
            //    label + ".xml"
            //    });
            //bool found = System.IO.File.Exists(path);

            ////unload the Type from the collection first, without saving it
            //DataStoreUnload(label, false);
            ////delete its file copy if one exists
            //if (found) { System.IO.File.Delete(path); }
        }

        public static void DataStoreSave(string label)
        {
            if (Contains(label))
            {
                DataSet set = new DataSet("DataStore");
                DataTable cfg = new DataTable("Config");
                cfg.Columns.Add(new DataColumn("Type", typeof(string)));
                cfg.Columns.Add(new DataColumn("Name", typeof(string)));
                cfg.AcceptChanges();
                var cdr = cfg.NewRow();
                cdr["Type"] = "datatype"; cdr["Name"] = label;
                cfg.Rows.Add(cdr);
                set.Tables.Add(cfg); //add the config table for metadata

                var blah = datastores.Tables[label];
                lock (lock_datastores)
                {
                    set.Tables.Add(blah.Copy());
                }

                set.AcceptChanges();


                throw new System.NotImplementedException("Finish making changes to the data store system.");
                //set.WriteXml(
                //System.IO.Path.Combine(new string[] { new ApplicationConfig().Path,
                //                        ModManagementSingleton.Instance.CurrentMod.ModHashID,
                //                        "data", label + ".xml" }),
                //        XmlWriteMode.WriteSchema);
            }
            else
            {
                MiniScriptSingleton.LogWarning("DataStoreSave could not find the Type '" + label + "'");
            }
        }

        public static ValMap DataStoreGetRandomInstance(string label)
        {
            if (Contains(label))
            {
                if (datastores.Tables[label].Rows.Count == 0) { return new ValMap(); }

                var idx = UnityEngine.Random.Range(0, datastores.Tables[label].Rows.Count);
                var row = datastores.Tables[label].Rows[idx];
                return row.ToValMap();
            }
            else
            {
#if UNITY_EDITOR
                MiniScriptSingleton.LogError("GetRandomInstance could not find Type '" + label + "'");
#endif
            }
            return null;
        }

        public static ValList DataStoreGetRandomInstances(string label, int quantity, bool unique)
        {
            if (Contains(label))
            {
                var table = datastores.Tables[label];
                if (table.Rows.Count >= quantity)
                {
                    if (unique)
                    {
                        ValList rst = new ValList();
                        for (int i = 0; i < quantity; i++)
                        {
                            var vm = DataStoreGetRandomInstance(label);
                            bool found = false;
                            foreach (ValMap vlm in rst.values)
                            {
                                if (vlm["PrimaryKey"].ToString() == vm["PrimaryKey"].ToString())
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found) { rst.values.Add(vm); }
                            else { i--; } //we pulled a duplicate
                        }
                        return rst;
                    }
                    else //not unique
                    {
                        ValList rst = new ValList();
                        for (int i = 0; i < quantity; i++)
                        {
                            rst.values.Add(DataStoreGetRandomInstance(label));
                        }
                        return rst;
                    }
                }
                else if (table.Rows.Count < quantity)
                {
                    MiniScriptSingleton.LogError("GetRandomInstances() was requested on Type '" + label +
                        "' which only has " + table.Rows.Count + " instances.  This makes it unable to fulfill the request of " +
                        quantity + " instances to be returned.");
                    return null;
                }

                return null;
            }
            else
            {
                MiniScriptSingleton.LogError("GetRandomInstances() could not find the Type '" + label + "'.");
                return null;
            }
        }

        public static ValList DataStoreGetInstances(string label)
        {
            if (Contains(label))
            {
                for (int i = 0; i < datastores.Tables.Count; i++)
                {
                    if (datastores.Tables[i].TableName == label)
                    {
                        return datastores.Tables[i].ToValList();
                    }
                }
            }
            else
            {
                MiniScriptSingleton.LogError("GetInstances could not find Type '" + label + "'");
            }

            return null;
        }

        public static ValMap DataStoreGetInstanceAtIndex(string label, int index)
        {
            if (Contains(label))
            {
                if (index >= 0 && index <= datastores.Tables[label].Rows.Count)
                {
                    return datastores.Tables[label].Rows[index].ToValMap();
                }

                MiniScriptSingleton.LogError("GetInstanceAtIndex 'index' value of '" + index +
                    "' was outside the range available. Upper='" + datastores.Tables[label].Rows.Count + "'");
            }
            else
            {
                MiniScriptSingleton.LogError("GetInstanceAtIndex could not find Type '" + label + "'");
            }

            return null;
        }

        public static void DataStoreDestroyInstance(string label, ValMap row)
        {
            if (Contains(label))
            {
                if (datastores.Tables[label].Rows.Count == 0) { return; }

                DataTable dt = datastores.Tables[label];
                DataRow[] rows = dt.Select("PrimaryKey='" + row["PrimaryKey"].ToString() + "'");
                if (rows.Length == 1)
                {
                    lock (lock_datastores) { dt.Rows.Remove(rows[0]); dt.AcceptChanges(); }
                }
                else if (rows.Length == 0)
                {
                    MiniScriptSingleton.LogWarning("DestroyInstance with ID(" + row["PrimaryKey"].ToString() + ") did not find any instances.");
                }
                else if (rows.Length > 1)
                {
                    MiniScriptSingleton.LogWarning("DestroyInstance with ID(" + row["PrimaryKey"].ToString() + ") found multiple instances.");
                }
            }
            else
            {
                MiniScriptSingleton.LogError("DestroyInstance could not find Type '" + label + "'");
            }
        }

        public static void DataStoreDestroyInstances(string label, ValList rows)
        {
            if (Contains(label))
            {
                foreach (Value v in rows.values)
                {
                    if (v is ValMap)
                    {
                        var vm = v as ValMap;
                        DataStoreDestroyInstance(label, vm);
                    }
                }
            }
        }

        public static void DataStoreCreateInstance(string label, out ValMap rst)
        {
            DataRow row = null;
            if (datastores.Tables.Contains(label))
            {
                row = datastores.Tables[label].NewRow();
                row["PrimaryKey"] = System.Guid.NewGuid().ToString();
                lock (lock_datastores) { datastores.Tables[label].Rows.Add(row); }
                if (OnDataWarehouseStoreRowAdd != null) { OnDataWarehouseStoreRowAdd(label, row.ToValMap()); }
            }

            if (row != null)
            {
                var tmpvm = row.ToValMap();
                rst = tmpvm;
                //assign Llambda delegate to handle the assignment operation for the ValMap
                rst.assignOverride = (key, value) =>
                {
                    string table = label;
                    //we cant let the scripts modify the PrimaryKey value
                    if (key.ToString() == "PrimaryKey") { return true; }

                    if (datastores.Tables[table].Columns.Contains(key.ToString()))
                    {   //select the datarow from the table
                        var dr = datastores.Tables[table].Select("PrimaryKey='" + tmpvm["PrimaryKey"].ToString() + "'");
                        //modify the datarow column
                        if (value is ValString &&
                            datastores.Tables[table].Columns[key.ToString()].DataType == typeof(string))
                        { dr[0][key.ToString()] = value.ToString(); }
                        else if (value is ValNumber &&
                            datastores.Tables[table].Columns[key.ToString()].DataType == typeof(double))
                        { dr[0][key.ToString()] = value.DoubleValue(); }

                        return true;
                    }
                    else  //table does not contain the column specified by key.ToString()
                    {
                        MiniScriptSingleton.LogWarning("Attempt to modify a ValMap from a DataStore('" + table + "'), the specified" +
                            " attribute ('" + key.ToString() + "') could not be found in the ValMap.");
                        return false; //cancel assignment
                    }
                };
                return;
            }
            else //could not find the datatable with 'label' name
            {
#if UNITY_EDITOR
                MiniScriptSingleton.LogError("CreateInstance() failed! The Type '" + label + "' could not be found.");
#endif
            }

            rst = null;
        }

        public static void DataStoreCreateInstances(string label, int quantity, out ValList lst)
        {
            if (Contains(label))
            {
                lst = new ValList();
                int count = 0;
                while (count < quantity)
                {
                    ValMap vm = new ValMap();
                    DataStoreCreateInstance(label, out vm);
                    lst.values.Add(vm);
                    count++;
                }
                return;
            }
            else
            {
#if UNITY_EDITOR
                MiniScriptSingleton.LogError("CreateInstances failed to find type ('" + label + "')");
                lst = null;
#endif
            }
            lst = new ValList();
        }

        public static void DataStoreInstanceCount(string label, out ValNumber qty)
        {
            if (datastores.Tables.Contains(label))
            {
                qty = new ValNumber(datastores.Tables[label].Rows.Count);
                return;
            }
            else
            {
#if UNITY_EDITOR
                MiniScriptSingleton.LogError("InstanceCount could not find the Type '" + label + "'");
#endif
                qty = new ValNumber(0);
            }
        }

        public static void DataStoreHasAttribute(string label, string aname, out ValNumber tf)
        {
            if (datastores.Tables.Contains(label))
            {
                if (datastores.Tables[label].Columns.Contains(aname))
                {
                    tf = ValNumber.one;
                    return;
                }
                else
                {
                    tf = ValNumber.zero;
                    return;
                }
            }

            tf = ValNumber.zero; //false by default            

#if UNITY_EDITOR
            MiniScriptSingleton.LogError("HasAttribute could not find the Type '" + label + "'");
#endif
            tf = ValNumber.zero;
        }

        public static void DataStoreAddAttribute(string label, string aname, Value v, out ValNumber tf)
        {
            if (aname.ToLower() == "primarykey")
            {
                MiniScriptSingleton.LogWarning("AttributeAdd on " + label + " failed since the Attribute name of 'PrimaryKey' is reserved and can not be used.");
                tf = ValNumber.zero;
                return;
            }

            if (datastores.Tables.Contains(label))
            {
                if (!datastores.Tables[label].Columns.Contains(aname))
                {
                    if (v is ValString) { datastores.Tables[label].Columns.Add(new DataColumn(aname, typeof(string))); tf = ValNumber.one; }
                    else if (v is ValNumber) { datastores.Tables[label].Columns.Add(new DataColumn(aname, typeof(double))); tf = ValNumber.one; }
                    else
                    {
                        tf = ValNumber.zero;
                        if (v != null)
                        {
                            MiniScriptSingleton.LogError("AttributeAdd on " + label + " failed since the argument type given was neither ValNumber nor ValString, but " +
                                "was a " + v.GetType().Name);                            
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("AttributeAdd on " + label + " failed since the argument type given was null.");
                        }
                    }
                    return;
                }
                else
                {
                    MiniScriptSingleton.LogError("AttributeAdd on " + label + " failed since the Type already has an attribute of the same name.");
                    tf = ValNumber.zero;
                    return;
                }
            }
            else
            {
#if UNITY_EDITOR
                MiniScriptSingleton.LogError("AttributeAdd failed to find Type '" + label + "'");
#endif
            }


            tf = ValNumber.zero;
        }

        public static void DataStoreRemoveAttribute(string label, string aname, out ValNumber tf)
        {
            if (aname.ToLower() == "primarykey")
            {
                MiniScriptSingleton.LogWarning("AttributeRemove can not remove the PrimaryKey property of the Type '" + label + "'");
                tf = ValNumber.zero; //false
                return;
            }

            if (datastores.Tables.Contains(label))
            {
                if (datastores.Tables[label].Columns.Contains(aname))
                {
                    datastores.Tables[label].Columns.Remove(aname);
                    tf = ValNumber.one; return;
                }
                else
                {
                    MiniScriptSingleton.LogError("AttributeRemove on " + label + " failed since the Type does not have an attribute of the given name.");
                    tf = ValNumber.zero;
                    return;
                }
            }
            tf = ValNumber.zero;
        }

        public static List<string> DataStoreList()
        {
            List<string> rst = new List<string>();
            lock (lock_datastores)
            {
                foreach (DataTable s in datastores.Tables)
                {
                    rst.Add(s.TableName);
                }
            }
            return rst;
        }

        static DataStoreWarehouse()
        {
            datastores = new DataSet();
            lock_datastores = new object();

            //strangely it is possible for the StreamingAssets folder to not exist when the application process is run
            //so lets verify that it does exist
            if (!System.IO.Directory.Exists(Application.streamingAssetsPath)) { System.IO.Directory.CreateDirectory(Application.streamingAssetsPath); }
            //
            //if (!System.IO.File.Exists(System.IO.Path.Combine(new string[] { Application.streamingAssetsPath, "workingset.xml" })))
            //{
            //    var set = CreateDefaultWorkingSet();
            //    set.WriteXml(System.IO.Path.Combine(new string[] { Application.streamingAssetsPath, "workingset.xml" }), XmlWriteMode.WriteSchema);
            //}
            //else
            //{

            //}
        }

        static DataSet CreateDefaultWorkingSet()
        {

            return null;
        }
    }
}

