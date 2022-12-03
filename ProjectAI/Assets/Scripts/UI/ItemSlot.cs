using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : Slot
{
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        if (preSlot.isSkill)
        {
            dd.Replace();
            return;
        }
        stat = eventData.pointerDrag.GetComponent<MonsterUI>().stat;
    }

    public override void SwapUI(DragDrop dd)
    {
        base.SwapUI(dd);
        preSlot.stat = stat;
    }
}
