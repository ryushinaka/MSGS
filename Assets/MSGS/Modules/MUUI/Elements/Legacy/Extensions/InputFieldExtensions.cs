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
    public static class InputFieldExtensions
    {
        public static ValMap ToValMap(this MiniScript.MSGS.MUUI.TwoDimensional.InputField text)
        {
            ValMap rst = new ValMap();
            rst.map.Add(new ValString("enabled"), ValNumber.Truth(text.enabled));
            rst.map.Add(new ValString("name"), new ValString(text.gameObject.name));
            rst.map.Add(new ValString("Text"), new ValString(text.Text.text));
            //font style to accent text drawn by the component
            rst.map.Add(new ValString("Bold"), ValNumber.Truth((text.Text.fontStyle & FontStyles.Bold) != 0));
            rst.map.Add(new ValString("Italic"), ValNumber.Truth((text.Text.fontStyle & FontStyles.Italic) != 0));
            rst.map.Add(new ValString("Underline"), ValNumber.Truth((text.Text.fontStyle & FontStyles.Underline) != 0));
            rst.map.Add(new ValString("Strike"), ValNumber.Truth((text.Text.fontStyle & FontStyles.Strikethrough) != 0));
            rst.map.Add(new ValString("Lowercase"), ValNumber.Truth((text.Text.fontStyle & FontStyles.LowerCase) != 0));
            rst.map.Add(new ValString("Uppercase"), ValNumber.Truth((text.Text.fontStyle & FontStyles.UpperCase) != 0));
            rst.map.Add(new ValString("SmallCaps"), ValNumber.Truth((text.Text.fontStyle & FontStyles.SmallCaps) != 0));

            rst.map.Add(new ValString("alignHorizontal"), new ValString(text.Text.horizontalAlignment.ToString()));
            rst.map.Add(new ValString("alignVertical"), new ValString(text.Text.verticalAlignment.ToString()));

            rst.map.Add(new ValString("FontSize"), new ValNumber(text.Text.fontSize));
            rst.map.Add(new ValString("FontSizeMin"), new ValNumber(text.Text.fontSizeMin));
            rst.map.Add(new ValString("FontSizeMax"), new ValNumber(text.Text.fontSizeMax));
            rst.map.Add(new ValString("AutoSize"), ValNumber.Truth(text.Text.autoSizeTextContainer));
            rst.map.Add(new ValString("ColorR"), new ValNumber(text.Text.color.r));
            rst.map.Add(new ValString("ColorG"), new ValNumber(text.Text.color.g));
            rst.map.Add(new ValString("ColorB"), new ValNumber(text.Text.color.b));
            rst.map.Add(new ValString("ColorA"), new ValNumber(text.Text.color.a));

            return rst;
        }

        public static bool UpdateInputField(this MiniScript.MSGS.MUUI.TwoDimensional.InputField text, Value a, Value b)
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
                            text.enabled = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_Text(" + text.gameObject.name + ").enabled property with a non-boolean(ValNumber) parameter." +
                                                            " The expected argument should be a ValNumber containing the bool value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "Text":
                        #region
                        if (b is ValString)
                        {
                            text.Text.SetText(((ValString)b).value);
                            return true;
                        }
                        else if (b is ValNumber)
                        {
                            text.Text.SetText(((ValNumber)b).value.ToString());
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_Text(" + text.gameObject.name + ").Text property with an argument that is neither a String nor a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "Bold":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((text.Text.fontStyle & FontStyles.Bold) != 0);
                            if (t.BoolValue())
                            {
                                //if its not set, set the flag to true
                                if (!isSet) { text.Text.fontStyle |= FontStyles.Bold; }
                            }
                            else if (!t.BoolValue())
                            {
                                //if the flag is true, set to false
                                if (isSet) { text.Text.fontStyle ^= FontStyles.Bold; }
                                //else the flag is already false, so do nothing
                            }
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_Text.Text(" + text.gameObject.name + ").FontStyle.Bold property with an argument that is neither a String nor a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "Italic":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((text.Text.fontStyle & FontStyles.Italic) != 0);

                            if (t.BoolValue())
                            {
                                if (!isSet) { text.Text.fontStyle |= FontStyles.Italic; }
                            }
                            else if (!t.BoolValue())
                            {
                                if (isSet) { text.Text.fontStyle ^= FontStyles.Italic; }
                            }
                            return true;

                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_Text(" + text.gameObject.name + ").FontStyle.Italic property with an argument that is not boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "Underline":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((text.Text.fontStyle & FontStyles.Underline) != 0);

                            if (t.BoolValue())
                            {
                                if (!isSet) { text.Text.fontStyle |= FontStyles.Underline; }
                            }
                            else if (!t.BoolValue())
                            {
                                if (isSet) { text.Text.fontStyle ^= FontStyles.Underline; }
                            }
                            return true;

                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_Text(" + text.gameObject.name + ").FontStyle.Underline property with an argument that is not boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "Strike":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((text.Text.fontStyle & FontStyles.Strikethrough) != 0);

                            if (t.BoolValue())
                            {
                                if (!isSet) { text.Text.fontStyle |= FontStyles.Strikethrough; }
                            }
                            else if (!t.BoolValue())
                            {
                                if (isSet) { text.Text.fontStyle ^= FontStyles.Strikethrough; }
                            }
                            return true;

                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_Text(" + text.gameObject.name + ").FontStyle.Strike property with an argument that is not boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "Lowercase":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((text.Text.fontStyle & FontStyles.LowerCase) != 0);

                            if (t.BoolValue())
                            {
                                if (!isSet) { text.Text.fontStyle |= FontStyles.LowerCase; }
                            }
                            else if (!t.BoolValue())
                            {
                                if (isSet) { text.Text.fontStyle ^= FontStyles.LowerCase; }
                            }
                            return true;

                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_text(" + text.gameObject.name + ").FontStyle.Lowercase property with an argument that is not boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "Uppercase":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((text.Text.fontStyle & FontStyles.UpperCase) != 0);

                            if (t.BoolValue())
                            {
                                if (!isSet) { text.Text.fontStyle |= FontStyles.UpperCase; }
                            }
                            else if (!t.BoolValue())
                            {
                                if (isSet) { text.Text.fontStyle ^= FontStyles.UpperCase; }
                            }
                            return true;

                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_text(" + text.gameObject.name + ").FontStyle.Uppercase property with an argument that is not boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "SmallCaps":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((text.Text.fontStyle & FontStyles.SmallCaps) != 0);

                            if (t.BoolValue())
                            {
                                if (!isSet) { text.Text.fontStyle |= FontStyles.SmallCaps; }
                            }
                            else if (!t.BoolValue())
                            {
                                if (isSet) { text.Text.fontStyle ^= FontStyles.SmallCaps; }
                            }
                            return true;

                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_text(" + text.gameObject.name + ").FontStyle.SmallCaps property with an argument that is not boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "alignVertical":
                        #region
                        if (b is ValString)
                        {
                            switch (b.ToString())
                            {
                                case "Top": text.Text.verticalAlignment = VerticalAlignmentOptions.Top; break;
                                case "Middle": text.Text.verticalAlignment = VerticalAlignmentOptions.Middle; break;
                                case "Bottom": text.Text.verticalAlignment = VerticalAlignmentOptions.Bottom; break;
                                case "Baseline": text.Text.verticalAlignment = VerticalAlignmentOptions.Baseline; break;
                                case "Geometry": text.Text.verticalAlignment = VerticalAlignmentOptions.Geometry; break;
                                case "Capline": text.Text.verticalAlignment = VerticalAlignmentOptions.Capline; break;
                                default:
                                    MiniScriptSingleton.LogError("Attempt to modify MUUI_Text(" + text.gameObject.name + ".alignVertical with an unsupported argument." +
                                        " Expected values are: Top, Middle, Bottom, Baseline, Geometry, or Capline");
                                    return false;
                            }
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_Text(" + text.gameObject.name + ".alignVertical with an unsupported argument.  Argument should be a String containing" +
                                " one of the following values: Top, Middle, Bottom, Baseline, Geometry, or Capline");
                            return false;
                        }
                    #endregion
                    case "alignHorizontal":
                        #region
                        if (b is ValString)
                        {
                            switch (b.ToString())
                            {
                                case "Left": text.Text.horizontalAlignment = HorizontalAlignmentOptions.Left; break;
                                case "Center": text.Text.horizontalAlignment = HorizontalAlignmentOptions.Center; break;
                                case "Right": text.Text.horizontalAlignment = HorizontalAlignmentOptions.Right; break;
                                case "Justified": text.Text.horizontalAlignment = HorizontalAlignmentOptions.Justified; break;
                                case "Flush": text.Text.horizontalAlignment = HorizontalAlignmentOptions.Flush; break;
                                case "Geometry": text.Text.horizontalAlignment = HorizontalAlignmentOptions.Geometry; break;
                                default:
                                    MiniScriptSingleton.LogError("Attempt to modify MUUI_Text(" + text.gameObject.name + ".alignHorizontal with an unsupported argument." +
                                        " Expected values are: Left, Center, Right, Justified, Flush, or Geometry");
                                    return false;
                            }
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_Text(" + text.gameObject.name + ".alignHorizontal with an unsupported argument.  Argument should be a String containing" +
                                " one of the following values: Left, Center, Right, Justified, Flush, or Geometry");
                            return false;
                        }
                    #endregion
                    case "FontSize":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            text.Text.fontSize = (float)t.value;
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_text(" + text.gameObject.name + ").FontSize property with an argument that is not a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "FontSizeMin":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            text.Text.fontSizeMin = (float)t.value;
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_text(" + text.gameObject.name + ").FontStyle.FontSizeMin property with an argument that is not a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "FontSizeMax":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            text.Text.fontSizeMax = (float)t.value;
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_text(" + text.gameObject.name + ").FontStyle.FontSizeMax property with an argument that is not a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "AutoSize":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            text.Text.autoSizeTextContainer = t.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_text(" + text.gameObject.name + ").FontStyle.AutoSize property with an argument that is not a boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "ColorR":
                        #region
                        if (b is ValNumber)
                        {
                            text.Text.color = new Color(b.FloatValue(), text.Text.color.g, text.Text.color.b, text.Text.color.a);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_text(" + text.gameObject.name + ").Color.R property with an argument that is not a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "ColorG":
                        #region
                        if (b is ValNumber)
                        {
                            text.Text.color = new Color(text.Text.color.r, b.FloatValue(), text.Text.color.b, text.Text.color.a);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_text(" + text.gameObject.name + ").Color.G property with an argument that is not a Number, no change made. " +
                                " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "ColorB":
                        #region
                        if (b is ValNumber)
                        {
                            text.Text.color = new Color(text.Text.color.r, text.Text.color.g, b.FloatValue(), text.Text.color.a);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_text(" + text.gameObject.name + ").Color.B property with an argument that is not a Number, no change made. " +
                                " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "ColorA":
                        #region
                        if (b is ValNumber)
                        {
                            text.Text.color = new Color(text.Text.color.r, text.Text.color.g, text.Text.color.b, b.FloatValue());
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_text(" + text.gameObject.name + ").Color.A property with an argument that is not a Number, no change made. " +
                                " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    default:
                        if (b is null) { MiniScriptSingleton.LogError("Attempt to modify MUUI_Text(" + text.gameObject.name + ") with an unknown property label (" + s + ")."); }
                        else { MiniScriptSingleton.LogError("Attempt to modify MUUI_Text(" + text.gameObject.name + ") with an unknown property label (" + s + ") with an argument that appears to be of type " + b.GetType().ToString()); }
                        return false;
                }
            }
            else
            {
                if (a == null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify MUUI_Text(" + text.gameObject.name + ") with a property label accessor that is null.");
                }
                else if (a != null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify MUUI_Text(" + text.gameObject.name + ") with a property label accessor that is not a String. It appears to be a (" + a.GetType().Name + ").");
                }

                return false;
            }
        }

        public static void SetupInputField(this MiniScript.MSGS.MUUI.TwoDimensional.InputField input, ref DataRow row)
        {

        }
    }
}
