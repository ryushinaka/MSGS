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
    public class MiniScriptBuildManager : OdinMenuEditorWindow
    {
        static Type[] types = TypeCache.GetTypesWithAttribute<ManageableAttribute>().OrderBy(m => m.Name).ToArray();
        Type selectedType;
        MiniScriptScriptableObjectType selectedEnumType;

        [MenuItem("MiniScript/Build Manager")]
        private static void OpenWindow()
        {
            Debug.Log("build");
            GetWindow<MiniScriptBuildManager>().Show();
        }

        protected override void OnGUI()
        {
            if (GuiUtils.SelectEnumList(ref selectedEnumType))
            {
                this.ForceMenuTreeRebuild();
            }

            base.OnGUI();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            foreach (var T in types)
            {
                var z = tree.AddAllAssetsAtPath(T.Name, "Assets", T, true, true);
                foreach (OdinMenuItem omi in z)
                {
                    if (omi.Value != null)
                    {
                        var mso = (MiniScriptScriptableObject)omi.Value;
                        //if (mso.ScriptableObjectType != selectedEnumType)
                        //{
                        //    omi.Remove();
                        //    tree.MarkDirty();
                        //}
                    }
                }
            }

            return tree;
        }
    }
}

