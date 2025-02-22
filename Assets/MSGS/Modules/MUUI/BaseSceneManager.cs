using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MiniScript.MSGS.MUUI.TwoDimensional;

namespace MiniScript.MSGS.MUUI
{
    /// <summary>
    /// Base class for a scene manager that is driven by MiniScript Scripting
    /// </summary>
    public abstract partial class BaseSceneManager : MonoBehaviour
    {
        public Dictionary<string, IControl> UsableControls;
        public GameObject controlPrefabs;
        public Dictionary<string, BaseForm> UsableForms;
        public GameObject formInstances;
        public GameObject holdingspace;

        public Dictionary<string, BaseForm> InstancedForms;

        public UnityEngine.Canvas canvas;
        
        #region Unity callbacks
        public virtual void Awake()
        {
            UsableControls = new Dictionary<string, IControl>();
            UsableForms = new Dictionary<string, BaseForm>();
            holdingspace = new GameObject(); holdingspace.name = "holdingspace";
            holdingspace.transform.SetParent(this.transform);
            if(gameObject.GetComponent<UnityEngine.Canvas>() == null)
            {
                var c = gameObject.GetComponentInChildren<UnityEngine.Canvas>(true);
                if(c == null)
                {                    
                    GameObject go = new GameObject("RuntimeCanvas");

                    UnityEngine.Canvas canvas = go.AddComponent<UnityEngine.Canvas>();
                    canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                    canvas.vertexColorAlwaysGammaSpace = true;
                    
                    var cs = go.AddComponent<CanvasScaler>();
                    cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                    cs.referenceResolution = new Vector2(1920, 1080);

                    go.AddComponent<GraphicRaycaster>();

                    canvas.transform.SetParent(this.transform, false);
                    c = go.GetComponent<UnityEngine.Canvas>();
                }
                canvas = c;
            }

            //reference gameObject to be the parent of all UI Prefabs that can be instantiated
            formInstances = new GameObject("formPrefabs");
            formInstances.AddComponent<RectTransform>();
            formInstances.transform.SetParent(this.transform);

            var ctl = Resources.Load<GameObject>("MUUI_Prefabs");
            var it = Instantiate(ctl, new Vector3(0, 0, 0), Quaternion.identity);            
            it.name = "controlPrefabs";

        }
        public virtual void Start()
        {
            
        }
        #endregion

        /// <summary>
        /// Creates an instance of the UI prefab specified.
        /// </summary>
        /// <param name="tname">The name of the UI prefab in the collection to instantiate.</param>
        /// <peram name="iname">The name of the instance of the UI Prefab created by this call</peram>
        public virtual void Instantiate(string tname, string iname) { }

        /// <summary>
        /// Reads the xml and optionally instantiates the UI Prefab immediately
        /// </summary>
        /// <param name="xml">xml text source to parse, as DataSet</param>
        /// <param name="label">the string literal to associate with this ui prefab for reference/lookup</param>
        /// <param name="defaultactive">defaults to false, when the xml is parsed should the UI prefab
        /// be loaded into the scene immediately?</param>
        public virtual void ReadUIPrefab(string xml, string label, bool defaultactive = false) { }

    }
}

