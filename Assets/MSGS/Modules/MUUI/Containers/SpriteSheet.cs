using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;

namespace MiniScript.MSGS.MUUI
{
    public class SpriteSheet
    {
        public string Name = string.Empty;
        //GUID, Label, Sprite
        public List<Tuple<string, string, Sprite>> tuples = new List<Tuple<string, string, Sprite>>();

        public bool Contains(string label)
        {
            for (int i = 0; i < tuples.Count; i++)
            {
                if (tuples[i].Item1 == label) return true;
            }
            return false;
        }
        
        public Sprite GetSprite(string label)
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

        public Sprite this[string label]
        {
            get
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
        }
       
        public int Count { get { return tuples.Count; } }

        public void AddSprite(string label, Sprite sp)
        {
            if (!Contains(label) && sp != null)
            {
                tuples.Add(Tuple.Create<string, string, Sprite>(label, Guid.NewGuid().ToString(), sp));
            }
            else if(Contains(label))
            {
                MiniScriptSingleton.LogError(
                    "Attempted to add duplicate label '" + label + "' to SpriteSheet[" + Name + "]"
                    );
            }
        }

        public void RemoveSprite(string label)
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
      
        public ValMap GetProperties()
        {
            ValMap map = new ValMap();
            //foreach(string s in Sprites.Keys)
            //{
            //    map.map.Add(new ValString(s), null);
            //}

            return map;
        }
    }
}

