using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public bool isDropped;
    public void OnDrop(PointerEventData eventData)
    {
        isDropped = true;
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.parent = transform;
            eventData.pointerDrag.transform.position = transform.position;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDropped = false;
    }
}
