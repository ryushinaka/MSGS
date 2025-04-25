using MiniScript.MSGS.Scripts;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MiniScript.MSGS.Unity.DevConsole
{
    public class ConsoleCommandSO : ScriptableObject
    {
        [HideInInspector]
        Interpreter interpreter = new Interpreter();
        
        public UInt64 stepCounter = 0;

        [TextArea(3, 15)]
        public string scriptSource = string.Empty;
        /// <summary>
        /// If set to true will cause the ConsoleCommandSO to generate debug messages to the log
        /// </summary>
        public bool debug = false;
        [HideInInspector]
        public bool compilebroke = false;
        [HideInInspector]
        public bool cancelRequested = false;

        [ShowInInspector]
        public DateTime startedWhen = DateTime.Now, finishedWhen;

        [ShowInInspector]
        public TimeSpan runTime
        {
            get
            {
                if (finishedWhen <= DateTime.Now && startedWhen < finishedWhen) { return finishedWhen - startedWhen; }
                return DateTime.Now - startedWhen;
            }
            private set { }
        }

        public bool hasErrors;
        public bool hasStdOutput;
        public bool needsMoreInput;
        public bool isRunning;

        public List<string> err_msgs, std_output;
        public IReadOnlyList<string> ErrMsgs
        {
            get { return err_msgs.AsReadOnly(); }
            set { }
        }

        public IReadOnlyList<string> StdOutput
        {
            get { return std_output.AsReadOnly(); }
            set { }
        }

        public ConsoleCommandSO()
        {
            OutputEvent = new UnityEngine.Events.UnityEvent<string>();

            err_msgs = new List<string>();
            std_output = new List<string>();

            interpreter.errorOutput = new TextOutputMethod(ErrorOutput);
            interpreter.implicitOutput = new TextOutputMethod(ImplicitOutput);
            interpreter.standardOutput = new TextOutputMethod(StandardOutput);
        }

        public void AddGlobal(string label, Value v)
        {
            interpreter.SetGlobalValue(label, v);
        }

        void ImplicitOutput(string msg)
        {
            if (debug) { Debug.Log("ConsoleCommandSO[Implicit]: " + msg); }
            OutputEvent.Invoke(msg);
            //std_output.Add("Imp: " + msg); hasStdOutput = true;
        }
        void ErrorOutput(string msg)
        {
            if (debug) { Debug.Log("ConsoleCommandSO[Error]: " + msg); compilebroke = true; }
            OutputEvent.Invoke(msg);
            //err_msgs.Add(DateTime.Now.ToString() + " " + msg); hasErrors = true;
        }
        void StandardOutput(string msg)
        {
            if (debug) { Debug.Log("ConsoleCommandSO[StdOut]: " + msg); }
            OutputEvent.Invoke(msg);
            //std_output.Add(DateTime.Now.ToString() + " " + msg); hasStdOutput = true;
        }

        public UnityEngine.Events.UnityEvent<string> OutputEvent;

        public void Run()
        {
            if (!isRunning)
            {
                isRunning = true;
                err_msgs.Clear(); std_output.Clear();

                interpreter.Reset(scriptSource); interpreter.Compile();
                #region assign objects for the globals reference/values to script code
                interpreter.SetGlobalValue("Audio", ScriptModuleConfiguration.Audio);
                
                interpreter.SetGlobalValue("Data", ScriptModuleConfiguration.Data);
                //if (debug) { Data.DataIntrinsics.debug = true; }
                interpreter.SetGlobalValue("Database", ScriptModuleConfiguration.Database);
                //if (debug) { Database.DatabaseModule.debug = true; }
                interpreter.SetGlobalValue("Host", ScriptModuleConfiguration.Host);
                //if (debug) { Host.HostModule.debug = true; }
                interpreter.SetGlobalValue("Json", ScriptModuleConfiguration.Json);
                interpreter.SetGlobalValue("UI", ScriptModuleConfiguration.UI);
                //if(debug) { MUUI.MUUIIntrinsics.debug = true; }
                interpreter.SetGlobalValue("Network", ScriptModuleConfiguration.Network);
                interpreter.SetGlobalValue("Schedule", ScriptModuleConfiguration.Schedule);
                interpreter.SetGlobalValue("Time", ScriptModuleConfiguration.Time);
                interpreter.SetGlobalValue("Xml", ScriptModuleConfiguration.Xml);
                interpreter.SetGlobalValue("Zip", ScriptModuleConfiguration.Zip);
                interpreter.SetGlobalValue("Unity", ScriptModuleConfiguration.Unity);
                #endregion

                if (!compilebroke)
                {
                    try
                    {
                        //if (debug) { Debug.Log($"ConsoleCommandSO[Info]: Script[{this.name}] was started at {System.DateTime.Now.Millisecond }"); }

                        startedWhen = DateTime.Now;
                        try
                        {
                            Action act = async () => { await RunScript(); };
                            act.Invoke();
                        }
                        catch (Exception ex)
                        {
                            Debug.Log("ConsoleCommandSO[Exception]: Script threw Exception while executing. " + ex.Message);
                        }

                        finishedWhen = DateTime.Now;

                        //if (debug) { Debug.Log($"ConsoleCommandSO[Info]: Script[{this.name}] was completed at {System.DateTime.Now.Millisecond }"); }
#if UNITY_EDITOR
                        foreach(string s in err_msgs) { OutputEvent.Invoke("Error: " + s); }
                        foreach (string s in std_output) { OutputEvent.Invoke("Std: " + s); }
#endif
                    }
                    catch (OperationCanceledException)
                    {
                        if (debug) { Debug.LogWarning($"ConsoleCommandSO[Warning]: Script was canceled."); }
                    }
                    catch (Exception ex)
                    {
                        if (debug) { Debug.LogError($"ConsoleCommandSO[Error]: Script encountered an Exception: {ex.Message}"); }
                    }
                }
            }
        }

        async Task RunScript()
        {
            while (!interpreter.done)
            {
                if (cancelRequested)
                {
                    if (debug) { Debug.LogWarning("ConsoleCommandSO[Info]: Cancellation requested before the script completed."); }             
                    cancelRequested = false;
                    interpreter.Stop();
                    return;
                }
                else { await Task.Run(() => { interpreter.Step(); stepCounter++; }); }
            }

            //if(debug) { Debug.Log("ConsoleCommandSO[Info]: interpreter finished."); }
        }
    }
}

