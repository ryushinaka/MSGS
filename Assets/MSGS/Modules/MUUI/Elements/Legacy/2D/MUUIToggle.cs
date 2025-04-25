using MiniScript.MSGS.MUUI.Extensions;
using MiniScript.MSGS.ScriptableObjects;
using MiniScript.MSGS.Unity;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace MiniScript.MSGS.MUUI.TwoDimensional
{
    [RequireComponent(typeof(UnityEngine.UI.Toggle))]
    public class MUUIToggle : MonoBehaviour, IControl, UI_Element, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        float mscroll;
        bool benter;
        public UnityEngine.UI.Toggle toggle;
        public UnityEngine.UI.Image Background, Checkmark;
        public TMPro.TextMeshProUGUI Label;
        public RectTransform rect;
        public bool rtl = false;

        public MiniScriptScriptAsset ScriptOnEnterMSA, ScriptOnExitMSA, ScriptOnLeftClickMSA, ScriptOnScrollUpMSA, ScriptOnScrollDownMSA,
           ScriptOnDoubleLeftClickMSA, ScriptOnRightClickMSA, ScriptOnDoubleRightClickMSA, ScriptOnMiddleClickMSA;

        public string ScriptOnToggle, ScriptOnEnter, ScriptOnExit, ScriptOnLeftClick, ScriptOnScrollUp, ScriptOnScrollDown,
            ScriptOnDoubleLeftClick, ScriptOnRightClick, ScriptOnDoubleRightClick, ScriptOnMiddleClick;

        List<int> events = new List<int>();
        #region public const int event declarations
        public const int ToggleMouseEnter = 1900;
        public const int ToggleMouseExit = 1901;
        public const int ToggleMouseLeftClick = 1902;
        public const int ToggleMouseRightClick = 1903;
        public const int ToggleMouseMiddleClick = 1904;
        public const int ToggleMouseDoubleLeftClick = 1905;
        public const int ToggleMouseDoubleRightClick = 1906;
        public const int ToggleMouseScrollUp = 1907;
        public const int ToggleMouseScrollDown = 1908;
        #endregion

        public ControlCollectionEventFilter eventFilter { get; set; }

        ValMap properties;
        void Awake()
        {
            properties = GetProperties();
        }

        void Start()
        {
            //default to all events unless overridden
            eventFilter = new ControlCollectionEventFilter()
            {
                events = new List<int>()
                {
                    ToggleMouseEnter,
                    ToggleMouseExit,
                    ToggleMouseLeftClick,
                    ToggleMouseRightClick,
                    ToggleMouseMiddleClick,
                    ToggleMouseDoubleLeftClick,
                    ToggleMouseDoubleRightClick,
                    ToggleMouseScrollUp,
                    ToggleMouseScrollDown,
                }
            };
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
                    tmp.ElementType = UIElementType.Toggle;
                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
                if (mscroll < 0)
                {
                    InputEvent tmp = new InputEvent();
                    tmp.OnScrollDown = true;
                    tmp.Element = this.Name;
                    tmp.ElementType = UIElementType.Toggle;
                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
            }
        }

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
            get { return UIElementType.Toggle; }
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
            if(properties == null)
            {
                properties = new ValMap();
                ValMap r = rect.ToValMap();
                r.assignOverride += new ValMap.AssignOverrideFunc(OnRectUpdate);
                properties.map.Add(new ValString("Rect"), r);
                r = this.ToValMap();
                r.assignOverride += new ValMap.AssignOverrideFunc(OnToggleUpdate);
                properties.map.Add(new ValString("Toggle"), r);
            }

            return properties;
        }

        public void OnToggleStateChange(bool value)
        {
            InputEvent tmp = new InputEvent();
            tmp.ClickLeft = true;
            tmp.Element = this.Name;
            tmp.ElementType = UIElementType.Toggle;
            MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
        }

        public bool OnRectUpdate(Value a, Value b)
        {
            return rect.UpdateRect(a, b);
        }

        public bool OnToggleUpdate(Value a, Value b)
        {
            return this.UpdateToggle(a, b);
        }

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            InputEvent tmp = new InputEvent();
            tmp.OnEnter = true;
            tmp.Element = this.Name;
            tmp.ElementType = UIElementType.Toggle;
            MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            InputEvent tmp = new InputEvent();
            tmp.OnExit = true;
            tmp.ElementType = UIElementType.Toggle;
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
                        ElementType = UIElementType.Toggle,
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
                        ElementType = UIElementType.Toggle,
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
                        ElementType = UIElementType.Toggle,
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
                        ElementType = UIElementType.Toggle,
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
                    ElementType = UIElementType.Toggle,
                    ScriptName = ScriptOnMiddleClick
                };

                MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
            }
        }

        public void HandleEvent()
        {
            //execute the appropriate script for the associated event passed to this Control

            //var p = MiniScriptSingleton.Scene.FindParent(this);
            //MiniScriptSingleton.EventSink.HandleEvent(p, eventData);
            throw new NotImplementedException();
        }

        /// <summary>
        /// pass in 'left' or 'right' as the argument for direction
        /// </summary>
        /// <param name="dir"></param>
        public void ApplyDirection(string dir)
        {
            //this function is the product of ChatGPT and as such might be entirely unreliable
            Toggle toggle = GetComponent<Toggle>();
            if (!toggle || !toggle.graphic || !toggle.targetGraphic) return;

            // Graphic is usually the "Background"
            RectTransform graphicRect = toggle.graphic.rectTransform;
            // Label is either TMP_Text or regular Text
            RectTransform labelRect = null;

            var texts = GetComponentsInChildren<MUUIText>(true);
            if (texts.Length > 0) labelRect = texts[0].Rect;

            var tmps = GetComponentsInChildren<TMP_Text>(true);
            if (tmps.Length > 0) labelRect = tmps[0].rectTransform;

            if (labelRect == null || graphicRect == null) return;

            // Move elements to opposite sides
            float spacing = 10f;

            graphicRect.anchorMin = new Vector2(0, 0.5f);
            graphicRect.anchorMax = new Vector2(0, 0.5f);
            graphicRect.pivot = new Vector2(0, 0.5f);

            labelRect.anchorMin = new Vector2(0, 0.5f);
            labelRect.anchorMax = new Vector2(0, 0.5f);
            labelRect.pivot = new Vector2(0, 0.5f);

            if (dir == "left")
            {
                graphicRect.anchoredPosition = new Vector2(0, 0);
                labelRect.anchoredPosition = new Vector2(graphicRect.sizeDelta.x + spacing, 0);
            }
            else if(dir == "right")
            {
                labelRect.anchoredPosition = new Vector2(0, 0);
                graphicRect.anchoredPosition = new Vector2(labelRect.sizeDelta.x + spacing, 0);
            }
        }

        [Button, Tooltip("Alternates between Left to Right and Right to Left")]
        public void FlipDirection()
        {
            rtl = !rtl;
            if(rtl) { ApplyDirection("right"); }
            else { ApplyDirection("left"); }
        }
    }
}
