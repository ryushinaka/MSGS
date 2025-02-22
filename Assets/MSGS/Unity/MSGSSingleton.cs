using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MiniScript.MSGS.Audio;
using System;
using System.Threading.Tasks;

namespace MiniScript.MSGS.Unity
{
    public class MSGSSingleton : MonoBehaviour
    {
        System.Diagnostics.Stopwatch stopwatch;
        [Tooltip("How many ticks to allow for processing queued Action's per frame")]
        public long magicNumber = 500000;

        void Start()
        {
            stopwatch = new System.Diagnostics.Stopwatch();

            var go = new GameObject();
            go.transform.localScale = new Vector3(1, 1, 1);
            go.transform.SetParent(this.transform);
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.AddComponent<AudioManager>();
        }

        private void Update()
        {
            //update UI values/properties here

        }

        private void FixedUpdate()
        {
            //update physics here, the transforms for everything
        }

        void LateUpdate()
        {
            lock (MiniScriptSingleton.ThingsToDoLock)
            {
                stopwatch.Reset();

                //code edit provided by Abledbody on 3/28/2024 to provide a more concise logic construct
                if (!MiniScriptSingleton.ThingsToDo.TryDequeue(out var action)) return;
                do
                {
                    stopwatch.Start(); action?.Invoke(); stopwatch.Stop();
                    if (stopwatch.ElapsedTicks >= magicNumber) return;
                }
                while (MiniScriptSingleton.ThingsToDo.TryDequeue(out action));

                //my original code: 3/28/2024 Ryushinaka
                //bool DoThings = true;
                //if (MiniScriptSingleton.ThingsToDoCount > 0)
                //{
                //    while (DoThings)
                //    {
                //        stopwatch.Start();
                //        MiniScriptSingleton.ThingsToDo.Dequeue().Invoke();
                //        stopwatch.Stop();

                //        if (MiniScriptSingleton.ThingsToDoCount > 0 &&
                //            stopwatch.ElapsedTicks <= magicNumber)
                //        {
                //            DoThings = true;
                //        }
                //        else
                //        {
                //            DoThings = false;
                //            stopwatch.Reset();
                //        }
                //    }
                //}
            }
        }

        private static readonly Queue<Action> _executionQueue = new Queue<Action>();

        /// <summary>
        /// Locks the queue and adds the IEnumerator to the queue
        /// </summary>
        /// <param name="action">IEnumerator function that will be executed from the main thread.</param>
        public void Enqueue(IEnumerator action)
        {
            lock (_executionQueue)
            {
                _executionQueue.Enqueue(() =>
                {
                    StartCoroutine(action);
                });
            }
        }

        /// <summary>
        /// Locks the queue and adds the Action to the queue
        /// </summary>
        /// <param name="action">function that will be executed from the main thread.</param>
        public void Enqueue(Action action)
        {
            Enqueue(ActionWrapper(action));
        }

        /// <summary>
        /// Locks the queue and adds the Action to the queue, returning a Task which is completed when the action completes
        /// </summary>
        /// <param name="action">function that will be executed from the main thread.</param>
        /// <returns>A Task that can be awaited until the action completes</returns>
        public Task EnqueueAsync(Action action)
        {
            var tcs = new TaskCompletionSource<bool>();

            void WrappedAction()
            {
                try
                {
                    action();
                    tcs.TrySetResult(true);
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            }

            Enqueue(ActionWrapper(WrappedAction));
            return tcs.Task;
        }

        IEnumerator ActionWrapper(Action a)
        {
            a();
            yield return null;
        }

        private static MSGSSingleton _instance = null;

        public static bool Exists()
        {
            return _instance != null;
        }

        public static MSGSSingleton Instance()
        {
            if (!Exists())
            {
                throw new Exception("UnityMainThreadDispatcher could not find the UnityMainThreadDispatcher object. Please ensure you have added the MainThreadExecutor Prefab to your scene.");
            }
            return _instance;
        }

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        void OnDestroy()
        {
            _instance = null;
        }
    }
}
