using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ToolTip : MonoBehaviour
{
    GameManager gameManager;

    float halfWidth;
    RectTransform rect;

    private void Awake()
    {
        gameManager = GameManager.Instance;

        halfWidth = GetComponentInParent<CanvasScaler>().referenceResolution.x * 0.5f;
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        transform.position = gameManager.uiManager.mouseCursorUI.transform.position;

        if (rect.anchoredPosition.x + rect.sizeDelta.x > halfWidth)
            rect.pivot = new Vector2(1, 0);
        else
            rect.pivot = new Vector2(0, 0);
    }
}
