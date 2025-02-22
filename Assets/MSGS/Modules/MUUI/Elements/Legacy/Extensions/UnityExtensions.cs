using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using TMPro;
using MiniScript;
using System;

namespace MiniScript.MSGS.MUUI.Extensions
{
    public static class UnityExtensions
    {
        public static void SetupRect(this RectTransform rect, ref DataRow row)
        {
            rect.anchoredPosition = new Vector2(float.Parse(row["anchoredPositionX"].ToString()),
                float.Parse(row["anchoredPositionY"].ToString()));

            rect.localPosition = new Vector3(float.Parse(row["localpositionX"].ToString()),
                float.Parse(row["localpositionY"].ToString()),
                float.Parse(row["localpositionZ"].ToString()));

            rect.localScale = new Vector3(float.Parse(row["localscaleX"].ToString()),
                float.Parse(row["localscaleY"].ToString()),
                float.Parse(row["localscaleZ"].ToString()));

            rect.offsetMax = new Vector2(float.Parse(row["offsetMaxX"].ToString()),
                float.Parse(row["offsetMaxY"].ToString()));

            rect.offsetMin = new Vector2(float.Parse(row["offsetMinX"].ToString()),
                float.Parse(row["offsetMinY"].ToString()));

            rect.anchorMin = new Vector2(float.Parse(row["anchorMinX"].ToString()),
                float.Parse(row["anchorMinY"].ToString()));

            rect.anchorMax = new Vector2(float.Parse(row["anchorMaxX"].ToString()),
                float.Parse(row["anchorMaxY"].ToString()));

            rect.sizeDelta = new Vector2(float.Parse(row["sizeDeltaX"].ToString()),
                float.Parse(row["sizeDeltaY"].ToString()));

            rect.pivot = new Vector2(float.Parse(row["pivotX"].ToString()),
                float.Parse(row["pivotY"].ToString()));

            rect.gameObject.SetActive((bool)row["enabled"]);

        }
        public static bool UpdateRect(this RectTransform rect, Value a, Value b)
        {
            if (a is ValString)
            {
                string s = ((ValString)a).value;
                switch (s)
                {
                    case "anchoredPositionX":
                        #region
                        if (b is ValNumber)
                        {
                            rect.anchoredPosition = new Vector2(b.FloatValue(), rect.anchoredPosition.y);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").anchoredPositionX property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "anchoredPositionY":
                        #region
                        if (b is ValNumber)
                        {
                            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, b.FloatValue());
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").anchoredPositionY property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "anchorMinX":
                        #region
                        if (b is ValNumber)
                        {
                            rect.anchorMin = new Vector2(b.FloatValue(), rect.anchorMin.y);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").anchorMinX property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "anchorMinY":
                        #region
                        if (b is ValNumber)
                        {
                            rect.anchorMin = new Vector2(rect.anchorMin.x, b.FloatValue());
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").anchorMinY property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "anchorMaxX":
                        #region
                        if (b is ValNumber)
                        {
                            rect.anchorMin = new Vector2(b.FloatValue(), rect.anchorMax.y);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").anchorMaxX property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "anchorMaxY":
                        #region
                        if (b is ValNumber)
                        {
                            rect.anchorMin = new Vector2(rect.anchorMax.x, b.FloatValue());
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").anchorMaxY property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "sizeDeltaX":
                        #region
                        if (b is ValNumber)
                        {
                            rect.sizeDelta = new Vector2(b.FloatValue(), rect.sizeDelta.y);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").sizeDeltaX property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "sizeDeltaY":
                        #region
                        if (b is ValNumber)
                        {
                            rect.sizeDelta = new Vector2(rect.sizeDelta.x, b.FloatValue());
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").sizeDeltaY property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "sizeDeltaMagnitude":
                        #region
                        MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").sizeDelta.Magnitude property with an argument. This is a read-only property.");
                        return false;
                    #endregion
                    case "sizeDeltaSqrMagnitude":
                        #region
                        MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").sizeDelta.SqrMagnitude property with an argument. This is a read-only property.");
                        return false;
                    #endregion
                    case "pivotX":
                        #region
                        if (b is ValNumber)
                        {
                            rect.pivot = new Vector2(b.FloatValue(), rect.pivot.y);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").pivotX property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "pivotY":
                        #region
                        if (b is ValNumber)
                        {
                            rect.pivot = new Vector2(rect.pivot.x, b.FloatValue());
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").pivotY property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "offsetMaxX":
                        #region
                        if (b is ValNumber)
                        {
                            rect.offsetMax = new Vector2(b.FloatValue(), rect.offsetMax.y);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").offsetMaxX property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "offsetMaxY":
                        #region
                        if (b is ValNumber)
                        {
                            rect.offsetMax = new Vector2(rect.offsetMax.x, b.FloatValue());
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").offsetMaxY property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "offsetMinY":
                        #region
                        if (b is ValNumber)
                        {
                            rect.offsetMin = new Vector2(b.FloatValue(), rect.offsetMin.y);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").offsetMinX property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "offsetMinX":
                        #region
                        if (b is ValNumber)
                        {
                            rect.offsetMin = new Vector2(rect.offsetMin.x, b.FloatValue());
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").offsetMinY property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "left":
                        #region
                        MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").rect.left property with an argument. This is a read-only property.");
                        return false;
                    #endregion
                    case "top":
                        #region
                        MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").rect.top property with an argument. This is a read-only property.");
                        return false;
                    #endregion
                    case "right":
                        #region
                        MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").rect.right property with an argument. This is a read-only property.");
                        return false;
                    #endregion
                    case "bottom":
                        #region
                        MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").rect.bottom property with an argument. This is a read-only property.");
                        return false;
                    #endregion
                    case "height":
                        #region
                        if (b is ValNumber)
                        {
                            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, b.FloatValue());
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").height property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "width":
                        #region
                        if (b is ValNumber)
                        {
                            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, b.FloatValue());
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").width property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "positionX":
                        #region
                        if (b is ValNumber)
                        {
                            rect.localPosition = new Vector3(b.FloatValue(), rect.localPosition.y, rect.localPosition.z);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").localposition.X property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "positionY":
                        #region
                        if (b is ValNumber)
                        {
                            rect.localPosition = new Vector3(rect.localPosition.x, b.FloatValue(), rect.localPosition.z);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").localposition.Y property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "positionZ":
                        #region
                        if (b is ValNumber)
                        {
                            rect.localPosition = new Vector3(rect.localPosition.x, rect.localPosition.y, b.FloatValue());
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").localposition.Z property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "ScaleX":
                        #region
                        if (b is ValNumber)
                        {
                            rect.localScale = new Vector3(b.FloatValue(), rect.localScale.y, rect.localScale.z);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").localScale.X property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "ScaleY":
                        #region
                        if (b is ValNumber)
                        {
                            rect.localScale = new Vector3(rect.localScale.x, b.FloatValue(), rect.localScale.z);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").localScale.Y property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "ScaleZ":
                        #region
                        if (b is ValNumber)
                        {
                            rect.localScale = new Vector3(rect.localScale.x, rect.localScale.y, b.FloatValue());
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ").localScale.Z property with an argument that is not a Number. " +
                                "It appears to be a " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    default:
                        if (b is null) { MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ") with an unknown property label (" + s + ")."); }
                        else { MiniScriptSingleton.LogError("Attempt to modify RectTransform with an unknown property label (" + s + ") with an argument that appears to be of type " + b.GetType().ToString()); }
                        return false;
                }
            }
            else
            {
                if (a == null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ") with an property label accessor that is null.");
                }
                else if (a != null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify RectTransform(" + rect.gameObject.name + ") with an property label accessor that is not a String. It appears to be a (" + a.GetType().Name + ").");
                }

                return false;
            }
        }
        public static ValMap ToValMap(this RectTransform rect)
        {
            ValMap map = new ValMap();
            //map.map.Add(new ValString("name"), new ValString(rect.gameObject.name));
            map.map.Add(new ValString("anchoredPositionX"), new ValNumber(rect.anchoredPosition.x));
            map.map.Add(new ValString("anchoredPositionY"), new ValNumber(rect.anchoredPosition.y));
            map.map.Add(new ValString("anchorMinX"), new ValNumber(rect.anchorMin.x));
            map.map.Add(new ValString("anchorMinY"), new ValNumber(rect.anchorMin.y));
            map.map.Add(new ValString("anchorMaxX"), new ValNumber(rect.anchorMax.x));
            map.map.Add(new ValString("anchorMaxY"), new ValNumber(rect.anchorMax.y));
            map.map.Add(new ValString("sizeDeltaX"), new ValNumber(rect.sizeDelta.x));
            map.map.Add(new ValString("sizeDeltaY"), new ValNumber(rect.sizeDelta.y));
            map.map.Add(new ValString("sizeDeltaMagnitude"), new ValNumber(rect.sizeDelta.magnitude));
            map.map.Add(new ValString("sizeDeltaSqrMagnitude"), new ValNumber(rect.sizeDelta.sqrMagnitude));
            map.map.Add(new ValString("pivotX"), new ValNumber(rect.pivot.x));
            map.map.Add(new ValString("pivotY"), new ValNumber(rect.pivot.y));
            map.map.Add(new ValString("offsetMaxX"), new ValNumber(rect.offsetMax.x));
            map.map.Add(new ValString("offsetMaxY"), new ValNumber(rect.offsetMax.y));
            map.map.Add(new ValString("offsetMinX"), new ValNumber(rect.offsetMin.x));
            map.map.Add(new ValString("offsetMinY"), new ValNumber(rect.offsetMin.y));
            map.map.Add(new ValString("left"), new ValNumber(rect.rect.xMin));
            map.map.Add(new ValString("top"), new ValNumber(rect.rect.yMin));
            map.map.Add(new ValString("right"), new ValNumber(rect.rect.xMax));
            map.map.Add(new ValString("bottom"), new ValNumber(rect.rect.yMax));
            map.map.Add(new ValString("height"), new ValNumber(rect.rect.height));
            map.map.Add(new ValString("width"), new ValNumber(rect.rect.width));
            map.map.Add(new ValString("positionX"), new ValNumber(rect.localPosition.x));
            map.map.Add(new ValString("positionY"), new ValNumber(rect.localPosition.y));
            map.map.Add(new ValString("positionZ"), new ValNumber(rect.localPosition.z));
            map.map.Add(new ValString("ScaleX"), new ValNumber(rect.localScale.x));
            map.map.Add(new ValString("ScaleY"), new ValNumber(rect.localScale.y));
            map.map.Add(new ValString("ScaleZ"), new ValNumber(rect.localScale.z));

            return map;
        }

        public static Dictionary<string, float> ToDictionary(this RectTransform rect)
        {
            Dictionary<string, float> dict = new Dictionary<string, float>();
            dict.Add("anchoredPositionX", rect.anchoredPosition.x);
            dict.Add("anchoredPositionY", rect.anchoredPosition.y);
            dict.Add("anchorMinX", rect.anchorMin.x);
            dict.Add("anchorMinY", rect.anchorMin.y);
            dict.Add("anchorMaxX", rect.anchorMax.x);
            dict.Add("anchorMaxY", rect.anchorMax.y);
            dict.Add("sizeDeltaX", rect.sizeDelta.x);
            dict.Add("sizeDeltaY", rect.sizeDelta.y);
            dict.Add("sizeDeltaMagnitude", rect.sizeDelta.magnitude);
            dict.Add("sizeDeltaSqrMagnitude", rect.sizeDelta.sqrMagnitude);
            dict.Add("pivotX", rect.pivot.x);
            dict.Add("pivotY", rect.pivot.y);
            dict.Add("offsetMaxX", rect.offsetMax.x);
            dict.Add("offsetMaxY", rect.offsetMax.y);
            dict.Add("offsetMinX", rect.offsetMin.x);
            dict.Add("offsetMinY", rect.offsetMin.y);
            dict.Add("left", rect.rect.xMin);
            dict.Add("top", rect.rect.yMin);
            dict.Add("right", rect.rect.xMax);
            dict.Add("bottom", rect.rect.yMax);
            dict.Add("height", rect.rect.height);
            dict.Add("width", rect.rect.width);
            dict.Add("positionX", rect.localPosition.x);
            dict.Add("positionY", rect.localPosition.y);
            dict.Add("positionZ", rect.localPosition.z);
            dict.Add("ScaleX", rect.localScale.x);
            dict.Add("ScaleY", rect.localScale.y);
            dict.Add("ScaleZ", rect.localScale.z);

            return dict;
        }

        public static void FromDictionary(this RectTransform rect, ref Dictionary<string, float> dict)
        {
            rect.anchoredPosition = new Vector2(dict["anchoredPositionX"], dict["anchoredPositionY"]);
            rect.localPosition = new Vector3(dict["localpositionX"], dict["localpositionY"], dict["localpositionZ"]);
            rect.localScale = new Vector3(dict["localscaleX"], dict["localscaleY"], dict["localscaleZ"]);
            rect.offsetMax = new Vector2(dict["offsetMaxX"], dict["offsetMaxY"]);
            rect.offsetMin = new Vector2(dict["offsetMinX"], dict["offsetMinY"]);
            rect.anchorMin = new Vector2(dict["anchorMinX"], dict["anchorMinY"]);
            rect.anchorMax = new Vector2(dict["anchorMaxX"], dict["anchorMaxY"]);
            rect.sizeDelta = new Vector2(dict["sizeDeltaX"], dict["sizeDeltaY"]);            
            rect.pivot = new Vector2(dict["pivotX"], dict["pivotY"]);            
        }


        public static ValMap ToValMap(this VerticalLayoutGroup group)
        {
            ValMap rst = new ValMap();
            rst.map.Add(new ValString("layoutPaddingLeft"), new ValNumber(group.padding.left));
            rst.map.Add(new ValString("layoutPaddingRight"), new ValNumber(group.padding.right));
            rst.map.Add(new ValString("layoutPaddingTop"), new ValNumber(group.padding.top));
            rst.map.Add(new ValString("layoutPaddingBottom"), new ValNumber(group.padding.bottom));
            rst.map.Add(new ValString("layoutSpacing"), new ValNumber(group.spacing));
            rst.map.Add(new ValString("layoutReserveArrangement"), ValNumber.Truth(group.reverseArrangement));
            rst.map.Add(new ValString("layoutControlHeight"), ValNumber.Truth(group.childControlHeight));
            rst.map.Add(new ValString("layoutControlWidth"), ValNumber.Truth(group.childControlWidth));
            rst.map.Add(new ValString("layoutUseChildScaleHeight"), ValNumber.Truth(group.childScaleHeight));
            rst.map.Add(new ValString("layoutUseChildScaleWidth"), ValNumber.Truth(group.childScaleWidth));
            rst.map.Add(new ValString("layoutForceExpandHeight"), ValNumber.Truth(group.childForceExpandHeight));
            rst.map.Add(new ValString("layoutForceExpandWidth"), ValNumber.Truth(group.childForceExpandWidth));
            rst.map.Add(new ValString("layoutChildAlignment"), new ValString(group.childAlignment.ToString()));
            return rst;
        }
        public static bool UpdateLayout(this VerticalLayoutGroup group, Value a, Value b)
        {
            if (a is ValString)
            {
                string s = ((ValString)a).value;
                switch (s)
                {
                    case "layoutPaddingLeft":
                        #region
                        if (b is ValNumber)
                        {
                            group.padding.left = b.IntValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify VerticalLayoutGroup(" + group.gameObject.name + ").layoutPaddingLeft property with a non-numeric(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a numeric value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutPaddingRight":
                        #region
                        if (b is ValNumber)
                        {
                            group.padding.right = b.IntValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify VerticalLayoutGroup(" + group.gameObject.name + ").layoutPaddingRight property with a non-numeric(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a numeric value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutPaddingTop":
                        #region
                        if (b is ValNumber)
                        {
                            group.padding.top = b.IntValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify VerticalLayoutGroup(" + group.gameObject.name + ").layoutPaddingTop property with a non-numeric(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a numeric value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutPaddingBottom":
                        #region
                        if (b is ValNumber)
                        {
                            group.padding.bottom = b.IntValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify VerticalLayoutGroup(" + group.gameObject.name + ").layoutPaddingBottom property with a non-numeric(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a numeric value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutSpacing":
                        #region
                        if (b is ValNumber)
                        {
                            group.spacing = b.FloatValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify VerticalLayoutGroup(" + group.gameObject.name + ").layoutSpacing property with a non-numeric(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a numeric value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutReverseArrangement":
                        #region 
                        if (b is ValNumber)
                        {
                            group.reverseArrangement = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify VerticalLayoutGroup(" + group.gameObject.name + ").layoutReverseArrangement property with a non-boolean(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutControlHeight":
                        #region 
                        if (b is ValNumber)
                        {
                            group.childControlHeight = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify VerticalLayoutGroup(" + group.gameObject.name + ").layoutControlHeight property with a non-boolean(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutControlWidth":
                        #region 
                        if (b is ValNumber)
                        {
                            group.childControlWidth = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify VerticalLayoutGroup(" + group.gameObject.name + ").layoutControlWidth property with a non-boolean(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutUseChildScaleHeight":
                        #region 
                        if (b is ValNumber)
                        {
                            group.childScaleHeight = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify VerticalLayoutGroup(" + group.gameObject.name + ").layoutUseChildScaleHeight property with a non-boolean(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutUseChildScaleWidth":
                        #region 
                        if (b is ValNumber)
                        {
                            group.childScaleWidth = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify VerticalLayoutGroup(" + group.gameObject.name + ").layoutUseChildScaleWidth property with a non-boolean(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutForceExpandHeight":
                        #region 
                        if (b is ValNumber)
                        {
                            group.childForceExpandHeight = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify VerticalLayoutGroup(" + group.gameObject.name + ").layoutForceExpandHeight property with a non-boolean(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutForceExpandWidth":
                        #region 
                        if (b is ValNumber)
                        {
                            group.childForceExpandWidth = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify VerticalLayoutGroup(" + group.gameObject.name + ").layoutForceExpandWidth property with a non-boolean(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutChildAlignment":
                        #region
                        if (b is ValString)
                        {
                            try
                            {
                                TextAnchor mt = (TextAnchor)System.Enum.Parse(typeof(TextAnchor), ((ValString)b).value);
                                group.childAlignment = mt;
                                return true;
                            }
                            catch
                            {
                                MiniScriptSingleton.LogError("Attempt to modify VerticalLayoutGroup(" + group.gameObject.name + ").layoutChildAlignment property with an unsupported string parameter." +
                                                            " The expected argument should be a ValString containing a string value of : " +
                                                            " LowerCenter, LowerLeft, LowerRight, MiddleCenter, MiddleLeft, MiddleRight, UpperCenter, UpperLeft, UpperRight. " +
                                                            "The argument given contains (  " + ((ValString)b).value + " ) for its string value.");
                                return false;
                            }
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify VerticalLayoutGroup(" + group.gameObject.name + ").layoutChildAlignment property with a non-String(ValString) parameter." +
                                                            " The expected argument should be a ValString containing a string value of : " +
                                                            " LowerCenter, LowerLeft, LowerRight, MiddleCenter, MiddleLeft, MiddleRight, UpperCenter, UpperLeft, UpperRight. " +
                                                            "The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    default:
                        if (b is null) { MiniScriptSingleton.LogError("Attempt to modify VerticalLayoutGroup(" + group.gameObject.name + ") with an unknown property label (" + s + ")."); }
                        else { MiniScriptSingleton.LogError("Attempt to modify VerticalLayoutGroup(" + group.gameObject.name + ") with an unknown property label (" + s + ") with an argument that appears to be of type " + b.GetType().ToString()); }
                        return false;
                }
            }
            else
            {
                if (a == null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify VerticalLayoutGroup(" + group.gameObject.name + ") with an property label accessor that is null.");
                }
                else if (a != null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify VerticalLayoutGroup(" + group.gameObject.name + ") with an property label accessor that is not a String. It appears to be a (" + a.GetType().Name + ").");
                }

                return false;
            }

        }
        public static void SetupVerticalLayoutGroup(this VerticalLayoutGroup group, ref DataRow row)
        {
            throw new NotImplementedException();
        }


        public static ValMap ToValMap(this ContentSizeFitter fitter)
        {
            ValMap rst = new ValMap();
            rst.map.Add(new ValString("sizeFitterHorizontal"), new ValString(fitter.horizontalFit.ToString()));
            rst.map.Add(new ValString("sizeFitterVertical"), new ValString(fitter.verticalFit.ToString()));
            return rst;
        }
        public static bool UpdateContentFitter(this ContentSizeFitter fitter, Value a, Value b)
        {
            if (a is ValString)
            {
                string s = ((ValString)a).value;
                switch (s)
                {
                    case "sizeFitterHorizontal":
                        #region
                        if (b is ValString)
                        {
                            try
                            {
                                fitter.horizontalFit = (ContentSizeFitter.FitMode)System.Enum.Parse(
                                    typeof(ContentSizeFitter.FitMode), ((ValString)b).value);
                                return true;
                            }
                            catch
                            {
                                MiniScriptSingleton.LogError("Attempt to modify ContentSizeFitter(" + fitter.gameObject.name + ").horizontalFit property with a parameter of unexpected value." +
                                                           " The expected argument should be a ValString containing the string values: Unconstrained, MinSize, or PreferredSize ");
                                return false;
                            }
                        }
                        else
                        {

                            MiniScriptSingleton.LogError("Attempt to modify ContentSizeFitter(" + fitter.gameObject.name + ").verticalFit property with a parameter of unexpected value." +
                                                       " The expected argument should be a ValString containing the string values: Unconstrained, MinSize, or PreferredSize ");
                            return false;
                        }
                    #endregion
                    case "sizeFitterVertical":
                        #region
                        if (b is ValString)
                        {
                            try
                            {
                                fitter.verticalFit = (ContentSizeFitter.FitMode)System.Enum.Parse(
                                    typeof(ContentSizeFitter.FitMode), ((ValString)b).value);
                                return true;
                            }
                            catch
                            {
                                MiniScriptSingleton.LogError("Attempt to modify ContentSizeFitter(" + fitter.gameObject.name + ").verticalFit property with a parameter of unexpected value." +
                                                           " The expected argument should be a ValString containing the string values: Unconstrained, MinSize, or PreferredSize ");
                                return false;
                            }
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify ContentSizeFitter(" + fitter.gameObject.name + ").verticalFit property with a parameter of unexpected value." +
                                                       " The expected argument should be a ValString containing the string values: Unconstrained, MinSize, or PreferredSize ");
                            return false;
                        }
                    #endregion
                    default:
                        if (b is null) { MiniScriptSingleton.LogError("Attempt to modify ContentSizeFitter(" + fitter.gameObject.name + ") with an unknown property label (" + s + ")."); }
                        else { MiniScriptSingleton.LogError("Attempt to modify ContentSizeFitter with an unknown property label (" + s + ") with an argument that appears to be of type " + b.GetType().ToString()); }
                        return false;
                }
            }
            else
            {
                if (a == null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify ContentSizeFitter(" + fitter.gameObject.name + ") with an property label accessor that is null.");
                }
                else if (a != null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify ContentSizeFitter(" + fitter.gameObject.name + ") with an property label accessor that is not a String. It appears to be a (" + a.GetType().Name + ").");
                }

                return false;

            }
        }
        public static void SetupContentFitter(this ContentSizeFitter fitter, ref DataRow row)
        {
            throw new NotImplementedException();
        }

    }
}