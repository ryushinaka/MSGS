using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MiniScript;
using UnityEngine.EventSystems;
using MiniScript.MSGS.MUUI.Extensions;
using System;

namespace MiniScript.MSGS.MUUI.TwoDimensional
{
    [RequireComponent(typeof(UnityEngine.UI.Slider))]
    public class Slider : MonoBehaviour, UI_Element, IControl, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        float mscroll;
        bool benter;
        public UnityEngine.UI.Slider slider;
        public Image Background, Handle, Fill;
        public GameObject FillArea, HandleArea;
        public RectTransform rect;
        ValMap properties;
        public string ScriptOnValueChanged;
        public string ScriptOnEnter, ScriptOnExit, ScriptOnLeftClick, ScriptOnScrollUp, ScriptOnScrollDown,
          ScriptOnDoubleLeftClick, ScriptOnRightClick, ScriptOnDoubleRightClick, ScriptOnMiddleClick;

        List<int> events = new List<int>();
        #region public const int event declarations
        public const int SliderMouseEnter = 1700;
        public const int SliderMouseExit = 1701;
        public const int SliderMouseLeftClick = 1702;
        public const int SliderMouseRightClick = 1703;
        public const int SliderMouseMiddleClick = 1704;
        public const int SliderMouseDoubleLeftClick = 1705;
        public const int SliderMouseDoubleRightClick = 1706;
        public const int SliderMouseScrollUp = 1707;
        public const int SliderMouseScrollDown = 1708;
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
                    tmp.ElementType = UIElementType.Slider;
                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
                if (mscroll < 0)
                {
                    InputEvent tmp = new InputEvent();
                    tmp.OnScrollDown = true;
                    tmp.Element = this.Name;
                    tmp.ElementType = UIElementType.Slider;
                    MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
                }
            }
        }

        ValNumber minvalue, maxvalue, currentvalue;
        public ValNumber MinValue
        {
            get { return minvalue; }
            set
            {
                if(value.value > maxvalue.value)
                {
                    MiniScriptSingleton.LogError("Attempt to modify MUUI_Slider(" + this.name + ").MinValue with a value greater than the current MaxValue. " +
                        "The current MaxValue is " + maxvalue.value.ToString() + " and the value that was rejected is " + value.value.ToString());
                }
                else if(value.value < maxvalue.value)
                {
                    minvalue = value;
                }
            }
        }
        public ValNumber MaxValue
        {
            get { return maxvalue; }
            set
            {
                if(value.value < minvalue.value)
                {
                    MiniScriptSingleton.LogError("Attempt to modify MUUI_Slider(" + this.name + ").MaxValue with a value less than the current MinValue. " +
                        "The current MinValue is " + minvalue.value.ToString() + " and the value that was rejected is " + value.value.ToString());
                }
                else
                {
                    maxvalue = value;
                }
            }
        }
        public ValNumber CurrentValue
        {
            get
            {
                //slider.float value range is 0 to 1
                double range = MaxValue.value - MinValue.value;
                double d2 = range * slider.value;
                currentvalue.value = MinValue.value + d2;
                return currentvalue;
            }
            set
            {
                
            }
        }
        
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
            get { return UIElementType.Slider; }
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
            r.assignOverride += new ValMap.AssignOverrideFunc(OnSliderUpdate);
            properties.map.Add(new ValString("Slider"), r);

            return properties;
        }

        bool OnRectUpdate(Value a, Value b)
        {
            return rect.UpdateRect(a, b);
        }

        public bool OnSliderUpdate(Value a, Value b)
        {
            return this.UpdateSlider(a, b);
        }

        public void OnSliderValueChange(float value)
        {
            ValMap z = (ValMap)properties["Slider"];
            z["Value"] = currentvalue;
            
            InputEvent ev = new InputEvent();
            ev.ScriptName = ScriptOnValueChanged;
            ev.Element = this.name;
            ev.ElementType = ObjectType;

            //execute the script to handle the updated slider value
            MiniScriptSingleton.EventSink.HandleEvent(this, ref ev);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            benter = true;
            InputEvent tmp = new InputEvent();
            tmp.OnEnter = true;
            tmp.Element = this.Name;
            tmp.ElementType = UIElementType.Slider;
            MiniScriptSingleton.EventSink.HandleEvent(this, ref tmp);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            benter = false;
            InputEvent tmp = new InputEvent();
            tmp.OnExit = true;
            tmp.Element = this.Name;
            tmp.ElementType = UIElementType.Slider;

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
                        ElementType = UIElementType.Slider,
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
                        ElementType = UIElementType.Slider,
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
                        ElementType = UIElementType.Slider,
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
                        ElementType = UIElementType.Slider,
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
                    ElementType = UIElementType.Slider,
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

