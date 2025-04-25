using MiniScript.MSGS.Unity.DataTransferObjects;
using MiniScript.MSGS.ScriptableObjects;
using MiniScript.MSGS.MUUI;
using System;
using TMPro;
using UnityEngine;

namespace MiniScript.MSGS.Unity.ScriptableObjects
{
    /// <summary>
    /// Contains all the properties of a Canvas driven Button
    /// </summary>
    [Serializable]
    public class MUUIButton2DSO : ScriptableObject
    {
        public GameObject goButton;
        public RectTransform rect;
        public UnityEngine.UI.Image image;
        public TextMeshProUGUI Text;
        public MiniScriptScriptAsset ScriptOnEnterMSA, ScriptOnExitMSA, ScriptOnLeftClickMSA, ScriptOnScrollUpMSA, ScriptOnScrollDownMSA,
            ScriptOnDoubleLeftClickMSA, ScriptOnRightClickMSA, ScriptOnDoubleRightClickMSA, ScriptOnMiddleClickMSA;        
        public string ScriptOnEnter, ScriptOnExit, ScriptOnLeftClick, ScriptOnScrollUp, ScriptOnScrollDown,
            ScriptOnDoubleLeftClick, ScriptOnRightClick, ScriptOnDoubleRightClick, ScriptOnMiddleClick;

        public ControlCollectionEventFilter eventFilter;

        public int childOf, instanceID;

        public void Init(ref MUUIButton2DDTO button)
        {
            
        }

        /// <summary>
        /// Instances the gameObject reference and all of its components and their properties
        /// </summary>
        /// <returns></returns>
        public GameObject Spawn()
        {
            return goButton;
        }
    }
}

