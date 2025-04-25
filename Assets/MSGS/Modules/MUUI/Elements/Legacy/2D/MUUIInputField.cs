using MiniScript.MSGS.MUUI.Extensions;
using MiniScript.MSGS.Unity;
using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MiniScript.MSGS.MUUI.TwoDimensional
{
    [RequireComponent(typeof(TMPro.TMP_InputField))]
    public class MUUIInputField : MonoBehaviour, UI_Element, IControl, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        float mscroll;
        bool benter;
        public RectTransform Rect;
        public TMP_InputField Inputfield;
        public TextMeshProUGUI Placeholder, Text;
        public RectMask2D Mask;
        public string ScriptOnEnter, ScriptOnExit, ScriptOnLeftClick, ScriptOnScrollUp, ScriptOnScrollDown,
           ScriptOnDoubleLeftClick, ScriptOnRightClick, ScriptOnDoubleRightClick, ScriptOnMiddleClick;

        void Awake()
        {
            properties = GetProperties();
        }

        void Start()
        {
            Rect = GetComponent<RectTransform>();
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
                    tmp.ElementType = UIElementType.InputField;
                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
                if (mscroll < 0)
                {
                    InputEvent tmp = new InputEvent();
                    tmp.OnScrollDown = true;
                    tmp.Element = this.Name;
                    tmp.ElementType = UIElementType.InputField;
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
            get { return UIElementType.InputField; }
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

                ValMap r = Rect.ToValMap();
                r.assignOverride += new ValMap.AssignOverrideFunc(OnRectTransformChanged);
                properties.map.Add(new ValString("Rect"), r);

                r = this.ToValMap();
                r.assignOverride += new ValMap.AssignOverrideFunc(OnInputFieldModified);
                properties.map.Add(new ValString("InputField"), r);
            }
            return properties;
        }

        #region
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            benter = true;
            InputEvent tmp = new InputEvent();
            tmp.OnEnter = true;
            tmp.Element = this.Name;
            tmp.ElementType = UIElementType.InputField;
            MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            benter = false;
            InputEvent tmp = new InputEvent();
            tmp.OnExit = true;
            tmp.ElementType = UIElementType.InputField;
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
                        ElementType = UIElementType.InputField,
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
                        ElementType = UIElementType.InputField,
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
                        ElementType = UIElementType.InputField,
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
                        ElementType = UIElementType.InputField,
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
                    ElementType = UIElementType.InputField,
                    ScriptName = ScriptOnMiddleClick
                };

                MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
            }
        }
        #endregion
        
        public GameObject GetGameObject()
        {
            return this.gameObject;
        }

        bool OnRectTransformChanged(Value a, Value b)
        {
            return Rect.UpdateRect(a, b);
        }

        bool OnInputFieldModified(Value a, Value b)
        {
            return InputFieldExtensions.UpdateInputField(this, a, b);            
        }

        void OnPlaceholderModified() { }
        void OnTextModified() { }
        void OnMaskModified() { }
        void OnInputFieldModified() { }

        #region Editor Extensions
