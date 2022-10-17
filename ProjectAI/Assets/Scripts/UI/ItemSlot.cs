using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool isDropped;
    public MonsterStat stat;
    protected RectTransform rect;
    protected GameManager gameManager;
    protected ItemSlot preSlot;

    protected virtual void Awake()
    {
        rect = GetComponent<RectTransform>();
        gameManager = GameManager.Instance;
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;

            if(isDropped)
            {
                DragDrop dd = eventData.pointerDrag.GetComponent<DragDrop>();
                preSlot = dd.preParent.GetComponent<ItemSlot>();
                GameObject obj = transform.GetChild(1).gameObject;

                obj.transform.SetParent(dd.preParent);
                obj.GetComponent<RectTransform>().position = dd.preParent.GetComponent<RectTransform>().position;
                preSlot.isDropped = true;
                preSlot.stat = stat;
            }

            stat = eventData.pointerDrag.GetComponent<MonsterUI>().stat;
        }
        isDropped = true;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
    }
}
