using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MiniScript.MSGS.Audio;
using System;
using System.Threading.Tasks;
using MiniScript.MSGS.Time;

namespace MiniScript.MSGS.Unity
{
    public class MSGSSingleton : MonoBehaviour
    {
        System.Diagnostics.Stopwatch stopwatch;

        void Start()
        {
            stopwatch = new System.Diagnostics.Stopwatch();            
        }

        private void Update()
        {

        }

        private void FixedUpdate()
        {
            TimeKeeper.Update(UnityEngine.Time.deltaTime); //update the game clock based on time elapsed

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
                    //if (stopwatch.ElapsedTicks >= magicNumber) return;
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

        static MSGSSingleton _instance = null;
        public static bool Exists() { return _instance != null; }

        public static MSGSSingleton Instance()
        {
            if (!Exists()) { Debug.LogError("MSGS Singleton does not exist!"); }
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
