using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MiniScript.MSGS.ScriptableObjects;
using MiniScript.MSGS.Scripts;
using MiniScript.MSGS.Data;

namespace MiniScript.MSGS.Unity
{
    /// <summary>
    /// Executes the attached scripts synchronously
    /// </summary>
    public class MiniScriptBehaviour : MonoBehaviour
    {
        #region
        [Tooltip("The List of scripts to execute in this GameObject's OnAwake callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnAwakeEvent;

        [Tooltip("The List of scripts to execute in this GameObject's OnFixedUpdate callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnFixedUpdateEvent;

        [Tooltip("The List of scripts to execute in this GameObject's OnLateUpdate callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnLateUpdateEvent;

        [Tooltip("The List of scripts to execute in this GameObject's OnApplicationPause callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnApplicationPauseEvent;

        [Tooltip("The List of scripts to execute in this GameObject's OnApplicationFocus callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnApplicationFocusEvent;

        [Tooltip("The List of scripts to execute in this GameObject's OnApplicationQuit callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnApplicationQuitEvent;

        [Tooltip("The List of scripts to execute in this GameObject's OnDestroy callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnDestroyEvent;

        [Tooltip("The List of scripts to execute in this GameObject's OnEnable callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnEnableEvent;

        [Tooltip("The List of scripts to execute in this GameObject's OnDisable callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnDisableEvent;

        [Tooltip("The List of scripts to execute in this GameObject's OnStart callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnStartEvent;

        [Tooltip("The List of scripts to execute in this GameObject's OnUpdate callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnUpdateEvent;
        #endregion

        public Interpreter intp;

        #region Unity Callbacks
        private void OnAwake()
        {
            if (OnAwakeEvent != null)
            {
                if (OnAwakeEvent.Count > 0)
                {
                    foreach (MiniScriptScriptAsset mssa in OnAwakeEvent)
                    {
                        intp.Reset(mssa.GetScriptFull()); intp.Compile();
                        ScriptModuleConfiguration.AssignGlobals(ref intp);
                        
                        intp.hostData = new CustomHostData();
                        intp.standardOutput = new TextOutputMethod(StdOutput);
                        intp.errorOutput = new TextOutputMethod(ErrOutput);
                        intp.implicitOutput = new TextOutputMethod(ImplicitOutput);
                        intp.RunUntilDone();
                    }
                }
            }
        }
        private void OnFixedUpdate()
        {
            if (OnFixedUpdateEvent != null)
            {
                if (OnFixedUpdateEvent.Count > 0)
                {
                    foreach (MiniScriptScriptAsset mssa in OnFixedUpdateEvent)
                    {
                        intp.Reset(mssa.GetScriptFull()); intp.Compile();
                        ScriptModuleConfiguration.AssignGlobals(ref intp);
                        
                        intp.hostData = new CustomHostData();
                        intp.standardOutput = new TextOutputMethod(StdOutput);
                        intp.errorOutput = new TextOutputMethod(ErrOutput);
                        intp.implicitOutput = new TextOutputMethod(ImplicitOutput);
                        intp.RunUntilDone();
                    }
                }
            }
        }
        private void OnLateUpdate()
        {
            if (OnLateUpdateEvent != null)
            {
                if (OnLateUpdateEvent.Count > 0)
                {
                    foreach (MiniScriptScriptAsset mssa in OnLateUpdateEvent)
                    {
                        intp.Reset(mssa.GetScriptFull()); intp.Compile();
                        ScriptModuleConfiguration.AssignGlobals(ref intp);

                        intp.hostData = new CustomHostData();
                        intp.standardOutput = new TextOutputMethod(StdOutput);
                        intp.errorOutput = new TextOutputMethod(ErrOutput);
                        intp.implicitOutput = new TextOutputMethod(ImplicitOutput);
                        intp.RunUntilDone();
                    }
                }
            }
        }
        private void OnApplicationPause(bool pause)
        {
            if (OnApplicationPauseEvent != null)
            {
                if (OnApplicationPauseEvent.Count > 0)
                {
                    foreach (MiniScriptScriptAsset mssa in OnApplicationPauseEvent)
                    {
                        intp.Reset(mssa.GetScriptFull()); intp.Compile();
                        ScriptModuleConfiguration.AssignGlobals(ref intp);

                        intp.SetGlobalValue("pause", ValNumber.Truth(pause));
                        intp.hostData = new CustomHostData();
                        intp.standardOutput = new TextOutputMethod(StdOutput);
                        intp.errorOutput = new TextOutputMethod(ErrOutput);
                        intp.implicitOutput = new TextOutputMethod(ImplicitOutput);
                        intp.RunUntilDone();
                    }
                }
            }
        }
        private void OnApplicationFocus(bool focus)
        {
            if (OnApplicationPauseEvent != null)
            {
                if (OnApplicationPauseEvent.Count > 0)
                {
                    foreach (MiniScriptScriptAsset mssa in OnApplicationPauseEvent)
                    {
                        intp.Reset(mssa.GetScriptFull()); intp.Compile();
                        ScriptModuleConfiguration.AssignGlobals(ref intp);
                        
                        intp.SetGlobalValue("focus", ValNumber.Truth(focus));
                        
                        intp.hostData = new CustomHostData();
                        intp.standardOutput = new TextOutputMethod(StdOutput);
                        intp.errorOutput = new TextOutputMethod(ErrOutput);
                        intp.implicitOutput = new TextOutputMethod(ImplicitOutput);
                        intp.RunUntilDone();
                    }
                }
            }
        }
        private void OnApplicationQuit()
        {
            if (OnApplicationQuitEvent != null)
            {
                if (OnApplicationQuitEvent.Count > 0)
                {
                    foreach (MiniScriptScriptAsset mssa in OnApplicationQuitEvent)
                    {
                        intp.Reset(mssa.GetScriptFull()); intp.Compile();
                        ScriptModuleConfiguration.AssignGlobals(ref intp);

                        intp.hostData = new CustomHostData();
                        intp.standardOutput = new TextOutputMethod(StdOutput);
                        intp.errorOutput = new TextOutputMethod(ErrOutput);
                        intp.implicitOutput = new TextOutputMethod(ImplicitOutput);
                        intp.RunUntilDone();
                    }
                }
            }
        }
        private void OnDestroy()
        {
            if (OnDestroyEvent != null)
            {
                if (OnDestroyEvent.Count > 0)
                {
                    foreach (MiniScriptScriptAsset mssa in OnDestroyEvent)
                    {
                        intp.Reset(mssa.GetScriptFull()); intp.Compile();
                        ScriptModuleConfiguration.AssignGlobals(ref intp);

                        intp.hostData = new CustomHostData();
                        intp.standardOutput = new TextOutputMethod(StdOutput);
                        intp.errorOutput = new TextOutputMethod(ErrOutput);
                        intp.implicitOutput = new TextOutputMethod(ImplicitOutput);
                        intp.RunUntilDone();
                    }
                }
            }
        }
        private void OnEnable()
        {
            if (OnEnableEvent != null)
            {
                if (OnEnableEvent.Count > 0)
                {
                    foreach (MiniScriptScriptAsset mssa in OnEnableEvent)
                    {
                        intp.Reset(mssa.GetScriptFull()); intp.Compile();
                        ScriptModuleConfiguration.AssignGlobals(ref intp);

                        intp.hostData = new CustomHostData();
                        intp.standardOutput = new TextOutputMethod(StdOutput);
                        intp.errorOutput = new TextOutputMethod(ErrOutput);
                        intp.implicitOutput = new TextOutputMethod(ImplicitOutput);
                        intp.RunUntilDone();
                    }
                }
            }
        }
        private void OnDisable()
        {
            if (OnDisableEvent != null)
            {
                if (OnDisableEvent.Count > 0)
                {
                    foreach (MiniScriptScriptAsset mssa in OnDisableEvent)
                    {
                        intp.Reset(mssa.GetScriptFull()); intp.Compile();
                        ScriptModuleConfiguration.AssignGlobals(ref intp);

                        intp.hostData = new CustomHostData();
                        intp.standardOutput = new TextOutputMethod(StdOutput);
                        intp.errorOutput = new TextOutputMethod(ErrOutput);
                        intp.implicitOutput = new TextOutputMethod(ImplicitOutput);
                        intp.RunUntilDone();
                    }
                }
            }
        }
        private void Start()
        {
            intp = new Interpreter();
            if (OnStartEvent != null)
            {
                if (OnStartEvent.Count > 0)
                {
                    foreach (MiniScriptScriptAsset mssa in OnStartEvent)
                    {
                        intp.Reset(mssa.GetScriptFull()); intp.Compile();
                        ScriptModuleConfiguration.AssignGlobals(ref intp);

                        intp.hostData = new CustomHostData();
                        intp.standardOutput = new TextOutputMethod(StdOutput);
                        intp.errorOutput = new TextOutputMethod(ErrOutput);
                        intp.implicitOutput = new TextOutputMethod(ImplicitOutput);
                        intp.RunUntilDone();
                    }
                }
            }
        }
        private void Update()
        {
            if (OnUpdateEvent != null)
            {
                if (OnUpdateEvent.Count > 0)
                {
                    foreach (MiniScriptScriptAsset mssa in OnUpdateEvent)
                    {
                        intp.Reset(mssa.GetScriptFull()); intp.Compile();
                        ScriptModuleConfiguration.AssignGlobals(ref intp);

                        intp.hostData = new CustomHostData();
                        intp.standardOutput = new TextOutputMethod(StdOutput);
                        intp.errorOutput = new TextOutputMethod(ErrOutput);
                        intp.implicitOutput = new TextOutputMethod(ImplicitOutput);
                        intp.RunUntilDone();
                    }
                }
            }
        }
        #endregion

        #region interpreter callbacks
        void StdOutput(string msg) { Debug.Log("std: " + msg); }
        void ErrOutput(string msg) { Debug.Log("err: " + msg); }
        void ImplicitOutput(string msg) { Debug.Log("imp: " + msg); }
        #endregion
    }
}
