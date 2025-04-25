using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.IO;
using System.Data;
using MiniScript.MSGS.MUUI.Extensions;
using MiniScript.MSGS.MUUI.TwoDimensional;
using MiniScript.MSGS.Unity;

namespace MiniScript.MSGS.MUUI
{
    public class LayoutManager : MonoBehaviour
    {
        public TextAsset File;
        public PrefabContainer Prefabs;
        public PrefabInstance Instance;

        private void Awake()
        {
            MiniScriptSingleton.PrefabContainer = Prefabs;
        }

        void Start()
        {

        }

        void Update()
        {

        }

        [Button]
        void LoadLayout()
        {
            if (File != null)
            {
                if (File.text.Length > 0)
                {
                    var stream = new MemoryStream();
                    stream.Write(File.bytes);
                    //Debug.Log("streamed bytes " + File.bytes.Length);

                    stream.Position = 0;
                    DataSet set = new DataSet();
                    set.ReadXml(stream, XmlReadMode.ReadSchema);

                    //Debug.Log("tables " + set.Tables.Count);
                    //System.Threading.Thread.Sleep(16);

                    //spawn all the prefabs based on the dataset records
                    //objects = new List<Tuple<int, int, GameObject>>();
                    SpawnPrefabs(ref set, this.transform);

                    //Debug.Log("spawned");
                }
            }
        }

