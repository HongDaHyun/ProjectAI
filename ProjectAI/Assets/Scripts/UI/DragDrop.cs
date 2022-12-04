using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    protected GameManager gameManager;
    Canvas canvas;
    [HideInInspector]public RectTransform rect;
    CanvasGroup canvasGroup;
    public Transform preParent;
    public Slot curSlot;
    public Slot preSlot;

    protected virtual void Awake()
    {
        gameManager = GameManager.Instance;
        rect = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    protected virtual void OnDisable()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        preParent = transform.parent;
        preSlot = preParent.GetComponent<Slot>();
        preSlot.isDropped = false;
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
            Replace();
        transform.localScale = new Vector3(0.8f, 0.8f, 1);
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public virtual void Replace()
    {
        transform.SetParent(preParent);

        rect.position = preParent.GetComponent<RectTransform>().position;
        preSlot.isDropped = true;
        transform.SetAsFirstSibling();
    }
}
