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
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public class CanvasButton : MonoBehaviour, IControl, UI_Element,
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerEnterHandler,
    IPointerExitHandler,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IScrollHandler
    {   
        public UnityEngine.UI.Button button;
        public RectTransform rect;
        public UnityEngine.UI.Image image;
        public TextMeshProUGUI Text;
        public string ScriptOnEnter, ScriptOnExit, ScriptOnLeftClick, ScriptOnScrollUp, ScriptOnScrollDown,
            ScriptOnDoubleLeftClick, ScriptOnRightClick, ScriptOnDoubleRightClick, ScriptOnMiddleClick;
        float mscroll;
        bool benter;

        void Awake()
        {
            //button = GetComponent<Button>();
        }

        void Update()
        {

        }

        #region
        // IPointerClickHandler
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Pointer Clicked on Button!");
            eventData.Use(); // Consumes the event
        }

        // IPointerDownHandler
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Pointer Down on Button!");
            eventData.Use();
        }

        // IPointerUpHandler
        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Pointer Up on Button!");
            eventData.Use();
        }

        // IPointerEnterHandler
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Pointer Entered Button!");
            //eventData.Use();
        }

        // IPointerExitHandler
        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("Pointer Exited Button!");
            //eventData.Use();
        }

        // IBeginDragHandler
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Begin Drag on Button!");
            eventData.Use();
        }

        // IDragHandler
        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("Dragging on Button!");
            eventData.Use();
        }

        // IEndDragHandler
        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("End Drag on Button!");
            eventData.Use();
        }

        // IScrollHandler
        public void OnScroll(PointerEventData eventData)
        {
            Debug.Log("Scroll on Button!");
            eventData.Use();
        }
        #endregion

        #region MUUI Control interface
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

            //r = button.ToValMap();
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
        #endregion

    }
}