        List<Tuple<int, int, GameObject>> objects;
        void SpawnPrefabs(ref DataSet set, Transform parent)
        {
            objects = new List<Tuple<int, int, GameObject>>();
            GameObject topNode = new GameObject(set.DataSetName);
            DataRow rect = null;
            
            //find the topNode and assign it
            foreach (DataRow vdr in set.Tables["GameObjects"].Rows)
            {
                int zzz = 0;
                if (!int.TryParse(vdr["childof"].ToString(), out zzz)) {
                    foreach(DataColumn dc in set.Tables["GameObjects"].Columns)
                    {
                        Debug.Log(dc.ColumnName + ":" + vdr[dc]);
                    }                    
                    continue;
                }

                //append hierarchy tuple data for sorting node depths
                if (int.Parse(vdr["childof"].ToString()) == -1)
                {
                    topNode.name = (string)vdr["Name"];
                    topNode.AddComponent<RectTransform>();
                    topNode.transform.SetParent(parent);
                    //the scale of the gameobject must be set before adding any children to it
                    topNode.transform.localScale = new Vector3(1, 1, 1);

                    //rect = set.Tables["RectTransform"].Select("OwnerInstanceID=" + (int)vdr["instanceid"])[0]; //get position data
                    //topNode.GetComponent<RectTransform>().SetupRect(ref rect); //set position
                    objects.Add(Tuple.Create<int, int, GameObject>((int)vdr["instanceid"], (int)vdr["childof"], topNode));
                }
            }

            //iterate every other node that is not at the top
            foreach (DataRow dr in set.Tables["GameObjects"].Rows)
            {
                //append hierarchy tuple data for sorting node depths
                if ((int)dr["childof"] != -1)
                {
                    int id = (int)dr["instanceid"]; //instanceid of the gameobject
                    GameObject go = null;

                    //determine what each gameobject represents in the data graph
                    DataRow[] rows = null;
                    rows = set.Tables["Button"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0)
                    {
                        //Debug.Log(id + " " + (int)dr["childof"] + " button");
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.ButtonPrefab);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIButton>().SetupButton(ref rows[0]);
                        rect = set.Tables["RectTransform"].Select("OwnerInstanceID=" + id)[0]; //get position data
                        go.GetComponent<RectTransform>().SetupRect(ref rect); //set position
                        objects.Add(Tuple.Create<int, int, GameObject>(id, (int)dr["childof"], go));
                        continue;
                    }
                    rows = set.Tables["ButtonAnimated"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0)
                    {
                        //Debug.Log(id + " " + (int)dr["childof"] + " buttonanimated");
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.ButtonAnimatedPrefab);
                        var sprites = set.Tables["SpritesTable"].Select("OwnerInstanceID=" + id);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIButtonAnimated>().SetupButton(ref rows[0], ref sprites);
                        rect = set.Tables["RectTransform"].Select("OwnerInstanceID=" + id)[0];
                        go.GetComponent<RectTransform>().SetupRect(ref rect);
                        objects.Add(Tuple.Create<int, int, GameObject>(id, (int)dr["childof"], go));
                        continue;
                    }
                    rows = set.Tables["Image"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0)
                    {
                        //Debug.Log(id + " " + (int)dr["childof"] + " image");
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.ImagePrefab);
                        byte[] barr = System.Convert.FromBase64String((string)rows[0]["Sprite"]);
                        Texture2D tex = new Texture2D(1, 1);
                        tex.LoadImage(barr);
                        var sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIImage>().SetupImage(ref rows[0]);
                        rect = set.Tables["RectTransform"].Select("OwnerInstanceID=" + id)[0];
                        go.GetComponent<RectTransform>().SetupRect(ref rect);
                        objects.Add(Tuple.Create<int, int, GameObject>(id, (int)dr["childof"], go));
                        continue;
                    }
                    rows = set.Tables["ImageAnimated"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0)
                    {
                        //Debug.Log(id + " " + (int)dr["childof"] + " image");
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.ImageAnimatedPrefab);
                        byte[] barr = System.Convert.FromBase64String((string)rows[0]["Sprite"]);
                        Texture2D tex = new Texture2D(1, 1);
                        tex.LoadImage(barr);
                        var sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIImage>().SetupImage(ref rows[0]);
                        rect = set.Tables["RectTransform"].Select("OwnerInstanceID=" + id)[0];
                        go.GetComponent<RectTransform>().SetupRect(ref rect);
                        objects.Add(Tuple.Create<int, int, GameObject>(id, (int)dr["childof"], go));
                        continue;
                    }
                    rows = set.Tables["Text"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0)
                    {
                        //Debug.Log(id + " " + (int)dr["childof"] + " text");
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.TextPrefab);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIText>().SetupText(ref rows[0]);
                        rect = set.Tables["RectTransform"].Select("OwnerInstanceID=" + id)[0];
                        go.GetComponent<RectTransform>().SetupRect(ref rect);
                        objects.Add(Tuple.Create<int, int, GameObject>(id, (int)dr["childof"], go));
                        continue;
                    }
                    rows = set.Tables["Panel"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0)
                    {
                        //Debug.Log(id + " " + (int)dr["childof"] + " panel");
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.PanelPrefab);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIPanel>().SetupPanel(ref rows[0]);
                        rect = set.Tables["RectTransform"].Select("OwnerInstanceID=" + id)[0];
                        go.GetComponent<RectTransform>().SetupRect(ref rect);
                        objects.Add(Tuple.Create<int, int, GameObject>(id, (int)dr["childof"], go));
                        continue;
                    }
                    rows = set.Tables["Canvas"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0)
                    {
                        //Debug.Log(id + " " + (int)dr["childof"] + " canvas");
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.CanvasPrefab);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUICanvas>().SetupCanvas(ref rows[0]);
                        rect = set.Tables["RectTransform"].Select("OwnerInstanceID=" + id)[0];
                        go.GetComponent<RectTransform>().SetupRect(ref rect);
                        objects.Add(Tuple.Create<int, int, GameObject>(id, (int)dr["childof"], go));
                        continue;
                    }
                    rows = set.Tables["DropDown"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0)
                    {
                        //Debug.Log(id + " " + (int)dr["childof"] + " dropdown");
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.DropDownPrefab);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIDropDown>().SetupDropDown(ref rows[0]);
                        rect = set.Tables["RectTransform"].Select("OwnerInstanceID=" + id)[0];
                        go.GetComponent<RectTransform>().SetupRect(ref rect);
                        objects.Add(Tuple.Create<int, int, GameObject>(id, (int)dr["childof"], go));
                        continue;
                    }
                    rows = set.Tables["InputField"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0)
                    {
                        //Debug.Log(id + " " + (int)dr["childof"] + " inputfield");
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.InputFieldPrefab);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIInputField>().SetupInputField(ref rows[0]);
                        rect = set.Tables["RectTransform"].Select("OwnerInstanceID=" + id)[0];
                        go.GetComponent<RectTransform>().SetupRect(ref rect);
                        objects.Add(Tuple.Create<int, int, GameObject>(id, (int)dr["childof"], go));
                        continue;
                    }
                    rows = set.Tables["Scrollview"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0)
                    {
                        //Debug.Log(id + " " + (int)dr["childof"] + " scrollview");
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.ScrollViewPrefab);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIScrollview>().SetupScrollview(ref rows[0]);
                        rect = set.Tables["RectTransform"].Select("OwnerInstanceID=" + id)[0];
                        go.GetComponent<RectTransform>().SetupRect(ref rect);
                        objects.Add(Tuple.Create<int, int, GameObject>(id, (int)dr["childof"], go));
                        continue;
                    }
                    rows = set.Tables["Slider"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0)
                    {
                        //Debug.Log(id + " " + (int)dr["childof"] + " slider");
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.SliderPrefab);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUISlider>().SetupSlider(ref rows[0]);
                        rect = set.Tables["RectTransform"].Select("OwnerInstanceID=" + id)[0];
                        go.GetComponent<RectTransform>().SetupRect(ref rect);
                        objects.Add(Tuple.Create<int, int, GameObject>(id, (int)dr["childof"], go));
                        continue;
                    }
                    rows = set.Tables["Toggle"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0)
                    {
                        //Debug.Log(id + " " + (int)dr["childof"] + " toggle");
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.TogglePrefab);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.MUUIToggle>().SetupToggle(ref rows[0]);
                        rect = set.Tables["RectTransform"].Select("OwnerInstanceID=" + id)[0];
                        go.GetComponent<RectTransform>().SetupRect(ref rect);
                        objects.Add(Tuple.Create<int, int, GameObject>(id, (int)dr["childof"], go));
                        continue;
                    }    
                }
            }

            var tobeRemoved = new List<Tuple<int, int, GameObject>>();
            
            Debug.Log("objects: " + objects.Count);
            
            //instanceID, childOF, GO instance
            foreach (Tuple<int, int, GameObject> tu in objects)
            {
                if (tu.Item2 != -1) //we are a child of some object
                {
                    //find our parent
                    foreach (Tuple<int, int, GameObject> tu2 in objects)
                    {
                        //Debug.Log(tu.Item1 + " / " + tu.Item2 + " / " + tu.Item3.name + " vs " + tu2.Item1 + " / " + tu2.Item2 + " / " + tu2.Item3.name);
                        if (tu2.Item1 == tu.Item2)
                        {
                            tu.Item3.transform.SetParent(tu2.Item3.transform);
                            //Debug.Log("set parent " + tu.Item1 + " to " + tu2.Item2);
                        }
                    }
                }
            }

            //set the parent *LOCATION* AFTER all the children have been added.
            //if you set the location before adding the children, they will be offset
            topNode.transform.localPosition = new Vector3(0, 0, 0);

            foreach (Tuple<int, int, GameObject> tu in tobeRemoved)
            {
                DestroyImmediate(tu.Item3);
            }
        }

    }
}

