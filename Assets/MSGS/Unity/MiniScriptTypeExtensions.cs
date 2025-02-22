using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniScript;

namespace MiniScript.MSGS.Extensions
{
    public static class MiniScriptTypeExtensions
    {
        #region ValMap
        public static string ToDebugString(this ValMap map)
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append("[");
            foreach (KeyValuePair<Value, Value> kv in map.map)
            {
                str.Append("{" + kv.Key.ToString() + ":" + kv.Value.ToString() + "},");
            }
            str.Append("]");
            return str.ToString();
        }

        public static string ToPrettyString(this ValMap map, string delimiter)
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append("[");
            foreach (KeyValuePair<Value, Value> kv in map.map)
            {
                str.Append("{" + kv.Key.ToString() + ":" + kv.Value.ToString() + "}" + delimiter);
            }
            str.Append("]");
            return str.ToString();
        }

        #endregion
    }
}

