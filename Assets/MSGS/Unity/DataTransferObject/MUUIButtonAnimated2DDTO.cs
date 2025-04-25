using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace MiniScript.MSGS.Unity.DataTransferObjects
{
    public class MUUIButtonAnimated2DDTO
    {
        public string label;
        public MUUIRectTransformDTO rect;
        public List<MUUIImage2DDTO> sprites;
        public MUUIText2DDTO Text;

        public MiniScriptScriptAssetDTO ScriptOnEnterMSA, ScriptOnExitMSA, ScriptOnLeftClickMSA, ScriptOnScrollUpMSA, ScriptOnScrollDownMSA,
            ScriptOnDoubleLeftClickMSA, ScriptOnRightClickMSA, ScriptOnDoubleRightClickMSA, ScriptOnMiddleClickMSA;

        public string ScriptOnEnter, ScriptOnExit, ScriptOnLeftClick, ScriptOnScrollUp, ScriptOnScrollDown,
            ScriptOnDoubleLeftClick, ScriptOnRightClick, ScriptOnDoubleRightClick, ScriptOnMiddleClick;

        public ControlCollectionEventFilterDTO eventFilter;

        public float timing;
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

        public static MUUIButtonAnimated2DDTO FromJson(string json)
        {
            var dto = new MUUIButtonAnimated2DDTO();

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

            var spritesJsonMatch = Regex.Match(json, "\"sprites\"\\s*:\\s*\\[(.*?)\\]", RegexOptions.Singleline);
            if (spritesJsonMatch.Success)
            {
                string rawArray = spritesJsonMatch.Groups[1].Value;
                var matches = Regex.Matches(rawArray, "\\{(?:[^{}]|(?<open>\\{)|(?<-open>\\}))*\\}");
                dto.sprites = new List<MUUIImage2DDTO>();
                foreach (Match m in matches)
                {
                    dto.sprites.Add(MUUIImage2DDTO.FromJson(m.Value));
                }
            }

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

            if (sprites != null && sprites.Count > 0)
            {
                sb.Append("\"sprites\":[");
                for (int i = 0; i < sprites.Count; i++)
                {
                    sb.Append(sprites[i].ToJson());
                    if (i < sprites.Count - 1)
                        sb.Append(",");
                }
                sb.Append("],");
            }

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

        public static MUUIButtonAnimated2DDTO FromXML(ref DataSet set, int instanceid)
        {
            var dto = new MUUIButtonAnimated2DDTO();
            //get the RectTransform datarow
            var rows = set.Tables["RectTransform"].Select("instanceid = " + instanceid);
            if (rows.Length > 0) { dto.rect = MUUIRectTransformDTO.FromXML(ref set, instanceid); }
            //get the image data (if any is given)
            rows = set.Tables["Sprites"].Select("instanceid = " + instanceid);
            if (rows != null && rows.Length > 0) {
                for (int i = 0; i < rows.Length; i++)
                {
                    DataRow dr = rows[i];
                    dto.sprites.Add(MUUIImage2DDTO.FromXML(ref dr, instanceid));
                }
            }

            dto.sprites.Sort((a, b) => a.index.CompareTo(b.index));
            //get the Text info for the button (if any is given)
            rows = set.Tables["Text"].Select("instanceid = " + instanceid);
            if (rows.Length > 0) { dto.Text = MUUIText2DDTO.FromXML(ref set, instanceid); }
            //get the event filter settings (if any is given)
            rows = set.Tables["EventFilter"].Select("instanceid = " + instanceid);
            if (rows.Length > 0) { dto.eventFilter = ControlCollectionEventFilterDTO.FromXML(ref set, instanceid); }
            return dto;
        }

        public void ToXML(ref DataSet set)
        {
            var row = schema.NewRow();
            row["label"] = label; row["enabled"] = enabled; row["childof"] = childOf; row["instanceid"] = instanceID;
            set.Tables["ButtonAnimated"].Rows.Add(row); //add self

            //add any sprites
            for(int i = 0; i < sprites.Count; i++)
            {
                sprites[0].index = i; //make sure the index value is set properly
                sprites[i].ToXML(ref set);
            }
            rect.ToXML(ref set); //recttransform
            Text.ToXML(ref set); //text element
            eventFilter.ToXML(ref set); //event filter
        }

        public MUUIButtonAnimated2DDTO()
        {
            if (!schemainitd) { InitSchema(); }
        }

        private static void InitSchema()
        {
            schema = new DataTable("MUUIButtonAnimated2D");
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