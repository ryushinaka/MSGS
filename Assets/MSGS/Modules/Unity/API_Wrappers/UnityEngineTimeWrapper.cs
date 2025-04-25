using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace MiniScript.MSGS.Unity
{
    public static class UnityEngineTimeWrapper
    {
        public static bool debug = false;
        public static ValMap Get()
        {
            ValMap map = new ValMap();

            var a = Intrinsic.Create("");
            a.AddParam("scale", 1);
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.Time.timeScale(" + context.GetLocalString("scale") + ")");
                if (context.GetLocalString("scale").Length > 0)
                {
                    var wi = AlternateThreadDispatcher.Get();
                    wi.Module = UnityModuleName.Time;
                    wi.FunctionName = UnityEngineTimeFunctions.timeScale;
                    wi.args = new object[1] { context.GetLocalFloat("scale") };

                    AlternateThreadDispatcher.Enqueue(ref wi);
                    wi.eventSlim.Wait();
                    var result = (float)wi.result;
                    AlternateThreadDispatcher.Return(ref wi);

                    return new Intrinsic.Result(result);
                }
                else
                {
                    var wi = AlternateThreadDispatcher.Get();
                    wi.Module = UnityModuleName.Time;
                    wi.FunctionName = UnityEngineTimeFunctions.fixedTime;

                    AlternateThreadDispatcher.Enqueue(ref wi);
                    wi.eventSlim.Wait();
                    var result = (float)wi.result;
                    AlternateThreadDispatcher.Return(ref wi);

                    return new Intrinsic.Result(result);
                }
            };
            map.map.Add(new ValString("timeScale"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
                "timeScale",
                "The maximum value of Time.timeScale in any given frame. This is a time in seconds that limits the increase of Time.time between two frames.",
                "UnityEngine.Time",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter {
                        Name = "timeScale",
                        variableType = typeof(float),
                        Comment = "The maximum value to assign for Time.deltaTime in a frame." }
                },
                new IntrinsicParameter
                {
                    Name = "timeScale",
                    variableType = typeof(double),
                    Comment = "The maximum value to assign for Time.deltaTime in a frame (double precision)."
                }
            );

            a = Intrinsic.Create("");
            a.AddParam("mdt", 0);
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.Time.maximumDeltaTime(" + context.GetLocalString("mdt") + ")");
                if (context.GetLocalString("mdt").Length > 0)
                {
                    var wi = AlternateThreadDispatcher.Get();
                    wi.Module = UnityModuleName.Time;
                    wi.FunctionName = UnityEngineTimeFunctions.maximumDeltaTime;
                    wi.args = new object[1] { context.GetLocalFloat("mdt") };

                    AlternateThreadDispatcher.Enqueue(ref wi);
                    wi.eventSlim.Wait();
                    var result = (float)wi.result;
                    AlternateThreadDispatcher.Return(ref wi);

                    return new Intrinsic.Result(result);
                }
                else
                {
                    var wi = AlternateThreadDispatcher.Get();
                    wi.Module = UnityModuleName.Time;
                    wi.FunctionName = UnityEngineTimeFunctions.maximumDeltaTime;

                    AlternateThreadDispatcher.Enqueue(ref wi);
                    wi.eventSlim.Wait();
                    var result = (float)wi.result;
                    AlternateThreadDispatcher.Return(ref wi);

                    return new Intrinsic.Result(result);
                }
            };
            map.map.Add(new ValString("maximumDeltaTime"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
                "maximumDeltaTime",
                "The maximum value of Time.deltaTime in any given frame. This is a time in seconds that limits the increase of Time.time between two frames.",
                "UnityEngine.Time",
                new List<IntrinsicParameter>() {
                    new IntrinsicParameter {
                        Name = "maximumDeltaTime",
                        variableType = typeof(float),
                        Comment = "The maximum value to assign for Time.deltaTime in a frame." }
                },
                new IntrinsicParameter
                {
                    Name = "maximumDeltaTime",
                    variableType = typeof(double),
                    Comment = "The maximum value to assign for Time.deltaTime in a frame (double precision)."
                }
            );

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.Time.fixedTime()");

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Time;
                wi.FunctionName = UnityEngineTimeFunctions.fixedTime;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                var result = (double)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result);
            };
            map.map.Add(new ValString("fixedTime"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("fixedTime",
                "The double precision time since the last FixedUpdate started (Read Only). This is the time in seconds since the start of the game.",
                "UnityEngine.Time",
                new List<IntrinsicParameter>(),
                new IntrinsicParameter()
                {
                    Name = "fixedTime",
                    variableType = typeof(double),
                    Comment = "The double precision time since the last FixedUpdate started"
                }
            );

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.Time.deltaTime()");

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Time;
                wi.FunctionName = UnityEngineTimeFunctions.deltaTime;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                var result = (float)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result);
            };
            map.map.Add(new ValString("deltaTime"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("deltaTime",
                "The interval in seconds from the last frame to the current one (Read Only).",
                "UnityEngine.Time",
                new List<IntrinsicParameter>(),
                new IntrinsicParameter()
                {
                    Name = "deltaTime",
                    variableType = typeof(double),
                    Comment = "The interval in seconds from the last frame to the current one"
                }
            );

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.Time.fixedDeltaTime()");

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Time;
                wi.FunctionName = UnityEngineTimeFunctions.fixedDeltaTime;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                var result = (float)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result);
            };
            map.map.Add(new ValString("fixedDeltaTime"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("fixedDeltaTime",
            "The interval in seconds of in-game time at which physics and other fixed frame rate updates (like MonoBehaviour's FixedUpdate) are performed.",
            "UnityEngine.Time",
            new List<IntrinsicParameter>(),
            new IntrinsicParameter()
            {
                Name = "unscaledDeltaTime",
                variableType = typeof(double),
                Comment = "The interval in seconds of in-game time at which physics and other fixed frame rate updates."
            });

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.Time.unscaledDeltaTime()");

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Time;
                wi.FunctionName = UnityEngineTimeFunctions.unscaledDeltaTime;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                var result = (float)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result);
            };
            map.map.Add(new ValString("unscaledDeltaTime"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("unscaledDeltaTime",
            "The double precision timeScale-independent time for this frame (Read Only). This is the time in seconds since the start of the game.",
            "UnityEngine.Time",
            new List<IntrinsicParameter>(),
            new IntrinsicParameter()
            {
                Name = "unscaledDeltaTime",
                variableType = typeof(double),
                Comment = "The double precision timeScale-independent interval in seconds from the last frame to the current one."
            });

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.Time.smoothDeltaTime()");

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Time;
                wi.FunctionName = UnityEngineTimeFunctions.smoothDeltaTime;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                var result = (float)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result);
            };
            map.map.Add(new ValString("smoothDeltaTime"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("smoothDeltaTime",
           "A smoothed out Time.deltaTime (Read Only).",
           "UnityEngine.Time",
           new List<IntrinsicParameter>(),
           new IntrinsicParameter()
           {
               Name = "smoothDeltaTime",
               variableType = typeof(double),
               Comment = "A smoothed out Time.deltaTime"
           });

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.Time.timeSinceLevelLoad()");

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Time;
                wi.FunctionName = UnityEngineTimeFunctions.timeSinceLevelLoad;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                var result = (double)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result);
            };
            map.map.Add(new ValString("timeSinceLevelLoad"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("timeSinceLevelLoad",
           "The double precision time in seconds since the last non-additive scene finished loading (Read Only).",
           "UnityEngine.Time",
           new List<IntrinsicParameter>(),
           new IntrinsicParameter()
           {
               Name = "timeSinceLevelLoad",
               variableType = typeof(double),
               Comment = ""
           });

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.Time.unscaledTime()");

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Time;
                wi.FunctionName = UnityEngineTimeFunctions.unscaledTime;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                var result = (double)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result);
            };
            map.map.Add(new ValString("unscaledTime"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("unscaledTime",
            "The double precision timeScale-independent time for this frame (Read Only). This is the time in seconds since the start of the game.",
            "UnityEngine.Time",
            new List<IntrinsicParameter>(),
            new IntrinsicParameter()
            {
                Name = "unscaledTime",
                variableType = typeof(double),
                Comment = ""
            });

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.Time.realtimeSinceStartup()");

                //var wi = AlternateThreadDispatcher.Get();
                //wi.Module = UnityModuleName.Time;
                //wi.FunctionName = UnityEngineTimeFunctions.realtimeSinceStartup;

                //AlternateThreadDispatcher.Enqueue(ref wi);
                //wi.eventSlim.Wait();
                //var result = (float)wi.result;
                //AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(UnityCachedValues.realTimeSinceStartup);
            };
            map.map.Add(new ValString("realTimeSinceStartup"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("realTimeSinceStartup",
             "The real time in seconds since the game started (Read Only). Double precision version of realtimeSinceStartup.",
             "UnityEngine.Time",
             new List<IntrinsicParameter>(),
             new IntrinsicParameter()
             {
                 Name = "realTimeSinceStartup",
                 variableType = typeof(double),
                 Comment = ""
             });

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.Time.frameCount()");

                //this code is left here for documentation, as for some reason this code causes a hard lock
                //in the UnityEditor & standalone build. see UnityHacksStatic.cs for explanation

                //var wi = AlternateThreadDispatcher.Get();
                //wi.Module = UnityModuleName.Time;
                //wi.FunctionName = UnityEngineTimeFunctions.frameCount;

                //AlternateThreadDispatcher.Enqueue(ref wi);
                //wi.eventSlim.Wait();
                //var result = (int)wi.result;
                //AlternateThreadDispatcher.Return(ref wi);

                //return new Intrinsic.Result(result);
                return new Intrinsic.Result((double)UnityCachedValues.frameCount);
            };

            map.map.Add(new ValString("frameCount"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("frameCount",
              "The total number of frames since the start of the game (Read Only).",
              "UnityEngine.Time",
              new List<IntrinsicParameter>(),
              new IntrinsicParameter()
              {
                  Name = "frameCount",
                  variableType = typeof(double),
                  Comment = ""
              });

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.Time.timeAsDouble()");

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Time;
                wi.FunctionName = UnityEngineTimeFunctions.time;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                var result = (double)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result);
            };
            map.map.Add(new ValString("time"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("time",
               "The time at the beginning of the current frame in seconds since the start of the application (Read Only).",
               "UnityEngine.Time",
               new List<IntrinsicParameter>(),
               new IntrinsicParameter()
               {
                   Name = "time",
                   variableType = typeof(double),
                   Comment = ""
               });

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.Time.fixedUnscaledTime");

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Time;
                wi.FunctionName = UnityEngineTimeFunctions.fixedUnscaledTime;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                var result = (double)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result);
            };
            map.map.Add(new ValString("fixedUnscaledTime"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("fixedUnscaledTime",
                "The double precision time since the last FixedUpdate started (Read Only). This is the time in seconds since the start of the game.",
                "UnityEngine.Time",
                new List<IntrinsicParameter>(),
                new IntrinsicParameter()
                {
                    Name = "time",
                    variableType = typeof(double),
                    Comment = "The double precision time since the last FixedUpdate started."
                });

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.Time.maximumParticleDeltaTime()");

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Time;
                wi.FunctionName = UnityEngineTimeFunctions.maximumParticleDeltaTime;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                var result = (float)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result);
            };
            map.map.Add(new ValString("maximumParticleDeltaTime"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("maximumParticleDeltaTime",
                "The maximum time a frame can spend on particle updates. If the frame takes longer than this, then updates are split into multiple smaller updates.",
                "UnityEngine.Time",
                new List<IntrinsicParameter>()
                {
                    new IntrinsicParameter()
                    {
                        Name = "(optional) particleDeltaTime",
                        variableType = typeof(float),
                        Comment = "The time value to set for the limit of particle processing per frame."
                    }
                },
                new IntrinsicParameter()
                {
                    Name = "time",
                    variableType = typeof(float),
                    Comment = "The float value representing the time limit for particle processing per frame."
                });

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) Debug.Log("UnityEngine.Time.fixedUnscaledDeltaTime()");

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Time;
                wi.FunctionName = UnityEngineTimeFunctions.fixedUnscaledDeltaTime;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                var result = (float)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result);
            };
            map.map.Add(new ValString("fixedUnscaledDeltaTime"), a.GetFunc());

            IntrinsicsHelpMetadata.Create("fixedUnscaledDeltaTime",
                "The interval in seconds of timeScale-independent (\"real\") time at which physics and other fixed frame rate updates are performed.",
                "UnityEngine.Time",
                new List<IntrinsicParameter>()
                {
                    new IntrinsicParameter()
                    {
                        Name = "(optional) fixedUnscaledDeltaTime",
                        variableType = typeof(float),
                        Comment = "The new value to assign for the interval to update physics from."
                    }
                },
                new IntrinsicParameter()
                {
                    Name = "time",
                    variableType = typeof(float),
                    Comment = "The float value for the interval in seconds."
                });

            return map;
        }

        public static string GetDebugScriptSource()
        {
            return "f = globals[\"Unity\"][\"Time\"].deltaTime \r\n" +
                "globals[\"Host\"].LogInfo \"DeltaTime: \" + f \r\n" +

                "f = globals[\"Unity\"][\"Time\"].fixedDeltaTime \r\n" +
                "globals[\"Host\"].LogInfo \"fixedDeltaTime: \" + f \r\n" +

                "f = globals[\"Unity\"][\"Time\"].fixedTime \r\n" +
                "globals[\"Host\"].LogInfo \"fixedTime: \" + f \r\n" +

                "f = globals[\"Unity\"][\"Time\"].fixedUnscaledDeltaTime \r\n" +
                "globals[\"Host\"].LogInfo \"fixedUnscaledDeltaTime: \" + f \r\n" +

                "f = globals[\"Unity\"][\"Time\"].fixedUnscaledTime \r\n" +
                "globals[\"Host\"].LogInfo \"fixedUnscaledTime: \" + f \r\n" +

                "f = globals[\"Unity\"][\"Time\"].frameCount \r\n" +
                "globals[\"Host\"].LogInfo \"frameCount: \" + f \r\n" +

                "f = globals[\"Unity\"][\"Time\"].maximumDeltaTime \r\n" +
                "globals[\"Host\"].LogInfo \"maximumDeltaTime: \" + f \r\n" +

                "f = globals[\"Unity\"][\"Time\"].maximumParticleDeltaTime \r\n" +
                "globals[\"Host\"].LogInfo \"maximumParticleDeltaTime: \" + f \r\n" +

                "f = globals[\"Unity\"][\"Time\"].realTimeSinceStartup \r\n" +
                "globals[\"Host\"].LogInfo \"realTimeSinceStartup: \" + f \r\n" +

                "f = globals[\"Unity\"][\"Time\"].smoothDeltaTime \r\n" +
                "globals[\"Host\"].LogInfo \"smoothDeltaTime: \" + f \r\n" +

                "f = globals[\"Unity\"][\"Time\"].time \r\n" +
                "globals[\"Host\"].LogInfo \"time: \" + f \r\n" +

                "f = globals[\"Unity\"][\"Time\"].timeScale \r\n" +
                "globals[\"Host\"].LogInfo \"timeScale: \" + f \r\n" +

                "f = globals[\"Unity\"][\"Time\"].timeSinceLevelLoad \r\n" +
                "globals[\"Host\"].LogInfo \"timeSinceLevelLoad: \" + f \r\n" +

                "f = globals[\"Unity\"][\"Time\"].unscaledDeltaTime \r\n" +
                "globals[\"Host\"].LogInfo \"unscaledDeltaTime: \" + f \r\n" +
                "";
        }

        public static void HandleWorkItem(ref AlternateThreadWorkItem item)
        {
            if (item.Module != UnityModuleName.Time)
            {
                throw new System.ArgumentException("A work item was given to UnityEngine.Time that was not part of the Time callable namespace.");
            }

            switch ((int)item.FunctionName)
            {
                case UnityEngineTimeFunctions.deltaTime:
                    var f = UnityEngine.Time.deltaTime;
                    item.result = f;
                    item.eventSlim.Set();
                    break;
                case UnityEngineTimeFunctions.fixedDeltaTime:
                    var f1 = UnityEngine.Time.fixedDeltaTime;
                    item.result = f1;
                    item.eventSlim.Set();
                    break;
                case UnityEngineTimeFunctions.fixedTime:
                    var f2 = UnityEngine.Time.fixedTimeAsDouble;
                    item.result = f2;
                    item.eventSlim.Set();
                    break;
                case UnityEngineTimeFunctions.fixedUnscaledDeltaTime:
                    var f3 = UnityEngine.Time.fixedUnscaledDeltaTime;
                    item.result = f3;
                    item.eventSlim.Set();
                    break;
                case UnityEngineTimeFunctions.fixedUnscaledTime:
                    var f4 = UnityEngine.Time.fixedUnscaledTimeAsDouble;
                    item.result = f4;
                    item.eventSlim.Set();
                    break;
                case UnityEngineTimeFunctions.frameCount:
                    var f5 = UnityEngine.Time.frameCount;
                    item.result = f5;
                    item.eventSlim.Set();
                    break;
                case UnityEngineTimeFunctions.maximumDeltaTime:
                    if (item.args.Length == 1)
                    {
                        UnityEngine.Time.maximumDeltaTime = (float)item.args[0];
                        var f6a = UnityEngine.Time.maximumDeltaTime;
                        item.result = f6a;
                        item.eventSlim.Set();
                        break;
                    }
                    else
                    {
                        var f6 = UnityEngine.Time.maximumDeltaTime;
                        item.result = f6;
                        item.eventSlim.Set();
                        break;
                    }
                case UnityEngineTimeFunctions.maximumParticleDeltaTime:
                    var f7 = UnityEngine.Time.maximumParticleDeltaTime;
                    item.result = f7;
                    item.eventSlim.Set();
                    break;
                case UnityEngineTimeFunctions.realtimeSinceStartup:
                    var f8 = UnityEngine.Time.realtimeSinceStartup;
                    item.result = f8;
                    item.eventSlim.Set();
                    break;
                case UnityEngineTimeFunctions.smoothDeltaTime:
                    var f9 = UnityEngine.Time.smoothDeltaTime;
                    item.result = f9;
                    item.eventSlim.Set();
                    break;
                case UnityEngineTimeFunctions.time:
                    var f10 = UnityEngine.Time.timeAsDouble;
                    item.result = f10;
                    item.eventSlim.Set();
                    break;
                case UnityEngineTimeFunctions.timeScale:
                    if(item.args.Length == 1)
                    {
                        UnityEngine.Time.timeScale = (float)item.args[0];
                        float f11 = UnityEngine.Time.timeScale;
                        item.result = (object)f11;
                        item.eventSlim.Set();
                    }
                    else
                    {
                        float f11 = UnityEngine.Time.timeScale;
                        item.result = (object)f11;
                        item.eventSlim.Set();
                    }
                    break;
                case UnityEngineTimeFunctions.timeSinceLevelLoad:
                    var f12 = UnityEngine.Time.timeSinceLevelLoadAsDouble;
                    item.result = (object)f12;
                    item.eventSlim.Set();
                    break;
                case UnityEngineTimeFunctions.unscaledDeltaTime:
                    float f13 = UnityEngine.Time.unscaledDeltaTime;
                    item.result = (object)f13;
                    item.eventSlim.Set();
                    break;
                case UnityEngineTimeFunctions.unscaledTime:
                    var f14 = UnityEngine.Time.unscaledTimeAsDouble;
                    item.result = f14;
                    item.eventSlim.Set();
                    break;
            }
        }
    }
}
