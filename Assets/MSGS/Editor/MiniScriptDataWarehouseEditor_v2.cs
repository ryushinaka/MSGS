using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using MiniScript.MSGS.ScriptableObjects;


namespace MiniScript.MSGS.Editor
{
    public class MiniScriptDataWarehouseEditor_v2 : OdinMenuEditorWindow
    {
        static Type[] types;
        static List<int> instanceids;

        [MenuItem("MiniScript/DataWarehouse Editor_2")]
        private static void OpenWindow()
        {
            GetWindow<MiniScriptDataWarehouseEditor_v2>().Show();
        }

        protected override void OnGUI()
        {
            if (GUILayout.Button("Export to documentation file"))
            {
                string path = Application.dataPath + "/datagraph.txt";
                //MiniScriptDataWarehouse.WriteGraphToFile(path);

                Debug.Log("DataWarehouse graph structure exported to: " + path);
            }
            GUILayout.Space(10);

            base.OnGUI();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            var result = new OdinMenuTree();
            instanceids = new List<int>();
            string[] arr = AssetDatabase.FindAssets("t:MiniScriptScriptableObject", new string[] { "Assets/MiniScriptDataTypes" });
            List<MiniScriptScriptableObject> list = new List<MiniScriptScriptableObject>();
            
            foreach (string t in arr)
            {
                list.Add(AssetDatabase.LoadAssetAtPath<MiniScriptScriptableObject>(AssetDatabase.GUIDToAssetPath(t)));
            }

            //now we walk the list and generate a OdinMenuTree from the list
            foreach (MiniScriptScriptableObject omi in list)
            {                
                //if (omi.ScriptableObjectType == MiniScriptScriptableObjectType.EmbeddedValue)
                //{
                    
                //}
            }

            return result;
        }

