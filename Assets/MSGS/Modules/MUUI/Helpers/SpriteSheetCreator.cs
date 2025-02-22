using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif


namespace MiniScript.MSGS.MUUI
{
    public class SpriteSheetCreator : SerializedMonoBehaviour
    {
        public string Filename, AtlasName;

        public List<SpriteSheetElement> SpriteSheet;

        private void Awake()
        {
            if (SpriteSheet == null) SpriteSheet = new List<SpriteSheetElement>();
        }

        void Start()
        {
            
        }

        void Update()
        {

        }

#if ODIN_INSPECTOR
        [Button]
#endif
        void Add()
        {
            if (SpriteSheet == null) { SpriteSheet = new List<SpriteSheetElement>(); }
            SpriteSheet.Add(new SpriteSheetElement());
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        void WriteSpriteSheet()
        {
            SpriteSheet atlas = new SpriteSheet();
            atlas.Name = AtlasName;
            foreach (SpriteSheetElement kv in SpriteSheet)
            {
                //atlas.AddSprite(kv.Label, kv.UniqueID, kv.Sprite);
                atlas.tuples.Add(Tuple.Create<string, string, Sprite>(kv.Label, kv.UniqueID, kv.Sprite));
            }
            
            atlas.SaveSpriteSheet(Application.streamingAssetsPath + "/" + Filename + ".xml");
            //Debug.Log("SpriteAtlas written to " + Application.streamingAssetsPath + "/" + Filename + ".xml (" + SpriteSheet.Count + ")");
        }
    }
}

