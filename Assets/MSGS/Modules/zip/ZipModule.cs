using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ICSharpCode.SharpZipLib.Zip;

namespace MiniScript.MSGS.Zip
{
    public static class ZipModule
    {
        internal static bool debug;
        static bool hasInitialized;
        static ValMap zipIntrinsics;
        static Dictionary<string, ZipFile> files;

        public static ValMap Get()
        {
            if (!hasInitialized) { Initialize(); }
            return zipIntrinsics;
        }

        public static void Initialize()
        {
            zipIntrinsics = new ValMap();
            files = new Dictionary<string, ZipFile>();

            var a = Intrinsic.Create("");
            #region Load
            a.AddParam("name", string.Empty);
            a.code = (context, PartialResult) => 
            {
                string path = Application.streamingAssetsPath + '/' + context.GetLocalString("name") + ".zip";
                if (System.IO.File.Exists(path))
                {
                    ArchiveHandler.GetFile(path);
                }
                else
                {
                    MiniScriptSingleton.LogWarning("ArchiveLoad: unable to find the specified archive file @ [ " + path + " ]");
                }

                return new Intrinsic.Result(null);
            };


            zipIntrinsics.map.Add(new ValString("Load"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("Load",
                "Loads an archive from the local filesystem, specified by a string literal as the filename without extension.",
                "Archive",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter() { Name = "name", variableType = typeof(string),
                    Comment = "Specifies the filename to be loaded, without extension (.zip is assumed)"
                    }
                },
                new IntrinsicParameter() { Name = "null", variableType = typeof(void) });
            #endregion

            a = Intrinsic.Create("");
            #region Unload            
            a.code = (context, PartialResult) =>
            {   
                ArchiveHandler.Close();

                return new Intrinsic.Result(null);
            };

            zipIntrinsics.map.Add(new ValString("Unload"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("Unload",
               "Unloads the archive from memory, allowing another to be loaded in its place if desired.",
               "Archive",
               new List<IntrinsicParameter>() { },
               new IntrinsicParameter() { Name = "null", variableType = typeof(void) });
            #endregion

            a = Intrinsic.Create("");
            #region GetScripts
            a.code = (context, PartialResult) =>
            {
              

                return new Intrinsic.Result(null);
            };

            zipIntrinsics.map.Add(new ValString("GetScripts"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("GetScripts",
               "Gets the ValList containing the names (string) of all scripts in the archive currently loaded.",
               "Archive",
               new List<IntrinsicParameter>() { },
               new IntrinsicParameter() { Name = "ValList", variableType = typeof(ValList), 
                   Comment = "Will contain ValString elements only." });

            #endregion

            hasInitialized = true;
        }
    }
}
