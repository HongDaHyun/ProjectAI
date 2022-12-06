using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ToolTip : MonoBehaviour
{
    protected GameManager gameManager;
    protected RectTransform rect;

    float halfWidth;

    private void Awake()
    {
        gameManager = GameManager.Instance;

        rect = GetComponent<RectTransform>();
        halfWidth = GetComponentInParent<CanvasScaler>().referenceResolution.x * 0.5f;
    }

    private void OnEnable()
    {
        transform.position = gameManager.uiManager.mouseCursorUI.transform.position;
    }

    private void Update()
    {
        transform.position = gameManager.uiManager.mouseCursorUI.transform.position;

        if (rect.anchoredPosition.x + rect.sizeDelta.x > halfWidth)
            rect.pivot = new Vector2(1, 0);
        else
            rect.pivot = new Vector2(0, 0);
    }

    public abstract void SetToolTip(MonsterStat stat);
}
