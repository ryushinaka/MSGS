using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace MiniScript.MSGS.Unity.DataTransferObjects
{
    public class MUUIScrollview2DDTO
    {
        public string label;
        public MUUIRectTransformDTO rect, rectViewport;
        public MUUIImage2DDTO imageBackground, imageBarHorizontal, imageBarVertical;

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

        public static MUUIScrollview2DDTO FromJson (string json)
        {
            var dto = new MUUIScrollview2DDTO();
            dto.label = ExtractValue("label", json);
            dto.enabled = bool.TryParse(ExtractValue("enabled", json), out var b) ? b : false;
            dto.childOf = int.TryParse(ExtractValue("childOf", json), out var c) ? c : 0;
            dto.instanceID = int.TryParse(ExtractValue("instanceID", json), out var id) ? id : 0;

            var rectJson = ExtractObject("rect", json);
            if (rectJson != null) dto.rect = MUUIRectTransformDTO.FromJson(rectJson);

            var rectViewport = ExtractObject("rectViewport", json);
            if (rectViewport != null) dto.rectViewport = MUUIRectTransformDTO.FromJson(rectViewport);

            var imageJson = ExtractObject("imageBackground", json);
            if (imageJson != null) dto.imageBackground = MUUIImage2DDTO.FromJson(imageJson);
            imageJson = ExtractObject("imageBarHorizontal", json);
            if (imageJson != null) dto.imageBarHorizontal = MUUIImage2DDTO.FromJson(imageJson);
            imageJson = ExtractObject("imageBarVertical", json);
            if (imageJson != null) dto.imageBarVertical = MUUIImage2DDTO.FromJson(imageJson);

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
            if (rectViewport != null) sb.AppendFormat("\"rectViewport\":{0},\n", rectViewport.ToJson());

            if (imageBackground != null) sb.AppendFormat("\"imageBackground\":{0},\n", imageBackground.ToJson());
            if (imageBarHorizontal != null) sb.AppendFormat("\"imageBarHorizontal\":{0},\n", imageBarHorizontal.ToJson());
            if (imageBarVertical != null) sb.AppendFormat("\"imageBarVertical\":{0},\n", imageBarVertical.ToJson());

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

        public static MUUIScrollview2DDTO FromXML(ref DataSet set, int identifier)
        {
            var dto = new MUUIScrollview2DDTO();
            dto.rect = MUUIRectTransformDTO.FromXML(ref set, identifier, 0);
            dto.rectViewport = MUUIRectTransformDTO.FromXML(ref set, identifier, 1);
            dto.imageBackground = MUUIImage2DDTO.FromXML(ref set, identifier, 0);
            dto.imageBarHorizontal = MUUIImage2DDTO.FromXML(ref set, identifier, 0);
            dto.imageBarVertical = MUUIImage2DDTO.FromXML(ref set, identifier, 0);



            return dto;
        }

        public void ToXml(ref DataSet set)
        {

        }


        public MUUIScrollview2DDTO()
        {
            if(!schemainitd) { InitSchema(); }
        }

        private static void InitSchema()
        {
            schema = new DataTable("MUUIScrollview2D");
            schema.Columns.Add(new DataColumn("label", typeof(string)));
            schema.Columns.Add(new DataColumn("enabled", typeof(bool)));
            schema.Columns.Add(new DataColumn("childof", typeof(int)));
            schema.Columns.Add(new DataColumn("instanceid", typeof(int)));

            //need some data structure to hold the elements of the scrollview control and its basic configuration settings


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