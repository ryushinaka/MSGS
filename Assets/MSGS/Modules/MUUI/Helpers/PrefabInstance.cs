using System.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using MiniScript;
using Sirenix.OdinInspector;
using MiniScript.MSGS.MUUI.Extensions;
using MiniScript.MSGS.Unity;

namespace MiniScript.MSGS.MUUI
{
    public class PrefabInstance : MonoBehaviour
    {
        List<UI_Element> _elements;
        public List<UI_Element> elements
        {
            get { return _elements; }
            set
            {
                if (value != null)
                {
                    if(_elements != null)
                    {
                        if (_elements.Count > 0)
                        {
                            foreach (UI_Element element in _elements)
                            {
                                //element.OnClick -= MouseEventConsumer;
                                //element.OnDoubleClick -= MouseEventConsumer;
                                //element.OnEnter -= MouseEventConsumer;
                                //element.OnExit -= MouseEventConsumer;
                                //element.OnMiddleClick -= MouseEventConsumer;
                                //element.OnRightClick -= MouseEventConsumer;
                            }
                        }
                    }

                    _elements = value;
                    foreach(UI_Element element in value)
                    {
                        //element.OnClick += new System.Action<MUUI_MouseEvent>(MouseEventConsumer);
                        //element.OnDoubleClick += new System.Action<MUUI_MouseEvent>(MouseEventConsumer);
                        //element.OnEnter += new System.Action<MUUI_MouseEvent>(MouseEventConsumer);
                        //element.OnExit += new System.Action<MUUI_MouseEvent>(MouseEventConsumer);
                        //element.OnMiddleClick += new System.Action<MUUI_MouseEvent>(MouseEventConsumer);
                        //element.OnRightClick += new System.Action<MUUI_MouseEvent>(MouseEventConsumer);                        
                    }
                }
            }
        }
        public DataSet set;
        //the list of int/int/gameobject to sort who is the child/parent
        //instanceid, parent, gameobject
        public List<Tuple<int, int, GameObject>> objects;

        public string PrefabName { get; set; }

        void Awake()
        {
            elements = new List<UI_Element>();
        }

        void Start()
        {

        }

        void Update()
        {

        }

        public void WalkTree()
        {

        }

        [Button]
        public void ResetRectTransforms()
        {   
            DataRow localref;
            for(int i = 0; i < set.Tables["RectTransform"].Rows.Count; i++)
            {
                foreach (Tuple<int, int, GameObject> t in objects)
                {
                    if (t.Item1 == (int)set.Tables["RectTransform"].Rows[i]["OwnerInstanceID"])
                    {
                        localref = set.Tables["RectTransform"].Rows[i];
                        t.Item3.GetComponent<RectTransform>().SetupRect(ref localref);
                    }
                }
            }
        }

        [Button]
        public void TestDebug()
        {
            GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }
    }
}