#if UNITY_EDITOR
        [MenuItem("GameObject/MSGS/Add/InputField", false, 0)]
        private static void EditorAddInputField()
        {
            var go = Selection.activeObject as GameObject;
            if (go == null) { return; }
            go.AddComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIInputField>();
            //make sure the references are put in place
            go.AddComponent<UnityEngine.UI.Image>();
            go.GetComponent<UnityEngine.UI.Image>().type = UnityEngine.UI.Image.Type.Sliced;
            go.AddComponent<UnityEngine.UI.Image>().maskable = true;
            go.AddComponent<UnityEngine.UI.Image>().fillCenter = true;
            go.AddComponent<UnityEngine.UI.Image>().pixelsPerUnitMultiplier = 1;
            foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
            {
                if (sprite.name == "InputFieldBackground")
                {
                    go.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                }
            }            

            var textArea = new GameObject("Text Area");
            textArea.transform.SetParent(go.transform);
            textArea.AddComponent<RectTransform>();
            var rt = textArea.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0.5f);
            rt.localPosition = new Vector3(0f, 0.5f, 0f);
            rt.localScale = new Vector3(1f, 1f, 1f);
            rt.offsetMax = new Vector2(-10f, -7f);
            rt.offsetMin = new Vector2(10f, 6f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.sizeDelta = new Vector2(-20f, -13f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            textArea.AddComponent<RectMask2D>();
            textArea.GetComponent<RectMask2D>().padding = new Vector4(-8f, -8f, -8f, -8f);

            var placeholder = new GameObject("Placeholder");
            placeholder.transform.SetParent(textArea.transform);
            placeholder.AddComponent<RectTransform>();
            rt = placeholder.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(0f, 0f, 0f);
            rt.localScale = new Vector3(1f, 1f, 1f);
            rt.offsetMax = new Vector2(0f, 0f);
            rt.offsetMin = new Vector2(0f, 0f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.anchorMax = new Vector2(0f, 0f);
            rt.sizeDelta = new Vector2(0f, 0f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            placeholder.AddComponent<LayoutElement>();
            placeholder.GetComponent<LayoutElement>().ignoreLayout = true;
            placeholder.GetComponent<LayoutElement>().layoutPriority = 1;
            placeholder.AddComponent<TextMeshProUGUI>();
            placeholder.GetComponent<TextMeshProUGUI>().fontSize = 14;
            placeholder.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Italic;
            placeholder.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top | TextAlignmentOptions.Left;
            placeholder.GetComponent<TextMeshProUGUI>().color = new Color(50f, 50f, 50f, 128f);

            var text = new GameObject("Text");
            text.transform.SetParent(placeholder.transform);
            text.AddComponent<RectTransform>();
            rt = text.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0f, 0f);
            rt.localPosition = new Vector3(0f, 0f, 0f);
            rt.localScale = new Vector3(1f, 1f, 1f);
            rt.offsetMax = new Vector2(0f, 0f);
            rt.offsetMin = new Vector2(0f, 0f);
            rt.anchorMin = new Vector2(0f, 0f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.sizeDelta = new Vector2(0f, 0f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            text.AddComponent<TextMeshProUGUI>();
            text.GetComponent<TextMeshProUGUI>().fontSize = 14;
            text.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            text.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top | TextAlignmentOptions.Left;
            text.GetComponent<TextMeshProUGUI>().color = new Color(50f, 50f, 50f, 255f);

            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIInputField>().Inputfield = go.GetComponent<TMPro.TMP_InputField>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIInputField>().Placeholder =
                placeholder.GetComponent<TextMeshProUGUI>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIInputField>().Text =
                text.GetComponent<TextMeshProUGUI>();
            go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIInputField>().Mask =
                textArea.GetComponent<RectMask2D>();
        }

        [MenuItem("GameObject/MSGS/Create/InputField", false, 0)]
        private static void EditorCreateInputField()
        {
            var go = Selection.activeObject as GameObject;
            if(go != null)
            {
                var tmp = new GameObject("InputField");
                tmp.transform.SetParent(go.transform);
                go = new GameObject("InputField"); ;
                go.AddComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIInputField>();
                //make sure the references are put in place
                go.AddComponent<UnityEngine.UI.Image>();
                go.GetComponent<UnityEngine.UI.Image>().type = UnityEngine.UI.Image.Type.Sliced;
                go.AddComponent<UnityEngine.UI.Image>().maskable = true;
                go.AddComponent<UnityEngine.UI.Image>().fillCenter = true;
                go.AddComponent<UnityEngine.UI.Image>().pixelsPerUnitMultiplier = 1;
                foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                {
                    if (sprite.name == "InputFieldBackground")
                    {
                        go.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                    }
                }

                var textArea = new GameObject("Text Area");
                textArea.transform.SetParent(go.transform);
                textArea.AddComponent<RectTransform>();
                var rt = textArea.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(0f, 0.5f);
                rt.localPosition = new Vector3(0f, 0.5f, 0f);
                rt.localScale = new Vector3(1f, 1f, 1f);
                rt.offsetMax = new Vector2(-10f, -7f);
                rt.offsetMin = new Vector2(10f, 6f);
                rt.anchorMin = new Vector2(0f, 0f);
                rt.anchorMax = new Vector2(1f, 1f);
                rt.sizeDelta = new Vector2(-20f, -13f);
                rt.pivot = new Vector2(0.5f, 0.5f);
                textArea.AddComponent<RectMask2D>();
                textArea.GetComponent<RectMask2D>().padding = new Vector4(-8f, -8f, -8f, -8f);

                var placeholder = new GameObject("Placeholder");
                placeholder.transform.SetParent(textArea.transform);
                placeholder.AddComponent<RectTransform>();
                rt = placeholder.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(0f, 0f);
                rt.localPosition = new Vector3(0f, 0f, 0f);
                rt.localScale = new Vector3(1f, 1f, 1f);
                rt.offsetMax = new Vector2(0f, 0f);
                rt.offsetMin = new Vector2(0f, 0f);
                rt.anchorMin = new Vector2(0f, 0f);
                rt.anchorMax = new Vector2(0f, 0f);
                rt.sizeDelta = new Vector2(0f, 0f);
                rt.pivot = new Vector2(0.5f, 0.5f);
                placeholder.AddComponent<LayoutElement>();
                placeholder.GetComponent<LayoutElement>().ignoreLayout = true;
                placeholder.GetComponent<LayoutElement>().layoutPriority = 1;
                placeholder.AddComponent<TextMeshProUGUI>();
                placeholder.GetComponent<TextMeshProUGUI>().fontSize = 14;
                placeholder.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Italic;
                placeholder.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top | TextAlignmentOptions.Left;
                placeholder.GetComponent<TextMeshProUGUI>().color = new Color(50f, 50f, 50f, 128f);

                var text = new GameObject("Text");
                text.transform.SetParent(placeholder.transform);
                text.AddComponent<RectTransform>();
                rt = text.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(0f, 0f);
                rt.localPosition = new Vector3(0f, 0f, 0f);
                rt.localScale = new Vector3(1f, 1f, 1f);
                rt.offsetMax = new Vector2(0f, 0f);
                rt.offsetMin = new Vector2(0f, 0f);
                rt.anchorMin = new Vector2(0f, 0f);
                rt.anchorMax = new Vector2(1f, 1f);
                rt.sizeDelta = new Vector2(0f, 0f);
                rt.pivot = new Vector2(0.5f, 0.5f);
                text.AddComponent<TextMeshProUGUI>();
                text.GetComponent<TextMeshProUGUI>().fontSize = 14;
                text.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
                text.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top | TextAlignmentOptions.Left;
                text.GetComponent<TextMeshProUGUI>().color = new Color(50f, 50f, 50f, 255f);

                go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIInputField>().Inputfield = go.GetComponent<TMPro.TMP_InputField>();
                go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIInputField>().Placeholder =
                    placeholder.GetComponent<TextMeshProUGUI>();
                go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIInputField>().Text =
                    text.GetComponent<TextMeshProUGUI>();
                go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIInputField>().Mask =
                    textArea.GetComponent<RectMask2D>();
            }
            else
            {
                var tmp = new GameObject("InputField");
                tmp.transform.SetParent(go.transform);
                go = tmp;
                go.AddComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIInputField>();
                //make sure the references are put in place
                go.AddComponent<UnityEngine.UI.Image>();
                go.GetComponent<UnityEngine.UI.Image>().type = UnityEngine.UI.Image.Type.Sliced;
                go.AddComponent<UnityEngine.UI.Image>().maskable = true;
                go.AddComponent<UnityEngine.UI.Image>().fillCenter = true;
                go.AddComponent<UnityEngine.UI.Image>().pixelsPerUnitMultiplier = 1;
                foreach (Sprite sprite in Resources.FindObjectsOfTypeAll<Sprite>())
                {
                    if (sprite.name == "InputFieldBackground")
                    {
                        go.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                    }
                }

                var textArea = new GameObject("Text Area");
                textArea.transform.SetParent(go.transform);
                textArea.AddComponent<RectTransform>();
                var rt = textArea.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(0f, 0.5f);
                rt.localPosition = new Vector3(0f, 0.5f, 0f);
                rt.localScale = new Vector3(1f, 1f, 1f);
                rt.offsetMax = new Vector2(-10f, -7f);
                rt.offsetMin = new Vector2(10f, 6f);
                rt.anchorMin = new Vector2(0f, 0f);
                rt.anchorMax = new Vector2(1f, 1f);
                rt.sizeDelta = new Vector2(-20f, -13f);
                rt.pivot = new Vector2(0.5f, 0.5f);
                textArea.AddComponent<RectMask2D>();
                textArea.GetComponent<RectMask2D>().padding = new Vector4(-8f, -8f, -8f, -8f);

                var placeholder = new GameObject("Placeholder");
                placeholder.transform.SetParent(textArea.transform);
                placeholder.AddComponent<RectTransform>();
                rt = placeholder.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(0f, 0f);
                rt.localPosition = new Vector3(0f, 0f, 0f);
                rt.localScale = new Vector3(1f, 1f, 1f);
                rt.offsetMax = new Vector2(0f, 0f);
                rt.offsetMin = new Vector2(0f, 0f);
                rt.anchorMin = new Vector2(0f, 0f);
                rt.anchorMax = new Vector2(0f, 0f);
                rt.sizeDelta = new Vector2(0f, 0f);
                rt.pivot = new Vector2(0.5f, 0.5f);
                placeholder.AddComponent<LayoutElement>();
                placeholder.GetComponent<LayoutElement>().ignoreLayout = true;
                placeholder.GetComponent<LayoutElement>().layoutPriority = 1;
                placeholder.AddComponent<TextMeshProUGUI>();
                placeholder.GetComponent<TextMeshProUGUI>().fontSize = 14;
                placeholder.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Italic;
                placeholder.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top | TextAlignmentOptions.Left;
                placeholder.GetComponent<TextMeshProUGUI>().color = new Color(50f, 50f, 50f, 128f);

                var text = new GameObject("Text");
                text.transform.SetParent(placeholder.transform);
                text.AddComponent<RectTransform>();
                rt = text.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(0f, 0f);
                rt.localPosition = new Vector3(0f, 0f, 0f);
                rt.localScale = new Vector3(1f, 1f, 1f);
                rt.offsetMax = new Vector2(0f, 0f);
                rt.offsetMin = new Vector2(0f, 0f);
                rt.anchorMin = new Vector2(0f, 0f);
                rt.anchorMax = new Vector2(1f, 1f);
                rt.sizeDelta = new Vector2(0f, 0f);
                rt.pivot = new Vector2(0.5f, 0.5f);
                text.AddComponent<TextMeshProUGUI>();
                text.GetComponent<TextMeshProUGUI>().fontSize = 14;
                text.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
                text.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top | TextAlignmentOptions.Left;
                text.GetComponent<TextMeshProUGUI>().color = new Color(50f, 50f, 50f, 255f);

                go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIInputField>().Inputfield = go.GetComponent<TMPro.TMP_InputField>();
                go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIInputField>().Placeholder =
                    placeholder.GetComponent<TextMeshProUGUI>();
                go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIInputField>().Text =
                    text.GetComponent<TextMeshProUGUI>();
                go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIInputField>().Mask =
                    textArea.GetComponent<RectMask2D>();
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

