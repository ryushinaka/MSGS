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
    public static class SliderExtensions
    {
        public static ValMap ToValMap(this MiniScript.MSGS.MUUI.TwoDimensional.Slider slider)
        {
            ValMap rst = new ValMap();
            rst.map.Add(new ValString("name"), new ValString(slider.name));
            rst.map.Add(new ValString("enabled"), ValNumber.Truth(true));
            rst.map.Add(new ValString("MinValue"), new ValNumber(0));
            rst.map.Add(new ValString("MaxValue"), new ValNumber(0));
            rst.map.Add(new ValString("Value"), new ValNumber(0));
            rst.map.Add(new ValString("OnValueChanged"), new ValString(slider.ScriptOnValueChanged));

            return rst;
        }

        public static bool UpdateSlider(this MiniScript.MSGS.MUUI.TwoDimensional.Slider slider, Value a, Value b)
        {
            if(a is ValString)
            {
                string s = ((ValString)a).value;
                switch (s)
                {
                    case "name":
                        #region
                        if (b is ValString)
                        {
                            slider.gameObject.name = ((ValString)b).value;
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Slider(" + slider.gameObject.name + ").name property with a non-String(ValString) parameter." +
                                                            " The expected argument should be a ValString containing the string literal value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                        #endregion
                    case "enabled":
                        #region
                        if (b is ValNumber)
                        {
                            slider.enabled = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Slider(" + slider.gameObject.name + ").enabled property with a non-boolean(ValNumber) parameter." +
                                                            " The expected argument should be a ValNumber containing the bool value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "MinValue":
                        #region
                        if (b is ValNumber)
                        {
                            slider.MinValue = (ValNumber)b;
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Slider(" + slider.gameObject.name + ").MinValue property with a non-Numeric(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing the numeric value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                        #endregion
                    case "MaxValue":
                        #region
                        if (b is ValNumber)
                        {
                            slider.MaxValue = (ValNumber)b;
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Slider(" + slider.gameObject.name + ").MaxValue property with a non-Numeric(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing the numeric value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                        #endregion
                    case "Value":
                        #region
                        if (b is ValNumber)
                        {
                            slider.CurrentValue = (ValNumber)b;
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Slider(" + slider.gameObject.name + ").Value property with a non-Numeric(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing the numeric value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                        #endregion
                    case "OnValueChanged":
                        #region
                        if (b is ValString)
                        {
                            if (MiniScriptSingleton.ScriptExists(((ValString)b).value))
                            {
                                slider.ScriptOnValueChanged = ((ValString)b).value;
                                return true;
                            }
                            else
                            {   //script does not exist within the mod data
                                MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_Slider(" + slider.gameObject.name + ").OnValueChanged property failed because the specified script(" +
                                    ((ValString)b).value + " could not be found.");
                                return false;
                            }
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify the script for MUUI_Slider(" + slider.gameObject.name + ").OnValueChanged property with a non-String parameter." +
                                " The expected argument should be a string containing the script name. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    default:
                        return false;
                }
            }
            else
            {
                if (a == null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify MUUI_Slider(" + slider.gameObject.name + ") with a property label accessor that is null.");
                }
                else if (a != null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify MUUI_Slider(" + slider.gameObject.name + ") with a property label accessor that is not a String. It appears to be a (" + a.GetType().Name + ").");
                }

                return false;
            }
        }

        public static void SetupSlider(this MiniScript.MSGS.MUUI.TwoDimensional.Slider slider, ref DataRow row)
        {
            slider.name = (string)row["name"];            
            slider.gameObject.SetActive((bool)row["enabled"]);
            slider.MinValue = (ValNumber)row["MinValue"];
            slider.MaxValue = (ValNumber)row["MaxValue"];
            slider.CurrentValue = (ValNumber)row["Value"];
            slider.ScriptOnValueChanged = (string)row["OnValueChanged"];
        }
    }
}
