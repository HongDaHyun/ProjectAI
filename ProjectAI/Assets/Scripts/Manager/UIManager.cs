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
    public RectTransform weaponSlotPanel;
    public RectTransform skillSlotPanel;
    public RectTransform popAppleRect;
    public RectTransform mouseCursorUI;
    public List<GameObject> deckList;
    public GameObject GameOver;
    public GameObject pause;
    public GameObject toolTip;
    public Animator invenPannel;
    public Animator[] Shop;
    public Text[] Soul;
    public Timer timer;
    public Vector3 mousePos;

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
        CurMousePos();
    }

    public void WeaponSlotCheck()
    {
        for(int i = 0; i < gameManager.playerAI.stat.ammo.Length; i++)
        {
            if (gameManager.playerAI.stat.ammo[i].level == 1 && !gameManager.playerAI.stat.ammo[i].isEquip)
            {
                WeaponSlot weaponSlot = gameManager.poolingManager.MakeObj("WeaponSlot").GetComponent<WeaponSlot>();
                weaponSlot.rect.SetParent(weaponSlotPanel);
                weaponSlot.rect.position = weaponSlotPanel.position;
                weaponSlot.rect.localScale = new Vector3(1, 1, 1);
                weaponSlot._name = gameManager.playerAI.stat.ammo[i].name;
                weaponSlot.weaponImg.sprite = gameManager.csvReader.spriteExportData(weaponSlot._name).sprite;
            }
            else if (gameManager.playerAI.stat.ammo[i].level == 0)
            {
                WeaponSlot[] slots = weaponSlotPanel.GetComponentsInChildren<WeaponSlot>();
                for(int k = 0; k < slots.Length; k++)
                {
                    if (slots[k]._name == gameManager.playerAI.stat.ammo[i].name)
                        slots[k].gameObject.SetActive(false);
                }
            }
        }
    }

    public void InstantiateSkill(SkillStat skillStat)
    {
        for (int i = 0; i < gameManager.playerAI.stat.skill.Length; i++)
        {
            if (gameManager.playerAI.stat.skill[i].level == 1 && !gameManager.playerAI.stat.skill[i].isEquip)
            {
                gameManager.playerAI.stat.skill[i].isEquip = true;
                for (int j = 0; j < gameManager.uiManager.slotsUI.skills.Count; j++)
                {
                    if (!gameManager.uiManager.slotsUI.skills[j].GetComponent<SkillSlot>().isDropped)
                    {
                        SkillSlot skillSlot = gameManager.uiManager.slotsUI.skills[j].GetComponent<SkillSlot>();
                        skillSlot.isDropped = true;
                        skillSlot.skillStat = skillStat;
                        SkillUI skillUI = gameManager.poolingManager.MakeObj("SkillUI").GetComponent<SkillUI>();
                        DragDrop dragDrop = skillUI.GetComponent<DragDrop>();
                        dragDrop.curSlot = skillSlot;
                        skillUI.SetUI(skillStat);
                        skillUI.transform.SetParent(gameManager.uiManager.slotsUI.skills[j].transform);
                        skillUI.transform.localScale = new Vector3(1, 1, 1);
                        skillUI.transform.SetAsFirstSibling();
                        dragDrop.rect.position = gameManager.uiManager.slotsUI.skills[j].rect.position;
                        gameManager.uiManager.slotsUI.FullSlot();
                        gameManager.uiManager.slotsUI.Evolution();
                        return;
                    }
                }

            }
            else if (gameManager.playerAI.stat.skill[i].level == 0)
            {
                gameManager.playerAI.stat.skill[i].isEquip = false;
                SkillSlot[] slots = skillSlotPanel.GetComponentsInChildren<SkillSlot>();
                for (int k = 0; k < slots.Length; k++)
                {
                    if (slots[k].skillStat.name == gameManager.playerAI.stat.skill[i].name)
                    {
                        slots[k].skillStat = new SkillStat();
                        slots[k].isDropped = false;
                        GameObject skillUI = slots[k].transform.GetChild(0).gameObject;
                        skillUI.transform.SetParent(transform.root);
                        skillUI.SetActive(false);
                    }
                }
            }
        }
    }

    public void SecSoul()
    {
        Soul[0].text = gameManager.money.ToString();
    }

    public void UIPopUpApple(int count)
    {
        gameManager.money += count;

        GameObject obj = gameManager.poolingManager.MakeObj("UIPopUp");
        obj.transform.SetParent(popAppleRect.transform);
        obj.transform.localScale = new Vector3(0.8f, 0.8f, 1);
        obj.GetComponent<RectTransform>().position = popAppleRect.position;
        StartCoroutine(obj.GetComponent<UIPopUp>().Pop(count));
    }

    int sell;
    public int TierForSale(Tier tier)
    {
        switch (tier)
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
        return sell;
    }

    public void Pause(bool active)
    {
        if (active)
            pause.SetActive(true);
        else if (!active)
            pause.SetActive(false);
    }

    private void CurMousePos()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
        Input.mousePosition.y, -Camera.main.transform.position.z));
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
    public List<EquipSlot> equipments;
    public List<SkillSlot> skills;
    public bool isInventroyFull, isEquipmentFull, isSkillFull;

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
        for (int i = 0; i < skills.Count; i++)
        {
            if (!skills[i].isDropped)
            {
                isSkillFull = false;
                break;
            }
            isSkillFull = true;
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