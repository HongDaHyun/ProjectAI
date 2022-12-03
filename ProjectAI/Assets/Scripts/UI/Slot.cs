using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool isDropped, isEquip, isSkill;
    public Image img;
    public RectTransform rect;
    [HideInInspector] public MonsterStat stat;
    [HideInInspector] public SkillStat skillStat;
    protected GameManager gameManager;
    protected Slot preSlot;
    protected DragDrop dd;

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
            dd = SetDragDrop(eventData);
            if (isDropped)
            {
                SwapUI(dd);
            }
            PreEquip();
            isDropped = true;
            dd.transform.SetAsFirstSibling();
            gameManager.uiManager.slotsUI.FullSlot();
        }
    }

    public virtual DragDrop SetDragDrop(PointerEventData eventData)
    {
        gameManager.soundManager.SFXPlayer(SoundManager.SFX.DropSlot);
        eventData.pointerDrag.transform.SetParent(transform);
        DragDrop dd = eventData.pointerDrag.GetComponent<DragDrop>();
        dd.rect.position = rect.position;

        preSlot = dd.preParent.GetComponent<Slot>();
        dd.curSlot = this;
        return dd;
    }

    public virtual void SwapUI(DragDrop dd)
    {
        GameObject obj = transform.GetChild(0).gameObject;
        obj.transform.SetParent(dd.preParent);
        obj.GetComponent<RectTransform>().position = dd.preParent.GetComponent<RectTransform>().position;
        preSlot.isDropped = true;
        obj.transform.SetAsFirstSibling();
    }

    public virtual void PreEquip()
    {
        if (preSlot.isEquip)
        {
            if (!isEquip)
                preSlot.img.fillAmount = 0;
            else if (preSlot == this)
            { }
            else if (isEquip)
                if (preSlot.isDropped)
                    preSlot.img.fillAmount = 1;
                else if (!preSlot.isDropped)
                    preSlot.img.fillAmount = 0;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
