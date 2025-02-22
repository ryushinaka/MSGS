using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using MiniScript.MSGS.Data;

namespace MiniScript.MSGS.Extensions
{
    public static class MiniScriptDataTableExtensions
    {
        /// <summary>
        /// Returns how many Row elements there are for the DataTable
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static int InstanceCount(this DataTable table)
        {
            return table.Rows.Count;
        }

        /// <summary>
        /// Adds a DataColumn to the DataTable 
        /// </summary>
        /// <param name="table">The DataTable to add the DataColumn to</param>
        /// <param name="aname">The Name property of the DataColumn</param>
        /// <param name="value">The ValString or ValNumber to assign to the DataColumn for its DataType and DefaultValue</param>
        /// <returns></returns>
        public static bool AddAttribute(this DataTable table, ValString aname, Value value)
        {
            if (!table.Columns.Contains(aname.value))
            {
                if (value is ValString)
                {
                    table.Columns.Add(new DataColumn(aname.value, typeof(string)));
                    table.Columns[aname.value].DefaultValue = "";
                    table.AcceptChanges();
                    DataStoreWarehouse.OnDataWarehouseStoreModified.Invoke("DataStore(" + table.TableName + ") " +
                        "has had a DataColumn added, name='" + aname.value + "' and defaultValue='" + value.ToString() +"'");
                    return true;
                }
                else if (value is ValNumber)
                {
                    table.Columns.Add(new DataColumn(aname.value, typeof(double)));
                    table.Columns[aname.value].DefaultValue = 0;
                    table.AcceptChanges();
                    DataStoreWarehouse.OnDataWarehouseStoreModified.Invoke("DataStore(" + table.TableName + ") " +
                       "has had a DataColumn added, name='" + aname.value + "' and defaultValue='" + value.ToString() + "'");
                    return true;
                }

                return false;
            }

            return false;
        }

        /// <summary>
        /// Creates a new instance of the 'type' represented by this DataTable definition
        /// </summary>
        /// <param name="table">The definition of a 'type' within the scripting framework</param>
        /// <returns>A ValMap containing keys for each DataColumn with default values</returns>
        public static ValMap CreateInstance(this DataTable table)
        {            
            var row = table.NewRow(); //create the datarow object
            table.Rows.Add(row); //add it to the datatable
            var rst = table.Columns.ToValMap(); //get the datacolumn mapping
            row.Populate(ref rst); //have the datarow assign values to the ValMap
            DataStoreWarehouse.OnDataWarehouseStoreRowAdd.Invoke("DataTable('" + table.TableName + "') has added a new row," +
                "PrimaryKey='" + rst["PrimaryKey"].ToString() + "'", rst);
            return rst;
        }

        /// <summary>
        /// Commits changes made to a ValMap back to the DataTable it was created from
        /// </summary>
        /// <param name="table">The DataTable to commit the changes to</param>
        /// <param name="row">The ValMap with values for this DataTable</param>
        /// <remarks>Passing a ValMap from another DataTable that it was not created from is undefined behaviour.</remarks>
        public static void UpdateInstance(this DataTable table, ValMap row)
        {
            //verify the PrimaryKey value exists, otherwise we have no way to find it in the DataTable
            if(row.ContainsKey("PrimaryKey"))
            {
                var rows = table.Select("PrimaryKey='" + row["PrimaryKey"].ToString() + "'");
                if (rows.Length == 1)
                {
                    foreach(DataColumn dc in table.Columns)
                    {
                        if (dc.DataType == typeof(string))
                        {
                            rows[0][dc] = row[dc.ColumnName].ToString();
                        }
                        else if (dc.DataType == typeof(double))
                        {
                            rows[0][dc] = row[dc.ColumnName].DoubleValue().ToString();
                        }
                    }
                    DataStoreWarehouse.OnDataWarehouseStoreRowModified("DataTable('" + table.TableName + "') has modified the DataRow with " +
                        "the PrimaryKey value('" + row["PrimaryKey"].ToString() + "')", row);
                }
                else if (rows.Length > 1)
                {
                    MiniScriptSingleton.LogError("DataTable('" + table.TableName + "') has multiple instances with the same " +
                        "PrimaryKey value('" + row["PrimaryKey"].ToString() + "').");
                }
                else if (rows.Length == 0)
                {
                    MiniScriptSingleton.LogError("DataTable('" + table.TableName + "') has no instances with the " +
                        "PrimaryKey value('" + row["PrimaryKey"].ToString() + "').");
                }
            }
        }

