using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniScript.MSGS.MUUI
{
    /// <summary>
    /// Top-level component in the Unity scene graph, for UI's that are managed
    /// purely by MiniScript scripting
    /// </summary>
    public class MUUISceneManager : BaseSceneManager
    {
        public override void Instantiate(string name, string iname)
        {
            if(InstancedForms.ContainsKey(iname))
            {
                MiniScriptSingleton.LogWarning(
                    "Instantiating a new UI prefab of type (" + name + "), with an already existing key of '" + iname + "'."
                    );
            }
            if(UsableForms.ContainsKey(name))
            {
                var it = Instantiate(UsableForms[name].gameObject);
                it.name = iname;

                //this is a slightly sticky point of UI behavior based on the scripting system.
                //the design choice has been to replace the existing UI element and destroy the previous one
                if (InstancedForms.ContainsKey(iname))
                {
                    //issue a warning for this state
                    MiniScriptSingleton.LogWarning(
                   "Instantiating a new UI prefab of type (" + name + "), with an already existing key of '" + iname + "'."
                   );

                    //because destruction of gameobjects and components is not instant and can be delayed
                    //inform the BaseForm instance that it is being destroyed to avoid raising invalid events
                    InstancedForms[iname].isDestroying = true;
                    InstancedForms[iname].eventQueue.Enqueue(null);
                    //move the UI prefab instance to the off canvas holding gameobject until its destroyed
                    InstancedForms[iname].gameObject.transform.SetParent(holdingspace.transform);
                    //remove the reference
                    InstancedForms.Remove(iname); 
                    //have Unity engine free up the allocated resources
                    Destroy(InstancedForms[iname].gameObject);
                }

                InstancedForms.Add(iname, it.GetComponent<BaseForm>());
                //tell the UI prefab instance it has been instanced for event handling
                //InstancedForms[iname].R

            }
            else
            {
                MiniScriptSingleton.LogWarning(
                    "Attempted to Instantiate UI Prefab (" + name + "), but no matching name could be found."
                    );
            }

        }

        public override void ReadUIPrefab(string xml, string label, bool defaultactive = false)
        {
        
        }
    }
}
