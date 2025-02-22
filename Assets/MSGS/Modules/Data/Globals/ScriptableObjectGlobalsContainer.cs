using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniScript.MSGS.ScriptableObjects;

namespace MiniScript.MSGS.Data.Globals
{
    /// <summary>
    /// This implements a fast lookup feature for the Label property of MiniScriptScriptableObjects.
    /// </summary>
    /// <remarks>The concept for this comes from an old post by a CCP developer (the makers of Eve Online,
    /// not the Chinese Communist Party).  In this post the developer mentions that they made a significant performance
    /// gain on pathfinding for plotting a route from A to B solar systems.  This was achieved by using a hashtable
    /// embedded within a hashtable to an unspecified depth.  Each hashtable stores 1 character/letter, with the topmost
    /// hashtable being the first letter/character.  This implementation will go to a depth of 16.  The performance gain
    /// comes from the hashtable Value containing a shorter list of matching values than the full quantity of the original list,
    /// increased depth makes the list that much shorter.
    /// Insertion of new data at runtime will be much slower than just reading out a value. This may be an issue depending on your scripting.
    /// </remarks>    
    public class ScriptableObjectContainer
    {
        List<KeyValuePair<string, MiniScriptScriptableObject>> pairs = new List<KeyValuePair<string, MiniScriptScriptableObject>>();

        public class Root
        {
            public Dictionary<Char, IndexElement> pairs = new Dictionary<char, IndexElement>();

            public IndexElement this[Char key]
            {
                get
                {
                    if (pairs.ContainsKey(key)) { return pairs[key]; }
                    else { return null; }
                }
                set { return; }
            }

            public bool ContainsKey(Char key)
            {
                if (pairs.ContainsKey(key)) { return true; }
                else { return false; }
            }
        }
        public class IndexElement
        {
            public Char key = Char.MinValue;
            public List<IndexElement> elements = new List<IndexElement>();
            public List<MiniScriptScriptableObject> scriptableObjects = new List<MiniScriptScriptableObject>();

            public bool ContainsKey(Char key)
            {
                foreach (IndexElement ie in elements)
                {
                    if (ie.key == key) { return true; }
                }

                return false;
            }

            public IndexElement this[Char key]
            {
                get
                {
                    foreach (IndexElement ie in elements)
                    {
                        if (ie.key == key) { return ie; }
                    }

                    return null;
                }
                set { return; }
            }

            public IndexElement(Char kkey)
            {
                key = kkey;
            }
            public IndexElement() { }
        }

        Root barf;

        public List<string> AvailableTypes;

        public bool canCreateInstance(string name)
        {
            if (AvailableTypes.Contains(name)) return true;

            return false;
        }

        public bool ContainsName(string name)
        {
            foreach (KeyValuePair<string, MiniScriptScriptableObject> kv in pairs)
            {
                if (kv.Key == name) { return true; }
            }

            return false;
        }

