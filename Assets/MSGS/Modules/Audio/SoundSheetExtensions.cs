using System.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using MiniScript.MSGS.PackageManager;
using System.IO;

namespace MiniScript.MSGS.Audio
{
    public static class SoundSheetExtensions
    {
        public static void LoadSoundSheet(this Album ss, string filepath)
        {
            if (System.IO.File.Exists(filepath))
            {
                DefaultDataFileChecker dfc = new DefaultDataFileChecker();
                var x = dfc.GetDataFileType(filepath);
                if (x == DataFileTypes.SoundSheet)
                {
                    System.IO.FileStream strm = System.IO.File.OpenRead(filepath);
                    LoadSoundSheet(ss, strm);
                }
                else
                {
                    MiniScriptSingleton.LogError("SoundSheet is not a valid soundsheet format");
                }
            }
            else
            {
                MiniScriptSingleton.LogError("SoundSheet file not found.");
            }
        }

        public static void LoadSoundSheet(this Album ss, Stream stream)
        {
            DataSet set = new DataSet();

            set.ReadXml(stream, XmlReadMode.ReadSchema);
            while (set == null)
            {
                System.Threading.Thread.Sleep(1);
            }

            if (set.Tables.Contains("SoundSheet"))
            {
                if (set.Tables["SoundSheet"].Columns.Contains("Key") &&
                    set.Tables["SoundSheet"].Columns.Contains("UniqueID") &&
                    set.Tables["SoundSheet"].Columns.Contains("Sound") &&
                    set.Tables["SoundSheet"].Columns.Contains("Label"))
                {
                    if (set.Tables["SoundSheet"].Columns["UniqueID"].DataType == typeof(string) &&
                        set.Tables["SoundSheet"].Columns["Sound"].DataType == typeof(byte[]) &&
                        set.Tables["SoundSheet"].Columns["Label"].DataType == typeof(string))
                    {

                        ss.Name = (string)set.Tables["Config"].Rows[0]["Name"];

                        foreach (DataRow dr in set.Tables["SoundSheet"].Rows)
                        {
                            var t = Tuple.Create<string, Guid, AudioClip, List<SoundSheetElementPositions>>(
                                (string)dr["Label"],
                                System.Guid.Parse((string)dr["UniqueID"]),
                                WaveUtility.ToAudioClip((byte[])dr["Sound"], 0, (string)dr["Label"]),
                                new List<SoundSheetElementPositions>()
                                );

                            if(set.Tables.Contains("Indices") && set.Tables["Indices"].Rows.Count > 0)
                            {
                                var rows = set.Tables["Indices"].Select("UniqueID='" + dr["UniqueID"] + "'");
                                foreach (DataRow dr2 in rows)
                                {
                                    t.Item4.Add(new SoundSheetElementPositions()
                                    {
                                        label = (string)dr2["Label"],
                                        position = (float)dr2["Position"]
                                    });
                                }
                            }

                            ss.tuples.Add(t);
                        }
                    }
                    else
                    {
                        MiniScriptSingleton.LogError("SoundSheet column types wrong");
                    }
                }
                else
                {
                    MiniScriptSingleton.LogError("SoundSheet table definition invalid");
                }
            }
            else
            {
                MiniScriptSingleton.LogError("SoundSheet tables incomplete");
            }

            set = null;
        }

        public static void LoadSoundSheet(this Album ss, ref DataSet set)
        {
            if (set.Tables.Contains("SoundSheet"))
            {
                if (set.Tables["SoundSheet"].Columns.Contains("Key") &&
                    set.Tables["SoundSheet"].Columns.Contains("UniqueID") &&
                    set.Tables["SoundSheet"].Columns.Contains("Sound") &&
                    set.Tables["SoundSheet"].Columns.Contains("Label"))
                {
                    if (set.Tables["SoundSheet"].Columns["UniqueID"].DataType == typeof(string) &&
                        set.Tables["SoundSheet"].Columns["Sound"].DataType == typeof(byte[]) &&
                        set.Tables["SoundSheet"].Columns["Label"].DataType == typeof(string))
                    {

                        ss.Name = (string)set.Tables["Config"].Rows[0]["Name"];

                        foreach (DataRow dr in set.Tables["SoundSheet"].Rows)
                        {
                            var t = Tuple.Create<string, Guid, AudioClip, List<SoundSheetElementPositions>>(
                                (string)dr["Label"],
                                System.Guid.Parse((string)dr["UniqueID"]),
                                WaveUtility.ToAudioClip((byte[])dr["Sound"], 0, (string)dr["Label"]),
                                new List<SoundSheetElementPositions>()
                                );

                            if (set.Tables.Contains("Indices") && set.Tables["Indices"].Rows.Count > 0)
                            {
                                var rows = set.Tables["Indices"].Select("UniqueID='" + dr["UniqueID"] + "'");
                                foreach (DataRow dr2 in rows)
                                {
                                    t.Item4.Add(new SoundSheetElementPositions()
                                    {
                                        label = (string)dr2["Label"],
                                        position = (float)dr2["Position"]
                                    });
                                }
                            }

                            ss.tuples.Add(t);
                        }
                    }
                    else
                    {
                        MiniScriptSingleton.LogError("SoundSheet column types wrong");
                    }
                }
                else
                {
                    MiniScriptSingleton.LogError("SoundSheet table definition invalid");
                }
            }
            else
            {
                MiniScriptSingleton.LogError("SoundSheet tables incomplete");
            }
        }

