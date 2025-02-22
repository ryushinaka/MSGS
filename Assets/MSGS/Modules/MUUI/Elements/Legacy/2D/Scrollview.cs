using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MiniScript;
using UnityEngine.EventSystems;
using MiniScript.MSGS.MUUI.Extensions;
using UnityEditor;
using System;


namespace MiniScript.MSGS.MUUI.TwoDimensional
{    
    public class Scrollview : MonoBehaviour, UI_Element, IControl, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        float mscroll;
        bool benter;
        public RectTransform Rect;
        public UnityEngine.UI.Image ScrollBackground, HorizontalBar, VerticalBar, HorizontalHandle, VerticalHandle;
        public ScrollRect ScrollRect;
        public GameObject ContentObject, Viewport;
        public Scrollbar Horizontal, Vertical;
        public VerticalLayoutGroup layoutgroup;
        public ContentSizeFitter fitter;
        public string ContentPrefab;
        public string ScriptOnEnter, ScriptOnExit, ScriptOnLeftClick, ScriptOnScrollUp, ScriptOnScrollDown,
           ScriptOnDoubleLeftClick, ScriptOnRightClick, ScriptOnDoubleRightClick, ScriptOnMiddleClick;

        List<int> events = new List<int>();
        #region public const int event declarations
        public const int ScrollviewMouseEnter = 1600;
        public const int ScrollviewMouseExit = 1601;
        public const int ScrollviewMouseLeftClick = 1602;
        public const int ScrollviewMouseRightClick = 1603;
        public const int ScrollviewMouseMiddleClick = 1604;
        public const int ScrollviewMouseDoubleLeftClick = 1605;
        public const int ScrollviewMouseDoubleRightClick = 1606;
        public const int ScrollviewMouseScrollUp = 1607;
        public const int ScrollviewMouseScrollDown = 1608;
        #endregion

        void Start()
        {
            
        }
                
