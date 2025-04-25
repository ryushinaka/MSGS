using UnityEngine;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;

namespace MiniScript.MSGS.Unity.DataTransferObjects
{
    public class MUUIImage2DDTO
    {
        public Sprite image;
        public string label;
        public int index;

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

        private static string ExtractValue(string key, string json)
        {
            var pattern = $"\"{key}\"\\s*:\\s*\"?(.*?)\"?(,|\\s*\\}})";
            var match = Regex.Match(json, pattern);
            return match.Success ? match.Groups[1].Value : null;
        }

        public static MUUIImage2DDTO FromJson(string json)
        {
            var dto = new MUUIImage2DDTO();

            string base64Label = ExtractValue("label", json);
            dto.label = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64Label));

            string imageData = ExtractValue("image", json);
            if (!string.IsNullOrEmpty(imageData))
            {
                byte[] pngData = Convert.FromBase64String(imageData);
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(pngData);
                dto.image = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }

            dto.enabled = bool.TryParse(ExtractValue("enabled", json), out var b) && b;
            dto.childof = int.TryParse(ExtractValue("childof", json), out var co) ? co : -1;
            dto.instanceid = int.TryParse(ExtractValue("instanceid", json), out var id) ? id : -1;
            dto.index = int.TryParse(ExtractValue("index", json), out var idx) ? idx : -1;

