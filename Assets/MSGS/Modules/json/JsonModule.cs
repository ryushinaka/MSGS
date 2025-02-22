using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace MiniScript.MSGS.Json
{
    public static class JsonModule
    {
        public static bool testMode = false;

        static bool hasInitialized = false;
        public static bool HasInitialized
        {
            get { return hasInitialized; }
        }

        static ValMap jsonIntrinsics;

        public static void Initialize()
        {
            jsonIntrinsics = new ValMap();

            var a = Intrinsic.Create("");
            #region Load json file
            a.AddParam("file", string.Empty);
            a.code = (context, partialResult) =>
            {





                return new Intrinsic.Result(null);
            };

            jsonIntrinsics.map.Add(TempValString.Get("Load"), a.GetFunc());
            #endregion

            #region Save Type to json file
            a.AddParam("file", string.Empty);
            a.AddParam("type", string.Empty);
            a.code = (context, partialResult) =>
            {

                return new Intrinsic.Result(null);
            };

            jsonIntrinsics.map.Add(TempValString.Get("Save"), a.GetFunc());
            #endregion
        }

        public static ValMap Get()
        {
            if (!hasInitialized) { Initialize(); }
            return jsonIntrinsics;
        }
    }
}

