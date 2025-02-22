using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;
using Sirenix.OdinInspector;

namespace MiniScript.MSGS.Audio
{
    public class Album
    {
        public string Name = string.Empty;

        public List<Tuple<string, Guid, AudioClip, List<SoundSheetElementPositions>>> tuples = new List<Tuple<string, Guid, AudioClip, List<SoundSheetElementPositions>>>();

        private static int nextId = 0;
        public int InstanceId { get; private set; }

        public bool Contains(string label)
        {
            for (int i = 0; i < tuples.Count; i++)
            {
                if (tuples[i].Item1 == label) return true;
            }
            return false;
        }
        public bool Contains(Guid id)
        {
            for (int i = 0; i < tuples.Count; i++)
            {
                if (tuples[i].Item2 == id) return true;
            }
            return false;
        }

        public AudioClip GetSound(Guid instanceid)
        {
            for (int i = 0; i < tuples.Count; i++)
            {
                if (tuples[i].Item2 == instanceid)
                {
                    return tuples[i].Item3;
                }
            }

            return null;
        }
        public AudioClip GetSound(string label)
        {
            for (int i = 0; i < tuples.Count; i++)
            {
                if (tuples[i].Item1 == label)
                {
                    return tuples[i].Item3;
                }
            }

            return null;
        }

        public int Count { get { return tuples.Count; } }

        public void AddSound(string label, Guid id, AudioClip sp)
        {
            if (!Contains(label) && !Contains(id) && sp != null)
            {
                tuples.Add(Tuple.Create<string, Guid, AudioClip, List<SoundSheetElementPositions>>(label, id, sp, new List<SoundSheetElementPositions>()));
                Debug.Log("added " + label + " " + id);
                return;
            }
            else
            {
                Debug.Log("rejected " + Contains(label) + " " +
                    Contains(id) + " " + (sp != null));
                //Debug.Log("rejected " + label + " " + id);
            }

            Debug.Log(tuples.Count);
        }

        public void AddSound(string label, Guid id, byte[] bytes)
        {

            if (!Contains(label) && !Contains(id) && bytes != null)
            {
                tuples.Add(Tuple.Create<string, Guid, AudioClip, List<SoundSheetElementPositions>>(label, id, WaveUtility.ToAudioClip(bytes, 0, label), new List<SoundSheetElementPositions>()));
                Debug.Log("added " + label + " " + id);
            }
            else
            {
                Debug.Log("rejected " + Contains(label) + " " +
                    Contains(id) + " " + (bytes != null));
                //Debug.Log("rejected " + label + " " + id);
            }

            Debug.Log(tuples.Count);
        }

        public void Create(string label, byte[] bytes)
        {
            if (!Contains(label))
            {
                if(tuples == null) { tuples = new List<Tuple<string, Guid, AudioClip, List<SoundSheetElementPositions>>>(); }
                tuples.Add(Tuple.Create<string, Guid, AudioClip, List<SoundSheetElementPositions>>(
                label,
                System.Guid.NewGuid(),
                WaveUtility.ToAudioClip(bytes, 0, label),
                new List<SoundSheetElementPositions>()
                    ));
            }
        }

        public void RemoveSound(string label)
        {
            for (int i = 0; i < tuples.Count; i++)
            {
                if (tuples[i].Item1 == label)
                {
                    tuples.RemoveAt(i);
                    break;
                }
            }
        }
        public void RemoveSound(Guid id)
        {
            for (int i = 0; i < tuples.Count; i++)
            {
                if (tuples[i].Item2 == id)
                {
                    tuples.RemoveAt(i);
                    break;
                }
            }
        }

        public AudioClip this[int index]
        {
            get
            {
                if (index >= 0 && tuples.Count >= 0 &&
                    index <= tuples.Count)
                {
                    return tuples[index].Item3;
                }
                return null;
            }
        }

        public AudioClip this[string name]
        {
            get
            {
                for (int i = 0; i < tuples.Count; i++)
                {
                    if (tuples[i].Item1 == name) return tuples[i].Item3;
                }
                return null;
            }
        }

        public ValMap GetProperties()
        {
            ValMap map = new ValMap();
            //foreach(string s in Sprites.Keys)
            //{
            //    map.map.Add(new ValString(s), null);
            //}

            return map;
        }

        public Album()
        {
            InstanceId = System.Threading.Interlocked.Increment(ref nextId);
        }
    }
}
