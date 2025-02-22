using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using MiniScript.MSGS;
using MiniScript.MSGS.ScriptableObjects;

namespace MiniScript.MSGS.Editor
{

    public class EditorFindAllPrefabs : OdinMenuEditorWindow
    {
        [MenuItem("MiniScript/Prefab Manager")]
        private static void OpenWindow()
        {   
            GetWindow<EditorFindAllPrefabs>().Show();
        }

        protected override void OnGUI()
        {
            base.OnGUI();
        }
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            string[] prefabGUIDs = AssetDatabase.FindAssets("t:Prefab");

            foreach(string pid in prefabGUIDs)
            {                
                tree.AddAssetAtPath(System.IO.Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath(pid)),
                    AssetDatabase.GUIDToAssetPath(pid));
            }

            return tree;
        }
    }
}
