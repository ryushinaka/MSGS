using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using MiniScript.MSGS.Data.Globals;
using UnityEditor;
using UnityEngine;

namespace MiniScript.MSGS.ScriptableObjects
{
    [Manageable]
    /// <summary>
    /// Base class for the MiniScript objects within Unity (this is not inclusive to those derived from the abstract base Value class)
    /// </summary>
    public class MiniScriptScriptableObject : SerializedScriptableObject
    {
        private Guid myGuid = System.Guid.NewGuid();
        /// <summary>
        /// Unique ID propery for each ScriptableObject instance
        /// </summary>
        /// <remarks>If the ScriptableObject is created in Unity as part of the Project, then the Editor will include the
        /// SO as part of the final build with the same GUID value it was given when it was created in the Project folder.
        /// If the ScriptableObject was instanced at runtime, this will help differentiate it from other instances of the same type 
        /// if that is the pattern chosen by the developer(s).</remarks>
        public Guid GetGuid { get { return myGuid; } }
        
        //[PropertyTooltip("Tells Unity and the Interpreter how to handle this object")]
        //public MiniScriptScriptableObjectType ScriptableObjectType;

        [Tooltip("The name of this script object, allows lookup/referencing it")]
        public string Label;

        public MiniScriptScriptableObject()
        {
            //when these are instances using ScriptableObject.CreateInstance() we need to tell
            //the MiniScript singleton that this has happened and update the DataStoreWarehouse
            //provided this MiniScriptScriptableObject is a DataType
            //if (ScriptableObjectType == MiniScriptScriptableObjectType.DataType)
            //{
            //    //register the object's creation with the DataStoreWarehouse
            //    string result = string.Empty;
            //    if(!MiniScriptGlobalsContainer.RegisterScriptableObject(this, out result))
            //    {
            //        MiniScriptSingleton.LogError("MSO error: " + result);
            //    }
            //}
        }
    }

    public static class MiniScriptScriptableObjectExtensions
    {
        /// <summary>
        /// Returns true if the MiniScriptScriptableObject object type has the Attribute of 'List'
        /// </summary>
        /// <param name="mso"></param>
        /// <returns></returns>
        public static bool IsList(this MiniScriptScriptableObject mso)
        {
            if (mso is IsListAttribute) { return true; }

            return false;
        }
        /// <summary>
        /// Returns true if the MiniScriptScriptableObject object type has the Attribute of 'Dictionary'
        /// </summary>
        /// <param name="mso"></param>
        /// <returns></returns>
        public static bool IsDictionary(this MiniScriptScriptableObject mso)
        {
            if (mso is IsDictionaryAttribute) { return true; }

            return false;
        }

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

    /// <summary>
    /// Convenience enum to help OdinInspector determine how to express ValNumber in Unity's Inspector panel
    /// </summary>
    public enum MiniScriptVariableType
    {
        ValString, ValBool, ValFloat, ValDouble, ValInt32
    }

    public enum MiniScriptScriptableObjectType
    {
        /// <summary>
        /// Specifies that a Script Asset should have its content treated as Intrinsic declaration(s) for the Interpreter
        /// </summary>
        Intrinsics,
        /// <summary>
        /// Specifies that the Script Asset should be treated as an embedded script, always available to the Interpreter from the Unity Build
        /// </summary>
        EmbeddedScript,
        /// <summary>
        /// Specifies that the Script Asset should be treated as a unique value associated with the given Label property, which is made available to the Interpreter as a ValMap
        /// </summary>
        EmbeddedValue,
        /// <summary>
        /// Specifies that the ScriptableObject is an embedded definition for a Type of object to be declared and made available for the Interpreter
        /// </summary>
        DataType,        
    }

    public interface IDatagraphProperties
    {
        object DatagraphProperties();
    }

    public interface IDatagraphValue
    {
        Value GetValue();
    }
}

