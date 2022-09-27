using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Camera UICam;
    public HpBarUI hpBarUI;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
}

[System.Serializable]
public struct HpBarUI
{
    public Slider hpBar;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int maxHP)
    {
        hpBar.maxValue = maxHP;
        hpBar.value = maxHP;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int curHP)
    {
        hpBar.value = curHP;

        fill.color = gradient.Evaluate(hpBar.normalizedValue);
    }
}