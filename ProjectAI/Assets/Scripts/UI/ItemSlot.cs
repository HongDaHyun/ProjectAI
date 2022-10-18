using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool isDropped;
    public MonsterStat stat;
    protected RectTransform rect;
    protected GameManager gameManager;
    protected ItemSlot preSlot;
    public Image img;

    protected void Awake()
    {
        rect = GetComponent<RectTransform>();
        gameManager = GameManager.Instance;
        img = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            gameManager.soundManager.SFXPlayer(SoundManager.SFX.DropSlot);
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;

            DragDrop dd = eventData.pointerDrag.GetComponent<DragDrop>();
            preSlot = dd.preParent.GetComponent<ItemSlot>();

            if (isDropped)
            {
                GameObject obj = transform.GetChild(1).gameObject;

                obj.transform.SetParent(dd.preParent);
                obj.GetComponent<RectTransform>().position = dd.preParent.GetComponent<RectTransform>().position;
                preSlot.isDropped = true;
                preSlot.stat = stat;
            }
            stat = eventData.pointerDrag.GetComponent<MonsterUI>().stat;
            isDropped = true;
            preSlot.img.fillAmount = 0;
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
    }
}
