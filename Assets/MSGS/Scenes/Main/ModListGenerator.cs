using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModListGenerator : MonoBehaviour
{
    public ModLoadingProgresssBar ModProgBar;
    public ModListviewController Listview;
    public GameObject Parent;
        
    void Start()
    {
        
    }
        
    void Update()
    {
        
    }

    public void CloseMenu()
    {
        Destroy(Parent);
    }
}
