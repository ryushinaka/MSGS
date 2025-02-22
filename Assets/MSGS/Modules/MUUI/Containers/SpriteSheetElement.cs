using UnityEngine;

namespace MiniScript.MSGS.MUUI
{
    public class SpriteSheetElement
    {
        public string Label;
        public string UniqueID = System.Guid.NewGuid().ToString();
        public Sprite Sprite;
    }
}

