using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop_Monster : DragDrop, IPointerClickHandler
{
    MonsterUI monsterUI;

    protected override void Awake()
    {
        base.Awake();
        monsterUI = GetComponent<MonsterUI>();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        monsterUI.stat = new MonsterStat();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        preSlot.stat = new MonsterStat();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerClick != null)
        {
            if (Input.GetMouseButtonUp(1))
            {
                gameManager.uiManager.Dump(monsterUI.stat.tier, curSlot.rect);

                if (curSlot.isEquip)
                {
                    curSlot.img.fillAmount = 0;
                }
                curSlot.isDropped = false;
                curSlot.stat = new MonsterStat();

                gameManager.uiManager.slotsUI.FullSlot();
                eventData.pointerDrag.SetActive(false);
            }
        }
    }

    public override void Replace()
    {
        base.Replace();
        preSlot.stat = monsterUI.stat;
    }
}
