using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniScript.MSGS.MUUI
{
    public class DefaultEventSink : IMiniScriptEventSink
    {
        public void HandleEvent(UI_Element element, ref InputEvent mevent)
        {
            string str = string.Empty;
            if (mevent.ClickLeft) { str += "[LeftClick]"; }
            if (mevent.ClickLeftDouble) { str += "[DoubleLeftClick]"; }
            if (mevent.ClickMiddle) { str += "[MiddleClick]"; }
            if (mevent.ClickRight) { str += "[RightClick]"; }
            if (mevent.ClickRightDouble) { str += "[DoubleRightClick]"; }
            if (mevent.OnEnter) { str += "[OnEnter]"; }
            if (mevent.OnExit) { str += "[OnExit]"; }
            if (mevent.OnScrollUp) { str += "[OnScrollUp]"; }
            if (mevent.OnScrollDown) { str += "[OnScrollDown]"; }

            Debug.Log(element.Name + "/" + element.ObjectType.ToString() + "/" + str);
        }
    }

}
