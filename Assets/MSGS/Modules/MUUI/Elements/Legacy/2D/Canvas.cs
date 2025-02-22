using System.Collections;
using System.Collections.Generic;
using MiniScript;
using UnityEngine;
using UnityEngine.UI;
using MiniScript.MSGS.MUUI.Extensions;

namespace MiniScript.MSGS.MUUI.TwoDimensional
{
    [RequireComponent(typeof(Canvas))]
    public class Canvas : MonoBehaviour, IControl, UI_Element
    {
        static GameObject self = null;
        Canvas canvas;
        UnityEngine.UI.CanvasScaler scaler;
        
        void Start()
        {
            if (self == null)
            {
                self = new GameObject();
                self.AddComponent<RectTransform>();
                canvas = self.AddComponent<Canvas>();
                scaler = self.AddComponent<UnityEngine.UI.CanvasScaler>();
                self.AddComponent<UnityEngine.UI.GraphicRaycaster>();
                //canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                //canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.None;
                properties = new ValMap();
                return;
            }

            properties = new ValMap();

            canvas = GetComponent<Canvas>();
            scaler = GetComponent<UnityEngine.UI.CanvasScaler>();
            //canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            //canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.None;
        }

        void Update()
        {

        }

        ValMap properties;
        public ValMap GetValMap
        {
            get { return properties; }
            set { return; }
        }

