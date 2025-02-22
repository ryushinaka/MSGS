using System.Collections.Generic;
using System.Data;

namespace MiniScript.MSGS.Extensions
{
    public static class MiniScriptDataExtensions
    {
        public static ValMap Clone(this ValMap a)
        {
            ValMap result = new ValMap();
            var it = a.map.GetEnumerator();
            while (it.MoveNext())
            {
                result.map.Add(it.Current.Key, it.Current.Value);
            }

            return result;
        }

        public static ValMap Merge(this ValMap a, ref ValMap b)
        {
            var it = b.map.GetEnumerator();
            while (it.MoveNext())
            {
                if (!a.map.ContainsKey(it.Current.Key))
                {
                    a.map.Add(it.Current.Key, it.Current.Value);
                }
            }
            return a;
        }
               
        public static ValMap ToValMap(this DataSet set)
        {
            //in case we need to come back and make use of this code, its here
            //ValMap rst = new ValMap();

            //ValMap tables = new ValMap();
            //foreach (DataTable dt in set.Tables)
            //{
            //    if(dt.TableName != "Config")
            //    {
            //        tables.map.Add(new ValString(dt.TableName), dt.ToValMap());
            //    }                
            //}
            //rst.map.Add(new ValString("Tables"), tables);

            //rst.map.Add(new ValString("Name"), new ValString(set.DataSetName));

            //return rst;

         
            ValMap tables = new ValMap();
            foreach (DataTable dt in set.Tables)
            {
                if (dt.TableName != "Config")
                {
                    tables.map.Add(new ValString(dt.TableName), dt.ToValList());
                }
            }

            return tables;
        }

        public static ValList ToValList(this DataTable table)
        {
            ValList rows = new ValList();
            foreach (DataRow dr in table.Rows)
            {
                rows.values.Add(dr.ToValMap());
            }

            return rows;
        }

        public static ValMap ToValMap(this DataRow row)
        {
            ValMap rst = new ValMap();
            foreach (DataColumn dc in row.Table.Columns)
            {
                if (dc.DataType == typeof(string))
                {
                    if(row[dc.ColumnName] == System.DBNull.Value)
                    {
                        rst.map.Add(new ValString(dc.ColumnName), new ValString(""));
                    }
                    else
                    {
                        rst.map.Add(new ValString(dc.ColumnName), new ValString(row[dc.ColumnName].ToString()));
                    }                    
                }
                else if (dc.DataType == typeof(double))
                {
                    if(row[dc.ColumnName] == System.DBNull.Value)
                    {
                        rst.map.Add(new ValString(dc.ColumnName), new ValNumber(0));
                    }
                    else
                    {
                        rst.map.Add(new ValString(dc.ColumnName), new ValNumber((double)row[dc.ColumnName]));
                    }
                }
            }

            return rst;
        }

        public static ValList ToValList(this List<string> l)
        {
            ValList vl = new ValList();
            foreach(string s in l)
            {
                vl.values.Add(new ValString(s));
            }
            return vl;
        }

        public static ValMap ToValMap(this DataColumnCollection col)
        {
            ValMap rst = new ValMap();
            foreach(DataColumn dc in col)
            {
                if(dc.DataType == typeof(string))
                {
                    rst.map.Add(new ValString(dc.ColumnName), new ValString("ValString"));
                }
                else if(dc.DataType == typeof(double))
                {
                    rst.map.Add(new ValString(dc.ColumnName), new ValString("ValNumber"));
                }                
            }

            return rst;
        }

        public static void Populate(this DataRow row, ref ValMap map)
        {
            foreach(DataColumn dc in row.Table.Columns)
            {
                if (dc.DataType == typeof(string))
                {
                    map[dc.ColumnName] = new ValString(row[dc.ColumnName].ToString());                    
                }
                else if (dc.DataType == typeof(double))
                {
                    map[dc.ColumnName] = new ValNumber(double.Parse(row[dc.ColumnName].ToString()));
                }
            }
        }
    }
}

