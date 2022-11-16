using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Cursor : MonoBehaviour
{
    public RectTransform canvasRect;
    public Camera uiCam;
    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
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
    }

    void MouseVisible()
    {
        if (Cursor.visible)
            Cursor.visible = false;
    }
}