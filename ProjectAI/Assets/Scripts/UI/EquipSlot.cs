using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot : ItemSlot
{
    public bool isSpawn;
    public Image img;

    protected override void Awake()
    {
        base.Awake();
        img = transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        gameManager.spawnManager.Spawn(this);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        if(preSlot != null && stat.name != null)
            if (preSlot.stat.name != stat.name)
                img.fillAmount = 1;
        img.transform.SetAsFirstSibling();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        img.transform.SetAsFirstSibling();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        img.transform.SetAsLastSibling();
    }
}
