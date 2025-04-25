using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniScript;

namespace MiniScript.MSGS.Unity
{
    public static class UnityModule
    {
        internal static bool debug;
        static bool hasInitialized;
        static ValMap intrinsics;
        public static ValMap Get()
        {
            if (!hasInitialized) { Initialize(); }
            return intrinsics;
        }

        static void Initialize()
        {
            intrinsics = new ValMap();
            var a = Intrinsic.Create("");
            #region CreatePrefab
            a.AddParam("prefabname", new ValString(string.Empty));
            a.code = (context, partialResult) =>
            {


                return new Intrinsic.Result(null);
            };

            intrinsics.map.Add(new ValString("CreatePrefab"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            a.AddParam("", new ValString(string.Empty));
            a.code = (context, partialResult) =>
            {

                return new Intrinsic.Result(null);
            };

            //Add the UnityEngine.Application wrapper to the ValMap
            intrinsics.map.Add(new ValString("App"), UnityEngineApplicationWrapper.Get());
            //add the UnityEngine.PlayerPrefs wrapper to the ValMap
            intrinsics.map.Add(new ValString("PlayerPrefs"), UnityEnginePlayerPrefsWrapper.Get());
            //add the UnityEngine.Random wrapper
            intrinsics.map.Add(new ValString("Random"), UnityEngineRandomWrapper.Get());
            //add the UnityEngine.Time wrapper to the ValMap
            intrinsics.map.Add(new ValString("Time"), UnityEngineTimeWrapper.Get());
            //add the UnityEngine.Screen wrapper to the ValMap
            intrinsics.map.Add(new ValString("Screen"), UnityEngineScreenWrapper.Get());
        }
    }
}
