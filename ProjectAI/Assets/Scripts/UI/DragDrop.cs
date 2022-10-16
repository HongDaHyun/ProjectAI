using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Canvas canvas;
    RectTransform rect;
    CanvasGroup canvasGroup;
    Transform preParent;
    ItemSlot itemSlot;
    MonsterUI monsterUI;

    private void Awake()
    {
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
        itemSlot = preParent.GetComponent<ItemSlot>();
        itemSlot.isDropped = false;
        itemSlot.stat = new MonsterStat();
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
            rect.position = preParent.GetComponent<RectTransform>().position;
            itemSlot.isDropped = true;
            itemSlot.stat = monsterUI.stat;
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}