        public static void SaveSoundSheet(this Album ss, string filepath)
        {
            DataSet set = new DataSet();
            DataTable dt = new DataTable("Config");
            DataColumn dc = new DataColumn("Type", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Name", typeof(string)); dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt); set.AcceptChanges();

            DataRow dr = dt.NewRow();
            dr["Type"] = "SoundSheet";
            dr["Name"] = ss.Name;
            set.Tables["Config"].Rows.Add(dr);

            dt = new DataTable("SoundSheet");
            dc = new DataColumn("Key", typeof(int));
            dc.AutoIncrement = true; dc.AutoIncrementSeed = 1;
            dt.Columns.Add(dc); dt.PrimaryKey = new DataColumn[1] { dc };
            dc = new DataColumn("UniqueID", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Label", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Sound", typeof(byte[])); dt.Columns.Add(dc);
            dc = new DataColumn("Channels", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("Samples", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("Frequency", typeof(int)); dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt); set.AcceptChanges();

            dt = new DataTable("Indices");
            dc = new DataColumn("Key", typeof(int));
            dc.AutoIncrement = true; dc.AutoIncrementSeed = 1;
            dt.Columns.Add(dc); dt.PrimaryKey = new DataColumn[1] { dc };
            dc = new DataColumn("UniqueID", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Label", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Position", typeof(float)); dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt); set.AcceptChanges();

            foreach (Tuple<string, Guid, AudioClip, List<SoundSheetElementPositions>> kv in ss.tuples)
            {
                dr = set.Tables["SoundSheet"].NewRow();
                dr["Label"] = kv.Item1;
                dr["UniqueID"] = kv.Item2.ToString();
                dr["Sound"] = WaveUtility.FromAudioClip(kv.Item3);
                dr["Channels"] = kv.Item3.channels;
                dr["Samples"] = kv.Item3.samples;
                dr["Frequency"] = kv.Item3.frequency;

                foreach (SoundSheetElementPositions ssep in kv.Item4)
                {
                    var dr2 = set.Tables["Indices"].NewRow();
                    dr2["UniqueID"] = dr["UniqueID"];
                    dr2["Label"] = ssep.label;
                    dr2["Position"] = ssep.position;
                    set.Tables["Indices"].Rows.Add(dr2);
                }

                set.Tables["SoundSheet"].Rows.Add(dr);
            }

            set.WriteXml(filepath, XmlWriteMode.WriteSchema);
            set = null;
        }

        public static void SaveSoundSheet(this Album ss, System.IO.Stream stream)
        {
            DataSet set = new DataSet();
            DataTable dt = new DataTable("Config");
            DataColumn dc = new DataColumn("Type", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Name", typeof(string)); dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt); set.AcceptChanges();

            DataRow dr = dt.NewRow();
            dr["Type"] = "SoundSheet";
            dr["Name"] = ss.Name;
            set.Tables["Config"].Rows.Add(dr);

            dt = new DataTable("SoundSheet");
            dc = new DataColumn("Key", typeof(int));
            dc.AutoIncrement = true; dc.AutoIncrementSeed = 1;
            dt.Columns.Add(dc); dt.PrimaryKey = new DataColumn[1] { dc };
            dc = new DataColumn("UniqueID", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Label", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Sound", typeof(byte[])); dt.Columns.Add(dc);
            dc = new DataColumn("Channels", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("Samples", typeof(int)); dt.Columns.Add(dc);
            dc = new DataColumn("Frequency", typeof(int)); dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt); set.AcceptChanges();

            dt = new DataTable("Indices");
            dc = new DataColumn("Key", typeof(int));
            dc.AutoIncrement = true; dc.AutoIncrementSeed = 1;
            dt.Columns.Add(dc); dt.PrimaryKey = new DataColumn[1] { dc };
            dc = new DataColumn("UniqueID", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Label", typeof(string)); dt.Columns.Add(dc);
            dc = new DataColumn("Position", typeof(float)); dt.Columns.Add(dc);
            dt.AcceptChanges(); set.Tables.Add(dt); set.AcceptChanges();

            foreach (Tuple<string, Guid, AudioClip, List<SoundSheetElementPositions>> kv in ss.tuples)
            {
                dr = set.Tables["SoundSheet"].NewRow();
                dr["Label"] = kv.Item1;
                dr["UniqueID"] = kv.Item2.ToString();
                dr["Sound"] = WaveUtility.FromAudioClip(kv.Item3);
                dr["Channels"] = kv.Item3.channels;
                dr["Samples"] = kv.Item3.samples;
                dr["Frequency"] = kv.Item3.frequency;

                foreach (SoundSheetElementPositions ssep in kv.Item4)
                {
                    var dr2 = set.Tables["Indices"].NewRow();
                    dr2["UniqueID"] = dr["UniqueID"];
                    dr2["Label"] = ssep.label;
                    dr2["Position"] = ssep.position;
                    set.Tables["Indices"].Rows.Add(dr2);
                }

                set.Tables["SoundSheet"].Rows.Add(dr);
            }

            set.WriteXml(stream, XmlWriteMode.WriteSchema);
            set = null;
        }
    }
}
