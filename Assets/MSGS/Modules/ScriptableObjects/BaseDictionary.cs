using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using MiniScript.MSGS.Data.Globals;

namespace MiniScript.MSGS.ScriptableObjects
{
    /// <summary>
    /// Provides a base class of a Dictionary as a ScriptableObject that can be exposed to scripts at runtime.
    /// </summary>
    /// <remarks>After the Register() method is called at runtime, this ScriptableObject will be part of the Globals
    /// container that is passed to each Script when they are executed.</remarks>
    public abstract class BaseDictionary : MiniScriptScriptableObject, IDatagraphProperties, IsDictionaryAttribute
    {   
        public abstract string GetAddress { get; set; }

        public abstract ValMap GetValMap();

        public abstract object DatagraphProperties();

        /// <summary>
        /// Adds this ScriptableObject to the Globals container in MiniScript
        /// </summary>
        public void Register(bool disallowChanges)
        {
            //register the object's creation with the container for Globals exposed to MiniScript scripts            
            if (MiniScriptSingleton.scriptableObjects.ContainsName(this.Label))
            {
                MiniScriptSingleton.LogError("ScriptableObjectContainer.Register: Label('" + this.Label + 
                    "') is already in use by another object. SO not registered.");
            }
            else
            {
                MiniScriptSingleton.scriptableObjects.Add(this);
            }
        }

        /// <summary>
        /// Removes this ScriptableObject from the Globals container in MiniScript
        /// </summary>
        public void Unregister()
        {
            MiniScriptSingleton.scriptableObjects.Remove(this);
        }
    }

    public static class BaseDictionaryExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="disallowUpdates"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool RegisterScriptableObject(this BaseDictionary dict, bool disallowChanges, out string msg)
        {
            if (!MiniScriptSingleton.scriptableObjects.ContainsName(dict.Label))
            {
                MiniScriptSingleton.scriptableObjects.Add(dict);
            }
            else
            {
                msg = "Globals container already contains a ScriptableObject with that label (" + dict.Label + ")";
                return false;
            }

            msg = string.Empty;
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="disallowUpdates"></param>
        /// <param name="act"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool RegisterScriptableObject(this BaseDictionary dict, bool disallowChanges, Action act, out string msg)
        {
            if (!MiniScriptSingleton.scriptableObjects.ContainsName(dict.Label)) MiniScriptSingleton.scriptableObjects.Add(dict);
            else
            {
                msg = "Globals container already contains a ScriptableObject with that label (" + dict.Label + ")";
                return false;
            }

            msg = string.Empty;
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static void UnregisterScriptableObject(this BaseDictionary dict)
        {
            if (MiniScriptSingleton.scriptableObjects.ContainsName(dict.Label))
            {
                MiniScriptSingleton.scriptableObjects.Remove(dict);
            }
        }
    }
}

