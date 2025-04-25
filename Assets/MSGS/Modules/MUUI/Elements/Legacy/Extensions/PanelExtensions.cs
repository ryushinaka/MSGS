using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using TMPro;
using MiniScript;
using System;
using MiniScript.MSGS.MUUI.TwoDimensional;

namespace MiniScript.MSGS.MUUI.Extensions
{
    public static class PanelExtensions 
    {
        public static ValMap ToValMap(this MUUIPanel panel)
        {
            ValMap rst = new ValMap();
            rst.map.Add(new ValString("enabled"), ValNumber.Truth(panel.enabled));
            rst.map.Add(new ValString("name"), new ValString(panel.gameObject.name));
            rst.map.Add(new ValString("draggable"), ValNumber.Truth(panel.isDraggable));

            return rst;
        }

        public static bool UpdatePanel(this MUUIPanel panel, Value a, Value b)
        {
            if (a is ValString)
            {
                string s = ((ValString)a).value;
                switch (s)
                {
                    case "enabled":
                        #region
                        if (b is ValNumber)
                        {
                            panel.enabled = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Panel(" + panel.gameObject.name + ").enabled property with a non-Boolean(ValNumber) parameter." +
                                                            " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "name":
                        #region
                        if (b is ValString)
                        {
                            panel.gameObject.name = ((ValString)b).value;
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Panel(" + panel.gameObject.name + ").name property with a non-String(ValString) parameter." +
                                                            " The expected argument should be a ValString containing the string literal value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "draggable":
                        #region
                        if (b is ValNumber)
                        {
                            panel.isDraggable = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Panel(" + panel.gameObject.name + ").draggable property with a non-Boolean(ValNumber) parameter." +
                                                            " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    default:
                        if (b is null) { MiniScriptSingleton.LogError("Attempt to modify MUUI_Panel(" + panel.gameObject.name + ") with an unknown property label (" + s + ")."); }
                        else { MiniScriptSingleton.LogError("Attempt to modify MUUI_Panel(" + panel.gameObject.name + ") with an unknown property label (" + s + ") with an argument that appears to be of type " + b.GetType().ToString()); }
                        return false;
                }
            }
            else
            {
                if (a == null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify MUUI_Panel(" + panel.gameObject.name + ") with a property label accessor that is null.");
                }
                else if (a != null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify MUUI_Panel(" + panel.gameObject.name + ") with a property label accessor that is not a String. It appears to be a (" + a.GetType().Name + ").");
                }

                return false;
            }
        }

        public static void SetupPanel(this MUUIPanel panel, ref DataRow row)
        {
            panel.name = (string)row["name"];            
            panel.gameObject.SetActive((bool)row["enabled"]);
            panel.isDraggable = (bool)row["draggable"];
        }
    }
}
