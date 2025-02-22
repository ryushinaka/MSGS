using UnityEngine;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;

namespace MiniScript.MSGS.Unity
{
    public class AlternateThreadDispatcher : MonoBehaviour
    {
        private static AlternateThreadDispatcher _instance;

        private static readonly ConcurrentQueue<AlternateThreadWorkItem> _jobs
               = new ConcurrentQueue<AlternateThreadWorkItem>();

        private static readonly ConcurrentStack<AlternateThreadWorkItem> pool
            = new ConcurrentStack<AlternateThreadWorkItem>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            // Create a GameObject with this dispatcher if one doesn't exist yet.
            if (_instance == null)
            {
                var dispatcherGO = new GameObject("MiniScriptMSGSAlternateThreadDispatcher");
                _instance = dispatcherGO.AddComponent<AlternateThreadDispatcher>();

                for(int i = 0; i < 1000; i++)
                {   //populate the pool so it can be used
                    pool.Push(new AlternateThreadWorkItem());
                }

                // Usually you don't want to destroy your dispatcher on load
                DontDestroyOnLoad(dispatcherGO);
            }

        }
        
        void Start()
        {
            
        }

        //we use FixedUpdate as the execution of this callback is more consistent/regular than just Update or LateUpdate
        void FixedUpdate()
        {
            try {
                while (_jobs.TryDequeue(out var job)) {
                    switch(job.Module)
                    {
                        case UnityModuleName.Random:
                            UnityEngineRandomWrapper.HandleWorkItem(ref job);
                            break;
                        case UnityModuleName.Application:
                            UnityEngineApplicationWrapper.HandleWorkItem(ref job);
                            break;
                        case UnityModuleName.PlayerPrefs:
                            UnityEnginePlayerPrefsWrapper.HandleWorkItem(ref job);
                            break;
                        case UnityModuleName.Time:
                            UnityEngineTimeWrapper.HandleWorkItem(ref job);
                            break;
                        default:
                            Debug.Log("Module " + (int)job.Module + " is not supported.");
                            break;
                    }
                } 
            }
            catch (Exception ex) { Debug.Log(ex.ToString()); }
        }

        public static void Enqueue(ref AlternateThreadWorkItem obj)
        {
            _jobs.Enqueue(obj);
        }

        public static AlternateThreadWorkItem Get()
        {
            AlternateThreadWorkItem rst;
            if(pool.TryPop(out rst)) { return rst; }
            else { return rst; }
        }

        public static void Return(ref AlternateThreadWorkItem item)
        {
            item.result = null;
            item.args = null;
            item.Module = UnityModuleName.Null;
            item.FunctionName = -1;
            item.eventSlim.Reset();
            pool.Push(item);

        }
    }
}

