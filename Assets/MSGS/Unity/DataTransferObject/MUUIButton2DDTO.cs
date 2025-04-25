using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace MiniScript.MSGS.Unity.DataTransferObjects
{
    public class MUUIButton2DDTO
    {
        public string label;
        public MUUIRectTransformDTO rect;
        public MUUIImage2DDTO image;
        public MUUIText2DDTO Text;

        public MiniScriptScriptAssetDTO ScriptOnEnterMSA, ScriptOnExitMSA, ScriptOnLeftClickMSA, ScriptOnScrollUpMSA, ScriptOnScrollDownMSA,
            ScriptOnDoubleLeftClickMSA, ScriptOnRightClickMSA, ScriptOnDoubleRightClickMSA, ScriptOnMiddleClickMSA;
        
        public string ScriptOnEnter, ScriptOnExit, ScriptOnLeftClick, ScriptOnScrollUp, ScriptOnScrollDown,
            ScriptOnDoubleLeftClick, ScriptOnRightClick, ScriptOnDoubleRightClick, ScriptOnMiddleClick;
        
        public ControlCollectionEventFilterDTO eventFilter;

        public bool enabled;
        public int childOf, instanceID;

        [NonSerialized]
        public static DataTable schema;
        [NonSerialized]
        public static bool schemainitd = false;
        public static string ExtractValue(string key, string json)
        {
            var pattern = $"\"{key}\"\\s*:\\s*\"?(.*?)\"?(,|\\s*\\}})";
            var match = Regex.Match(json, pattern);
            return match.Success ? match.Groups[1].Value : null;
        }
        public static string ExtractObject(string key, string json)
        {
            var pattern = $"\"{key}\"\\s*:\\s*(\\{{(?:[^{{}}]|(?<open>\\{{)|(?<-open>\\}}))*(?(open)(?!))\\}})";
            var match = Regex.Match(json, pattern);
            return match.Success ? match.Groups[1].Value : null;
        }

        public static MUUIButton2DDTO FromJson(string json)
        {
            var dto = new MUUIButton2DDTO();

            dto.label = ExtractValue("label", json);
            dto.ScriptOnEnter = ExtractValue("ScriptOnEnter", json);
            dto.ScriptOnExit = ExtractValue("ScriptOnExit", json);
            dto.ScriptOnLeftClick = ExtractValue("ScriptOnLeftClick", json);
            dto.ScriptOnScrollUp = ExtractValue("ScriptOnScrollUp", json);
            dto.ScriptOnScrollDown = ExtractValue("ScriptOnScrollDown", json);
            dto.ScriptOnDoubleLeftClick = ExtractValue("ScriptOnDoubleLeftClick", json);
            dto.ScriptOnRightClick = ExtractValue("ScriptOnRightClick", json);
            dto.ScriptOnDoubleRightClick = ExtractValue("ScriptOnDoubleRightClick", json);
            dto.ScriptOnMiddleClick = ExtractValue("ScriptOnMiddleClick", json);

            dto.enabled = json.Contains("\"enabled\":true");
            dto.childOf = int.TryParse(ExtractValue("childOf", json), out var c) ? c : 0;
            dto.instanceID = int.TryParse(ExtractValue("instanceID", json), out var id) ? id : 0;

            var rectJson = ExtractObject("rect", json);
            if (rectJson != null)
                dto.rect = MUUIRectTransformDTO.FromJson(rectJson);

            var imageJson = ExtractObject("image", json);
            if (imageJson != null)
                dto.image = MUUIImage2DDTO.FromJson(imageJson);

            var textJson = ExtractObject("Text", json);
            if (textJson != null)
                dto.Text = MUUIText2DDTO.FromJson(textJson);

            var eventFilterJson = ExtractObject("eventFilter", json);
            if (eventFilterJson != null)
                dto.eventFilter = ControlCollectionEventFilterDTO.FromJson(eventFilterJson);

            return dto;
        }

        public string ToJson()
        {
            var sb = new StringBuilder();
            sb.Append("{");

            sb.AppendFormat("\"label\":\"{0}\",", label);
            sb.AppendFormat("\"enabled\":{0},", enabled.ToString().ToLower());
            sb.AppendFormat("\"childOf\":{0},", childOf);
            sb.AppendFormat("\"instanceID\":{0},", instanceID);

            if (rect != null)
                sb.AppendFormat("\"rect\":{0},", rect.ToJson());

            if (image != null)
                sb.AppendFormat("\"image\":{0},", image.ToJson());

            if (Text != null)
                sb.AppendFormat("\"Text\":{0},", Text.ToJson());

            if (eventFilter != null)
                sb.AppendFormat("\"eventFilter\":{0},", eventFilter.ToJson());

            sb.AppendFormat("\"ScriptOnEnter\":\"{0}\",", ScriptOnEnter);
            sb.AppendFormat("\"ScriptOnExit\":\"{0}\",", ScriptOnExit);
            sb.AppendFormat("\"ScriptOnLeftClick\":\"{0}\",", ScriptOnLeftClick);
            sb.AppendFormat("\"ScriptOnScrollUp\":\"{0}\",", ScriptOnScrollUp);
            sb.AppendFormat("\"ScriptOnScrollDown\":\"{0}\",", ScriptOnScrollDown);
            sb.AppendFormat("\"ScriptOnDoubleLeftClick\":\"{0}\",", ScriptOnDoubleLeftClick);
            sb.AppendFormat("\"ScriptOnRightClick\":\"{0}\",", ScriptOnRightClick);
            sb.AppendFormat("\"ScriptOnDoubleRightClick\":\"{0}\",", ScriptOnDoubleRightClick);
            sb.AppendFormat("\"ScriptOnMiddleClick\":\"{0}\"", ScriptOnMiddleClick);

            sb.Append("}");
            return sb.ToString();
        }

        public static MUUIButton2DDTO FromXML(ref DataSet set, int identifier)
        {
            var dto = new MUUIButton2DDTO();
            dto.rect = MUUIRectTransformDTO.FromXML(ref set, identifier);
            dto.image = MUUIImage2DDTO.FromXML(ref set, identifier);
            dto.Text = MUUIText2DDTO.FromXML(ref set, identifier);
            dto.eventFilter = ControlCollectionEventFilterDTO.FromXML(ref set, identifier);

            DataRow[] rows = set.Tables["MUUIButton2D"].Select("instanceid = " + identifier);
            if(rows != null && rows.Length > 0)
            {
                dto.ScriptOnEnter = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(rows[0].Field<string>("ScriptOnEnter")));
                dto.ScriptOnExit = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(rows[0].Field<string>("ScriptOnExit")));
                dto.ScriptOnLeftClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(rows[0].Field<string>("ScriptOnLeftClick")));
                dto.ScriptOnDoubleLeftClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(rows[0].Field<string>("ScriptOnDoubleLeftClick")));
                dto.ScriptOnScrollUp = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(rows[0].Field<string>("ScriptOnScrollUp")));
                dto.ScriptOnScrollDown = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(rows[0].Field<string>("ScriptOnScrollDown")));
                dto.ScriptOnRightClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(rows[0].Field<string>("ScriptOnRightClick")));
                dto.ScriptOnDoubleRightClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(rows[0].Field<string>("ScriptOnDoubleRightClick")));
                dto.ScriptOnMiddleClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(rows[0].Field<string>("ScriptOnMiddleClick")));

                rows = set.Tables["MiniScriptScriptAsset"].Select("guid = " + rows[0].Field<string>("ScriptOnEnterMSA"));
                if (rows != null && rows.Length > 0) dto.ScriptOnEnterMSA = MiniScriptScriptAssetDTO.FromXML(ref rows[0]);
                rows = set.Tables["MiniScriptScriptAsset"].Select("guid = " + rows[0].Field<string>("ScriptOnExitMSA"));
                if (rows != null && rows.Length > 0) dto.ScriptOnExitMSA = MiniScriptScriptAssetDTO.FromXML(ref rows[0]);
                rows = set.Tables["MiniScriptScriptAsset"].Select("guid = " + rows[0].Field<string>("ScriptOnLeftClickMSA"));
                if (rows != null && rows.Length > 0) dto.ScriptOnLeftClickMSA = MiniScriptScriptAssetDTO.FromXML(ref rows[0]);
                rows = set.Tables["MiniScriptScriptAsset"].Select("guid = " + rows[0].Field<string>("ScriptOnDoubleLeftClickMSA"));
                if (rows != null && rows.Length > 0) dto.ScriptOnDoubleLeftClickMSA = MiniScriptScriptAssetDTO.FromXML(ref rows[0]);
                rows = set.Tables["MiniScriptScriptAsset"].Select("guid = " + rows[0].Field<string>("ScriptOnScrollUpMSA"));
                if (rows != null && rows.Length > 0) dto.ScriptOnScrollUpMSA = MiniScriptScriptAssetDTO.FromXML(ref rows[0]);
                rows = set.Tables["MiniScriptScriptAsset"].Select("guid = " + rows[0].Field<string>("ScriptOnScrollDownMSA"));
                if (rows != null && rows.Length > 0) dto.ScriptOnScrollDownMSA = MiniScriptScriptAssetDTO.FromXML(ref rows[0]);
                rows = set.Tables["MiniScriptScriptAsset"].Select("guid = " + rows[0].Field<string>("ScriptOnRightClickMSA"));
                if (rows != null && rows.Length > 0) dto.ScriptOnRightClickMSA = MiniScriptScriptAssetDTO.FromXML(ref rows[0]);
                rows = set.Tables["MiniScriptScriptAsset"].Select("guid = " + rows[0].Field<string>("ScriptOnDoubleRightClickMSA"));
                if (rows != null && rows.Length > 0) dto.ScriptOnDoubleRightClickMSA = MiniScriptScriptAssetDTO.FromXML(ref rows[0]);
                rows = set.Tables["MiniScriptScriptAsset"].Select("guid = " + rows[0].Field<string>("ScriptOnMiddleClickMSA"));
                if (rows != null && rows.Length > 0) dto.ScriptOnMiddleClickMSA = MiniScriptScriptAssetDTO.FromXML(ref rows[0]);
            }

            return dto;
        }

        /// <summary>
        /// Returns a list containing the Button datarow, rect datarow, image datarow, text datarow, eventfilter datarow
        /// </summary>
        /// <returns></returns>
        public void ToXML(ref DataSet set)
        {
            var row = schema.NewRow();
            row["label"] = label; row["enabled"] = enabled; row["childof"] = childOf; row["instanceid"] = instanceID;
        
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

            row["ScriptOnEnter"] = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(ScriptOnEnter));
            row["ScriptOnExit"] = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(ScriptOnEnter));
            row["ScriptOnLeftClick"] = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(ScriptOnLeftClick));
            row["ScriptOnScrollUp"] = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(ScriptOnScrollUp));
            row["ScriptOnScrollDown"] = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(ScriptOnScrollDown));
            row["ScriptOnDoubleLeftClick"] = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(ScriptOnDoubleLeftClick));
            row["ScriptOnRightClick"] = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(ScriptOnRightClick));
            row["ScriptOnDoubleRightClick"] = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(ScriptOnDoubleRightClick));
            row["ScriptOnMiddleClick"] = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes(ScriptOnMiddleClick));            

            set.Tables["MUUIButton2D"].Rows.Add(row);

            rect.ToXML(ref set);
            Text.ToXML(ref set);
            eventFilter.ToXML(ref set);
            image.ToXML(ref set);
        }

        public MUUIButton2DDTO()
        {
            if (!schemainitd) { InitSchema(); }
        }

        public static void InitSchema()
        {
            schema = new DataTable("MUUIButton2D");
            schema.Columns.Add(new DataColumn("label", typeof(string)));            
            schema.Columns.Add(new DataColumn("enabled", typeof(bool)));
            schema.Columns.Add(new DataColumn("childof", typeof(int)));
            schema.Columns.Add(new DataColumn("instanceid", typeof(int)));

            schema.Columns.Add(new DataColumn("ScriptOnEnter", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnExit", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnLeftClick", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnDoubleLeftClick", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnScrollUp", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnScrollDown", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnRightClick", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnDoubleRightClick", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnMiddleClick", typeof(string)));            

            schema.AcceptChanges();
            schemainitd = true;
        }
    }
}

