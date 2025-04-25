using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using TMPro;
using MiniScript;
using System;
using UnityEngine.EventSystems;

#pragma warning disable IDE0090
#pragma warning disable IDE0038

namespace MiniScript.MSGS.Unity
{
    public static class UnityTypeExtensions
    {
        public static ValMap ToValMap(this Dictionary<int, List<ValMap>> dict)
        {
            throw new NotImplementedException();
        }
        public static bool Contains(this List<GameObject> lst, GameObject go)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].GetInstanceID() == go.GetInstanceID()) { return true; }
            }
            return false;
        }

        #region Rect
        public static ValMap ToValMap(this Rect r)
        {
            ValMap map = new ValMap();
            map.map.Add(new ValString("left"), new ValNumber(r.xMin));
            map.map.Add(new ValString("top"), new ValNumber(r.yMin));
            map.map.Add(new ValString("right"), new ValNumber(r.xMax));
            map.map.Add(new ValString("bottom"), new ValNumber(r.yMax));
            map.map.Add(new ValString("height"), new ValNumber(r.height));
            map.map.Add(new ValString("width"), new ValNumber(r.width));

            map.map.Add(new ValString("x"), new ValNumber(r.x));
            map.map.Add(new ValString("y"), new ValNumber(r.y));
            map.map.Add(new ValString("center"), r.center.ToValMap());
            map.map.Add(new ValString("position"), r.position.ToValMap());
            map.map.Add(new ValString("size"), r.size.ToValMap());
            return map;
        }

        public static Rect FromValMap(this ValMap map)
        {
            Rect r = new Rect();
            r.xMin = map.map[new ValString("left")].FloatValue();
            r.xMin = map.map[new ValString("top")].FloatValue();
            r.xMin = map.map[new ValString("right")].FloatValue();
            r.xMin = map.map[new ValString("bottom")].FloatValue();
            r.xMin = map.map[new ValString("height")].FloatValue();
            r.xMin = map.map[new ValString("width")].FloatValue();

            r.xMin = map.map[new ValString("x")].FloatValue();
            r.xMin = map.map[new ValString("y")].FloatValue();
            var vm = (ValMap)map.map[new ValString("center")];
            r.center = new Vector2().FromValMap(ref vm);
            vm = (ValMap)map.map[new ValString("position")];
            r.position = new Vector2().FromValMap(ref vm);
            vm = (ValMap)map.map[new ValString("size")];
            r.size = new Vector2().FromValMap(ref vm);
            return r;
        }


        #endregion

        #region RectTransform
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
        #endregion

        #region VerticalLayoutGroup
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
        #endregion

        #region ContentSizeFitter
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
        #endregion

        #region Color
        public static ValList ToValList(this UnityEngine.Color color)
        {
            ValList rst = new ValList();
            rst.values.Add(new ValNumber(color.r));
            rst.values.Add(new ValNumber(color.g));
            rst.values.Add(new ValNumber(color.b));
            rst.values.Add(new ValNumber(color.a));
            return rst;
        }
        public static Color ToColor(this ValList map)
        {
            Color color = new Color();
            if (map.values.Count == 3)
            {
                color.r = map.values[0].FloatValue();
                color.g = map.values[1].FloatValue();
                color.b = map.values[2].FloatValue();
            }
            else if (map.values.Count == 4)
            {
                color.r = map.values[0].FloatValue();
                color.g = map.values[1].FloatValue();
                color.b = map.values[2].FloatValue();
                color.a = map.values[3].FloatValue();
            }
            else
            {
                Debug.Log("UnityEngine.Color.FromValList: extension method expected 3 or 4 values and was given " + map.values.Count + " instead.");
            }

            return color;
        }
        #endregion

        #region Vectors
        public static ValMap ToValMap(this Vector2 vec)
        {
            ValMap map = new ValMap();
            map.map.Add(new ValString("x"), new ValNumber(vec.x));
            map.map.Add(new ValString("y"), new ValNumber(vec.y));
            return map;
        }
        public static Vector2 FromValMap(this Vector2 vec, ref ValMap map)
        {
            return new Vector2(
                map.map[new ValString("x")].FloatValue(),
                map.map[new ValString("y")].FloatValue()
                );
        }
        public static ValList ToValList(this Vector2 vec)
        {
            ValList list = new ValList();
            list.values.Add(new ValNumber(vec.x));
            list.values.Add(new ValNumber(vec.y));
            return list;
        }
        public static Vector2 FromValList(this Vector2 vec, ref ValList list)
        {
            return new Vector2(list.values[0].FloatValue(), list.values[1].FloatValue());
        }

        public static ValMap ToValMap(this Vector2Int vec)
        {
            ValMap map = new ValMap();
            map.map.Add(new ValString("x"), new ValNumber(vec.x));
            map.map.Add(new ValString("y"), new ValNumber(vec.y));
            return map;
        }
        public static Vector2Int FromValMap(this Vector2Int vec, ref ValMap map)
        {
            return new Vector2Int(
                map.map[new ValString("x")].IntValue(),
                map.map[new ValString("y")].IntValue()
                );
        }
        public static ValList ToValList(this Vector2Int vec)
        {
            ValList list = new ValList();
            list.values.Add(new ValNumber(vec.x));
            list.values.Add(new ValNumber(vec.y));
            return list;
        }
        public static Vector2Int FromValList(this Vector2Int vec, ref ValList list)
        {
            return new Vector2Int(list.values[0].IntValue(), list.values[1].IntValue());
        }

        public static ValMap ToValMap(this Vector3 vec)
        {
            ValMap map = new ValMap();
            map.map.Add(new ValString("x"), new ValNumber(vec.x));
            map.map.Add(new ValString("y"), new ValNumber(vec.y));
            map.map.Add(new ValString("z"), new ValNumber(vec.z));
            return map;
        }
        public static Vector3 FromValMap(this Vector3 vec, ref ValMap map)
        {
            return new Vector3(
                map.map[new ValString("x")].FloatValue(),
                map.map[new ValString("y")].FloatValue(),
                map.map[new ValString("z")].FloatValue()
                );
        }
        public static ValList ToValList(this Vector3 vec)
        {
            ValList list = new ValList();
            list.values.Add(new ValNumber(vec.x));
            list.values.Add(new ValNumber(vec.y));
            list.values.Add(new ValNumber(vec.z));
            return list;
        }
        public static Vector3 FromValList(this Vector3 vec, ref ValList list)
        {
            return new Vector3(list.values[0].FloatValue(), list.values[1].FloatValue(), list.values[2].FloatValue());
        }

        public static ValMap ToValMap(this Vector3Int vec)
        {
            ValMap map = new ValMap();
            map.map.Add(new ValString("x"), new ValNumber(vec.x));
            map.map.Add(new ValString("y"), new ValNumber(vec.y));
            map.map.Add(new ValString("z"), new ValNumber(vec.z));
            return map;
        }
        public static Vector3Int FromValMap(this Vector3Int vec, ref ValMap map)
        {
            return new Vector3Int(
                map.map[new ValString("x")].IntValue(),
                map.map[new ValString("y")].IntValue(),
                map.map[new ValString("z")].IntValue()
                );
        }
        public static ValList ToValList(this Vector3Int vec)
        {
            ValList list = new ValList();
            list.values.Add(new ValNumber(vec.x));
            list.values.Add(new ValNumber(vec.y));
            list.values.Add(new ValNumber(vec.z));
            return list;
        }
        public static Vector3Int FromValList(this Vector3Int vec, ref ValList list)
        {
            return new Vector3Int(list.values[0].IntValue(), list.values[1].IntValue(), list.values[2].IntValue());
        }

        public static ValMap ToValMap(this Vector4 vec)
        {
            ValMap map = new ValMap();
            map.map.Add(new ValString("x"), new ValNumber(vec.x));
            map.map.Add(new ValString("y"), new ValNumber(vec.y));
            map.map.Add(new ValString("z"), new ValNumber(vec.z));
            map.map.Add(new ValString("w"), new ValNumber(vec.w));
            return map;
        }
        public static Vector4 FromValMap(this Vector4 vec, ref ValMap map)
        {
            return new Vector4(
                map.map[new ValString("x")].FloatValue(),
                map.map[new ValString("y")].FloatValue(),
                map.map[new ValString("z")].FloatValue(),
                map.map[new ValString("w")].FloatValue()
                );
        }
        public static ValList ToValList(this Vector4 vec)
        {
            ValList list = new ValList();
            list.values.Add(new ValNumber(vec.x));
            list.values.Add(new ValNumber(vec.y));
            list.values.Add(new ValNumber(vec.z));
            list.values.Add(new ValNumber(vec.w));
            return list;
        }
        public static Vector4 FromValList(this Vector4 vec, ref ValList list)
        {
            return new Vector4(list.values[0].FloatValue(), list.values[1].FloatValue(), list.values[2].FloatValue(), list.values[3].FloatValue());
        }
        #endregion

        #region Quaternion
        public static ValMap ToValMap(this Quaternion q)
        {
            ValMap result = new ValMap();
            result.map.Add(new ValString("w"), new ValNumber(q.w));
            result.map.Add(new ValString("x"), new ValNumber(q.x));
            result.map.Add(new ValString("y"), new ValNumber(q.y));
            result.map.Add(new ValString("z"), new ValNumber(q.z));

            return result;
        }
        public static ValList ToValList(this Quaternion q)
        {
            ValList list = new ValList();
            list.values.Add(new ValNumber(q.x));
            list.values.Add(new ValNumber(q.y));
            list.values.Add(new ValNumber(q.z));
            list.values.Add(new ValNumber(q.w));
            return list;
        }

        public static Quaternion FromValMap(this Quaternion q, ValMap map)
        {
            Quaternion result = new Quaternion();
            result.Set(map["x"].FloatValue(), map["y"].FloatValue(), map["z"].FloatValue(), map["w"].FloatValue());
            return result;
        }
        public static Quaternion FromValMap(this Quaternion q, ValList list)
        {
            Quaternion result = new Quaternion();
            result.Set(list.values[0].FloatValue(), list.values[1].FloatValue(), list.values[2].FloatValue(), list.values[3].FloatValue());
            return result;
        }
        #endregion

        #region Random.State
        public static ValList ToValList(this UnityEngine.Random.State state)
        {
            int size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(UnityEngine.Random.State));
            byte[] result = new byte[size];
            unsafe { fixed (byte* ptr = result) { *(UnityEngine.Random.State*)ptr = state; } }
            ValList list = new ValList(
                new List<Value>() {
                    new ValNumber(result[0]),new ValNumber(result[1]),new ValNumber(result[2]),new ValNumber(result[3]),new ValNumber(result[4]),
                    new ValNumber(result[5]),new ValNumber(result[6]),new ValNumber(result[7]),new ValNumber(result[8]),new ValNumber(result[9]),
                    new ValNumber(result[10]),new ValNumber(result[11]),new ValNumber(result[12]),new ValNumber(result[13]),new ValNumber(result[14]),
                    new ValNumber(result[15]),
                }
                );

            return list;
        }
        public static UnityEngine.Random.State ToRandomState(this ValList list)
        {
            if (list.values.Count != 16)
            {
                throw new ArgumentException("ValList.ToRandomState: requires 16 elements and only " + list.values.Count +
                    " were given.");
            }

            int size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(UnityEngine.Random.State));
            byte[] bitss = new byte[size];
            for (int i = 0; i < 16; i++) { bitss[i] = (byte)list.values[i].IntValue(); }
            UnityEngine.Random.State basic = new UnityEngine.Random.State();
            unsafe { fixed (byte* ptr = bitss) { basic = *(UnityEngine.Random.State*)ptr; } }
            return basic;
        }
        public static UnityEngine.Random.State BaseRandomState()
        {
            int size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(UnityEngine.Random.State));
            byte[] bitss = new byte[size];
            foreach (byte b in bitss) { bitss[b] = 0; }

            UnityEngine.Random.State blarg = new UnityEngine.Random.State();
            unsafe { fixed (byte* ptr = bitss) { blarg = *(UnityEngine.Random.State*)ptr; } }
            return blarg;
        }
        #endregion

        #region Resolution
        public static ValMap ToValMap(this Resolution res)
        {
            ValMap map = new ValMap();
            map.map.Add(new ValString("height"), new ValNumber(res.height));
            map.map.Add(new ValString("width"), new ValNumber(res.width));
            map.map.Add(new ValString("refreshnum"), new ValNumber(res.refreshRateRatio.numerator));
            map.map.Add(new ValString("refreshden"), new ValNumber(res.refreshRateRatio.denominator));
            map.map.Add(new ValString("refreshvalue"), new ValNumber(res.refreshRateRatio.value));
            return map;
        }
        public static UnityEngine.Resolution ToResolution(this ValMap map)
        {
            UnityEngine.Resolution res = new Resolution();
            res.height = map["height"].IntValue();
            res.width = map["width"].IntValue();
            res.refreshRateRatio = new RefreshRate()
            {
                numerator = map["refreshnum"].UIntValue(),
                denominator = map["refreshden"].UIntValue()
            };
            return res;
        }

        public static ValList ToValList(this Resolution res)
        {
            ValList tmp = new ValList();
            tmp.values.Add(new ValNumber(res.height));
            tmp.values.Add(new ValNumber(res.width));
            tmp.values.Add(new ValNumber(res.refreshRateRatio.numerator));
            tmp.values.Add(new ValNumber(res.refreshRateRatio.denominator));
            tmp.values.Add(new ValNumber(res.refreshRateRatio.value));

            return tmp;
        }
        public static UnityEngine.Resolution ToResolution(this ValList lst)
        {
            UnityEngine.Resolution res = new Resolution();
            res.height = lst.values[0].IntValue();
            res.width = lst.values[1].IntValue();
            res.refreshRateRatio = new RefreshRate()
            {
                numerator = lst.values[2].UIntValue(),
                denominator = lst.values[3].UIntValue()
            };

            return res;
        }

        public static ValList ToValList(this Resolution[] res)
        {
            ValList lst = new ValList();
            foreach (Resolution r in res)
            {
                lst.values.Add(r.ToValMap());
            }
            return lst;
        }
        #endregion

        #region FullScreenMode
        public static ValNumber ToValNumber(this FullScreenMode mode)
        {
            ValNumber num = new ValNumber(-1);

            if (mode == FullScreenMode.ExclusiveFullScreen) { num.value = 0; }
            else if (mode == FullScreenMode.FullScreenWindow) { num.value = 1; }
            else if (mode == FullScreenMode.MaximizedWindow) { num.value = 2; }
            else if (mode == FullScreenMode.Windowed) { num.value = 3; }

            return num;
        }

        public static FullScreenMode ToFullScreenMode(this ValNumber num)
        {
            switch (num.value)
            {
                case 0: return FullScreenMode.ExclusiveFullScreen;
                case 1: return FullScreenMode.FullScreenWindow;
                case 2: return FullScreenMode.MaximizedWindow;
                case 3: return FullScreenMode.Windowed;
            }

            //default to whatever was set on application load
            return UnityCachedValues.FullScreenMode;
        }
        #endregion

        #region DisplayInfo
        private static ValMap ToValMap(this RefreshRate rr)
        {
            ValMap map2 = new ValMap();
            map2["refreshnum"] = new ValNumber(rr.numerator);
            map2["refreshden"] = new ValNumber(rr.denominator);
            map2["rate"] = new ValNumber(rr.value);
            map2["refresh"] = map2;
            return map2;
        }
        public static ValMap ToValMap(this DisplayInfo di)
        {
            ValMap map = new ValMap();
            map["width"] = new ValNumber(di.width);
            map["height"] = new ValNumber(di.height);
            map["name"] = new ValString(di.name);
            di.refreshRate.ToValMap();

            return map;
        }
        #endregion

        #region ScreenOrientation
        public static ValNumber ToValNumber(this ScreenOrientation so)
        {
            switch (so)
            {
                case ScreenOrientation.AutoRotation: return new ValNumber(5);
                case ScreenOrientation.LandscapeRight: return new ValNumber(4);
                case ScreenOrientation.LandscapeLeft: return new ValNumber(3);
                case ScreenOrientation.PortraitUpsideDown: return new ValNumber(2);
                case ScreenOrientation.Portrait: return new ValNumber(1);
            };

            MiniScriptSingleton.LogInfo("ScreenOrientation.ToValNumber was given an unhandled value of {" + so.ToString() + "}");
            return new ValNumber(0);
        }

        public static ScreenOrientation ToOrientation(this ValNumber num)
        {
            switch (num.value)
            {
                case 1: return ScreenOrientation.Portrait;
                case 2: return ScreenOrientation.PortraitUpsideDown;
                case 3: return ScreenOrientation.LandscapeLeft;
                case 4: return ScreenOrientation.LandscapeRight;
                case 5: return ScreenOrientation.AutoRotation;
            }
            //if we reached here then we got an invalid value for the ValNumber, so lets inform the debugger
            MiniScriptSingleton.LogInfo("ScreenOrientation.FromValNumber was given an unhandled value of {" + num.value + "}");

            return ScreenOrientation.AutoRotation;
        }
        #endregion

        #region PointerEventData
        public static ValMap ToValMap(this PointerEventData ped) {
            


            return null; 
        }

        public static PointerEventData ToPointerEventData(this ValMap map) {
            PointerEventData ped = new PointerEventData(EventSystem.current);


            return ped; 
        }
        #endregion

        #region ColorBlock
        public static ValMap ToValMap(this ColorBlock block)
        {
            ValMap map = new ValMap();
            map.map.Add(new ValString("colorMultiplier"), new ValNumber(block.colorMultiplier));
            map.map.Add(new ValString("fadeDuration"), new ValNumber(block.fadeDuration));
            map.map.Add(new ValString("disabledColor"), block.disabledColor.ToValList());
            map.map.Add(new ValString("highlightedColor"), block.highlightedColor.ToValList());
            map.map.Add(new ValString("normalColor"), block.normalColor.ToValList());
            map.map.Add(new ValString("pressedColor"), block.pressedColor.ToValList());

            return map;
        }
        public static ColorBlock ColorBlock(ref ValMap map)
        {
            ColorBlock cb = UnityEngine.UI.ColorBlock.defaultColorBlock;
            cb.colorMultiplier = ((ValNumber)map.map[new ValString("colorMultiplier")]).FloatValue(); 
            cb.fadeDuration = ((ValNumber)map.map[new ValString("fadeDuration")]).FloatValue();
            cb.disabledColor = ((ValList)map.map[new ValString("disabledColor")]).ToColor();
            cb.highlightedColor = ((ValList)map.map[new ValString("highlightedColor")]).ToColor();
            cb.normalColor = ((ValList)map.map[new ValString("normalColor")]).ToColor();
            cb.pressedColor = ((ValList)map.map[new ValString("pressedColor")]).ToColor();

            return cb;
        }
        #endregion
    }
}
#pragma warning restore IDE0090
#pragma warning restore IDE0038
