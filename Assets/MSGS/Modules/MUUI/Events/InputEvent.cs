using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.MUUI
{
    public class InputEvent
    {
        ValMap properties;

        public string PrefabName
        {
            get { return ((ValString)properties["Prefab"]).value; }
            set { properties.map[new ValString("Prefab")] = new ValString(value); }
        }

        public string Element
        {
            get { return ((ValString)properties.map[new ValString("Element")]).value; }
            set { properties.map[new ValString("Element")] = new ValString(value); }
        }

        public UIElementType ElementType
        {
            get;
            set;
        }

        public string ScriptName
        {
            get { return ((ValString)properties.map[new ValString("ScriptName")]).value; }
            set
            {
                if(value.Length > 0)
                {
                    if (MiniScriptSingleton.ScriptExists(value))
                    {
                        properties.map[new ValString("ScriptName")] = new ValString(value);
                    }
                    else
                    {
                        MiniScriptSingleton.LogError("Attempt to assign Script to event handler and the specified script (" + value + ") was not found in the mod." +
                            " Element type is [" + ElementType.ToString() + "] and element name is [" + Element + "]");
                    }
                }
            }
        }

        public bool OnEnter
        {
            get { return properties.map[new ValString("OnEnter")].BoolValue(); }
            set { properties.map[new ValString("OnEnter")] = ValNumber.Truth(value); }
        }
        
        public bool OnExit
        {
            get { return properties.map[new ValString("OnExit")].BoolValue(); }
            set { properties.map[new ValString("OnExit")] = ValNumber.Truth(value); }
        }

        public bool OnScrollUp
        {
            get { return properties.map[new ValString("ScrollUp")].BoolValue(); }
            set { properties.map[new ValString("ScrollUp")] = ValNumber.Truth(value); }
        }

        public bool OnScrollDown
        {
            get { return properties.map[new ValString("ScrollDown")].BoolValue(); }
            set { properties.map[new ValString("ScrollDown")] = ValNumber.Truth(value); }
        }

        public bool ClickLeft
        {
            get { return properties.map[new ValString("ClickLeft")].BoolValue(); }
            set { properties.map[new ValString("ClickLeft")] = ValNumber.Truth(value); }
        }
        public bool ClickMiddle
        {
            get { return properties.map[new ValString("ClickMiddle")].BoolValue(); }
            set { properties.map[new ValString("ClickMiddle")] = ValNumber.Truth(value); }
        }
        public bool ClickMiddleDouble
        {
            get { return properties.map[new ValString("ClickMiddleDouble")].BoolValue(); }
            set { properties.map[new ValString("ClickMiddleDouble")] = ValNumber.Truth(value); }
        }
        public bool ClickRight
        {
            get { return properties.map[new ValString("ClickRight")].BoolValue(); }
            set { properties.map[new ValString("ClickRight")] = ValNumber.Truth(value); }
        }
        public bool ClickLeftDouble
        {
            get { return properties.map[new ValString("ClickLeftDouble")].BoolValue(); }
            set { properties.map[new ValString("ClickLeftDouble")] = ValNumber.Truth(value); }
        }
        public bool ClickRightDouble
        {
            get { return properties.map[new ValString("ClickRightDouble")].BoolValue(); }
            set { properties.map[new ValString("ClickRightDouble")] = ValNumber.Truth(value); }
        }

        public KeyCode KeyCode1
        {
            get { return (KeyCode)System.Enum.Parse(typeof(KeyCode), ((ValString)properties.map[new ValString("KeyCode1")]).value); }
            set { properties.map[new ValString("KeyCode1")] = new ValString(value.ToString()); }
        }
        public KeyCode KeyCode2
        {
            get { return (KeyCode)System.Enum.Parse(typeof(KeyCode), ((ValString)properties.map[new ValString("KeyCode2")]).value); }
            set { properties.map[new ValString("KeyCode2")] = new ValString(value.ToString()); }
        }
        public KeyCode KeyCode3
        {
            get { return (KeyCode)System.Enum.Parse(typeof(KeyCode), ((ValString)properties.map[new ValString("KeyCode3")]).value); }
            set { properties.map[new ValString("KeyCode3")] = new ValString(value.ToString()); }
        }

        public double Value
        {
            get { return properties["Value"].DoubleValue(); }
            set { properties["Value"] = new ValNumber(value); }
        }

        public ValMap GetValMap
        {
            get { return properties; }
            set { return; }
        }

        public ValMap GetProperties()
        {
            return properties;
        }

        internal InputEvent()
        {
            ElementType = UIElementType.None;
            properties = new ValMap();
            
            properties.map.Add(new ValString("PrefabName"), new ValString(string.Empty));
            properties.map.Add(new ValString("Element"), new ValString(string.Empty));
            properties.map.Add(new ValString("ClickLeft"), ValNumber.Truth(false));
            properties.map.Add(new ValString("ClickMiddle"), ValNumber.Truth(false));
            properties.map.Add(new ValString("ClickRight"), ValNumber.Truth(false));
            properties.map.Add(new ValString("ClickLeftDouble"), ValNumber.Truth(false));
            properties.map.Add(new ValString("ClickRightDouble"), ValNumber.Truth(false));
            properties.map.Add(new ValString("OnEnter"), ValNumber.Truth(false));
            properties.map.Add(new ValString("OnExit"), ValNumber.Truth(false));
            properties.map.Add(new ValString("ScrollUp"), ValNumber.Truth(false));
            properties.map.Add(new ValString("ScrollDown"), ValNumber.Truth(false));
            properties.map.Add(new ValString("ScriptName"), new ValString(string.Empty));
            properties.map.Add(new ValString("KeyCode1"), new ValString(KeyCode.None.ToString()));
            properties.map.Add(new ValString("KeyCode2"), new ValString(KeyCode.None.ToString()));
            properties.map.Add(new ValString("KeyCode3"), new ValString(KeyCode.None.ToString()));
            properties.map.Add(new ValString("Value"), new ValNumber(0));
        }
    
        
    }
}


