using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public Camera UICam;
    public HpBarUI hpBarUI;
    public ExpBarUI expBarUI;
    public SlotsUI slotsUI;
    public List<GameObject> deckList;
    public GameObject GameOver;
    public GameObject pause;
    public GameObject[] Shop;
    public Text[] Soul;
    public Timer timer;

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
        if(!gameManager.isGameOver)
        {
            timer.UpdateTimer();
        }
    }

    public void SecSoul()
    {
        Soul[0].text = gameManager.money.ToString();
    }

    int sell;
    public void Dump(Tier tier, RectTransform rect)
    {
        switch(tier)
        {
            case Tier.Common:
                sell = 1;
                break;
            case Tier.Rare:
                sell = 2;
                break;
            case Tier.Epic:
                sell = 3;
                break;
            case Tier.Legend:
                sell = 5;
                break;
        }
        gameManager.money += sell;

        GameObject obj = gameManager.poolingManager.MakeObj("UIPopUp");
        obj.transform.SetParent(rect.transform);
        obj.transform.localScale = new Vector3(1, 1, 1);
        obj.GetComponent<RectTransform>().position = rect.position;
        StartCoroutine(obj.GetComponent<UIPopUp>().Pop(sell));
    }

    public void Pause(bool active)
    {
        if (active)
            pause.SetActive(true);
        else if (!active)
            pause.SetActive(false);
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
        gameManager.playerAI.curHP = gameManager.playerAI.stat.hp;
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
public struct ExpBarUI
{
    public Slider expBar;
    public Image fill;
    public Text levelTxt;

    float imsi;

    public void SetExp()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.playerAI.curExp = 0;
        expBar.value = 0f;
    }

    public void UpdateExp(PlayerAI ai)
    {
        imsi = (float)ai.curExp / (float)ai.stat.exp;

        expBar.value = Mathf.Lerp(expBar.value, imsi, Time.deltaTime * 10);

        if (ai.curExp >= ai.stat.exp)
        {
            ai.curExp -= ai.stat.exp;
            ai.level++;
            ai.stat.exp = (ai.level * ai.level + ai.level) * 5;
            levelTxt.text = ai.level.ToString();
        }
    }
}

[Serializable]
public struct SlotsUI
{
    public List<ItemSlot> inventorys;
    public List<ItemSlot> equipments;
    public bool isInventroyFull, isEquipmentFull;

    public void FullSlot()
    {
        for (int i = 0; i < inventorys.Count; i++)
        {
            if (!inventorys[i].isDropped)
            {
                isInventroyFull = false;
                break;
            }
            isInventroyFull = true;
            
        }
        for (int i = 0; i < equipments.Count; i++)
        {
            if (!equipments[i].isDropped)
            {
                isEquipmentFull = false;
                break;
            }
            isEquipmentFull = true;
        }
    }

    public void Evolution()
    {
        int k = 0;
        for(int i = 0; i < inventorys.Count; i++)
        {
            for(int j = 0; j < inventorys.Count; j++)
            {
                if (inventorys[i].stat.name == inventorys[j].stat.name && inventorys[i].stat.name != "" && i != j)
                    k++;
            }
            if(k >= 3)
            {
                Debug.Log("��ȭ " + inventorys[i].stat.name);
            }
            k = 0;
        }
    }
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