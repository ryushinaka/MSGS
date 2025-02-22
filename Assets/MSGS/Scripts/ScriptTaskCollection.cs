using System.Collections.Concurrent;
using System.Collections.Generic;
using MiniScript.MSGS.Scripts;

namespace MiniScript.MSGS
{
    /// <summary>
    /// Container for ScriptExecutionContext instances
    /// </summary>
    public class ScriptTaskCollection
    {
        ConcurrentDictionary<TempValString, ScriptExecutionContext> tasks =
        new ConcurrentDictionary<TempValString, ScriptExecutionContext>();

        /// <summary>
        /// 
        /// </summary>
        public List<string> TaskNames
        {
            get
            {
                var it = tasks.Keys.GetEnumerator();
                List<string> rst = new List<string>();
                while (it.MoveNext())
                {
                    rst.Add(it.Current.value);
                }
                return rst;
            }
            set { }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<ScriptExecutionContext> TaskContainers
        {
            get
            {
                return new List<ScriptExecutionContext>(tasks.Values);
            }
            set { }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return tasks.Count; }
            set { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
        public bool TaskAdd(string taskName, ScriptExecutionContext sec)
        {
            return tasks.TryAdd(TempValString.Get(taskName), sec);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskName"></param>
        public void TaskRemove(string taskName)
        {
            ScriptExecutionContext tmp = null;
            tasks.TryRemove(TempValString.Get(taskName), out tmp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
        public bool TryGetTask(string taskName, out ScriptExecutionContext sec)
        {
            return tasks.TryGetValue(TempValString.Get(taskName), out sec);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            tasks.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(string taskName)
        {
            return tasks.ContainsKey(TempValString.Get(taskName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<ScriptExecutionContext> GetEnumerator()
        {
            return tasks.Values.GetEnumerator();
        }
    }
}
