using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniScript.MSGS.MUUI
{
    /// <summary>
    /// Base class for ScriptEventContainerType1,Type2, and Type3
    /// </summary>
    public class ScriptEventContainer : MonoBehaviour
    {
        public virtual void ScriptOnEnter(InputEvent ip, PointerEventData ped) { }

        public virtual void ScriptOnExit(InputEvent ip, PointerEventData ped) { }

        public virtual void ScriptOnLeftClick(InputEvent ip, PointerEventData ped) { }

        public virtual void ScriptOnDoubleLeftClick(InputEvent ip, PointerEventData ped) { }

        public virtual void ScriptOnScrollUp(InputEvent ip, PointerEventData ped) { }

        public virtual void ScriptOnScrollDown(InputEvent ip, PointerEventData ped) { }

        public virtual void ScriptOnRightClick(InputEvent ip, PointerEventData ped) { }

        public virtual void ScriptOnDoubleRightClick(InputEvent ip, PointerEventData ped) { }

        public virtual void ScriptOnMiddleClick(InputEvent ip, PointerEventData ped) { }

    }
}