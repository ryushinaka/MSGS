using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using MiniScript.MSGS.PackageManager;
using System.Threading;
using System;

namespace MiniScript.MSGS.Zip
{
    /// <summary>
    /// Holds the ZipFile object and provides convenience methods to handle it
    /// </summary>
    public static class ArchiveHandler
    {
        static ZipFile zfile = null;

        public static IEnumerator GetURL(string url)
        {
            UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            if (www.result == UnityEngine.Networking.UnityWebRequest.Result.ConnectionError) { }
            else if (www.result == UnityEngine.Networking.UnityWebRequest.Result.ProtocolError) { }
            else if (www.result == UnityEngine.Networking.UnityWebRequest.Result.DataProcessingError) { }
            else
            {   //the request should be completed and ready to read out the data received
                //but first we check to see if there is any other kind of error message waiting for us
                if (www.downloadHandler.error.Length > 0)
                {
                    MiniScript.MSGS.MiniScriptSingleton.LogError(
                        "Error while downloading: " + www.downloadHandler.error
                        );
                    zfile = null;
                }

                //now we know we're safe and ready to proceed with the file data, under the assumption
                //its a valid zipfile
                System.IO.MemoryStream stream = new MemoryStream(www.downloadHandler.data);
                stream.Position = 0;
                zfile = new ZipFile(stream, false);
            }

            yield return null;
        }

        public static void GetFile(string file)
        {
            System.IO.FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
            stream.Position = 0;
            zfile = new ZipFile(stream);

            //yield return null;
        }

        public static bool isValidArchive()
        {
            if (zfile == null) { MiniScriptSingleton.LogError("Error while validating archive."); }
            else
            {
                var zz = zfile.GetEntry("manifest.xml");
                if (zz != null)
                {
                    var z = zfile.GetInputStream(zz);
                    if (Manifest.isValidManifest(ref z))
                    {
                        return true;
                    }

                    return false;
                }
                else
                {
#if UNITY_EDITOR
                    Debug.Log("manifest not found: " + zfile.Name);
#endif
                }
            }

            return false;
        }

        public static ZipFile Get()
        {
            return zfile;
        }

        public static void Close()
        {
            if (zfile != null) zfile.Close();
            zfile = null;
        }

        public static void ValidateArchives(object pa)
        {
            //List<string> result = new List<string>();
            //foreach(string p in parent.paths)
            //{
            //    GetFile(p);
            //    if(Get() != null)
            //    {
            //        if(isValidArchive())
            //        {
            //            var z = zfile.GetInputStream(zfile.GetEntry("manifest.xml"));
            //            if(Manifest.isValidManifest(ref z))
            //            {
            //                result.Add(p);
            //            }
            //            else
            //            {
            //                Debug.Log("manifest bad: " + p);
            //            }
            //        }
            //        else
            //        {
            //            Debug.Log("invalid: " + p);
            //        }
            //    }

            //    Close();
            //}

            //parent.PopulateList(ref result);            
        }

        public static void CreateNew()
        {
            FastZip fz = new FastZip();

        }
    }
}

