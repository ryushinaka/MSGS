using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MiniScript.MSGS.MUUI.Extensions;
using UnityEditor;
using System;

namespace MiniScript.MSGS.MUUI.TwoDimensional
{
    [RequireComponent(typeof(UnityEngine.UI.Image))]
    public class ImageAnimated : MonoBehaviour, UI_Element, IControl, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        float mscroll;
        bool benter;
        public UnityEngine.UI.Image image;
        public RectTransform Rect;
        public List<Sprite> sprites;
        public float cycleTime;
        public bool DoAnimate;
        public int currentFrame = 0;
        public string ScriptOnEnter, ScriptOnExit, ScriptOnLeftClick, ScriptOnScrollUp, ScriptOnScrollDown,
            ScriptOnDoubleLeftClick, ScriptOnRightClick, ScriptOnDoubleRightClick, ScriptOnMiddleClick;

        List<int> events = new List<int>();
        #region public const int event declarations
        public const int ImageAnimatedMouseEnter = 1500;
        public const int ImageAnimatedMouseExit = 1501;
        public const int ImageAnimatedMouseLeftClick = 1502;
        public const int ImageAnimatedMouseRightClick = 1503;
        public const int ImageAnimatedMouseMiddleClick = 1504;
        public const int ImageAnimatedMouseDoubleLeftClick = 1505;
        public const int ImageAnimatedMouseDoubleRightClick = 1506;
        public const int ImageAnimatedMouseScrollUp = 1507;
        public const int ImageAnimatedMouseScrollDown = 1508;
        #endregion

        void Awake()
        {
            sprites = new List<Sprite>();
            cycleTime = 0.07f;
        }

        void Start()
        {
            StartAnimation();
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
                    tmp.ElementType = UIElementType.ImageAnimated;
                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
                if (mscroll < 0)
                {
                    InputEvent tmp = new InputEvent();
                    tmp.OnScrollDown = true;
                    tmp.Element = this.Name;
                    tmp.ElementType = UIElementType.ImageAnimated;
                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
            }
        }

        private IEnumerator AnimationCycle()
        {
            while (true)
            {
                yield return new WaitForSeconds(cycleTime);

                if (DoAnimate && sprites.Count > 0)
                {
                    if (currentFrame < sprites.Count - 1)
                    {
                        currentFrame++;
                    }
                    else if (currentFrame == sprites.Count - 1)
                    {
                        currentFrame = 0;
                    }

                    this.GetComponent<Image>().image.sprite = sprites[currentFrame];
                }
            }
        }

        public void StartAnimation()
        {
            StartCoroutine("AnimationCycle");
            DoAnimate = true;
            currentFrame = 0;
        }
        public void StopAnimation()
        {
            StopCoroutine("AnimationCycle");
            DoAnimate = false;
            currentFrame = 0;
        }
        public void PauseAnimation()
        {
            StopCoroutine("AnimationCycle");
            DoAnimate = false;
        }
        public void ResumeAnimation()
        {
            StartCoroutine("AnimationCycle");
            DoAnimate = true;
        }

        ValMap properties;
        public ValMap GetValMap
        {
            get { return properties; }
            set { return; }
        }

        public ValMap GetProperties()
        {
            properties = new ValMap();

            ValMap a = Rect.ToValMap();
            a.assignOverride += new ValMap.AssignOverrideFunc(OnUpdateRectTransform);
            properties.map.Add(new ValString("rect"), a);

            a = this.ToValMap();
            a.assignOverride += new ValMap.AssignOverrideFunc(OnUpdateImage);
            properties.map.Add(new ValString("image"), a);

            return properties;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public UIElementType ObjectType
        {
            get { return UIElementType.ImageAnimated; }
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
            tmp.ElementType = UIElementType.ImageAnimated;
            MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            benter = false;
            InputEvent tmp = new InputEvent();
            tmp.OnExit = true;
            tmp.ElementType = UIElementType.ImageAnimated;
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
                        ElementType = UIElementType.ImageAnimated,
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
                        ElementType = UIElementType.ImageAnimated,
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
                        ElementType = UIElementType.ImageAnimated,
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
                        ElementType = UIElementType.ImageAnimated,
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
                    ElementType = UIElementType.ImageAnimated,
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
        [MenuItem("GameObject/MSGS/Add/ImageAnimated", false, 0)]

        private static void EditorAddImage()
        {
            var go = Selection.activeObject as GameObject;
            if (go == null) { return; }
            go.AddComponent<MiniScript.MSGS.MUUI.TwoDimensional.ImageAnimated>();
            //make sure the references are put in place
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.ImageAnimated>().image = go.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.ImageAnimated>().Rect = go.GetComponent<UnityEngine.RectTransform>();

        }


        [MenuItem("GameObject/MSGS/Create/ImageAnimated", false, 0)]
        private static void EditorCreateImage()
        {
            var go = new GameObject("ImageAnimated");
            var parent = Selection.activeObject as GameObject;
            go.transform.SetParent(parent.transform);
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.AddComponent<MiniScript.MSGS.MUUI.TwoDimensional.ImageAnimated>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.ImageAnimated>().image = go.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.ImageAnimated>().Rect = go.GetComponent<UnityEngine.RectTransform>();

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

