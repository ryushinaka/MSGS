using System;
using System.Collections.Generic;

namespace MiniScript.MSGS.ScriptableObjects
{
    public static class MiniScriptScriptableObjectExtensions
    {
        /// <summary>
        /// Returns true if instance is found in the list, false otherwise.
        /// </summary>
        /// <param name="mso"></param>
        /// <param name="scriptableObject"></param>
        /// <returns></returns>
        public static bool ContainsMSO(this List<MiniScriptScriptableObject> mso, MiniScriptScriptableObject scriptableObject)
        {
            foreach(MiniScriptScriptableObject m in mso)
            {
                if(m.GetGuid == scriptableObject.GetGuid) { return true; }
            }

            return false;
        }
        /// <summary>
        /// Returns true if instance is found in the list, false otherwise.
        /// </summary>
        /// <param name="mso"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool ContainsMSO(this List<MiniScriptScriptableObject> mso, Guid id)
        {
            foreach (MiniScriptScriptableObject m in mso)
            {
                if (m.GetGuid == id) { return true; }
            }

            return false;
        }
        /// <summary>
        /// Returns true if instance is found in the list, false otherwise.
        /// </summary>
        /// <param name="mso"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static bool ContainsMSO(this List<MiniScriptScriptableObject> mso, string label)
        {
            foreach (MiniScriptScriptableObject m in mso)
            {
                if (m.Label == label) { return true; }
            }

            return false;
        }

        /// <summary>
        /// Returns true if the MiniScriptScriptableObject is found in the list, on false return the 'result' is set to null
        /// </summary>
        /// <param name="mso"></param>
        /// <param name="id"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryGet(this List<MiniScriptScriptableObject> mso, Guid id, out MiniScriptScriptableObject result)
        {
            result = null;
            foreach(MiniScriptScriptableObject so in mso)
            {
                if(so.GetGuid.Equals(id)) { result = so; return true; }
            }

            return false;
        }

        /// <summary>
        /// Returns true if the MiniScriptScriptableObject is found in the list, on false return the 'result' is set to null
        /// </summary>
        /// <param name="mso"></param>
        /// <param name="label"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryGet(this List<MiniScriptScriptableObject> mso, string label, out MiniScriptScriptableObject result)
        {
            result = null;
            foreach(MiniScriptScriptableObject so in mso)
            {
                if(so.Label == label) { result = so; return true; }
            }

            return false;
        }
    }
}

