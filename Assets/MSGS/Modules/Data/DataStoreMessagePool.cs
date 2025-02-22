using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace MiniScript.MSGS.Data
{
    public class DataStoreMessagePool
    {
        private readonly ConcurrentStack<DataStoreMessage> _objects;
        private static int _poolsize = 0;
        
        public bool IsInitialized { get; set; }
        
        public DataStoreMessagePool()
        {
            IsInitialized = false;
            _objects = new ConcurrentStack<DataStoreMessage>();            
        }

        public DataStoreMessage Get()
        {
            DataStoreMessage tmpHWND;
            if (_objects.TryPop(out tmpHWND)) { return tmpHWND; }
            else { tmpHWND = new DataStoreMessage(this); }
            return tmpHWND;
        }

        public void Return(DataStoreMessage item)
        {
            _objects.Push(item);
        }

        public void Initialize(int poolsize)
        {
            _poolsize = poolsize;
            Task.Factory.StartNew(new Action(DoInitialize));
        }

        void DoInitialize()
        {
            DataStoreMessagePool me = this;
            for(int i = 0; i < _poolsize; i ++)
            {
                _objects.Push(new DataStoreMessage(me));
            }
            IsInitialized = true;
        }
    }
}
