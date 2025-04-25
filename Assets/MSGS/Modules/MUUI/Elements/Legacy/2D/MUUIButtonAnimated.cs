using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.EventSystems;
using TMPro;
using System;
using MiniScript.MSGS.MUUI.Extensions;
using UnityEditor;
using MiniScript.MSGS.Unity;

namespace MiniScript.MSGS.MUUI.TwoDimensional
{
    [RequireComponent(typeof(UnityEngine.UI.Image))]
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public class MUUIButtonAnimated : MonoBehaviour, UI_Element, IControl, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        float mscroll;
        bool benter;
        public UnityEngine.UI.Image image;
        public RectTransform Rect;
        public List<Sprite> sprites;
        public float cycleTime;
        public bool DoAnimate;
        public int currentFrame = 0;
        public TextMeshProUGUI Text;
        public string ScriptOnEnter, ScriptOnExit, ScriptOnLeftClick, ScriptOnScrollUp, ScriptOnScrollDown,
            ScriptOnDoubleLeftClick, ScriptOnRightClick, ScriptOnDoubleRightClick, ScriptOnMiddleClick;

        List<int> events = new List<int>();

        #region public const int event declarations
        public const int ButtonAnimatedMouseEnter = 1200;
        public const int ButtonAnimatedMouseExit = 1201;
        public const int ButtonAnimatedMouseLeftClick = 1202;
        public const int ButtonAnimatedMouseRightClick = 1203;
        public const int ButtonAnimatedMouseMiddleClick = 1204;
        public const int ButtonAnimatedMouseDoubleLeftClick = 1205;
        public const int ButtonAnimatedMouseDoubleRightClick = 1206;
        public const int ButtonAnimatedMouseScrollUp = 1207;
        public const int ButtonAnimatedMouseScrollDown = 1208;
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
                    ButtonAnimatedMouseEnter,
                    ButtonAnimatedMouseExit,
                    ButtonAnimatedMouseLeftClick,
                    ButtonAnimatedMouseRightClick,
                    ButtonAnimatedMouseMiddleClick,
                    ButtonAnimatedMouseDoubleLeftClick,
                    ButtonAnimatedMouseDoubleRightClick,
                    ButtonAnimatedMouseScrollUp,
                    ButtonAnimatedMouseScrollDown,
                }
            };

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
                    tmp.ElementType = UIElementType.Button;
                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
                if (mscroll < 0)
                {
                    InputEvent tmp = new InputEvent();
                    tmp.OnScrollDown = true;
                    tmp.Element = this.Name;
                    tmp.ElementType = UIElementType.Button;
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
                    if (currentFrame < sprites.Count-1)
                    {
                        currentFrame++;
                    }
                    else if (currentFrame == sprites.Count-1)
                    {
                        currentFrame = 1;
                    }

                    image.sprite = sprites[currentFrame];
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

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        public ValMap GetProperties()
        {
            if(properties == null)
            {
                properties = new ValMap();

                ValMap r = Rect.ToValMap();
                r.assignOverride += new ValMap.AssignOverrideFunc(OnRectUpdate);
                properties.map.Add(new ValString("Rect"), r);

                //r.assignOverride += new ValMap.AssignOverrideFunc(OnButtonUpdate);
                //properties.map.Add(new ValString("Button"), r);
            }

            return properties;
        }

        bool OnRectUpdate(Value a, Value b)
        {
            return Rect.UpdateRect(a, b);
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
            tmp.ElementType = UIElementType.ButtonAnimated;
            MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            benter = false;
            InputEvent tmp = new InputEvent();
            tmp.OnExit = true;
            tmp.ElementType = UIElementType.ButtonAnimated;
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
                        ElementType = UIElementType.ButtonAnimated,
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
                        ElementType = UIElementType.ButtonAnimated,
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
                        ElementType = UIElementType.ButtonAnimated,
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
                        ElementType = UIElementType.ButtonAnimated,
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
                    ElementType = UIElementType.ButtonAnimated,
                    ScriptName = ScriptOnMiddleClick
                };

                MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
            }
        }

        #region Editor Extensions
#if UNITY_EDITOR
        [MenuItem("GameObject/MSGS/Add/ButtonAnimated", false, 0)]
        private static void AddConfig()
        {
            var go = Selection.activeObject as GameObject;
            if (go == null) { return; }
            go.AddComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIButtonAnimated>();
            //make sure the references are put in place
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIButtonAnimated>().image = go.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIButtonAnimated>().Rect = go.GetComponent<UnityEngine.RectTransform>();
            var child = new GameObject("Text (TMP)");
            child.transform.SetParent(go.transform);
            child.AddComponent<TextMeshProUGUI>();
            child.transform.localPosition = new Vector3(0, 0, 0);
            child.transform.localScale = new Vector3(1, 1, 1);
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIButtonAnimated>().Text = child.GetComponent<TextMeshProUGUI>();
        }


        [MenuItem("GameObject/MSGS/Create/ButtonAnimated", false, 0)]
        private static void CreateButton()
        {
            var go = new GameObject("ButtonAnimated");
            var parent = Selection.activeObject as GameObject;
            go.transform.SetParent(parent.transform);
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.AddComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIButtonAnimated>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIButtonAnimated>().image = go.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIButtonAnimated>().Rect = go.GetComponent<UnityEngine.RectTransform>();

            var child = new GameObject("Text (TMP)");
            child.transform.SetParent(go.transform);
            child.AddComponent<TextMeshProUGUI>();
            child.transform.localPosition = new Vector3(0, 0, 0);
            child.transform.localScale = new Vector3(1, 1, 1);
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIButtonAnimated>().Text = child.GetComponent<TextMeshProUGUI>();
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
