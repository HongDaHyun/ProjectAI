using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouse_Cursor : MonoBehaviour
{
    public RectTransform canvasRect;
    public Camera uiCam;

    GameManager gameManager;
    Animator anim;
    RectTransform rectTransform;
    Image img;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        rectTransform = GetComponent<RectTransform>();
        anim = GetComponent<Animator>();
        img = GetComponent<Image>();
    }

    private void Start()
    {
        MouseVisible();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MouseVisible();
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, uiCam, out anchoredPos);
        rectTransform.anchoredPosition = anchoredPos;
        Skill();
    }

    void MouseVisible()
    {
        if (Cursor.visible)
            Cursor.visible = false;
    }

    private void Skill()
    {
        if (gameManager.isSkill)
        {
            anim.SetBool("IsAtk", true);
            img.raycastTarget = true;
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }

        else if (!gameManager.isSkill)
        {
            anim.SetBool("IsAtk", false);
            img.raycastTarget = false;
            rectTransform.pivot = new Vector2(0.15f, 0.9f);
        }
    }
}