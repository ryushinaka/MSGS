using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.ScriptableObjects
{
    public static class ScriptableObjectModule
    {

        static ValMap soIntrinsics;
        static bool hasInitialized;

        public static void Initialize()
        {
            soIntrinsics = new ValMap();

            #region ScriptableObject intrinsics
            soIntrinsics = new ValMap();

            var a = Intrinsic.Create("");
            #region CreateSO
            a.AddParam("typename", string.Empty);
            a.AddParam("storagename", string.Empty);
            a.code = (context, partialResult) =>
            {
                if (MiniScriptSingleton.scriptableObjects.AvailableTypes.Contains(context.GetLocalString("typename")))
                {
                    var whatever = ScriptableObject.CreateInstance(context.GetLocalString("typename")) as MiniScriptScriptableObject;
                    if (whatever == null)
                    {
                        MiniScriptSingleton.LogError("Intrinsic 'CreateSO' was given a Type name that was not instanceable.");
                    }
                    else
                    {
                        whatever.Label = context.GetLocalString("storagename");
                        if (whatever.Label.Length == 0)
                        {
                            MiniScriptSingleton.LogError("Intrinsic 'CreateSO' was given a zero length label value for the SO created. This is not permitted, SO destroyed.");
                        }
                        else
                        {
                            MiniScriptSingleton.scriptableObjects.Add(whatever);
                        }
                        return new Intrinsic.Result(null);
                    }

                    return new Intrinsic.Result(null);
                }
                else
                {
                    return new Intrinsic.Result(null);
                }
            };

            IntrinsicsHelpMetadata.Create("CreateSO",
               "Creates an instance of the specified Type/class of ScriptableObject, optionally storing it with the 'storagename' string associated as its identifier.",
               "Data",
               new List<IntrinsicParameter>() {
                   new IntrinsicParameter() { variableType = typeof(string), Name = "typename", Comment = "The Type/class name of the ScriptableObject to create for the instance." },
                   new IntrinsicParameter() { variableType = typeof(string), Name = "storagename", Comment = "The name of the ScriptableObject to associate/label the instance with for reference." } },
               new IntrinsicParameter() { variableType = typeof(void), Comment = "Returns null." });

            soIntrinsics.map.Add(TempValString.Get("CreateSO"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region GetSO
            a.AddParam("storagename", string.Empty);
            a.code = (context, partialResult) =>
            {
                //return the ValList or ValMap exposed/represented by the ScriptableObject, null if its not found
                var rst = MiniScriptSingleton.scriptableObjects.Get(context.GetLocalString("storagename"));
                if (rst is IsListAttribute)
                {
                    var z = (IsListAttribute)rst;
                    return new Intrinsic.Result(z.GetValList());
                }
                else if (rst is IsDictionaryAttribute)
                {
                    var zz = (IsDictionaryAttribute)rst;
                    return new Intrinsic.Result(zz.GetValMap());
                }

                return new Intrinsic.Result(null);
            };

            IntrinsicsHelpMetadata.Create("GetSO",
               "Finds/returns the associated ScriptableObject based on the 'storagename' argument, if one can be found in the container.",
               "Data",
               new List<IntrinsicParameter>() {
                   new IntrinsicParameter() { variableType = typeof(string), Name = "storagename", Comment = "The name of the ScriptableObject to associate/label the instance with for reference." } },
               new IntrinsicParameter() { variableType = typeof(void), Comment = "Returns ValList or ValMap if its a valid SO, null otherwise." });

            soIntrinsics.map.Add(TempValString.Get("GetSO"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region RemoveSO
            a.AddParam("storagename", string.Empty);
            a.code = (context, partialResult) =>
            {   //remove the specified ScriptableObject from storage   
                MiniScriptSingleton.scriptableObjects.Remove(context.GetLocalString("storagename"));
                return new Intrinsic.Result(null);
            };

            IntrinsicsHelpMetadata.Create("RemoveSO",
              "Removes the specified ScriptableObject from storage.",
              "Data",
              new List<IntrinsicParameter>() { new IntrinsicParameter() { variableType = typeof(string), Name = "storagename", Comment = "The name of the ScriptableObject to remove." } },
              new IntrinsicParameter() { variableType = typeof(void), Comment = "Returns null." });

            soIntrinsics.map.Add(TempValString.Get("RemoveSO"), a.GetFunc());
            #endregion

            #endregion //end ScriptableObject intrinsics

            hasInitialized = true;
        }

        public static ValMap Get()
        {
            if(!hasInitialized) { Initialize(); }
            return soIntrinsics;
        }

    }
}

