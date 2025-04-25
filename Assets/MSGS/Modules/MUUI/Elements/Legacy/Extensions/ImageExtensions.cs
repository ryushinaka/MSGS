using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using TMPro;
using MiniScript;
using System;
using MiniScript.MSGS.MUUI.TwoDimensional;

namespace MiniScript.MSGS.MUUI
{
    public static class ImageExtensions
    {
        public static ValMap ToValMap(this MiniScript.MSGS.MUUI.TwoDimensional.MUUIImage image)
        {
            ValMap map = new ValMap();
            map.map.Add(new ValString("enabled"), ValNumber.Truth(image.enabled));
            map.map.Add(new ValString("name"), new ValString(image.gameObject.name));

            map.map.Add(new ValString("alphaThreshold"), new ValNumber(image.image.alphaHitTestMinimumThreshold));

            map.map.Add(new ValString("fillAmount"), new ValNumber(image.image.fillAmount));

            if (image.image.fillCenter) { map.map.Add(new ValString("fillCenter"), new ValNumber(1)); }
            else { map.map.Add(new ValString("fillCenter"), new ValNumber(0)); }

            if (image.image.fillClockwise) { map.map.Add(new ValString("fillClockwise"), new ValNumber(1)); }
            else { map.map.Add(new ValString("fillClockwise"), new ValNumber(0)); }

            map.map.Add(new ValString("fillMethod"), new ValString(image.image.fillMethod.ToString()));

            //string fillorigin = "";
            //switch ((Image.FillMethod)image.image.fillMethod)
            //{
            //    case Image.FillMethod.Horizontal:
            //        fillorigin = ((Image.OriginHorizontal)image.image.fillOrigin).ToString();
            //        break;
            //    case Image.FillMethod.Vertical:
            //        fillorigin = ((Image.OriginVertical)image.image.fillOrigin).ToString();
            //        break;
            //    case Image.FillMethod.Radial90:
            //        fillorigin = ((Image.Origin90)image.image.fillOrigin).ToString();
            //        break;
            //    case Image.FillMethod.Radial180:
            //        fillorigin = ((Image.Origin180)image.image.fillOrigin).ToString();
            //        break;
            //    case Image.FillMethod.Radial360:
            //        fillorigin = ((Image.Origin360)image.image.fillOrigin).ToString();
            //        break;
            //}
            //map.map.Add(new ValString("fillOrigin"), new ValString(fillorigin));

            if (image.image.hasBorder) { map.map.Add(new ValString("hasBorder"), new ValNumber(1)); }
            else { map.map.Add(new ValString("hasBorder"), new ValNumber(0)); }

            if (image.image.preserveAspect) { map.map.Add(new ValString("preserveAspect"), new ValNumber(1)); }
            else { map.map.Add(new ValString("preserveAspect"), new ValNumber(0)); }

            map.map.Add(new ValString("type"), new ValString(image.image.type.ToString()));

            map.map.Add(new ValString("colorR"), new ValNumber(image.image.color.r));
            map.map.Add(new ValString("colorG"), new ValNumber(image.image.color.g));
            map.map.Add(new ValString("colorB"), new ValNumber(image.image.color.b));
            map.map.Add(new ValString("colorA"), new ValNumber(image.image.color.a));

            map.map.Add(new ValString("maskable"), ValNumber.Truth(image.image.maskable));

            return map;
        }

