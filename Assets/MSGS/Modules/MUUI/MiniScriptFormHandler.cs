using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MiniScript.MSGS.MUUI
{
    //[RequireComponent(typeof())]
    /// <summary>
    /// A container of a set of UI elements.
    /// </summary>
    /// <remarks>Acts as the root node for a collection of UI elements, also provides management for the elements it contains.</remarks>
    public class MiniScriptFormHandler : BaseForm
    {
        #region Members
        internal BaseForm parent;

        #endregion

        #region events
        public new Action OnKeyPress, OnKeyUp, OnKeyDown;
        public new Action OnMouseClick, OnMouseDown, OnMouseUp;
        //public Action On
        #endregion

        #region MonoBehaviour callbacks
        void Start()
        {

        }
                
        void Update()
        {

        }
        #endregion

        #region 
        /// <summary>
        /// Sets the flag for if the Form is resizable
        /// </summary>
        public new bool isResizable { get;set; }
        
        /// <summary>
        /// Allows UI events to pass from this MiniScriptFormHandler instance to the next
        /// MiniScriptFormHandler instance above it in Unity's scene graph/heirarchy.
        /// </summary>
        public new bool EnableEventPassthrough { get;set; }
       
        /// <summary>
        /// Disables this UI element in Unity's scene graph.
        /// </summary>
        /// <remarks>Any children will be kept in their current state when this is called.</remarks>
        public new void Hide() { }
        
        /// <summary>
        /// Enables this UI element in Unity's scene graph.
        /// </summary>
        /// <remarks>Any children will be kept in their previous state if Hide() was previously called.
        /// Events are enabled by default, to override use the overload method Show(bool).</remarks>
        public new void Show() { }
        /// <summary>
        /// Enables this UI element in Unity's scene graph.
        /// </summary>
        /// <remarks>Any children will be kept in their previous state if Hide() was previously called.</remarks>
        /// <param name="enableEvents">true if events should be raised, false otherwise.</param>
        public new void Show(bool enableEvents)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new List<IControl> GetUIComponents()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Loads the UI and initializes/sets the children of this root node
        /// </summary>
        /// <param name="xml"></param>
        public new void LoadUI(string xml)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Removes this MiniScriptFormHandler instance and all of its children from Unity's scene graph.
        /// </summary>
        public new void Destroy()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}

