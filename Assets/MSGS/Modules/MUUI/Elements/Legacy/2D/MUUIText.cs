using MiniScript.MSGS.MUUI.Extensions;
using MiniScript.MSGS.Unity;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniScript.MSGS.MUUI.TwoDimensional
{
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    public class MUUIText : MonoBehaviour, UI_Element, IControl, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        float mscroll;
        bool benter;
        public RectTransform Rect;
        public TextMeshProUGUI mText;
        ValMap properties;
        public string ScriptOnEnter, ScriptOnExit, ScriptOnLeftClick, ScriptOnScrollUp, ScriptOnScrollDown,
            ScriptOnDoubleLeftClick, ScriptOnRightClick, ScriptOnDoubleRightClick, ScriptOnMiddleClick;

        List<int> events = new List<int>();
        #region public const int event declarations
        public const int TextMouseEnter = 1800;
        public const int TextMouseExit = 1801;
        public const int TextMouseLeftClick = 1802;
        public const int TextMouseRightClick = 1803;
        public const int TextMouseMiddleClick = 1804;
        public const int TextMouseDoubleLeftClick = 1805;
        public const int TextMouseDoubleRightClick = 1806;
        public const int TextMouseScrollUp = 1807;
        public const int TextMouseScrollDown = 1808;
        #endregion

        public ControlCollectionEventFilter eventFilter { get; set; }

        void Awake() { }
        void Start()
        {
            //default to all events unless overridden
            eventFilter = new ControlCollectionEventFilter()
            {
                events = new List<int>()
                {
                    TextMouseEnter,
                    TextMouseExit,
                    TextMouseLeftClick,
                    TextMouseRightClick,
                    TextMouseMiddleClick,
                    TextMouseDoubleLeftClick,
                    TextMouseDoubleRightClick,
                    TextMouseScrollUp,
                    TextMouseScrollDown,
                }
            };

            properties = GetProperties();
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
                    tmp.ElementType = UIElementType.Text;
                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
                if (mscroll < 0)
                {
                    InputEvent tmp = new InputEvent();
                    tmp.OnScrollDown = true;
                    tmp.Element = this.Name;
                    tmp.ElementType = UIElementType.Text;
                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
            }
        }

        public ValMap GetValMap
        {
            get {
                if(properties == null) { properties = GetProperties(); }
                return properties;
            }
            set { return; }
        }

        public ValMap GetProperties()
        {
            if (properties == null) { 
                properties = new ValMap();

                ValMap r = Rect.ToValMap();
                r.assignOverride += new ValMap.AssignOverrideFunc(OnRectTransformChanged);
                properties.map.Add(new ValString("Rect"), r);

                r = this.ToValMap();
                r.assignOverride += new ValMap.AssignOverrideFunc(OnTextChanged);
                properties.map.Add(new ValString("Text"), r);
            }

            return properties;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public UIElementType ObjectType
        {
            get { return UIElementType.Text; }
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

        bool OnRectTransformChanged(Value a, Value b)
        {         
            return Rect.UpdateRect(a,b);
        }
        bool OnTextChanged(Value a, Value b)
        {   
            return mText.UpdateTextMeshProUGUI(a, b);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            benter = true;
            InputEvent tmp = new InputEvent();
            tmp.OnEnter = true;
            tmp.Element = this.Name;
            tmp.ElementType = UIElementType.Text;
            MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            benter = false;
            InputEvent tmp = new InputEvent();
            tmp.OnExit = true;
            tmp.Element = this.Name;
            tmp.ElementType = UIElementType.Text;

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
                        ElementType = UIElementType.Text,
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
                        ElementType = UIElementType.Text,
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
                        ElementType = UIElementType.Text,
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
                        ElementType = UIElementType.Text,
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
                    ElementType = UIElementType.Text,
                    ScriptName = ScriptOnMiddleClick
                };

                MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
            }
        }

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        public void HandleEvent()
        {
            //execute the appropriate script for the associated event passed to this Control

            //var p = MiniScriptSingleton.Scene.FindParent(this);
            //MiniScriptSingleton.EventSink.HandleEvent(p, eventData);
            throw new NotImplementedException();
        }
    }
}