        void Update()
        {
            if (benter)
            {
                mscroll = Input.GetAxis("Mouse ScrollWheel");

                if (mscroll > 0)
                {
                    InputEvent tmp = new InputEvent();
                    tmp.OnScrollUp = true;
                    tmp.Element = this.Name;
                    tmp.ElementType = UIElementType.Scrollview;
                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
                if (mscroll < 0)
                {
                    InputEvent tmp = new InputEvent();
                    tmp.OnScrollDown = true;
                    tmp.Element = this.Name;
                    tmp.ElementType = UIElementType.Scrollview;
                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
            }
        }

        ValMap properties;
        public ValMap GetValMap
        {
            get { return properties; }
            set { return; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public UIElementType ObjectType
        {
            get { return UIElementType.Scrollview; }
            set { }
        }

        public int ParentID
        {
            get
            {
                if (this.transform.parent != null)
                {
                    return this.transform.parent.gameObject.GetInstanceID();
                }
                return -1;
            }
            set { }
        }

        public ValMap GetProperties()
        {
            properties = new ValMap();

            ValMap r = Rect.ToValMap();
            r.assignOverride += new ValMap.AssignOverrideFunc(OnRectUpdate);
            properties.map.Add(new ValString("Rect"), r);
            
            r = fitter.ToValMap();
            r.assignOverride += new ValMap.AssignOverrideFunc(OnContentFitterUpdate);
            properties.map.Add(new ValString("Fitter"), r);

            r = layoutgroup.ToValMap();
            r.assignOverride += new ValMap.AssignOverrideFunc(OnVerticalLayoutUpdate);
            properties.map.Add(new ValString("LayoutGroup"), r);

            r.assignOverride += new ValMap.AssignOverrideFunc(OnScrollviewUpdate);
            properties.map.Add(new ValString("Scrollview"), r);
            
            return properties;            
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            benter = true;
            InputEvent tmp = new InputEvent();
            tmp.OnEnter = true;
            tmp.Element = this.Name;
            tmp.ElementType = UIElementType.Scrollview;
            MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            benter = false;
            InputEvent tmp = new InputEvent();
            tmp.OnExit = true;
            tmp.Element = this.Name;
            tmp.ElementType = UIElementType.Scrollview;

            MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
        }

        float leftclicked = 0, rightclicked = 0;
        float leftclicktime = 0, rightclicktime = 0;
        float leftclickdelay = 0.5f, rightclickdelay = 0.5f;
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                // Detecting double click
                leftclicked++;

                if (leftclicked == 1) { leftclicktime = UnityEngine.Time.time; }

                bool didraisedoubleclick = false;
                if (leftclicked > 1 && UnityEngine.Time.time - leftclicktime < leftclickdelay)
                {
                    // Double click detected
                    leftclicked = 0;
                    leftclicktime = 0;

                    didraisedoubleclick = true;

                    InputEvent tmp = new InputEvent
                    {
                        ClickLeftDouble = true,
                        Element = this.Name,
                        ElementType = UIElementType.Scrollview,
                        ScriptName = ScriptOnDoubleLeftClick
                    };

                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
                else if (leftclicked > 2 || UnityEngine.Time.time - leftclicktime > 1) { leftclicked = 0; }

                if (!didraisedoubleclick)
                {
                    InputEvent tmp = new InputEvent
                    {
                        ClickLeft = true,
                        Element = this.Name,
                        ElementType = UIElementType.Scrollview,
                        ScriptName = ScriptOnLeftClick
                    };

                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                // Detecting double click
                rightclicked++;

                if (rightclicked == 1) { rightclicktime = UnityEngine.Time.time; }

                bool didraisedoubleclick = false;
                if (rightclicked > 1 && UnityEngine.Time.time - rightclicktime < rightclickdelay)
                {
                    // Double click detected
                    rightclicked = 0;
                    rightclicktime = 0;

                    didraisedoubleclick = true;

                    InputEvent tmp = new InputEvent
                    {
                        ClickRightDouble = true,
                        Element = this.Name,
                        ElementType = UIElementType.Scrollview,
                        ScriptName = ScriptOnDoubleRightClick
                    };

                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
                else if (rightclicked > 2 || UnityEngine.Time.time - rightclicktime > 1) { rightclicked = 0; }

                if (!didraisedoubleclick)
                {
                    InputEvent tmp = new InputEvent
                    {
                        ClickRight = true,
                        Element = this.Name,
                        ElementType = UIElementType.Scrollview,
                        ScriptName = ScriptOnRightClick
                    };

                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Middle)
            {
                InputEvent tmp = new InputEvent
                {
                    ClickMiddle = true,
                    Element = this.Name,
                    ElementType = UIElementType.Scrollview,
                    ScriptName = ScriptOnMiddleClick
                };

                MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
            }
        }

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        bool OnRectUpdate(Value a, Value b)
        {
            return Rect.UpdateRect(a, b);
        }

        bool OnContentFitterUpdate(Value a, Value b)
        {
            return fitter.UpdateContentFitter(a, b);
        }

        bool OnVerticalLayoutUpdate(Value a, Value b)
        {
            return layoutgroup.UpdateLayout(a, b);
        }

        bool OnScrollviewUpdate(Value a, Value b)
        {
            return this.UpdateScrollview(a, b);
        }

        #region Editor Extensions
#if UNITY_EDITOR
        [MenuItem("GameObject/MSGS/Add/Scrollview")]
        private static void AddScrollview()
        {
            var go = Selection.activeObject as GameObject;
            if (go == null) { return; }
            go.AddComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>();
            go.AddComponent<RectTransform>();
            var rt = go.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 50f);
            rt.localPosition = new Vector3(0f, 50f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(80f, 65f);
            rt.offsetMin = new Vector2(-80f, 35f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.sizeDelta = new Vector2(160f, 30f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            go.AddComponent<ScrollRect>();
            go.AddComponent<UnityEngine.UI.Image>();
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "Background")
                {
                    go.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }
            go.GetComponent<UnityEngine.UI.Image>().type = UnityEngine.UI.Image.Type.Sliced;
            go.GetComponent<UnityEngine.UI.Image>().maskable = true;
            go.GetComponent<UnityEngine.UI.Image>().fillCenter = true;
            go.GetComponent<UnityEngine.UI.Image>().pixelsPerUnitMultiplier = 1f;

            go.GetComponent<ScrollRect>().horizontal = true;
            go.GetComponent<ScrollRect>().vertical = true;
            go.GetComponent<ScrollRect>().movementType = ScrollRect.MovementType.Elastic;
            go.GetComponent<ScrollRect>().elasticity = 0.1f;
            go.GetComponent<ScrollRect>().inertia = true;
            go.GetComponent<ScrollRect>().decelerationRate = 0.135f;
            go.GetComponent<ScrollRect>().scrollSensitivity = 1f;

            #region viewport
            var viewport = new GameObject("Viewport");
            viewport.AddComponent<RectTransform>();
            viewport.AddComponent<UnityEngine.UI.Image>();
            viewport.AddComponent<Mask>();
            viewport.GetComponent<Mask>().showMaskGraphic = false;
            viewport.transform.SetParent(go.transform);
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "UIMask")
                {
                    viewport.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }
            rt = viewport.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(-100f, 140f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(-17f, 0f);
            rt.offsetMin = new Vector2(0f, 17f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(-17f, -17f);
            rt.pivot = new Vector2(0f, 1f);

            var content = new GameObject("Content");
            content.AddComponent<RectTransform>();
            content.AddComponent<ContentSizeFitter>();
            content.AddComponent<VerticalLayoutGroup>();
            content.transform.SetParent(viewport.transform);
            rt = content.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(0f, 0f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(0f, 0f);
            rt.offsetMin = new Vector2(0f, -400f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(0f, 1f);
            rt.sizeDelta = new Vector2(0f, 400f);
            rt.pivot = new Vector2(0f, 1f);
            
            content.GetComponent<VerticalLayoutGroup>().padding = new RectOffset(0, 0, 0, 0);
            content.GetComponent<VerticalLayoutGroup>().childControlHeight = false;
            content.GetComponent<VerticalLayoutGroup>().childControlWidth = false;
            content.GetComponent<VerticalLayoutGroup>().childScaleHeight = false;
            content.GetComponent<VerticalLayoutGroup>().childScaleWidth = false;
            content.GetComponent<VerticalLayoutGroup>().childForceExpandHeight = false;
            content.GetComponent<VerticalLayoutGroup>().childForceExpandWidth = false;
            content.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.UpperLeft;
            content.GetComponent<VerticalLayoutGroup>().reverseArrangement = false;

            content.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            content.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            #endregion

            #region Scrollbar Horizontal
            var horiz = new GameObject("Scrollbar Horizontal");
            horiz.AddComponent<RectTransform>();
            horiz.AddComponent<UnityEngine.UI.Image>();
            horiz.AddComponent<Scrollbar>();
            horiz.transform.SetParent(go.transform);
            rt = horiz.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(-100f, -140f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(-17f, 20f);
            rt.offsetMin = new Vector2(0f, 0f);
            rt.anchorMax = new Vector2(1f, 0f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(-17f, 20f);
            rt.pivot = new Vector2(0f, 0f);

            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "Background")
                {
                    horiz.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }
            horiz.GetComponent<UnityEngine.UI.Image>().type = UnityEngine.UI.Image.Type.Sliced;
            horiz.GetComponent<UnityEngine.UI.Image>().maskable = true;
            horiz.GetComponent<UnityEngine.UI.Image>().fillCenter = true;
            horiz.GetComponent<UnityEngine.UI.Image>().pixelsPerUnitMultiplier = 1f;

            var hsliding = new GameObject("Sliding Area");
            hsliding.AddComponent<RectTransform>();
            hsliding.transform.SetParent(horiz.transform);
            rt = hsliding.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(91.5f, 10f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(-10f, -10f);
            rt.offsetMin = new Vector2(10f, 10f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(-20f, 20f);
            rt.pivot = new Vector2(0.5f, 0.5f);

            var hhandle = new GameObject("Handle");
            hhandle.AddComponent<RectTransform>();
            hhandle.AddComponent<UnityEngine.UI.Image>();
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "UISprite")
                {
                    hhandle.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }
            hhandle.GetComponent<UnityEngine.UI.Image>().type = UnityEngine.UI.Image.Type.Sliced;
            hhandle.GetComponent<UnityEngine.UI.Image>().maskable = true;
            hhandle.GetComponent<UnityEngine.UI.Image>().fillCenter = true;
            hhandle.GetComponent<UnityEngine.UI.Image>().pixelsPerUnitMultiplier = 1f;
            hhandle.transform.SetParent(hsliding.transform);
            rt = hhandle.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(0f, 0f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(10f, 10f);
            rt.offsetMin = new Vector2(-10f, -10f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(20f, 20f);
            rt.pivot = new Vector2(0.5f, 0.5f);

            #endregion

            #region Scrollbar Vertical
            var verti = new GameObject("Scrollbar Vertical");
            verti.AddComponent<RectTransform>();
            verti.AddComponent<UnityEngine.UI.Image>();
            verti.AddComponent<Scrollbar>();
            verti.transform.SetParent(go.transform);
            rt = verti.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(100f, 140f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(0f, 0f);
            rt.offsetMin = new Vector2(-20f, 17f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(1f, 0f);
            rt.sizeDelta = new Vector2(20f, -17f);
            rt.pivot = new Vector2(1f, 1f);

            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "Background")
                {
                    verti.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }
            verti.GetComponent<UnityEngine.UI.Image>().type = UnityEngine.UI.Image.Type.Sliced;
            verti.GetComponent<UnityEngine.UI.Image>().maskable = true;
            verti.GetComponent<UnityEngine.UI.Image>().fillCenter = true;
            verti.GetComponent<UnityEngine.UI.Image>().pixelsPerUnitMultiplier = 1f;

            var vsliding = new GameObject("Sliding Area");
            vsliding.AddComponent<RectTransform>();
            vsliding.transform.SetParent(verti.transform);
            rt = vsliding.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(-10f, -132f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(-10f, -10f);
            rt.offsetMin = new Vector2(10f, 10f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(-20f, -20f);
            rt.pivot = new Vector2(0.5f, 0.5f);

            var vhandle = new GameObject("Handle");
            vhandle.AddComponent<RectTransform>();
            vhandle.AddComponent<UnityEngine.UI.Image>();
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "UISprite")
                {
                    vhandle.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }
            vhandle.GetComponent<UnityEngine.UI.Image>().type = UnityEngine.UI.Image.Type.Sliced;
            vhandle.GetComponent<UnityEngine.UI.Image>().maskable = true;
            vhandle.GetComponent<UnityEngine.UI.Image>().fillCenter = true;
            vhandle.GetComponent<UnityEngine.UI.Image>().pixelsPerUnitMultiplier = 1f;
            vhandle.transform.SetParent(vsliding.transform);
            rt = vhandle.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(0f, 41.5f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(10f, 10f);
            rt.offsetMin = new Vector2(-10f, -10f);
            rt.anchorMax = new Vector2(1f, 0.34055f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(20f, 20f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            #endregion

            horiz.GetComponent<Scrollbar>().handleRect = hhandle.GetComponent<RectTransform>();
            horiz.GetComponent<Scrollbar>().targetGraphic = hhandle.GetComponent<UnityEngine.UI.Image>();

            verti.GetComponent<Scrollbar>().handleRect = vhandle.GetComponent<RectTransform>();
            verti.GetComponent<Scrollbar>().targetGraphic = vhandle.GetComponent<UnityEngine.UI.Image>();

            go.GetComponent<ScrollRect>().content = content.GetComponent<RectTransform>();
            go.GetComponent<ScrollRect>().viewport = viewport.GetComponent<RectTransform>();
            go.GetComponent<ScrollRect>().horizontalScrollbar = horiz.GetComponent<Scrollbar>();
            go.GetComponent<ScrollRect>().verticalScrollbar = verti.GetComponent<Scrollbar>();
            go.GetComponent<ScrollRect>().horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            go.GetComponent<ScrollRect>().verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            go.GetComponent<ScrollRect>().verticalScrollbarSpacing = -3;
            go.GetComponent<ScrollRect>().horizontalScrollbarSpacing = -3;

            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().Rect = go.GetComponent<RectTransform>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().ScrollRect = go.GetComponent<ScrollRect>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().ScrollBackground = go.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().HorizontalBar = horiz.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().VerticalBar = verti.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().HorizontalHandle = hhandle.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().VerticalHandle = vhandle.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().ContentObject = content;
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().Viewport = viewport;
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().Horizontal = horiz.GetComponent<Scrollbar>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().Vertical = verti.GetComponent<Scrollbar>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().layoutgroup = content.GetComponent<VerticalLayoutGroup>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().fitter = content.GetComponent<ContentSizeFitter>();
        }


        [MenuItem("GameObject/MSGS/Create/Scrollview")]
        private static void CreateScrollView()
        {
            var go = Selection.activeObject as GameObject;
            if (go == null) 
            {
                go = new GameObject("Scrollview");
            }
            else
            {
                var tmp = new GameObject("Scrollview");
                tmp.transform.SetParent(go.transform);
                go = tmp;
            }

            go.AddComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>();
            go.AddComponent<RectTransform>();
            var rt = go.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 50f);
            rt.localPosition = new Vector3(0f, 50f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(80f, 65f);
            rt.offsetMin = new Vector2(-80f, 35f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.sizeDelta = new Vector2(160f, 30f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            go.AddComponent<ScrollRect>();
            go.AddComponent<UnityEngine.UI.Image>();
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "Background")
                {
                    go.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }
            go.GetComponent<UnityEngine.UI.Image>().type = UnityEngine.UI.Image.Type.Sliced;
            go.GetComponent<UnityEngine.UI.Image>().maskable = true;
            go.GetComponent<UnityEngine.UI.Image>().fillCenter = true;
            go.GetComponent<UnityEngine.UI.Image>().pixelsPerUnitMultiplier = 1f;

            go.GetComponent<ScrollRect>().horizontal = true;
            go.GetComponent<ScrollRect>().vertical = true;
            go.GetComponent<ScrollRect>().movementType = ScrollRect.MovementType.Elastic;
            go.GetComponent<ScrollRect>().elasticity = 0.1f;
            go.GetComponent<ScrollRect>().inertia = true;
            go.GetComponent<ScrollRect>().decelerationRate = 0.135f;
            go.GetComponent<ScrollRect>().scrollSensitivity = 1f;

            #region viewport
            var viewport = new GameObject("Viewport");
            viewport.AddComponent<RectTransform>();
            viewport.AddComponent<UnityEngine.UI.Image>();
            viewport.AddComponent<Mask>();
            viewport.GetComponent<Mask>().showMaskGraphic = false;
            viewport.transform.SetParent(go.transform);
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "UIMask")
                {
                    viewport.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }
            rt = viewport.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(-100f, 140f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(-17f, 0f);
            rt.offsetMin = new Vector2(0f, 17f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(-17f, -17f);
            rt.pivot = new Vector2(0f, 1f);

            var content = new GameObject("Content");
            content.AddComponent<RectTransform>();
            content.AddComponent<ContentSizeFitter>();
            content.AddComponent<VerticalLayoutGroup>();
            content.transform.SetParent(viewport.transform);
            rt = content.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(0f, 0f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(0f, 0f);
            rt.offsetMin = new Vector2(0f, -400f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(0f, 1f);
            rt.sizeDelta = new Vector2(0f, 400f);
            rt.pivot = new Vector2(0f, 1f);

            content.GetComponent<VerticalLayoutGroup>().padding = new RectOffset(0, 0, 0, 0);
            content.GetComponent<VerticalLayoutGroup>().childControlHeight = false;
            content.GetComponent<VerticalLayoutGroup>().childControlWidth = false;
            content.GetComponent<VerticalLayoutGroup>().childScaleHeight = false;
            content.GetComponent<VerticalLayoutGroup>().childScaleWidth = false;
            content.GetComponent<VerticalLayoutGroup>().childForceExpandHeight = false;
            content.GetComponent<VerticalLayoutGroup>().childForceExpandWidth = false;
            content.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.UpperLeft;
            content.GetComponent<VerticalLayoutGroup>().reverseArrangement = false;

            content.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            content.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            #endregion

            #region Scrollbar Horizontal
            var horiz = new GameObject("Scrollbar Horizontal");
            horiz.AddComponent<RectTransform>();
            horiz.AddComponent<UnityEngine.UI.Image>();
            horiz.AddComponent<Scrollbar>();
            horiz.transform.SetParent(go.transform);
            rt = horiz.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(-100f, -140f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(-17f, 20f);
            rt.offsetMin = new Vector2(0f, 0f);
            rt.anchorMax = new Vector2(1f, 0f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(-17f, 20f);
            rt.pivot = new Vector2(0f, 0f);

            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "Background")
                {
                    horiz.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }
            horiz.GetComponent<UnityEngine.UI.Image>().type = UnityEngine.UI.Image.Type.Sliced;
            horiz.GetComponent<UnityEngine.UI.Image>().maskable = true;
            horiz.GetComponent<UnityEngine.UI.Image>().fillCenter = true;
            horiz.GetComponent<UnityEngine.UI.Image>().pixelsPerUnitMultiplier = 1f;

            var hsliding = new GameObject("Sliding Area");
            hsliding.AddComponent<RectTransform>();
            hsliding.transform.SetParent(horiz.transform);
            rt = hsliding.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(91.5f, 10f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(-10f, -10f);
            rt.offsetMin = new Vector2(10f, 10f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(-20f, 20f);
            rt.pivot = new Vector2(0.5f, 0.5f);

            var hhandle = new GameObject("Handle");
            hhandle.AddComponent<RectTransform>();
            hhandle.AddComponent<UnityEngine.UI.Image>();
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "UISprite")
                {
                    hhandle.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }
            hhandle.GetComponent<UnityEngine.UI.Image>().type = UnityEngine.UI.Image.Type.Sliced;
            hhandle.GetComponent<UnityEngine.UI.Image>().maskable = true;
            hhandle.GetComponent<UnityEngine.UI.Image>().fillCenter = true;
            hhandle.GetComponent<UnityEngine.UI.Image>().pixelsPerUnitMultiplier = 1f;
            hhandle.transform.SetParent(hsliding.transform);
            rt = hhandle.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(0f, 0f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(10f, 10f);
            rt.offsetMin = new Vector2(-10f, -10f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(20f, 20f);
            rt.pivot = new Vector2(0.5f, 0.5f);

            #endregion

            #region Scrollbar Vertical
            var verti = new GameObject("Scrollbar Vertical");
            verti.AddComponent<RectTransform>();
            verti.AddComponent<UnityEngine.UI.Image>();
            verti.AddComponent<Scrollbar>();
            verti.transform.SetParent(go.transform);
            rt = verti.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(100f, 140f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(0f, 0f);
            rt.offsetMin = new Vector2(-20f, 17f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(1f, 0f);
            rt.sizeDelta = new Vector2(20f, -17f);
            rt.pivot = new Vector2(1f, 1f);

            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "Background")
                {
                    verti.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }
            verti.GetComponent<UnityEngine.UI.Image>().type = UnityEngine.UI.Image.Type.Sliced;
            verti.GetComponent<UnityEngine.UI.Image>().maskable = true;
            verti.GetComponent<UnityEngine.UI.Image>().fillCenter = true;
            verti.GetComponent<UnityEngine.UI.Image>().pixelsPerUnitMultiplier = 1f;

            var vsliding = new GameObject("Sliding Area");
            vsliding.AddComponent<RectTransform>();
            vsliding.transform.SetParent(verti.transform);
            rt = vsliding.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(-10f, -132f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(-10f, -10f);
            rt.offsetMin = new Vector2(10f, 10f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(-20f, -20f);
            rt.pivot = new Vector2(0.5f, 0.5f);

            var vhandle = new GameObject("Handle");
            vhandle.AddComponent<RectTransform>();
            vhandle.AddComponent<UnityEngine.UI.Image>();
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "UISprite")
                {
                    vhandle.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }
            vhandle.GetComponent<UnityEngine.UI.Image>().type = UnityEngine.UI.Image.Type.Sliced;
            vhandle.GetComponent<UnityEngine.UI.Image>().maskable = true;
            vhandle.GetComponent<UnityEngine.UI.Image>().fillCenter = true;
            vhandle.GetComponent<UnityEngine.UI.Image>().pixelsPerUnitMultiplier = 1f;
            vhandle.transform.SetParent(vsliding.transform);
            rt = vhandle.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(0f, 41.5f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(10f, 10f);
            rt.offsetMin = new Vector2(-10f, -10f);
            rt.anchorMax = new Vector2(1f, 0.34055f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(20f, 20f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            #endregion

            horiz.GetComponent<Scrollbar>().handleRect = hhandle.GetComponent<RectTransform>();
            horiz.GetComponent<Scrollbar>().targetGraphic = hhandle.GetComponent<UnityEngine.UI.Image>();

            verti.GetComponent<Scrollbar>().handleRect = vhandle.GetComponent<RectTransform>();
            verti.GetComponent<Scrollbar>().targetGraphic = vhandle.GetComponent<UnityEngine.UI.Image>();

            go.GetComponent<ScrollRect>().content = content.GetComponent<RectTransform>();
            go.GetComponent<ScrollRect>().viewport = viewport.GetComponent<RectTransform>();
            go.GetComponent<ScrollRect>().horizontalScrollbar = horiz.GetComponent<Scrollbar>();
            go.GetComponent<ScrollRect>().verticalScrollbar = verti.GetComponent<Scrollbar>();
            go.GetComponent<ScrollRect>().horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            go.GetComponent<ScrollRect>().verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            go.GetComponent<ScrollRect>().verticalScrollbarSpacing = -3;
            go.GetComponent<ScrollRect>().horizontalScrollbarSpacing = -3;

            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().Rect = go.GetComponent<RectTransform>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().ScrollRect = go.GetComponent<ScrollRect>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().ScrollBackground = go.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().HorizontalBar = horiz.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().VerticalBar = verti.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().HorizontalHandle = hhandle.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().VerticalHandle = vhandle.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().ContentObject = content;
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().Viewport = viewport;
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().Horizontal = horiz.GetComponent<Scrollbar>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().Vertical = verti.GetComponent<Scrollbar>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().layoutgroup = content.GetComponent<VerticalLayoutGroup>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().fitter = content.GetComponent<ContentSizeFitter>();
        }

#endif
        #endregion

        public void HandleEvent()
        {
            //execute the appropriate script for the associated event passed to this Control

            //var p = MiniScriptSingleton.Scene.FindParent(this);
            //MiniScriptSingleton.EventSink.HandleEvent(p, eventData);
            throw new NotImplementedException();
        }
    }
}

