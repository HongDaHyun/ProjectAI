using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool isDropped, isEquip;
    public Image img;
    public MonsterStat stat;
    public RectTransform rect;
    protected GameManager gameManager;
    protected ItemSlot preSlot;

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
            DragDrop dd = eventData.pointerDrag.GetComponent<DragDrop>();
            dd.rect.position = rect.position;

            preSlot = dd.preParent.GetComponent<ItemSlot>();
            dd.curSlot = this;

            if (isDropped)
            {
                GameObject obj = transform.GetChild(1).gameObject;
                obj.transform.SetParent(dd.preParent);
                obj.GetComponent<RectTransform>().position = dd.preParent.GetComponent<RectTransform>().position;
                preSlot.isDropped = true;
                preSlot.stat = stat;
            }
            stat = eventData.pointerDrag.GetComponent<MonsterUI>().stat;
            if(preSlot.isEquip)
            {
                if (!isEquip)
                    preSlot.img.fillAmount = 0;
                else if (preSlot == this)
                { }
                else if (isEquip)
                    if(preSlot.isDropped)
                        preSlot.img.fillAmount = 1;
                    else if(!preSlot.isDropped)
                        preSlot.img.fillAmount = 0;
            }
            isDropped = true;
            gameManager.uiManager.slotsUI.FullSlot();
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
    }
}
