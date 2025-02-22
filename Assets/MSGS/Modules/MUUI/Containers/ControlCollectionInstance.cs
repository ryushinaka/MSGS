using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.MUUI
{
    /// <summary>
    /// Contains a set of UI controls and their state
    /// </summary>
    public class ControlCollectionInstance : MonoBehaviour
    {
        //add an enum w/ flags to allow filtering of the event pump per subscriber
        //look into mini-micro's events.ms to check your code for silliness
        //events can be handled by one script, or multiple scripts as assigned in the Inspector
        //it is also possible for startup.ms scripts to specify the event handling without
        //  any pre-configuration in the Inspector

        /// <summary>
        /// Enable/Disable the event pump
        /// </summary>
        public bool EventPumpState
        {
            get;
            set;
        }

        /// <summary>
        /// Config for which events are allowed to be raised and which are suppressed
        /// </summary>
        public BitArray EventPumpFilter
        {
            get;
            set;
        }

        void Start()
        {

        }

        void Update()
        {

        }

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