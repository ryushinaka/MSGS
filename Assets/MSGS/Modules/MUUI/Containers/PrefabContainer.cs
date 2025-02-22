using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using MiniScript.MSGS.MUUI.Extensions;
using MiniScript.MSGS.MUUI.TwoDimensional;

namespace MiniScript.MSGS.MUUI
{
    public class PrefabContainer : MonoBehaviour
    {
        public GameObject CanvasPrefab, ButtonPrefab, ButtonAnimatedPrefab, DropDownPrefab, InputFieldPrefab,
                ImagePrefab, PanelPrefab, RawImagePrefab, ScrollViewPrefab, ImageAnimatedPrefab,
                SliderPrefab, TextPrefab, TogglePrefab, RectPrefab, CustomPrefabs, CanvasParent;
        public Dictionary<string, GameObject> Prefabs;
        public Dictionary<string, DataSet> prefabSets;

        void Start()
        {
            Prefabs = new Dictionary<string, GameObject>();
            prefabSets = new Dictionary<string, DataSet>();
            MiniScriptSingleton.PrefabContainer = this;
        }

        void Update()
        {

        }

        public void InstantiatePrefab(UIElementType type, ref DataRow dr, out GameObject go)
        {
            switch (type)
            {
                case UIElementType.Button:
                    //clone the prefab
                    GameObject button = Instantiate(ButtonPrefab);
                    //pass the datarow to the script for initialization
                    button.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Button>().SetupButton(ref dr);
                    //return the new button to the caller/handler
                    go = button;
                    break;
                case UIElementType.Canvas:
                    GameObject canvas = Instantiate(CanvasPrefab);
                    canvas.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Canvas>().SetupCanvas(ref dr);
                    go = canvas;
                    break;
                case UIElementType.DropDown:
                    GameObject drop = Instantiate(DropDownPrefab);
                    drop.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.DropDown>().SetupDropDown(ref dr);
                    go = drop;
                    break;
                case UIElementType.Image:
                    GameObject image = Instantiate(ImagePrefab);
                    image.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Image>().SetupImage(ref dr);
                    go = image;
                    break;
                case UIElementType.InputField:
                    GameObject input = Instantiate(InputFieldPrefab);
                    input.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.InputField>().SetupInputField(ref dr);
                    go = input;
                    break;
                case UIElementType.Panel:
                    GameObject panel = Instantiate(PanelPrefab);
                    panel.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Panel>().SetupPanel(ref dr);
                    go = panel;
                    break;
                //case MUUIElementType.RawImage:

                //    go = null;
                //    break;
                case UIElementType.RectTransform:
                    GameObject rectT = Instantiate(RectPrefab);
                    rectT.GetComponent<RectTransform>().SetupRect(ref dr);
                    go = rectT;
                    break;
                case UIElementType.Scrollview:
                    GameObject scroll = Instantiate(ScrollViewPrefab);
                    scroll.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().SetupScrollview(ref dr);
                    go = scroll;
                    break;
                case UIElementType.Slider:
                    GameObject slider = Instantiate(SliderPrefab);
                    slider.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Slider>().SetupSlider(ref dr);
                    go = slider;
                    break;
                case UIElementType.Text:
                    GameObject text = Instantiate(TextPrefab);
                    text.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Text>().SetupText(ref dr);
                    go = text;
                    break;
                case UIElementType.Toggle:
                    GameObject toggle = Instantiate(TogglePrefab);
                    toggle.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Toggle>().SetupToggle(ref dr);
                    go = toggle;
                    break;
                default:
                    string msg = dr.Table.DataSet.DataSetName + "/" + dr.Table.TableName;
                    MiniScriptSingleton.LogWarning("Attempt to instantiate unsupported UI Element object." + '\n' + msg);
                    go = null;
                    break;
            }
        }

