using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.Unity
{
    public class UnityEngineApplicationWrapper
    {
        /// <summary>
        /// True for debug output to be generated for intrinsic calls
        /// </summary>
        public static bool debug = false;

        public static ValMap Get()
        {
            ValMap map = new ValMap();

            var a = Intrinsic.Create("");
            a.AddParam("value", ValNumber.Truth(false));
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Application.runInBackground"); }

                bool val = context.GetLocalBool("value");

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Application;
                wi.FunctionName = UnityEngineApplicationFunctions.runInBackground;

                if (context.GetLocal("value") != null) { wi.args = new object[1] { (object)val }; }
                else { wi.args = new object[0]; }
                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                val = (bool)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(ValNumber.Truth(val));
            };
            map.map.Add(new ValString("runInBackground"), a.GetFunc());

            a = Intrinsic.Create("");
            a.AddParam("value", (int)UnityEngine.Application.backgroundLoadingPriority);
            a.code = (context, partialResult) =>
            {
                string result = string.Empty;

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Application;
                wi.FunctionName = UnityEngineApplicationFunctions.BackgroundLoadingPriority;

                if (context.GetLocalInt("value") == 0 || context.GetLocalInt("value") == 1 ||
                context.GetLocalInt("value") == 2 || context.GetLocalInt("value") == 4)
                { wi.args = new object[1] { (object)context.GetLocalInt("value") }; }
                else if (context.GetLocalString("value") == "Low" || context.GetLocalString("value") == "low")
                { wi.args = new object[1] { (object)0 }; }
                else if (context.GetLocalString("value") == "BelowNormal" || context.GetLocalString("value") == "belownormal")
                { wi.args = new object[1] { (object)1 }; }
                else if (context.GetLocalString("value") == "Normal" || context.GetLocalString("value") == "normal")
                { wi.args = new object[1] { (object)2 }; }
                else if (context.GetLocalString("value") == "High" || context.GetLocalString("value") == "high")
                { wi.args = new object[1] { (object)4 }; }
                else { return new Intrinsic.Result(string.Empty); }

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                result = (string)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result);

                //return new Intrinsic.Result((int)UnityEngine.Application.backgroundLoadingPriority);
            };
            map.map.Add(new ValString("BackgroundLoadingPriority"), a.GetFunc());

            a = Intrinsic.Create("");
            a.AddParam("filename", string.Empty);
            a.AddParam("superSize", 0);
            a.code = (context, partialResult) =>
            {
                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Application;
                wi.FunctionName = UnityEngineApplicationFunctions.CaptureScreenshot;

                if (context.GetLocalString("filename") != string.Empty) {
                    if (context.GetLocalInt("superSize") != 0) {
                        wi.args = new object[2] { (object)context.GetLocalString("filename"), (object)context.GetLocalInt("superSize") };
                    }
                    else { wi.args = new object[1] { (object)context.GetLocalString("filename") }; }
                }
                else { wi.args = new object[0]; }
                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(null);
            };
            map.map.Add(new ValString("CaptureScreenshot"), a.GetFunc());

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Application;
                wi.FunctionName = UnityEngineApplicationFunctions.Quit;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                AlternateThreadDispatcher.Return(ref wi);
                
                return new Intrinsic.Result(null);
            };
            map.map.Add(new ValString("Quit"), a.GetFunc());

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Application;
                wi.FunctionName = UnityEngineApplicationFunctions.SystemLanguage;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result((string)wi.result);
            };
            map.map.Add(new ValString("SystemLanguage"), a.GetFunc());

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Application;
                wi.FunctionName = UnityEngineApplicationFunctions.RuntimePlatform;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result((string)wi.result);
            };
            map.map.Add(new ValString("RuntimePlatform"), a.GetFunc());

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Application;
                wi.FunctionName = UnityEngineApplicationFunctions.AppVersion;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result((string)wi.result);
            };
            map.map.Add(new ValString("AppVersion"), a.GetFunc());

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Application;
                wi.FunctionName = UnityEngineApplicationFunctions.UnityVersion;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result((string)wi.result);
            };
            map.map.Add(new ValString("UnityVersion"), a.GetFunc());

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Application;
                wi.FunctionName = UnityEngineApplicationFunctions.temporaryCachePath;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result((string)wi.result);
            };
            map.map.Add(new ValString("temporaryCachePath"), a.GetFunc());

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Application;
                wi.FunctionName = UnityEngineApplicationFunctions.streamingAssetsPath;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result((string)wi.result);
            };
            map.map.Add(new ValString("streamingAssetsPath"), a.GetFunc());

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Application;
                wi.FunctionName = UnityEngineApplicationFunctions.persistentDataPath;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result((string)wi.result);
            };
            map.map.Add(new ValString("persistentDataPath"), a.GetFunc());

            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Application;
                wi.FunctionName = UnityEngineApplicationFunctions.dataPath;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result((string)wi.result);
            };
            map.map.Add(new ValString("dataPath"), a.GetFunc());

            return map;
        }

        public static string GetDebugScriptSource()
        {
            return "";
        }

        public static void HandleWorkItem(ref AlternateThreadWorkItem item)
        {
            if (item.Module != UnityModuleName.Application)
            {
                throw new System.ArgumentNullException("A work item was given to UnityEngine.Application that was not part of the Application callable namespace.");
            }

            switch ((int)item.FunctionName)
            {
                case UnityEngineApplicationFunctions.AppVersion:
                    item.result = UnityEngine.Application.version;
                    break;
                case UnityEngineApplicationFunctions.BackgroundLoadingPriority:
                    if (item.args.Length == 1)
                    {
                        switch ((int)item.args[0])
                        {
                            case 0:
                                UnityEngine.Application.backgroundLoadingPriority = ThreadPriority.Low;
                                break;
                            case 1:
                                UnityEngine.Application.backgroundLoadingPriority = ThreadPriority.BelowNormal;
                                break;
                            case 2:
                                UnityEngine.Application.backgroundLoadingPriority = ThreadPriority.Normal;
                                break;
                            case 4:
                                UnityEngine.Application.backgroundLoadingPriority = ThreadPriority.High;
                                break;
                        }
                    }
                    else
                    {
                        item.result = ((int)UnityEngine.Application.backgroundLoadingPriority).ToString();
                    }
                    break;
                case UnityEngineApplicationFunctions.CaptureScreenshot:
                    if (item.args.Length == 2)
                    {
                        ScreenCapture.CaptureScreenshot((string)item.args[0] + ".png", (int)item.args[1]);
                    }
                    else if (item.args.Length == 1)
                    {
                        ScreenCapture.CaptureScreenshot((string)item.args[0] + ".png");
                    }
                    else if (item.args.Length == 0)
                    {
                        ScreenCapture.CaptureScreenshot((string)System.Guid.NewGuid().ToString() + ".png");
                    }
                    break;
                case UnityEngineApplicationFunctions.dataPath:
                    item.result = UnityEngine.Application.dataPath;
                    break;
                case UnityEngineApplicationFunctions.persistentDataPath:
                    item.result = UnityEngine.Application.persistentDataPath;
                    break;
                case UnityEngineApplicationFunctions.Quit:
                    UnityEngine.Application.Quit();
                    break;
                case UnityEngineApplicationFunctions.runInBackground:
                    if (item.args.Length == 1) { UnityEngine.Application.runInBackground = (bool)item.args[0]; }
                    item.result = UnityEngine.Application.runInBackground;
                    break;
                case UnityEngineApplicationFunctions.RuntimePlatform:
                    item.result = UnityEngine.Application.platform.ToString();
                    break;
                case UnityEngineApplicationFunctions.streamingAssetsPath:
                    item.result = UnityEngine.Application.streamingAssetsPath;
                    break;
                case UnityEngineApplicationFunctions.SystemLanguage:
                    item.result = UnityEngine.Application.systemLanguage.ToString();
                    break;
                case UnityEngineApplicationFunctions.temporaryCachePath:
                    item.result = UnityEngine.Application.temporaryCachePath;
                    break;
                case UnityEngineApplicationFunctions.UnityVersion:
                    item.result = UnityEngine.Application.unityVersion;
                    break;
                default:
                    throw new System.ArgumentNullException("A work item was given to UnityEngine.Application but the function was invalid.");
            }
            item.eventSlim.Set();
        }
    }
}
