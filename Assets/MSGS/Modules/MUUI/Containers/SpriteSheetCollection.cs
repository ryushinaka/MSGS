using System;
using System.Collections.Generic;
using System.Data;

namespace MiniScript.MSGS.MUUI
{
    public static class SpriteSheetCollection
    {
        static Dictionary<string, SpriteSheet> sprites = new Dictionary<string, SpriteSheet>();
        static object _locker = new object();

        public static ICollection<string> Keys { get { return sprites.Keys; } }

        public static ICollection<SpriteSheet> Values { get { return sprites.Values; } }

        public static int Count { get { return sprites.Count; } }

        public static void Add(string key, SpriteSheet value)
        {
            if(key.Length > 0)
            {
                if (!sprites.ContainsKey(key))
                {
                    sprites.Add(key, value);
                }
                else
                {
                    MiniScriptSingleton.LogError(
                        "Attempted to add duplicate label[" + key + "]" + 
                        " to SpriteSheetCollection."
                        );
                }
            }
        }

        public static void Add(KeyValuePair<string, SpriteSheet> item)
        {
            if (item.Key.Length > 0)
            {
                if (!sprites.ContainsKey(item.Key))
                {
                    sprites.Add(item.Key, item.Value);
                }
                else
                {
                    MiniScriptSingleton.LogError(
                        "Attempted to add duplicate label[" + item.Key + "]" +
                        " to SpriteSheetCollection."
                        );
                }
            }
        }

        public static void Clear()
        {
            sprites.Clear();
        }

        public static SpriteSheet Get(string str)
        {
            if (sprites.ContainsKey(str)) { return sprites[str]; }
            else { return null; }
        }

        public static bool Contains(KeyValuePair<string, SpriteSheet> item)
        {
            if (sprites.ContainsKey(item.Key)) { return true; }
            else return false;
        }

        public static bool ContainsKey(string key)
        {
            return sprites.ContainsKey(key);
        }

        public static IEnumerator<KeyValuePair<string, SpriteSheet>> GetEnumerator()
        {
            return sprites.GetEnumerator();
        }

        public static ValMap GetProperties()
        {
            throw new System.NotImplementedException();
        }

        public static bool Remove(string key)
        {
            return sprites.Remove(key);
        }

        public static bool Remove(KeyValuePair<string, SpriteSheet> item)
        {
            return sprites.Remove(item.Key);
        }

        static bool IsSpriteSheet(ref DataSet set)
        {
            if (set.Tables.Contains("Config"))
            {
                if (set.Tables["Config"].Columns.Contains("Type") && set.Tables["Config"].Columns.Contains("Name") && set.Tables["Config"].Rows.Count == 1)
                {
                    if (set.Tables["Config"].Rows[0]["Type"].ToString() == "spritesheet") { return true; }
                }
            }
            return false;
        }

        /// <summary>
        /// Exposes the SpriteSheetCollection contents to the scripting system
        /// </summary>
        /// <returns></returns>
        public static ValMap Properties()
        {
            ValMap map = new ValMap();


            return map;
        }

        public static string Debug()
        {
            int cols = 0, sc = 0;

            foreach (KeyValuePair<string, SpriteSheet> kv in sprites)
            {
                cols++;
                sc += kv.Value.Count;
            }

            return "Sprites: (Sheets/" + cols + ")(Sprites/" + sc + ") ";
        }

        static SpriteSheetCollection()
        {
            //default generic container
            sprites.Add("sprites", new SpriteSheet());
        }
    }
}
