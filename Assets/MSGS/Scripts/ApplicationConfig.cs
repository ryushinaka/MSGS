using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharpConfig;
using System.IO;

namespace MiniScript.MSGS
{
    public class ApplicationConfig
    {
        Configuration settings;
        static string filename = "applicationconfig.txt";

        string path;
        public string Path
        {
            get { return path; }
            set { path = value; WriteConfig(); }
        }
        int resx = 640;
        public int ResolutionX
        {
            get { return resx; }
            set { resx = value; WriteConfig(); }
        }
        int resy = 480;
        public int ResolutionY
        {
            get { return resy; }
            set { resy = value; WriteConfig(); }
        }
        int rr = 60;
        public int RefreshRate
        {
            get { return rr; }
            set { rr = value; WriteConfig(); }
        }
        string fs = FullScreenMode.Windowed.ToString();
        public FullScreenMode FullScreen
        {
            get { return (FullScreenMode)System.Enum.Parse(typeof(FullScreenMode), fs); }
            set { fs = value.ToString(); WriteConfig(); }
        }

        public ApplicationConfig()
        {
            settings = null;
            if (File.Exists(System.IO.Path.Combine(new string[] {
                Application.persistentDataPath,
                filename
                })))
            {
                settings = SharpConfig.Configuration.LoadFromFile(
                    System.IO.Path.Combine(new string[] {
                Application.persistentDataPath,
                filename
                }));
            }
            else { DefaultConfig(); }

            if (settings.Contains("DataFolder", "Path"))
            {
                Path = settings["DataFolder"]["Path"].StringValue;
            }
            else
            {
                Path = System.IO.Path.Combine(new string[] {
                Application.persistentDataPath,
                filename
                });
            }

            if (settings.Contains("Graphics"))
            {
                if (settings["Graphics"].Contains("ResolutionX"))
                {
                    ResolutionX = settings["Graphics"]["ResolutionX"].IntValue;
                }
                else { ResolutionX = 640; }
                if (settings["Graphics"].Contains("ResolutionY"))
                {
                    ResolutionY = settings["Graphics"]["ResolutionY"].IntValue;
                }
                else { ResolutionY = 480; }
                if (settings["Graphics"].Contains("FullScreen"))
                {
                    FullScreen = (FullScreenMode)System.Enum.Parse(typeof(FullScreenMode), settings["Graphics"]["FullScreen"].StringValue);
                }
                else { FullScreen = FullScreenMode.Windowed; }
                if (settings["Graphics"].Contains("RefreshRate"))
                {
                    RefreshRate = settings["Graphics"]["RefreshRate"].IntValue;
                }
                else { RefreshRate = 60; }
            }
            else
            {   //default values
                ResolutionX = 640; ResolutionY = 480;
                RefreshRate = 60;
                FullScreen = FullScreenMode.Windowed;
            }
        }
        
        void WriteConfig()
        {
            var set = new Configuration();
            set.Add("DataFolder");
            set["DataFolder"].Add("Path", path);
            set["Graphics"].Add("ResolutionX", resx);
            set["Graphics"].Add("ResolutionY", resy);
            set["Graphics"].Add("RefreshRate", rr);
            set["Graphics"].Add("FullScreen", fs);

            set.SaveToFile(System.IO.Path.Combine(new string[] {
            Application.persistentDataPath, filename }));
        }
        
        void DefaultConfig()
        {
            settings = new Configuration();
            settings.Add("DataFolder");
            settings["DataFolder"].Add("Path", Application.persistentDataPath);

            settings.Add("Graphics");
            settings["Graphics"].Add("ResolutionX", "640");
            settings["Graphics"].Add("ResolutionY", "480");
            settings["Graphics"].Add("FullScreen", FullScreenMode.Windowed.ToString());
            settings["Graphics"].Add("RefreshRate", "60");

            //WriteConfig();
        }
    }
}

