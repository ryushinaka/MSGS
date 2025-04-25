using MiniScript.MSGS.MUUI.Extensions;
using MiniScript.MSGS.ScriptableObjects;
using MiniScript.MSGS.Unity;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniScript.MSGS.MUUI.TwoDimensional
{
    [RequireComponent(typeof(UnityEngine.UI.Image))]
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public class MUUIButton : MonoBehaviour, UI_Element, IControl, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler
    {
        float mscroll;
        bool benter;
        public RectTransform rect;
        public UnityEngine.UI.Image image;
        public TextMeshProUGUI Text;

        public ScriptEventContainer eventContainer;

        [FoldoutGroup("Scripts by ScriptableObject"), SerializeField]
        public MiniScriptScriptAsset ScriptOnEnterMSA, ScriptOnExitMSA, ScriptOnLeftClickMSA, ScriptOnScrollUpMSA, ScriptOnScrollDownMSA,
            ScriptOnDoubleLeftClickMSA, ScriptOnRightClickMSA, ScriptOnDoubleRightClickMSA, ScriptOnMiddleClickMSA;
        [FoldoutGroup("Scripts by Name"), SerializeField]
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
                    ButtonMouseEnter,
                    ButtonMouseExit,
                    ButtonMouseLeftClick,
                    ButtonMouseRightClick,
                    ButtonMouseMiddleClick,
                    ButtonMouseDoubleLeftClick,
                    ButtonMouseDoubleRightClick,
                    ButtonMouseScrollUp,
                    ButtonMouseScrollDown,
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
                    if(ScriptOnScrollUpMSA != null)
                    {
                        var ones = ScriptableObject.CreateInstance<OneShotScript>();
                        ones.scriptSource = ScriptOnScrollUpMSA.GetScriptFull();
                        ones.Run();
                    }
                }
                if (mscroll < 0)
                {   
                    if (ScriptOnScrollDownMSA != null)
                    {
                        var ones = ScriptableObject.CreateInstance<OneShotScript>();
                        ones.scriptSource = ScriptOnScrollDownMSA.GetScriptFull();
                        ones.Run();
                    }
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
            if(properties == null)
            {
                properties = new ValMap();

                ValMap r = rect.ToValMap();
                r.assignOverride += new ValMap.AssignOverrideFunc(OnRectUpdate);
                properties.map.Add(new ValString("Rect"), r);

                r = this.ToValMap();
                r.assignOverride += new ValMap.AssignOverrideFunc(OnButtonUpdate);
                properties.map.Add(new ValString("Button"), r);
            }

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
            //InputEvent tmp = new InputEvent();
            //tmp.OnEnter = true;
            //tmp.Element = this.Name;
            //tmp.ElementType = UIElementType.Button;
            //MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
            if (ScriptOnEnterMSA != null)
            {
                var ones = ScriptableObject.CreateInstance<OneShotScript>();
                ones.scriptSource = ScriptOnEnterMSA.ScriptContent;
                ones.Run();
            }
            else if (ScriptOnEnter.Length > 0)
            {
                if (MiniScriptSingleton.ScriptExists(ScriptOnEnter))
                {
                    var ones = ScriptableObject.CreateInstance<OneShotScript>();
                    ones.scriptSource =
                        MiniScriptSingleton.Scripts[ScriptOnEnter];
                    ones.Run();
                }
            }
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            benter = false;
            //InputEvent tmp = new InputEvent();
            //tmp.OnExit = true;
            //tmp.ElementType = UIElementType.Button;
            //tmp.Element = this.Name;

            //MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
            if (ScriptOnExitMSA != null)
            {
                var ones = ScriptableObject.CreateInstance<OneShotScript>();
                ones.scriptSource = ScriptOnExitMSA.ScriptContent;
                ones.Run();
            }
            else if (ScriptOnExit.Length > 0)
            {
                if (MiniScriptSingleton.ScriptExists(ScriptOnExit))
                {
                    var ones = ScriptableObject.CreateInstance<OneShotScript>();
                    ones.scriptSource =
                        MiniScriptSingleton.Scripts[ScriptOnExit];
                    ones.Run();
                }
            }
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

                    if (ScriptOnDoubleLeftClickMSA != null)
                    {   
                        var ones = ScriptableObject.CreateInstance<OneShotScript>();
                        ones.scriptSource = ScriptOnDoubleLeftClickMSA.GetScriptFull();
                        ones.Compile();
                        ones.RunSync();
                    }
                    else if (ScriptOnDoubleLeftClick.Length > 0)
                    {
                        if (MiniScriptSingleton.ScriptExists(ScriptOnDoubleLeftClick))
                        {
                            var ones = ScriptableObject.CreateInstance<OneShotScript>();
                            ones.scriptSource =
                                MiniScriptSingleton.Scripts[ScriptOnDoubleLeftClick];
                            ones.Compile();
                            ones.RunSync();
                        }
                    }
                }
                else if (leftclicked > 2 || UnityEngine.Time.time - leftclicktime > 1) { leftclicked = 0; }

                if (!didraisedoubleclick)
                {   
                    if(ScriptOnLeftClickMSA != null)
                    {   
                        var ones = ScriptableObject.CreateInstance<OneShotScript>();
                        ones.scriptSource = ScriptOnLeftClickMSA.ScriptContent;
                        ones.Compile();
                        ones.RunSync();
                    }
                    else if(ScriptOnLeftClick.Length > 0)
                    {
                        if (MiniScriptSingleton.ScriptExists(ScriptOnLeftClick))
                        {
                            var ones = ScriptableObject.CreateInstance<OneShotScript>();
                            ones.scriptSource = MiniScriptSingleton.Scripts[ScriptOnLeftClick];
                            ones.Compile();
                            ones.RunSync();
                        }
                    }
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

                    if (ScriptOnDoubleRightClickMSA != null)
                    {
                        var ones = ScriptableObject.CreateInstance<OneShotScript>();
                        ones.scriptSource = ScriptOnDoubleRightClickMSA.ScriptContent;
                        ones.Run();
                    }
                    else if (ScriptOnDoubleRightClick.Length > 0)
                    {
                        if (MiniScriptSingleton.ScriptExists(ScriptOnDoubleRightClick))
                        {
                            var ones = ScriptableObject.CreateInstance<OneShotScript>();
                            ones.scriptSource =
                                MiniScriptSingleton.Scripts[ScriptOnDoubleRightClick];
                            ones.Run();
                        }
                    }
                }
                else if (rightclicked > 2 || UnityEngine.Time.time - rightclicktime > 1) { rightclicked = 0; }

                if (!didraisedoubleclick)
                {
                    //InputEvent tmp = new InputEvent
                    //{
                    //    ClickRight = true,
                    //    Element = this.Name,
                    //    ElementType = UIElementType.Button,
                    //    ScriptName = ScriptOnRightClick
                    //};

                    //MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                    if (ScriptOnRightClickMSA != null)
                    {
                        var ones = ScriptableObject.CreateInstance<OneShotScript>();
                        ones.scriptSource = ScriptOnRightClickMSA.ScriptContent;
                        ones.Run();
                    }
                    else if (ScriptOnRightClick.Length > 0)
                    {
                        if (MiniScriptSingleton.ScriptExists(ScriptOnRightClick))
                        {
                            var ones = ScriptableObject.CreateInstance<OneShotScript>();
                            ones.scriptSource =
                                MiniScriptSingleton.Scripts[ScriptOnRightClick];
                            ones.Run();
                        }
                    }
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Middle)
            {   
                if (ScriptOnMiddleClickMSA != null)
                {
                    var ones = ScriptableObject.CreateInstance<OneShotScript>();
                    ones.scriptSource = ScriptOnMiddleClickMSA.ScriptContent;
                    ones.Run();
                }
                else if (ScriptOnMiddleClick.Length > 0)
                {
                    if (MiniScriptSingleton.ScriptExists(ScriptOnMiddleClick))
                    {
                        var ones = ScriptableObject.CreateInstance<OneShotScript>();
                        ones.scriptSource =
                            MiniScriptSingleton.Scripts[ScriptOnMiddleClick];
                        ones.Run();
                    }
                }
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
            go.AddComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIButton > ();
            //make sure the references are put in place
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIButton>().image = go.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIButton>().rect = go.GetComponent<UnityEngine.RectTransform>();
            var child = new GameObject("Text (TMP)");
            child.transform.SetParent(go.transform);
            child.AddComponent<TextMeshProUGUI>();
            child.transform.localPosition = new Vector3(0, 0, 0);
            child.transform.localScale = new Vector3(1, 1, 1);
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIButton>().Text = child.GetComponent<TextMeshProUGUI>();
        }

        [MenuItem("GameObject/MSGS/Create/Button", false, 0)]
        private static void CreateButton()
        {
            var go = new GameObject("Button");
            var parent = Selection.activeObject as GameObject;
            go.transform.SetParent(parent.transform);
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.AddComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIButton>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIButton>().image = go.GetComponent<UnityEngine.UI.Image>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIButton>().rect = go.GetComponent<UnityEngine.RectTransform>();

            var child = new GameObject("Text (TMP)");
            child.transform.SetParent(go.transform);
            child.AddComponent<TextMeshProUGUI>();
            child.transform.localPosition = new Vector3(0, 0, 0);
            child.transform.localScale = new Vector3(1, 1, 1);
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIButton>().Text = child.GetComponent<TextMeshProUGUI>();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            throw new NotImplementedException();
        }




#endif
        #endregion

    }
}

