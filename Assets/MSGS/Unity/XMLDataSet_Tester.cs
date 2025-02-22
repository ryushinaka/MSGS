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
using System.Xml;

namespace MiniScript.MSGS.Unity
{
    public class XMLDataSet_Tester : MonoBehaviour
    {
        [Sirenix.OdinInspector.FilePath(AbsolutePath = true, Extensions = "", ParentFolder = "Assets/", RequireExistingPath = true, UseBackslashes = true)]
        public string FilePath;

        [Button]
        public void Test_File_Integrity()
        {
            DataSet set = new DataSet();
            set.ReadXml(FilePath, XmlReadMode.ReadSchema);

            Debug.Log("Test Complete");
        }
    }
}

