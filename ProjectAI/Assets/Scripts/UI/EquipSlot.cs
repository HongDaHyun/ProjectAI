using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot : ItemSlot
{
    public bool isSpawn;

    private void Update()
    {
        gameManager.spawnManager.Spawn(this);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        if (preSlot != null && _name != "")
            if (preSlot._name != _name)
                img.fillAmount = 1;
        gameManager.synergyManager.SynergyNotice();
    }
}
