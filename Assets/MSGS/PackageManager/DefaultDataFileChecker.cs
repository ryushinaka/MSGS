using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Data;
using UnityEngine.Serialization;

namespace MiniScript.MSGS.PackageManager
{
    public class DefaultDataFileChecker : IDataFileChecker
    {
        public DataFileTypes GetDataFileType(string filepath)
        {
            if (System.IO.File.Exists(filepath))
            {
                StreamReader rdr = new StreamReader(File.OpenRead(filepath));
                return GetDataFileType(ref rdr);
            }

            return DataFileTypes.Unknown;
        }

        public DataFileTypes GetDataFileType(ref StreamReader stream)
        {
            //first we check to see if its valid DataSet XML
            try
            {
                DataSet set = new DataSet();
                set.ReadXml(stream, XmlReadMode.ReadSchema);

                if (set.Tables.Count > 0)
                {
                    //looks like we have a dataset of some kind
                    if (set.Tables.Contains("Config"))
                    {
                        if (set.Tables["Config"].Columns.Contains("Type") && set.Tables["Config"].Columns.Contains("Name"))
                        {
                            if (set.Tables["Config"].Rows.Count == 1)
                            {
                                switch ((string)set.Tables["Config"].Rows[0]["Type"])
                                {
                                    case "Prefab":
                                        return DataFileTypes.UIprefab;
                                    case "Data":
                                        if (set.Tables.Count > 2)
                                        {
                                            return DataFileTypes.DataTypeWithRecords;
                                        }
                                        else
                                        {
                                            return DataFileTypes.DataType;
                                        }
                                    case "SpriteSheet":
                                        return DataFileTypes.SpriteSheet;
                                    case "SoundSheet":
                                        return DataFileTypes.SoundSheet;

                                    default:
                                        Debug.Log("herpderp: " + set.DataSetName + " tables: " + set.Tables.Count);
                                        break;
                                }
                            }
                            else
                            {
                                Debug.Log("XML file Config has " + set.Tables["Config"].Rows.Count + " rows, expected 1");
                                return DataFileTypes.Unknown;
                            }
                        }
                        else
                        {
                            Debug.Log("XML file missing proper columns for Config table");
                            return DataFileTypes.Unknown;
                        }
                    }
                    else
                    {
                        Debug.Log("XML file missing expected Config table [" + set.DataSetName + "]");
                        return DataFileTypes.Unknown;
                    }
                }
            }
            catch { }

            return DataFileTypes.Unknown;
        }

        bool isValidDataType(ref DataSet set)
        {
            if (set.Tables.Contains("Config"))
            {
                if (set.Tables["Config"].Columns.Contains("Type") && set.Tables["Config"].Columns.Contains("Name"))
                {
                    if (set.Tables["Config"].Rows.Count == 1)
                    {
                        //since it has the Config table, we'll blindly assume the rest of the dataset is formatted correctly
                        return true;
                    }
                    return false;
                }
            }

            return false;
        }
    }
}

