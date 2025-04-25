using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace MiniScript.MSGS.Unity.DataTransferObjects
{
    public class MUUIDropDown2DDTO
    {
        public string label;
        public MUUIRectTransformDTO rect;
        public MUUIImage2DDTO image;
        public MUUIText2DDTO text;

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

        public static MUUIDropDown2DDTO FromJson(string json)
        {
            var dto = new MUUIDropDown2DDTO();

            dto.label = ExtractValue("label", json);
            dto.enabled = bool.TryParse(ExtractValue("enabled", json), out var b) ? b : false;
            dto.childOf = int.TryParse(ExtractValue("childOf", json), out var c) ? c : 0;
            dto.instanceID = int.TryParse(ExtractValue("instanceID", json), out var id) ? id : 0;
            
            var rectJson = ExtractObject("rect", json);
            if (rectJson != null) dto.rect = MUUIRectTransformDTO.FromJson(rectJson);

            var imageJson = ExtractObject("image", json);
            if (imageJson != null) dto.image = MUUIImage2DDTO.FromJson(imageJson);

            var textJson = ExtractObject("Text", json);
            if (textJson != null) dto.text = MUUIText2DDTO.FromJson(textJson);

            var eventFilterJson = ExtractObject("eventFilter", json);
            if (eventFilterJson != null) dto.eventFilter = ControlCollectionEventFilterDTO.FromJson(eventFilterJson);

            dto.ScriptOnEnter = ExtractValue("ScriptOnEnter", json);
            dto.ScriptOnExit = ExtractValue("ScriptOnExit", json);
            dto.ScriptOnLeftClick = ExtractValue("ScriptOnLeftClick", json);
            dto.ScriptOnScrollUp = ExtractValue("ScriptOnScrollUp", json);
            dto.ScriptOnScrollDown = ExtractValue("ScriptOnScrollDown", json);
            dto.ScriptOnDoubleLeftClick = ExtractValue("ScriptOnDoubleLeftClick", json);
            dto.ScriptOnRightClick = ExtractValue("ScriptOnRightClick", json);
            dto.ScriptOnDoubleRightClick = ExtractValue("ScriptOnDoubleRightClick", json);
            dto.ScriptOnMiddleClick = ExtractValue("ScriptOnMiddleClick", json);

            dto.ScriptOnEnterMSA = MiniScriptScriptAssetDTO.FromJson(ExtractValue("ScriptOnEnterMSA", json));
            dto.ScriptOnExitMSA = MiniScriptScriptAssetDTO.FromJson(ExtractValue("ScriptOnExitMSA", json));
            dto.ScriptOnLeftClickMSA = MiniScriptScriptAssetDTO.FromJson(ExtractValue("ScriptOnLeftClickMSA", json));
            dto.ScriptOnScrollUpMSA = MiniScriptScriptAssetDTO.FromJson(ExtractValue("ScriptOnScrollUpMSA", json));
            dto.ScriptOnScrollDownMSA = MiniScriptScriptAssetDTO.FromJson(ExtractValue("ScriptOnScrollDownMSA", json));
            dto.ScriptOnDoubleLeftClickMSA = MiniScriptScriptAssetDTO.FromJson(ExtractValue("ScriptOnDoubleLeftClickMSA", json));
            dto.ScriptOnRightClickMSA = MiniScriptScriptAssetDTO.FromJson(ExtractValue("ScriptOnRightClickMSA", json));
            dto.ScriptOnDoubleRightClickMSA = MiniScriptScriptAssetDTO.FromJson(ExtractValue("ScriptOnDoubleRightClickMSA", json));
            dto.ScriptOnMiddleClickMSA = MiniScriptScriptAssetDTO.FromJson(ExtractValue("ScriptOnMiddleClickMSA", json));

            return dto;
        }

        public string ToJson()
        {
            var sb = new StringBuilder();
            sb.Append("{");

            sb.AppendFormat("\"label\":\"{0}\",\n", label);
            sb.AppendFormat("\"enabled\":{0},\n", enabled.ToString().ToLower());
            sb.AppendFormat("\"childOf\":{0},\n", childOf);
            sb.AppendFormat("\"instanceID\":{0},\n", instanceID);

            if (rect != null) sb.AppendFormat("\"rect\":{0},\n", rect.ToJson());

            if (image != null) sb.AppendFormat("\"image\":{0},\n", image.ToJson());

            if (text != null) sb.AppendFormat("\"Text\":{0},\n", text.ToJson());

            if (eventFilter != null) sb.AppendFormat("\"eventFilter\":{0},\n", eventFilter.ToJson());

            sb.AppendFormat("\"ScriptOnEnter\":\"{0}\",\n", ScriptOnEnter);
            sb.AppendFormat("\"ScriptOnExit\":\"{0}\",\n", ScriptOnExit);
            sb.AppendFormat("\"ScriptOnLeftClick\":\"{0}\",\n", ScriptOnLeftClick);
            sb.AppendFormat("\"ScriptOnScrollUp\":\"{0}\",\n", ScriptOnScrollUp);
            sb.AppendFormat("\"ScriptOnScrollDown\":\"{0}\",\n", ScriptOnScrollDown);
            sb.AppendFormat("\"ScriptOnDoubleLeftClick\":\"{0}\",\n", ScriptOnDoubleLeftClick);
            sb.AppendFormat("\"ScriptOnRightClick\":\"{0}\",\n", ScriptOnRightClick);
            sb.AppendFormat("\"ScriptOnDoubleRightClick\":\"{0}\",\n", ScriptOnDoubleRightClick);
            sb.AppendFormat("\"ScriptOnMiddleClick\":\"{0}\"\n", ScriptOnMiddleClick);

            sb.AppendFormat("\"ScriptOnEnterMSA\":\"{0}\",\n", ScriptOnEnterMSA);
            sb.AppendFormat("\"ScriptOnExitMSA\":\"{0}\",\n", ScriptOnExitMSA);
            sb.AppendFormat("\"ScriptOnLeftClickMSA\":\"{0}\",\n", ScriptOnLeftClickMSA);
            sb.AppendFormat("\"ScriptOnScrollUpMSA\":\"{0}\",\n", ScriptOnScrollUpMSA);
            sb.AppendFormat("\"ScriptOnScrollDownMSA\":\"{0}\",\n", ScriptOnScrollDownMSA);
            sb.AppendFormat("\"ScriptOnDoubleLeftClickMSA\":\"{0}\",\n", ScriptOnDoubleLeftClickMSA);
            sb.AppendFormat("\"ScriptOnRightClickMSA\":\"{0}\",\n", ScriptOnRightClickMSA);
            sb.AppendFormat("\"ScriptOnDoubleRightClickMSA\":\"{0}\",\n", ScriptOnDoubleRightClickMSA);
            sb.AppendFormat("\"ScriptOnMiddleClickMSA\":\"{0}\"\n", ScriptOnMiddleClickMSA);

            sb.Append("}");
            return sb.ToString();
        }

        public static MUUIDropDown2DDTO FromXML(ref DataSet set, int identifier)
        {
            var dto = new MUUIDropDown2DDTO();
            dto.rect = MUUIRectTransformDTO.FromXML(ref set, identifier);
            dto.image = MUUIImage2DDTO.FromXML(ref set, identifier);
            dto.text = MUUIText2DDTO.FromXML(ref set, identifier);
            dto.eventFilter = ControlCollectionEventFilterDTO.FromXML(ref set, identifier);
            return dto;
        }

        public void ToXML(ref DataSet set)
        {
            var row = schema.NewRow();
            row["label"] = label; row["enabled"] = enabled; row["childof"] = childOf; row["instanceid"] = instanceID;
            set.Tables["Button"].Rows.Add(row);
            rect.ToXML(ref set);
            text.ToXML(ref set);
            eventFilter.ToXML(ref set);
            image.ToXML(ref set);
        }

        public MUUIDropDown2DDTO()
        {
            if (!schemainitd) { InitSchema(); }
        }

        private static void InitSchema()
        {
            schema = new DataTable("MUUIToggle2D");
            schema.Columns.Add(new DataColumn("label", typeof(string)));
            schema.Columns.Add(new DataColumn("enabled", typeof(bool)));
            schema.Columns.Add(new DataColumn("childof", typeof(int)));
            schema.Columns.Add(new DataColumn("instanceid", typeof(int)));
            
            //need some data structure to hold the elements of the drop down control and its basic configuration settings
            
            
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