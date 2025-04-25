using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Concurrent;
using MiniScript.MSGS.Unity;
using MiniScript.MSGS.Unity.DevConsole;

public class ScriptableObjectCache 
{
    public static ConcurrentQueue<OneShotScript> soOneShots = null;
    public static ConcurrentQueue<ConsoleCommandSO> soConsole = null;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static void InitializeCache()
    {
        soOneShots = new ConcurrentQueue<OneShotScript>();
        for(int i = 0; i < 1000; i++)
        {
            soOneShots.Enqueue(ScriptableObject.CreateInstance<OneShotScript>());
        }

        soConsole = new ConcurrentQueue<ConsoleCommandSO>();
        for (int i = 0; i < 100; i++)
        {
            soConsole.Enqueue(ScriptableObject.CreateInstance<ConsoleCommandSO>());
        }
    } 
}