        public bool Add(MiniScriptScriptableObject mso)
        {
            //disallow unlabeled SO's from being in the container
            if (mso.Label.Length == 0) { return false; }
            if (!ContainsName(mso.Label))
            {
                pairs.Add(new KeyValuePair<string, MiniScriptScriptableObject>(mso.Label, mso));
                return true;
            }

            return false;

            #region
            //int depth = 0;
            //var arr = mso.Label.ToCharArray();
            //bool didAdd = false;

            //for (int i = 0; i < arr.Length; i++)
            //{
            //    Char c = arr[i];
            //    #region 
            //    if (depth == 0)
            //    {
            //        if (barf.ContainsKey(c)) { depth++; }
            //        else { barf.pairs.Add(c, new IndexElement()); depth++; }
            //        if (i == arr.Length && !barf[arr[0]].scriptableObjects.ContainsMSO(mso))
            //        {
            //            barf[arr[0]].scriptableObjects.Add(mso);
            //            didAdd = true;
            //        }
            //    }
            //    else if (depth == 1)
            //    {
            //        if (barf[arr[0]].ContainsKey(arr[1])) { depth++; }
            //        else
            //        {
            //            IndexElement ie = new IndexElement();
            //            ie.key = c;
            //            barf.pairs[arr[0]].elements.Add(ie);
            //        }
            //        if (i == arr.Length && !barf[arr[0]][arr[1]].scriptableObjects.ContainsMSO(mso))
            //        {
            //            barf[arr[0]][arr[1]].scriptableObjects.Add(mso);
            //            didAdd = true;
            //        }
            //    }
            //    else if (depth == 2)
            //    {

            //        if (barf[arr[0]][arr[1]].ContainsKey(arr[2])) { depth++; }
            //        else
            //        {
            //            IndexElement ie = new IndexElement();
            //            ie.key = c;
            //            barf[arr[0]][arr[1]].elements.Add(ie);                        
            //        }
            //        if (i == arr.Length && !barf[arr[0]][arr[1]][arr[2]].scriptableObjects.ContainsMSO(mso))
            //        {
            //            barf[arr[0]][arr[1]][arr[2]].scriptableObjects.Add(mso);
            //            didAdd = true;
            //        }
            //    }
            //    else if (depth == 3)
            //    {
            //        if (barf[arr[0]][arr[1]][arr[2]].ContainsKey(arr[3])) { depth++; }
            //        else
            //        {
            //            IndexElement ie = new IndexElement();
            //            ie.key = c;
            //            barf[arr[0]][arr[1]][arr[2]].elements.Add(ie);
            //        }
            //        if (i == arr.Length && !barf[arr[0]][arr[1]][arr[2]][arr[3]].scriptableObjects.ContainsMSO(mso))
            //        {
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]].scriptableObjects.Add(mso);
            //            didAdd = true;
            //        }
            //    }
            //    else if (depth == 4)
            //    {
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]].ContainsKey(arr[4])) { depth++; }
            //        else
            //        {
            //            IndexElement ie = new IndexElement();
            //            ie.key = c;
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]].elements.Add(ie);
            //        }
            //        if (i == arr.Length && !barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]].scriptableObjects.ContainsMSO(mso))
            //        {
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]].scriptableObjects.Add(mso);
            //            didAdd = true;
            //        }
            //    }
            //    else if (depth == 5)
            //    {
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]].ContainsKey(arr[5])) { depth++; }
            //        else
            //        {
            //            IndexElement ie = new IndexElement();
            //            ie.key = c;
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]].elements.Add(ie);
            //        }
            //        if (i == arr.Length && !barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]].scriptableObjects.ContainsMSO(mso))
            //        {
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]].scriptableObjects.Add(mso);
            //            didAdd = true;
            //        }
            //    }
            //    else if (depth == 6)
            //    {
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]].ContainsKey(arr[6])) { depth++; }
            //        else
            //        {
            //            IndexElement ie = new IndexElement();
            //            ie.key = c;
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]].elements.Add(ie);
            //        }
            //        if (i == arr.Length && !barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]].scriptableObjects.ContainsMSO(mso))
            //        {
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]].scriptableObjects.Add(mso);
            //            didAdd = true;
            //        }
            //    }
            //    else if (depth == 7)
            //    {
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]].ContainsKey(arr[7])) { depth++; }
            //        else
            //        {
            //            IndexElement ie = new IndexElement();
            //            ie.key = c;
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]].elements.Add(ie);
            //        }
            //        if (i == arr.Length && !barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]].scriptableObjects.ContainsMSO(mso))
            //        {
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]].scriptableObjects.Add(mso);
            //            didAdd = true;
            //        }
            //    }
            //    else if (depth == 8)
            //    {
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]].ContainsKey(arr[8])) { depth++; }
            //        else
            //        {
            //            IndexElement ie = new IndexElement();
            //            ie.key = c;
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]].elements.Add(ie);
            //        }
            //        if (i == arr.Length && !barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]].scriptableObjects.ContainsMSO(mso))
            //        {
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]].scriptableObjects.Add(mso);
            //            didAdd = true;
            //        }
            //    }
            //    else if (depth == 9)
            //    {
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]].ContainsKey(arr[9])) { depth++; }
            //        else
            //        {
            //            IndexElement ie = new IndexElement();
            //            ie.key = c;
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]].elements.Add(ie);
            //        }
            //        if (i == arr.Length && !barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]].scriptableObjects.ContainsMSO(mso))
            //        {
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]].scriptableObjects.Add(mso);
            //            didAdd = true;
            //        }
            //    }
            //    else if (depth == 10)
            //    {
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]].ContainsKey(arr[10])) { depth++; }
            //        else
            //        {
            //            IndexElement ie = new IndexElement();
            //            ie.key = c;
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]].elements.Add(ie);
            //        }
            //        if (i == arr.Length && !barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]].scriptableObjects.ContainsMSO(mso))
            //        {
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]].scriptableObjects.Add(mso);
            //            didAdd = true;
            //        }
            //    }
            //    else if (depth == 11)
            //    {
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]].ContainsKey(arr[11])) { depth++; }
            //        else
            //        {
            //            IndexElement ie = new IndexElement();
            //            ie.key = c;
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]].elements.Add(ie);
            //        }
            //        if (i == arr.Length && !barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]].scriptableObjects.ContainsMSO(mso))
            //        {
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]].scriptableObjects.Add(mso);
            //            didAdd = true;
            //        }
            //    }
            //    else if (depth == 12)
            //    {
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]].ContainsKey(arr[12])) { depth++; }
            //        else
            //        {
            //            IndexElement ie = new IndexElement();
            //            ie.key = c;
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]].elements.Add(ie);
            //        }
            //        if (i == arr.Length && !barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]].scriptableObjects.ContainsMSO(mso))
            //        {
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]].scriptableObjects.Add(mso);
            //            didAdd = true;
            //        }
            //    }
            //    else if (depth == 13)
            //    {
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]].ContainsKey(arr[13])) { depth++; }
            //        else
            //        {
            //            IndexElement ie = new IndexElement();
            //            ie.key = c;
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]].elements.Add(ie);
            //        }
            //        if (i == arr.Length && !barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]][arr[13]].scriptableObjects.ContainsMSO(mso))
            //        {
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]][arr[13]].scriptableObjects.Add(mso);
            //            didAdd = true;
            //        }
            //    }
            //    else if (depth == 14)
            //    {
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]][arr[13]].ContainsKey(arr[14])) { depth++; }
            //        else
            //        {
            //            IndexElement ie = new IndexElement();
            //            ie.key = c;
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]][arr[13]].elements.Add(ie);
            //        }
            //        if (i == arr.Length && !barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]][arr[13]][arr[14]].scriptableObjects.ContainsMSO(mso))
            //        {
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]][arr[13]][arr[14]].scriptableObjects.Add(mso);
            //            didAdd = true;
            //        }
            //    }
            //    else if (depth == 15)
            //    {
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]][arr[13]][arr[14]].ContainsKey(arr[15])) { depth++; }
            //        else
            //        {
            //            IndexElement ie = new IndexElement();
            //            ie.key = c;
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]][arr[13]][arr[14]].elements.Add(ie);
            //        }
            //        if (i == arr.Length && !barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]][arr[13]][arr[14]][arr[15]].scriptableObjects.ContainsMSO(mso))
            //        {
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]][arr[13]][arr[14]][arr[15]].scriptableObjects.Add(mso);
            //            didAdd = true;
            //        }
            //    }
            //    else if (depth == 16)
            //    {
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]][arr[13]][arr[14]][arr[15]].ContainsKey(arr[16])) { depth++; }
            //        else
            //        {
            //            IndexElement ie = new IndexElement();
            //            ie.key = c;
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]][arr[13]][arr[14]][arr[15]].elements.Add(ie);
            //        }
            //        if (i == arr.Length && !barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]][arr[13]][arr[14]][arr[15]][arr[16]].scriptableObjects.ContainsMSO(mso))
            //        {
            //            barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]][arr[13]][arr[14]][arr[15]][arr[16]].scriptableObjects.Add(mso);
            //            didAdd = true;
            //        }
            //    }
            //    else { }
            //    #endregion

            //    Debug.Log("depth: " + depth + " key:" + c);
            //}

            //return didAdd;
            #endregion
        }

        public void Remove(MiniScriptScriptableObject mso)
        {   //since unlabeled SO's are disallowed, neither can they be removed
            if (mso.Label.Length == 0) { return; }

            for (int i = 0; i < pairs.Count; i++)
            {
                if (pairs[i].Key == mso.Label) { pairs.RemoveAt(i); return; }
            }
        }

        public void Remove(string label)
        {
            //since unlabeled SO's are disallowed, neither can they be removed
            if (label.Length == 0) { return; }

            for (int i = 0; i < pairs.Count; i++)
            {
                if (pairs[i].Key == label) { pairs.RemoveAt(i); return; }
            }
        }

        public MiniScriptScriptableObject Get(string label)
        {
            var arr = label.ToCharArray();
            
            for (int i = 0; i < pairs.Count; i++)
            {
                if (pairs[i].Key == label) { return pairs[i].Value; }
            }

            #region 
            //if (arr.Length == 0)
            //{   //Adding SO's without labels is disallowed, so retrieving them is also disallowed
            //    return null;
            //}
            //else if (arr.Length == 1) //if the SO's label is only 1 character in length, we just check the top level of the "tree"
            //{   //see if the character is indexed/hashed
            //    if (barf.ContainsKey(arr[0]))
            //    {   //try to retrieve it if it is, and return its reference
            //        if (barf[arr[0]].scriptableObjects.TryGet(label, out result)) { return result; }
            //    }
            //}
            //else if (arr.Length == 2)
            //{
            //    if (barf[arr[0]].ContainsKey(arr[1]))
            //    {   //try to retrieve it if it is, and return its reference
            //        if (barf[arr[0]].scriptableObjects.TryGet(label, out result)) { return result; }
            //    }
            //}
            //else if (arr.Length == 3)
            //{
            //    if (barf[arr[0]][arr[1]].ContainsKey(arr[2]))
            //    {   //try to retrieve it if it is, and return its reference
            //        if (barf[arr[0]][arr[1]].scriptableObjects.TryGet(label, out result)) { return result; }
            //    }
            //}
            //else if (arr.Length == 4)
            //{
            //    if (barf[arr[0]][arr[1]][arr[2]].ContainsKey(arr[3]))
            //    {   //try to retrieve it if it is, and return its reference
            //        if (barf[arr[0]][arr[1]][arr[2]].scriptableObjects.TryGet(label, out result)) { return result; }
            //    }
            //}
            //else if (arr.Length == 5)
            //{
            //    if (barf[arr[0]][arr[1]][arr[2]][arr[3]].ContainsKey(arr[4]))
            //    {   //try to retrieve it if it is, and return its reference
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]].scriptableObjects.TryGet(label, out result)) { return result; }
            //    }
            //}
            //else if (arr.Length == 6)
            //{
            //    if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]].ContainsKey(arr[5]))
            //    {   //try to retrieve it if it is, and return its reference
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]].scriptableObjects.TryGet(label, out result)) { return result; }
            //    }
            //}
            //else if (arr.Length == 7)
            //{
            //    if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]].ContainsKey(arr[6]))
            //    {   //try to retrieve it if it is, and return its reference
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]].scriptableObjects.TryGet(label, out result)) { return result; }
            //    }
            //}
            //else if (arr.Length == 8)
            //{
            //    if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]].ContainsKey(arr[7]))
            //    {   //try to retrieve it if it is, and return its reference
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]].scriptableObjects.TryGet(label, out result)) { return result; }
            //    }
            //}
            //else if (arr.Length == 9)
            //{
            //    if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]].ContainsKey(arr[8]))
            //    {   //try to retrieve it if it is, and return its reference
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]].scriptableObjects.TryGet(label, out result)) { return result; }
            //    }
            //}
            //else if (arr.Length == 10)
            //{
            //    if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]].ContainsKey(arr[9]))
            //    {   //try to retrieve it if it is, and return its reference
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]].scriptableObjects.TryGet(label, out result)) { return result; }
            //    }
            //}
            //else if (arr.Length == 11)
            //{
            //    if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]].ContainsKey(arr[10]))
            //    {   //try to retrieve it if it is, and return its reference
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]].scriptableObjects.TryGet(label, out result)) { return result; }
            //    }
            //}
            //else if (arr.Length == 12)
            //{
            //    if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]].ContainsKey(arr[11]))
            //    {   //try to retrieve it if it is, and return its reference
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]].scriptableObjects.TryGet(label, out result)) { return result; }
            //    }
            //}
            //else if (arr.Length == 13)
            //{
            //    if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]].ContainsKey(arr[12]))
            //    {   //try to retrieve it if it is, and return its reference
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]].scriptableObjects.TryGet(label, out result)) { return result; }
            //    }
            //}
            //else if (arr.Length == 14)
            //{
            //    if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]].ContainsKey(arr[13]))
            //    {   //try to retrieve it if it is, and return its reference
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]].scriptableObjects.TryGet(label, out result)) { return result; }
            //    }
            //}
            //else if (arr.Length == 15)
            //{
            //    if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]][arr[13]].ContainsKey(arr[14]))
            //    {   //try to retrieve it if it is, and return its reference
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]][arr[13]].scriptableObjects.TryGet(label, out result)) { return result; }
            //    }
            //}
            //else if (arr.Length == 16)
            //{
            //    if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]][arr[13]][arr[14]].ContainsKey(arr[15]))
            //    {   //try to retrieve it if it is, and return its reference
            //        if (barf[arr[0]][arr[1]][arr[2]][arr[3]][arr[4]][arr[5]][arr[6]][arr[7]][arr[8]][arr[9]][arr[10]][arr[11]][arr[12]][arr[13]][arr[14]].scriptableObjects.TryGet(label, out result)) { return result; }
            //    }
            //}
            //else { }
            #endregion

            return null;
        }

        public bool TryGet(string label, out MiniScriptScriptableObject mso)
        {
            mso = Get(label);
            if (mso != null) return true;

            return false;
        }

        /// <summary>
        /// Appends the contents of the container to the ValMap given
        /// </summary>
        /// <param name="globals">The ValMap to append to</param>
        /// <remarks>This allows the </remarks>
        public void SafeMerge(ref ValMap globals)
        {
            throw new NotImplementedException("SafeMerge");
        }

        public ScriptableObjectContainer()
        {
            AvailableTypes = new List<string>();
            //populate the AvailableTypes list with all BaseList and BaseDictionary inherited classes
            var lst = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(type => type.IsSubclassOf(typeof(BaseList)));
            foreach(Type t in lst)
            {
                if (!AvailableTypes.Contains(t.Name)) { AvailableTypes.Add(t.Name); }
            }
            var dict = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(type => type.IsSubclassOf(typeof(BaseList)));
            foreach (Type t in dict)
            {
                if (!AvailableTypes.Contains(t.Name)) { AvailableTypes.Add(t.Name); }                
            }
        }
    }

}
