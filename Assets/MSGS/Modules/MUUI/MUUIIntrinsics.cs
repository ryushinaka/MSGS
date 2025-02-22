using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MiniScript;
using MiniScript.MSGS.MUUI.Extensions;

namespace MiniScript.MSGS.MUUI
{
    public static class MUUIIntrinsics
    {
        static List<GameObject> OberservedGameObjects;
        public static GameObject RootGameObject;

        static ValMap uiIntrinsics;
        
        static void InitializeUnityPrefabs()
        {
            
        }

        public static ValMap Get()
        {
            if(uiIntrinsics == null) { Initialize(); }
            return uiIntrinsics;
        }

        /// <summary>
        /// Allocate the Intrinsic methods for the Legacy UI components of Unity3D UI
        /// </summary>
        /// <remarks>Legacy UI in this context refers to the following component types:
        /// Button (provided by TextMeshPro)
        /// Dropdown (provided by TextMeshPro)
        /// InputField (provided by TextMeshPro)
        /// Image
        /// Panel
        /// RawImage
        /// Scrollbar
        /// ScrollView
        /// Slider
        /// TextField (provided by TextMeshPro)
        /// </remarks>
        public static void Initialize()
        {
            uiIntrinsics = new ValMap();
            OberservedGameObjects = new List<GameObject>();
            InitializeUnityPrefabs();

            var a = Intrinsic.Create("Initialize_MUUI");
            a.code = (context, partialResult) =>
            {
                return new Intrinsic.Result(null);

            };

            //get list of prefabs (available)
            //get list of prefabs (instanced, in the scene)


            //Element intrinsics
            //Prefab intrinsics           

            #region Prefab Intrinsics

            #endregion
            
            #region Unity Object Intrinsics

            #region Game Objects
            #endregion

            #region RectTransform
            a = Intrinsic.Create("RectTransform");

            a.AddParam("name");
            a.code = (context, partialResult) =>
            {
                var x = GetGameObject(context.GetVar("name").ToString());
                if (x != null)
                {

                    var c = x.GetComponent<RectTransform>();
                    ValMap map = c.ToValMap();
                    map.map.Add(new ValString("X"), new ValNumber(c.anchoredPosition.x));
                    map.map.Add(new ValString("Y"), new ValNumber(c.anchoredPosition.y));

                    return new Intrinsic.Result(map, true);
                }
                context.interpreter.errorOutput("RectTransform: The 'name' argument of (" +
                    context.GetVar("name").ToString() + ") was not found.");
                return new Intrinsic.Result(null, true);
            };
            #endregion

            #region Canvas Intrinsics
            #endregion

            #endregion
        }

        static GameObject GetGameObject(string name)
        {
            GameObject rst = null;
            for (int i = 0; i < OberservedGameObjects.Count; i++)
            {
                if (OberservedGameObjects[i].name == name)
                {
                    rst = OberservedGameObjects[i];
                    i = int.MaxValue;
                }
            }
            return rst;
        }
                
    }
}

