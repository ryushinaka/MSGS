using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniScript.MSGS.Data;
using MiniScript.MSGS.Unity;
using MiniScript.MSGS.ScriptableObjects;
using MiniScript.MSGS.Unity.DevConsole;

namespace MiniScript.MSGS.Time
{
    public static class TimeKeeper
    {
        static ValMap timeIntrinsics;
        static volatile int _running, _paused;
        internal static bool debug;
        static float tickduration = 0f;
        static float accumulated = 0f;
        public static double tickCounter = 0;

        public static UnityEngine.Events.UnityEvent tickEvent;
        static List<string> scripts = new List<string>();
        static object scriptLock = new object();

        static void handleTickEvent()
        {
            if (scripts.Count > 0)
            {
                lock (scriptLock)
                {
                    for (int i = 0; i < scripts.Count; i++)
                    {
                        var ones = ScriptableObject.CreateInstance<OneShotScript>();
                        ones.scriptSource = MiniScriptSingleton.Scripts[scripts[i]];
                        ones.Compile();
                        ones.AddGlobal("tickCounter", new ValNumber(tickCounter));
                        ones.RunSync();
                    }
                }
            }
        }

        public static void SetTickDuration(float duration)
        {
            tickduration = duration;
        }

        public static void Initialize()
        {
            _running = 0; _paused = 1;
            timeIntrinsics = new ValMap();
            tickEvent = new UnityEngine.Events.UnityEvent();
            tickEvent.AddListener(new UnityEngine.Events.UnityAction(handleTickEvent));

            var a = Intrinsic.Create("");
            #region Start
            a.code = (context, partialResult) =>
            {
                TimeKeeper.Start();
                return new Intrinsic.Result(null, true);
            };

            timeIntrinsics.map.Add(new ValString("Start"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region Stop
            a.code = (context, partialResult) =>
            {
                TimeKeeper.Stop();
                return new Intrinsic.Result(null, true);
            };

            timeIntrinsics.map.Add(new ValString("Stop"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region Pause
            a.code = (context, partialResult) =>
            {
                TimeKeeper.Pause();
                return new Intrinsic.Result(null, true);
            };

            timeIntrinsics.map.Add(new ValString("Pause"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region Resume
            a.code = (context, partialResult) =>
            {
                TimeKeeper.Resume();
                return new Intrinsic.Result(null, true);
            };

            timeIntrinsics.map.Add(new ValString("Resume"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region SetTickDuration
            a.AddParam("duration", new ValNumber(1f));
            a.code = (context, partialResult) =>
            {
                TimeKeeper.SetTickDuration(context.GetLocalFloat("duration"));
                return new Intrinsic.Result(null, true);
            };

            timeIntrinsics.map.Add(new ValString("SetTickDuration"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region AddScript
            a.AddParam("script", string.Empty);
            a.code = (context, partialResult) =>
            {
                if (MiniScriptSingleton.ScriptExists(context.GetLocalString("script")))
                {
                    lock (scriptLock) { scripts.Add(context.GetLocalString("script")); }
                }
                return new Intrinsic.Result(null, true);
            };

            timeIntrinsics.map.Add(new ValString("AddScript"), a.GetFunc());
            #endregion

            a = Intrinsic.Create("");
            #region RemoveScript
            a.AddParam("script", string.Empty);
            a.code = (context, partialResult) =>
            {
                if (scripts.Contains(context.GetLocalString("script")))
                {
                    lock(scriptLock) { scripts.Remove(context.GetLocalString("script")); }
                }
                return new Intrinsic.Result(null, true);
            };

            timeIntrinsics.map.Add(new ValString("RemoveScript"), a.GetFunc());
            #endregion
        }

        public static void Start()
        {
            if (_running == 0)
            {
                _running = 1; _paused = 0;
            }
        }

        public static void Stop()
        {
            if (_running == 1)
            {
                _running = 0; _paused = 0;
            }
        }

        public static void Pause()
        {
            if (_running == 1)
            {
                _paused = 1; _running = 1;
            }
        }

        public static void Resume()
        {
            if (_paused == 1)
            {
                _running = 1; _paused = 0;
            }
        }

        public static void Update(float elapsed) {
            if (_running == 1) {
                if(_paused == 0)
                {
                    if (accumulated + elapsed >= tickduration)
                    {
                        accumulated = (accumulated + elapsed) - tickduration;
                        tickCounter++;
                        tickEvent.Invoke();
                    }
                    else { accumulated += elapsed; }
                }
            }
        }

        public static void AddScript(string name, ref MiniScriptScriptAsset mssa) { }
        public static void AddScript(string name, ref OneShotScript ones) { }
        public static void AddScript(string name, ref ConsoleCommandSO ccso) { }
        public static void AddScript(string name, string source) { }

        public static void RemoveScript(string name) { }

        public static List<string> ScriptNames() { return null; }

        public static int ScriptQuantity() { return 0; }

        public static ValMap Get()
        {
            if (timeIntrinsics == null) { Initialize(); }
            return timeIntrinsics;
        }

        static ValMap properties;
        public static ValMap GetProperties()
        {
            if (properties == null) { properties = new ValMap(); properties.assignOverride = new ValMap.AssignOverrideFunc(AssignOverride); }



            return properties;
        }

        static bool AssignOverride(Value a, Value b)
        {

            return false;
        }
    }
}

