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
    public static class ToggleExtensions 
    {
        public static ValMap ToValMap(this MiniScript.MSGS.MUUI.TwoDimensional.MUUIToggle toggle)
        {
            ValMap rst = new ValMap();
            rst.map.Add(new ValString("enabled"), ValNumber.Truth(toggle.enabled));
            rst.map.Add(new ValString("name"), new ValString(toggle.name));
            rst.map.Add(new ValString("Text"), new ValString(toggle.Label.text));
            rst.map.Add(new ValString("OnToggleChanged"), new ValString(toggle.ScriptOnToggle));
            //rst.map.Add(new ValString("OnPointerEnterHandler"), new ValString(toggle.ScriptOnEnter));
            //rst.map.Add(new ValString("OnPointerExitHandler"), new ValString(toggle.ScriptOnExit));
            //rst.map.Add(new ValString("OnPointerLeftClickHandler"), new ValString(toggle.ScriptOnLeftClick));
            //rst.map.Add(new ValString("OnPointerRightClickHandler"), new ValString(toggle.ScriptOnRightClick));
            //rst.map.Add(new ValString("OnPointerDoubleLeftClickHandler"), new ValString(toggle.ScriptOnDoubleLeftClick));
            //rst.map.Add(new ValString("OnPointerDoubleRightClickHandler"), new ValString(toggle.ScriptOnDoubleRightClick));
            //rst.map.Add(new ValString("OnPointerMiddleClickHandler"), new ValString(toggle.ScriptOnMiddleClick));

            return rst;
        }

        public static bool UpdateToggle(this MiniScript.MSGS.MUUI.TwoDimensional.MUUIToggle toggle, Value a, Value b)
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
                            toggle.enabled = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Toggle(" + toggle.gameObject.name + ").enabled property with a non-Boolean(ValNumber) parameter." +
                                                            " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "name":
                        #region
                        if (b is ValString)
                        {
                            toggle.gameObject.name = ((ValString)b).value;
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Toggle(" + toggle.gameObject.name + ").name property with a non-String(ValString) parameter." +
                                                            " The expected argument should be a ValString containing the string literal value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "OnToggleChanged":
                        #region
                        if (b is ValString)
                        {
                            if (MiniScriptSingleton.ScriptExists(((ValString)b).value))
                            {
                                toggle.ScriptOnToggle = ((ValString)b).value;
                                return true;
                            }
                            else
                            {   //script does not exist within the mod data
                                MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_Toggle:(" + toggle.gameObject.name + ").OnToggleClickHandler property failed because the specified script(" +
                                    ((ValString)b).value + " could not be found.");
                                return false;
                            }
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Toggle:(" + toggle.gameObject.name + ").OnToggleClickHandler property with a non-String parameter." +
                                " The expected argument should be a string containing the script name. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    
                        //case "Text":
                    //    if (b is ValString)
                    //    {
                    //        string text = ((ValString)b).value;
                    //        if (text.Length > 0)
                    //        {
                    //            toggle.Label.SetText(text);
                    //            return true;
                    //        }
                    //        else
                    //        {
                    //            MiniScriptSingleton.LogWarning("Attempt to modify mouse event handler script for MUUI_Toggle(" + toggle.gameObject.name + ").Text property with an empty/zero-length string parameter.");
                    //            return true;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Toggle(" + toggle.gameObject.name + ").Text property with a non-String(ValString) parameter." +
                    //                                        " The expected argument should be a ValString containing a string value. The argument given appears to be of " + b.GetType().Name + " type.");
                    //        return false;
                    //    }

                    #region fluff
                    //case "OnPointerEnterHandler":
                    //    #region
                    //    if (b is ValString)
                    //    {
                    //        if (MiniScriptSingleton.ScriptExists(((ValString)b).value))
                    //        {
                    //            toggle.ScriptOnEnter = ((ValString)b).value;
                    //            return true;
                    //        }
                    //        else
                    //        {   //script does not exist within the mod data
                    //            MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_Toggle(" + toggle.gameObject.name + ").OnPointerEnter property failed because the specified script(" +
                    //                ((ValString)b).value + " could not be found.");
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Toggle(" + toggle.gameObject.name + ").OnPointerEnter property with a non-String parameter." +
                    //            " The expected argument should be a string containing the script name. The argument given appears to be of " + b.GetType().Name + " type.");
                    //        return false;
                    //    }
                    //#endregion
                    //case "OnPointerExitHandler":
                    //    #region
                    //    if (b is ValString)
                    //    {
                    //        if (MiniScriptSingleton.ScriptExists(((ValString)b).value))
                    //        {
                    //            toggle.ScriptOnExit = ((ValString)b).value;
                    //            return true;
                    //        }
                    //        else
                    //        {   //script does not exist within the mod data
                    //            MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_Toggle(" + toggle.gameObject.name + ").OnPointerExit property failed because the specified script(" +
                    //                ((ValString)b).value + " could not be found.");
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Toggle(" + toggle.gameObject.name + ").OnPointerEnter property with a non-String parameter." +
                    //            " The expected argument should be a string containing the script name. The argument given appears to be of " + b.GetType().Name + " type.");
                    //        return false;
                    //    }
                    //#endregion
                    //case "OnPointerLeftClickHandler":
                    //    #region
                    //    if (b is ValString)
                    //    {
                    //        if (MiniScriptSingleton.ScriptExists(((ValString)b).value))
                    //        {
                    //            toggle.ScriptOnLeftClick = ((ValString)b).value;
                    //            return true;
                    //        }
                    //        else
                    //        {   //script does not exist within the mod data
                    //            MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_Toggle(" + toggle.gameObject.name + ").OnPointerLeftClick property failed because the specified script(" +
                    //                ((ValString)b).value + " could not be found.");
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Toggle(" + toggle.gameObject.name + ").OnPointerLeftClick property with a non-String parameter." +
                    //            " The expected argument should be a string containing the script name. The argument given appears to be of " + b.GetType().Name + " type.");
                    //        return false;
                    //    }
                    //#endregion
                    //case "OnPointerDoubleLeftClickHandler":
                    //    #region
                    //    if (b is ValString)
                    //    {
                    //        if (MiniScriptSingleton.ScriptExists(((ValString)b).value))
                    //        {
                    //            toggle.ScriptOnDoubleLeftClick = ((ValString)b).value;
                    //            return true;
                    //        }
                    //        else
                    //        {   //script does not exist within the mod data
                    //            MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_Toggle(" + toggle.gameObject.name + ").OnPointerDoubleLeftClick property failed because the specified script(" +
                    //                ((ValString)b).value + " could not be found.");
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Toggle(" + toggle.gameObject.name + ").OnPointerDoubleLeftClick property with a non-String parameter." +
                    //            " The expected argument should be a string containing the script name. The argument given appears to be of " + b.GetType().Name + " type.");
                    //        return false;
                    //    }
                    //#endregion
                    //case "OnPointerRightClickHandler":
                    //    #region
                    //    if (b is ValString)
                    //    {
                    //        if (MiniScriptSingleton.ScriptExists(((ValString)b).value))
                    //        {
                    //            toggle.ScriptOnRightClick = ((ValString)b).value;
                    //            return true;
                    //        }
                    //        else
                    //        {   //script does not exist within the mod data
                    //            MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_Toggle(" + toggle.gameObject.name + ").OnPointerRightClick property failed because the specified script(" +
                    //                ((ValString)b).value + " could not be found.");
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Toggle(" + toggle.gameObject.name + ").OnPointerRightClick property with a non-String parameter." +
                    //            " The expected argument should be a string containing the script name. The argument given appears to be of " + b.GetType().Name + " type.");
                    //        return false;
                    //    }
                    //#endregion
                    //case "OnPointerDoubleRightClickHandler":
                    //    #region
                    //    if (b is ValString)
                    //    {
                    //        if (MiniScriptSingleton.ScriptExists(((ValString)b).value))
                    //        {
                    //            toggle.ScriptOnDoubleRightClick = ((ValString)b).value;
                    //            return true;
                    //        }
                    //        else
                    //        {   //script does not exist within the mod data
                    //            MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_Toggle(" + toggle.gameObject.name + ").OnPointerDoubleRightClick property failed because the specified script(" +
                    //                ((ValString)b).value + " could not be found.");
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Toggle(" + toggle.gameObject.name + ").OnPointerDoubleRightClick property with a non-String parameter." +
                    //            " The expected argument should be a string containing the script name. The argument given appears to be of " + b.GetType().Name + " type.");
                    //        return false;
                    //    }
                    //#endregion
                    //case "OnPointerMiddleClickHandler":
                    //    #region
                    //    if (b is ValString)
                    //    {
                    //        if (MiniScriptSingleton.ScriptExists(((ValString)b).value))
                    //        {
                    //            toggle.ScriptOnMiddleClick = ((ValString)b).value;
                    //            return true;
                    //        }
                    //        else
                    //        {   //script does not exist within the mod data
                    //            MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_Toggle(" + toggle.gameObject.name + ").OnPointerMiddleClick property failed because the specified script(" +
                    //                ((ValString)b).value + " could not be found.");
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Toggle(" + toggle.gameObject.name + ").OnPointerMiddleClick property with a non-String parameter." +
                    //            " The expected argument should be a string containing the script name. The argument given appears to be of " + b.GetType().Name + " type.");
                    //        return false;
                    //    }
                    //#endregion
                    #endregion
                    default:
                        if (b is null) { MiniScriptSingleton.LogError("Attempt to modify MUUI_Toggle(" + toggle.gameObject.name + ") with an unknown property label (" + s + ")."); }
                        else { MiniScriptSingleton.LogError("Attempt to modify MUUI_Toggle(" + toggle.gameObject.name + ") with an unknown property label (" + s + ") with an argument that appears to be of type " + b.GetType().ToString()); }
                        return false;
                }
            }
            else
            {
                if (a == null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify MUUI_Toggle(" + toggle.gameObject.name + ") with a property label accessor that is null.");
                }
                else if (a != null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify MUUI_Toggle(" + toggle.gameObject.name + ") with a property label accessor that is not a String. It appears to be a (" + a.GetType().Name + ").");
                }

                return false;
            }
        }

        public static void SetupToggle(this MiniScript.MSGS.MUUI.TwoDimensional.MUUIToggle toggle, ref DataRow row)
        {            
            toggle.gameObject.SetActive((bool)row["enabled"]);
            toggle.name = (string)row["name"];
            toggle.ScriptOnToggle = (string)row["OnToggleChanged"];
        }
    }
}