using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.Unity
{
    public class UnityEngineScreenWrapper
    {
        public static bool debug = false;
        public static ValMap Get()
        {
            ValMap map = new ValMap();

            #region autorotateToLandscapeLeft
            var a = Intrinsic.Create("");
            a.AddParam("autorotate", ValNumber.Truth(false));
            a.code = (context, partialResult) =>
            {
                if(debug) { Debug.Log("UnityEngine.Screen.autorotateToLandscapeLeft " + context.GetLocalBool("autorotate"));
                    return new Intrinsic.Result(null, true); }

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Screen;
                wi.FunctionName = UnityEngineScreenFunctions.ScreenAutorotateToLandscapeLeft;
                wi.args = new object[1] { context.GetLocalBool("autorotate", false) };

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                bool result = (bool)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(ValNumber.Truth(result), true);
            };
            map.map.Add(new ValString("autorotateToLandscapeLeft"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
              "autorotateToLandscapeLeft",
              "Gets/Sets autorotation to Landscape Left",
              "UnityEngine.Screen",
              new List<IntrinsicParameter>() {
                    new IntrinsicParameter {
                        Name = "autorotation",
                        variableType = typeof(bool),
                        Comment = "The bool value to set for the state of autorotateToLandscapeLeft." } },
              new IntrinsicParameter
              {
                  Name = "return",
                  variableType = typeof(bool),
                  Comment = "The bool value for the state of autorotateToLandscapeLeft."
              }
            );
            #endregion

            #region autorotateToLandscapeRight
            a = Intrinsic.Create("");
            a.AddParam("autorotate", ValNumber.Truth(false));
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Screen.autorotateToLandscapeRight " + context.GetLocalBool("autorotate"));
                    return new Intrinsic.Result(null, true); }

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Screen;
                wi.FunctionName = UnityEngineScreenFunctions.ScreenAutorotateToLandscapeRight;
                wi.args = new object[1] { context.GetLocalBool("autorotate", false) };

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                bool result = (bool)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(ValNumber.Truth(result), true);
            };
            map.map.Add(new ValString("autorotateToLandscapeRight"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
              "autorotateToLandscapeRight",
              "Gets/Sets autorotation to Landscape Right",
              "UnityEngine.Screen",
              new List<IntrinsicParameter>() {
                    new IntrinsicParameter {
                        Name = "autorotation",
                        variableType = typeof(bool),
                        Comment = "The bool value to set for the state of autorotateToLandscapeRight." } },
              new IntrinsicParameter
              {
                  Name = "return",
                  variableType = typeof(bool),
                  Comment = "The bool value for the state of autorotateToLandscapeRight."
              }
            );
            #endregion

            #region AutorotateToPortrait
            a = Intrinsic.Create("");
            a.AddParam("autorotate", ValNumber.Truth(false));
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Screen.autorotateToPortrait " + context.GetLocalBool("autorotate")); 
                    return new Intrinsic.Result(null, true); }

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Screen;
                wi.FunctionName = UnityEngineScreenFunctions.ScreenAutorotateToPortrait;
                wi.args = new object[1] { context.GetLocalBool("autorotate", false) };

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                bool result = (bool)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(ValNumber.Truth(result), true);
            };
            map.map.Add(new ValString("autorotateToPortrait"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
              "autorotateToPortrait",
              "Gets/Sets autorotation for autorotate to Portrait",
              "UnityEngine.Screen",
              new List<IntrinsicParameter>() {
                    new IntrinsicParameter {
                        Name = "autorotation",
                        variableType = typeof(bool),
                        Comment = "The bool value to set for the state of autorotateToPortrait." } },
              new IntrinsicParameter
              {
                  Name = "return",
                  variableType = typeof(bool),
                  Comment = "The bool value for the state of autorotateToPortrait."
              }
            );
            #endregion

            #region AutorotateToPortraitUpsideDown
            a = Intrinsic.Create("");
            a.AddParam("autorotate", ValNumber.Truth(false));
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Screen.autorotateToPortraitUpsideDown " + context.GetLocalBool("autorotate")); 
                    return new Intrinsic.Result(null, true); }

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Screen;
                wi.FunctionName = UnityEngineScreenFunctions.ScreenAutorotateToPortraitUpsideDown;
                wi.args = new object[1] { context.GetLocalBool("autorotate", false) };

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                bool result = (bool)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(ValNumber.Truth(result), true);
            };
            map.map.Add(new ValString("autorotateToPortraitUpsideDown"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
              "autorotateToPortraitUpsideDown",
              "Gets/Sets autorotation for autorotate to Portrait Upside/Down",
              "UnityEngine.Screen",
              new List<IntrinsicParameter>() {
                    new IntrinsicParameter {
                        Name = "autorotation",
                        variableType = typeof(bool),
                        Comment = "The bool value to set for the state of Portrait Upside/Down." } },
              new IntrinsicParameter
              {
                  Name = "return",
                  variableType = typeof(bool),
                  Comment = "The bool value for the state of Portrait Upside/Down."
              }
            );
            #endregion

            #region Brightness
            a = Intrinsic.Create("");
            a.AddParam("brightness", UnityCachedValues.brightness);
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Screen.brightness " + context.GetLocalFloat("brightness")); 
                    return new Intrinsic.Result(null, true); }

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Screen;
                wi.FunctionName = UnityEngineScreenFunctions.ScreenBrightness;
                wi.args = new object[1] { context.GetLocalBool("brightness", false) };

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                float result = (float)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(new ValNumber(result), true);
            };
            map.map.Add(new ValString("brightness"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
              "brightness",
              "Gets/Sets the brightness value for the Screen",
              "UnityEngine.Screen",
              new List<IntrinsicParameter>() {
                    new IntrinsicParameter {
                        Name = "autorotation",
                        variableType = typeof(float),
                        Comment = "The float value to set for the brightness of the screen." } },
              new IntrinsicParameter
              {
                  Name = "return",
                  variableType = typeof(float),
                  Comment = "The float value for the brightness of the screen."
              }
            );
            #endregion

            #region Resolution
            a = Intrinsic.Create("");
            a.AddParam("resolution", new ValMap());
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Screen.Resolution " + ((ValMap)context.GetLocal("resolution")).ToString());
                    return new Intrinsic.Result(null, true); }
                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Screen;
                wi.FunctionName = UnityEngineScreenFunctions.ScreenCurrentResolution;
                wi.args = new object[1] { context.GetLocal("resolution", new ValMap()) };

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                ValMap result = (ValMap)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result, true);
            };
            map.map.Add(new ValString("currentResolution"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
              "currentResolution",
              "Gets/Sets the Resolution and RefreshRate for the current Screen.",
              "UnityEngine.Screen",
              new List<IntrinsicParameter>() {
                    new IntrinsicParameter {
                        Name = "autorotation",
                        variableType = typeof(ValMap),
                        Comment = "The values [height, width, numerator, denominator] of Resolution and RefreshRate." } },
              new IntrinsicParameter
              {
                  Name = "return",
                  variableType = typeof(ValMap),
                  Comment = "The values [height, width, numerator, denominator] of Resolution and RefreshRate."
              }
            );
            #endregion

            #region Cutouts
            //a = Intrinsic.Create("");
            //a.code = (context, partialResult) =>
            //{
            //if (debug)
            //{
            //    Debug.Log("UnityEngine.Screen.brightness " + ((ValMap)context.GetLocal("resolution")).ToString());
            //    return new Intrinsic.Result(null, true);
            //}
            //    var wi = AlternateThreadDispatcher.Get();
            //    wi.Module = UnityModuleName.Screen;
            //    wi.FunctionName = UnityEngineScreenFunctions.ScreenCutouts;
            //    wi.args = null;

            //    AlternateThreadDispatcher.Enqueue(ref wi);
            //    wi.eventSlim.Wait();
            //    ValList result = (ValList)wi.result;
            //    AlternateThreadDispatcher.Return(ref wi);

            //    return new Intrinsic.Result(result, true);
            //};
            //map.map.Add(new ValString("cutouts"), a.GetFunc());

            //IntrinsicsHelpMetadata.Create(
            //  "cutouts",
            //  "Gets/Sets the Resolution and RefreshRate for the current Screen.",
            //  "UnityEngine.Screen",
            //  new List<IntrinsicParameter>() {
            //        new IntrinsicParameter {
            //            Name = "autorotation",
            //            variableType = typeof(ValMap),
            //            Comment = "The values [height, width, numerator, denominator] of Resolution and RefreshRate." } },
            //  new IntrinsicParameter
            //  {
            //      Name = "return",
            //      variableType = typeof(ValMap),
            //      Comment = "The values [height, width, numerator, denominator] of Resolution and RefreshRate."
            //  }
            //);
            #endregion

            #region dpi
            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Screen.dpi "); return new Intrinsic.Result(null, true); }

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Screen;
                wi.FunctionName = UnityEngineScreenFunctions.Screendpi;
                wi.args = null;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                float result = (float)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(new ValNumber(result), true);
            };
            map.map.Add(new ValString("dpi"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
              "dpi",
              "Gets the dpi of the current Screen",
              "UnityEngine.Screen",
              new List<IntrinsicParameter>(),
              new IntrinsicParameter
              {
                  Name = "return",
                  variableType = typeof(float),
                  Comment = "The dpi value of the current Screen."
              }
            );
            #endregion

            #region FullScreen
            a = Intrinsic.Create("");
            a.AddParam("fullscreen", ValNumber.Truth(UnityCachedValues.FullScreen));
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Screen.fullscreen " + context.GetLocalBool("fullscreen")); return new Intrinsic.Result(null, true); }

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Screen;
                wi.FunctionName = UnityEngineScreenFunctions.ScreenFullScreen;
                wi.args = new object[1] { context.GetLocalBool("fullscreen", UnityCachedValues.FullScreen) };

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                bool result = (bool)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(ValNumber.Truth(result), true);
            };
            map.map.Add(new ValString("fullscreen"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
              "fullscreen",
              "Gets/Sets the fullscreen toggle of the current Screen",
              "UnityEngine.Screen",
              new List<IntrinsicParameter>(),
              new IntrinsicParameter
              {
                  Name = "return",
                  variableType = typeof(bool),
                  Comment = "The bool value of the fullscreen toggle."
              }
            );
            #endregion

            #region FullScreenMode
            a = Intrinsic.Create("");
            a.AddParam("mode", UnityCachedValues.FullScreenMode.ToValNumber());
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Screen.fullscreenmode " + context.GetLocal("mode").ToString()); return new Intrinsic.Result(null, true); }

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Screen;
                wi.FunctionName = UnityEngineScreenFunctions.ScreenFullScreenMode;
                wi.args = new object[1] { context.GetLocal("mode", UnityCachedValues.FullScreenMode.ToValNumber()) };

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                ValNumber result = (ValNumber)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result, true);
            };
            map.map.Add(new ValString("FullScreenMode"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
              "fullscreen",
              "Gets/Sets the fullscreen toggle of the current Screen",
              "UnityEngine.Screen",
              new List<IntrinsicParameter>() {
                  new IntrinsicParameter()
                  {
                      Name = "mode",
                      variableType = typeof(ValNumber),
                      Comment = "The ValNumber of the FullScreenMode setting: ExclusiveFullScreen=0, FullScreenWindow=1, MaximizedWindow=2, Windowed=3"
                  }
              },
              new IntrinsicParameter
              {
                  Name = "return",
                  variableType = typeof(ValNumber),
                  Comment = "The ValNumber value of the FullScreenMode setting for the current Screen."
              }
            );
            #endregion

            #region Height
            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Screen.height "); return new Intrinsic.Result(null, true); }

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Screen;
                wi.FunctionName = UnityEngineScreenFunctions.ScreenHeight;
                wi.args = null;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                ValNumber result = (ValNumber)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result, true);
            };
            map.map.Add(new ValString("height"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
              "height",
              "Gets the current height of the screen window in pixels",
              "UnityEngine.Screen",
              new List<IntrinsicParameter>(),
              new IntrinsicParameter
              {
                  Name = "return",
                  variableType = typeof(ValNumber),
                  Comment = "The current height of the screen window in pixels."
              }
            );
            #endregion

            #region Width
            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Screen.width "); return new Intrinsic.Result(null, true); }


                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Screen;
                wi.FunctionName = UnityEngineScreenFunctions.ScreenWidth;
                wi.args = null;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                ValNumber result = (ValNumber)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result, true);
            };
            map.map.Add(new ValString("width"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
              "width",
              "Gets the current width of the screen window in pixels.",
              "UnityEngine.Screen",
              new List<IntrinsicParameter>(),
              new IntrinsicParameter
              {
                  Name = "return",
                  variableType = typeof(ValNumber),
                  Comment = "The current width of the screen window in pixels."
              }
            );
            #endregion

            #region MainWindowDisplayInfo
            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Screen.windowdisplayinfo "); return new Intrinsic.Result(null, true); }

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Screen;
                wi.FunctionName = UnityEngineScreenFunctions.ScreenMainWindowDisplayInfo;
                wi.args = null;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                ValMap result = (ValMap)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result, true);
            };
            map.map.Add(new ValString("MainWindowDisplayInfo"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
              "MainWindowDisplayInfo",
              "The display information associated with the display that the main application window is on.",
              "UnityEngine.Screen",
              new List<IntrinsicParameter>(),
              new IntrinsicParameter
              {
                  Name = "return",
                  variableType = typeof(ValMap),
                  Comment = "The display information associated with the display that the main application window is on."
              }
            );
            #endregion

            #region MainWindowPosition
            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Screen.MainWindowPosition "); return new Intrinsic.Result(null, true); }

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Screen;
                wi.FunctionName = UnityEngineScreenFunctions.ScreenMainWindowPosition;
                wi.args = null;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                ValMap result = (ValMap)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result, true);
            };
            map.map.Add(new ValString("MainWindowPosition"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
              "MainWindowPosition",
              "The position of the top left corner of the main window relative to the top left corner of the display.",
              "UnityEngine.Screen",
              new List<IntrinsicParameter>(),
              new IntrinsicParameter
              {
                  Name = "return",
                  variableType = typeof(ValMap),
                  Comment = "The position of the top left corner of the main window relative to the top left corner of the display."
              }
            );
            #endregion

            #region Orientation
            a = Intrinsic.Create("");
            a.AddParam("oreo", UnityCachedValues.orientation.ToValNumber());
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Screen.orientation " + context.GetLocal("oreo").ToString()); return new Intrinsic.Result(null, true); }

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Screen;
                wi.FunctionName = UnityEngineScreenFunctions.ScreenOrientation;
                wi.args = new object[1] { (ValNumber)context.GetLocal("oreo") };

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                ValNumber result = (ValNumber)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result, true);
            };
            map.map.Add(new ValString("orientation"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
              "orientation",
              "Gets/Sets the logical orientation of the screen.",
              "UnityEngine.Screen",
              new List<IntrinsicParameter>(),
              new IntrinsicParameter
              {
                  Name = "return",
                  variableType = typeof(ValNumber),
                  Comment = "The current logical orientation of the screen."
              }
            );
            #endregion

            #region Resolutions
            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Screen.resolutions "); return new Intrinsic.Result(null, true); }

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Screen;
                wi.FunctionName = UnityEngineScreenFunctions.ScreenResolutions;
                wi.args = null;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                ValList result = (ValList)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result, true);
            };
            map.map.Add(new ValString("resolutions"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
              "resolutions",
              "Gets the available resolutions for the display",
              "UnityEngine.Screen",
              new List<IntrinsicParameter>(),
              new IntrinsicParameter
              {
                  Name = "return",
                  variableType = typeof(ValList),
                  Comment = "The ValList containing ValMaps of each supported Resolution."
              }
            );
            #endregion

            #region safearea
            a = Intrinsic.Create("");
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Screen.safearea "); return new Intrinsic.Result(null, true); }

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Screen;
                wi.FunctionName = UnityEngineScreenFunctions.ScreenSafeArea;
                wi.args = null;

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                ValMap result = (ValMap)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result, true);
            };
            map.map.Add(new ValString("safearea"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
              "safearea",
              "Gets the safe area of the screen in pixels.",
              "UnityEngine.Screen",
              new List<IntrinsicParameter>(),
              new IntrinsicParameter
              {
                  Name = "return",
                  variableType = typeof(ValMap),
                  Comment = "Returns the safe area of the screen in pixels."
              }
            );
            #endregion

            #region SetResolution
            a = Intrinsic.Create("");
            a.AddParam("height", 0);
            a.AddParam("width", 0);
            a.AddParam("fullscreen", ValNumber.Truth(true));
            a.code = (context, partialResult) =>
            {
                if (debug) { Debug.Log("UnityEngine.Screen.SetResolution " + context.GetLocal("height").ToString() + "/" +
                    context.GetLocal("width").ToString() + "/" + context.GetLocal("fullscreen").ToString()); 
                    return new Intrinsic.Result(null, true); }

                var wi = AlternateThreadDispatcher.Get();
                wi.Module = UnityModuleName.Screen;
                wi.FunctionName = UnityEngineScreenFunctions.ScreenSetResolution;
                wi.args = new object[3] {
                    (ValNumber)context.GetLocal("height"),
                    (ValNumber)context.GetLocal("width"),
                    ValNumber.Truth(context.GetLocalBool("fullscreen"))
                };

                AlternateThreadDispatcher.Enqueue(ref wi);
                wi.eventSlim.Wait();
                ValList result = (ValList)wi.result;
                AlternateThreadDispatcher.Return(ref wi);

                return new Intrinsic.Result(result, true);
            };
            map.map.Add(new ValString("SetResolution"), a.GetFunc());

            IntrinsicsHelpMetadata.Create(
              "SetResolution",
              "Sets the resolution for the current display",
              "UnityEngine.Screen",
              new List<IntrinsicParameter>(),
              null
            );
            #endregion

            #region SleepTimeout
            //a = Intrinsic.Create("");
            //a.AddParam("timeout", string.Empty);
            //a.code = (context, partialResult) =>
            //{
            //    if (debug) { Debug.Log("UnityEngine.Screen.SleepTimeout "); return new Intrinsic.Result(null, true); }
            //
            //    var wi = AlternateThreadDispatcher.Get();
            //    wi.Module = UnityModuleName.Screen;
            //    wi.FunctionName = UnityEngineScreenFunctions.ScreenSleepTimeout;
            //    wi.args = new object[1] { context.GetLocalString("timeout", string.Empty) };

            //    AlternateThreadDispatcher.Enqueue(ref wi);
            //    wi.eventSlim.Wait();
            //    ValString result = (ValString)wi.result;
            //    AlternateThreadDispatcher.Return(ref wi);

            //    return new Intrinsic.Result(result, true);
            //};
            //map.map.Add(new ValString("SleepTimeout"), a.GetFunc());

            //IntrinsicsHelpMetadata.Create(
            //  "SleepTimeout",
            //  "	A power saving setting, allowing the screen to dim some time after the last active user interaction.",
            //  "UnityEngine.Screen",
            //  new List<IntrinsicParameter>() { 
            //      new IntrinsicParameter()
            //      {
            //          Name = "timeout",
            //          variableType = typeof(string),
            //          Comment = "NeverSleep or SystemSetting are the valid values."
            //      }
            //  },
            //  null
            //);
            #endregion

            return map;
        }

        public static string GetDebugScriptSource()
        {
            string source = 
                "globals[\"Unity\"][\"Screen\"].autorotateToLandscapeLeft \r\n" +
                "globals[\"Unity\"][\"Screen\"].autorotateToLandscapeRight \r\n" +
                "globals[\"Unity\"][\"Screen\"].autorotateToPortrait \r\n" +
                "globals[\"Unity\"][\"Screen\"].autorotateToPortraitUpsideDown \r\n" +
                //"globals[\"Unity\"][\"Screen\"].brightness \r\n" + //mobile only
                "globals[\"Unity\"][\"Screen\"].currentResolution \r\n" +
                //"globals[\"Unity\"][\"Screen\"].Cutouts \r\n" + //mobile only
                "globals[\"Unity\"][\"Screen\"].dpi \r\n" +
                "globals[\"Unity\"][\"Screen\"].fullscreen(true) \r\n" +
                "globals[\"Unity\"][\"Screen\"].FullScreenMode(0) \r\n" +
                "globals[\"Unity\"][\"Screen\"].height \r\n" +
                "globals[\"Unity\"][\"Screen\"].width \r\n" +
                "globals[\"Unity\"][\"Screen\"].MainWindowDisplayInfo \r\n" +
                "globals[\"Unity\"][\"Screen\"].MainWindowPosition \r\n" +
                "globals[\"Unity\"][\"Screen\"].orientation \r\n" +
                "globals[\"Unity\"][\"Screen\"].resolutions \r\n" +
                "globals[\"Unity\"][\"Screen\"].safearea \r\n" +
                "globals[\"Unity\"][\"Screen\"].SetResolution(640,480,0) \r\n" +
                //"globals[\"Unity\"][\"Screen\"].SleepTimeout \r\n" + //mobile only
                "";

            return source;
        }

        public static void HandleWorkItem(ref AlternateThreadWorkItem item)
        {
            if (item.Module != UnityModuleName.Screen)
            {
                throw new System.ArgumentException("A work item was given to UnityEngine.Screen that was not part of the Screen callable namespace.");
            }

            switch ((int)item.FunctionName)
            {
                case UnityEngineScreenFunctions.ScreenAutorotateToLandscapeLeft:
                    if (item.args.Length == 1)
                    {
                        if (((bool)item.args[0]) != UnityEngine.Screen.autorotateToLandscapeLeft)
                            UnityEngine.Screen.autorotateToLandscapeLeft = (bool)item.args[0];
                    }
                    var f = UnityEngine.Screen.autorotateToLandscapeLeft;
                    item.result = f;
                    item.eventSlim.Set();
                    break;
                case UnityEngineScreenFunctions.ScreenAutorotateToLandscapeRight:
                    if (item.args.Length == 1)
                    {
                        if (((bool)item.args[0]) != UnityEngine.Screen.autorotateToLandscapeRight)
                            UnityEngine.Screen.autorotateToLandscapeRight = (bool)item.args[0];
                    }
                    var f2 = UnityEngine.Screen.autorotateToLandscapeRight;
                    item.result = f2;
                    item.eventSlim.Set();
                    break;
                case UnityEngineScreenFunctions.ScreenAutorotateToPortrait:
                    if (item.args.Length == 1)
                    {
                        if (((bool)item.args[0]) != UnityEngine.Screen.autorotateToPortrait)
                            UnityEngine.Screen.autorotateToPortrait = (bool)item.args[0];
                    }
                    var f3 = UnityEngine.Screen.autorotateToPortrait;
                    item.result = f3;
                    item.eventSlim.Set();
                    break;
                case UnityEngineScreenFunctions.ScreenAutorotateToPortraitUpsideDown:
                    if (item.args.Length == 1)
                    {
                        if (((bool)item.args[0]) != UnityEngine.Screen.autorotateToPortraitUpsideDown)
                            UnityEngine.Screen.autorotateToPortraitUpsideDown = (bool)item.args[0];
                    }
                    var f4 = UnityEngine.Screen.autorotateToPortraitUpsideDown;
                    item.result = f4;
                    item.eventSlim.Set();
                    break;
                case UnityEngineScreenFunctions.ScreenBrightness:
                    if (item.args.Length == 1)
                    {
                        if (((float)item.args[0]) != UnityEngine.Screen.brightness)
                            UnityEngine.Screen.brightness = (float)item.args[0];
                    }
                    var f5 = UnityEngine.Screen.brightness;
                    item.result = f5;
                    item.eventSlim.Set();
                    break;
                case UnityEngineScreenFunctions.ScreenCurrentResolution:
                    var f6 = UnityEngine.Screen.currentResolution.ToValMap();
                    item.result = f6;
                    item.eventSlim.Set();
                    break;
                case UnityEngineScreenFunctions.ScreenCutouts:
                    var rects = UnityEngine.Screen.cutouts;
                    var f7 = new ValList();
                    foreach (Rect r in rects) { f7.values.Add(r.ToValMap()); }
                    item.result = f7;
                    item.eventSlim.Set();
                    break;
                case UnityEngineScreenFunctions.Screendpi:
                    item.result = UnityEngine.Screen.dpi;
                    item.eventSlim.Set();
                    break;
                case UnityEngineScreenFunctions.ScreenFullScreen:
                    if(item.args.Length == 1) {
                        if(((bool)item.args[0]) != UnityEngine.Screen.fullScreen) {
                            UnityEngine.Screen.fullScreen = (bool)item.args[0];
                            UnityCachedValues.FullScreen = UnityEngine.Screen.fullScreen;
                        }
                    }
                    item.result = UnityEngine.Screen.fullScreen;
                    item.eventSlim.Set();
                    break;
                case UnityEngineScreenFunctions.ScreenFullScreenMode:
                    if(item.args.Length == 1)
                    {
                        ValNumber vn = (ValNumber)item.args[0];
                        FullScreenMode fsm = vn.ToFullScreenMode();
                        if (UnityEngine.Screen.fullScreenMode != fsm) UnityEngine.Screen.fullScreenMode = fsm;
                        UnityCachedValues.FullScreenMode = UnityEngine.Screen.fullScreenMode;
                    }
                    item.result = UnityEngine.Screen.fullScreenMode.ToValNumber();
                    item.eventSlim.Set();
                    break;
                case UnityEngineScreenFunctions.ScreenHeight:
                    item.result = UnityEngine.Screen.height;
                    item.eventSlim.Set();
                    break;
                case UnityEngineScreenFunctions.ScreenMainWindowDisplayInfo:
                    item.result = UnityEngine.Screen.mainWindowDisplayInfo.ToValMap();
                    item.eventSlim.Set();
                    break;
                case UnityEngineScreenFunctions.ScreenMainWindowPosition:
                    item.result = UnityEngine.Screen.mainWindowPosition.ToValMap();
                    item.eventSlim.Set();
                    break;
                case UnityEngineScreenFunctions.ScreenOrientation:
                    if(item.args.Length == 1)
                    {
                        ValNumber so = (ValNumber)item.args[0];
                        UnityEngine.Screen.orientation = so.ToOrientation();
                        UnityCachedValues.orientation = UnityEngine.Screen.orientation;
                    }
                    item.eventSlim.Set();
                    break;
                case UnityEngineScreenFunctions.ScreenResolutions:
                    ValList tmpreslst = UnityEngine.Screen.resolutions.ToValList();
                    item.result = tmpreslst;
                    item.eventSlim.Set();
                    break;
                case UnityEngineScreenFunctions.ScreenSafeArea:
                    item.result = UnityEngine.Screen.safeArea.ToValMap();
                    item.eventSlim.Set();
                    break;
                case UnityEngineScreenFunctions.ScreenSetResolution:
                    if(item.args.Length == 1)
                    {
                        ValMap vm = (ValMap)item.args[0];
                        UnityEngine.Screen.SetResolution(
                            vm["height"].IntValue(),
                            vm["width"].IntValue(),
                            vm["fullscreen"].BoolValue()
                            );
                    }
                    item.eventSlim.Set();
                    break;
                case UnityEngineScreenFunctions.ScreenSleepTimeout:
                    //mobile only, intrinsic disabled
                    break;
                case UnityEngineScreenFunctions.ScreenWidth:
                    item.result = UnityEngine.Screen.width;
                    item.eventSlim.Set();
                    break;
            }
        }
    }
}

