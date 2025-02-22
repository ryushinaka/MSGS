using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MiniScript;
using UnityEngine.EventSystems;
using MiniScript.MSGS.MUUI.Extensions;
using UnityEditor;
using TMPro;
using System;

namespace MiniScript.MSGS.MUUI.TwoDimensional
{
    [RequireComponent(typeof(UnityEngine.UI.Image))]
    [RequireComponent(typeof(TMPro.TMP_Dropdown))]
    public class DropDown : MonoBehaviour, IControl, UI_Element, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        float mscroll;
        bool benter;
        public TMPro.TMP_Dropdown dropdown;
        public TMPro.TextMeshProUGUI defaultText;
        public UnityEngine.UI.Image image;
        public RectTransform rect;
        public string ScriptOnEnter, ScriptOnExit, ScriptOnLeftClick, ScriptOnScrollUp, ScriptOnScrollDown,
            ScriptOnDoubleLeftClick, ScriptOnRightClick, ScriptOnDoubleRightClick, ScriptOnMiddleClick;

        List<int> events = new List<int>();
        #region public const int event declarations
        public const int DropDownMouseEnter = 1300;
        public const int DropDownMouseExit = 1301;
        public const int DropDownMouseLeftClick = 1302;
        public const int DropDownMouseRightClick = 1303;
        public const int DropDownMouseMiddleClick = 1304;
        public const int DropDownMouseDoubleLeftClick = 1305;
        public const int DropDownMouseDoubleRightClick = 1306;
        public const int DropDownMouseScrollUp = 1307;
        public const int DropDownMouseScrollDown = 1308;
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
                    tmp.ElementType = UIElementType.DropDown;
                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
                if (mscroll < 0)
                {
                    InputEvent tmp = new InputEvent();
                    tmp.OnScrollDown = true;
                    tmp.Element = this.Name;
                    tmp.ElementType = UIElementType.DropDown;
                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
            }
        }

        ValMap properties;
        public ValMap GetValMap
        {
            get { return properties; }
            set { }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public UIElementType ObjectType
        {
            get { return UIElementType.DropDown; }
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

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        public ValMap GetProperties()
        {
            properties = new ValMap();
            ValMap r = rect.ToValMap();
            r.assignOverride += new ValMap.AssignOverrideFunc(OnRectUpdate);
            properties.map.Add(new ValString("Rect"), r);
            r = this.ToValMap();
            r.assignOverride += new ValMap.AssignOverrideFunc(OnDropDownUpdate);
            properties.map.Add(new ValString("DropDown"), r);

            return properties;
        }

        public bool OnRectUpdate(Value a, Value b)
        {
            return rect.UpdateRect(a, b);
        }

        public bool OnDropDownUpdate(Value a, Value b)
        {
            return this.UpdateDropDown(a, b);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            InputEvent tmp = new InputEvent();
            tmp.OnEnter = true;
            tmp.Element = this.Name;
            tmp.ElementType = UIElementType.DropDown;
            MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            InputEvent tmp = new InputEvent();
            tmp.OnExit = true;
            tmp.ElementType = UIElementType.DropDown;
            tmp.Element = this.Name;

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
                        ElementType = UIElementType.DropDown,
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
                        ElementType = UIElementType.DropDown,
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
                        ElementType = UIElementType.DropDown,
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
                        ElementType = UIElementType.DropDown,
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
                    ElementType = UIElementType.DropDown,
                    ScriptName = ScriptOnMiddleClick
                };

                MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
            }
        }

        void SetToDefault()
        {
            Sprite DefaultUISprite = null;
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "UISprite")
                    DefaultUISprite = sprite;
            }

            image.sprite = DefaultUISprite;
            image.type = UnityEngine.UI.Image.Type.Sliced;
        }

        List<object> GetOptions()
        {

            return null;
        }

        #region Editor Extensions
