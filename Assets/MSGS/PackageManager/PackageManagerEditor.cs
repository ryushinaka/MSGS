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
using System.Xml;
using MiniScript.MSGS.MUUI;

namespace MiniScript.MSGS.PackageManager
{
    public class PackageManagerEditor : MonoBehaviour
    {
        [FolderPath(AbsolutePath = true, ParentFolder = "Assets/", RequireExistingPath = true)]
        public string PkgFolder;

        [Sirenix.OdinInspector.FilePath(AbsolutePath = true, Extensions = "", ParentFolder = "Assets/", RequireExistingPath = true, UseBackslashes = true)]
        public string FilePath;

        [Tooltip("Enable if you want to see debug console output in the Unity Editor")]
        public bool DebugOutput;

        [Tooltip("The password to apply to the package file if you want to use one")]
        public string Password;

        PackageImporter packageImporter;

        [Button]
        public void ExportPackage()
        {
            string fname = string.Empty;
            //make sure there is a path to be handled
            //either that specified by the ModFolder property or a selected folder in Project hierarchy
            if (PkgFolder.Length == 0)
            {
                Debug.Log("Not path specified to Export Package.");
            }
            else
            {
                if (Directory.Exists(PkgFolder))
                {
                    fname = PkgFolder.Substring(PkgFolder.LastIndexOf("/"), PkgFolder.Length - PkgFolder.LastIndexOf("/"));
                    fname = fname.Remove(0, 1);
                    fname = fname + ".zip";
                }

                //now we have a folder/path to manipulate, lets generate the Manifest of its contents
                ManifestFactory factory = new ManifestFactory();
                Manifest mani = null;
                if (DebugOutput) { mani = factory.Path(PkgFolder, true); }
                else { mani = factory.Path(PkgFolder, false); }

                //Debug.Log(mani.Count);
                //now that we have a Manifest of the package contents, we'll iterate the contents
                //of the Manifest to select validated content for the generated zip file

                FileStream fsOut = File.OpenWrite(Application.streamingAssetsPath + "/" + fname);

                using (var zipStream = new ZipOutputStream(fsOut))
                {
                    //0-9, 9 being the highest level of compression
                    zipStream.SetLevel(9);

                    // optional. Null is the same as not setting. Required if using AES.
                    //zipStream.Password = password;
                    if (!String.IsNullOrEmpty(Password)) { zipStream.Password = Password; }
                    else { zipStream.Password = null; }

                    // This setting will strip the leading part of the folder path in the entries, 
                    // to make the entries relative to the starting folder.
                    // To include the full path for each entry up to the drive root, assign to 0.
                    int folderOffset = PkgFolder.Length + (PkgFolder.EndsWith("\\") ? 0 : 1);
                    //buffer for chunking files into the compressed zipstream
                    var buffer = new byte[4096];

                    //make sure the Manifest itself gets written to the zipfile before everything else                    
                    var newEntry = new ZipEntry("manifest.xml");
                    newEntry.DateTime = System.DateTime.Now;
                    MemoryStream memstream = new MemoryStream();
                    mani.Finalize().WriteXml(memstream, System.Data.XmlWriteMode.WriteSchema);
                    memstream.Flush(); memstream.Position = 0;
                    newEntry.Size = memstream.Length;
                    zipStream.PutNextEntry(newEntry);
                    StreamUtils.Copy(memstream, zipStream, buffer);
                    zipStream.CloseEntry();

                    var it = mani.Entries;
                    while (it.MoveNext())
                    {
                        //Debug.Log(it.Current.path);
                        var fi = new FileInfo(it.Current.path);
                        //var entryName = it.Current.path.Substring(folderOffset);
                        var entryName = it.Current.filename;

                        entryName = ZipEntry.CleanName(entryName);

                        newEntry = new ZipEntry(entryName);
                        newEntry.DateTime = fi.LastWriteTime;
                        newEntry.Size = fi.Length;
                        zipStream.PutNextEntry(newEntry);

                        //Debug.Log(newEntry.ToString());
                        using (FileStream fsInput = File.OpenRead(it.Current.path))
                        {
                            StreamUtils.Copy(fsInput, zipStream, buffer);
                        }
                        zipStream.CloseEntry();
                    }

                    //CompressFolder(path, zipStream, folderOffset);
                }
            }
        }

        [Button]
        public void ImportPackage()
        {
            packageImporter = new PackageImporter();
            
            packageImporter.FilePath = this.FilePath;
            packageImporter.Password = this.Password;
            packageImporter.ImportPackage();

            if (DebugOutput)
            {
                Debug.Log(
                    "Images: " + SpriteSheetCollection.Debug() +
                    "  Scripts: " + MiniScriptSingleton.Scripts.Count +
                    "  Sounds: " + SoundSheetCollection.Debug() + 
                    " Entries: " + packageImporter.EntriesTotal + "/" + packageImporter.EntriesCompleted
                    );
            }
        }
    }
}