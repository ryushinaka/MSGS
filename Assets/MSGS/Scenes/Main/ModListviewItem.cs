using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModListviewItem : MonoBehaviour
{
    public Button button;
    public TMPro.TextMeshProUGUI Text;
    public ModListviewController Parent;
    public string FullPath;

    void Start()
    {
        
    }
        
    void Update()
    {
        
    }        

    public void OnClick()
    {
        Parent.OnListviewItemClick(this);
    }
}
