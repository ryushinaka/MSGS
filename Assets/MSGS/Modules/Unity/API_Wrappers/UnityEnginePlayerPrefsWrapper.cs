using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.Unity
{
    public static class UnityEnginePlayerPrefsWrapper
    {
        public static bool debug = false;

        public static ValMap Get()
        {
            ValMap map = new ValMap();

            var a = Intrinsic.Create("");
            a.AddParam("key", string.Empty);
            a.AddParam("default", string.Empty);
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.PlayerPrefs.SetString(" + context.GetLocalString("key") + "," + context.GetLocalString("default") + ")");
                if (context.GetLocalString("key").Length > 0 && context.GetLocalString("default").Length > 0)
                {
                    var wi = AlternateThreadDispatcher.Get();
                    wi.Module = UnityModuleName.PlayerPrefs;
                    wi.FunctionName = UnityEnginePlayerPrefsFunctions.SetString;
                    wi.args = new object[2] { context.GetLocalString("key"), context.GetLocalString("default") };

                    AlternateThreadDispatcher.Enqueue(ref wi);
                    wi.eventSlim.Wait();
                    AlternateThreadDispatcher.Return(ref wi);

                    return new Intrinsic.Result(null);
                }
                else { return new Intrinsic.Result(null); }
            };
            map.map.Add(new ValString("SetString"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("SetString",
               "Sets a single string value for the preference identified by the given key.",
               "UnityEngine.PlayerPrefs",
               new List<IntrinsicParameter>()
               {
                   new IntrinsicParameter()
                   {
                       Name = "key",
                       variableType = typeof(string),
                       Comment = "The string literal that identifies the PlayerPref you want to assign to."
                   },
                   new IntrinsicParameter()
                   {
                       Name = "default",
                       variableType = typeof(string),
                       Comment = "The string literal that is assigned to the 'key' parameter. example: SetString(\"mykey\", \"myValue\")"
                   }
               },
               null);

            a = Intrinsic.Create("");
            a.AddParam("key", string.Empty);
            a.AddParam("default", float.NaN);
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.PlayerPrefs.SetFloat(" + context.GetLocalString("key") + "," + context.GetLocalString("default") + ")");

                if (context.GetLocalString("key").Length > 0 && context.GetLocalString("default").Length > 0)
                {
                    var wi = AlternateThreadDispatcher.Get();
                    wi.Module = UnityModuleName.PlayerPrefs;
                    wi.FunctionName = UnityEnginePlayerPrefsFunctions.SetFloat;
                    wi.args = new object[2] { context.GetLocalString("key"), context.GetLocalFloat("default") };

                    AlternateThreadDispatcher.Enqueue(ref wi);
                    wi.eventSlim.Wait();
                    AlternateThreadDispatcher.Return(ref wi);

                    return new Intrinsic.Result(null);
                }
                else { return new Intrinsic.Result(null); }
            };
            map.map.Add(new ValString("SetFloat"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("SetFloat",
               "Sets the float value of the preference identified by the given key. ",
               "UnityEngine.PlayerPrefs",
               new List<IntrinsicParameter>()
               {
                   new IntrinsicParameter()
                   {
                       Name = "key",
                       variableType = typeof(string),
                       Comment = "The string literal that identifies the PlayerPref you want to assign to."
                   },
                   new IntrinsicParameter()
                   {
                       Name = "default",
                       variableType = typeof(float),
                       Comment = "The float value that is assigned to the 'key' parameter. example: SetFloat \"mykey\", 0.002 "
                   }
               },
               null);

            a = Intrinsic.Create("");
            a.AddParam("key", string.Empty);
            a.AddParam("default", (int)0);
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.PlayerPrefs.SetInt(" + context.GetLocalString("key") + "," + context.GetLocalString("default") + ")");
                if (context.GetLocalString("key").Length > 0 && context.GetLocalString("default").Length > 0)
                {
                    var wi = AlternateThreadDispatcher.Get();
                    wi.Module = UnityModuleName.PlayerPrefs;
                    wi.FunctionName = UnityEnginePlayerPrefsFunctions.SetInt;
                    wi.args = new object[2] { context.GetLocalString("key"), context.GetLocalInt("default") };

                    AlternateThreadDispatcher.Enqueue(ref wi);
                    wi.eventSlim.Wait();
                    AlternateThreadDispatcher.Return(ref wi);

                    return new Intrinsic.Result(null);
                }
                else { return new Intrinsic.Result(null); }
            };
            map.map.Add(new ValString("SetInt"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("SetInt",
               "Sets the integer value of the preference identified by the given key. ",
               "UnityEngine.PlayerPrefs",
               new List<IntrinsicParameter>()
               {
                   new IntrinsicParameter()
                   {
                       Name = "key",
                       variableType = typeof(string),
                       Comment = "The string literal that identifies the PlayerPref you want to assign to."
                   },
                   new IntrinsicParameter()
                   {
                       Name = "default",
                       variableType = typeof(int),
                       Comment = "The integer value that is assigned to the 'key' parameter. example: SetFloat \"mykey\", 123456 "
                   }
               },
               null);

            a = Intrinsic.Create("");
            a.AddParam("key", string.Empty);
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.PlayerPrefs.GetInt(" + context.GetLocalString("key") + ")");
                if (context.GetLocalString("key").Length > 0)
                {
                    int value = 0;
                    var wi = AlternateThreadDispatcher.Get();
                    wi.Module = UnityModuleName.PlayerPrefs;
                    wi.FunctionName = UnityEnginePlayerPrefsFunctions.GetInt;
                    wi.args = new object[1] { context.GetLocalString("key") };

                    AlternateThreadDispatcher.Enqueue(ref wi);
                    wi.eventSlim.Wait();
                    value = (int)wi.result;
                    AlternateThreadDispatcher.Return(ref wi);

                    return new Intrinsic.Result(value);
                }
                else { return new Intrinsic.Result(null); }
            };
            map.map.Add(new ValString("GetInt"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("GetInt",
               "gets an integer value for the preference identified by the given key.",
               "UnityEngine.PlayerPrefs",
               new List<IntrinsicParameter>()
               {
                   new IntrinsicParameter()
                   {
                       Name = "key",
                       variableType = typeof(string),
                       Comment = "The string literal that identifies the PlayerPref you want to retrieve."
                   }
               },
               new IntrinsicParameter()
               {
                   Name = "return",
                   variableType = typeof(int),
                   Comment = "The integer value of the 'key' in PlayerPrefs, otherwise returns null"
               });

            a = Intrinsic.Create("");
            a.AddParam("key", string.Empty);
            a.AddParam("default", float.NaN);
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.PlayerPrefs.GetFloat(" + context.GetLocalString("key") + "," + context.GetLocalString("default") + ")");

                if (context.GetLocalString("key").Length > 0)
                {
                    float value = 0f;
                    var wi = AlternateThreadDispatcher.Get();
                    wi.Module = UnityModuleName.PlayerPrefs;
                    wi.FunctionName = UnityEnginePlayerPrefsFunctions.GetFloat;
                    wi.args = new object[2] { context.GetLocalString("key"), context.GetLocalFloat("default") };

                    AlternateThreadDispatcher.Enqueue(ref wi);
                    wi.eventSlim.Wait();
                    value = (float)wi.result;
                    AlternateThreadDispatcher.Return(ref wi);

                    return new Intrinsic.Result(value);
                }
                else { return new Intrinsic.Result(null); }
            };
            map.map.Add(new ValString("GetFloat"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("GetFloat",
               "Sets a single float value for the preference identified by the given key.",
               "UnityEngine.PlayerPrefs",
               new List<IntrinsicParameter>()
               {
                   new IntrinsicParameter()
                   {
                       Name = "key",
                       variableType = typeof(string),
                       Comment = "The string literal that identifies the PlayerPref you want to retrieve."
                   },
                   new IntrinsicParameter()
                   {
                       Name = "default",
                       variableType = typeof(float),
                       Comment = "The float value that is assigned to the 'key' parameter if the 'key' is not found in PlayerPrefs."
                   }
               },
               new IntrinsicParameter()
               {
                   Name = "return",
                   variableType = typeof(float),
                   Comment = "The float value of the 'key' in PlayerPrefs, otherwise returns float.NaN"
               });

            a = Intrinsic.Create("");
            a.AddParam("key", string.Empty);
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.PlayerPrefs.GetString(" + context.GetLocalString("key") + ")");

                if (context.GetLocalString("key").Length > 0)
                {
                    var wi = AlternateThreadDispatcher.Get();
                    wi.Module = UnityModuleName.PlayerPrefs;
                    wi.FunctionName = UnityEnginePlayerPrefsFunctions.GetString;
                    wi.args = new object[2] { context.GetLocalString("key"), context.GetLocalString("default") };

                    AlternateThreadDispatcher.Enqueue(ref wi);
                    wi.eventSlim.Wait();
                    AlternateThreadDispatcher.Return(ref wi);

                    return new Intrinsic.Result((string)wi.result);
                }

                return new Intrinsic.Result(null);
            };
            map.map.Add(new ValString("GetString"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("GetString",
               "Returns the value corresponding to key in the preference file if it exists.",
               "UnityEngine.PlayerPrefs",
               new List<IntrinsicParameter>()
               {
                   new IntrinsicParameter()
                   {
                       Name = "key",
                       variableType = typeof(string),
                       Comment = "The string literal that identifies the PlayerPref you want to retrieve."
                   },
                   new IntrinsicParameter()
                   {
                       Name = "default",
                       variableType = typeof(string),
                       Comment = "The string value that is assigned to the 'return' parameter if the 'key' is not found in PlayerPrefs."
                   }
               },
               new IntrinsicParameter()
               {
                   Name = "return",
                   variableType = typeof(string),
                   Comment = "The string value of the 'key' in PlayerPrefs, otherwise returns string.Empty"
               });

            a = Intrinsic.Create("");
            a.AddParam("key", string.Empty);
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.PlayerPrefs.HasKey(" + context.GetLocalString("key") + ")");

                if (context.GetLocalString("key").Length > 0)
                {
                    bool bigB = false;
                    var wi = AlternateThreadDispatcher.Get();
                    wi.Module = UnityModuleName.PlayerPrefs;
                    wi.FunctionName = UnityEnginePlayerPrefsFunctions.HasKey;
                    wi.args = new object[1] { context.GetLocalString("key") };

                    AlternateThreadDispatcher.Enqueue(ref wi);
                    wi.eventSlim.Wait();
                    bigB = (bool)wi.result;
                    AlternateThreadDispatcher.Return(ref wi);

                    return new Intrinsic.Result(ValNumber.Truth(bigB));
                }

                return new Intrinsic.Result(null);
            };
            map.map.Add(new ValString("HasKey"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("HasKey",
               "Returns true if the given key exists in PlayerPrefs, otherwise returns false.",
               "UnityEngine.PlayerPrefs",
               new List<IntrinsicParameter>()
               {
                   new IntrinsicParameter()
                   {
                       Name = "key",
                       variableType = typeof(string),
                       Comment = "The string literal that identifies the PlayerPref you want to verify it exists."
                   }
               },
               new IntrinsicParameter()
               {
                   Name = "return",
                   variableType = typeof(bool),
                   Comment = "Returns true if 'key' is present in PlayerPrefs, false otherwise."
               });

            a = Intrinsic.Create("");
            a.AddParam("key", string.Empty);
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.PlayerPrefs.DeleteKey(" + context.GetLocalString("key") + ")");
                if (context.GetLocalString("key").Length > 0)
                {
                    var wi = AlternateThreadDispatcher.Get();
                    wi.Module = UnityModuleName.PlayerPrefs;
                    wi.FunctionName = UnityEnginePlayerPrefsFunctions.DeleteKey;
                    wi.args = new object[1] { context.GetLocalString("key") };

                    AlternateThreadDispatcher.Enqueue(ref wi);
                    wi.eventSlim.Wait();
                    AlternateThreadDispatcher.Return(ref wi);
                }

                return new Intrinsic.Result(null);
            };
            map.map.Add(new ValString("DeleteKey"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("DeleteKey",
             "Removes the given key from the PlayerPrefs. If the key does not exist, DeleteKey has no impact.",
             "UnityEngine.PlayerPrefs",
             new List<IntrinsicParameter>()
             {
                   new IntrinsicParameter()
                   {
                       Name = "key",
                       variableType = typeof(string),
                       Comment = "The string literal of the 'key' in PlayerPrefs you want to remove."
                   }
             },
             null);

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.PlayerPrefs.DeleteAll()");

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.PlayerPrefs;
                wi.FunctionName = UnityEnginePlayerPrefsFunctions.DeleteAll;
                
                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                AlternateThreadDispatcher.Return(ref wi);
                
                return new Intrinsic.Result(null);
            };
            map.map.Add(new ValString("DeleteAll"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("DeleteAll",
             "Removes all keys and values from the preferences. Use with caution.",
             "UnityEngine.PlayerPrefs",
             null, null);

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.PlayerPrefs.Save()");

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.PlayerPrefs;
                wi.FunctionName = UnityEnginePlayerPrefsFunctions.Save;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(null);
            };
            map.map.Add(new ValString("Save"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("Save",
             "Saves all modified preferences.",
             "UnityEngine.PlayerPrefs",
             null, null);

            return map;
        }

        public static string GetDebugScriptSource()
        {
            return
                "globals[\"Unity\"][\"PlayerPrefs\"].SetString(\"testString\", \"SomeValue\")\r\n" +
                //"globals[\"Unity\"][\"PlayerPrefs\"].Save() \r\n" +
                "globals[\"Unity\"][\"PlayerPrefs\"].SetFloat(\"testFloat\", 0.00887234432)\r\n" +
                //"globals[\"Unity\"][\"PlayerPrefs\"].Save() \r\n" +
                "globals[\"Unity\"][\"PlayerPrefs\"].SetInt(\"testInt\", 123456)\r\n" +
                //"globals[\"Unity\"][\"PlayerPrefs\"].Save() \r\n" +

                "f = globals[\"Unity\"][\"PlayerPrefs\"].GetInt(\"testInt\")\r\n" +
                "globals[\"Host\"].LogInfo(\"GetInt from key 'testInt' =  \" + f)\r\n" +

                "globals[\"Unity\"][\"PlayerPrefs\"].GetFloat(\"testFloat\") \r\n" +
                "globals[\"Host\"].LogInfo \"GetFloat from key 'testFloat' =  \" + f \r\n" +

                "globals[\"Unity\"][\"PlayerPrefs\"].GetString(\"testString\") \r\n" +
                "globals[\"Host\"].LogInfo \"GetString from key 'testString' =  \" + f \r\n" +

                "globals[\"Unity\"][\"PlayerPrefs\"].DeleteKey(\"testFloat\") \r\n" +
                "f = globals[\"Unity\"][\"PlayerPrefs\"].HasKey(\"testFloat\") \r\n" +
                "globals[\"Host\"].LogInfo \"HasKey \" + f \r\n" +

                "globals[\"Unity\"][\"PlayerPrefs\"].Save() \r\n" +
                "globals[\"Unity\"][\"PlayerPrefs\"].DeleteAll() \r\n";
        }

        public static void HandleWorkItem(ref AlternateThreadWorkItem item)
        {
            if (item.Module != UnityModuleName.PlayerPrefs)
            {
                throw new System.ArgumentNullException("A work item was given to UnityEngine.PlayerPrefs that was not part of the PlayerPrefs callable namespace.");
            }

            switch ((int)item.FunctionName)
            {
                case UnityEnginePlayerPrefsFunctions.DeleteAll:
                    UnityEngine.PlayerPrefs.DeleteAll();
                    item.eventSlim.Set();
                    break;
                case UnityEnginePlayerPrefsFunctions.DeleteKey:
                    UnityEngine.PlayerPrefs.DeleteKey((string)item.args[0]);
                    item.eventSlim.Set();
                    break;
                case UnityEnginePlayerPrefsFunctions.GetFloat:
                    if (UnityEngine.PlayerPrefs.HasKey((string)item.args[0])) {
                        //strange boxing/unboxing behavior in Unity requires this
                        var f =  UnityEngine.PlayerPrefs.GetFloat((string)item.args[0]);
                        item.result = f;
                        item.eventSlim.Set();
                    } else { item.result = float.NaN; item.eventSlim.Set(); }                    
                    break;
                case UnityEnginePlayerPrefsFunctions.GetString:
                    if (UnityEngine.PlayerPrefs.HasKey((string)item.args[0])) {
                        //strange boxing/unboxing behavior in Unity requires this
                        var s = UnityEngine.PlayerPrefs.GetString((string)item.args[0], string.Empty);
                        item.result = s;
                        item.eventSlim.Set();
                    } else { item.result = (object)string.Empty; item.eventSlim.Set(); }                    
                    break;
                case UnityEnginePlayerPrefsFunctions.GetInt:
                    if (UnityEngine.PlayerPrefs.HasKey((string)item.args[0])) {
                        //strange boxing/unboxing behavior in Unity requires this
                        var i = UnityEngine.PlayerPrefs.GetInt((string)item.args[0]);
                        item.result = i;
                        item.eventSlim.Set();
                    } else { item.result = 0; item.eventSlim.Set(); }
                    break;
                case UnityEnginePlayerPrefsFunctions.HasKey:
                    //strange boxing/unboxing behavior in Unity requires this
                    var b = UnityEngine.PlayerPrefs.HasKey((string)item.args[0]);
                    item.result = b;
                    item.eventSlim.Set();
                    break;
                case UnityEnginePlayerPrefsFunctions.Save:
                    UnityEngine.PlayerPrefs.Save();
                    item.eventSlim.Set();
                    break;
                case UnityEnginePlayerPrefsFunctions.SetFloat:
                    UnityEngine.PlayerPrefs.SetFloat((string)item.args[0], (float)item.args[1]);
                    item.eventSlim.Set();
                    break;
                case UnityEnginePlayerPrefsFunctions.SetString:
                    UnityEngine.PlayerPrefs.SetString((string)item.args[0], (string)item.args[1]);
                    item.eventSlim.Set();
                    break;
                case UnityEnginePlayerPrefsFunctions.SetInt:
                    UnityEngine.PlayerPrefs.SetInt((string)item.args[0], (int)item.args[1]);
                    item.eventSlim.Set();
                    break;
                default:
                    throw new System.ArgumentNullException("A work item was given to UnityEngine.PlayerPrefs but the function was invalid.");
            }
        }
    }
}
