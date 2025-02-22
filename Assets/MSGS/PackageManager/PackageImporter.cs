using System.Collections;
using System;
using System.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using MiniScript.MSGS.Scripts;
using MiniScript.MSGS.Data;
using MiniScript.MSGS.Audio;
using MiniScript.MSGS.MUUI;
using System.Xml;

namespace MiniScript.MSGS.PackageManager
{
    public class PackageImporter
    {
#if ODIN_INSPECTOR
        [FolderPath(AbsolutePath = true, ParentFolder = "Assets/", RequireExistingPath = true)]
#endif
        public string PkgFolder;

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.FilePath(AbsolutePath = true, Extensions = "", ParentFolder = "Assets/", RequireExistingPath = true, UseBackslashes = true)]
#endif
        public string FilePath;

        [Tooltip("Enable if you want to see debug console output in the Unity Editor")]
        public bool DebugOutput;

        [Tooltip("The password to apply to the package file if you want to use one")]
        public string Password;

        public int EntriesTotal = 0, EntriesCompleted = 0;

        public delegate void CompletionCallback(bool value, string msg);
        public static CompletionCallback OnCompletion;

        public void ImportPackage()
        {
            Manifest mani = new Manifest();

            //validate the file path given exists
            if (File.Exists(FilePath))
            {
                using (var fsInput = File.OpenRead(FilePath))
                {
                    var buffer = new byte[4096];
                    using (var zf = new ZipFile(fsInput))
                    {
                        if (!String.IsNullOrEmpty(Password))
                        {
                            // AES encrypted entries are handled automatically
                            zf.Password = Password;
                        }

                        bool foundManifest = false;
                        foreach (ZipEntry zipEntry in zf)
                        {
                            //we're just looking for the manifest entry on this first loop
                            if (zipEntry.Name == "manifest.xml")
                            {
                                foundManifest = true;
                                MemoryStream mstream = new MemoryStream((int)zipEntry.Size);
                                var iss = zf.GetInputStream(zipEntry);
                                StreamUtils.Copy(iss, mstream, buffer); iss.Close();
                                mstream.Position = 0;
                                mani = new Manifest(mstream);
                            }
                        }

                        if (!foundManifest)
                        {
                            //this is not permitted, debug.log the error and abort
                            MiniScriptSingleton.LogError("Package chosen (" + PkgFolder + ") does not contain a manifest. Unable to import package.");
                            return;
                        }
                        else
                        {
                            //make sure the container for all the contents is initialized
                            //MiniScriptSingleton.currentMod = new GameModification();

                            //manifest has been found, so lets process the entries of it
                            if (mani.Count > 0)
                            {
                                IDataFileChecker dchecker = new DefaultDataFileChecker();
                                IManifestFileChecker fchecker = new DefaultManifestFileChecker();
                                ScriptFileChecker schecker = new ScriptFileChecker();

                                EntriesTotal = mani.Count;

                                var it = mani.Entries;
                                while (it.MoveNext())
                                {
                                    var sc = zf.GetInputStream(zf.GetEntry(it.Current.filename));
                                    MemoryStream ms = new MemoryStream();

                                    switch (it.Current.ftype)
                                    {
                                        case FileTypes.Data:
                                            #region
                                            DataStruct ds = new DataStruct();
                                            ds.stream = sc;
                                            ds.name = it.Current.filename;
                                            ds.dchecker = dchecker;
                                            ReadDataEntry(ds);
                                            break;
                                            #endregion
                                        case FileTypes.Image:
                                            ReadImageEntry(ref sc, ref it.Current.filename);
                                            break;
                                        case FileTypes.Model3D:
                                            #region

                                            #endregion
                                        case FileTypes.Script:
                                            ReadScriptEntry(ref sc, ref schecker, ref it.Current.filename);
                                            break;
                                        case FileTypes.Sound:
                                            #region
                                            //this is if it is a raw .wav file outside of a soundsheet record
                                            AudioSystemSingleton.Instance.AddTrack(
                                                it.Current.filename,
                                                WaveUtility.ToAudioClip(sc)
                                                );
                                            break;
                                            #endregion
                                        default:
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                Debug.Log("Package chosen (" + PkgFolder + ") has a manifest with zero entries. Nothing to do.");
                                return;
                            }
                        }
                    }
                }

                if (DebugOutput)
                {
                    Debug.Log(
                        "Images: " + SpriteSheetCollection.Debug() +
                        "  Scripts: " + MiniScriptSingleton.Scripts.Count +
                        "  Sounds: " + SoundSheetCollection.Debug()
                        );
                }
            }
        }

        static void ReadDataEntry(object dst)
        {
            var ds = (DataStruct)dst;
            //Debug.Log("?: " + ds.name);

            MemoryStream ms = new MemoryStream();
            StreamUtils.Copy(ds.stream, ms, new byte[4096]);
            ms.Position = 0;
            StreamReader drdr = new StreamReader(ms);
            var x = ds.dchecker.GetDataFileType(ref drdr);
            var xds = new DataSet();

            ms.Position = 0; //if you dont reset the position of the memorystream, then the .ReadXml call will fail, as its already at end of stream in position
            xds.ReadXml(ms, XmlReadMode.ReadSchema);

            switch (x)
            {
                case DataFileTypes.DataType:
                case DataFileTypes.DataTypeWithRecords:
                    DataStoreWarehouse.DataStoreCreate(ref xds);
                    OnCompletion.Invoke(true, "Data: " + xds.DataSetName);
                    break;
                case DataFileTypes.Script:
                    //do nothing, handled seperately
                    break;
                case DataFileTypes.UIprefab:
                    MiniScriptSingleton.PrefabContainer.LoadPrefab(ref xds);
                    OnCompletion.Invoke(true, "UIPrefab: " + xds.DataSetName);
                    break;
                case DataFileTypes.SpriteSheet:
                    SpriteSheet sps = new SpriteSheet();
                    sps = sps.LoadSpriteSheet(ref xds);

                    if (sps.Name.Length != 0)
                    {
                        SpriteSheetCollection.Add(sps.Name, sps);
                        OnCompletion.Invoke(true, "SpriteSheet: " + sps.Name);
                    }
                    else
                    {
                        OnCompletion.Invoke(false, "SpriteSheet: " + sps.Name);
                    }
                    break;
                case DataFileTypes.SoundSheet:
                    Album ss = new Album();
                    ss.LoadSoundSheet(ref xds);
                    SoundSheetCollection.Add(new KeyValuePair<string, Album>(ss.Name, ss));
                    OnCompletion.Invoke(true, "SoundSheet: " + ss.Name);
                    break;
                default:
                    OnCompletion.Invoke(false, "PackageImporter.ReadDataEntry: unhandled DataFileType (" + ds.name + ")");
                    break;
            }
        }

        static void ReadImageEntry(ref Stream stream, ref string name)
        {
            byte[] buffer = new byte[4096];
            MemoryStream ms = new MemoryStream();
            StreamUtils.Copy(stream, ms, buffer);
            var Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(ms.ToArray()))        // Load the imagedata into the texture (size is set automatically)
            {
                if (Tex2D != null)
                {
                    Sprite NewSprite = Sprite.Create(Tex2D, new Rect(0, 0, Tex2D.width, Tex2D.height),
                        new Vector2(0, 0), 100f, 0, SpriteMeshType.FullRect);
                    try
                    {
                        SpriteSheetCollection.Get("sprites").AddSprite(name, NewSprite);
                        OnCompletion.Invoke(true, "SpriteSheet: " + name);
                    }
                    catch (System.Exception ex)
                    {
                        MiniScriptSingleton.LogError("PackageImporter.ReadImageEntry: " + ex.Message);
                        OnCompletion.Invoke(false, "PackageImporter.ReadImageEntry: " + ex.Message);
                    }
                }
            }
        }

        static void ReadScriptEntry(ref Stream stream, ref ScriptFileChecker schecker, ref string entryname)
        {
            StreamReader srdr = new StreamReader(stream);
            ScriptExecutionContext2 script = new ScriptExecutionContext2();
            script.ScriptSource = srdr.ReadToEnd();
            //validate the script with the Parser
            schecker.ValidateScript(script.ScriptSource);

            if (schecker.HasErrOutput)
            {
                MiniScriptSingleton.LogError("Script '" + script.Label + "' has errors during compile. Error:: " + schecker.exception.Message);
                OnCompletion.Invoke(false, string.Empty);
                return;
            }
            if (schecker.Incomplete)
            {
                MiniScriptSingleton.LogError("Script '" + script.Label + "' is incomplete and needs additional logic to be compiled.");
                OnCompletion.Invoke(false, string.Empty);
                return;
            }

            //if the script is complete and has no errors, add it to the current storage device
            MiniScriptSingleton.Scripts.Add(entryname, script.ScriptSource);
            OnCompletion.Invoke(true, "Script: " + script.Label);
        }

        private static void CompressFolder(string path, ZipOutputStream zipStream, int folderOffset)
        {
            var files = Directory.GetFiles(path);
            //Debug.Log(path);

            foreach (var filename in files)
            {
                if (filename.EndsWith(".txt") || filename.EndsWith(".xml") || filename.EndsWith(".fbx")
                    || filename.EndsWith(".wav") || filename.EndsWith(".png") || filename.EndsWith(".jpg") || filename.EndsWith(".bmp"))
                {
                    var fi = new FileInfo(filename);

                    // Make the name in zip based on the folder
                    var entryName = filename.Substring(folderOffset);

                    // Remove drive from name and fix slash direction
                    entryName = ZipEntry.CleanName(entryName);

                    var newEntry = new ZipEntry(entryName);

                    // Note the zip format stores 2 second granularity
                    newEntry.DateTime = fi.LastWriteTime;

                    // Specifying the AESKeySize triggers AES encryption. 
                    // Allowable values are 0 (off), 128 or 256.
                    // A password on the ZipOutputStream is required if using AES.
                    //   newEntry.AESKeySize = 256;

                    // To permit the zip to be unpacked by built-in extractor in WinXP and Server2003,
                    // WinZip 8, Java, and other older code, you need to do one of the following: 
                    // Specify UseZip64.Off, or set the Size.
                    // If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, 
                    // you do not need either, but the zip will be in Zip64 format which
                    // not all utilities can understand.
                    //   zipStream.UseZip64 = UseZip64.Off;
                    newEntry.Size = fi.Length;

                    zipStream.PutNextEntry(newEntry);

                    // Zip the file in buffered chunks
                    // the "using" will close the stream even if an exception occurs
                    var buffer = new byte[4096];
                    using (FileStream fsInput = File.OpenRead(filename))
                    {
                        StreamUtils.Copy(fsInput, zipStream, buffer);
                    }
                    zipStream.CloseEntry();
                }
            }

            // Recursively call CompressFolder on all folders in path
            var folders = Directory.GetDirectories(path);
            foreach (var folder in folders)
            {
                CompressFolder(folder, zipStream, folderOffset);
            }
        }

        public static Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100.0f, SpriteMeshType spriteType = SpriteMeshType.FullRect)
        {
            // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference

            Texture2D SpriteTexture = LoadTexture(FilePath);
            if (SpriteTexture == null) { SpriteTexture = new Texture2D(400, 200); }
            Sprite NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height),
                new Vector2(0, 0), PixelsPerUnit, 0, spriteType);

            return NewSprite;
        }

        public static Texture2D LoadTexture(string FilePath)
        {

            // Load a PNG or JPG file from disk to a Texture2D
            // Returns null if load fails

            Texture2D Tex2D;
            byte[] FileData;

            if (File.Exists(FilePath))
            {
                FileData = File.ReadAllBytes(FilePath);
                Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
                if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                    return Tex2D;                 // If data = readable -> return texture
            }
            else
            {
#if UNITY_EDITOR
                //Debug.Log("not found: " + FilePath);
#endif
            }
            return null;                     // Return null if load failed
        }

        public static void SaveSprite(string filepath, Sprite sp)
        {
            //Debug.Log("SaveSpritePath= " + filepath);
            //Debug.Log(filepath.Length);

            if (sp == null) return;

            byte[] bytes = ConvertSpriteToTexture(sp).EncodeToPNG();

            if (bytes.Length > 0)
            {
                var z = File.OpenWrite(filepath);
                z.Write(bytes, 0, bytes.Length);
                z.Flush(); z.Close();
            }
        }

        static Texture2D ConvertSpriteToTexture(Sprite sprite)
        {
            try
            {
                if (sprite.rect.width != sprite.texture.width)
                {
                    Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.RGB24, false);
                    Color[] colors = newText.GetPixels();
                    Color[] newColors = sprite.texture.GetPixels((int)Mathf.CeilToInt(sprite.textureRect.x),
                                                                 (int)Mathf.CeilToInt(sprite.textureRect.y),
                                                                 (int)Mathf.CeilToInt(sprite.textureRect.width),
                                                                 (int)Mathf.CeilToInt(sprite.textureRect.height));
                    //Debug.Log(colors.Length + "_" + newColors.Length);
                    newText.SetPixels(newColors);
                    newText.Apply();
                    return newText;
                }
                else
                    return sprite.texture;
            }
            catch
            {
                return null;
            }
        }

        public PackageImporter()
        {
            OnCompletion += new CompletionCallback(Completed);
        }

        void Completed(bool v, string s)
        {
            if(v) { EntriesCompleted++; }
#if UNITY_EDITOR
            //if (DebugOutput) { Debug.Log(v.ToString() + " " + s); }
#endif
        }

        internal struct DataStruct
        {
            public Stream stream;
            public string name;
            public IDataFileChecker dchecker;
        }
        internal struct ImageStruct
        {
            public Stream stream;
            public string name;
        }
    }
}