#if UNITY_EDITOR
        [MenuItem("GameObject/MSGS/Add/DropDown", false, 0)]

        private static void EditorAddDropDown()
        {
            var go = Selection.activeObject as GameObject;
            if (go == null) { return; }
            go.AddComponent<MiniScript.MSGS.MUUI.TwoDimensional.DropDown>();
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
            go.AddComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.DropDown>().rect = go.GetComponent<RectTransform>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.DropDown>().image = go.GetComponent<UnityEngine.UI.Image>();
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "UISprite")
                {
                    go.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }

            #region Label
            var label = new GameObject("Label");
            label.AddComponent<RectTransform>();
            rt = label.GetComponent<RectTransform>();
            label.transform.SetParent(go.transform);
            rt.anchoredPosition = new Vector2(-7.5f, -0.5f);
            rt.localPosition = new Vector3(-7.5f, -0.5f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(-25f, -7f);
            rt.offsetMin = new Vector2(10f, 6f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(-35f, -13f);
            rt.pivot = new Vector2(0.5f, 0.5f);

            label.AddComponent<TextMeshProUGUI>();
            var text = label.GetComponent<TextMeshProUGUI>();
            text.fontSize = 14;
            text.alignment = TextAlignmentOptions.Left | TextAlignmentOptions.Midline;
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.DropDown>().defaultText = label.GetComponent<TextMeshProUGUI>();
            #endregion

            #region Arrow
            var arrow = new GameObject("Arrow");
            arrow.AddComponent<RectTransform>();
            arrow.transform.SetParent(go.transform);
            rt = arrow.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(-15f, 0f);
            rt.localPosition = new Vector3(65f, 0f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(-5f, 10f);
            rt.offsetMin = new Vector2(-25f, -10f);
            rt.anchorMax = new Vector2(1f, 0.5f);
            rt.anchorMin = new Vector2(1f, 0.5f);
            rt.sizeDelta = new Vector2(20f, 20f);
            rt.pivot = new Vector2(0.5f, 0.5f);

            arrow.AddComponent<UnityEngine.UI.Image>();
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "DropdownArrow")
                {
                    arrow.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }
            #endregion

            #region Template
            var template = new GameObject("Template");
            template.AddComponent<RectTransform>();
            template.transform.SetParent(go.transform);
            rt = arrow.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 2f);
            rt.localPosition = new Vector3(0f, -13f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(0f, 2f);
            rt.offsetMin = new Vector2(0f, -148f);
            rt.anchorMax = new Vector2(1f, 0f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(0f, 150f);
            rt.pivot = new Vector2(0.5f, 1f);

            template.AddComponent<UnityEngine.UI.Image>();
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "UISprite")
                {
                    arrow.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }
            //template.GetComponent<UnityEngine.UI.Image>().fillCenter = true;

            template.AddComponent<ScrollRect>();
            var scroll = template.GetComponent<ScrollRect>();
            scroll.vertical = true; scroll.horizontal = false; scroll.inertia = true;
            scroll.movementType = ScrollRect.MovementType.Clamped;
            scroll.decelerationRate = 0.135f;

            var viewport = new GameObject("Viewport");
            viewport.AddComponent<RectTransform>();
            viewport.transform.SetParent(template.transform);
            rt = viewport.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(-80f, 0f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(-18f, 0f);
            rt.offsetMin = new Vector2(0f, 0f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(-18f, 0f);
            rt.pivot = new Vector2(0f, 1f);

            viewport.AddComponent<UnityEngine.UI.Image>();
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "UIMask")
                {
                    viewport.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }
            viewport.AddComponent<Mask>();

            var content = new GameObject("Content");
            content.AddComponent<RectTransform>();
            content.transform.SetParent(viewport.transform);
            rt = content.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(71f, 0f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(0f, 0f);
            rt.offsetMin = new Vector2(0f, -28f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(0f, 1f);
            rt.sizeDelta = new Vector2(0f, 28f);
            rt.pivot = new Vector2(0.5f, 1f);

            var item = new GameObject("Item");
            item.AddComponent<RectTransform>();
            item.transform.SetParent(content.transform);
            item.AddComponent<Toggle>();
            rt = item.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(0f, -14f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(0f, 10f);
            rt.offsetMin = new Vector2(0f, -10f);
            rt.anchorMax = new Vector2(1f, 0.5f);
            rt.anchorMin = new Vector2(0f, 0.5f);
            rt.sizeDelta = new Vector2(0f, 20f);
            rt.pivot = new Vector2(0.5f, 0.5f);

            var itembackground = new GameObject("Item Background");
            itembackground.AddComponent<RectTransform>();
            itembackground.AddComponent<UnityEngine.UI.Image>();
            itembackground.transform.SetParent(item.transform);
            rt = itembackground.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(0f, 0f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(0f, 0f);
            rt.offsetMin = new Vector2(0f, 0f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(0f, 0f);
            rt.pivot = new Vector2(0.5f, 0.5f);

            var itemCheckmark = new GameObject("Item Checkmark");
            itemCheckmark.AddComponent<RectTransform>();
            itemCheckmark.AddComponent<UnityEngine.UI.Image>();
            itemCheckmark.transform.SetParent(item.transform);
            rt = itemCheckmark.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(10f, 0f);
            rt.localPosition = new Vector3(-61f, 0f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(20f, 10f);
            rt.offsetMin = new Vector2(0f, -10f);
            rt.anchorMax = new Vector2(0f, 0.5f);
            rt.anchorMin = new Vector2(0f, 0.5f);
            rt.sizeDelta = new Vector2(20f, 20f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "Checkmark")
                {
                    itemCheckmark.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }

            var itemlabel = new GameObject("Item Label");
            itemCheckmark.AddComponent<RectTransform>();
            itemCheckmark.AddComponent<TextMeshProUGUI>();
            itemCheckmark.transform.SetParent(item.transform);
            rt = itemCheckmark.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(5f, -0.5f);
            rt.localPosition = new Vector3(5f, -0.5f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(-10f, -2f);
            rt.offsetMin = new Vector2(20f, 1f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(-30f, -3f);
            rt.pivot = new Vector2(0.5f, 0.5f);

            itemCheckmark.GetComponent<TextMeshProUGUI>().fontSize = 14;
            itemCheckmark.GetComponent<TextMeshProUGUI>().SetText("Option A");
            itemCheckmark.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left | TextAlignmentOptions.Midline;
            #endregion

            #region Scrollbar
            var Scrollbar = new GameObject("Scrollbar");
            Scrollbar.AddComponent<RectTransform>();
            Scrollbar.AddComponent<Scrollbar>();
            Scrollbar.AddComponent<UnityEngine.UI.Image>();
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "Background")
                {
                    Scrollbar.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }
            Scrollbar.transform.SetParent(go.transform);

            rt = Scrollbar.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(80f, 0f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(0f, 0f);
            rt.offsetMin = new Vector2(-20f, 0f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(1f, 0f);
            rt.sizeDelta = new Vector2(20f, 0f);
            rt.pivot = new Vector2(1f, 1f);

            var SlidingArea = new GameObject("Sliding Area");
            SlidingArea.AddComponent<RectTransform>();
            SlidingArea.transform.SetParent(scroll.transform);
            rt = SlidingArea.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(-10f, -75f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(-10f, -10f);
            rt.offsetMin = new Vector2(10f, 10f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(-20f, -20f);
            rt.pivot = new Vector2(0.5f, 0.5f);

            var Handle = new GameObject("Handle");
            Handle.AddComponent<RectTransform>();
            Handle.AddComponent<UnityEngine.UI.Image>();
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "UISprite")
                {
                    Handle.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }
            Handle.transform.SetParent(SlidingArea.transform);

            rt = Handle.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(0f, -52f, 0f);
            rt.localScale = new Vector3(1, 1, 1);
            rt.offsetMax = new Vector2(10f, 10f);
            rt.offsetMin = new Vector2(-10f, -10f);
            rt.anchorMax = new Vector2(1f, 0.2f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(20f, 20f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            #endregion
        }


        [MenuItem("GameObject/MSGS/Create/DropDown", false, 0)]

        private static void EditorCreateDropDown()
        {
            if (Selection.activeObject != null && Selection.activeObject is GameObject)
            {
                var go = new GameObject("DropDown");
                go.transform.SetParent(((GameObject)Selection.activeObject).transform);
                go.AddComponent<RectTransform>();
                go.AddComponent<MiniScript.MSGS.MUUI.TwoDimensional.DropDown>();
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
                go.AddComponent<UnityEngine.UI.Image>();
                go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.DropDown>().dropdown = go.GetComponent<TMPro.TMP_Dropdown>();
                go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.DropDown>().rect = go.GetComponent<RectTransform>();
                go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.DropDown>().image = go.GetComponent<UnityEngine.UI.Image>();
                foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                {
                    if (sprite.name == "UISprite")
                    {
                        go.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                    }
                }

                #region Label
                var label = new GameObject("Label");
                label.AddComponent<RectTransform>();
                rt = label.GetComponent<RectTransform>();
                label.transform.SetParent(go.transform);
                rt.anchoredPosition = new Vector2(-7.5f, -0.5f);
                rt.localPosition = new Vector3(-7.5f, -0.5f, 0f);
                rt.localScale = new Vector3(1, 1, 1);
                rt.offsetMax = new Vector2(-25f, -7f);
                rt.offsetMin = new Vector2(10f, 6f);
                rt.anchorMax = new Vector2(1f, 1f);
                rt.anchorMin = new Vector2(0f, 0f);
                rt.sizeDelta = new Vector2(-35f, -13f);
                rt.pivot = new Vector2(0.5f, 0.5f);

                label.AddComponent<TextMeshProUGUI>();
                var text = label.GetComponent<TextMeshProUGUI>();
                text.fontSize = 14;
                text.alignment = TextAlignmentOptions.Left | TextAlignmentOptions.Midline;
                go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.DropDown>().defaultText = label.GetComponent<TextMeshProUGUI>();
                #endregion

                #region Arrow
                var arrow = new GameObject("Arrow");
                arrow.AddComponent<RectTransform>();
                arrow.transform.SetParent(go.transform);
                rt = arrow.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(-15f, 0f);
                rt.localPosition = new Vector3(65f, 0f, 0f);
                rt.localScale = new Vector3(1, 1, 1);
                rt.offsetMax = new Vector2(-5f, 10f);
                rt.offsetMin = new Vector2(-25f, -10f);
                rt.anchorMax = new Vector2(1f, 0.5f);
                rt.anchorMin = new Vector2(1f, 0.5f);
                rt.sizeDelta = new Vector2(20f, 20f);
                rt.pivot = new Vector2(0.5f, 0.5f);

                arrow.AddComponent<UnityEngine.UI.Image>();
                foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                {
                    if (sprite.name == "DropdownArrow")
                    {
                        arrow.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                    }
                }
                #endregion

                #region Template
                var template = new GameObject("Template");
                template.AddComponent<RectTransform>();
                template.transform.SetParent(go.transform);
                rt = template.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(0f, 2f);
                rt.localPosition = new Vector3(0f, -13f, 0f);
                rt.localScale = new Vector3(1, 1, 1);
                rt.offsetMax = new Vector2(0f, 2f);
                rt.offsetMin = new Vector2(0f, -148f);
                rt.anchorMax = new Vector2(1f, 0f);
                rt.anchorMin = new Vector2(0f, 0f);
                rt.sizeDelta = new Vector2(0f, 150f);
                rt.pivot = new Vector2(0.5f, 1f);

                template.AddComponent<UnityEngine.UI.Image>();
                foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                {
                    if (sprite.name == "UISprite")
                    {
                        arrow.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                    }
                }
                //template.GetComponent<UnityEngine.UI.Image>().fillCenter = true;

                template.AddComponent<ScrollRect>();
                var scroll = template.GetComponent<ScrollRect>();
                scroll.vertical = true; scroll.horizontal = false; scroll.inertia = true;
                scroll.movementType = ScrollRect.MovementType.Clamped;
                scroll.decelerationRate = 0.135f;

                var viewport = new GameObject("Viewport");
                viewport.AddComponent<RectTransform>();
                viewport.transform.SetParent(template.transform);
                rt = viewport.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(0f, 0f);
                rt.localPosition = new Vector3(-80f, 0f, 0f);
                rt.localScale = new Vector3(1, 1, 1);
                rt.offsetMax = new Vector2(-18f, 0f);
                rt.offsetMin = new Vector2(0f, 0f);
                rt.anchorMax = new Vector2(1f, 1f);
                rt.anchorMin = new Vector2(0f, 0f);
                rt.sizeDelta = new Vector2(-18f, 0f);
                rt.pivot = new Vector2(0f, 1f);

                viewport.AddComponent<UnityEngine.UI.Image>();
                foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                {
                    if (sprite.name == "UIMask")
                    {
                        viewport.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                    }
                }
                viewport.AddComponent<Mask>();

                var content = new GameObject("Content");
                content.AddComponent<RectTransform>();
                content.transform.SetParent(viewport.transform);
                rt = content.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(0f, 0f);
                rt.localPosition = new Vector3(71f, 0f, 0f);
                rt.localScale = new Vector3(1, 1, 1);
                rt.offsetMax = new Vector2(0f, 0f);
                rt.offsetMin = new Vector2(0f, -28f);
                rt.anchorMax = new Vector2(1f, 1f);
                rt.anchorMin = new Vector2(0f, 1f);
                rt.sizeDelta = new Vector2(0f, 28f);
                rt.pivot = new Vector2(0.5f, 1f);

                var item = new GameObject("Item");
                item.AddComponent<RectTransform>();
                item.transform.SetParent(content.transform);
                item.AddComponent<UnityEngine.UI.Toggle>();
                rt = item.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(0f, 0f);
                rt.localPosition = new Vector3(0f, -14f, 0f);
                rt.localScale = new Vector3(1, 1, 1);
                rt.offsetMax = new Vector2(0f, 10f);
                rt.offsetMin = new Vector2(0f, -10f);
                rt.anchorMax = new Vector2(1f, 0.5f);
                rt.anchorMin = new Vector2(0f, 0.5f);
                rt.sizeDelta = new Vector2(0f, 20f);
                rt.pivot = new Vector2(0.5f, 0.5f);

                var itembackground = new GameObject("Item Background");
                itembackground.AddComponent<RectTransform>();
                itembackground.AddComponent<UnityEngine.UI.Image>();
                itembackground.transform.SetParent(item.transform);
                rt = itembackground.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(0f, 0f);
                rt.localPosition = new Vector3(0f, 0f, 0f);
                rt.localScale = new Vector3(1, 1, 1);
                rt.offsetMax = new Vector2(0f, 0f);
                rt.offsetMin = new Vector2(0f, 0f);
                rt.anchorMax = new Vector2(1f, 1f);
                rt.anchorMin = new Vector2(0f, 0f);
                rt.sizeDelta = new Vector2(0f, 0f);
                rt.pivot = new Vector2(0.5f, 0.5f);

                var itemCheckmark = new GameObject("Item Checkmark");
                itemCheckmark.AddComponent<RectTransform>();
                itemCheckmark.AddComponent<UnityEngine.UI.Image>();
                itemCheckmark.transform.SetParent(item.transform);
                rt = itemCheckmark.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(10f, 0f);
                rt.localPosition = new Vector3(-61f, 0f, 0f);
                rt.localScale = new Vector3(1, 1, 1);
                rt.offsetMax = new Vector2(20f, 10f);
                rt.offsetMin = new Vector2(0f, -10f);
                rt.anchorMax = new Vector2(0f, 0.5f);
                rt.anchorMin = new Vector2(0f, 0.5f);
                rt.sizeDelta = new Vector2(20f, 20f);
                rt.pivot = new Vector2(0.5f, 0.5f);
                foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                {
                    if (sprite.name == "Checkmark")
                    {
                        itemCheckmark.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                    }
                }

                var itemlabel = new GameObject("Item Label");
                itemlabel.AddComponent<RectTransform>();
                itemlabel.AddComponent<TextMeshProUGUI>();
                itemlabel.transform.SetParent(item.transform);
                rt = itemlabel.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(5f, -0.5f);
                rt.localPosition = new Vector3(5f, -0.5f, 0f);
                rt.localScale = new Vector3(1, 1, 1);
                rt.offsetMax = new Vector2(-10f, -2f);
                rt.offsetMin = new Vector2(20f, 1f);
                rt.anchorMax = new Vector2(1f, 1f);
                rt.anchorMin = new Vector2(0f, 0f);
                rt.sizeDelta = new Vector2(-30f, -3f);
                rt.pivot = new Vector2(0.5f, 0.5f);

                itemlabel.GetComponent<TextMeshProUGUI>().fontSize = 14;
                itemlabel.GetComponent<TextMeshProUGUI>().SetText("Option A");
                itemlabel.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left | TextAlignmentOptions.Midline;
                itemlabel.GetComponent<TextMeshProUGUI>().color = new Color(50f, 50f, 50f);
                #endregion

                #region Scrollbar
                var Scrollbar = new GameObject("Scrollbar");
                Scrollbar.AddComponent<RectTransform>();
                Scrollbar.AddComponent<Scrollbar>();
                Scrollbar.AddComponent<UnityEngine.UI.Image>();
                foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                {
                    if (sprite.name == "Background")
                    {
                        Scrollbar.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                    }
                }
                Scrollbar.transform.SetParent(template.transform);

                rt = Scrollbar.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(0f, 0f);
                rt.localPosition = new Vector3(80f, 0f, 0f);
                rt.localScale = new Vector3(1, 1, 1);
                rt.offsetMax = new Vector2(0f, 0f);
                rt.offsetMin = new Vector2(-20f, 0f);
                rt.anchorMax = new Vector2(1f, 1f);
                rt.anchorMin = new Vector2(1f, 0f);
                rt.sizeDelta = new Vector2(20f, 0f);
                rt.pivot = new Vector2(1f, 1f);

                var SlidingArea = new GameObject("Sliding Area");
                SlidingArea.AddComponent<RectTransform>();
                SlidingArea.transform.SetParent(Scrollbar.transform);
                rt = SlidingArea.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(0f, 0f);
                rt.localPosition = new Vector3(-10f, -75f, 0f);
                rt.localScale = new Vector3(1, 1, 1);
                rt.offsetMax = new Vector2(-10f, -10f);
                rt.offsetMin = new Vector2(10f, 10f);
                rt.anchorMax = new Vector2(1f, 1f);
                rt.anchorMin = new Vector2(0f, 0f);
                rt.sizeDelta = new Vector2(-20f, -20f);
                rt.pivot = new Vector2(0.5f, 0.5f);

                var Handle = new GameObject("Handle");
                Handle.AddComponent<RectTransform>();
                Handle.AddComponent<UnityEngine.UI.Image>();
                foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                {
                    if (sprite.name == "UISprite")
                    {
                        Handle.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                    }
                }
                Handle.transform.SetParent(SlidingArea.transform);

                rt = Handle.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(0f, 0f);
                rt.localPosition = new Vector3(0f, -52f, 0f);
                rt.localScale = new Vector3(1, 1, 1);
                rt.offsetMax = new Vector2(10f, 10f);
                rt.offsetMin = new Vector2(-10f, -10f);
                rt.anchorMax = new Vector2(1f, 0.2f);
                rt.anchorMin = new Vector2(0f, 0f);
                rt.sizeDelta = new Vector2(20f, 20f);
                rt.pivot = new Vector2(0.5f, 0.5f);
                #endregion

                template.SetActive(false);
                viewport.GetComponent<UnityEngine.UI.Image>().fillCenter = true;
                viewport.GetComponent<UnityEngine.UI.Image>().maskable = true;
                viewport.GetComponent<UnityEngine.UI.Image>().type = UnityEngine.UI.Image.Type.Sliced;
                viewport.GetComponent<UnityEngine.UI.Mask>().showMaskGraphic = true;
                item.GetComponent<UnityEngine.UI.Toggle>().isOn = true;
                item.GetComponent<UnityEngine.UI.Toggle>().graphic = itemCheckmark.GetComponent<UnityEngine.UI.Image>();
                item.GetComponent<UnityEngine.UI.Toggle>().targetGraphic = itembackground.GetComponent<UnityEngine.UI.Image>();
            }
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