        void AddChildren(ref OdinMenuItem item, ref MiniScriptScriptableObject mso)
        {
            //IDatagraphProperties p = (IDatagraphProperties)mso;
            //if (mso.IsList() && p.HasChildren())
            //{
            //    //add the element to the menu, then add the children
            //    var item2 = new OdinMenuItem(item.MenuTree, mso.name, mso); instanceids.Add(mso.GetInstanceID());
            //    item.ChildMenuItems.Add(item2);
            //    Debug.Log("list: " + item2.GetFullPath());
            //    //iterate the list for which elements have children to populate the menu correctly
            //    //worlds best comment:  if you add additional List types derived from MSO, make sure this is updated

            //    if (mso is ValListDictNMSScriptable)
            //    {
            //        var vldnms = (ValListDictNMSScriptable)mso;
            //        foreach (ValMapNMSScriptable v in vldnms.Values)
            //        {
            //            var abc = (MiniScriptScriptableObject)v;
            //            if (v.HasChildren() && !instanceids.Contains(abc.GetInstanceID()))
            //            {
            //                Debug.Log("AddChild1: " + abc.GetInstanceID() + " " + item2.GetFullPath());
            //                instanceids.Add(abc.GetInstanceID()); AddChildren(ref item2, ref abc);
            //            }
            //        }
            //    }
            //    else if (mso is ValListDictSMSScriptable)
            //    {
            //        var vldsms = (ValListDictSMSScriptable)mso;
            //        foreach (ValMapSMSScriptable v in vldsms.Values)
            //        {
            //            var abc = (MiniScriptScriptableObject)v;
            //            if (v.HasChildren() && !instanceids.Contains(abc.GetInstanceID()))
            //            {
            //                Debug.Log("AddChild2: " + abc.GetInstanceID() + " " + item2.GetFullPath());
            //                instanceids.Add(abc.GetInstanceID()); AddChildren(ref item2, ref abc);
            //            }
            //        }
            //    }
            //    else if (mso is ValListMSScriptable)
            //    {
            //        var vlms = (ValListMSScriptable)mso;
            //        foreach (MiniScriptScriptableObject v in vlms.Values)
            //        {
            //            var abc = (IDatagraphProperties)v;
            //            var vv = v; //to satisfy C# for syntax rules
            //            if (abc.HasChildren() && !instanceids.Contains(vv.GetInstanceID()))
            //            {
            //                Debug.Log("AddChild3: " + vv.GetInstanceID() + " " + item2.GetFullPath());
            //                instanceids.Add(v.GetInstanceID()); AddChildren(ref item2, ref vv);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        Debug.Log("How?");
            //    }
            //}
            //else if (mso.IsDictionary() && p.HasChildren())
            //{
            //    var item2 = new OdinMenuItem(item.MenuTree, mso.name, mso); instanceids.Add(mso.GetInstanceID());
            //    item.ChildMenuItems.Add(item2);
            //    Debug.Log("??: " + item.ChildMenuItems[item.ChildMenuItems.Count].GetFullPath());

            //    if (mso is ValMapNLMSScriptable)
            //    {
            //        var vlmnlms = (ValMapNLMSScriptable)mso;
            //        //foreach keyvaluepair in the dictionary, add a childmenuitem
            //        foreach (KeyValuePair<ValNumber, ValListMSScriptable> kv in vlmnlms.Values)
            //        {
            //            var vl = kv.Value; //get the List in the .Value

            //            foreach (MiniScriptScriptableObject abc in vl.Values)
            //            {   //iterate over the list, ensuring all children are added
            //                if (abc is IDatagraphProperties)
            //                {
            //                    instanceids.Add(abc.GetInstanceID());
            //                    var item3 = new OdinMenuItem(item2.MenuTree, abc.name, abc);
            //                    item.ChildMenuItems.Add(new OdinMenuItem(item2.MenuTree, abc.name, abc));

            //                    if (((IDatagraphProperties)abc).HasChildren() && !instanceids.Contains(vl.GetInstanceID()))
            //                    {
            //                        MiniScriptScriptableObject abcc = abc;
            //                        Debug.Log("AddChild4: " + abc.GetInstanceID() + " " + item3.GetFullPath());
            //                        AddChildren(ref item3, ref abcc);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    else if (mso is ValMapSLMSScriptable)
            //    {
            //        var vlmnlms = (ValMapSLMSScriptable)mso;
            //        //foreach keyvaluepair in the dictionary, add a childmenuitem
            //        foreach (KeyValuePair<ValStringScriptable, ValListMSScriptable> kv in vlmnlms.Values)
            //        {
            //            var vl = kv.Value; //get the List in the .Value

            //            foreach (MiniScriptScriptableObject abc in vl.Values)
            //            {   //iterate over the list, ensuring all children are added
            //                if (abc is IDatagraphProperties)
            //                {
            //                    instanceids.Add(abc.GetInstanceID());
            //                    //add the item to the odinmenutree
            //                    var itemA = new OdinMenuItem(item2.MenuTree, abc.name, abc);
            //                    item2.ChildMenuItems.Add(itemA);

            //                    if (((IDatagraphProperties)abc).HasChildren() && !instanceids.Contains(vl.GetInstanceID()))
            //                    {
            //                        MiniScriptScriptableObject abcc = abc;
            //                        Debug.Log("AddChild5: " + abcc.GetInstanceID() + " " + itemA.GetFullPath());
            //                        AddChildren(ref itemA, ref abcc);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    else if (mso is ValMapSMSScriptable)
            //    {
            //        var vlmnlms = (ValMapSMSScriptable)mso;
            //        //foreach keyvaluepair in the dictionary, add a childmenuitem
            //        foreach (KeyValuePair<ValStringScriptable, MiniScriptScriptableObject> kv in vlmnlms.Values)
            //        {
            //            var vl = kv.Value; //get the List in the .Value

            //            instanceids.Add(vl.GetInstanceID());
            //            //add the item to the odinmenutree
            //            var itemA = new OdinMenuItem(item2.MenuTree, vl.name, vl);

            //            if (((IDatagraphProperties)vl).HasChildren() && !instanceids.Contains(vl.GetInstanceID()))
            //            {
            //                Debug.Log("AddChild6: " + vl.GetInstanceID() + " " + item2.GetFullPath());
            //                AddChildren(ref itemA, ref vl);
            //            }
            //        }
            //    }
            //    else if (mso is ValMapNMSScriptable)
            //    {
            //        var vlmnlms = (ValMapNMSScriptable)mso;
            //        //foreach keyvaluepair in the dictionary, add a childmenuitem
            //        foreach (KeyValuePair<ValDoubleScriptable, MiniScriptScriptableObject> kv in vlmnlms.Values)
            //        {
            //            var vl = kv.Value; //get the List in the .Value
            //            if (vl is IDatagraphProperties)
            //            {   //add the item to the odinmenutree
            //                var itemB = new OdinMenuItem(item2.MenuTree, vl.name, vl);
            //                item2.ChildMenuItems.Add(itemB);

            //                if (((IDatagraphProperties)vl).HasChildren())
            //                {
            //                    Debug.Log("AddChild7: " + vl.GetInstanceID() + " " + item2.GetFullPath());
            //                    AddChildren(ref itemB, ref vl);
            //                }
            //            }
            //        }
            //    }
            //}
            //else
            //{   //it is neither List nor Dictionary, so it cant have further children
            //    Debug.Log("AddChildXX: " + mso.GetInstanceID() + " " + item.GetFullPath());
            //    item.ChildMenuItems.Add(new OdinMenuItem(item.MenuTree, mso.name, mso));
            //}
        }
    }
}
