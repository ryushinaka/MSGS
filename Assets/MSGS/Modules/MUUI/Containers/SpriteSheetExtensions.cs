using UnityEngine;
using System.Data;
using System;

namespace MiniScript.MSGS.MUUI
{
    public static class SpriteSheetExtensions
    {


        public static SpriteSheet LoadSpriteSheet(this SpriteSheet ss, ref DataSet set)
        {
            SpriteSheet result = new SpriteSheet();
            result.Name = set.DataSetName;

            //Debug.Log(set.DataSetName + " " + set.Tables.Count);

            if (set.Tables.Contains("SpriteSheet"))
            {
                if (set.Tables["SpriteSheet"].Columns.Contains("UniqueID") &&
                    set.Tables["SpriteSheet"].Columns.Contains("Sprite") &&
                    set.Tables["SpriteSheet"].Columns.Contains("Label"))
                {
                    if (set.Tables["SpriteSheet"].Columns["UniqueID"].DataType == typeof(string) &&
                        set.Tables["SpriteSheet"].Columns["Sprite"].DataType == typeof(string) &&
                        set.Tables["SpriteSheet"].Columns["Label"].DataType == typeof(string))
                    {
                        foreach (DataRow dr in set.Tables["SpriteSheet"].Rows)
                        {
                            Texture2D tmp = new Texture2D(1, 1);
                            byte[] val = System.Convert.FromBase64String(dr["Sprite"].ToString());
                            tmp.LoadImage(val);

                            var x = Sprite.Create(tmp, new Rect(0, 0, tmp.width, tmp.height), new Vector2(0, 0));

                            result.tuples.Add(Tuple.Create<string, string, Sprite>((string)dr["Label"], (string)dr["UniqueID"], x));
                        }

                        return result;
                    }
                    else
                    {
                        MiniScriptSingleton.LogError("Call to SpriteSheet.Load failed as the xml dataset did not contain properly declared columns for the 'SpriteAtlas' datatable.");
                        return null;
                    }
                }
                else
                {
                    MiniScriptSingleton.LogError("Call to SpriteSheet.Load failed as the xml dataset did not contain a properly declared datatable for 'SpriteAtlas'.");
                    return null;
                }
            }
            else
            {
                MiniScriptSingleton.LogError("Call to SpriteAtlas.Load failed as the expected xml dataset did not have the 'SpriteAtlas' datatable declared.");
                return null;
            }
        }

        public static void SaveSpriteSheet(this SpriteSheet ss, string filepath)
        {
            if (ss.Name.Length == 0)
            {
                MiniScriptSingleton.LogError("SpriteSheet must have a Name value assigned to it before it can be saved to file.");
                return;
            }

            DataSet set = new DataSet(ss.Name);
            set.DataSetName = ss.Name;
            DataTable dt = new DataTable("SpriteSheet");
            DataColumn dc = new DataColumn("UniqueID", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Label", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Sprite", typeof(string)); dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt); set.AcceptChanges();

            dt = new DataTable("Config");
            dc = new DataColumn("Type", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Name", typeof(string)); dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt); set.AcceptChanges();
            DataRow dr = dt.NewRow();
            dr["Type"] = "SpriteSheet";
            dr["Name"] = ss.Name;
            dt.Rows.Add(dr);

            foreach (Tuple<string, string, Sprite> tu in ss.tuples)
            {
                dr = set.Tables["SpriteSheet"].NewRow();
                dr["UniqueID"] = tu.Item1;
                dr["Label"] = tu.Item2;
                byte[] barr = tu.Item3.texture.EncodeToPNG();
                dr["Sprite"] = System.Convert.ToBase64String(barr);
                set.Tables["SpriteSheet"].Rows.Add(dr);
            }

            set.WriteXml(filepath, XmlWriteMode.WriteSchema);
        }
    }
}

