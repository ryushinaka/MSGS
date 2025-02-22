using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.Host
{
    public class CommandLineArguments
    {
        Dictionary<string, string> argpairs;

        public bool hasArguments
        {
            get
            {
                if (argpairs.Count > 0) return true;
                return false;
            }
        }

        public ValMap ToValMap()
        {
            ValMap map = new ValMap();
            foreach(KeyValuePair<string,string>kv in argpairs)
            {
                map.map.Add(new ValString(kv.Key), new ValString(kv.Value));
            }
            return map;
        }

        #region
        public bool BatchMode
        {
            get
            {
                if (argpairs.ContainsKey("batchmode")) return true;
                return false;
            }
        }
        public bool DisableGPUSkinning
        {
            get
            {
                if (argpairs.ContainsKey("disablegpuskinning")) return true;
                return false;
            }
        }
        public bool ForceClamped
        {
            get
            {
                if (argpairs.ContainsKey("forceclamped")) return true;
                return false;
            }
        }
        public bool ForceD3D11SingleThread
        {
            get
            {
                if (argpairs.ContainsKey("forced3d11singlethread")) return true;
                return false;
            }
        }
        public bool ForceDeviceIndex
        {
            get
            {
                if (argpairs.ContainsKey("forcedeviceindex")) return true;
                return false;
            }
        }
        public bool ForceGLCore
        {
            get
            {
                if (argpairs.ContainsKey("forceglcore")) return true;
                return false;
            }
        }
        public bool ForceGLCoreXY
        {
            get
            {
                if (argpairs.ContainsKey("forceglcorexy")) return true;
                return false;
            }
        }
        public bool ForceVulkan
        {
            get
            {
                if (argpairs.ContainsKey("forcevulkan")) return true;
                return false;
            }
        }
        public bool ForceWayland
        {
            get
            {
                if (argpairs.ContainsKey("forcewayland")) return true;
                return false;
            }
        }
        public bool MonitorN
        {
            get
            {
                if (argpairs.ContainsKey("monitor")) return true;
                return false;
            }
        }
        public bool NoGraphics
        {
            get
            {
                if (argpairs.ContainsKey("nographics")) return true;
                return false;
            }
        }
        public bool NoStereoRendering
        {
            get
            {
                if (argpairs.ContainsKey("nostereorendering")) return true;
                return false;
            }
        }
        public bool PopupWindow
        {
            get
            {
                if (argpairs.ContainsKey("popupwindow")) return true;
                return false;
            }
        }
        public bool ScreenFullScreen
        {
            get
            {
                if (argpairs.ContainsKey("screenfullscreen")) return true;
                return false;
            }
        }
        public bool ScreenHeight
        {
            get
            {
                if (argpairs.ContainsKey("screenheight")) return true;
                return false;
            }
        }
        public bool ScreenWidth
        {
            get
            {
                if (argpairs.ContainsKey("screenwidth")) return true;
                return false;
            }
        }
        public bool ScreenQuality
        {
            get
            {
                if (argpairs.ContainsKey("screenquality")) return true;
                return false;
            }
        }
        public bool ForceLowPowerDevice
        {
            get
            {
                if (argpairs.ContainsKey("forcelowpowerdevice")) return true;
                return false;
            }
        }
        public bool ForceMetal
        {
            get
            {
                if (argpairs.ContainsKey("forcemetal")) return true;
                return false;
            }
        }
        public bool ForceD3D11
        {
            get
            {
                if (argpairs.ContainsKey("forced3d11")) return true;
                return false;
            }
        }
        public bool ForceD3D12
        {
            get
            {
                if (argpairs.ContainsKey("forced3d12")) return true;
                return false;
            }
        }
        public bool ForceD3D11FlipModel
        {
            get
            {
                if (argpairs.ContainsKey("forced3d11flipmodel")) return true;
                return false;
            }
        }
        public bool ForceD3D11BitBltModel
        {
            get
            {
                if (argpairs.ContainsKey("forced3d11bitbltmodel")) return true;
                return false;
            }
        }
        public bool ParentHWND
        {
            get
            {
                if (argpairs.ContainsKey("parenthwnd")) return true;
                return false;
            }
        }
        public bool SingleInstance
        {
            get
            {
                if (argpairs.ContainsKey("singleinstance")) return true;
                return false;
            }
        }
        public bool WindowMode
        {
            get
            {
                if (argpairs.ContainsKey("windowmode")) return true;
                return false;
            }
        }
        #endregion

        public string LogFile
        {
            get
            {
                if (argpairs.ContainsKey("logfile")) return argpairs["logfile"];

                return string.Empty;
            }
        }

        public string Package
        {
            get
            {
                if (argpairs.ContainsKey("package")) return argpairs["package"];

                return string.Empty;
            }
        }

        // Parse the command-line the first time this class is accessed.
        void CommandLine()
        {
            string[] args = System.Environment.GetCommandLineArgs();
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i].ToLower())
                    {    
                        case "-batchmode": argpairs.Add("batchmode", string.Empty); break;
                        case "-disable-gpu-skinning": argpairs.Add("disablegpuskinning", string.Empty); break;
                        case "-force-clamped": argpairs.Add("forceclamped", string.Empty); break;
                        case "-force-d3d11-singlethreaded": argpairs.Add("forced3d11singlethreaded", string.Empty); break;
                        case "-force-device-index": argpairs.Add("forcedeviceindex", string.Empty); break;
                        case "-force-glcore": argpairs.Add("forceglcore", string.Empty); break;
                        case "-force-glcoreXY": argpairs.Add("forceglcorexy", string.Empty); break;
                        case "-force-vulkan": argpairs.Add("forcevulkan", string.Empty); break;
                        case "-force-wayland": argpairs.Add("forcewayland", string.Empty); break;
                        case "-monitor N": argpairs.Add("monitor", args[i + 1]); break;
                        case "-nographics": argpairs.Add("nographics", string.Empty); break;
                        case "-nolog": argpairs.Add("nolog", string.Empty); break;
                        case "-no-stereo-rendering": argpairs.Add("nostereorendering", string.Empty); break;
                        case "-popupwindow": argpairs.Add("popupwindow", string.Empty); break;
                        case "-screen-fullscreen": argpairs.Add("screenfullscreen", string.Empty); break;
                        case "-screen-height": argpairs.Add("screenheight", args[i + 1]); break;
                        case "-screen-width": argpairs.Add("screenwidth", args[i + 1]); break;
                        case "-force-low-power-device": argpairs.Add("forcelowpowerdevice", string.Empty); break;
                        case "-force-metal": argpairs.Add("forcemetal", string.Empty); break;
                        case "-force-d3d11": argpairs.Add("forced3d11", string.Empty); break;
                        case "-force-d3d12": argpairs.Add("forced3d12", string.Empty); break;
                        case "-force-d3d11-flip-model": argpairs.Add("forced3d11flipmodel", string.Empty); break;
                        case "-force-d3d11-bitblt-model": argpairs.Add("forced3d11bitbltmodel", string.Empty); break;
                        case "--parentHWND<HWND> delayed": argpairs.Add("parenthwnd", args[i + 1]); break;
                        case "-single-instance": argpairs.Add("singleinstance", string.Empty); break;
                        case "-window-mode": argpairs.Add("windowmode", string.Empty); break;
                        case "-quit": argpairs.Add("quit", string.Empty); break;
                        case "-usehub": argpairs.Add("usehub", string.Empty); break;
                        case "-hubipc": argpairs.Add("hubipc", string.Empty); break;
                        case "-skipupgradedialogs": argpairs.Add("skipupgradedialogs", string.Empty); break;

                        case "-logfile":
                            if (args.Length >= i + 1) { argpairs.Add("logfile", args[i + 1]); }
                            else { argpairs.Add("logfile", string.Empty); }
                            break;
                        case "-executemethod":
                            argpairs.Add("executemethod", args[i + 1]);
                            break;
                        case "-hubsessionid":
                            argpairs.Add("hubsessionid", args[i + 1]);
                            break;

                        case "-cloudenvironment":
                            argpairs.Add("skipupgradedialogs", args[i + 1]);
                            break;

                        //--------------------MiniScript implementation arguments here
                        #region 
                        case "-package":
                            argpairs.Add("package", args[i + 1]);
                            break;
                        #endregion

                        default:
                            //unhandled value
                            break;
                    }
                }
            }
        }
    }
}
