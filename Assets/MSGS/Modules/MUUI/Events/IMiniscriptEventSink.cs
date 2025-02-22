using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniScript;
using MiniScript.MSGS;

namespace MiniScript.MSGS.MUUI
{
    public interface IMiniScriptEventSink
    {
        public void HandleEvent(UI_Element element, ref InputEvent mevent);        
    }
}