        /// <summary>
        /// Creates multiple Instances of the specified quantity in this DataTable
        /// </summary>
        /// <param name="table">The DataTable to create the Instances in</param>
        /// <param name="quantity">How many rows are to be created</param>
        /// <returns>A ValList containing ValMap elements of each DataRow</returns>
        public static ValList CreateInstances(this DataTable table, ValNumber quantity)
        {
            if(quantity.IntValue() > 0)
            {
                ValList rst = new ValList();
                int count = quantity.IntValue();
                while(count > 0)
                {
                    var row = table.NewRow(); //create the datarow object
                    table.Rows.Add(row); //add it to the datatable
                    var map = table.Columns.ToValMap(); //get the datacolumn mapping
                    row.Populate(ref map); //have the datarow assign values to the ValMap
                    DataStoreWarehouse.OnDataWarehouseStoreRowAdd.Invoke("DataTable('" + table.TableName + "') has added a new row," +
                        "PrimaryKey='" + map["PrimaryKey"].ToString() + "'", map);
                    count--;
                }

                return rst;
            }

            return null;
        }

        /// <summary>
        /// Removes the Instance matching the 'id' parameter from the DataTable
        /// </summary>
        /// <param name="table">The DataTable to delete the Instance from</param>
        /// <param name="id"></param>
        public static void DestroyInstance(this DataTable table, ValString id)
        {
            var rows = table.Select("PrimaryKey='" + id.value + "'");
            if (rows.Length == 1)
            {
                table.Rows.Remove(rows[0]);
                DataStoreWarehouse.OnDataWarehouseStoreRowRemoved("DataTable('" + table.TableName + "') has removed a row, " +
                    "PrimaryKey='" + id.value + "'", rows[0].ToValMap());
            }
            else if (rows.Length > 1)
            {
                MiniScriptSingleton.LogError("DataTable('" + table.TableName + "') has multiple instances with the same " +
                    "PrimaryKey value('" + id.value + "').");
            }
            else if (rows.Length == 0)
            {
                MiniScriptSingleton.LogError("DataTable('" + table.TableName + "') has no instances with the " +
                    "PrimaryKey value('" + id.value + "').");
            }
        }

        /// <summary>
        /// Removes the Instance matching the 'id' parameter from the DataTable
        /// </summary>
        /// <param name="table">The DataTable to delete the Instance from</param>
        /// <param name="id">The GUID containing the value of the PrimaryKey</param>
        public static void DestroyInstance(this DataTable table, Guid id)
        {
            var rows = table.Select("PrimaryKey='" + id.ToString() + "'");
            if (rows.Length == 1) { }
            else if (rows.Length > 1)
            {
                MiniScriptSingleton.LogError("DataTable('" + table.TableName + "') has multiple instances with the same " +
                    "PrimaryKey value('" + id.ToString() + "').");
            }
            else if (rows.Length == 0)
            {
                MiniScriptSingleton.LogError("DataTable('" + table.TableName + "') has no instances with the " +
                    "PrimaryKey value('" + id.ToString() + "').");
            }
        }

        /// <summary>
        /// Returns a ValMap containing the DataRow values of the Instance matching 'id'
        /// </summary>
        /// <param name="table">The DataTable to get the Instance from</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ValMap GetInstance(this DataTable table, ValString id)
        {
            var rows = table.Select("PrimaryKey='" + id.value + "'");
            if (rows.Length == 1)
            {
                return rows[0].ToValMap();
            }
            else if (rows.Length > 1)
            {
                MiniScriptSingleton.LogError("DataTable('" + table.TableName + "') has multiple instances with the same " +
                    "PrimaryKey value('" + id.value + "').");
            }
            else if (rows.Length == 0)
            {
                MiniScriptSingleton.LogError("DataTable('" + table.TableName + "') has no instances with the " +
                    "PrimaryKey value('" + id.value + "').");
            }

            return null;
        }