        public void InstantiatePrefabs(List<DataSet> prefabs)
        {
            if (prefabs == null) return;
            if (prefabs.Count == 0) return;
            if (Prefabs == null) Prefabs = new Dictionary<string, GameObject>();
            //safety check
            if (MiniScriptSingleton.PrefabContainer == null) MiniScriptSingleton.PrefabContainer = this;

            DataSet set = null;
            for(int i = 0; i < prefabs.Count; i++)            
            {
                set = prefabs[i];

                #region
                List<GameObject> objects = new List<GameObject>();
                List<Tuple<int, int, GameObject>> instanceids = new List<Tuple<int, int, GameObject>>();

                //iterate the datarows to add each element to the prefab
                foreach (DataRow gameobj in set.Tables["GameObjects"].Rows)
                {   //each game object record corresponds with a ?element? of some kind
                    #region
                    GameObject go = null;
                    int id = int.Parse(gameobj["instanceid"].ToString());

                    DataRow[] frames = null;
                    DataRow[] rows = set.Tables["Button"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.ButtonPrefab);
                        new WaitForEndOfFrame();
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Button>().SetupButton(ref rows[0]);
                    }

                    rows = set.Tables["ButtonAnimated"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {                        
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.ButtonAnimatedPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        new WaitForEndOfFrame();
                        frames = set.Tables["SpritesTable"].Select("OwnerInstanceID=" + id);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.ButtonAnimated>().SetupButton(ref rows[0], ref frames);
                    }

                    rows = set.Tables["Image"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.ImagePrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Image>().SetupImage(ref rows[0]);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["ImageAnimated"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.ImageAnimatedPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        frames = set.Tables["SpritesTable"].Select("OwnerInstanceID=" + id);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.ImageAnimated>().SetupImageAnimated(ref rows[0], ref frames);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["Text"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.TextPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Text>().SetupText(ref rows[0]);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["Panel"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.PanelPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Panel>().SetupPanel(ref rows[0]);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["Canvas"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.CanvasPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Canvas>().SetupCanvas(ref rows[0]);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["DropDown"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.DropDownPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.DropDown>().SetupDropDown(ref rows[0]);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["InputField"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.InputFieldPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.InputField>().SetupInputField(ref rows[0]);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["Scrollview"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.ScrollViewPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().SetupScrollview(ref rows[0]);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["Slider"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.SliderPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        go.GetComponent<Slider>().SetupSlider(ref rows[0]);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["Toggle"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.TogglePrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        go.GetComponent<Toggle>().SetupToggle(ref rows[0]);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["RectTransform"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        if (go == null)
                        {
                            go = Instantiate(MiniScriptSingleton.PrefabContainer.RectPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                            go.GetComponent<RectTransform>().SetupRect(ref rows[0]);
                            go.name = (string)rows[0]["name"];
                        }
                        else
                        {
                            go.GetComponent<RectTransform>().SetupRect(ref rows[0]);
                            go.name = (string)rows[0]["name"];
                        }
                    }

                    objects.Add(go);
                    //save the instanceid from the dataset for use after this for loop ends
                    if (instanceids == null) { instanceids = new List<Tuple<int, int, GameObject>>(); }
                    if (rows.Length > 0 && rows != null)
                    {
                        instanceids.Add(
                        Tuple.Create<int, int, GameObject>(
                            id, (int)rows[0]["ChildOf"], go));
                    }
                    if (rows == null || rows.Length == 0)
                    {
                        Debug.Log("zero rows on " + id);
                    }
                    #endregion
                }

                var x = instanceids[0].Item3.AddComponent<PrefabInstance>();
                x.objects = instanceids;
                x.set = set;
                
                //before we set the RectTransform on all the children, we have to assign the parent to the CustomPrefabs object otherwise Unity will treat the scale/position in a random manner
                instanceids[0].Item3.transform.SetParent(CustomPrefabs.transform);
                //assign the reference to the dictionary, for easy instancing
                Prefabs.Add((string)set.Tables["Config"].Rows[0]["Type"], instanceids[0].Item3);

                //iterate the list of int/int/gameobject to sort who is the child/parent
                //instanceid, parent, gameobject
                foreach (Tuple<int, int, GameObject> tup in instanceids)
                {
                    foreach (Tuple<int, int, GameObject> abc in instanceids)
                    {
                        if (tup.Item2 == abc.Item1)
                        {
                            tup.Item3.transform.SetParent(abc.Item3.transform);

                            DataRow[] rect = set.Tables["RectTransform"].Select("OwnerInstanceID=" + tup.Item1);
                            if (rect != null)
                            {
                                if (rect.Length > 0)
                                {
                                    tup.Item3.GetComponent<RectTransform>().SetupRect(ref rect[0]);
                                }
                                else
                                {
                                    Debug.Log("No RectTransform for id=" + tup.Item1);
                                }
                            }
                            else
                            {
                                Debug.Log("Null entry on id=" + tup.Item1);
                            }
                        }
                    }
                }
                
                #endregion
            }
        }

        public GameObject InstantiatePrefab(string prefabname)
        {
            if(prefabSets.ContainsKey(prefabname))
            {
                //safety check
                if (MiniScriptSingleton.PrefabContainer == null) MiniScriptSingleton.PrefabContainer = this;

                var set = prefabSets[prefabname]; //get the dataSet containing the definition of the prefab

                #region
                List<GameObject> objects = new List<GameObject>();
                List<Tuple<int, int, GameObject>> instanceids = new List<Tuple<int, int, GameObject>>();

                //iterate the datarows to add each element to the prefab
                foreach (DataRow gameobj in set.Tables["GameObjects"].Rows)
                {   //each game object record corresponds with a ?element? of some kind
                    #region
                    GameObject go = null;
                    int id = int.Parse(gameobj["instanceid"].ToString());

                    DataRow[] frames = null;
                    DataRow[] rows = set.Tables["Button"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.ButtonPrefab);
                        new WaitForEndOfFrame();
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Button>().SetupButton(ref rows[0]);
                    }

                    rows = set.Tables["ButtonAnimated"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.ButtonAnimatedPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        new WaitForEndOfFrame();
                        frames = set.Tables["SpritesTable"].Select("OwnerInstanceID=" + id);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.ButtonAnimated>().SetupButton(ref rows[0], ref frames);
                    }

                    rows = set.Tables["Image"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.ImagePrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Image>().SetupImage(ref rows[0]);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["ImageAnimated"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.ImageAnimatedPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        frames = set.Tables["SpritesTable"].Select("OwnerInstanceID=" + id);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.ImageAnimated>().SetupImageAnimated(ref rows[0], ref frames);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["Text"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.TextPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Text>().SetupText(ref rows[0]);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["Panel"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.PanelPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Panel>().SetupPanel(ref rows[0]);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["Canvas"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.CanvasPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Canvas>().SetupCanvas(ref rows[0]);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["DropDown"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.DropDownPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.DropDown>().SetupDropDown(ref rows[0]);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["InputField"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.InputFieldPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.InputField>().SetupInputField(ref rows[0]);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["Scrollview"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.ScrollViewPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Scrollview>().SetupScrollview(ref rows[0]);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["Slider"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.SliderPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Slider>().SetupSlider(ref rows[0]);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["Toggle"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        go = Instantiate(MiniScriptSingleton.PrefabContainer.TogglePrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                        go.GetComponent<MiniScript.MSGS.MUUI.TwoDimensional.Toggle>().SetupToggle(ref rows[0]);
                        go.name = (string)rows[0]["name"];
                    }

                    rows = set.Tables["RectTransform"].Select("OwnerInstanceID=" + id);
                    if (rows.Length > 0 && rows != null)
                    {
                        if (go == null)
                        {
                            go = Instantiate(MiniScriptSingleton.PrefabContainer.RectPrefab, Vector3.zero, Quaternion.identity, CanvasParent.transform);
                            go.GetComponent<RectTransform>().SetupRect(ref rows[0]);
                            go.name = (string)rows[0]["name"];
                        }
                        else
                        {
                            go.GetComponent<RectTransform>().SetupRect(ref rows[0]);
                            go.name = (string)rows[0]["name"];
                        }
                    }

                    objects.Add(go);
                    //save the instanceid from the dataset for use after this for loop ends
                    if (instanceids == null) { instanceids = new List<Tuple<int, int, GameObject>>(); }
                    if (rows.Length > 0 && rows != null)
                    {
                        instanceids.Add(
                        Tuple.Create<int, int, GameObject>(
                            id, (int)rows[0]["ChildOf"], go));
                    }
                    if (rows == null || rows.Length == 0)
                    {
                        Debug.Log("zero rows on " + id);
                    }
                    #endregion
                }

                var x = instanceids[0].Item3.AddComponent<PrefabInstance>();
                x.objects = instanceids;
                x.set = set;

                //before we set the RectTransform on all the children, we have to assign the parent to the CustomPrefabs object otherwise Unity will treat the scale/position in a random manner
                instanceids[0].Item3.transform.SetParent(CustomPrefabs.transform);
                //assign the reference to the dictionary, for easy instancing
                Prefabs.Add((string)set.Tables["Config"].Rows[0]["Type"], instanceids[0].Item3);

                //iterate the list of int/int/gameobject to sort who is the child/parent
                //instanceid, parent, gameobject
                foreach (Tuple<int, int, GameObject> tup in instanceids)
                {
                    foreach (Tuple<int, int, GameObject> abc in instanceids)
                    {
                        if (tup.Item2 == abc.Item1)
                        {
                            tup.Item3.transform.SetParent(abc.Item3.transform);

                            DataRow[] rect = set.Tables["RectTransform"].Select("OwnerInstanceID=" + tup.Item1);
                            if (rect != null)
                            {
                                if (rect.Length > 0)
                                {
                                    tup.Item3.GetComponent<RectTransform>().SetupRect(ref rect[0]);
                                }
                                else
                                {
                                    MiniScriptSingleton.LogError("InstantiatePrefab: No RectTransform for id=" + tup.Item1);
                                }
                            }
                            else
                            {
                                MiniScriptSingleton.LogError("InstantiatePrefab: Null entry on id=" + tup.Item1);
                            }
                        }
                    }
                }
                #endregion

                return new GameObject(prefabname);
            }

            return new GameObject("prefab_notFound");
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void LoadPrefab()
        {
            string[] str = System.IO.Directory.GetFiles(Application.dataPath, "*.xml");
            string lines = string.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                str[i] = str[i].Replace("\\", "/");
            }
            //Debug.Log(lines);

            List<DataSet> sets = new List<DataSet>();

            foreach (string fi in str)
            {
                DataSet set = new DataSet();
                set.ReadXml(System.IO.File.OpenText(fi), XmlReadMode.Auto);
                //Debug.Log("testing file: " + fi);
                if (isPrefabSchema(ref set))
                {   //if the dataset is both a valid Prefab set and has the complete proper schema
                    sets.Add(set); //store it in our prefab list                    
                }
            }

            InstantiatePrefabs(sets);
        }

        public void LoadPrefab(ref DataSet set)
        {
            if(prefabSets == null) { prefabSets = new Dictionary<string, DataSet>(); }

            if(!prefabSets.ContainsKey(set.DataSetName))
            {
                prefabSets.Add(set.DataSetName, set);
            }
        }

        public void UnloadPrefab(string name)
        {
            if(prefabSets.ContainsKey(name))
            {
                prefabSets.Remove(name);
            }
        }

        bool isPrefabSchema(ref DataSet set)
        {
            if (set.Tables.Contains("Config"))
            {
                if (set.Tables["Config"].Rows.Count == 1)
                {
                    DataRow dr = set.Tables["Config"].Rows[0];

                    if(dr["Type"].ToString() == "Prefab")
                    {
                        //this is the prefab we are looking for
                        //if anyone has modified the dataset to not conform to expectations
                        //then this code will crash the process ungracefully, by design/intent
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            
            return false;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void ClearPrefabs()
        {
            while(CustomPrefabs.transform.childCount >0)
            {
                Transform tr = CustomPrefabs.transform.GetChild(0);
                tr.SetParent(null);
                DestroyImmediate(tr.gameObject);
            }

            Prefabs.Clear();
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void SpawnPrefab(string name)
        {
            //sometimes happens in Unity
            if(Prefabs == null) { Prefabs = new Dictionary<string, GameObject>(); }

            if (Prefabs.ContainsKey(name))
            {
                GameObject go = Instantiate(Prefabs[name], Vector3.zero, Quaternion.identity, CanvasParent.transform);
                go.transform.SetParent(CanvasParent.transform);
                go.transform.localPosition = new Vector3(0, 0, 0);
                go.transform.localScale = new Vector3(1, 1, 1);

                //GameObject go = Instantiate(Prefabs[name], CanvasParent.transform, false);
            }
        }
    }
}

