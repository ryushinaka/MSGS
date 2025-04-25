using System;
using System.Data;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Text.RegularExpressions;

namespace MiniScript.MSGS.Unity.DataTransferObjects
{
    public class MiniScriptScriptAssetDTO
    {
        public string label;
        public string scontent;
        public Guid guid;
        public int instanceid;

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

        public static MiniScriptScriptAssetDTO FromJson(string json)
        {
            MiniScriptScriptAssetDTO dto = new MiniScriptScriptAssetDTO();
            dto.label = System.Text.UTF8Encoding.UTF8.GetString(System.Convert.FromBase64String(ExtractValue("label", json)));
            dto.scontent = System.Text.UTF8Encoding.UTF8.GetString(System.Convert.FromBase64String(ExtractValue("script", json)));
            dto.scontent = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(dto.scontent));
            dto.guid = Guid.Parse(ExtractValue("guid", json));
            dto.instanceid = int.Parse(ExtractValue("instanceid", json));
            return dto;
        }

        public string ToJson()
        {
            var sb = new StringBuilder();
            sb.Append("{");
            sb.AppendFormat("\"label\": ", System.Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(label)), ",\n");
            sb.Append($"\"script\": \"{System.Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(scontent))},\n");
            sb.Append($"\"guid\": {guid.ToString()},\n");
            sb.Append($"\"instanceid\": \"{instanceid}\",\n");
            sb.Append("}");
            return sb.ToString();
        }

        public static MiniScriptScriptAssetDTO FromXML(ref DataRow row)
        {
            MiniScriptScriptAssetDTO dto = new MiniScriptScriptAssetDTO();
            dto.label = System.Text.UTF8Encoding.UTF8.GetString(System.Convert.FromBase64String(row.Field<string>("label")));
            dto.scontent = System.Text.UTF8Encoding.UTF8.GetString(System.Convert.FromBase64String(row.Field<string>("content")));
            dto.guid = System.Guid.Parse(row.Field<string>("guid"));
            dto.instanceid = row.Field<int>("instanceid");
            return dto;
        }

        public void ToXML(ref DataSet set, out string str)
        {
            var dr = schema.NewRow();
            dr["label"] = System.Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(label));
            dr["content"] = System.Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(scontent));
            str = System.Guid.NewGuid().ToString();
            dr["guid"] = str;
            dr["instanceid"] = instanceid;

            set.Tables["MiniScriptScriptAsset"].Rows.Add(dr);
        }

        public MiniScriptScriptAssetDTO()
        {
            if (!MiniScriptScriptAssetDTO.schemainitd) { InitSchema(); }
        }

        static void InitSchema()
        {
            schema = new DataTable("MiniScriptScriptAsset");
            schema.Columns.Add(new DataColumn("label", typeof(string)));
            schema.Columns.Add(new DataColumn("content", typeof(string)));
            schema.Columns.Add(new DataColumn("guid", typeof(string)));
            schema.Columns.Add(new DataColumn("instanceid", typeof(int)));

            schema.AcceptChanges();
            schemainitd = true;
        }
    }
}