        public static bool UpdateImage(this MiniScript.MSGS.MUUI.TwoDimensional.MUUIImage image, Value a, Value b)
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
                            image.gameObject.name = ((ValString)b).value;
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for Image(" + image.gameObject.name + ").name property with a non-String(ValString) parameter." +
                                                            " The expected argument should be a ValString containing the string literal value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "enabled":
                        #region
                        if (b is ValNumber)
                        {
                            image.enabled = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for Image(" + image.gameObject.name + ").enabled property with a non-boolean(ValNumber) parameter." +
                                                            " The expected argument should be a ValNumber containing the bool value. The argument given appears to be of " + b.GetType().Name + " type.");
                            return false;
                        }
                    #endregion
                    case "alphaThreshold":
                        #region
                        if (b is ValNumber)
                        {
                            image.image.alphaHitTestMinimumThreshold = a.FloatValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ").alphaThreshold with a property label accessor that is not a Number. " +
                                " Argument given appears to be of type " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "fillAmount":
                        #region
                        if (b is ValNumber)
                        {
                            image.image.fillAmount = a.FloatValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ").fillAmount with a property label accessor that is not a Number. " +
                                " Argument given appears to be of type " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "fillCenter":
                        #region
                        if (b is ValNumber)
                        {
                            image.image.fillCenter = a.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ").fillCenter with a property label accessor that is not a Boolean. " +
                                " Argument given appears to be of type " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "fillClockwise":
                        #region
                        if (b is ValNumber)
                        {
                            image.image.fillClockwise = a.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ").fillClockwise with a property label accessor that is not a Boolean. " +
                                " Argument given appears to be of type " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "fillMethod":
                        #region
                        if (b is ValString)
                        {
                            string fm = ((ValString)b).value;
                            switch (fm)
                            {
                                case "Horizontal": image.image.fillMethod = UnityEngine.UI.Image.FillMethod.Horizontal; return true;
                                case "Vertical": image.image.fillMethod = UnityEngine.UI.Image.FillMethod.Vertical; return true;
                                case "Radial90": image.image.fillMethod = UnityEngine.UI.Image.FillMethod.Radial90; return true;
                                case "Radial180": image.image.fillMethod = UnityEngine.UI.Image.FillMethod.Radial180; return true;
                                case "Radial360": image.image.fillMethod = UnityEngine.UI.Image.FillMethod.Radial360; return true;
                                default:
                                    MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ").fillMethod property  with an argument that is not an approved string literal, no change made. " +
                                " Argument given appears to be of type " + b.GetType().Name + ". Acceptable string literals are {Horizontal, Vertical, Radial90, Radial180, Radial360}");
                                    return false;
                            }
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ").fillMethod property  with an argument that is not String, no change made. " +
                                " Argument given appears to be of type " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "hasBorder":
                        #region
                        MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ").hasBorder is a read-only property, no change made. " +
                                " Argument given appears to be of type " + b.GetType().Name);
                        return false;

                    #endregion
                    case "preserveAspect":
                        #region
                        if (b is ValNumber)
                        {
                            image.image.preserveAspect = a.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ").preserveAspect with a property value that is not a Boolean. " +
                                " Argument given appears to be of type " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "type":
                        #region
                        if (b is ValString)
                        {
                            string st = ((ValString)b).value;
                            switch (st)
                            {
                                case "Simple": image.image.type = UnityEngine.UI.Image.Type.Simple; return true;
                                case "Sliced": image.image.type = UnityEngine.UI.Image.Type.Sliced; return true;
                                case "Tiled": image.image.type = UnityEngine.UI.Image.Type.Tiled; return true;
                                case "Filled": image.image.type = UnityEngine.UI.Image.Type.Filled; return true;
                                default:
                                    MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ").type with a property value that is not an acceptable string literal. " +
                                " Argument given appears to be of type " + b.GetType().Name + ". Acceptable string literals are {Simple, Sliced, Tiled, Filled}");
                                    return false;
                            }
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ").type with a property value that is not a String. " +
                                " Argument given appears to be of type " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "colorR":
                        #region
                        if (b is ValNumber)
                        {

                            image.image.color = new Color(b.FloatValue(), image.image.color.g, image.image.color.b, image.image.color.a);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ").color.R with a property value that is not a Number. " +
                                " Argument given appears to be of type " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "colorG":
                        #region
                        if (b is ValNumber)
                        {

                            image.image.color = new Color(image.image.color.r, b.FloatValue(), image.image.color.b, image.image.color.a);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ").color.G with a property value that is not a Number. " +
                                " Argument given appears to be of type " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "colorB":
                        #region
                        if (b is ValNumber)
                        {

                            image.image.color = new Color(image.image.color.r, image.image.color.g, b.FloatValue(), image.image.color.a);
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ").color.B with a property value that is not a Number. " +
                                " Argument given appears to be of type " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "colorA":
                        #region
                        if (b is ValNumber)
                        {
                            image.image.color = new Color(image.image.color.r, image.image.color.g, image.image.color.b, b.FloatValue());
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ").color.A with a property value that is not a Number. " +
                                " Argument given appears to be of type " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    case "maskable":
                        #region
                        if (b is ValNumber)
                        {
                            image.image.maskable = b.BoolValue();
                            return true;
                        }
                        else
                        {
                            MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ").maskable with a property value that is not a Boolean. " +
                                " Argument given appears to be of type " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    default:
                        if (b is null) { MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ") with an unknown property label (" + s + ")."); }
                        else { MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ") with an unknown property label (" + s + ") with an argument that appears to be of type " + b.GetType().ToString()); }
                        return false;
                }
            }
            else
            {
                if (a == null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ") with a property label accessor that is null.");
                }
                else if (a != null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify Image(" + image.gameObject.name + ") with a property label accessor that is not a String. It appears to be a (" + a.GetType().Name + ").");
                }

                return false;
            }
        }

        public static void SetupImage(this MiniScript.MSGS.MUUI.TwoDimensional.MUUIImage image, ref DataRow row)
        {
            image.image.color = new Color(
                float.Parse(row["ColorR"].ToString()),
                float.Parse(row["ColorG"].ToString()),
                float.Parse(row["ColorB"].ToString()),
                float.Parse(row["ColorA"].ToString()));
            image.image.raycastTarget = (bool)row["RaycastTarget"];
            image.image.maskable = (bool)row["Maskable"];
            image.image.preserveAspect = (bool)row["PreserveAspect"];

            if(((string)row["Sprite"]).Length > 0)
            {
                byte[] barr = System.Convert.FromBase64String((string)row["Sprite"]);
                Texture2D tex = new Texture2D(1, 1);
                tex.LoadImage(barr);
                var sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0),
                    100f, 0, SpriteMeshType.FullRect);                
                image.image.sprite = sp;
            }
            else
            {
                foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                {
                    if (sprite.name == "UISprite")
                        image.image.sprite = sprite;
                }
            }

            
            image.gameObject.SetActive(bool.Parse(row["enabled"].ToString()));
            image.image.name = (string)row["name"];
            //image.image.alphaHitTestMinimumThreshold = (float)row["AlphaThreshold"];
            image.image.fillAmount = (float)row["fillAmount"];
            image.image.fillCenter = (bool)row["fillCenter"];
            image.image.fillClockwise = (bool)row["fillClockwise"];
            image.image.fillMethod = (UnityEngine.UI.Image.FillMethod)System.Enum.Parse(typeof(UnityEngine.UI.Image.FillMethod), (string)row["fillMethod"]);
            image.image.type = (UnityEngine.UI.Image.Type)System.Enum.Parse(typeof(UnityEngine.UI.Image.Type), (string)row["type"]);
        }
    }
}
