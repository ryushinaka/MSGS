using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using TMPro;

namespace MiniScript.MSGS.MUUI.TwoDimensional
{
    public static class ButtonAnimatedExtensions
    {

        public static ValMap ToValMap(this MUUIButtonAnimated button)
        {
            ValMap rst = new ValMap();
            rst.map.Add(new ValString("InstanceID"), new ValNumber(button.gameObject.GetInstanceID()));
            rst.map.Add(new ValString("enabled"), ValNumber.Truth(button.enabled));
            rst.map.Add(new ValString("name"), new ValString(button.gameObject.name));
            rst.map.Add(new ValString("OnPointerEnterHandler"), new ValString(button.ScriptOnEnter));
            rst.map.Add(new ValString("OnPointerExitHandler"), new ValString(button.ScriptOnExit));
            rst.map.Add(new ValString("OnPointerLeftClickHandler"), new ValString(button.ScriptOnLeftClick));
            rst.map.Add(new ValString("OnPointerRightClickHandler"), new ValString(button.ScriptOnRightClick));
            rst.map.Add(new ValString("OnPointerDoubleLeftClickHandler"), new ValString(button.ScriptOnDoubleLeftClick));
            rst.map.Add(new ValString("OnPointerDoubleRightClickHandler"), new ValString(button.ScriptOnDoubleRightClick));
            rst.map.Add(new ValString("OnPointerMiddleClickHandler"), new ValString(button.ScriptOnMiddleClick));

            //rst.map.Add(new ValString("Sprite"), new ValString(string.Empty));
            rst.map.Add(new ValString("ColorR"), new ValString(button.image.color.r.ToString()));
            rst.map.Add(new ValString("ColorG"), new ValString(button.image.color.g.ToString()));
            rst.map.Add(new ValString("ColorB"), new ValString(button.image.color.b.ToString()));
            rst.map.Add(new ValString("ColorA"), new ValString(button.image.color.a.ToString()));

            rst.map.Add(new ValString("Maskable"), ValNumber.Truth(button.image.maskable));
            rst.map.Add(new ValString("PreserveAspect"), ValNumber.Truth(button.image.preserveAspect));

            rst.map.Add(new ValString("Text"), new ValString(button.Text.text));
            rst.map.Add(new ValString("FontSize"), new ValString(button.Text.text));
            rst.map.Add(new ValString("FontSizeMin"), new ValNumber(button.Text.fontSizeMin));
            rst.map.Add(new ValString("FontSizeMax"), new ValNumber(button.Text.fontSizeMax));
            rst.map.Add(new ValString("AutoSize"), ValNumber.Truth(button.Text.autoSizeTextContainer));
            rst.map.Add(new ValString("TextColorR"), new ValNumber(button.Text.color.r));
            rst.map.Add(new ValString("TextColorG"), new ValNumber(button.Text.color.g));
            rst.map.Add(new ValString("TextColorB"), new ValNumber(button.Text.color.b));
            rst.map.Add(new ValString("TextColorA"), new ValNumber(button.Text.color.a));
            rst.map.Add(new ValString("alignHorizontal"), new ValString(button.Text.horizontalAlignment.ToString()));
            rst.map.Add(new ValString("alignVertical"), new ValString(button.Text.verticalAlignment.ToString()));

            rst.map.Add(new ValString("Bold"), ValNumber.Truth((button.Text.fontStyle & FontStyles.Bold) != 0));
            rst.map.Add(new ValString("Italic"), ValNumber.Truth((button.Text.fontStyle & FontStyles.Italic) != 0));
            rst.map.Add(new ValString("Underline"), ValNumber.Truth((button.Text.fontStyle & FontStyles.Underline) != 0));
            rst.map.Add(new ValString("Strike"), ValNumber.Truth((button.Text.fontStyle & FontStyles.Strikethrough) != 0));
            rst.map.Add(new ValString("Lowercase"), ValNumber.Truth((button.Text.fontStyle & FontStyles.LowerCase) != 0));
            rst.map.Add(new ValString("Uppercase"), ValNumber.Truth((button.Text.fontStyle & FontStyles.UpperCase) != 0));
            rst.map.Add(new ValString("SmallCaps"), ValNumber.Truth((button.Text.fontStyle & FontStyles.SmallCaps) != 0));

            return rst;
        }

