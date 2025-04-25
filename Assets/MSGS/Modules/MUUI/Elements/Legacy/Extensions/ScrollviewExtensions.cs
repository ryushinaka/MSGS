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
    public static class ScrollviewExtensions
    {
        public static ValMap ToValMap(this MUUIScrollview view)
        {
            ValMap rst = new ValMap();
            rst.map.Add(new ValString("enabled"), ValNumber.Truth(view.enabled));
            rst.map.Add(new ValString("ContentPrefab"), new ValString(view.ContentPrefab));
            rst.map.Add(new ValString("MovementType"), new ValString(view.ScrollRect.movementType.ToString()));
            rst.map.Add(new ValString("Inertia"), ValNumber.Truth(view.ScrollRect.inertia));
            rst.map.Add(new ValString("DecelerationRate"), new ValNumber(view.ScrollRect.decelerationRate));
            rst.map.Add(new ValString("ScrollSensitivity"), new ValNumber(view.ScrollRect.scrollSensitivity));
            rst.map.Add(new ValString("showScrollbarHorizontal"), ValNumber.Truth(view.Vertical.enabled));
            rst.map.Add(new ValString("showScrollbarVertical"), ValNumber.Truth(view.Horizontal.enabled));
            //content size fitter
            //rst.map.Add(new ValString("sizeFitterHorizontal"), new ValString(view.fitter.horizontalFit.ToString()));
            //rst.map.Add(new ValString("sizeFitterVertical"), new ValString(view.fitter.verticalFit.ToString()));
            //vertical layout group            
            //rst.map.Add(new ValString("layoutPaddingLeft"), new ValNumber(view.layoutgroup.padding.left));
            //rst.map.Add(new ValString("layoutPaddingRight"), new ValNumber(view.layoutgroup.padding.right));
            //rst.map.Add(new ValString("layoutPaddingTop"), new ValNumber(view.layoutgroup.padding.top));
            //rst.map.Add(new ValString("layoutPaddingBottom"), new ValNumber(view.layoutgroup.padding.bottom));
            //rst.map.Add(new ValString("layoutSpacing"), new ValNumber(view.layoutgroup.spacing));
            //rst.map.Add(new ValString("layoutReserveArrangement"), ValNumber.Truth(view.layoutgroup.reverseArrangement));
            //rst.map.Add(new ValString("layoutControlHeight"), ValNumber.Truth(view.layoutgroup.childControlHeight));
            //rst.map.Add(new ValString("layoutControlWidth"), ValNumber.Truth(view.layoutgroup.childControlWidth));
            //rst.map.Add(new ValString("layoutUseChildScaleHeight"), ValNumber.Truth(view.layoutgroup.childScaleHeight));
            //rst.map.Add(new ValString("layoutUseChildScaleWidth"), ValNumber.Truth(view.layoutgroup.childScaleWidth));
            //rst.map.Add(new ValString("layoutForceExpandHeight"), ValNumber.Truth(view.layoutgroup.childForceExpandHeight));
            //rst.map.Add(new ValString("layoutForceExpandWidth"), ValNumber.Truth(view.layoutgroup.childForceExpandWidth));
            //rst.map.Add(new ValString("layoutChildAlignment"), new ValString(view.layoutgroup.childAlignment.ToString()));
            return rst;
        }

        public static bool UpdateScrollview(this MUUIScrollview view, Value a, Value b)
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
                            view.enabled = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify the script for Scrollview(" + view.gameObject.name + ").enabled property with a non-boolean(ValNumber) parameter." +
                                                            " The expected argument should be a ValNumber containing the bool value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "ContentPrefab":
                        #region
                        if (b is ValString)
                        {
                            string ss = ((ValString)b).value;
                            if (ss.Length == 0)
                            {
                                MiniScriptSingleton.LogError("Attempt to modify the script for Scrollview(" + view.gameObject.name + ").ContentPrefab property with a non-existent parameter." +
                                    " The name of the prefab must have a string length greater than zero.");
                                return false;
                            }
                            else if (!MiniScriptSingleton.PrefabExists(ss))
                            {
                                MiniScriptSingleton.LogError("Attempt to modify the prefab for Scrollview(" + view.gameObject.name + ").ContentPrefab property with a non-existent parameter." +
                                    " The prefab file assigned to the Scrollview object must exist prior to being assigned as a property.");
                                return false;
                            }
                            else if (MiniScriptSingleton.PrefabExists(ss))
                            {
                                view.ContentPrefab = ss;
                                return true;
                            }
                            else
                            {
                                MiniScriptSingleton.LogError("Unhandled attempt to modify the prefab for Scrollview(" + view.gameObject.name + ").ContentPrefab property. " +
                                   b.GetType().Name + " / " + ss);
                                return false;
                            }
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify the Scrollview(" + view.gameObject.name + ").ContentPrefab property with a non-string(ValString) parameter." +
                                                           " The expected argument should be a ValString containing the name of the prefab. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "MovementType":
                        #region
                        if (b is ValString)
                        {
                            try
                            {
                                ScrollRect.MovementType mt = (ScrollRect.MovementType)System.Enum.Parse(typeof(ScrollRect.MovementType), ((ValString)b).value);
                                view.ScrollRect.movementType = mt;
                                return true;
                            }
                            catch
                            {
                                MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").MovementType property with an unsupported string parameter." +
                                                            " The expected argument should be a ValString containing a string value of : " +
                                                            " Unrestricted, Elastic, or Clamped. Case sensitive!" +
                                                            "The argument given contains (  " + ((ValString)b).value + " ) for its string value.");
                                return false;
                            }
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").MovementType property with a non-String(ValString) parameter." +
                                                            " The expected argument should be a ValString containing a string value of : " +
                                                            " Unrestricted, Elastic, or Clamped. " +
                                                            "The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "Inertia":
                        #region 
                        if (b is ValNumber)
                        {
                            view.ScrollRect.inertia = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").Inertia property with a non-boolean(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "DecelerationRate":
                        #region
                        if (b is ValNumber)
                        {
                            view.ScrollRect.decelerationRate = b.FloatValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").DecelerationRate property with a non-numeric(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a numeric value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "ScrollSensitivity":
                        #region
                        if (b is ValNumber)
                        {
                            view.ScrollRect.scrollSensitivity = b.FloatValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").ScrollSensitivity property with a non-numeric(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a numeric value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "showScrollbarHorizontal":
                        #region
                        if (b is ValNumber)
                        {
                            view.ScrollRect.horizontalScrollbar.enabled = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").showScrollbarHorizontal property with a non-boolean(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "showScrollbarVertical":
                        #region
                        if (b is ValNumber)
                        {
                            view.ScrollRect.verticalScrollbar.enabled = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").showScrollbarVertical property with a non-boolean(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    //case "sizeFitterHorizontal":
                    //    #region
                    //    if (b is ValString)
                    //    {
                    //        try
                    //        {
                    //            view.fitter.horizontalFit = (ContentSizeFitter.FitMode)System.Enum.Parse(typeof(ContentSizeFitter.FitMode), ((ValString)b).value);
                    //            return true;
                    //        }
                    //        catch
                    //        {
                    //            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").contentSizeFitterHorizontal property with a non-String(ValString) parameter." +
                    //                                        " The expected argument should be a ValString containing a string value of : " +
                    //                                        " MinSize, PreferredSize, or Unconstrained. " +
                    //                                        "The argument given appears to be of " + b.GetType().Name + " type.");
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").contentSizeFitterHorizontal property with a non-String(ValString) parameter." +
                    //                                        " The expected argument should be a ValString containing a string value of : " +
                    //                                        " Unrestricted, Elastic, or Clamped. " +
                    //                                        "The argument given appears to be of " + b.GetType().Name + " type.");
                    //        return false;
                    //    }
                    //#endregion
                    //case "sizeFitterVertical":
                    //    #region
                    //    if (b is ValString)
                    //    {
                    //        try
                    //        {
                    //            view.fitter.verticalFit = (ContentSizeFitter.FitMode)System.Enum.Parse(typeof(ContentSizeFitter.FitMode), ((ValString)b).value);
                    //            return true;
                    //        }
                    //        catch
                    //        {
                    //            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").contentSizeFitterVertical property with a non-String(ValString) parameter." +
                    //                                        " The expected argument should be a ValString containing a string value of : " +
                    //                                        " MinSize, PreferredSize, or Unconstrained. " +
                    //                                        "The argument given appears to be of " + b.GetType().Name + " type.");
                    //            return false;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").contentSizeFitterVertical property with a non-String(ValString) parameter." +
                    //                                        " The expected argument should be a ValString containing a string value of : " +
                    //                                        " MinSize, PreferredSize, or Unconstrained. " +
                    //                                        "The argument given appears to be of " + b.GetType().Name + " type.");
                    //        return false;
                    //    }
                    //#endregion
                    case "layoutPaddingLeft":
                        #region
                        if (b is ValNumber)
                        {
                            view.layoutgroup.padding.left = b.IntValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").layoutPaddingLeft property with a non-numeric(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a numeric value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutPaddingRight":
                        #region
                        if (b is ValNumber)
                        {
                            view.layoutgroup.padding.right = b.IntValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").layoutPaddingRight property with a non-numeric(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a numeric value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutPaddingTop":
                        #region
                        if (b is ValNumber)
                        {
                            view.layoutgroup.padding.top = b.IntValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").layoutPaddingTop property with a non-numeric(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a numeric value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutPaddingBottom":
                        #region
                        if (b is ValNumber)
                        {
                            view.layoutgroup.padding.bottom = b.IntValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").layoutPaddingBottom property with a non-numeric(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a numeric value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutSpacing":
                        #region
                        if (b is ValNumber)
                        {
                            view.layoutgroup.spacing = b.FloatValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").layoutSpacing property with a non-numeric(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a numeric value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutReverseArrangement":
                        #region 
                        if (b is ValNumber)
                        {
                            view.layoutgroup.reverseArrangement = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").layoutReverseArrangement property with a non-boolean(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutControlHeight":
                        #region 
                        if (b is ValNumber)
                        {
                            view.layoutgroup.childControlHeight = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").layoutControlHeight property with a non-boolean(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutControlWidth":
                        #region 
                        if (b is ValNumber)
                        {
                            view.layoutgroup.childControlWidth = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").layoutControlWidth property with a non-boolean(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutUseChildScaleHeight":
                        #region 
                        if (b is ValNumber)
                        {
                            view.layoutgroup.childScaleHeight = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").layoutUseChildScaleHeight property with a non-boolean(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutUseChildScaleWidth":
                        #region 
                        if (b is ValNumber)
                        {
                            view.layoutgroup.childScaleWidth = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").layoutUseChildScaleWidth property with a non-boolean(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutForceExpandHeight":
                        #region 
                        if (b is ValNumber)
                        {
                            view.layoutgroup.childForceExpandHeight = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").layoutForceExpandHeight property with a non-boolean(ValNumber) parameter." +
                                                           " The expected argument should be a ValNumber containing a boolean value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "layoutForceExpandWidth":
                        #region 
                        if (b is ValNumber)
                        {
                            view.layoutgroup.childForceExpandWidth = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").layoutForceExpandWidth property with a non-boolean(ValNumber) parameter." +
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
                                view.layoutgroup.childAlignment = mt;
                                return true;
                            }
                            catch
                            {
                                MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").layoutChildAlignment property with an unsupported string parameter." +
                                                            " The expected argument should be a ValString containing a string value of : " +
                                                            " LowerCenter, LowerLeft, LowerRight, MiddleCenter, MiddleLeft, MiddleRight, UpperCenter, UpperLeft, UpperRight. " +
                                                            "The argument given contains (  " + ((ValString)b).value + " ) for its string value.");
                                return false;
                            }
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ").layoutChildAlignment property with a non-String(ValString) parameter." +
                                                            " The expected argument should be a ValString containing a string value of : " +
                                                            " LowerCenter, LowerLeft, LowerRight, MiddleCenter, MiddleLeft, MiddleRight, UpperCenter, UpperLeft, UpperRight. " +
                                                            "The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    default:
                        if (b is null) { MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ") with an unknown property label (" + s + ")."); }
                        else { MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ") with an unknown property label (" + s + ") with an argument that appears to be of type " + b.GetType().ToString()); }
                        return false;
                }
            }
            else
            {
                if (a == null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ") with a property label accessor that is null.");
                }
                else if (a != null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify Scrollview(" + view.gameObject.name + ") with a property label accessor that is not a String. It appears to be a (" + a.GetType().Name + ").");
                }

                return false;
            }
        }

        public static void SetupScrollview(this MUUIScrollview view, ref DataRow row)
        {
            view.ContentPrefab = (string)row["ContentPrefab"].ToString();
            view.ScrollRect.movementType = (ScrollRect.MovementType)System.Enum.Parse(typeof(ScrollRect.MovementType), (string)row["MovementType"]);
            view.ScrollRect.inertia = (bool)row["Inertia"];
            view.ScrollRect.decelerationRate = (float)row["DecelerationRate"];
            view.ScrollRect.scrollSensitivity = (float)row["ScrollSensitivity"];
            view.ScrollRect.verticalScrollbar.enabled = (bool)row["showScrollbarVertical"];
            view.ScrollRect.horizontalScrollbar.enabled = (bool)row["showScrollbarHorizontal"];

            view.fitter.horizontalFit = (ContentSizeFitter.FitMode)System.Enum.Parse(typeof(ContentSizeFitter.FitMode), (string)row["sizeFitterHorizontal"]);
            view.fitter.verticalFit = (ContentSizeFitter.FitMode)System.Enum.Parse(typeof(ContentSizeFitter.FitMode), (string)row["sizeFitterVertical"]);

            view.layoutgroup.padding.left = (int)row["layoutPaddingLeft"];
            view.layoutgroup.padding.right = (int)row["layoutPaddingRight"];
            view.layoutgroup.padding.top = (int)row["layoutPaddingTop"];
            view.layoutgroup.padding.bottom = (int)row["layoutPaddingBottom"];
            view.layoutgroup.spacing = (float)row["layoutSpacing"];
            view.layoutgroup.reverseArrangement = (bool)row["layoutReverseArrangement"];
            view.layoutgroup.childControlHeight = (bool)row["layoutControlHeight"];
            view.layoutgroup.childControlWidth = (bool)row["layoutControlWidth"];
            view.layoutgroup.childScaleHeight = (bool)row["layoutUseChildScaleHeight"];
            view.layoutgroup.childScaleWidth = (bool)row["layoutUseChildScaleWidth"];
            view.layoutgroup.childForceExpandHeight = (bool)row["layoutForceExpandHeight"];
            view.layoutgroup.childForceExpandWidth = (bool)row["layoutForceExpandWidth"];
            view.layoutgroup.childAlignment = (TextAnchor)System.Enum.Parse(typeof(TextAnchor), (string)row["layoutChildAlignment"]);

            if (((string)row["Sprite"]).Length > 0)
            {
                byte[] barr = System.Convert.FromBase64String((string)row["Sprite"]);
                Texture2D tex = new Texture2D(1, 1);
                tex.LoadImage(barr);
                var sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0),
                    100f, 0, SpriteMeshType.FullRect);
                view.ScrollBackground.sprite = sp;
            }
            else
            {
                foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                {
                    if (sprite.name == "UISprite")
                        view.ScrollBackground.sprite = sprite;
                }
            }
            
            view.ScrollBackground.color = new Color(
                float.Parse(row["ColorR"].ToString()),
                float.Parse(row["ColorG"].ToString()),
                float.Parse(row["ColorB"].ToString()),
                float.Parse(row["ColorA"].ToString()));                        
            
            if(((string)row["HorizontalSprite"]).Length > 0)
            {
                byte[] barr = System.Convert.FromBase64String((string)row["HorizontalSprite"]);
                Texture2D tex = new Texture2D(1, 1);
                tex.LoadImage(barr);
                var sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0),
                    100f, 0, SpriteMeshType.FullRect);
                view.HorizontalBar.sprite = sp;
            }
            else
            {
                foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                {
                    if (sprite.name == "Background")
                        view.HorizontalBar.sprite = sprite;
                }
            }
            view.HorizontalBar.color = new Color(
                float.Parse(row["HorizontalColorR"].ToString()),
                float.Parse(row["HorizontalColorG"].ToString()),
                float.Parse(row["HorizontalColorB"].ToString()),
                float.Parse(row["HorizontalColorA"].ToString())
                );

            if (((string)row["HorizontalHandleSprite"]).Length > 0)
            {
                byte[] barr = System.Convert.FromBase64String((string)row["HorizontalHandleSprite"]);
                Texture2D tex = new Texture2D(1, 1);
                tex.LoadImage(barr);
                var sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0),
                    100f, 0, SpriteMeshType.FullRect);
                view.HorizontalHandle.sprite = sp;
            }
            else
            {
                foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                {
                    if (sprite.name == "UISprite")
                        view.HorizontalHandle.sprite = sprite;
                }
            }
            view.HorizontalHandle.color = new Color(
               float.Parse(row["HorizontalHandleColorR"].ToString()),
               float.Parse(row["HorizontalHandleColorG"].ToString()),
               float.Parse(row["HorizontalHandleColorB"].ToString()),
               float.Parse(row["HorizontalHandleColorA"].ToString())
               );

            if (((string)row["VerticalSprite"]).Length > 0)
            {
                byte[] barr = System.Convert.FromBase64String((string)row["VerticalSprite"]);
                Texture2D tex = new Texture2D(1, 1);
                tex.LoadImage(barr);
                var sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0),
                    100f, 0, SpriteMeshType.FullRect);
                view.VerticalBar.sprite = sp;
            }
            else
            {
                foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                {
                    if (sprite.name == "Background")
                        view.VerticalBar.sprite = sprite;
                }
            }
            view.VerticalBar.color = new Color(
                float.Parse(row["VerticalColorR"].ToString()),
                float.Parse(row["VerticalColorG"].ToString()),
                float.Parse(row["VerticalColorB"].ToString()),
                float.Parse(row["VerticalColorA"].ToString())
                );

            if (((string)row["VerticalHandleSprite"]).Length > 0)
            {
                byte[] barr = System.Convert.FromBase64String((string)row["VerticalHandleSprite"]);
                Texture2D tex = new Texture2D(1, 1);
                tex.LoadImage(barr);
                var sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0),
                    100f, 0, SpriteMeshType.FullRect);
                view.VerticalHandle.sprite = sp;
            }
            else
            {
                view.VerticalHandle.sprite =
                    MiniScriptSingleton.PrefabContainer.ScrollViewPrefab.GetComponent<MUUIScrollview>().VerticalHandle.sprite;
                //foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                //{
                //    if (sprite.name == "UISprite")
                //        view.VerticalHandle.sprite = sprite;
                //}
            }
            view.VerticalHandle.color = new Color(
                float.Parse(row["VerticalHandleColorR"].ToString()),
                float.Parse(row["VerticalHandleColorG"].ToString()),
                float.Parse(row["VerticalHandleColorB"].ToString()),
                float.Parse(row["VerticalHandleColorA"].ToString())
                );

            
            view.gameObject.SetActive((bool)row["enabled"]);
            view.name = (string)row["name"];
        }
    }
}
