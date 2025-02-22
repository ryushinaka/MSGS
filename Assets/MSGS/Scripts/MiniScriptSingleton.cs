using MiniScript.MSGS.Data;
using MiniScript.MSGS.Data.Globals;
using MiniScript.MSGS.MUUI;
using MiniScript.MSGS.Audio;
using MiniScript.MSGS.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace MiniScript.MSGS
{
    public static class MiniScriptSingleton
    {
        //public static GameModification currentMod;// = new GameModification();
        public static Dictionary<string, string> Scripts = new Dictionary<string, string>();

        //Task Scheduler for scripts executed without a Unity GameObject association
        public static MiniScriptScriptScheduler scheduler;
        //
        static DataStoreMessagePool datamessagepool;
        //
        static InputEventMessagePool inputmessagepool;
        //
        public static ScriptableObjectContainer scriptableObjects;

        //UI prefabs for instancing/managing
        public static PrefabContainer PrefabContainer;
        
        //UI event queue/pump/consumer
        public static IMiniScriptEventSink EventSink;
        
        //reference to the Audio system, script accessible
        public static AudioManager Audio;

        static System.Threading.Thread myThread;

        public static object ThingsToDoLock;
        public static Queue<Action> ThingsToDo;
        public static int ThingsToDoCount
        {
            get { return ThingsToDo.Count; }
        }

        public static void QueueScript(ref System.Threading.WaitCallback callback)
        {
            
        }

        public static void LogWarning(string v)
        {
#if UNITY_EDITOR
            Debug.LogWarning("Editor: " + v);
#else
            
#endif
        }
        
        public static void LogError(string v)
        {
#if UNITY_EDITOR
            Debug.LogError("Editor: " + v);
#else
            
#endif
        }
       
        public static void LogInfo(string v)
        {
#if UNITY_EDITOR
            Debug.Log("Editor: " + v);
#else
            
#endif
        }
        
        static void OnGameTimeTick(GameTimeStruct time)
        {
            //scripts[UserAction.INTERNAL_GameTimeTick].RunUntilDone();
        }

        static void StandardOutput(string value)
        {
#if UNITY_EDITOR
            Debug.Log("MiniScriptSingleton.StandardOutput: " + value);
#endif
        }

        static void ErrorOutput(string value)
        {
#if UNITY_EDITOR
            Debug.Log("MiniScriptSingleton.ErrorOutput: " + value);
#endif
        }

        static MiniScriptSingleton()
        {
            //Application.targetFrameRate = 60;
            ThingsToDoLock = new object();
            ThingsToDo = new Queue<Action>();
            EventSink = new DefaultEventSink();
            scriptableObjects = new ScriptableObjectContainer();
            scheduler = new MiniScriptScriptScheduler(25);
            //myThread = new System.Threading.Thread(
            //    new System.Threading.ParameterizedThreadStart(ThreadLoop));

            //PrefabContainer = Resources.Load<PrefabContainer>("MUUI_Prefabs");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.quitting += EditorApplication_quitting;
#else
            UnityEngine.Application.quitting += Application_quitting;
#endif
        }

        private static void EditorApplication_quitting()
        {
            
        }

        private static void Application_quitting()
        {
            
        }

        private static System.IO.FileStream TryOpen(string filePath, System.IO.FileMode fileMode, System.IO.FileAccess fileAccess, System.IO.FileShare fileShare, int maximumAttempts, int attemptWaitMS)
        {
            System.IO.FileStream fs = null;
            int attempts = 0;

            // Loop allow multiple attempts
            while (true)
            {
                try
                {
                    fs = System.IO.File.Open(filePath, fileMode, fileAccess, fileShare);

                    //If we get here, the File.Open succeeded, so break out of the loop and return the FileStream
                    break;
                }
                catch (System.IO.IOException)
                {
                    // Check the numbere of attempts to ensure no infinite loop
                    attempts++;
                    if (attempts > maximumAttempts)
                    {
                        // Too many attempts,cannot Open File, break and return null 
                        fs = null;
                        break;
                    }
                    else
                    {
                        // Sleep before making another attempt
                        System.Threading.Thread.Sleep(attemptWaitMS);
                    }
                }
            }
            // Reutn the filestream, may be valid or null
            return fs;
        }

        internal static bool ScriptExists(string v)
        {
            return false;
            //throw new NotImplementedException();
        }

        internal static bool PrefabExists(string ss)
        {
            throw new NotImplementedException();
        }
    }
}