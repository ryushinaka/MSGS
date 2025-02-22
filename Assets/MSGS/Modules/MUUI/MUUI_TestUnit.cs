using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniScript;
using System.Xml;
using MiniScript.MSGS.MUUI.TwoDimensional;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace MiniScript.MSGS.MUUI
{
    public class MUUI_TestUnit : MonoBehaviour
    {
        public Text text;

        void Start()
        {
            

        }

        void Update()
        {

        }

        void StdOutput(string msg) { Debug.Log("Std: " + msg); }
        void ErrOutput(string msg) { Debug.Log("Err: " + msg); }

        #if ODIN_INSPECTOR
        [Button]
        #endif
        private void TestButton()
        {
            ValMap a = text.GetValMap;
            Debug.Log(a.Count);
            
            ValMap z = (ValMap)a["Text"];
            z.assignOverride(new ValString("Bold"), ValNumber.Truth(false));


            Debug.Log("done");
        }
    }
}
