using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCan : MonoBehaviour, IDropHandler
{
    GameManager gameManager;
    RectTransform rect;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        rect = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            MonsterUI monsterUI = eventData.pointerDrag.GetComponent<MonsterUI>();
            DragDrop dragDrop = monsterUI.GetComponent<DragDrop>();
            switch (monsterUI.stat.tier)
            {
                case Tier.Common:
                    Dump(1);
                    break;
                case Tier.Rare:
                    Dump(2);
                    break;
                case Tier.Epic:
                    Dump(3);
                    break;
                case Tier.Legend:
                    Dump(5);
                    break;
            }
            if (dragDrop.itemSlot.isEquip)
                dragDrop.itemSlot.img.fillAmount = 0;
            gameManager.uiManager.slotsUI.FullSlot();
            eventData.pointerDrag.SetActive(false);
        }
    }

    public void Dump(int money)
    {
        gameManager.money += money;

        GameObject obj = gameManager.poolingManager.MakeObj("UIPopUp");
        obj.transform.SetParent(transform);
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.GetComponent<RectTransform>().position = rect.position;
        StartCoroutine(obj.GetComponent<UIPopUp>().Pop(money));
    }
}
