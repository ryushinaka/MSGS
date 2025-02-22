using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Data;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace MiniScript.MSGS.MUUI
{
    /// <summary>
    /// This object is for debugging purposes and should not be used in a production setting.
    /// </summary>
    public class SpriteSheetInstance : MonoBehaviour
    {
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.FilePath(AbsolutePath = true, Extensions = "", ParentFolder = "Assets/", RequireExistingPath = true, UseBackslashes = true)]
#endif
        public string FilePath;

        public SpriteSheet Sprites;

        // Start is called before the first frame update
        void Start()
        {
            Sprites = new SpriteSheet();
        }

        // Update is called once per frame
        void Update()
        {

        }


#if ODIN_INSPECTOR
        [Button]
#endif
        public void LoadSpriteSheet()
        {
            Sprites = new SpriteSheet();
            Stream strm = File.OpenRead(FilePath);
            DataSet set = new DataSet();
            set.ReadXml(strm, XmlReadMode.ReadSchema);
            Sprites = Sprites.LoadSpriteSheet(ref set);

            Debug.Log(Sprites.Name + " " + Sprites.Count);
        }
    }
}
