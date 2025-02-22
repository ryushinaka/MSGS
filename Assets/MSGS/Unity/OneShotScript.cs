using MiniScript.MSGS.Scripts;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MiniScript.MSGS.Unity
{
    //How does this differ from the ScriptExecutionContent?
    //This object is derived from ScriptableObject and is not a MonoBehaviour
    //ScriptExecutionContext is designed around having a GameObject as its core/base
    //It does not allow for manipulation of script content dynamically
    //It allows the assignment of 1 or more GameObject's that are made accessable to the script via its globals reference

    /// <summary>
    /// An instance of a MiniScript script that executes independently from the caller
    /// </summary>
    /// <remarks>Allows the execution of a script as immediately as possible without impeding the callers execution thereafter, or
    /// for long running scripts.</remarks>
    [CreateAssetMenu(fileName = "NewOneShotScript", menuName = "MiniScript/SO OneShotScript", order = 3)]
    public class OneShotScript : ScriptableObject
    {
        [HideInInspector]
        Interpreter interpreter = new Interpreter();
        [HideInInspector]
        List<GameObject> observed = new List<GameObject>();
        [HideInInspector]
        Dictionary<int, List<ValMap>> maps = new Dictionary<int, List<ValMap>>();

        public UInt64 stepCounter = 0;

        [TextArea(3, 15)]
        public string scriptSource = string.Empty;
        /// <summary>
        /// If set to true will cause the OneShotScript to generate debug messages to the log
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

        public bool AutoRestart { get; set; }

        public void AddGameObject(GameObject obj)
        {
            //have to specify using the extension method and not the default .Contains()
            //this helps to ensure that we dont duplicate the gameobject and its components by mistake
            if (!MiniScript.MSGS.Unity.UnityTypeExtensions.Contains(observed, obj))
            {
                observed.Add(obj);
                maps.Add(obj.GetInstanceID(), new List<ValMap>());
                foreach (Component c in obj.GetComponents(typeof(Component)))
                {   //does this Component implement IMiniscript? if so, call its .ToValMap()                    
                    var reff = maps[obj.GetInstanceID()];
                    if (c is IMiniScript) { reff.Add(((IMiniScript)c).ToValMap()); }
                }
            }
            else
            {
                Debug.Log("OneShotScript[Error]: Attempted to add a gameObject(" + obj.name + "/" +
                    obj.GetInstanceID() + ") for observing, gameObject is already observed.");
            }
        }

        public OneShotScript()
        {
            err_msgs = new List<string>();
            std_output = new List<string>();

            interpreter.errorOutput = new TextOutputMethod(ErrorOutput);
            interpreter.implicitOutput = new TextOutputMethod(ImplicitOutput);
            interpreter.standardOutput = new TextOutputMethod(StandardOutput);
            interpreter.hostData = new CustomHostData();
        }

        void ImplicitOutput(string msg)
        {
            if (debug) { Debug.Log("OneShotScript SO[Implicit]: " + msg); }
            std_output.Add("Imp: " + msg); hasStdOutput = true;
        }
        void ErrorOutput(string msg)
        {
            if (debug) { Debug.Log("OneShotScript SO[Error]: " + msg); compilebroke = true; }
            err_msgs.Add(DateTime.Now.ToString() + " " + msg); hasErrors = true;
        }
        void StandardOutput(string msg)
        {
            if (debug) { Debug.Log("OneShotScript SO[StdOut]: " + msg); }
            std_output.Add(DateTime.Now.ToString() + " " + msg); hasStdOutput = true;
        }

        public void Stop()
        {
            cancelRequested = true;
        }

        public void Run()
        {
            if(!isRunning)
            {
                isRunning = true;
                err_msgs.Clear(); std_output.Clear();

                //special sauce for all the Intrinsics is within CustomHostData
                //interpreter.hostData = new CustomHostData();

                //interpreter.SetGlobalValue("observed", maps.ToValMap()); //the magic of extensions

                interpreter.Reset(scriptSource); interpreter.Compile();
                #region assign objects for the globals reference/values to script code
                interpreter.SetGlobalValue("Audio", ScriptModuleConfiguration.Audio);
                interpreter.SetGlobalValue("Data", ScriptModuleConfiguration.Data);
                interpreter.SetGlobalValue("Database", ScriptModuleConfiguration.Database);
                interpreter.SetGlobalValue("Host", ScriptModuleConfiguration.Host);
                interpreter.SetGlobalValue("Json", ScriptModuleConfiguration.Json);
                interpreter.SetGlobalValue("UI", ScriptModuleConfiguration.UI);
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
                        if (debug) { Debug.Log($"OneShotScript[Info]: Script[{this.name}] was started at {System.DateTime.Now.Millisecond }"); }

                        startedWhen = DateTime.Now;
                        try {
                            Action act = async () => { await RunScript(); };
                            act.Invoke();
                        }
                        catch(Exception ex)
                        {
                            Debug.Log("OneShotScript[Exception]: Script threw Exception while executing. " + ex.Message);
                        }
                        
                        finishedWhen = DateTime.Now;

                        if (debug) { Debug.Log($"OneShotScript[Info]: Script[{this.name}] was completed at {System.DateTime.Now.Millisecond }"); }
                    }
                    catch (OperationCanceledException)
                    {
                        if (debug) { Debug.LogWarning($"OneShotScript[Warning]: Script[{this.name}] was canceled."); }
                    }
                    catch (Exception ex)
                    {
                        if (debug) { Debug.LogError($"OneShotScript[Error]: Script[{this.name}] encountered an Exception: {ex.Message}"); }
                    }
                }
            }
           
        }

        public void Run(object obj)
        {
            Debug.Log("meow");
            if (!isRunning)
            {
                isRunning = true;
                err_msgs.Clear(); std_output.Clear();

                //special sauce for all the Intrinsics is within CustomHostData
                //interpreter.hostData = new CustomHostData();

                //interpreter.SetGlobalValue("observed", maps.ToValMap()); //the magic of extensions

                interpreter.Reset(scriptSource); interpreter.Compile();
                Debug.Log("compiled!");
                #region assign objects for the globals reference/values to script code
                interpreter.SetGlobalValue("Audio", ScriptModuleConfiguration.Audio);
                interpreter.SetGlobalValue("Data", ScriptModuleConfiguration.Data);
                interpreter.SetGlobalValue("Database", ScriptModuleConfiguration.Database);
                interpreter.SetGlobalValue("Host", ScriptModuleConfiguration.Host);
                interpreter.SetGlobalValue("Json", ScriptModuleConfiguration.Json);
                interpreter.SetGlobalValue("UI", ScriptModuleConfiguration.UI);
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
                        if (debug) { Debug.Log($"OneShotScript[Info]: Script[{this.name}] was started at {System.DateTime.Now.Millisecond }"); }

                        startedWhen = DateTime.Now;
                        try
                        {
                            //Action act = async () => { await RunScript(); };
                            //act.Invoke();
                        }
                        catch (Exception ex)
                        {
                            Debug.Log("OneShotScript[Exception]: Script threw Exception while executing. " + ex.Message);
                        }

                        finishedWhen = DateTime.Now;

                        if (debug) { Debug.Log($"OneShotScript[Info]: Script[{this.name}] was completed at {System.DateTime.Now.Millisecond }"); }
                    }
                    catch (OperationCanceledException)
                    {
                        if (debug) { Debug.LogWarning($"OneShotScript[Warning]: Script[{this.name}] was canceled."); }
                    }
                    catch (Exception ex)
                    {
                        if (debug) { Debug.LogError($"OneShotScript[Error]: Script[{this.name}] encountered an Exception: {ex.Message}"); }
                    }
                }
            }
        }

        async Task RunScript()
        {
            while (!interpreter.done) {
                if (cancelRequested) {
                    if (debug) { Debug.LogWarning("OneShotScript[Info]: Cancellation requested before the script completed."); }
                    AutoRestart = false;
                    cancelRequested = false;
                    interpreter.Stop();
                    return; }
                else { await Task.Run(() => { interpreter.Step(); stepCounter++; }); }
            }
            if (AutoRestart) { interpreter.Restart(); stepCounter = 0; }
        }
    }
}