        public static bool UpdateButton(this MUUIButtonAnimated button, Value a, Value b)
        {
            if (a is ValString)
            {
                string s = ((ValString)a).value;
                switch (s)
                {
                    case "name":
                        #region
                        if (b is ValString)
                        {
                            button.gameObject.name = ((ValString)b).value;
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_ButtonAnimated(" + button.gameObject.name + ").name property with a non-String(ValString) parameter." +
                                                            " The expected argument should be a ValString containing the string literal value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "enabled":
                        #region
                        if (b is ValNumber)
                        {
                            button.enabled = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_ButtonAnimated(" + button.gameObject.name + ").enabled property with a non-boolean(ValNumber) parameter." +
                                                            " The expected argument should be a ValNumber containing the bool value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "OnPointerEnterHandler":
                        #region
                        if (b is ValString)
                        {
                            if (MiniScriptSingleton.ScriptExists(((ValString)b).value))
                            {
                                button.ScriptOnEnter = ((ValString)b).value;
                                return true;
                            }
                            else
                            {   //script does not exist within the mod data
                                MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_ButtonAnimated(" + button.gameObject.name + ").OnPointerEnterHandler property failed because the specified script(" +
                                    ((ValString)b).value + " could not be found.");
                                return false;
                            }
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_ButtonAnimated(" + button.gameObject.name + ").OnPointerEnterHandler property with a non-String parameter." +
                                " The expected argument should be a string containing the script name. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "OnPointerExitHandler":
                        #region
                        if (b is ValString)
                        {
                            if (MiniScriptSingleton.ScriptExists(((ValString)b).value))
                            {
                                button.ScriptOnExit = ((ValString)b).value;
                                return true;
                            }
                            else
                            {   //script does not exist within the mod data
                                MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_ButtonAnimated(" + button.gameObject.name + ").OnPointerExitHandler property failed because the specified script(" +
                                    ((ValString)b).value + " could not be found.");
                                return false;
                            }
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_ButtonAnimated(" + button.gameObject.name + ").OnPointerExitHandler property with a non-String parameter." +
                                " The expected argument should be a string containing the script name. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "OnPointerLeftClickHandler":
                        #region
                        if (b is ValString)
                        {
                            if (MiniScriptSingleton.ScriptExists(((ValString)b).value))
                            {
                                button.ScriptOnLeftClick = ((ValString)b).value;
                                return true;
                            }
                            else
                            {   //script does not exist within the mod data
                                MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_ButtonAnimated(" + button.gameObject.name + ").OnPointerClickHandler property failed because the specified script(" +
                                    ((ValString)b).value + " could not be found.");
                                return false;
                            }
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_ButtonAnimated(" + button.gameObject.name + ").OnPointerClickHandler property with a non-String parameter." +
                                " The expected argument should be a string containing the script name. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "OnPointerDoubleLeftClickHandler":
                        #region
                        if (b is ValString)
                        {
                            if (MiniScriptSingleton.ScriptExists(((ValString)b).value))
                            {
                                button.ScriptOnDoubleLeftClick = ((ValString)b).value;
                                return true;
                            }
                            else
                            {   //script does not exist within the mod data
                                MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_ButtonAnimated(" + button.gameObject.name + ").OnPointerDoubleClickHandler property failed because the specified script(" +
                                    ((ValString)b).value + " could not be found.");
                                return false;
                            }
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_ButtonAnimated(" + button.gameObject.name + ").OnPointerDoubleClickHandler property with a non-String parameter." +
                                " The expected argument should be a string containing the script name. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "OnPointerDoubleRightClickHandler":
                        #region
                        if (b is ValString)
                        {
                            if (MiniScriptSingleton.ScriptExists(((ValString)b).value))
                            {
                                button.ScriptOnDoubleRightClick = ((ValString)b).value;
                                return true;
                            }
                            else
                            {   //script does not exist within the mod data
                                MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_ButtonAnimated(" + button.gameObject.name + ").OnPointerDoubleClickHandler property failed because the specified script(" +
                                    ((ValString)b).value + " could not be found.");
                                return false;
                            }
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_ButtonAnimated(" + button.gameObject.name + ").OnPointerDoubleClickHandler property with a non-String parameter." +
                                " The expected argument should be a string containing the script name. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "OnPointerRightClickHandler":
                        #region
                        if (b is ValString)
                        {
                            if (MiniScriptSingleton.ScriptExists(((ValString)b).value))
                            {
                                button.ScriptOnRightClick = ((ValString)b).value;
                                return true;
                            }
                            else
                            {   //script does not exist within the mod data
                                MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_ButtonAnimated(" + button.gameObject.name + ").OnPointerRightClickHandler property failed because the specified script(" +
                                    ((ValString)b).value + " could not be found.");
                                return false;
                            }
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_ButtonAnimated(" + button.gameObject.name + ").OnPointerRightClickHandler property with a non-String parameter." +
                                " The expected argument should be a string containing the script name. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "OnPointerMiddleClickHandler":
                        #region
                        if (b is ValString)
                        {
                            if (MiniScriptSingleton.ScriptExists(((ValString)b).value))
                            {
                                button.ScriptOnMiddleClick = ((ValString)b).value;
                                return true;
                            }
                            else
                            {   //script does not exist within the mod data
                                MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_ButtonAnimated(" + button.gameObject.name + ").OnPointerMiddleClickHandler property failed because the specified script(" +
                                    ((ValString)b).value + " could not be found.");
                                return false;
                            }
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_ButtonAnimated(" + button.gameObject.name + ").OnPointerMiddleClickHandler property with a non-String parameter." +
                                " The expected argument should be a string containing the script name. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    #region
                    //case "Sprite":
                    //    #region
                    //    if (b is ValString)
                    //    {
                    //        //the string value given should link to a valid SpriteAtlas object
                    //        //if (SpriteSheetCollection.ContainsAssetPath(((ValString)b).value))
                    //        //{
                    //        //    var sac = SpriteSheetCollection.GetSpriteSheet(((ValString)b).value);

                    //        //    button.image.sprite =
                    //        //        SpriteSheetCollection.GetSpriteSheet(((ValString)b).value).Sprites[0];
                    //        //    return true;
                    //        //}
                    //        //else
                    //        //{
                    //        //    MiniScriptSingleton.LogWarning("Sprite Asset Path given for MUUI_ButtonAnimated(" + button.gameObject.name + ").Sprite was not found." +
                    //        //        " Asset Path value given was: " + ((ValString)b).value);
                    //        //    return false;
                    //        //}
                    //        return true;
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ").Sprite with a property value that is not a String. " +
                    //            " Argument given appears to be of type " + b.GetType().Name);
                    //        return false;
                    //    }
                    //#endregion
                    #endregion
                    case "ColorR":
                        #region
                        if (b is ValNumber)
                        {
                            button.image.color = new Color(b.FloatValue(), button.image.color.g, button.image.color.b, button.image.color.a);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ").Image.color.R with a property value that is not a Number. " +
                                " Argument given appears to be of type " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "ColorG":
                        #region
                        if (b is ValNumber)
                        {
                            button.image.color = new Color(button.image.color.r, b.FloatValue(), button.image.color.b, button.image.color.a);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ").Image.color.G with a property value that is not a Number. " +
                                " Argument given appears to be of type " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "ColorB":
                        #region
                        if (b is ValNumber)
                        {
                            button.image.color = new Color(button.image.color.r, button.image.color.g, b.FloatValue(), button.image.color.a);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ").Image.color.B with a property value that is not a Number. " +
                                " Argument given appears to be of type " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "ColorA":
                        #region
                        if (b is ValNumber)
                        {
                            button.image.color = new Color(button.image.color.r, button.image.color.a, button.image.color.b, b.FloatValue());
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ").Image.color.A with a property value that is not a Number. " +
                                " Argument given appears to be of type " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "Text":
                        #region
                        if (b is ValString)
                        {
                            if (((ValString)b).value.Length == 0) { button.Text.enabled = false; }
                            else
                            {
                                if (button.Text.enabled == false) { button.Text.enabled = true; }
                                button.Text.SetText(((ValString)b).value);
                            }
                            return true;
                        }
                        else if (b is ValNumber)
                        {
                            if (button.Text.enabled == false) { button.Text.enabled = true; }
                            button.Text.SetText(((ValNumber)b).value.ToString());
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ").Text property with an argument that is neither a String nor a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "Bold":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((button.Text.fontStyle & FontStyles.Bold) != 0);
                            if (t.BoolValue())
                            {
                                //if its not set, set the flag to true
                                if (!isSet) { button.Text.fontStyle |= FontStyles.Bold; }
                            }
                            else if (!t.BoolValue())
                            {
                                //if the flag is true, set to false
                                if (isSet) { button.Text.fontStyle ^= FontStyles.Bold; }
                                //else the flag is already false, so do nothing
                            }
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ").FontStyle.Bold property with an argument that is neither a String nor a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "Italic":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((button.Text.fontStyle & FontStyles.Italic) != 0);

                            if (t.BoolValue())
                            {
                                if (!isSet) { button.Text.fontStyle |= FontStyles.Italic; }
                            }
                            else if (!t.BoolValue())
                            {
                                if (isSet) { button.Text.fontStyle ^= FontStyles.Italic; }
                            }
                            return true;

                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ").FontStyle.Italic property with an argument that is not boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "Underline":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((button.Text.fontStyle & FontStyles.Underline) != 0);

                            if (t.BoolValue())
                            {
                                if (!isSet) { button.Text.fontStyle |= FontStyles.Underline; }
                            }
                            else if (!t.BoolValue())
                            {
                                if (isSet) { button.Text.fontStyle ^= FontStyles.Underline; }
                            }
                            return true;

                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ").FontStyle.Underline property with an argument that is not boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "Strike":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((button.Text.fontStyle & FontStyles.Strikethrough) != 0);

                            if (t.BoolValue())
                            {
                                if (!isSet) { button.Text.fontStyle |= FontStyles.Strikethrough; }
                            }
                            else if (!t.BoolValue())
                            {
                                if (isSet) { button.Text.fontStyle ^= FontStyles.Strikethrough; }
                            }
                            return true;

                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ").FontStyle.Strike property with an argument that is not boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "Lowercase":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((button.Text.fontStyle & FontStyles.LowerCase) != 0);

                            if (t.BoolValue())
                            {
                                if (!isSet) { button.Text.fontStyle |= FontStyles.LowerCase; }
                            }
                            else if (!t.BoolValue())
                            {
                                if (isSet) { button.Text.fontStyle ^= FontStyles.LowerCase; }
                            }
                            return true;

                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ").FontStyle.Lowercase property with an argument that is not boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "Uppercase":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((button.Text.fontStyle & FontStyles.UpperCase) != 0);

                            if (t.BoolValue())
                            {
                                if (!isSet) { button.Text.fontStyle |= FontStyles.UpperCase; }
                            }
                            else if (!t.BoolValue())
                            {
                                if (isSet) { button.Text.fontStyle ^= FontStyles.UpperCase; }
                            }
                            return true;

                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ").FontStyle.Uppercase property with an argument that is not boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "SmallCaps":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((button.Text.fontStyle & FontStyles.SmallCaps) != 0);

                            if (t.BoolValue())
                            {
                                if (!isSet) { button.Text.fontStyle |= FontStyles.SmallCaps; }
                            }
                            else if (!t.BoolValue())
                            {
                                if (isSet) { button.Text.fontStyle ^= FontStyles.SmallCaps; }
                            }
                            return true;

                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ").FontStyle.SmallCaps property with an argument that is not boolean, no change made. " +
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
                                case "Top": button.Text.verticalAlignment = VerticalAlignmentOptions.Top; break;
                                case "Middle": button.Text.verticalAlignment = VerticalAlignmentOptions.Middle; break;
                                case "Bottom": button.Text.verticalAlignment = VerticalAlignmentOptions.Bottom; break;
                                case "Baseline": button.Text.verticalAlignment = VerticalAlignmentOptions.Baseline; break;
                                case "Geometry": button.Text.verticalAlignment = VerticalAlignmentOptions.Geometry; break;
                                case "Capline": button.Text.verticalAlignment = VerticalAlignmentOptions.Capline; break;
                                default:
                                    MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ".alignVertical with an unsupported argument." +
                                        " Expected values are: Top, Middle, Bottom, Baseline, Geometry, or Capline");
                                    return false;
                            }
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ".alignVertical with an unsupported argument.  Argument should be a String containing" +
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
                                case "Left": button.Text.horizontalAlignment = HorizontalAlignmentOptions.Left; break;
                                case "Center": button.Text.horizontalAlignment = HorizontalAlignmentOptions.Center; break;
                                case "Right": button.Text.horizontalAlignment = HorizontalAlignmentOptions.Right; break;
                                case "Justified": button.Text.horizontalAlignment = HorizontalAlignmentOptions.Justified; break;
                                case "Flush": button.Text.horizontalAlignment = HorizontalAlignmentOptions.Flush; break;
                                case "Geometry": button.Text.horizontalAlignment = HorizontalAlignmentOptions.Geometry; break;
                                default:
                                    MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ".alignHorizontal with an unsupported argument." +
                                        " Expected values are: Left, Center, Right, Justified, Flush, or Geometry");
                                    return false;
                            }
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ".alignHorizontal with an unsupported argument.  Argument should be a String containing" +
                                " one of the following values: Left, Center, Right, Justified, Flush, or Geometry");
                            return false;
                        }
                    #endregion
                    case "FontSize":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            button.Text.fontSize = (float)t.value;
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ").FontSize property with an argument that is not a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "FontSizeMin":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            button.Text.fontSizeMin = (float)t.value;
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ").FontStyle.FontSizeMin property with an argument that is not a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "FontSizeMax":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            button.Text.fontSizeMax = (float)t.value;
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ").FontStyle.FontSizeMax property with an argument that is not a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "AutoSize":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            button.Text.autoSizeTextContainer = t.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ").FontStyle.AutoSize property with an argument that is not a boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    default:
                        if (b is null) { MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ") with an unknown property label (" + s + ")."); }
                        else { MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ") with an unknown property label (" + s + ") with an argument that appears to be of type " + b.GetType().ToString()); }
                        return false;
                }
            }
            else
            {
                if (a == null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ") with a property label accessor that is null.");
                }
                else if (a != null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify MUUI_ButtonAnimated(" + button.gameObject.name + ") with a property label accessor that is not a String. It appears to be a (" + a.GetType().Name + ").");
                }

                return false;
            }
        }

        public static void SetupButton(this MUUIButtonAnimated button, ref DataRow row, ref DataRow[] sprites)
        {
            button.ScriptOnEnter = (string)row["OnPointerEnterHandler"];
            button.ScriptOnExit = (string)row["OnPointerExitHandler"];
            button.ScriptOnLeftClick = (string)row["OnPointerLeftClickHandler"];
            button.ScriptOnDoubleLeftClick = (string)row["OnPointerDoubleLeftClickHandler"];
            button.ScriptOnRightClick = (string)row["OnPointerRightClickHandler"];
            button.ScriptOnDoubleRightClick = (string)row["OnPointerDoubleRightClickHandler"];
            button.ScriptOnMiddleClick = (string)row["OnPointerMiddleClickHandler"];

            if (((string)row["Sprite"]).Length > 0)
            {
                byte[] barr = System.Convert.FromBase64String((string)row["Sprite"]);
                Texture2D tex = new Texture2D(100, 100);
                tex.LoadImage(barr);
                var sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0),
                    100, 0, SpriteMeshType.FullRect);
                button.image.sprite = sp;
            }
            else
            {
                foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                {
                    if (sprite.name == "UISprite")
                        button.image.sprite = sprite;
                }
            }

            button.image.color = new Color(
                float.Parse((string)row["ColorR"]),
                float.Parse((string)row["ColorG"]),
                float.Parse((string)row["ColorB"]),
                float.Parse((string)row["ColorA"]));
            button.image.maskable = (bool)row["Maskable"];
            button.image.preserveAspect = (bool)row["PreserveAspect"];
                        
            button.sprites = new List<Sprite>();

            if(sprites != null)
            {
                foreach(DataRow dr in sprites)
                {
                    byte[] barr = System.Convert.FromBase64String((string)dr["Sprite"]);
                    Texture2D tex = new Texture2D(100, 100);
                    tex.LoadImage(barr);
                    //Debug.Log(tex.width + "/" + tex.height);
                    var sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0),
                        100, 0, SpriteMeshType.FullRect);
                    button.sprites.Add(sp);
                }
            }

            button.currentFrame = (int)row["StartFrame"];
            button.cycleTime = (float)row["CycleTime"];
            button.DoAnimate = (bool)row["DoAnimate"];

            if (row["TextValue"].ToString().Length > 0) button.Text.SetText((string)row["TextValue"]);
            else { button.Text.enabled = false; }

            if ((bool)row["Bold"]) button.Text.fontStyle |= FontStyles.Bold;
            if ((bool)row["Italic"]) button.Text.fontStyle |= FontStyles.Italic;
            if ((bool)row["Underline"]) button.Text.fontStyle |= FontStyles.Underline;
            if ((bool)row["Strike"]) button.Text.fontStyle |= FontStyles.Strikethrough;
            if ((bool)row["Lowercase"]) button.Text.fontStyle |= FontStyles.LowerCase;
            if ((bool)row["Uppercase"]) button.Text.fontStyle |= FontStyles.UpperCase;
            if ((bool)row["SmallCaps"]) button.Text.fontStyle |= FontStyles.SmallCaps;

            button.Text.horizontalAlignment =
                (HorizontalAlignmentOptions)System.Enum.Parse(
                    typeof(HorizontalAlignmentOptions), (string)row["alignHorizontal"]);
            button.Text.verticalAlignment =
                (VerticalAlignmentOptions)System.Enum.Parse(
                    typeof(VerticalAlignmentOptions), (string)row["alignVertical"]);
            button.Text.fontSize = (int)row["FontSize"];
            button.Text.fontSizeMin = (int)row["FontSizeMin"];
            button.Text.fontSizeMax = (int)row["FontSizeMax"];
            button.Text.autoSizeTextContainer = (bool)row["AutoSize"];

            button.gameObject.SetActive((bool)row["enabled"]);            
            button.name = (string)row["name"];
        }
    }
}


