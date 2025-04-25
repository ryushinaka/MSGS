using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace MiniScript.MSGS.Unity.DataTransferObjects
{
    public class MUUIText2DDTO
    {
        public string label;
        public string textvalue;
        public float fontsize;
        public string font;
        public int fontsizemin, fontsizemax;
        public bool autosize;
        public int textcolorR, textcolorG, textcolorB, textColorA;
        public string alignHorizontal, alignVertical;
        public bool bold, italic, underline, strike, lowercase, uppercase, smallcaps;

        public ControlCollectionEventFilterDTO eventFilter;

        public bool enabled;
        public int childof, instanceid;

        public MiniScriptScriptAssetDTO ScriptOnEnterMSA, ScriptOnExitMSA, ScriptOnLeftClickMSA, ScriptOnScrollUpMSA, ScriptOnScrollDownMSA,
          ScriptOnDoubleLeftClickMSA, ScriptOnRightClickMSA, ScriptOnDoubleRightClickMSA, ScriptOnMiddleClickMSA;

        public string ScriptOnEnter, ScriptOnExit, ScriptOnLeftClick, ScriptOnScrollUp, ScriptOnScrollDown,
            ScriptOnDoubleLeftClick, ScriptOnRightClick, ScriptOnDoubleRightClick, ScriptOnMiddleClick;

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

        public static MUUIText2DDTO FromJson(string json)
        {
            var dto = new MUUIText2DDTO();
            if (string.IsNullOrEmpty(json)) { return dto; }

            dto.label = ExtractValue("label", json);
            bool.TryParse(ExtractValue("enabled", json), out dto.enabled);
            int.TryParse(ExtractValue("childof", json), out dto.childof);
            int.TryParse(ExtractValue("instanceid", json), out dto.instanceid);

            dto.textvalue = ExtractValue("textvalue", json);
            dto.font = ExtractValue("font", json);
            dto.alignHorizontal = ExtractValue("alignHorizontal", json);
            dto.alignVertical = ExtractValue("alignVertical", json);

            float.TryParse(ExtractValue("fontsize", json), out dto.fontsize);
            int.TryParse(ExtractValue("fontsizemin", json), out dto.fontsizemin);
            int.TryParse(ExtractValue("fontsizemax", json), out dto.fontsizemax);

            int.TryParse(ExtractValue("textcolorR", json), out dto.textcolorR);
            int.TryParse(ExtractValue("textcolorG", json), out dto.textcolorG);
            int.TryParse(ExtractValue("textcolorB", json), out dto.textcolorB);
            int.TryParse(ExtractValue("textColorA", json), out dto.textColorA);

            bool.TryParse(ExtractValue("autosize", json), out dto.autosize);
            bool.TryParse(ExtractValue("bold", json), out dto.bold);
            bool.TryParse(ExtractValue("italic", json), out dto.italic);
            bool.TryParse(ExtractValue("underline", json), out dto.underline);
            bool.TryParse(ExtractValue("strike", json), out dto.strike);
            bool.TryParse(ExtractValue("lowercase", json), out dto.lowercase);
            bool.TryParse(ExtractValue("uppercase", json), out dto.uppercase);
            bool.TryParse(ExtractValue("smallcaps", json), out dto.smallcaps);

            dto.eventFilter = ControlCollectionEventFilterDTO.FromJson(ExtractValue("eventFilter", json));

            dto.ScriptOnEnter = System.Text.UTF8Encoding.UTF8.GetString(System.Convert.FromBase64String(ExtractValue("ScriptOnEnter", json)));
            dto.ScriptOnExit = System.Text.UTF8Encoding.UTF8.GetString(System.Convert.FromBase64String(ExtractValue("ScriptOnExit", json)));
            dto.ScriptOnLeftClick = System.Text.UTF8Encoding.UTF8.GetString(System.Convert.FromBase64String(ExtractValue("ScriptOnLeftClick", json)));
            dto.ScriptOnScrollUp = System.Text.UTF8Encoding.UTF8.GetString(System.Convert.FromBase64String(ExtractValue("ScriptOnScrollUp", json)));
            dto.ScriptOnScrollDown = System.Text.UTF8Encoding.UTF8.GetString(System.Convert.FromBase64String(ExtractValue("ScriptOnScrollDown", json)));
            dto.ScriptOnDoubleLeftClick = System.Text.UTF8Encoding.UTF8.GetString(System.Convert.FromBase64String(ExtractValue("ScriptOnDoubleLeftClick", json)));
            dto.ScriptOnRightClick = System.Text.UTF8Encoding.UTF8.GetString(System.Convert.FromBase64String(ExtractValue("ScriptOnRightClick", json)));
            dto.ScriptOnDoubleRightClick = System.Text.UTF8Encoding.UTF8.GetString(System.Convert.FromBase64String(ExtractValue("ScriptOnDoubleRightClick", json)));
            dto.ScriptOnMiddleClick = System.Text.UTF8Encoding.UTF8.GetString(System.Convert.FromBase64String(ExtractValue("ScriptOnMiddleClick", json)));

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
            sb.Append($"\"label\":\"{label}\",\n");
            sb.Append($"\"enabled\":{enabled.ToString().ToLower()},\n");
            sb.Append($"\"childof\":{childof},\n");
            sb.Append($"\"instanceid\":{instanceid},\n");

            sb.Append($"\"textvalue\":\"{textvalue}\",\n");
            sb.Append($"\"fontsize\":{fontsize},\n");
            sb.Append($"\"font\":\"{font}\",\n");
            sb.Append($"\"fontsizemin\":{fontsizemin},\n");
            sb.Append($"\"fontsizemax\":{fontsizemax},\n");
            sb.Append($"\"autosize\":{autosize.ToString().ToLower()},\n");
            sb.Append($"\"textcolorR\":{textcolorR},\n");
            sb.Append($"\"textcolorG\":{textcolorG},\n");
            sb.Append($"\"textcolorB\":{textcolorB},\n");
            sb.Append($"\"textColorA\":{textColorA},\n");
            sb.Append($"\"alignHorizontal\":\"{alignHorizontal}\",\n");
            sb.Append($"\"alignVertical\":\"{alignVertical}\",\n");
            sb.Append($"\"bold\":{bold.ToString().ToLower()},\n");
            sb.Append($"\"italic\":{italic.ToString().ToLower()},\n");
            sb.Append($"\"underline\":{underline.ToString().ToLower()},\n");
            sb.Append($"\"strike\":{strike.ToString().ToLower()},\n");
            sb.Append($"\"lowercase\":{lowercase.ToString().ToLower()},\n");
            sb.Append($"\"uppercase\":{uppercase.ToString().ToLower()},\n");
            sb.Append($"\"smallcaps\":{smallcaps.ToString().ToLower()},\n");
            
            sb.AppendFormat("\"ScriptOnEnter\":\"{0}\",\n", ScriptOnEnter);
            sb.AppendFormat("\"ScriptOnExit\":\"{0}\",\n", ScriptOnExit);
            sb.AppendFormat("\"ScriptOnLeftClick\":\"{0}\",\n", ScriptOnLeftClick);
            sb.AppendFormat("\"ScriptOnScrollUp\":\"{0}\",\n", ScriptOnScrollUp);
            sb.AppendFormat("\"ScriptOnScrollDown\":\"{0}\",\n", ScriptOnScrollDown);
            sb.AppendFormat("\"ScriptOnDoubleLeftClick\":\"{0}\",\n", ScriptOnDoubleLeftClick);
            sb.AppendFormat("\"ScriptOnRightClick\":\"{0}\",\n", ScriptOnRightClick);
            sb.AppendFormat("\"ScriptOnDoubleRightClick\":\"{0}\",\n", ScriptOnDoubleRightClick);
            sb.AppendFormat("\"ScriptOnMiddleClick\":\"{0}\",\n", ScriptOnMiddleClick);

            sb.AppendFormat("\"ScriptOnEnterMSA\":\"{0}\",\n", ScriptOnEnterMSA.ToJson());
            sb.AppendFormat("\"ScriptOnExitMSA\":\"{0}\",\n", ScriptOnExitMSA.ToJson());
            sb.AppendFormat("\"ScriptOnLeftClickMSA\":\"{0}\",\n", ScriptOnLeftClickMSA.ToJson());
            sb.AppendFormat("\"ScriptOnScrollUpMSA\":\"{0}\",\n", ScriptOnScrollUpMSA.ToJson());
            sb.AppendFormat("\"ScriptOnScrollDownMSA\":\"{0}\",\n", ScriptOnScrollDownMSA.ToJson());
            sb.AppendFormat("\"ScriptOnDoubleLeftClickMSA\":\"{0}\",\n", ScriptOnDoubleLeftClickMSA.ToJson());
            sb.AppendFormat("\"ScriptOnRightClickMSA\":\"{0}\",\n", ScriptOnRightClickMSA.ToJson());
            sb.AppendFormat("\"ScriptOnDoubleRightClickMSA\":\"{0}\",\n", ScriptOnDoubleRightClickMSA.ToJson());
            sb.AppendFormat("\"ScriptOnMiddleClickMSA\":\"{0}\",\n", ScriptOnMiddleClickMSA.ToJson());

            sb.AppendFormat("\"eventFilter\":\"{0}\"\n", eventFilter.ToJson());

            sb.Append("}");
            return sb.ToString();
        }

        public static MUUIText2DDTO FromXML(ref DataSet set, int instanceid)
        {
            var dto = new MUUIText2DDTO();
            DataRow[] rows = set.Tables["MUUIText2D"].Select("identifier = " + instanceid);
            DataRow row = rows[0];

            dto.label = row.Field<string>("label");
            dto.textvalue = row.Field<string>("textvalue");
            dto.font = row.Field<string>("font");
            dto.alignHorizontal = row.Field<string>("alignHorizontal");
            dto.alignVertical = row.Field<string>("alignVertical");

            dto.fontsize = row.Field<float>("fontsize");
            dto.fontsizemin = row.Field<int>("fontsizemin");
            dto.fontsizemax = row.Field<int>("fontsizemax");

            dto.textcolorR = row.Field<int>("textcolorR");
            dto.textcolorG = row.Field<int>("textcolorG");
            dto.textcolorB = row.Field<int>("textcolorB");
            dto.textColorA = row.Field<int>("textColorA");

            dto.autosize = row.Field<bool>("autosize");
            dto.bold = row.Field<bool>("bold");
            dto.italic = row.Field<bool>("italic");
            dto.underline = row.Field<bool>("underline");
            dto.strike = row.Field<bool>("strike");
            dto.lowercase = row.Field<bool>("lowercase");
            dto.uppercase = row.Field<bool>("uppercase");
            dto.smallcaps = row.Field<bool>("smallcaps");
            dto.enabled = row.Field<bool>("enabled");

            dto.childof = row.Field<int>("childof");
            dto.instanceid = row.Field<int>("instanceid");

            dto.ScriptOnEnter = row.Field<string>("ScriptOnEnter");
            dto.ScriptOnExit = row.Field<string>("ScriptOnExit");
            dto.ScriptOnLeftClick = row.Field<string>("ScriptOnLeftClick");
            dto.ScriptOnScrollUp = row.Field<string>("ScriptOnScrollUp");
            dto.ScriptOnScrollDown = row.Field<string>("ScriptOnScrollDown");
            dto.ScriptOnDoubleLeftClick = row.Field<string>("ScriptOnDoubleLeftClick");
            dto.ScriptOnRightClick = row.Field<string>("ScriptOnRightClick");
            dto.ScriptOnDoubleRightClick = row.Field<string>("ScriptOnDoubleRightClick");
            dto.ScriptOnMiddleClick = row.Field<string>("ScriptOnMiddleClick");


            rows = set.Tables["MiniScriptScriptAsset"].Select("guid = " + row.Field<string>("ScriptOnEnterMSA"));
            if (rows != null && rows.Length > 0) dto.ScriptOnEnterMSA = MiniScriptScriptAssetDTO.FromXML(ref rows[0]);
            rows = set.Tables["MiniScriptScriptAsset"].Select("guid = " + row.Field<string>("ScriptOnExitMSA"));
            if (rows != null && rows.Length > 0) dto.ScriptOnExitMSA = MiniScriptScriptAssetDTO.FromXML(ref rows[0]);
            rows = set.Tables["MiniScriptScriptAsset"].Select("guid = " + row.Field<string>("ScriptOnLeftClickMSA"));
            if (rows != null && rows.Length > 0) dto.ScriptOnExitMSA = MiniScriptScriptAssetDTO.FromXML(ref rows[0]);
            rows = set.Tables["MiniScriptScriptAsset"].Select("guid = " + row.Field<string>("ScriptOnScrollUpMSA"));
            if (rows != null && rows.Length > 0) dto.ScriptOnExitMSA = MiniScriptScriptAssetDTO.FromXML(ref rows[0]);
            rows = set.Tables["MiniScriptScriptAsset"].Select("guid = " + row.Field<string>("ScriptOnScrollDownMSA"));
            if (rows != null && rows.Length > 0) dto.ScriptOnExitMSA = MiniScriptScriptAssetDTO.FromXML(ref rows[0]);
            rows = set.Tables["MiniScriptScriptAsset"].Select("guid = " + row.Field<string>("ScriptOnDoubleLeftClickMSA"));
            if (rows != null && rows.Length > 0) dto.ScriptOnExitMSA = MiniScriptScriptAssetDTO.FromXML(ref rows[0]);
            rows = set.Tables["MiniScriptScriptAsset"].Select("guid = " + row.Field<string>("ScriptOnRightClickMSA"));
            if (rows != null && rows.Length > 0) dto.ScriptOnExitMSA = MiniScriptScriptAssetDTO.FromXML(ref rows[0]);
            rows = set.Tables["MiniScriptScriptAsset"].Select("guid = " + row.Field<string>("ScriptOnDoubleRightClickMSA"));
            if (rows != null && rows.Length > 0) dto.ScriptOnExitMSA = MiniScriptScriptAssetDTO.FromXML(ref rows[0]);
            rows = set.Tables["MiniScriptScriptAsset"].Select("guid = " + row.Field<string>("ScriptOnMiddleClickMSA"));
            if (rows != null && rows.Length > 0) dto.ScriptOnExitMSA = MiniScriptScriptAssetDTO.FromXML(ref rows[0]);

            return dto;
        }

        public void ToXML(ref DataSet set)
        {
            var row = schema.NewRow();

            row["label"] = label;
            row["textvalue"] = textvalue;
            row["fontsize"] = fontsize;
            row["font"] = font;
            row["fontsizemin"] = fontsizemin;
            row["fontsizemax"] = fontsizemax;
            row["autosize"] = autosize;
            row["textcolorR"] = textcolorR;
            row["textcolorG"] = textcolorG;
            row["textcolorB"] = textcolorB;
            row["textColorA"] = textColorA;
            row["alignHorizontal"] = alignHorizontal;
            row["alignVertical"] = alignVertical;
            row["bold"] = bold;
            row["italic"] = italic;
            row["underline"] = underline;
            row["strike"] = strike;
            row["lowercase"] = lowercase;
            row["uppercase"] = uppercase;
            row["smallcaps"] = smallcaps;
            row["enabled"] = enabled;
            row["childof"] = childof;
            row["instanceid"] = instanceid;

            //any scripts called by name for these events
            row["ScriptOnEnter"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnEnter));
            row["ScriptOnExit"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnExit));
            row["ScriptOnLeftClick"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnLeftClick));
            row["ScriptOnScrollUp"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnScrollUp));
            row["ScriptOnScrollDown"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnScrollDown));
            row["ScriptOnDoubleLeftClick"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnDoubleLeftClick));
            row["ScriptOnRightClick"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnRightClick));
            row["ScriptOnDoubleRightClick"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnDoubleRightClick));
            row["ScriptOnMiddleClick"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(ScriptOnMiddleClick));

            //and now any MiniScriptScriptAsset objects that may be called for these events
            string tmp = string.Empty;
            ScriptOnEnterMSA.ToXML(ref set, out tmp); row["ScriptOnEnterMSA"] = tmp;
            ScriptOnExitMSA.ToXML(ref set, out tmp); row["ScriptOnExitMSA"] = tmp;
            ScriptOnLeftClickMSA.ToXML(ref set, out tmp); row["ScriptOnLeftClickMSA"] = tmp;
            ScriptOnScrollUpMSA.ToXML(ref set, out tmp); row["ScriptOnScrollUpMSA"] = tmp;
            ScriptOnScrollDownMSA.ToXML(ref set, out tmp); row["ScriptOnScrollDownMSA"] = tmp;
            ScriptOnDoubleLeftClickMSA.ToXML(ref set, out tmp); row["ScriptOnDoubleLeftClickMSA"] = tmp;
            ScriptOnRightClickMSA.ToXML(ref set, out tmp); row["ScriptOnRightClickMSA"] = tmp;
            ScriptOnDoubleRightClickMSA.ToXML(ref set, out tmp); row["ScriptOnDoubleRightClickMSA"] = tmp;
            ScriptOnMiddleClickMSA.ToXML(ref set, out tmp); row["ScriptOnMiddleClickMSA"] = tmp;

            set.Tables["MUUIText2D"].Rows.Add(row);
        }

        public MUUIText2DDTO()
        {
            if (!schemainitd) { InitSchema(); }
        }

        private static void InitSchema()
        {
            schema = new DataTable("MUUIText2D");
            schema.Columns.Add(new DataColumn("label", typeof(string)));
            schema.Columns.Add(new DataColumn("enabled", typeof(bool)));
            schema.Columns.Add(new DataColumn("childof", typeof(int)));
            schema.Columns.Add(new DataColumn("instanceid", typeof(int)));

            schema.Columns.Add(new DataColumn("textvalue", typeof(string)));
            schema.Columns.Add(new DataColumn("fontsize", typeof(float)));
            schema.Columns.Add(new DataColumn("font", typeof(string)));
            schema.Columns.Add(new DataColumn("fontsizemin", typeof(int)));
            schema.Columns.Add(new DataColumn("fontsizemax", typeof(int)));
            schema.Columns.Add(new DataColumn("autosize", typeof(bool)));
            schema.Columns.Add(new DataColumn("textcolorR", typeof(int)));
            schema.Columns.Add(new DataColumn("textcolorG", typeof(int)));
            schema.Columns.Add(new DataColumn("textcolorB", typeof(int)));
            schema.Columns.Add(new DataColumn("textColorA", typeof(int)));
            schema.Columns.Add(new DataColumn("alignHorizontal", typeof(string)));
            schema.Columns.Add(new DataColumn("alignVertical", typeof(string)));
            schema.Columns.Add(new DataColumn("bold", typeof(bool)));
            schema.Columns.Add(new DataColumn("italic", typeof(bool)));
            schema.Columns.Add(new DataColumn("underline", typeof(bool)));
            schema.Columns.Add(new DataColumn("strike", typeof(bool)));
            schema.Columns.Add(new DataColumn("lowercase", typeof(bool)));
            schema.Columns.Add(new DataColumn("uppercase", typeof(bool)));
            schema.Columns.Add(new DataColumn("smallcaps", typeof(bool)));

            schema.Columns.Add(new DataColumn("ScriptOnEnter", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnExit", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnLeftClick", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnScrollUp", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnScrollDown", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnDoubleLeftClick", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnRightClick", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnDoubleRightClick", typeof(string)));
            schema.Columns.Add(new DataColumn("ScriptOnMiddleClick", typeof(string)));
          
            schema.AcceptChanges();
            schemainitd = true;
        }
    }
}
