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
    public Timer timer;

    public bool isInventroyFull, isEquipmentFull;

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

    private void Start()
    {
        timer.SetTimer();
    }

    private void Update()
    {
        SecSoul();
        FullSlot();
        timer.UpdateTimer();
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

    public void FullSlot()
    {
        for(int i = 0; i < slotsUI.inventorys.Count; i++)
        {
            if (!slotsUI.inventorys[i].GetComponent<ItemSlot>().isDropped)
                isInventroyFull = false;
            else
                isInventroyFull = true;
        }
        for (int i = 0; i < slotsUI.equipments.Count; i++)
        {
            if (!slotsUI.equipments[i].GetComponent<ItemSlot>().isDropped)
                isEquipmentFull = false;
            else
                isEquipmentFull = true;
        }
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

[Serializable]
public struct Timer
{
    public Text timerUI;
    public float setTime;
    public Gradient gradient;
    public float curTime;
    public float min, sec;

    public void SetTimer()
    {
        curTime = setTime;
        timerUI.color = gradient.Evaluate(1f);
    }

    public void UpdateTimer()
    {
        if (curTime > 0)
            curTime -= Time.deltaTime;
        else if (curTime == 0)
            curTime = 0f;

        sec = (int)(curTime % 60);
        min = (int)(curTime / 60 % 60);
        timerUI.text = string.Format("{0:0}:{1:0}", min, sec);
        timerUI.color = gradient.Evaluate(curTime / setTime);
    }
}