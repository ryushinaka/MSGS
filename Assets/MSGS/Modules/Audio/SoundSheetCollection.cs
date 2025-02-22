using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

namespace MiniScript.MSGS.Audio
{
    public static class SoundSheetCollection
    {
        static Dictionary<string, Album> AudioClips = new Dictionary<string, Album>();
        static object _locker = new object();

        public static ICollection<string> Keys { get { return AudioClips.Keys; } }

        public static ICollection<Album> Values { get { return AudioClips.Values; } }

        public static int Count { get { return AudioClips.Count; } }

        public static AudioClip Get(string name)
        {
            foreach(KeyValuePair<string, Album>kv in AudioClips)
            {
                if (kv.Value.Contains(name))
                {
                    return kv.Value.GetSound(name);
                }
            }

            return null;
        }

        public static AudioClip Get(string sheet, string name)
        {
            if(AudioClips.ContainsKey(sheet))
            {
                if (AudioClips[sheet].Contains(name)) { return AudioClips[sheet][name]; }
                else { return null; }
            }

            return null;
        }

        public static string Debug() 
        {
            int count = 0;
            foreach(KeyValuePair<string, Album> kv in AudioClips)
            {
                count += kv.Value.Count;
            }
            return "Sounds: " + AudioClips.Count + " sheets and " + count + " tracks total."; 
        }

        public static void Add(string key, Album value)
        {
            if(AudioClips.ContainsKey(key))
            {
                MiniScriptSingleton.LogError(
                    "Attempt to add SoundSheet[" + key + "], it already exists in the collection."
                    );
                return;
            }
            if(value == null)
            {
                MiniScriptSingleton.LogWarning(
                    "Attempted to assign null when adding SoundSheet[" + key + "] to the collection."
                    );
                return;
            }

            AudioClips.Add(key, value);
        }

        public static void Add(KeyValuePair<string, Album> item)
        {
            if (AudioClips.ContainsKey(item.Key))
            {
                MiniScriptSingleton.LogError(
                    "Attempt to add SoundSheet[" + item.Key + "], it already exists in the collection."
                    );
                return;
            }
            if (item.Value == null)
            {
                MiniScriptSingleton.LogWarning(
                    "Attempted to assign null when adding SoundSheet[" + item.Key + "] to the collection."
                    );
                return;
            }

            AudioClips.Add(item.Key, item.Value);
        }

        public static Album GetSoundSheet(string key)
        {
            if (ContainsKey(key)) return AudioClips[key];
            return null;
        }

        public static void Clear()
        {
            AudioClips.Clear();
        }

        public static bool Contains(KeyValuePair<string, Album> item)
        {
            if (AudioClips.ContainsKey(item.Key)) { return true; }
            else return false;
        }

        public static bool ContainsKey(string key)
        {
            return AudioClips.ContainsKey(key);
        }

        public static IEnumerator<KeyValuePair<string, Album>> GetEnumerator()
        {
            return AudioClips.GetEnumerator();
        }

        public static ValMap GetProperties()
        {
            throw new System.NotImplementedException();
        }

        public static bool Remove(string key)
        {
            return AudioClips.Remove(key);
        }

        public static bool Remove(KeyValuePair<string, Album> item)
        {
            return AudioClips.Remove(item.Key);
        }

        static bool IsSoundSheet(ref DataSet set)
        {
            if (set.Tables.Contains("Config"))
            {
                if (set.Tables["Config"].Columns.Contains("Type") && set.Tables["Config"].Columns.Contains("Name") && set.Tables["Config"].Rows.Count == 1)
                {
                    if (set.Tables["Config"].Rows[0]["Type"].ToString() == "SoundSheet") { return true; }
                }
            }
            return false;
        }
    }
}

