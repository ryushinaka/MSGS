using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using MiniScript.MSGS.ScriptableObjects;

namespace MiniScript.MSGS.Demonstrations
{
    public class CPUTestBenchmark : MonoBehaviour
    {
        public MiniScriptScriptAsset ScriptAsset;
        public GameObject prefab;

        int cpuCount = System.Environment.ProcessorCount;

        [ShowInInspector]
        public int LogicalCpuCount
        {
            get { return cpuCount; }
        }

        [Range(1f, 128f)]
        public int InstancesToRun = 32;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        [Button]
        public void StartTest()
        {
            List<GameObject> gos = new List<GameObject>();

            for(int x = 0; x < InstancesToRun; x++)
            {
                var go = Instantiate(prefab);
                go.transform.SetParent(this.transform);
                System.Threading.Thread.Sleep(1);
                gos.Add(go);
            }

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            Debug.Log("Cpu Test Start!");
            watch.Start();
            foreach(GameObject go in gos)
            {
                go.GetComponent<MiniScriptExecutor>().ExecuteScript();
            }
            watch.Stop();

            Debug.Log("Cpu Test Finished!  Time== " + watch.ElapsedMilliseconds);
        }
    }
}
