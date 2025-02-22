using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.MUUI
{
    public enum UIMessageType
    {
        Form_Instantiate,
        Form_Destroy,
        Form_Move,
        Form_Resize,
        Form_FocusGain,
        Form_FocusLost,
        Form_Enable,
        Form_Disable,
        Form_Shown,
        Form_Hidden,

        APP_FocusGain,
        APP_FocusLost,
        APP_Instantiate,
        APP_Destroy,


    }
}
