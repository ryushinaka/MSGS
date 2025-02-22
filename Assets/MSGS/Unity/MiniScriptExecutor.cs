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
            if (ScriptAsset.Archive) { }
            if (ScriptAsset.Audio) { }
            if (ScriptAsset.Data) { intp.SetGlobalValue("data", DataIntrinsics.Get()); }
            if (ScriptAsset.Database) { }
            if (ScriptAsset.Host) { intp.SetGlobalValue("host", HostModule.Get()); }
            if (ScriptAsset.Json) { intp.SetGlobalValue("json", JsonModule.Get()); }
            if (ScriptAsset.Network) { intp.SetGlobalValue("network", NetworkModule.Get()); }
            if (ScriptAsset.MUUI) { intp.SetGlobalValue("ui", MUUIIntrinsics.Get()); }
            if (ScriptAsset.Schedule) { intp.SetGlobalValue("schedule", ScheduleManager.Get()); }
            if (ScriptAsset.Time) { intp.SetGlobalValue("time", TimeKeeper.Get()); }
            if (ScriptAsset.XML) { intp.SetGlobalValue("xml", XmlModule.Get()); }
            if (ScriptAsset.Zip) { intp.SetGlobalValue("zip", ZipModule.Get()); }

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