        /// <summary>
        /// Returns a ValMap containing the DataRow values of the Instance matching 'id'
        /// </summary>
        /// <param name="table">The DataTable to get the Instance from</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ValMap GetInstance(this DataTable table, Guid id)
        {
            var rows = table.Select("PrimaryKey='" + id.ToString() + "'");
            if (rows.Length == 1)
            {
                return rows[0].ToValMap();
            }
            else if (rows.Length > 1)
            {
                MiniScriptSingleton.LogError("DataTable('" + table.TableName + "') has multiple instances with the same " +
                    "PrimaryKey value('" + id.ToString() + "').");
            }
            else if (rows.Length == 0)
            {
                MiniScriptSingleton.LogError("DataTable('" + table.TableName + "') has no instances with the " +
                    "PrimaryKey value('" + id.ToString() + "').");
            }

            return null;
        }

        /// <summary>
        /// Returns a ValMap containing the DataRow values of the Instance matching 'id'
        /// </summary>
        /// <param name="table">The DataTable to return all Instances from</param>
        /// <returns>A ValList containing all Instances (as ValMap elements)</returns>
        public static ValList GetInstances(this DataTable table)
        {
            return table.ToValList();
        }

        /// <summary>
        /// Returns a ValMap of ValString,ValString that describes the column definitions of the DataTable
        /// </summary>
        /// <param name="table">The DataTable to get the column definitions of</param>
        /// <returns>A ValMap of ValString,ValString pairs {ColumnName,"ValString" or "ValNumber"}</returns>
        public static ValMap GetMSType(this DataTable table)
        {
            return table.Columns.ToValMap();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ValMap GetRandomInstance(this DataTable table)
        {
            throw new NotImplementedException();
        }

        public static ValList GetRandomInstances(this DataTable table, ValNumber quantity, ValNumber unique)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool HasAttribute(this DataTable table, ValString name)
        {
            return table.Columns.Contains(name.value);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="aname"></param>
        /// <returns></returns>
        public static bool RemoveAttribute(this DataTable table, ValString aname)
        {
            if (table.HasAttribute(aname))
            {   //disallow removing the PrimaryKey column
                if(aname.value != "PrimaryKey")
                {
                    table.Columns.Remove(aname.value);
                    table.AcceptChanges();
                    return true;
                }
            }

            return false;
        }

        public static ValList Select(this DataTable table, ValString aname, ValString value)
        {
            var rows = table.Select(aname.value + "='" + value.value + "'");
            if(rows.Length > 0)
            {
                ValList vl = new ValList();
                foreach(DataRow dr in rows)
                {
                    vl.values.Add(dr.ToValMap());
                }
                return vl;
            }

            return null;
        }

        public static ValList Select(this DataTable table, ValString aname, ValNumber value)
        {
            var rows = table.Select(aname.value + "=" + value.value.ToString());
            if (rows.Length > 0)
            {
                ValList vl = new ValList();
                foreach (DataRow dr in rows)
                {
                    vl.values.Add(dr.ToValMap());
                }
                return vl;
            }

            return null;
        }

        public static ValList Select(this DataTable table, ValString aname, ValNumber lower, ValNumber upper)
        {
            var rows = table.Select(aname.value + " >= " + lower.value.ToString() + " AND " + aname.value + " <= " + upper.value.ToString());
            if (rows.Length > 0)
            {
                ValList vl = new ValList();
                foreach (DataRow dr in rows)
                {
                    vl.values.Add(dr.ToValMap());
                }
                return vl;
            }

            return null; 
        }

        public static ValList SelectRegx(this DataTable table, ValString aname, ValString pattern)
        {
            //string pattern = @"\ba\w*\b";
            //string input = "An extraordinary day dawns with each new day.";
            //Match m = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
            //if (m.Success)
            //    Console.WriteLine("Found '{0}' at position {1}.", m.Value, m.Index);
            ValList vl = new ValList();
            foreach(DataRow dr in table.Rows)
            {
                //does the column value match the pattern given?
                Match m = Regex.Match(dr[aname.value].ToString(), pattern.value, RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    vl.values.Add(dr.ToValMap());
                }
            }
            return vl;
        }

        public static bool ValueAssignChecker(Value key, Value value)
        {
            throw new NotImplementedException();
        }
    }
}
