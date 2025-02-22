using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniScript.MSGS.Data.Globals;
using System;

namespace MiniScript.MSGS.ScriptableObjects
{
    /// <summary>
    /// Base class for a List data type that is also a ScriptableObject
    /// </summary>
    /// <remarks>If you have some data you want to express as a List (ValList to the scripting), and you also
    /// want this data to be handled by Unity specifically, use this object as a ScriptableObject instance.
    /// </remarks>
    public abstract class BaseList : MiniScriptScriptableObject, IDatagraphProperties, IsListAttribute
    {
        public abstract string GetAddress { get; set; }

        public abstract object DatagraphProperties();

        public abstract ValList GetValList();

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
       

    public static class BaseListExtensions
    {
        /// <summary>
        /// Adds this ScriptableObject to the Globals container so that scripts can access its values
        /// </summary>
        /// <param name="list"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool RegisterScriptableObject(this BaseList list, bool disallowChanges, out string msg)
        {
            if(!MiniScriptSingleton.scriptableObjects.ContainsName(list.Label)) MiniScriptSingleton.scriptableObjects.Add(list);
            else
            {
                msg = "Globals container already contains a ScriptableObject with that label (" + list.Label + ")";
                return false;
            }

            msg = string.Empty;
            return true;
        }

        public static bool RegisterScriptableObject(this BaseList list, bool disallowChanges, Action act, out string msg)
        {
            if (!MiniScriptSingleton.scriptableObjects.ContainsName(list.Label)) MiniScriptSingleton.scriptableObjects.Add(list);
            else
            {
                msg = "Globals container already contains a ScriptableObject with that label (" + list.Label + ")";
                return false;
            }

            msg = string.Empty;
            return true;
        }

        /// <summary>
        /// Removes this ScriptableObject from the Globals container
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static void UnregisterScriptableObject(this BaseList list,bool disallowChanges)
        {   
            if(MiniScriptSingleton.scriptableObjects.ContainsName(list.Label))
            {
                MiniScriptSingleton.scriptableObjects.Remove(list);
            }
        }
    }
}

