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
    public static class DropDownExtensions 
    {
        public static ValMap ToValMap(this MUUIDropDown drop)
        {
            ValMap rst = new ValMap();
            rst.map.Add(new ValString("InstanceID"), new ValNumber(drop.gameObject.GetInstanceID()));
            rst.map.Add(new ValString("enabled"), ValNumber.Truth(drop.enabled));
            rst.map.Add(new ValString("name"), new ValString(drop.name));
            //rst.map.Add(new ValString("ContentPrefab"), new ValString(drop.ContentPrefab));
            //rst.map.Add(new ValString("MaskingSprite"), new ValString(string.Empty));
            //rst.map.Add(new ValString("OnPointerEnterHandler"), new ValString(drop.ScriptOnEnter));
            //rst.map.Add(new ValString("OnPointerExitHandler"), new ValString(drop.ScriptOnEnter));
            //rst.map.Add(new ValString("OnPointerLeftClickHandler"), new ValString(drop.ScriptOnLeftClick));
            //rst.map.Add(new ValString("OnPointerDoubleLeftClickHandler"), new ValString(drop.ScriptOnDoubleLeftClick));
            //rst.map.Add(new ValString("OnPointerDoubleRightClickHandler"), new ValString(drop.ScriptOnDoubleRightClick));
            //rst.map.Add(new ValString("OnPointerRightClickHandler"), new ValString(drop.ScriptOnRightClick));
            //rst.map.Add(new ValString("OnPointerMiddleClickHandler"), new ValString(drop.ScriptOnMiddleClick));

            //default Text properties of the dropdown control
            rst.map.Add(new ValString("Bold"), ValNumber.Truth((drop.defaultText.fontStyle & FontStyles.Bold) != 0));
            rst.map.Add(new ValString("Italic"), ValNumber.Truth((drop.defaultText.fontStyle & FontStyles.Italic) != 0));
            rst.map.Add(new ValString("Underline"), ValNumber.Truth((drop.defaultText.fontStyle & FontStyles.Underline) != 0));
            rst.map.Add(new ValString("Strike"), ValNumber.Truth((drop.defaultText.fontStyle & FontStyles.Strikethrough) != 0));
            rst.map.Add(new ValString("Lowercase"), ValNumber.Truth((drop.defaultText.fontStyle & FontStyles.LowerCase) != 0));
            rst.map.Add(new ValString("Uppercase"), ValNumber.Truth((drop.defaultText.fontStyle & FontStyles.UpperCase) != 0));
            rst.map.Add(new ValString("SmallCaps"), ValNumber.Truth((drop.defaultText.fontStyle & FontStyles.SmallCaps) != 0));

            rst.map.Add(new ValString("alignHorizontal"), new ValString(drop.defaultText.horizontalAlignment.ToString()));
            rst.map.Add(new ValString("alignVertical"), new ValString(drop.defaultText.verticalAlignment.ToString()));

            rst.map.Add(new ValString("FontSize"), new ValNumber(drop.defaultText.fontSize));
            rst.map.Add(new ValString("FontSizeMin"), new ValNumber(drop.defaultText.fontSizeMin));
            rst.map.Add(new ValString("FontSizeMax"), new ValNumber(drop.defaultText.fontSizeMax));
            rst.map.Add(new ValString("AutoSize"), ValNumber.Truth(drop.defaultText.autoSizeTextContainer));
            rst.map.Add(new ValString("ColorR"), new ValNumber(drop.defaultText.color.r));
            rst.map.Add(new ValString("ColorG"), new ValNumber(drop.defaultText.color.g));
            rst.map.Add(new ValString("ColorB"), new ValNumber(drop.defaultText.color.b));
            rst.map.Add(new ValString("ColorA"), new ValNumber(drop.defaultText.color.a));

            //rst.map.Add(new ValString("AssignItems"), new ValMap());
            return rst;
        }

        public static bool UpdateDropDown(this MUUIDropDown drop, Value a, Value b)
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
                            drop.enabled = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify the script for MUUI_DropDown(" + drop.gameObject.name + ").enabled property with a non-boolean(ValNumber) parameter." +
                                                            " The expected argument should be a ValNumber containing the bool value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "name":
                        #region
                        if (b is ValString)
                        {
                            drop.gameObject.name = ((ValString)b).value;
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_DropDown(" + drop.gameObject.name + ").name property with a non-String(ValString) parameter." +
                                                            " The expected argument should be a ValString containing the string literal value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    #region fluff
                    //case "OnPointerEnterHandler":
                    //    #region
                    //    if (b is ValString)
                    //    {
                    //        if (MiniScriptSingleton.ScriptExists(((ValString)b).value))
                    //        {
                    //            drop.ScriptOnEnter = ((ValString)b).value;
                    //            return true;
                    //        }
                    //        else
                    //        {   //script does not exist within the mod data
                    //            MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_DropDown(" + drop.gameObject.name + ").OnPointerEnterHandler property failed because the specified script(" +
                    //                ((ValString)b).value + " could not be found.");
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_DropDown(" + drop.gameObject.name + ").OnPointerEnterHandler property with a non-String parameter." +
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
                    //            drop.ScriptOnExit = ((ValString)b).value;
                    //            return true;
                    //        }
                    //        else
                    //        {   //script does not exist within the mod data
                    //            MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_DropDown(" + drop.gameObject.name + ").OnPointerExitHandler property failed because the specified script(" +
                    //                ((ValString)b).value + " could not be found.");
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_DropDown(" + drop.gameObject.name + ").OnPointerExitHandler property with a non-String parameter." +
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
                    //            drop.ScriptOnLeftClick = ((ValString)b).value;
                    //            return true;
                    //        }
                    //        else
                    //        {   //script does not exist within the mod data
                    //            MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_DropDown(" + drop.gameObject.name + ").OnPointerClickHandler property failed because the specified script(" +
                    //                ((ValString)b).value + " could not be found.");
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_DropDown(" + drop.gameObject.name + ").OnPointerClickHandler property with a non-String parameter." +
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
                    //            drop.ScriptOnDoubleLeftClick = ((ValString)b).value;
                    //            return true;
                    //        }
                    //        else
                    //        {   //script does not exist within the mod data
                    //            MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_DropDown(" + drop.gameObject.name + ").OnPointerDoubleClickHandler property failed because the specified script(" +
                    //                ((ValString)b).value + " could not be found.");
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_DropDown(" + drop.gameObject.name + ").OnPointerDoubleClickHandler property with a non-String parameter." +
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
                    //            drop.ScriptOnDoubleRightClick = ((ValString)b).value;
                    //            return true;
                    //        }
                    //        else
                    //        {   //script does not exist within the mod data
                    //            MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_DropDown(" + drop.gameObject.name + ").OnPointerDoubleClickHandler property failed because the specified script(" +
                    //                ((ValString)b).value + " could not be found.");
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_DropDown(" + drop.gameObject.name + ").OnPointerDoubleClickHandler property with a non-String parameter." +
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
                    //            drop.ScriptOnRightClick = ((ValString)b).value;
                    //            return true;
                    //        }
                    //        else
                    //        {   //script does not exist within the mod data
                    //            MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_DropDown(" + drop.gameObject.name + ").OnPointerRightClickHandler property failed because the specified script(" +
                    //                ((ValString)b).value + " could not be found.");
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_DropDown(" + drop.gameObject.name + ").OnPointerRightClickHandler property with a non-String parameter." +
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
                    //            drop.ScriptOnMiddleClick = ((ValString)b).value;
                    //            return true;
                    //        }
                    //        else
                    //        {   //script does not exist within the mod data
                    //            MiniScriptSingleton.LogWarning("Assigning a new script to MUUI_DropDown(" + drop.gameObject.name + ").OnPointerMiddleClickHandler property failed because the specified script(" +
                    //                ((ValString)b).value + " could not be found.");
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_DropDown(" + drop.gameObject.name + ").OnPointerMiddleClickHandler property with a non-String parameter." +
                    //            " The expected argument should be a string containing the script name. The argument given appears to be of " + b.GetType().Name + " type.");
                    //        return false;
                    //    }
                    //#endregion      
                    //#endregion
                    //case "MaskingSprite":
                    //    #region
                    //    if (b is ValString)
                    //    {
                    //        //the string value given should link to a valid SpriteAtlas object
                    //        if (SpriteSheetCollection.ContainsAssetPath(((ValString)b).value))
                    //        {
                    //            drop.image.sprite = SpriteSheetCollection.GetSprite(((ValString)b).value);
                    //            return true;
                    //        }
                    //        else
                    //        {
                    //            MiniScriptSingleton.LogWarning("Sprite Sheet Path given for MUUI_DropDown(" + drop.gameObject.name + ").MaskingSprite was not found." +
                    //                " Asset Path value given was: " + ((ValString)b).value);
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.gameObject.name + ").MaskingSprite with a property value that is not a String. " +
                    //            " Argument given appears to be of type " + b.GetType().Name);
                    //        return false;
                    //    }                    
                    //case "AssignItems":
                    //    #region
                    //    if (b is ValMap)
                    //    {
                    //        //on assignment to AssignItems we expect a ValMap to be given containing ValString/ValString pairs
                    //        //the Key is the Sprite path, the Value is the Text label
                    //        ValMap vm = (ValMap)b;

                    //        if (vm.Count >= 0)
                    //        {
                    //            drop.dropdown.ClearOptions();
                    //            TMP_Dropdown.OptionDataList list = new TMP_Dropdown.OptionDataList();
                    //            foreach (KeyValuePair<Value, Value> kv in vm.map)
                    //            {
                    //                if (kv.Key is ValString && kv.Value is ValString)
                    //                {
                    //                    //for each keyvaluepair we only really care that a text value is given, the sprite path is optional data
                    //                    string key = ((ValString)kv.Key).value;
                    //                    string value = ((ValString)kv.Value).value;

                    //                    if (value.Length > 0)
                    //                    {
                    //                        //if (SpriteSheetCollection.ContainsAssetPath(key))
                    //                        //{
                    //                        //    //add the option element to the dropdown with the sprite
                    //                        //    drop.dropdown.options.Add(new TMP_Dropdown.OptionData(((ValString)kv.Value).value,
                    //                        //        SpriteSheetCollection.GetSprite(key)));
                    //                        //    return true;
                    //                        //}
                    //                        //else
                    //                        //{
                    //                        //    //add the option element to the dropdown without the sprite
                    //                        //    drop.dropdown.options.Add(new TMP_Dropdown.OptionData(((ValString)kv.Value).value));
                    //                        //    return true;
                    //                        //}
                    //                    }
                    //                    else if (value.Length == 0)
                    //                    {
                    //                        //the text property of the keyvaluepair was zero length
                    //                        MiniScriptSingleton.LogWarning("Attempt to assign items to MUUI_DropDown(" + drop.gameObject.name + ").AssignItems with a KeyValuePair where the Value argument was a zero length string.");
                    //                        return false;
                    //                    }
                    //                }
                    //                else
                    //                {   //either or both arguments are not ValString as expected
                    //                    MiniScriptSingleton.LogWarning("Attempt to assign items to MUUI_DropDown(" + drop.gameObject.name + ").AssignItems with argument(s) that do not match the expected." +
                    //                        "Key={" + kv.Key.ToString() + "} Value={" + kv.Value.ToString() + "}  Expected arguments should be of ValString/Key and ValString/Value pairs.");
                    //                }
                    //            }
                    //            return true;
                    //        }
                    //        else
                    //        {
                    //            MiniScriptSingleton.LogWarning("Attempt to assign items to MUUI_DropDown(" + drop.gameObject.name + ").AssignItems with no elements given as arguments: ValMap was empty.");
                    //            return false;
                    //        }
                    //    }
                    //    else if (b is ValList)
                    //    {
                    //        //list[] = [0,0,0,0,0,0]
                    //        //valmap m = ["x","w" : "a","b"]
                    //        return true;
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogWarning("Attempt to assign items to MUUI_DropDown(" + drop.gameObject.name + ").AssignItems with an argument was not the expected ValMap. "
                    //            + "Argument given appears to be of " + b.GetType().Name + " type.");
                    //        return false;
                    //    }
                    //#endregion
                    #endregion
                    case "Bold":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((drop.defaultText.fontStyle & FontStyles.Bold) != 0);
                            if (t.BoolValue())
                            {
                                //if its not set, set the flag to true
                                if (!isSet) { drop.defaultText.fontStyle |= FontStyles.Bold; }
                            }
                            else if (!t.BoolValue())
                            {
                                //if the flag is true, set to false
                                if (isSet) { drop.defaultText.fontStyle ^= FontStyles.Bold; }
                                //else the flag is already false, so do nothing
                            }
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown.Text(" + drop.defaultText.gameObject.name + ").FontStyle.Bold property with an argument that is neither a String nor a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "Italic":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((drop.defaultText.fontStyle & FontStyles.Italic) != 0);

                            if (t.BoolValue())
                            {
                                if (!isSet) { drop.defaultText.fontStyle |= FontStyles.Italic; }
                            }
                            else if (!t.BoolValue())
                            {
                                if (isSet) { drop.defaultText.fontStyle ^= FontStyles.Italic; }
                            }
                            return true;

                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.defaultText.gameObject.name + ").FontStyle.Italic property with an argument that is not boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "Underline":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((drop.defaultText.fontStyle & FontStyles.Underline) != 0);

                            if (t.BoolValue())
                            {
                                if (!isSet) { drop.defaultText.fontStyle |= FontStyles.Underline; }
                            }
                            else if (!t.BoolValue())
                            {
                                if (isSet) { drop.defaultText.fontStyle ^= FontStyles.Underline; }
                            }
                            return true;

                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.defaultText.gameObject.name + ").FontStyle.Underline property with an argument that is not boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "Strike":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((drop.defaultText.fontStyle & FontStyles.Strikethrough) != 0);

                            if (t.BoolValue())
                            {
                                if (!isSet) { drop.defaultText.fontStyle |= FontStyles.Strikethrough; }
                            }
                            else if (!t.BoolValue())
                            {
                                if (isSet) { drop.defaultText.fontStyle ^= FontStyles.Strikethrough; }
                            }
                            return true;

                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.defaultText.gameObject.name + ").FontStyle.Strike property with an argument that is not boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "Lowercase":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((drop.defaultText.fontStyle & FontStyles.LowerCase) != 0);

                            if (t.BoolValue())
                            {
                                if (!isSet) { drop.defaultText.fontStyle |= FontStyles.LowerCase; }
                            }
                            else if (!t.BoolValue())
                            {
                                if (isSet) { drop.defaultText.fontStyle ^= FontStyles.LowerCase; }
                            }
                            return true;

                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.defaultText.gameObject.name + ").FontStyle.Lowercase property with an argument that is not boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "Uppercase":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((drop.defaultText.fontStyle & FontStyles.UpperCase) != 0);

                            if (t.BoolValue())
                            {
                                if (!isSet) { drop.defaultText.fontStyle |= FontStyles.UpperCase; }
                            }
                            else if (!t.BoolValue())
                            {
                                if (isSet) { drop.defaultText.fontStyle ^= FontStyles.UpperCase; }
                            }
                            return true;

                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.defaultText.gameObject.name + ").FontStyle.Uppercase property with an argument that is not boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "SmallCaps":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            bool isSet = ((drop.defaultText.fontStyle & FontStyles.SmallCaps) != 0);

                            if (t.BoolValue())
                            {
                                if (!isSet) { drop.defaultText.fontStyle |= FontStyles.SmallCaps; }
                            }
                            else if (!t.BoolValue())
                            {
                                if (isSet) { drop.defaultText.fontStyle ^= FontStyles.SmallCaps; }
                            }
                            return true;

                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.defaultText.gameObject.name + ").FontStyle.SmallCaps property with an argument that is not boolean, no change made. " +
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
                                case "Top": drop.defaultText.verticalAlignment = VerticalAlignmentOptions.Top; break;
                                case "Middle": drop.defaultText.verticalAlignment = VerticalAlignmentOptions.Middle; break;
                                case "Bottom": drop.defaultText.verticalAlignment = VerticalAlignmentOptions.Bottom; break;
                                case "Baseline": drop.defaultText.verticalAlignment = VerticalAlignmentOptions.Baseline; break;
                                case "Geometry": drop.defaultText.verticalAlignment = VerticalAlignmentOptions.Geometry; break;
                                case "Capline": drop.defaultText.verticalAlignment = VerticalAlignmentOptions.Capline; break;
                                default:
                                    MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.defaultText.gameObject.name + ".alignVertical with an unsupported argument." +
                                        " Expected values are: Top, Middle, Bottom, Baseline, Geometry, or Capline");
                                    return false;
                            }
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.defaultText.gameObject.name + ".alignVertical with an unsupported argument.  Argument should be a String containing" +
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
                                case "Left": drop.defaultText.horizontalAlignment = HorizontalAlignmentOptions.Left; break;
                                case "Center": drop.defaultText.horizontalAlignment = HorizontalAlignmentOptions.Center; break;
                                case "Right": drop.defaultText.horizontalAlignment = HorizontalAlignmentOptions.Right; break;
                                case "Justified": drop.defaultText.horizontalAlignment = HorizontalAlignmentOptions.Justified; break;
                                case "Flush": drop.defaultText.horizontalAlignment = HorizontalAlignmentOptions.Flush; break;
                                case "Geometry": drop.defaultText.horizontalAlignment = HorizontalAlignmentOptions.Geometry; break;
                                default:
                                    MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.defaultText.gameObject.name + ".alignHorizontal with an unsupported argument." +
                                        " Expected values are: Left, Center, Right, Justified, Flush, or Geometry");
                                    return false;
                            }
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.defaultText.gameObject.name + ".alignHorizontal with an unsupported argument.  Argument should be a String containing" +
                                " one of the following values: Left, Center, Right, Justified, Flush, or Geometry");
                            return false;
                        }
                    #endregion
                    case "FontSize":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            drop.defaultText.fontSize = (float)t.value;
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.defaultText.gameObject.name + ").FontSize property with an argument that is not a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "FontSizeMin":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            drop.defaultText.fontSizeMin = (float)t.value;
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.defaultText.gameObject.name + ").FontStyle.FontSizeMin property with an argument that is not a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "FontSizeMax":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            drop.defaultText.fontSizeMax = (float)t.value;
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.defaultText.gameObject.name + ").FontStyle.FontSizeMax property with an argument that is not a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "AutoSize":
                        #region
                        if (b is ValNumber)
                        {
                            ValNumber t = (ValNumber)b;
                            drop.defaultText.autoSizeTextContainer = t.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.defaultText.gameObject.name + ").FontStyle.AutoSize property with an argument that is not a boolean, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "ColorR":
                        #region
                        if (b is ValNumber)
                        {
                            drop.defaultText.color = new Color(b.FloatValue(), drop.defaultText.color.g, drop.defaultText.color.b, drop.defaultText.color.a);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.defaultText.gameObject.name + ").Color.R property with an argument that is not a Number, no change made. " +
                                 " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "ColorG":
                        #region
                        if (b is ValNumber)
                        {
                            drop.defaultText.color = new Color(drop.defaultText.color.r, b.FloatValue(), drop.defaultText.color.b, drop.defaultText.color.a);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.defaultText.gameObject.name + ").Color.G property with an argument that is not a Number, no change made. " +
                                " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "ColorB":
                        #region
                        if (b is ValNumber)
                        {
                            drop.defaultText.color = new Color(drop.defaultText.color.r, drop.defaultText.color.g, b.FloatValue(), drop.defaultText.color.a);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.defaultText.gameObject.name + ").Color.B property with an argument that is not a Number, no change made. " +
                                " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    case "ColorA":
                        #region
                        if (b is ValNumber)
                        {
                            drop.defaultText.color = new Color(drop.defaultText.color.r, drop.defaultText.color.g, drop.defaultText.color.b, b.FloatValue());
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.defaultText.gameObject.name + ").Color.A property with an argument that is not a Number, no change made. " +
                                " Argument appears to be of type " + b.GetType().ToString());
                            return false;
                        }
                    #endregion
                    default:
                        if (b is null) { MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.gameObject.name + ") with an unknown property label (" + s + ")."); }
                        else { MiniScriptSingleton.LogError("Attempt to modify MUUI_DropDown(" + drop.gameObject.name + ") with an unknown property label (" + s + ") with an argument that appears to be of type " + b.GetType().ToString()); }
                        return false;
                }
            }
            else
            {

                return false;
            }
        }

        public static void SetupDropDown(this MUUIDropDown drop, ref DataRow row)
        {
            //drop.defaultText.SetText((string)row["TextValue"]);
            drop.defaultText.fontSize = (int)row["FontSize"];
            drop.defaultText.fontSizeMin = (int)row["FontSizeMin"];
            drop.defaultText.fontSizeMax = (int)row["FontSizeMax"];
            drop.defaultText.autoSizeTextContainer = (bool)row["AutoSize"];
            drop.defaultText.color = new Color(
                float.Parse((string)row["TextColorR"]),
                float.Parse((string)row["TextColorG"]),
                float.Parse((string)row["TextColorB"]),
                float.Parse((string)row["TextColorA"]));
            drop.defaultText.horizontalAlignment =
                (HorizontalAlignmentOptions)System.Enum.Parse(
                    typeof(HorizontalAlignmentOptions), (string)row["alignHorizontal"]);
            drop.defaultText.verticalAlignment =
                (VerticalAlignmentOptions)System.Enum.Parse(
                    typeof(VerticalAlignmentOptions), (string)row["alignVertical"]);

            if ((bool)row["Bold"]) drop.defaultText.fontStyle |= FontStyles.Bold;
            if ((bool)row["Italic"]) drop.defaultText.fontStyle |= FontStyles.Italic;
            if ((bool)row["Underline"]) drop.defaultText.fontStyle |= FontStyles.Underline;
            if ((bool)row["Strike"]) drop.defaultText.fontStyle |= FontStyles.Strikethrough;
            if ((bool)row["Lowercase"]) drop.defaultText.fontStyle |= FontStyles.LowerCase;
            if ((bool)row["Uppercase"]) drop.defaultText.fontStyle |= FontStyles.UpperCase;
            if ((bool)row["SmallCaps"]) drop.defaultText.fontStyle |= FontStyles.SmallCaps;

            drop.name = (string)row["name"];
            drop.gameObject.SetActive((bool)row["enabled"]);            
        }
    }
}
