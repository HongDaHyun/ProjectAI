using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public bool isDropped;
    public MonsterStat stat;
    protected RectTransform rect;
    protected GameManager gameManager;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        gameManager = GameManager.Instance;
    }

    public void OnDrop(PointerEventData eventData)
    {
        isDropped = true;
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
            stat = eventData.pointerDrag.GetComponent<MonsterUI>().stat;
        }
    }
}
