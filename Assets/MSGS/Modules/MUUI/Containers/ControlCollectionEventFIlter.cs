using MiniScript.MSGS.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MiniScript.MSGS.MUUI
{
    [Serializable]
    public class ControlCollectionEventFilter : IValMap
    {
        public List<int> events = new List<int>();

        public ValMap getValMap()
        {
            ValMap map = new ValMap();



            return map;
        }

        static bool DisallowAssignment(Value key, Value value)
        {
            // Assignment override function: return true to cancel (override)
            // the assignment, or false to allow it to happen as normal.
            throw new RuntimeException("DataIntrinsics: Assignment to protected map");
        }

        public bool Contains(int value)
        {
            int a = events.BinarySearch(value);
            if(a > -1) { return true; }
            return false;
        }

        public void Add(int value)
        {
            if(!events.Contains(value)) { events.Add(value); events.Sort(); }
        }

        public ControlCollectionEventFilter(ref ControlCollectionEventFilter copy)
        {
            foreach(int i in copy.events)
            {
                if (!events.Contains(i)) { events.Add(i); }
            }
        }

        public ControlCollectionEventFilter()
        {

        }
    }
}

