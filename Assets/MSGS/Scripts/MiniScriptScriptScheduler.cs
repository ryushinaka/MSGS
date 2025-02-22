using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiniScript.MSGS.Scripts;
using MiniScript;
using MiniScript.MSGS;

/// <summary>
/// Allows the scheduling of ScriptExecutionContext objects, which accomplishes multithreading
/// for script execution outside of Unity's main thread.
/// </summary>
public class MiniScriptScriptScheduler
{
    public readonly ScriptTaskCollection tasks = new ScriptTaskCollection();

    Dictionary<string, Thread> dedicatedThreads = new Dictionary<string, Thread>();


    public void EnqueueTask(ref ScriptExecutionContext sec, string taskName, bool dedicatedThread, int delayBetweenRestartsMs = 1, 
        bool autorestart = false)
    {
        //setup worker thread to create the Task and SEC parameters
        //return immediately to not block caller
        
        
    }

    public void DequeueTask() { }


    public MiniScriptScriptScheduler(int poolsize) { }


}
