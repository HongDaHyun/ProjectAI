using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCan : MonoBehaviour, IDropHandler
{
    GameManager gameManager;
    [HideInInspector]public RectTransform rect;

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

            gameManager.uiManager.UIPopUpApple(gameManager.uiManager.TierForSale(monsterUI.stat.tier));

            if (dragDrop.preSlot.isEquip)
                dragDrop.preSlot.img.fillAmount = 0;
            gameManager.uiManager.slotsUI.FullSlot();
            eventData.pointerDrag.SetActive(false);
        }
    }
}
