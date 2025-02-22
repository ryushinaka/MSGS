using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.Host
{
    public static class HostModule
    {
        static ValMap hostIntrinsics;
        public static bool debug = false;

        public static ValMap Get()
        {
            if (hostIntrinsics != null) { return hostIntrinsics; }
            else
            {
                Initialize(); return hostIntrinsics;
            }
        }

        public static void Initialize()
        {
            hostIntrinsics = new ValMap();

            var a = Intrinsic.Create("");
            #region GetGUID            
            a.code = (context, partialResult) =>
             {
                 var s = TempValString.Get(System.Guid.NewGuid().ToString());
                 return new Intrinsic.Result(s, true);
             };

            hostIntrinsics.map.Add(new ValString("GetGUID"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("GetGUID",
                "Instructs Unity3D to generate a GUID from the .NET API call Guid.NewGuid().ToString()",
                "Host",
                 new List<IntrinsicParameter>() { },
                new IntrinsicParameter() { });
            #endregion

            a = Intrinsic.Create("");
            #region Command Line Arguments
            a.code = (context, partialResult) =>
            {
                CommandLineArguments cla = new CommandLineArguments();
                if (cla.hasArguments)
                {
                    return new Intrinsic.Result(cla.ToValMap(), true);
                }
                else
                {
                    return new Intrinsic.Result(null, true);
                }
            };

            hostIntrinsics.map.Add(new ValString("CLIargs"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("CLIargs",
                "Returns the ValMap containing any/all arguments given when the Host process started",
                "Host",
                 new List<IntrinsicParameter>() { },
                new IntrinsicParameter() { });
            #endregion

            a = Intrinsic.Create("");
            #region LoadMod
            a.code = (context, partialResult) =>
            {

                return new Intrinsic.Result(null, true);
            };

            hostIntrinsics.map.Add(new ValString("LoadMod"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("LoadMod",
                "Unity3D will load the specified mod file/archive, replacing the existing loaded mod if any.",
                "Host",
                 new List<IntrinsicParameter>() { },
                new IntrinsicParameter() { });
            #endregion

            a = Intrinsic.Create("");
            #region QuitGame
            a.code = (context, partialResult) =>
            {

                return new Intrinsic.Result(null, true);
            };

            hostIntrinsics.map.Add(new ValString("QuitGame"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("QuitGame",
                "Unity3D will unload the mod and any session related data, going back to the mod selection scene.",
                "Host",
                 new List<IntrinsicParameter>() { },
                new IntrinsicParameter() { });
            #endregion

            a = Intrinsic.Create("");
            #region StartGame
            a.code = (context, partialResult) =>
            {

                return new Intrinsic.Result(null, true);
            };

            hostIntrinsics.map.Add(new ValString("StartGame"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("StartGame",
                "Unity3D will start the loaded mod, calling the 'startup' script within the mod archive.",
                "Host",
                 new List<IntrinsicParameter>() { },
                new IntrinsicParameter() { });
            #endregion

            a = Intrinsic.Create("");
            #region Reset
            a.code = (context, partialResult) =>
            {

                return new Intrinsic.Result(null, true);
            };

            hostIntrinsics.map.Add(new ValString("Reset"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("Reset",
                "Unity3D will aggressively unload the mod, clean any session data, and bring the current " +
                "environment to a 'fresh' state.",
                "Host",
                 new List<IntrinsicParameter>() { },
                new IntrinsicParameter() { });
            #endregion

            a = Intrinsic.Create("");
            #region LogWarning
            a.AddParam("msg", string.Empty);
            a.code = (context, partialResult) =>
            {
                MiniScriptSingleton.LogWarning(context.GetLocalString("msg"));
                return new Intrinsic.Result(null, true);
            };

            hostIntrinsics.map.Add(new ValString("LogWarning"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("LogWarning",
                "Unity3D ",
                "Host",
                 new List<IntrinsicParameter>() { },
                new IntrinsicParameter() { });
            #endregion

            a = Intrinsic.Create("");
            #region LogError
            a.AddParam("msg", string.Empty);
            a.code = (context, partialResult) =>
            {
                MiniScriptSingleton.LogError(context.GetLocalString("msg"));
                return new Intrinsic.Result(null, true);
            };

            hostIntrinsics.map.Add(new ValString("LogError"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("LogError",
                "Unity3D ",
                "Host",
                 new List<IntrinsicParameter>() { },
                new IntrinsicParameter() { });
            #endregion

            a = Intrinsic.Create("");
            #region LogInfo
            a.AddParam("msg", string.Empty);
            a.code = (context, partialResult) =>
            {
                MiniScriptSingleton.LogInfo(context.GetLocalString("msg"));
                return new Intrinsic.Result(null, true);
            };

            hostIntrinsics.map.Add(new ValString("LogInfo"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("LogInfo",
                "Unity3D ",
                "Host",
                 new List<IntrinsicParameter>() { },
                new IntrinsicParameter() { });
            #endregion

            a = Intrinsic.Create("");
            #region PrettyPrint
            a.AddParam("value");
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("Host.PrettyPrint " + context.GetLocal("value").ToString()); }

                Value v = context.GetLocal("value");
                string result = string.Empty;
                if (v is ValNumber)
                {
                    ValNumber v1 = v as ValNumber;
                    result = v1.ToString(context.vm);
                }
                else if (v is ValString)
                {
                    ValString v2 = v as ValString;
                    result = v2.value;
                }
                else if (v is ValMap)
                {
                    ValMap v3 = v as ValMap;
                    System.Text.StringBuilder str = new System.Text.StringBuilder();
                    str.Append("Map[");
                    foreach (KeyValuePair<Value, Value> kv in v3.map)
                    {
                        str.Append("{" + kv.Key.ToString(context.vm) + ":" + kv.Value.ToString(context.vm) + "},");
                    }
                    str.Append("]");
                    result = str.ToString();
                }
                else if (v is ValList)
                {
                    System.Text.StringBuilder str = new System.Text.StringBuilder();
                    ValList v4 = v as ValList;
                    str.Append("List[" + v4.values.Count + "]: ");
                    foreach (Value vv in v4.values)
                    {
                        str.Append(vv.ToString(context.vm) + ",");
                    }
                    result = str.ToString();
                }
                else if (v is ValFunction)
                {
                    ValFunction v6 = v as ValFunction;
                    result = v6.ToString(context.vm);
                }
                else { result = "oopsie? " + v.FullEval(context).GetType().FullName; }

                return new Intrinsic.Result(new ValString(result), true);
            };

            hostIntrinsics.map.Add(new ValString("PrettyPrint"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("PrettyPrint",
                "Takes the MiniScript value given and creates a string formatted representation of that object.",
                "Host",
                 new List<IntrinsicParameter>() {
                     new IntrinsicParameter()
                     {
                         Name = "value",
                         variableType = typeof(Value),
                         Comment = "Any MiniScript object type (ValNumber, ValString, ValMap, ValList, Value)"
                     }
                 },
                new IntrinsicParameter()
                {
                    Name = "return",
                    variableType = typeof(string),
                    Comment = "The string representation of the 'value' object passed to the instrinsic function."
                });
            #endregion
        }
    }
}
