using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MiniScript.MSGS.Scripts
{
    //https://docs.unity3d.com/Manual/job-system-custom-nativecontainer-example.html - this needs a new version written to this spec

    /// <summary>
    /// Provides encapsulation for executing a MiniScript script.
    /// </summary>
    public class ScriptExecutionContext
    {
        Interpreter interpreter;
        Task task;
        CancellationTokenSource cts;
        public UInt64 stepcounter = 0;
        volatile bool started = false;
        bool hasErrors = false;
#pragma warning disable CS0414
        bool hasStdOutput = false;
#pragma warning restore
        public List<string> errmsgs, stdoutput;

        public void SetGlobalValue(string varName, Value value)
        {
            interpreter.SetGlobalValue(varName, value);
        }

        public Value GetGlobalValue(string varName)
        {
            return interpreter.GetGlobalValue(varName);
        }
               
        #region Execution Parameters
        bool didModifyPrepended = false;
        object lockPrependedScripts = new object();
        Dictionary<string, string> prependedscripts = new Dictionary<string, string>();
        public Dictionary<string, string> prependedScripts
        {
            //return a copy of the internal collection
            get
            {
                Dictionary<string, string> rst = null;
                //thread safety lock
                lock (lockPrependedScripts) { rst = new Dictionary<string, string>(prependedScripts); }
                return rst;
            }
            set { }
        }

        public void AddPrependedScript(string scriptname, string source)
        {
            lock (lockPrependedScripts)
            {
                if (!prependedScripts.ContainsKey(scriptname)) { prependedScripts.Add(scriptname, source); didModifyPrepended = true; }
            }
        }

        public void RemovePrependedScript(string scriptname)
        {
            lock (lockPrependedScripts)
            {
                if (prependedScripts.ContainsKey(scriptname)) { prependedScripts.Remove(scriptname); didModifyPrepended = true; }
            }
        }

        string scriptsource = string.Empty;
        public string ScriptSource
        {
            get { return scriptsource; }
            set
            {
                if (!started)
                {
                    scriptsource = value;
                    interpreter.Reset(scriptsource);
                    interpreter.Compile();
                    errmsgs.Clear(); stdoutput.Clear();
                }
            }
        }

        /// <summary>
        /// The string literal for this ScriptExecutionContext
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// True if you want the script to continue/restart executing after it finishes
        /// </summary>
        public bool AutoRestart { get; set; }
        #endregion

        #region Execution state control methods
        /// <summary>
        /// Schedules this scripts execution on a TaskScheduler and starts the Task
        /// </summary>
        /// <remarks></remarks>
        public void Start()
        {
            //check interpreter state after parsing script source
            if (interpreter.NeedMoreInput())
            {   //something didnt finish so we abort/return early
                return;
            }

            //check flag to make sure we dont try to start the Task multiple times if its already running
            if (started) { return; }

            //set flag so we dont queue the same script multiple times
            started = true;

            //set or reset the stepcounter for metrics
            stepcounter = 0;

            //was the prepended script collection modified? if so, update the script source
            if (didModifyPrepended)
            {   //a StringBuilder as we could potentially be using a fair amount of text content
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                //lock for thread safety
                lock (lockPrependedScripts)
                {
                    foreach (KeyValuePair<string, string> kv in prependedScripts)
                    {
                        str.Append(kv.Value);
                    }
                }

                str.Append(scriptsource); //add the actual script source after the prepended scripts
                interpreter.Reset(str.ToString()); //set the interpreter to use the new script source

                //before we compile, we reset the flags for hasErrors and hasStdOutput
                hasErrors = false; hasStdOutput = false;
                //now we compile and we're ready to setup the task object
                interpreter.Compile();
            }

            if (hasErrors)
            {
                //something caused a problem during/after .Compile()  OR during the last script execution, abort here with a friendly message to StdOutput
                stdoutput.Add("Errors were found in the last compilation/execution of this script(" + Label + "), exiting Start() before script execution began.");
                return;
            }

            //create task to execute the interpreter/script source with
            //task = MiniScriptSingleton.scheduler.factory.StartNew(() =>
            //{
            //    stepcounter = 0; //reset the stepcounter for this iteration

            //    while (!interpreter.done)
            //    {
            //        //do we need to cancel the script execution?
            //        if (cts.IsCancellationRequested) { interpreter.Stop(); started = false; return; }
            //        //take a step on the VM
            //        interpreter.Step();
            //        //increment the step counter for the sake of metrics
            //        stepcounter++;
            //    }

            //    //set the flag to false so it can be started again if requested
            //    started = false;

            //}, cts.Token);

            //add the task to the Scheduler for management
            //MiniScriptSingleton.scheduler.EnqueueTask(ref task);

            UnityEngine.Debug.Log("started");
        }

        /// <summary>
        /// Stops the scripts execution
        /// </summary>
        public void Stop()
        {
            cts.Cancel();
        }
        #endregion

        //property or method to append messages to the DataStoreWarehouse queue
        public void Data() { }

        //property or method to append messages to the MUUI queue of updates
        public void UI() { }

        public ScriptExecutionContext()
        {
            interpreter = new Interpreter();
            interpreter.errorOutput = new TextOutputMethod(ErrorOutput); //assign callback handlers
            interpreter.standardOutput = new TextOutputMethod(StandardOutput);
            errmsgs = new List<string>(); stdoutput = new List<string>(); //initialize Lists for callback message storage

            //this allows the intrinsic methods to access *this* object (pun?)
            interpreter.hostData = this;

            //token for managing the task executing the script/interpreter
            cts = new CancellationTokenSource();
        }

        void ErrorOutput(string msg) { errmsgs.Add(DateTime.Now.ToString() + " " + msg); hasErrors = true; }
        void StandardOutput(string msg) { stdoutput.Add(DateTime.Now.ToString() + " " + msg); hasStdOutput = true; }
    }
}

