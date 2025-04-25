using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MiniScript.MSGS.MUUI.Extensions;
using System;
using MiniScript.MSGS.Unity;

namespace MiniScript.MSGS.MUUI.TwoDimensional
{
    [RequireComponent(typeof(UnityEngine.UI.Image))]
    public class MUUIImage : MonoBehaviour, UI_Element, IControl, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        float mscroll;
        bool benter;
        public UnityEngine.UI.Image image;
        public RectTransform Rect;
        public string ScriptOnEnter, ScriptOnExit, ScriptOnLeftClick, ScriptOnScrollUp, ScriptOnScrollDown,
            ScriptOnDoubleLeftClick, ScriptOnRightClick, ScriptOnDoubleRightClick, ScriptOnMiddleClick;

        List<int> events = new List<int>();
        #region public const int event declarations
        public const int ImageMouseEnter = 1400;
        public const int ImageMouseExit = 1401;
        public const int ImageMouseLeftClick = 1402;
        public const int ImageMouseRightClick = 1403;
        public const int ImageMouseMiddleClick = 1404;
        public const int ImageMouseDoubleLeftClick = 1405;
        public const int ImageMouseDoubleRightClick = 1406;
        public const int ImageMouseScrollUp = 1407;
        public const int ImageMouseScrollDown = 1408;
        #endregion

        public ControlCollectionEventFilter eventFilter { get; set; }

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
                    ImageMouseEnter,
                    ImageMouseExit,
                    ImageMouseLeftClick,
                    ImageMouseRightClick,
                    ImageMouseMiddleClick,
                    ImageMouseDoubleLeftClick,
                    ImageMouseDoubleRightClick,
                    ImageMouseScrollUp,
                    ImageMouseScrollDown,
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
                    tmp.ElementType = UIElementType.Image;
                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
                if (mscroll < 0)
                {
                    InputEvent tmp = new InputEvent();
                    tmp.OnScrollDown = true;
                    tmp.Element = this.Name;
                    tmp.ElementType = UIElementType.Image;
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
            get { return UIElementType.Image; }
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

                ValMap a = Rect.ToValMap();
                a.assignOverride += new ValMap.AssignOverrideFunc(OnUpdateRectTransform);
                properties.map.Add(new ValString("rect"), a);

                a = this.ToValMap();
                a.assignOverride += new ValMap.AssignOverrideFunc(OnUpdateImage);
                properties.map.Add(new ValString("image"), a);
            }

            return properties;
        }

        bool OnUpdateRectTransform(Value a, Value b)
        {
            return Rect.UpdateRect(a, b);
        }
        bool OnUpdateImage(Value a, Value b)
        {
            return this.UpdateImage(a, b);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            benter = true;
            InputEvent tmp = new InputEvent();
            tmp.OnEnter = true;
            tmp.Element = this.Name;
            tmp.ElementType = UIElementType.Image;
            MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            benter = false;
            InputEvent tmp = new InputEvent();
            tmp.OnExit = true;
            tmp.Element = this.Name;
            tmp.ElementType = UIElementType.Image;

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
                        ElementType = UIElementType.Image,
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
                        ElementType = UIElementType.Image,
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
                        ElementType = UIElementType.Image,
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
                        ElementType = UIElementType.Image,
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
                    ElementType = UIElementType.Image,
                    ScriptName = ScriptOnMiddleClick
                };

                MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
            }
        }

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        #region Editor Extensions
#if UNITY_EDITOR
        [MenuItem("GameObject/MSGS/Add/Image", false, 0)]

        private static void EditorAddImage()
        {
            var go = Selection.activeObject as GameObject;
            if (go == null) { return; }
            go.AddComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIImage>();
            //make sure the references are put in place
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIImage>().image = go.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIImage>().Rect = go.GetComponent<UnityEngine.RectTransform>();

            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "UISprite")
                {
                    go.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }
        }


        [MenuItem("GameObject/MSGS/Create/Image", false, 0)]
        private static void EditorCreateImage()
        {
            var go = new GameObject("Image");
            var parent = Selection.activeObject as GameObject;
            go.transform.SetParent(parent.transform);
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.AddComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIImage>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIImage>().image = go.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIImage>().Rect = go.GetComponent<UnityEngine.RectTransform>();

            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "UISprite")
                {
                    go.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
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

