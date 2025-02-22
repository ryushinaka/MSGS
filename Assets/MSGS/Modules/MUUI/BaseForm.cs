using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Concurrent;

namespace MiniScript.MSGS.MUUI
{
    /// <summary>
    /// Abstract base class for handling UI elements in a Unity scene.
    /// </summary>
    public abstract partial class BaseForm : MonoBehaviour
    {
        public bool isResizable { get; set; }
        public bool EnableEventPassthrough { get; set; }

        public bool isDestroying { get; set; }

        public string EventHandler;
        public ConcurrentQueue<object> eventQueue;

        public Action OnKeyPress, OnKeyUp, OnKeyDown;
        public Action OnMouseClick, OnMouseDown, OnMouseUp;

        /// <summary>
        /// Hide this UI element, without affecting its children's states.
        /// </summary>
        public virtual void Hide() { }

        /// <summary>
        /// Show this UI element, possibly re-enabling events.
        /// </summary>
        /// <param name="enableEvents">Whether to enable events for this element.</param>
        public virtual void Show(bool enableEvents = true) { }
        
        public virtual void Show() { }

        public virtual List<IControl> GetUIComponents() { return new List<IControl>(); }
        
        /// <summary>
        /// Load UI elements from a specified XML.
        /// </summary>
        /// <param name="xml">XML string to load UI from.</param>
        public virtual void LoadUI(string xml)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Removes this UI element and all its children from the Unity scene graph.
        /// </summary>
        public virtual void Destroy()
        {
            throw new System.NotImplementedException();
        }
    }
}
