using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

namespace MiniScript.MSGS.Unity.DataTransferObjects
{
    public class ControlCollectionEventFilterDTO
    {
        public List<int> events = new List<int>();
        public int instanceid;

        [NonSerialized]
        public static DataTable schema;
        [NonSerialized]
        public static bool schemainitd = false;

        public static ControlCollectionEventFilterDTO FromJson(string sss)
        {
            return JsonUtility.FromJson<ControlCollectionEventFilterDTO>(sss);
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public static ControlCollectionEventFilterDTO FromXML(ref DataSet set, int identifier)
        {
            ControlCollectionEventFilterDTO ccef = new ControlCollectionEventFilterDTO();
            DataRow[] rows = set.Tables["ControlEventFilters"].Select("instanceid = " + identifier);
            DataRow row = rows[0];

            if (string.IsNullOrWhiteSpace(row["events"].ToString())) return ccef;
            string[] parts = row["events"].ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts) { if (int.TryParse(part, out int value)) { ccef.events.Add(value); } }
            ccef.instanceid = row.Field<int>("instanceid");
            
            return ccef;
        }

        public void ToXML(ref DataSet set)
        {
            DataRow row = set.Tables["EventFilter"].NewRow();
            row["events"] = string.Join(",", events);
            row["instanceid"] = instanceid;

            set.Tables["EventFilter"].Rows.Add(row);
        }

        public ControlCollectionEventFilterDTO() {
            if (!schemainitd) InitSchema();
        }

        private static void InitSchema()
        {
            schema = new DataTable("ControlEventFilters");
            schema.Columns.Add(new DataColumn("events", typeof(string)));
            schema.Columns.Add(new DataColumn("instanceid", typeof(string)));
            schema.AcceptChanges();
        }
    }
}
