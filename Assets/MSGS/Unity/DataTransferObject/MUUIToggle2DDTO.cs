using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace MiniScript.MSGS.Unity.DataTransferObjects
{
    public class MUUIToggle2DDTO
    {
        public string label;
        public bool interactable;
        public MUUIRectTransformDTO rect;
        public MUUIImage2DDTO background, checkmark;
        public MUUIText2DDTO LabelText;

        public MiniScriptScriptAssetDTO ScriptOnEnterMSA, ScriptOnExitMSA, ScriptOnLeftClickMSA, ScriptOnScrollUpMSA, ScriptOnScrollDownMSA,
           ScriptOnDoubleLeftClickMSA, ScriptOnRightClickMSA, ScriptOnDoubleRightClickMSA, ScriptOnMiddleClickMSA;

        public string ScriptOnEnter, ScriptOnExit, ScriptOnLeftClick, ScriptOnScrollUp, ScriptOnScrollDown,
            ScriptOnDoubleLeftClick, ScriptOnRightClick, ScriptOnDoubleRightClick, ScriptOnMiddleClick;

        public ControlCollectionEventFilterDTO eventFilter;

        public bool enabled;
        public int childof, instanceid;

        [NonSerialized]
        public static DataTable schema;
        [NonSerialized]
        public static bool schemainitd = false;

        private static string ExtractValue(string key, string json)
        {
            var pattern = $"\"{key}\"\\s*:\\s*\"?(.*?)\"?(,|\\s*\\}})";
            var match = Regex.Match(json, pattern);
            return match.Success ? match.Groups[1].Value : null;
        }

        public static MUUIToggle2DDTO FromJson(string json)
        {
            var dto = new MUUIToggle2DDTO();
            dto.label = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("label", json)));
            dto.interactable = bool.TryParse(ExtractValue("interactable", json), out var b) && b;
            dto.enabled = bool.TryParse(ExtractValue("enabled", json), out var c) && c;
            dto.childof = int.TryParse(ExtractValue("childof", json), out var co) ? co : -1;
            dto.instanceid = int.TryParse(ExtractValue("instanceid", json), out var id) ? id : -1;
            dto.rect = MUUIRectTransformDTO.FromJson(ExtractValue("rect", json));

            string imageData = ExtractValue("background", json);
            if (!string.IsNullOrEmpty(imageData)) { dto.background = MUUIImage2DDTO.FromJson(ExtractValue("background", json)); }

            imageData = ExtractValue("checkmark", json);
            if (!string.IsNullOrEmpty(imageData)) { dto.checkmark = MUUIImage2DDTO.FromJson(ExtractValue("checkmark", json)); }

            imageData = ExtractValue("eventFilter", json);
            if (!string.IsNullOrEmpty(imageData)) { dto.eventFilter = ControlCollectionEventFilterDTO.FromJson(imageData); }

            dto.LabelText = MUUIText2DDTO.FromJson(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("LabelText", json))));

            dto.ScriptOnEnter = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("ScriptOnEnter", json)));
            dto.ScriptOnExit = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("ScriptOnExit", json)));
            dto.ScriptOnLeftClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("ScriptOnLeftClick", json)));
            dto.ScriptOnDoubleLeftClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("ScriptOnDoubleLeftClick", json)));
            dto.ScriptOnScrollUp = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("ScriptOnScrollUp", json)));
            dto.ScriptOnScrollDown = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("ScriptOnScrollDown", json)));
            dto.ScriptOnRightClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("ScriptOnRightClick", json)));
            dto.ScriptOnDoubleRightClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("ScriptOnDoubleRightClick", json)));
            dto.ScriptOnMiddleClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("ScriptOnMiddleClick", json)));

            dto.ScriptOnEnterMSA = MiniScriptScriptAssetDTO.FromJson(ExtractValue("ScriptOnEnterMSA", json));
            dto.ScriptOnExitMSA = MiniScriptScriptAssetDTO.FromJson(ExtractValue("ScriptOnExitMSA", json));
            dto.ScriptOnLeftClickMSA = MiniScriptScriptAssetDTO.FromJson(ExtractValue("ScriptOnLeftClickMSA", json));
            dto.ScriptOnDoubleLeftClickMSA = MiniScriptScriptAssetDTO.FromJson(ExtractValue("ScriptOnDoubleLeftClickMSA", json));
            dto.ScriptOnScrollUpMSA = MiniScriptScriptAssetDTO.FromJson(ExtractValue("ScriptOnScrollUpMSA", json));
            dto.ScriptOnScrollDownMSA = MiniScriptScriptAssetDTO.FromJson(ExtractValue("ScriptOnScrollDownMSA", json));
            dto.ScriptOnRightClickMSA = MiniScriptScriptAssetDTO.FromJson(ExtractValue("ScriptOnRightClickMSA", json));
            dto.ScriptOnDoubleRightClickMSA = MiniScriptScriptAssetDTO.FromJson(ExtractValue("ScriptOnDoubleRightClickMSA", json));
            dto.ScriptOnMiddleClickMSA = MiniScriptScriptAssetDTO.FromJson(ExtractValue("ScriptOnMiddleClickMSA", json));

            return dto;
        }

        public string ToJson()
        {
            var sb = new StringBuilder();
            sb.Append("{");

            sb.AppendFormat("\"label\":\"{0}\",", label);
            sb.AppendFormat("\"interactable\":{0},", interactable.ToString().ToLower());
            sb.AppendFormat("\"enabled\":{0},", enabled.ToString().ToLower());
            sb.AppendFormat("\"childOf\":{0},", childof);
            sb.AppendFormat("\"instanceID\":{0},", instanceid);

            if (rect != null)
                sb.AppendFormat("\"rect\":{0},", rect.ToJson());

            if (background != null)
                sb.AppendFormat("\"background\":{0},", background.ToJson());

            if (checkmark != null)
                sb.AppendFormat("\"checkmark\":{0},", checkmark.ToJson());

            if (LabelText != null)
                sb.AppendFormat("\"LabelText\":{0},", LabelText.ToJson());

            if (eventFilter != null)
                sb.AppendFormat("\"eventFilter\":{0},", eventFilter.ToJson());

            sb.AppendFormat("\"ScriptOnEnter\":\"{0}\",", Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnEnter)));
            sb.AppendFormat("\"ScriptOnExit\":\"{0}\",", Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnExit)));
            sb.AppendFormat("\"ScriptOnLeftClick\":\"{0}\",", Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnLeftClick)));
            sb.AppendFormat("\"ScriptOnScrollUp\":\"{0}\",", Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnScrollUp)));
            sb.AppendFormat("\"ScriptOnScrollDown\":\"{0}\",", Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnScrollDown)));
            sb.AppendFormat("\"ScriptOnDoubleLeftClick\":\"{0}\",", Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnDoubleLeftClick)));
            sb.AppendFormat("\"ScriptOnRightClick\":\"{0}\",", Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnRightClick)));
            sb.AppendFormat("\"ScriptOnDoubleRightClick\":\"{0}\",", Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnDoubleRightClick)));
            sb.AppendFormat("\"ScriptOnMiddleClick\":\"{0}\"", Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnMiddleClick)));

            sb.AppendFormat("\"ScriptOnEnterMSA\":\"{0}\",", ScriptOnEnterMSA.ToJson());
            sb.AppendFormat("\"ScriptOnExitMSA\":\"{0}\",", ScriptOnExitMSA.ToJson());
            sb.AppendFormat("\"ScriptOnLeftClickMSA\":\"{0}\",", ScriptOnLeftClickMSA.ToJson());
            sb.AppendFormat("\"ScriptOnScrollUpMSA\":\"{0}\",", ScriptOnScrollUpMSA.ToJson());
            sb.AppendFormat("\"ScriptOnScrollDownMSA\":\"{0}\",", ScriptOnScrollDownMSA.ToJson());
            sb.AppendFormat("\"ScriptOnDoubleLeftClickMSA\":\"{0}\",", ScriptOnDoubleLeftClickMSA.ToJson());
            sb.AppendFormat("\"ScriptOnRightClickMSA\":\"{0}\",", ScriptOnRightClickMSA.ToJson());
            sb.AppendFormat("\"ScriptOnDoubleRightClickMSA\":\"{0}\",", ScriptOnDoubleRightClickMSA.ToJson());
            sb.AppendFormat("\"ScriptOnMiddleClickMSA\":\"{0}\",", ScriptOnMiddleClickMSA.ToJson());

            sb.Append("}");
            return sb.ToString();
        }

        public static MUUIToggle2DDTO FromXML(ref DataSet set, int instanceid)
        {
            var dto = new MUUIToggle2DDTO();
            DataRow[] rows = set.Tables["MUUIToggle2D"].Select("instanceid = " + instanceid);
            if (rows != null && rows.Length > 0)
            {
                dto.label = rows[0].Field<string>("label");
                dto.enabled = rows[0].Field<bool>("enabled");
                dto.childof = rows[0].Field<int>("childof");
                dto.instanceid = rows[0].Field<int>("instanceid");

                #region MiniScriptScriptAssets
                var rowsB = set.Tables["MiniScriptScriptAsset"].Select("guid = " + rows[0].Field<string>("ScriptOnEnterMSA"));
                if (rowsB != null && rowsB.Length > 0) dto.ScriptOnEnterMSA = MiniScriptScriptAssetDTO.FromXML(ref rowsB[0]);
                rowsB = set.Tables["MiniScriptScriptAsset"].Select("guid = " + rows[0].Field<string>("ScriptOnExitMSA"));
                if (rowsB != null && rowsB.Length > 0) dto.ScriptOnExitMSA = MiniScriptScriptAssetDTO.FromXML(ref rowsB[0]);
                rowsB = set.Tables["MiniScriptScriptAsset"].Select("guid = " + rows[0].Field<string>("ScriptOnLeftClickMSA"));
                if (rowsB != null && rowsB.Length > 0) dto.ScriptOnLeftClickMSA = MiniScriptScriptAssetDTO.FromXML(ref rowsB[0]);
                rowsB = set.Tables["MiniScriptScriptAsset"].Select("guid = " + rows[0].Field<string>("ScriptOnScrollUpMSA"));
                if (rowsB != null && rowsB.Length > 0) dto.ScriptOnScrollUpMSA = MiniScriptScriptAssetDTO.FromXML(ref rowsB[0]);
                rowsB = set.Tables["MiniScriptScriptAsset"].Select("guid = " + rows[0].Field<string>("ScriptOnScrollDownMSA"));
                if (rowsB != null && rowsB.Length > 0) dto.ScriptOnScrollDownMSA = MiniScriptScriptAssetDTO.FromXML(ref rowsB[0]);
                rowsB = set.Tables["MiniScriptScriptAsset"].Select("guid = " + rows[0].Field<string>("ScriptOnDoubleLeftClickMSA"));
                if (rowsB != null && rowsB.Length > 0) dto.ScriptOnDoubleLeftClickMSA = MiniScriptScriptAssetDTO.FromXML(ref rowsB[0]);
                rowsB = set.Tables["MiniScriptScriptAsset"].Select("guid = " + rows[0].Field<string>("ScriptOnRightClickMSA"));
                if (rowsB != null && rowsB.Length > 0) dto.ScriptOnRightClickMSA = MiniScriptScriptAssetDTO.FromXML(ref rowsB[0]);
                rowsB = set.Tables["MiniScriptScriptAsset"].Select("guid = " + rows[0].Field<string>("ScriptOnDoubleRightClickMSA"));
                if (rowsB != null && rowsB.Length > 0) dto.ScriptOnDoubleRightClickMSA = MiniScriptScriptAssetDTO.FromXML(ref rowsB[0]);
                rowsB = set.Tables["MiniScriptScriptAsset"].Select("guid = " + rows[0].Field<string>("ScriptOnMiddleClickMSA"));
                if (rowsB != null && rowsB.Length > 0) dto.ScriptOnMiddleClickMSA = MiniScriptScriptAssetDTO.FromXML(ref rowsB[0]);
                #endregion

                #region Scripts                
                dto.ScriptOnEnter = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(rows[0].Field<string>("ScriptOnEnter")));
                dto.ScriptOnExit = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(rows[0].Field<string>("ScriptOnExit")));
                dto.ScriptOnLeftClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(rows[0].Field<string>("ScriptOnLeftClick")));
                dto.ScriptOnDoubleLeftClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(rows[0].Field<string>("ScriptOnDoubleLeftClick")));
                dto.ScriptOnScrollUp = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(rows[0].Field<string>("ScriptOnScrollUp")));
                dto.ScriptOnScrollDown = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(rows[0].Field<string>("ScriptOnScrollDown")));
                dto.ScriptOnRightClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(rows[0].Field<string>("ScriptOnRightClick")));
                dto.ScriptOnDoubleRightClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(rows[0].Field<string>("ScriptOnDoubleRightClick")));
                dto.ScriptOnMiddleClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(rows[0].Field<string>("ScriptOnMiddleClick")));
                #endregion
            }

            return dto;
        }

        public void ToXml(ref DataSet set)
        {
            DataRow row = schema.NewRow();
            row["label"] = label;
            row["enabled"] = enabled;
            row["childof"] = childof;
            row["instanceid"] = instanceid;

            row["ScriptOnEnter"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnEnter));
            row["ScriptOnExit"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnExit));
            row["ScriptOnLeftClick"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnLeftClick));
            row["ScriptOnScrollUp"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnScrollUp));
            row["ScriptOnScrollDown"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnScrollDown));
            row["ScriptOnDoubleLeftClick"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnDoubleLeftClick));
            row["ScriptOnRightClick"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnRightClick));
            row["ScriptOnDoubleRightClick"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnDoubleRightClick));
            row["ScriptOnMiddleClick"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnMiddleClick));

            string str = string.Empty;
            ScriptOnEnterMSA.ToXML(ref set, out str); row["ScriptOnEnterMSA"] = str;
            ScriptOnExitMSA.ToXML(ref set, out str); row["ScriptOnExitMSA"] = str;
            ScriptOnLeftClickMSA.ToXML(ref set, out str); row["ScriptOnLeftClickMSA"] = str;
            ScriptOnScrollUpMSA.ToXML(ref set, out str); row["ScriptOnScrollUpMSA"] = str;
            ScriptOnScrollDownMSA.ToXML(ref set, out str); row["ScriptOnScrollDownMSA"] = str;
            ScriptOnDoubleLeftClickMSA.ToXML(ref set, out str); row["ScriptOnDoubleLeftClickMSA"] = str;
            ScriptOnRightClickMSA.ToXML(ref set, out str); row["ScriptOnRightClickMSA"] = str;
            ScriptOnDoubleRightClickMSA.ToXML(ref set, out str); row["ScriptOnDoubleRightClickMSA"] = str;
            ScriptOnMiddleClickMSA.ToXML(ref set, out str); row["ScriptOnMiddleClickMSA"] = str;

            set.Tables["MUUIToggle2D"].Rows.Add(row);
        }

        public MUUIToggle2DDTO() { if (!schemainitd) { InitSchema(); } }

        private static void InitSchema()
        {
            schema = new DataTable("MUUIToggle2D");
            schema.Columns.Add(new DataColumn("label", typeof(string)));
            schema.Columns.Add(new DataColumn("enabled", typeof(bool)));
            schema.Columns.Add(new DataColumn("childof", typeof(int)));
            schema.Columns.Add(new DataColumn("instanceid", typeof(int)));

            #region scripts and MSA's
            schema.Columns.Add(new DataColumn("ScriptOnEnterMSA", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnExitMSA", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnLeftClickMSA", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnScrollUpMSA", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnScrollDownMSA", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnDoubleLeftClickMSA", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnRightClickMSA", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnDoubleRightClickMSA", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnMiddleClickMSA", typeof(string)));

            schema.Columns.Add(new DataColumn("ScriptOnEnter", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnExit", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnLeftClick", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnScrollUp", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnScrollDown", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnDoubleLeftClick", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnRightClick", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnDoubleRightClick", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnMiddleClick", typeof(string)));
            #endregion

            schema.AcceptChanges();

            schemainitd = true;
        }
    }
}