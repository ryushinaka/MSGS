using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MiniScript.MSGS.ScriptableObjects;

namespace MiniScript.MSGS.Unity
{
    /// <summary>
    /// Executes the attached scripts asynchronously (off the unity main thread)
    /// </summary>
    public class MiniScriptBehaviourAsync : MonoBehaviour
    {
        #region
        [Tooltip("The List of scripts to execute in this GameObjects OnAwake callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnAwakeEvent;

        [Tooltip("The List of scripts to execute in this GameObjects OnFixedUpdate callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnFixedUpdateEvent;

        [Tooltip("The List of scripts to execute in this GameObjects OnLateUpdate callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnLateUpdateEvent;

        [Tooltip("The List of scripts to execute in this GameObjects OnApplicationPause callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnApplicationPauseEvent;

        [Tooltip("The List of scripts to execute in this GameObjects OnApplicationFocus callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnApplicationFocusEvent;

        [Tooltip("The List of scripts to execute in this GameObjects OnApplicationQuit callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnApplicationQuitEvent;

        [Tooltip("The List of scripts to execute in this GameObjects OnDestroy callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnDestroyEvent;

        [Tooltip("The List of scripts to execute in this GameObjects OnEnable callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnEnableEvent;

        [Tooltip("The List of scripts to execute in this GameObjects OnDisable callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnDisableEvent;

        [Tooltip("The List of scripts to execute in this GameObjects OnStart callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnStartEvent;

        [Tooltip("The List of scripts to execute in this GameObjects OnUpdate callback"), SerializeField]
        public List<MiniScriptScriptAsset> OnUpdateEvent;
        #endregion

        public Interpreter intp;

        #region Unity Callbacks
        private void OnAwake()
        {
            intp = new Interpreter();
        }
        private void OnFixedUpdate() { }
        private void OnLateUpdate() { }
        private void OnApplicationPause(bool pause) { }
        private void OnApplicationFocus(bool focus) { }
        private void OnApplicationQuit() { }
        private void OnDestroy() { }
        private void OnEnable() { }
        private void OnDisable() { }

        private void Start()
        {

        }

        private void Update()
        {

        }
        #endregion
    }
}
