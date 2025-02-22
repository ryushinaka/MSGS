using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using MiniScript.MSGS.MUUI.TwoDimensional;

namespace MiniScript.MSGS.MUUI.Extensions
{
    public static class ImageAnimatedExtensions
    {
        public static ValMap ToValMap(this ImageAnimated image)
        {
            ValMap map = new ValMap();
            map.map.Add(new ValString("enabled"), ValNumber.Truth(image.enabled));
            map.map.Add(new ValString("name"), new ValString(image.name));

            map.map.Add(new ValString("colorR"), new ValNumber(image.image.color.r));
            map.map.Add(new ValString("colorG"), new ValNumber(image.image.color.g));
            map.map.Add(new ValString("colorB"), new ValNumber(image.image.color.b));
            map.map.Add(new ValString("colorA"), new ValNumber(image.image.color.a));

            map.map.Add(new ValString("maskable"), ValNumber.Truth(image.image.maskable));

            return map;
        }

        public static bool UpdateImage(this ImageAnimated image, Value a, Value b)
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
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_ImageAnimated(" + image.gameObject.name + ").name property with a non-String(ValString) parameter." +
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
                            MiniScriptSingleton.LogError("Attempt to modify mouse event handler script for MUUI_ImageAnimated(" + image.gameObject.name + ").enabled property with a non-boolean(ValNumber) parameter." +
                                                            " The expected argument should be a ValNumber containing the bool value. The argument given appears to be of " + b.GetType().Name + " type.");
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
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ImageAnimated(" + image.gameObject.name + ").color.R with a property value that is not a Number. " +
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
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ImageAnimated(" + image.gameObject.name + ").color.G with a property value that is not a Number. " +
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
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ImageAnimated(" + image.gameObject.name + ").color.B with a property value that is not a Number. " +
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
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ImageAnimated(" + image.gameObject.name + ").color.A with a property value that is not a Number. " +
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
                            MiniScriptSingleton.LogError("Attempt to modify MUUI_ImageAnimated(" + image.gameObject.name + ").maskable with a property value that is not a Boolean. " +
                                " Argument given appears to be of type " + b.GetType().Name);
                            return false;
                        }
                    #endregion
                    default:
                        if (b is null) { MiniScriptSingleton.LogError("Attempt to modify MUUI_ImageAnimated(" + image.gameObject.name + ") with an unknown property label (" + s + ")."); }
                        else { MiniScriptSingleton.LogError("Attempt to modify MUUI_ImageAnimated(" + image.gameObject.name + ") with an unknown property label (" + s + ") with an argument that appears to be of type " + b.GetType().ToString()); }
                        return false;
                }
            }
            else
            {
                if (a == null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify MUUI_ImageAnimated(" + image.gameObject.name + ") with a property label accessor that is null.");
                }
                else if (a != null)
                {
                    MiniScriptSingleton.LogError("Attempt to modify MUUI_ImageAnimated(" + image.gameObject.name + ") with a property label accessor that is not a String. It appears to be a (" + a.GetType().Name + ").");
                }

                return false;
            }
        }

        public static void SetupImageAnimated(this ImageAnimated image, ref DataRow row, ref DataRow[] sprites)
        {
            image.image.color = new Color(
                float.Parse(row["ColorR"].ToString()),
                float.Parse(row["ColorG"].ToString()),
                float.Parse(row["ColorB"].ToString()),
                float.Parse(row["ColorA"].ToString()));
            image.image.maskable = (bool)row["maskable"];

            image.cycleTime = (float)row["CycleTime"];
            image.DoAnimate = (bool)row["DoAnimate"];
            image.currentFrame = (int)row["StartFrame"];

            if (((string)row["Sprite"]).Length > 0)
            {
                byte[] barr = System.Convert.FromBase64String((string)row["Sprite"]);
                Texture2D tex = new Texture2D(100, 100);
                tex.LoadImage(barr);
                var sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0),
                    100, 0, SpriteMeshType.FullRect);
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
                        
            image.sprites = new List<Sprite>();

            if (sprites != null)
            {
                foreach (DataRow dr in sprites)
                {
                    byte[] barr = System.Convert.FromBase64String((string)dr["Sprite"]);
                    Texture2D tex = new Texture2D(100, 100);
                    tex.LoadImage(barr);
                    var sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0),
                        100, 0, SpriteMeshType.FullRect);
                    image.sprites.Add(sp);
                }
            }

            image.currentFrame = (int)row["StartFrame"];
            image.cycleTime = (float)row["CycleTime"];
            image.DoAnimate = (bool)row["DoAnimate"];
                        
            image.gameObject.SetActive(bool.Parse(row["enabled"].ToString()));
            image.gameObject.name = (string)row["name"];
        }
    }
}

