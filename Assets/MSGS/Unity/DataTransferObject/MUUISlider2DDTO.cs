using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace MiniScript.MSGS.Unity.DataTransferObjects
{
    public class MUUISlider2DDTO
    {
        public string label;
        public MUUIRectTransformDTO rect, rectViewport;
        public MUUIImage2DDTO imageBackground, imageBarHorizontal, imageBarVertical;

        public MiniScriptScriptAssetDTO ScriptOnEnterMSA, ScriptOnExitMSA, ScriptOnLeftClickMSA, ScriptOnScrollUpMSA, ScriptOnScrollDownMSA,
          ScriptOnDoubleLeftClickMSA, ScriptOnRightClickMSA, ScriptOnDoubleRightClickMSA, ScriptOnMiddleClickMSA;

        public string ScriptOnEnter, ScriptOnExit, ScriptOnLeftClick, ScriptOnScrollUp, ScriptOnScrollDown,
           ScriptOnDoubleLeftClick, ScriptOnRightClick, ScriptOnDoubleRightClick, ScriptOnMiddleClick;

        public ControlCollectionEventFilterDTO eventFilter;

        public bool enabled;
        public int childOf, instanceID;

        [NonSerialized]
        public static DataTable schema;
        [NonSerialized]
        public static bool schemainitd = false;

    }
}