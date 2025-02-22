using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using MiniScript;
using MiniScript.MSGS;
using System;
using System.Data;
using MiniScript.MSGS.MUUI.Extensions;
using UnityEditor;

namespace MiniScript.MSGS.MUUI.TwoDimensional
{
    [RequireComponent(typeof(UnityEngine.UI.Image))]
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public class Button : MonoBehaviour, UI_Element, IControl, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler
    {
        float mscroll;
        bool benter;
        public RectTransform rect;
        public UnityEngine.UI.Image image;
        public TextMeshProUGUI Text;
        public string ScriptOnEnter, ScriptOnExit, ScriptOnLeftClick, ScriptOnScrollUp, ScriptOnScrollDown,
            ScriptOnDoubleLeftClick, ScriptOnRightClick, ScriptOnDoubleRightClick, ScriptOnMiddleClick;

        
        List<int> events = new List<int>();
        #region public const int event declarations
        public const int ButtonMouseEnter = 1100;
        public const int ButtonMouseExit = 1101;
        public const int ButtonMouseLeftClick = 1102;
        public const int ButtonMouseRightClick = 1103;
        public const int ButtonMouseMiddleClick = 1104;
        public const int ButtonMouseDoubleLeftClick = 1105;
        public const int ButtonMouseDoubleRightClick = 1106;
        public const int ButtonMouseScrollUp = 1107;
        public const int ButtonMouseScrollDown = 1108;
        #endregion

        public ControlCollectionEventFilter eventFilter { get; set; }

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
                    tmp.Value = mscroll;
                    tmp.Element = this.Name;
                    tmp.ElementType = UIElementType.Button;
                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
                if (mscroll < 0)
                {
                    InputEvent tmp = new InputEvent();
                    tmp.OnScrollDown = true;
                    tmp.Value = mscroll;
                    tmp.Element = this.Name;
                    tmp.ElementType = UIElementType.Button;
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
            get { return UIElementType.Button; }
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

            ValMap r = rect.ToValMap();
            r.assignOverride += new ValMap.AssignOverrideFunc(OnRectUpdate);
            properties.map.Add(new ValString("Rect"), r);

            r = this.ToValMap();
            r.assignOverride += new ValMap.AssignOverrideFunc(OnButtonUpdate);
            properties.map.Add(new ValString("Button"), r);



            return properties;
        }

        bool OnRectUpdate(Value a, Value b)
        {
            return rect.UpdateRect(a, b);
        }

        bool OnButtonUpdate(Value a, Value b)
        {
            return this.UpdateButton(a, b);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            benter = true;
            InputEvent tmp = new InputEvent();
            tmp.OnEnter = true;
            tmp.Element = this.Name;
            tmp.ElementType = UIElementType.Button;
            MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            benter = false;
            InputEvent tmp = new InputEvent();
            tmp.OnExit = true;
            tmp.ElementType = UIElementType.Button;
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
                        ElementType = UIElementType.Button,
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
                        ElementType = UIElementType.Button,
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
                        ElementType = UIElementType.Button,
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
                        ElementType = UIElementType.Button,
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
                    ElementType = UIElementType.Button,
                    ScriptName = ScriptOnMiddleClick
                };

                MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
            }
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {

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

        #region Editor Extensions
#if UNITY_EDITOR
        [MenuItem("GameObject/MSGS/Add/Button", false, 0)]
        private static void AddConfig()
        {
            var go = Selection.activeObject as GameObject;
            if(go == null) { return; }
            go.AddComponent<MiniScript.MSGS.MUUI.TwoDimensional.Button > ();
            //make sure the references are put in place
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Button>().image = go.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Button>().rect = go.GetComponent<UnityEngine.RectTransform>();
            var child = new GameObject("Text (TMP)");
            child.transform.SetParent(go.transform);
            child.AddComponent<TextMeshProUGUI>();
            child.transform.localPosition = new Vector3(0, 0, 0);
            child.transform.localScale = new Vector3(1, 1, 1);
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Button>().Text = child.GetComponent<TextMeshProUGUI>();
        }

        [MenuItem("GameObject/MSGS/Create/Button", false, 0)]
        private static void CreateButton()
        {
            var go = new GameObject("Button");
            var parent = Selection.activeObject as GameObject;
            go.transform.SetParent(parent.transform);
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.AddComponent<MiniScript.MSGS.MUUI.TwoDimensional.Button>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Button>().image = go.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Button>().rect = go.GetComponent<UnityEngine.RectTransform>();

            var child = new GameObject("Text (TMP)");
            child.transform.SetParent(go.transform);
            child.AddComponent<TextMeshProUGUI>();
            child.transform.localPosition = new Vector3(0, 0, 0);
            child.transform.localScale = new Vector3(1, 1, 1);
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Button>().Text = child.GetComponent<TextMeshProUGUI>();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }

        


#endif
        #endregion
    }
}

