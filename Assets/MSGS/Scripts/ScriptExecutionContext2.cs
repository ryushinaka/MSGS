using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace MiniScript.MSGS.Scripts
{   
    public class ScriptExecutionContext2 : MonoBehaviour
    {
        public delegate void onCompletionCallback();
        public onCompletionCallback onCompletion;

        Interpreter interpreter;
        public UInt64 stepcounter;
        public float scriptTime;
        bool cancelRequested;
        public bool started;
        public bool ended;
        public bool hasErrors;
        public bool hasStdOutput;
        public bool needsMoreInput;
        ValMap map;

        public bool didModifyPrepended;
        Dictionary<string, string> prependedscripts;
        public Dictionary<string, string> prependedScripts
        {
            //return a copy of the internal collection
            get
            {
                Dictionary<string, string> rst = null;
                //thread safety lock
                rst = new Dictionary<string, string>(prependedScripts);
                return rst;
            }
            set { }
        }

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

        public void AddPrependedScript(string scriptname, string source)
        {
            if (!prependedScripts.ContainsKey(scriptname)) { prependedScripts.Add(scriptname, source); didModifyPrepended = true; }
        }

        public void RemovePrependedScript(string scriptname)
        {
            if (prependedScripts.ContainsKey(scriptname)) { prependedScripts.Remove(scriptname); didModifyPrepended = true; }
        }

        string scriptsource;
        public string ScriptSource
        {
            get { return scriptsource; }
            set
            {
                scriptsource = value;
                if (interpreter == null)
                {
                    interpreter = new Interpreter();
                    interpreter.standardOutput = new TextOutputMethod(StandardOutput);
                    interpreter.errorOutput = new TextOutputMethod(ErrorOutput);
                    interpreter.implicitOutput = new TextOutputMethod(ImplicitOutput);
                }

                interpreter.Reset(scriptsource);
                interpreter.Compile();
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
                
        public void Execute(object obj)
        {
            #region
            //check flag to make sure we dont try to start the Task multiple times if its already running
            if (started) { return; }
            //set flag so we dont queue the same script multiple times
            else { started = true; ended = false; }

            if (err_msgs == null) { err_msgs = new List<string>(); }
            else { err_msgs.Clear(); }
            if (std_output == null) { std_output = new List<string>(); }
            else { std_output.Clear(); }

            //was the prepended script collection modified? if so, update the script source
            if (didModifyPrepended)
            {   //a StringBuilder as we could potentially be using a fair amount of text content
                System.Text.StringBuilder str = new System.Text.StringBuilder();

                foreach (KeyValuePair<string, string> kv in prependedScripts)
                {
                    str.AppendLine(kv.Value);
                }

                str.Append(scriptsource); //add the actual script source after the prepended scripts
                interpreter.Reset(str.ToString()); //set the interpreter to use the new script source
                //now we compile and we're ready to setup the task object
                interpreter.Compile();                
            }

            //check interpreter state after parsing script source
            if (interpreter.NeedMoreInput())
            {   //something didnt finish so we abort/return early   
                std_output.Add("Needs More Input");               
                needsMoreInput = true;
                return;
            }

            stepcounter = 0; //reset the stepcounter for this iteration

            if (hasErrors)
            {
                //something caused a problem during/after .Compile()  OR during the last script execution, abort here with a friendly message to StdOutput
                std_output.Add("Errors were found in the last compilation/execution of this script(" + Label + "), exiting Start() before script execution began.");
                Debug.Log("hasE");
                return;
            }
            
            interpreter.Reset(scriptsource);
            interpreter.Compile();

            //StartCoroutine(WaitAndPrint(scriptTime));

            while (!interpreter.done)
            {
                interpreter.RunUntilDone(scriptTime);
                stepcounter++;
                System.Threading.Thread.Sleep(1);
                //yield return null;
            }

            ended = true; started = false;            
            #endregion           
        }

        public IEnumerator WaitAndPrint(float waitTime)
        {
            if(interpreter == null) { yield return null; }

            while (!interpreter.done)
            {
                interpreter.RunUntilDone(waitTime);
                stepcounter++;
                yield return null;
            }
        }

        //bool loggedEnd = false;
        void Update()
        {
            #region
            //if (interpreter.Running())
            //{
            //    interpreter.RunUntilDone(0.001f, true);
            //    //interpreter.Step(); stepcounter++;
            //}
            //else
            //{
            //    if (!ended)
            //    {
            //        ended = true;
            //        Debug.Log("execute ended " + DateTime.Now.Ticks.ToString());
            //    }
            //}

            //if (AutoRestart && ended)
            //{
            //    started = true; ended = false;
            //    stepcounter = 0;
            //    interpreter.Restart();
            //}
            #endregion

            //if (interpreter.Running() && ended == false) { interpreter.RunUntilDone(0.01f); stepcounter++; }

            //if(!interpreter.Running() && !loggedEnd) { Debug.Log("done @" + DateTime.Now.Ticks.ToString()); loggedEnd = true; }
        }

        void Start()
        {
            interpreter = new Interpreter();
            interpreter.errorOutput = new TextOutputMethod(ErrorOutput); //assign callback handlers
            interpreter.standardOutput = new TextOutputMethod(StandardOutput);
            err_msgs = new List<string>(); std_output = new List<string>(); //initialize Lists for callback message storage

            //this allows the intrinsic methods to access *this* object (pun?)
            interpreter.hostData = this;
        }

        //property or method to append messages to the DataStoreWarehouse queue
        public void Data() { }

        //property or method to append messages to the MUUI queue of updates
        public void UI() { }

        void ImplicitOutput(string msg) { std_output.Add("Imp: " + msg); hasStdOutput = true; }
        void ErrorOutput(string msg) { err_msgs.Add(DateTime.Now.ToString() + " " + msg); hasErrors = true; }
        void StandardOutput(string msg) { std_output.Add(DateTime.Now.ToString() + " " + msg); hasStdOutput = true; }
    }
}

