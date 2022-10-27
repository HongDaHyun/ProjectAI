using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    GameManager gameManager;
    Canvas canvas;
    [HideInInspector]public RectTransform rect;
    CanvasGroup canvasGroup;
    public Transform preParent;
    public ItemSlot curSlot;
    public ItemSlot preSlot;
    MonsterUI monsterUI;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        rect = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        monsterUI = GetComponent<MonsterUI>();
    }

    private void OnDisable()
    {
        monsterUI.stat = new MonsterStat();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        preParent = transform.parent;
        preSlot = preParent.GetComponent<ItemSlot>();
        preSlot.isDropped = false;
        preSlot.stat = new MonsterStat();
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.parent.name == canvas.name)
        {
            transform.SetParent(preParent);
            if (preParent.GetComponent<ItemSlot>().isEquip)
                transform.SetAsFirstSibling();

            rect.position = preParent.GetComponent<RectTransform>().position;
            preSlot.isDropped = true;
            preSlot.stat = monsterUI.stat;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
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
}
