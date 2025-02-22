using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModLoadingProgresssBar : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public Image ProgressBar;

    void Start()
    {

    }

    void Update()
    {

    }

    public int FillAmount
    {
        get { return 0; }
        set
        {
            if (value >= 0 && value <= 100)
            {
                ProgressBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ((float)value/100 * 1200));
            }
        }
    }

}