        string UI_Element.Name { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        UIElementType UI_Element.ObjectType => throw new System.NotImplementedException();

        int UI_Element.ParentID => throw new System.NotImplementedException();

        public ValMap GetProperties()
        {
            ValMap map = new ValMap();
            map.assignOverride = new ValMap.AssignOverrideFunc(AssignOverride);
            //if (canvas.pixelPerfect) { map.map.Add(new ValString("PixelPerfect"), ValNumber.one); }
            //else { map.map.Add(new ValString("PixelPerfect"), ValNumber.zero); }

            switch (scaler.uiScaleMode)
            {
                case UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPhysicalSize:
                    map.map.Add(new ValString("UIScaleMode"), new ValString("Physical"));
                    break;
                case UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize:
                    map.map.Add(new ValString("UIScaleMode"), new ValString("Pixel"));
                    break;
                case UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize:
                    map.map.Add(new ValString("UIScaleMode"), new ValString("Scale"));
                    break;
            }

            ValList l = new ValList(); l.values.Add(new ValNumber(scaler.referenceResolution.x));
            l.values.Add(new ValNumber(scaler.referenceResolution.y));
            map.map.Add(new ValString("ReferenceResolution"), l);

            map.map.Add(new ValString("PixelsPerUnit"), new ValNumber(100));

            switch (scaler.screenMatchMode)
            {
                case UnityEngine.UI.CanvasScaler.ScreenMatchMode.Expand:
                    map.map.Add(new ValString("MatchMode"), new ValString("Expand"));
                    break;
                case UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight:
                    map.map.Add(new ValString("MatchMode"), new ValString("Match"));
                    break;
                case UnityEngine.UI.CanvasScaler.ScreenMatchMode.Shrink:
                    map.map.Add(new ValString("MatchMode"), new ValString("Shrink"));
                    break;
            }

            map.map.Add(new ValString("Match"), new ValNumber(scaler.matchWidthOrHeight));

            map.map.Add(new ValString("ScaleFactor"), new ValNumber(scaler.scaleFactor));

            return map;
        }

        bool AssignOverride(Value a, Value b)
        {
            if (a is ValString)
            {
                string key = ((ValString)a).value;
                switch (key)
                {
                    //case "RenderMode":
                    case "PixelPerfect":
                        #region
                        if (b is ValNumber)
                        {
                            bool bb = b.BoolValue();
                            //canvas.pixelPerfect = bb;
                            break;
                        }
                        MiniScriptSingleton.LogError("PixelPerfect requires a ValNumber of true or false.");
                        break;
                    #endregion
                    //case "SortOrder":
                    #region

                    #endregion
                    //case "TargetDisplay":
                    #region

                    #endregion
                    case "UIScaleMode":
                        #region 
                        //acceptable parameters are Pixel/Physical/Scale
                        if (b is ValString)
                        {
                            string bb = ((ValString)b).value;

                            if (bb == "Pixel") { scaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize; break; }
                            else if (bb == "Physical") { scaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPhysicalSize; break; }
                            else if (bb == "Scale") { scaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize; break; }
                            else
                            {
                                MiniScriptSingleton.LogError("UIScaleMode property change on Canvas(\"" + this.name + "\" was attempted, however the " +
                            "value given as parameter was from the approved labels of (Pixel, Physical, Scale). Parameter given was (" + bb + ")");
                            }
                            break;
                        }

                        MiniScriptSingleton.LogError("UIScaleMode property change on Canvas(\"" + this.name + "\") was attempted, however the " +
                            "value given as parameter was not a string as expected.");
                        break;
                    #endregion
                    case "ScaleFactor":
                        #region
                        if (b is ValNumber)
                        {
                            float i = b.FloatValue();
                            if (i >= 0f)
                            {
                                scaler.scaleFactor = i;
                                break;
                            }
                            MiniScriptSingleton.LogError("ScaleFactor property change on Canvas Scaler(\"" + this.name + "\") was attempted with a zero or negative value.");
                            break;
                        }
                        MiniScriptSingleton.LogError("ScaleFactor property change on Canvas Scaler(\"" + this.name + "\") was attempted, however the" +
                            " value given as parameter was not a float value as expected.");
                        break;
                    #endregion
                    case "PixelsPerUnit":
                        #region
                        if (b is ValNumber)
                        {
                            float i = b.FloatValue();
                            if (i >= 0f)
                            {
                                scaler.referencePixelsPerUnit = i;
                                break;
                            }
                            MiniScriptSingleton.LogError("PixelsPerUnit property change on Canvas Scaler(\"" + this.name + "\") was attempted with a zero or negative value.");
                            break;
                        }
                        MiniScriptSingleton.LogError("PixelsPerUnit property change on Canvas Scaler(\"" + this.name + "\") was attempted with an " +
                            "invalid parameter type. Expected type is ValNumber.");
                        break;
                    #endregion
                    case "ReferenceResolution":
                        #region
                        if (b is ValList)
                        {
                            ValList l = (ValList)b;

                            if (l.values.Count == 2)
                            {
                                if (l.values[0] is ValNumber && l.values[1] is ValNumber)
                                {
                                    scaler.referenceResolution = new Vector2(l.values[0].FloatValue(), l.values[1].FloatValue());
                                    break;
                                }
                                else
                                {
                                    MiniScriptSingleton.LogError("ReferenceResolution expects a ValList containing 2 ValNumber elements, this is not what happened.");
                                }
                            }
                            MiniScriptSingleton.LogError("ReferenceResolution property change on Canvas Scaler(\"" + this.name + "\") was attempted with an unexpected ValList length." +
                                " Expected length is 2 and the given length was " + l.values.Count);
                            break;
                        }
                        MiniScriptSingleton.LogError("ReferenceResolution property change on Canvas Scaler(\"" + this.name + "\") was attempted with an invalid parameter type. Expected type is ValList.");
                        break;
                    #endregion
                    case "MatchMode":
                        #region
                        if (b is ValString)
                        {
                            string s = b.ToString();

                            if (s == "Match") { scaler.screenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight; }
                            else if (s == "Expand") { scaler.screenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode.Expand; }
                            else if (s == "Shrink") { scaler.screenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode.Shrink; }

                            MiniScriptSingleton.LogError("MatchMode parameter should be Match, Expand, or Shrink, what was given was '" + s + "'");
                            break;
                        }
                        MiniScriptSingleton.LogError("MatchMode expects a ValString parameter, which was not given.");
                        break;
                    #endregion
                    case "Match":
                        #region
                        if (b is ValNumber)
                        {
                            float f = b.FloatValue();
                            scaler.matchWidthOrHeight = f;
                        }
                        MiniScriptSingleton.LogError("Match value expects a ValNumber which is not what was given.");
                        break;
                    #endregion
                    default:
                        MiniScriptSingleton.LogError("Unhandled Property change attempted on Canvas(\"" + this.name + "\"), attempted to change '" + key + "' which is unsupported.");
                        break;
                }
            }
            MiniScriptSingleton.LogError("ValMap of Canvas UI properties uses ValString for its key properties. An attempt was made to access a non-ValString key.");
            return false;
        }

        GameObject UI_Element.GetGameObject()
        {
            return this.gameObject;
        }

        public void HandleEvent()
        {
            throw new System.NotImplementedException();
        }
    }
}
