using System;
using System.Data;
using UnityEngine;

namespace MiniScript.MSGS.Unity.DataTransferObjects
{
    public class MUUIRectTransformDTO
    {
        public float anchoredPositionx, anchoredPositiony;
        public float anchorMinx, anchorMiny, anchorMaxx, anchorMaxy;
        public float sizeDeltax, sizeDeltay;
        public float pivotX, pivotY;
        public float offsetMaxx, offsetMaxy, offsetMinx, offsetMiny;
        public float left, top, right, bottom, height, width;
        public float lposx, lposy, lposz;
        public float scalex, scaley, scalez;

        public int instanceID, index;

        [NonSerialized]
        public static DataTable schema;
        [NonSerialized]
        public static bool schemainitd = false;

        public static MUUIRectTransformDTO FromJson(string sss)
        {
            return JsonUtility.FromJson<MUUIRectTransformDTO>(sss);
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
        public static MUUIRectTransformDTO FromXML(ref DataSet set, int identifier, int indice)
        {
            MUUIRectTransformDTO dto = new MUUIRectTransformDTO();
            DataRow[] rows = set.Tables["RectTransform"].Select("instanceid = " + identifier);
            if (rows != null && rows.Length >= 2)
            {
                DataRow row = rows[0];
                if (rows[0].Field<int>("index") == indice) { row = rows[0]; }
                else if (rows[1].Field<int>("index") == indice) { row = rows[1]; }

                dto.anchoredPositionx = row.Field<float>("anchoredPositionx");
                dto.anchoredPositiony = row.Field<float>("anchoredPositiony");
                dto.anchorMinx = row.Field<float>("anchorMinx");
                dto.anchorMiny = row.Field<float>("anchorMiny");
                dto.anchorMaxx = row.Field<float>("anchorMaxx");
                dto.anchorMaxy = row.Field<float>("anchorMaxy");

                dto.sizeDeltax = row.Field<float>("sizeDeltax");
                dto.sizeDeltay = row.Field<float>("sizeDeltay");

                dto.pivotX = row.Field<float>("pivotX");
                dto.pivotY = row.Field<float>("pivotY");

                dto.offsetMaxx = row.Field<float>("offsetMaxx");
                dto.offsetMaxy = row.Field<float>("offsetMaxy");
                dto.offsetMinx = row.Field<float>("offsetMinx");
                dto.offsetMiny = row.Field<float>("offsetMiny");

                dto.left = row.Field<float>("left");
                dto.top = row.Field<float>("top");
                dto.right = row.Field<float>("right");
                dto.bottom = row.Field<float>("bottom");

                dto.height = row.Field<float>("height");
                dto.width = row.Field<float>("width");

                dto.lposx = row.Field<float>("lposx");
                dto.lposy = row.Field<float>("lposy");
                dto.lposz = row.Field<float>("lposz");

                dto.scalex = row.Field<float>("scalex");
                dto.scaley = row.Field<float>("scaley");
                dto.scalez = row.Field<float>("scalez");

                dto.instanceID = row.Field<int>("instanceID");
                dto.index = row.Field<int>("index");
            }

            return dto;
        }
        public static MUUIRectTransformDTO FromXML(ref DataSet set, int identifier)
        {
            MUUIRectTransformDTO dto = new MUUIRectTransformDTO();
            DataRow[] rows = set.Tables["RectTransform"].Select("instanceid = " + identifier);
            if (rows != null && rows.Length > 0)
            {
                DataRow row = rows[0];
                dto.anchoredPositionx = row.Field<float>("anchoredPositionx");
                dto.anchoredPositiony = row.Field<float>("anchoredPositiony");
                dto.anchorMinx = row.Field<float>("anchorMinx");
                dto.anchorMiny = row.Field<float>("anchorMiny");
                dto.anchorMaxx = row.Field<float>("anchorMaxx");
                dto.anchorMaxy = row.Field<float>("anchorMaxy");

                dto.sizeDeltax = row.Field<float>("sizeDeltax");
                dto.sizeDeltay = row.Field<float>("sizeDeltay");

                dto.pivotX = row.Field<float>("pivotX");
                dto.pivotY = row.Field<float>("pivotY");

                dto.offsetMaxx = row.Field<float>("offsetMaxx");
                dto.offsetMaxy = row.Field<float>("offsetMaxy");
                dto.offsetMinx = row.Field<float>("offsetMinx");
                dto.offsetMiny = row.Field<float>("offsetMiny");

                dto.left = row.Field<float>("left");
                dto.top = row.Field<float>("top");
                dto.right = row.Field<float>("right");
                dto.bottom = row.Field<float>("bottom");

                dto.height = row.Field<float>("height");
                dto.width = row.Field<float>("width");

                dto.lposx = row.Field<float>("lposx");
                dto.lposy = row.Field<float>("lposy");
                dto.lposz = row.Field<float>("lposz");

                dto.scalex = row.Field<float>("scalex");
                dto.scaley = row.Field<float>("scaley");
                dto.scalez = row.Field<float>("scalez");

                dto.instanceID = row.Field<int>("instanceID");
                dto.index = row.Field<int>("index");
            }

            return dto;
        }

        public void ToXML(ref DataSet set)
        {
            DataRow row = schema.NewRow();
            row["anchoredPositionx"] = anchoredPositionx;
            row["anchoredPositiony"] = anchoredPositiony;
            row["anchorMinx"] = anchorMinx;
            row["anchorMiny"] = anchorMiny;
            row["anchorMaxx"] = anchorMaxx;
            row["anchorMaxy"] = anchorMaxy;
            row["sizeDeltax"] = sizeDeltax;
            row["sizeDeltay"] = sizeDeltay;
            row["pivotX"] = pivotX;
            row["pivotY"] = pivotY;
            row["offsetMaxx"] = offsetMaxx;
            row["offsetMaxy"] = offsetMaxy;
            row["offsetMinx"] = offsetMinx;
            row["offsetMiny"] = offsetMiny;
            row["left"] = left;
            row["top"] = top;
            row["right"] = right;
            row["bottom"] = bottom;
            row["height"] = height;
            row["width"] = width;
            row["lposx"] = lposx;
            row["lposy"] = lposy;
            row["lposz"] = lposz;
            row["scalex"] = scalex;
            row["scaley"] = scaley;
            row["scalez"] = scalez;
            row["instanceID"] = instanceID;
            row["index"] = index;

            set.Tables["RectTransform"].Rows.Add(row);
        }

        public MUUIRectTransformDTO()
        {
            if (!schemainitd) { InitSchema(); }
        }

        private static void InitSchema()
        {
            schema = new DataTable("RectTransform");
            schema.Columns.Add(new DataColumn("anchoredPositionx", typeof(float)));
            schema.Columns.Add(new DataColumn("anchoredPositiony", typeof(float)));
            schema.Columns.Add(new DataColumn("anchorMinx", typeof(float)));
            schema.Columns.Add(new DataColumn("anchorMiny", typeof(float)));
            schema.Columns.Add(new DataColumn("anchorMaxx", typeof(float)));
            schema.Columns.Add(new DataColumn("anchorMaxy", typeof(float)));
            schema.Columns.Add(new DataColumn("sizeDeltax", typeof(float)));
            schema.Columns.Add(new DataColumn("sizeDeltay", typeof(float)));
            schema.Columns.Add(new DataColumn("pivotX", typeof(float)));
            schema.Columns.Add(new DataColumn("pivotY", typeof(float)));
            schema.Columns.Add(new DataColumn("offsetMaxx", typeof(float)));
            schema.Columns.Add(new DataColumn("offsetMaxy", typeof(float)));
            schema.Columns.Add(new DataColumn("offsetMinx", typeof(float)));
            schema.Columns.Add(new DataColumn("offsetMiny", typeof(float)));
            schema.Columns.Add(new DataColumn("left", typeof(float)));
            schema.Columns.Add(new DataColumn("top", typeof(float)));
            schema.Columns.Add(new DataColumn("right", typeof(float)));
            schema.Columns.Add(new DataColumn("bottom", typeof(float)));
            schema.Columns.Add(new DataColumn("height", typeof(float)));
            schema.Columns.Add(new DataColumn("width", typeof(float)));
            schema.Columns.Add(new DataColumn("lposx", typeof(float)));
            schema.Columns.Add(new DataColumn("lposy", typeof(float)));
            schema.Columns.Add(new DataColumn("lposz", typeof(float)));
            schema.Columns.Add(new DataColumn("scalex", typeof(float)));
            schema.Columns.Add(new DataColumn("scaley", typeof(float)));
            schema.Columns.Add(new DataColumn("scalez", typeof(float)));

            schema.Columns.Add(new DataColumn("instanceID", typeof(int)));
            schema.Columns.Add(new DataColumn("index", typeof(int)));
            schema.AcceptChanges();
        }
    }
}
