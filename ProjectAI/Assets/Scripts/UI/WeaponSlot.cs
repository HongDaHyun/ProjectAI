using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    [HideInInspector] public RectTransform rect;
    [HideInInspector] public Image weaponImg;
    [HideInInspector] public string _name;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        weaponImg = transform.GetChild(0).GetComponent<Image>();
    }
}
