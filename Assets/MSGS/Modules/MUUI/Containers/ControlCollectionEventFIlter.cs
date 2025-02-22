using MiniScript.MSGS.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.MUUI
{
    public class ControlCollectionEventFilter : IValMap
    {
        List<int> events = new List<int>();

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
    }
}

