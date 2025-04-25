using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using MiniScript;
using MiniScript.MSGS.Audio;
using MiniScript.MSGS.Data;
using MiniScript.MSGS.Time;
using MiniScript.MSGS.Host;
using MiniScript.MSGS.Json;
using MiniScript.MSGS.Schedule;
using MiniScript.MSGS.MUUI;
using MiniScript.MSGS.XML;
using MiniScript.MSGS.Zip;
using MiniScript.MSGS.Network;
using MiniScript.MSGS.ScriptableObjects;
using MiniScript.MSGS.Scripts;

namespace MiniScript.MSGS
{
    public class MiniScriptExecutor : MonoBehaviour
    {   
        public MiniScriptScriptAsset ScriptAsset;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        [Button]
        public void ExecuteScript()
        {
            var intp = new Interpreter();
                        
            if(ScriptAsset != null) { intp = new Interpreter(ScriptAsset.ScriptContent); }
            else { return; }

            intp.errorOutput = ErrOutput;
            intp.standardOutput = StdOutput;
            intp.Compile();
            //add the modules desired to the globals valmap for script access
            intp.SetGlobalValue("Audio", ScriptModuleConfiguration.Audio);
            intp.SetGlobalValue("Data", ScriptModuleConfiguration.Data);
            intp.SetGlobalValue("Database", ScriptModuleConfiguration.Database);
            intp.SetGlobalValue("Host", ScriptModuleConfiguration.Host);
            intp.SetGlobalValue("Json", ScriptModuleConfiguration.Json);
            intp.SetGlobalValue("UI", ScriptModuleConfiguration.UI);
            intp.SetGlobalValue("Network", ScriptModuleConfiguration.Network);
            intp.SetGlobalValue("Schedule", ScriptModuleConfiguration.Schedule);
            intp.SetGlobalValue("Time", ScriptModuleConfiguration.Time);
            intp.SetGlobalValue("Xml", ScriptModuleConfiguration.Xml);
            intp.SetGlobalValue("Zip", ScriptModuleConfiguration.Zip);
            intp.SetGlobalValue("Unity", ScriptModuleConfiguration.Unity);

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            intp.RunUntilDone();
            watch.Stop();

            StdOutput("Time: " + watch.ElapsedMilliseconds);

        }

        void StdOutput(string msg) { Debug.Log("Std: " + msg); }
        void ErrOutput(string msg) { Debug.Log("Err: " + msg); }
    }

}

