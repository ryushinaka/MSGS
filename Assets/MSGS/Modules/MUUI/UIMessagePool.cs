using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace MiniScript.MSGS.MUUI
{
    /// <summary>
    /// Object pooling class for InputEvent objects
    /// </summary>
    /// <remarks>If this class needs to be used on a version of Unity prior to 2021 LTS, then this class will have to be modified as the UnityEngine.Pool is required by this implementation.</remarks>
    public class InputEventMessagePool
    {
        //https://docs.unity3d.com/2021.1/Documentation/ScriptReference/Pool.IObjectPool_1.Get.html
        //Unity "approved" design pattern

        private readonly IObjectPool<InputEvent> inputPool;

        public InputEventMessagePool(int initialSize, int maxSize)
        {
            inputPool = new ObjectPool<InputEvent>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, initialSize, maxSize);
            for (int i = 0; i < initialSize; i++)
            {
                inputPool.Release(CreatePooledItem());
            }
        }

        public InputEvent Get()
        {
            return inputPool.Get();
        }

        public void Release(ref InputEvent mevent)
        {
            inputPool.Release(mevent);
        }

        InputEvent CreatePooledItem()
        {
            InputEvent mevent = new InputEvent();
            return mevent;
        }

        // Called when an item is returned to the pool using Release
        void OnReturnedToPool(InputEvent mevent)
        {
            //Which action carries the overhead: Release or Get?  Profiling your code carefully will show the answer
            //but in this case, Get suffers the overhead of resetting the InputEvent data
        }

        // Called when an item is taken from the pool using Get
        void OnTakeFromPool(InputEvent mevent)
        {
            mevent.ClickLeft = false;
            mevent.ClickLeftDouble = false;
            mevent.ClickMiddle = false;
            mevent.ClickRight = false;
            mevent.ClickRightDouble = false;
            mevent.Element = string.Empty;
            mevent.ElementType = UIElementType.None;
            mevent.KeyCode1 = KeyCode.None;
            mevent.KeyCode2 = KeyCode.None;
            mevent.KeyCode3 = KeyCode.None;
            mevent.OnEnter = false;
            mevent.OnExit = false;
            mevent.OnScrollDown = false;
            mevent.OnScrollUp = false;
            mevent.PrefabName = string.Empty;
            mevent.ScriptName = string.Empty;
        }

        // If the pool capacity is reached then any items returned will be destroyed.
        // We can control what the destroy behavior does, here we destroy the GameObject.
        void OnDestroyPoolObject(InputEvent mevent)
        {
            
        }
    }
}

