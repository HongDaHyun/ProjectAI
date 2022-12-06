using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : Slot
{
    bool isHover;
    float hoverTime;
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

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (!isDropped)
            return;
        isHover = true;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        isHover = false;
        hoverTime = 0;
        gameManager.uiManager.toolTip.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isHover)
            return;
        hoverTime += Time.deltaTime;

        if (hoverTime > 2f)
        {
            isHover = false;
            gameManager.uiManager.toolTip.gameObject.SetActive(true);
            gameManager.uiManager.toolTip.SetToolTip(stat);
        }
    }
}
