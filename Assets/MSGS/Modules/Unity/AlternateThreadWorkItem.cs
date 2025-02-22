using System.Threading;

namespace MiniScript.MSGS.Unity
{
    public class AlternateThreadWorkItem
    {
        public ManualResetEventSlim eventSlim = new ManualResetEventSlim();
        public UnityModuleName Module;
        public int FunctionName;
        public object[] args;
        public object result;
    }
}

