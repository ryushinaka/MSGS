using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace MiniScript.MSGS.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewMiniScriptConfiguration", menuName = "MiniScript/New Configuration", order = 2)]
    public class MiniScriptConfiguration : ScriptableObject
    {
        [Tooltip("The Data Model declaration, if any.")]
        public MiniScriptScriptAsset DataModel;

        [Tooltip("The Script that is executed second, after the Data Model is imported.")]
        public MiniScriptScriptAsset StartupScript;

        [Sirenix.OdinInspector.FolderPath(AbsolutePath = true, ParentFolder = "Assets/", RequireExistingPath = true, UseBackslashes = true)]
        public string ProjectBaseFolder;
    }
}