            dto.ScriptOnEnter = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("ScriptOnEnter", json)));
            dto.ScriptOnExit = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("ScriptOnExit", json)));
            dto.ScriptOnLeftClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("ScriptOnLeftClick", json)));
            dto.ScriptOnScrollUp = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("ScriptOnScrollUp", json)));
            dto.ScriptOnScrollDown = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("ScriptOnScrollDown", json)));
            dto.ScriptOnDoubleLeftClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("ScriptOnDoubleLeftClick", json)));
            dto.ScriptOnRightClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("ScriptOnRightClick", json)));
            dto.ScriptOnDoubleRightClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("ScriptOnDoubleRightClick", json)));
            dto.ScriptOnMiddleClick = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(ExtractValue("ScriptOnMiddleClick", json)));

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
            string base64Image = image != null && image.texture != null ? Convert.ToBase64String(image.texture.EncodeToPNG()) : "";
            var sb = new StringBuilder();

            sb.Append("{");
            sb.AppendFormat("\"label\": ", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(label ?? "")), ",\n");
            sb.AppendFormat("\"image\": ", base64Image, ",\n");
            sb.AppendFormat("\"index\": ", index, ",\n");
            sb.AppendFormat("\"enabled\": ", enabled.ToString().ToLower(), ",\n");
            sb.AppendFormat("\"childof\": ", childof, ",\n");
            sb.AppendFormat("\"instanceid\": ", instanceid, ",\n");

            sb.AppendFormat("\"ScriptOnEnter\":\"{0}\",\n", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ScriptOnEnter)));
            sb.AppendFormat("\"ScriptOnExit\":\"{0}\",\n", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ScriptOnExit)));
            sb.AppendFormat("\"ScriptOnLeftClick\":\"{0}\",\n", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ScriptOnLeftClick)));
            sb.AppendFormat("\"ScriptOnScrollUp\":\"{0}\",\n", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ScriptOnScrollUp)));
            sb.AppendFormat("\"ScriptOnScrollDown\":\"{0}\",\n", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ScriptOnScrollDown)));
            sb.AppendFormat("\"ScriptOnDoubleLeftClick\":\"{0}\",\n", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ScriptOnDoubleLeftClick)));
            sb.AppendFormat("\"ScriptOnRightClick\":\"{0}\",\n", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ScriptOnRightClick)));
            sb.AppendFormat("\"ScriptOnDoubleRightClick\":\"{0}\",\n", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ScriptOnDoubleRightClick)));
            sb.AppendFormat("\"ScriptOnMiddleClick\":\"{0}\",\n", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ScriptOnMiddleClick)));

            sb.AppendFormat("\"ScriptOnEnterMSA\":\"{0}\",\n", ScriptOnEnterMSA.ToJson());
            sb.AppendFormat("\"ScriptOnExitMSA\":\"{0}\",\n", ScriptOnExitMSA.ToJson());
            sb.AppendFormat("\"ScriptOnLeftClickMSA\":\"{0}\",\n", ScriptOnLeftClickMSA.ToJson());
            sb.AppendFormat("\"ScriptOnScrollUpMSA\":\"{0}\",\n", ScriptOnScrollUpMSA.ToJson());
            sb.AppendFormat("\"ScriptOnScrollDownMSA\":\"{0}\",\n", ScriptOnScrollDownMSA.ToJson());
            sb.AppendFormat("\"ScriptOnDoubleLeftClickMSA\":\"{0}\",\n", ScriptOnDoubleLeftClickMSA.ToJson());
            sb.AppendFormat("\"ScriptOnRightClickMSA\":\"{0}\",\n", ScriptOnRightClickMSA.ToJson());
            sb.AppendFormat("\"ScriptOnDoubleRightClickMSA\":\"{0}\",\n", ScriptOnDoubleRightClickMSA.ToJson());
            sb.AppendFormat("\"ScriptOnMiddleClickMSA\":\"{0}\"\n", ScriptOnMiddleClickMSA.ToJson());
            sb.Append("}");

            return sb.ToString();
        }

        public static MUUIImage2DDTO FromXML(ref DataSet set, int instanceid)
        {
            MUUIImage2DDTO dto = new MUUIImage2DDTO();
            DataRow[] rows = set.Tables["Sprites"].Select("identifier = " + instanceid);
            DataRow row = rows[0];

            dto.label = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(row.Field<string>("label")));
            Texture2D tex = new Texture2D(1, 1);
            tex.LoadImage(Convert.FromBase64String(row.Field<string>("image")));
            dto.image = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            dto.enabled = row.Field<bool>("enabled");
            dto.childof = row.Field<int>("childof");
            dto.instanceid = row.Field<int>("instanceid");
            dto.index = row.Field<int>("index");

            return dto;
        }

        //This is required by MUUIImageAnimated and MUUIButtonAnimated
        public static MUUIImage2DDTO FromXML(ref DataRow row, int instanceid)
        {
            var dto = new MUUIImage2DDTO();
            dto.label = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(row.Field<string>("label")));
            Texture2D tex = new Texture2D(1, 1);
            tex.LoadImage(Convert.FromBase64String(row.Field<string>("image")));
            dto.image = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            dto.enabled = row.Field<bool>("enabled");
            dto.childof = row.Field<int>("childof");
            dto.instanceid = row.Field<int>("instanceid");
            dto.index = row.Field<int>("index");

            return dto;
        }

        public static MUUIImage2DDTO FromXML(ref DataSet set, int instanceid, int indice)
        {
            var dto = new MUUIImage2DDTO();
            DataRow[] rows = set.Tables["MUUIImage2D"].Select("instanceid = " + instanceid);
            if (rows != null && rows.Length > 0 && indice >= 0 && indice <= rows.Length)
            {
                DataRow row = rows[indice];
                dto.label = System.Text.UTF8Encoding.UTF8.GetString(Convert.FromBase64String(row.Field<string>("label")));
                Texture2D tex = new Texture2D(1, 1);
                tex.LoadImage(Convert.FromBase64String(row.Field<string>("image")));
                dto.image = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                dto.enabled = row.Field<bool>("enabled");
                dto.childof = row.Field<int>("childof");
                dto.instanceid = row.Field<int>("instanceid");
                dto.index = row.Field<int>("index");

                dto.ScriptOnEnter = row.Field<string>("ScriptOnEnter");
                
            }

            return dto;
        }

        public void ToXML(ref DataSet set)
        {
            DataRow dr = set.Tables["MUUIImage2D"].NewRow();
            dr["label"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(label));
            dr["image"] = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(
                Convert.ToBase64String(image.texture.EncodeToPNG())));
            dr["enabled"] = enabled;
            dr["childof"] = childof;
            dr["index"] = index;
            dr["instanceid"] = instanceid;
            set.Tables["MUUIImage2D"].Rows.Add(dr);
        }

        public MUUIImage2DDTO()
        {
            if (!MUUIImage2DDTO.schemainitd) { InitSchema(); }
        }

        private static void InitSchema()
        {
            schema = new DataTable("MUUIImage2D");
            schema.Columns.Add(new DataColumn("label", typeof(string)));
            schema.Columns.Add(new DataColumn("image", typeof(string)));
            schema.Columns.Add(new DataColumn("enabled", typeof(bool)));
            schema.Columns.Add(new DataColumn("childof", typeof(int)));
            schema.Columns.Add(new DataColumn("index", typeof(int)));
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