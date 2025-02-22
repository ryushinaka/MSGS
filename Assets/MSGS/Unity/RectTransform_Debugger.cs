using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

[RequireComponent(typeof(RectTransform))]
public class RectTransform_Debugger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#if ODIN_INSPECTOR
    [Button]
#endif
    public void Debug_Output()
    {
        var rt = GetComponent<RectTransform>();
        
        Debug.Log("AnchoredPosition: " + rt.anchoredPosition.x + "/" + rt.anchoredPosition.y);

        Debug.Log("LocalPosition: " + rt.localPosition.x + "/" + rt.localPosition.y + "/" + rt.localPosition.z);

        Debug.Log("LocalScale: " + rt.localScale.x + "/" + rt.localScale.y + "/" + rt.localScale.z);

        Debug.Log("offsetMax: " + rt.offsetMax.x + "/" + rt.offsetMax.y);

        Debug.Log("offsetMin: " + rt.offsetMin.x + "/" + rt.offsetMin.y);

        Debug.Log("anchorMin: " + rt.anchorMin.x + "/" + rt.anchorMin.y);

        Debug.Log("anchorMax: " + rt.anchorMax.x + "/" + rt.anchorMax.y);

        Debug.Log("sizeDelta: " + rt.sizeDelta.x + "/" + rt.sizeDelta.y);

        Debug.Log("pivot: " + rt.pivot.x + "/" + rt.pivot.y);

        Debug.Log("Enabled: " + rt.gameObject.activeSelf);
    }
}
