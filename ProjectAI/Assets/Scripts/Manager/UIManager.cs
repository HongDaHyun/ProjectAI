using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public Camera UICam;
    public HpBarUI hpBarUI;
    public SlotsUI slotsUI;
    public List<GameObject> deckList;
    public GameObject GameOver;
    public GameObject[] Shop;
    public Text[] Soul;

    private static UIManager instance = null;

    GameManager gameManager;

    private void Awake()
    {
        if(instance)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        SecSoul();
    }

    public void Shopping()
    {
        if (Shop[0].activeSelf)
            Shop[0].SetActive(false);
        else if (!Shop[0].activeSelf)
            Shop[0].SetActive(true);
    }

    public void SecSoul()
    {
        Soul[0].text = gameManager.money.ToString();
    }
}

[Serializable]
public struct HpBarUI
{
    public Slider hpBar;
    public Gradient gradient;
    public Image fill;

    float imsi;

    public void SetMaxHP()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.playerAI.curHP = gameManager.playerAI.maxHP;
        hpBar.value = 1f;
        fill.color = gradient.Evaluate(1f);
    }

    public void UpdateHP(int curHP, int maxHP)
    {
        imsi = (float)curHP / (float)maxHP;

        hpBar.value = Mathf.Lerp(hpBar.value, imsi, Time.deltaTime * 10);

        fill.color = gradient.Evaluate(hpBar.normalizedValue);
    }
}

[Serializable]
public struct SlotsUI
{
    public List<GameObject> inventorys;
    public List<GameObject> equipments;
}