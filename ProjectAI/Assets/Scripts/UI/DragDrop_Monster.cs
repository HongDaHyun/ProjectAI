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
        monsterUI._name = "";
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        preSlot._name = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerClick != null)
        {
            if (Input.GetMouseButtonUp(1))
            {
                gameManager.uiManager.UIPopUpApple(gameManager.uiManager.TierForSale(gameManager.csvReader.monsterExportData(monsterUI._name).tier));
                gameManager.csvReader.monsterExportData(monsterUI._name).num--;
                gameManager.synergyManager.SynergyCheck();
                gameManager.synergyManager.SynergyNotice();

                if (curSlot.isEquip)
                {
                    curSlot.img.fillAmount = 0;
                }
                curSlot.isDropped = false;
                curSlot._name = "";

                gameManager.uiManager.slotsUI.FullSlot();
                eventData.pointerDrag.SetActive(false);
            }
        }
    }

    public override void Replace()
    {
        base.Replace();
        preSlot._name = monsterUI._name;
    }
}
