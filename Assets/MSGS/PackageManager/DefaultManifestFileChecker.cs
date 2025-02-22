using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace MiniScript.MSGS.PackageManager
{

    public class DefaultManifestFileChecker : IManifestFileChecker
    {
        static List<string> validExtensions = new List<string>()
        {
            //scripts & data formats
            ".txt", ".xml", 
            //3D models
            ".fbx", 
            //sounds
            ".wav", ".ogg", ".mp3", ".flac", ".aiff", ".aif", ".mod", ".it", ".s3m", ".xm",
            //image formats
            ".png", ".jpg", ".bmp", ".tga", ".tif", ".psd"
        };

        public bool ApprovedFile(string filepath)
        {
            filepath = filepath.Replace("\\", "/");
            if (File.Exists(filepath))
            {
                if (validExtensions.Contains(Path.GetExtension(filepath)))
                {
                    //for now, we're satisfied with just checking the extension
                    //Debug.Log("valid: " + filepath);
                    return true;
                }
                else
                {
                    //Debug.Log("invalid extension: " + filepath);
                    return false;
                }
            }
            {
                Debug.Log("file not found: " + filepath);
                return false;
            }
        }

        public FileTypes GetFileType(string filepath)
        {
            //check the file for its Type (image, sound, data, etc)
            //for now, we're just going to assume based on the file extension
            if (filepath.EndsWith(".txt"))
            {
                return FileTypes.Script;
            }
            else if (filepath.EndsWith(".xml"))
            {
                //which kind of data file?
                //this could be a spritesheet, ui prefab, or data type / data record definition
                //so we return .Data which will tell the calling code to verify its Data File type
                return FileTypes.Data;
            }
            #region 3D Model file types
            //for simplicity, we're only allowing fbx right now
            else if (filepath.EndsWith(".fbx")) { return FileTypes.Model3D; }
            #endregion
            #region Sound file types
            else if (filepath.EndsWith(".mp3") || filepath.EndsWith(".ogg") || filepath.EndsWith(".wav") ||
                filepath.EndsWith(".flac") || filepath.EndsWith(".aiff") || filepath.EndsWith(".aif") ||
                filepath.EndsWith(".mod") || filepath.EndsWith(".it") || filepath.EndsWith(".s3m") ||
                filepath.EndsWith(".xm")) { return FileTypes.Sound; }
            #endregion
            #region Image file types
            else if (filepath.EndsWith(".png") || filepath.EndsWith(".jpg") || filepath.EndsWith(".bmp") ||
                filepath.EndsWith(".tga") || filepath.EndsWith(".tif") || filepath.EndsWith(".psd"))
                { return FileTypes.Image; }
            #endregion

            return FileTypes.Unknown;
        }

        public DataFileTypes GetDataFileType(string filepath)
        {
            //load the file as dataset

            return  DataFileTypes.Unknown;
        }
    }
}
