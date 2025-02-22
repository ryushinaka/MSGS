using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MiniScript.MSGS.MUUI.TwoDimensional;

namespace MiniScript.MSGS.MUUI
{
    public interface UI_Element
    {
        string Name { get; set; }
        
        UIElementType ObjectType { get; }

        int ParentID { get; }

        GameObject GetGameObject();

        void HandleEvent();
    }

    public enum UIElementType
    {
        None,
        RectTransform,
        Button,
        ButtonAnimated,
        Image,
        ImageAnimated,
        Canvas,
        DropDown,
        InputField,
        Panel,
        RawImage,
        Scrollview,
        Slider,
        Text,
        Toggle,
    }    
}

