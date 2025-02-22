using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

namespace MiniScript.MSGS.XML
{
    public static class XmlModule
    {
        public static bool testMode = false;

        static bool hasInitialized = false;
        public static bool HasInitialized
        {
            get { return hasInitialized; }
        }

        static ValMap xmlIntrinsics;

        public static void Initialize()
        {
            xmlIntrinsics = new ValMap();

            var a = Intrinsic.Create("");
            #region Load xml file
            a.AddParam("file", string.Empty);
            a.code = (context, partialResult) =>
            {

                return new Intrinsic.Result(null);
            };

            xmlIntrinsics.map.Add(TempValString.Get("Load"), a.GetFunc());
            #endregion

            #region Save Type to xml file
            a.AddParam("file", string.Empty);
            a.AddParam("type", string.Empty);
            a.code = (context, partialResult) =>
            {

                return new Intrinsic.Result(null);
            };

            xmlIntrinsics.map.Add(TempValString.Get("Save"), a.GetFunc());
            #endregion
        }

        public static ValMap Get()
        {
            if(!hasInitialized) { Initialize(); }
            return xmlIntrinsics;
        }
    }
}

