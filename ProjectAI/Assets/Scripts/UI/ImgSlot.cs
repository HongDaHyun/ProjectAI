using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImgSlot : MonoBehaviour
{
    [HideInInspector] public RectTransform rect;
    [HideInInspector] public Image img;
    [HideInInspector] public string _name;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        img = transform.GetChild(0).GetComponent<Image>();
    }
}
