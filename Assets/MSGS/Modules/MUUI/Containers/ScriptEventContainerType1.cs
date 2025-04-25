using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniScript.MSGS.MUUI
{
    /// <summary>
    /// Convenience container for MiniScript Scripts executed by event callbacks
    /// </summary>
    public class ScriptEventContainerType1 : ScriptEventContainer
    {
        public Dictionary<string, string> Scripts;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void ScriptOnEnter(InputEvent ip, PointerEventData ped) { }

        public override void ScriptOnExit(InputEvent ip, PointerEventData ped) { }

        public override void ScriptOnLeftClick(InputEvent ip, PointerEventData ped) { }

        public override void ScriptOnDoubleLeftClick(InputEvent ip, PointerEventData ped) { }

        public override void ScriptOnScrollUp(InputEvent ip, PointerEventData ped) { }

        public override void ScriptOnScrollDown(InputEvent ip, PointerEventData ped) { }

        public override void ScriptOnRightClick(InputEvent ip, PointerEventData ped) { }

        public override void ScriptOnDoubleRightClick(InputEvent ip, PointerEventData ped) { }

        public override void ScriptOnMiddleClick(InputEvent ip, PointerEventData ped) { }
    }
}