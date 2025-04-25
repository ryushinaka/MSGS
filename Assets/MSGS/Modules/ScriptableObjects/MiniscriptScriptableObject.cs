using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace MiniScript.MSGS.ScriptableObjects
{
    /// <summary>
    /// Base class for the MiniScript objects within Unity (this is not inclusive to those derived from the abstract base Value class)
    /// </summary>
    public class MiniScriptScriptableObject : SerializedScriptableObject
    {
        private Guid myGuid = System.Guid.NewGuid();
        /// <summary>
        /// Unique ID propery for each ScriptableObject instance
        /// </summary>      
        public Guid GetGuid { get { return myGuid; } }
       
        [Tooltip("The name of this script object, allows lookup/referencing it")]
        public string Label;

        public MiniScriptScriptableObject()
        {
            
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

