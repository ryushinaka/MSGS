using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MiniScript.MSGS.Zip;
using MiniScript.MSGS.PackageManager;

public class ModListviewController : MonoBehaviour
{
    public ScrollRect Scrollview;
    public ModLoadingProgresssBar ProgBar;
    public GameObject itemPrefab;
    public ModListGenerator Parent;
    public Transform content;

    List<string> paths;
    List<string> tmp;

    void Start()
    {
        paths = new List<string>();
        tmp = new List<string>(System.IO.Directory.GetFiles(Application.streamingAssetsPath));

        foreach (string s in tmp)
        {   //do any files have the Zip file extension?
            if (s.EndsWith("zip"))
            {
                paths.Add(s);
                //lst.Add(System.IO.Path.GetFileNameWithoutExtension(s));
            }
        }

        List<DataStruct> data = new List<DataStruct>();
        foreach (string p in paths)
        {
            //Which of the .zip files pass validation check?
            ArchiveHandler.GetFile(p);
            if (ArchiveHandler.isValidArchive())
            {
                data.Add(new DataStruct()
                {
                    FullPath = p,
                    Name = System.IO.Path.GetFileNameWithoutExtension(p)
                });
            }
            
            ArchiveHandler.Close();
        }

        PopulateList(ref data);
    }

    void Update()
    {

    }

    public void OnListviewItemClick(ModListviewItem item)
    {   
        PackageImporter pkg = new PackageImporter();
        pkg.FilePath = item.FullPath;
        //pkg.DebugOutput = true;
        pkg.ImportPackage();

        //Debug.Log("Imported: " + item.Text.text);
    }

    public void PopulateList(ref List<DataStruct> archives)
    {
        foreach (DataStruct s in archives)
        {
            GameObject go = Instantiate(itemPrefab);
            go.transform.SetParent(content);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.GetComponent<ModListviewItem>().Text.SetText(s.Name);
            go.GetComponent<ModListviewItem>().Parent = this;
            go.GetComponent<ModListviewItem>().FullPath = s.FullPath;
        }
    }

    public class DataStruct
    {
        public string FullPath;
        public string Name;
    }
}
