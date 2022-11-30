using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    Animator anim;

    public int id;

    public MonsterStat monsterStat;
    public CardUI cardUI;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        anim = GetComponent<Animator>();
        SetCardUI();
    }

    private void OnEnable()
    {
        anim.enabled = false;
        transform.localScale = new Vector3(1, 1, 1);
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    private void OnMouseEnter()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    private void OnMouseExit()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnMouseUp()
    {
        anim.enabled = true;
        gameManager.soundManager.SFXPlayer(SoundManager.SFX.SelectCard);
        CardClick();
        InstantiateMonsterUI();
    }

    public void SetCard()
    {
        monsterStat = MakeRanCard(RanCard());
        SpriteStat spriteStat = gameManager.csvReader.spriteExportData(monsterStat.name);

        cardUI.charImg.sprite = spriteStat.sprite;
        cardUI.name.text = monsterStat.name;
        SetStar();
    }

    void SetStar()
    {
        switch(monsterStat.tier)
        {
            case Tier.Common:
                cardUI.star.sprite = gameManager.csvReader.spriteExportData("Star1").sprite;
                break;
            case Tier.Rare:
                cardUI.star.sprite = gameManager.csvReader.spriteExportData("Star2").sprite;
                break;
            case Tier.Epic:
                cardUI.star.sprite = gameManager.csvReader.spriteExportData("Star3").sprite;
                break;
            case Tier.Legend:
                cardUI.star.sprite = gameManager.csvReader.spriteExportData("Star4").sprite;
                break;
        }
    }

    Tier RanCard()
    {
        int weight = 0;
        int selectNum = 0;

        selectNum = Mathf.RoundToInt(gameManager.csvReader.percentTotal * Random.Range(0.0f, 1.0f));

        for (int i = 0; i < gameManager.csvReader.list.percentages.Length; i++)
        {
            weight += gameManager.csvReader.list.percentages[i].weight;
            if(selectNum <= weight)
            {
                return gameManager.csvReader.list.percentages[i].tier;
            }
        }
        return 0;
    }

    MonsterStat MakeRanCard(Tier _tier)
    {
        List<MonsterStat> list = new List<MonsterStat>();
        for(int i = 0; i < gameManager.csvReader.list.monsterStats.Length; i++)
        {
            if (_tier == gameManager.csvReader.list.monsterStats[i].tier)
                list.Add(gameManager.csvReader.list.monsterStats[i]);
        }
        int ran = Random.Range(0, list.Count);
        return list[ran];
    }

    void SetCardUI()
    {
        cardUI.charImg = transform.GetChild(0).GetComponent<Image>();
        cardUI.name = transform.GetChild(1).GetComponent<Text>();
        cardUI.star = transform.GetChild(2).GetComponent<Image>();
    }

    void CardClick()
    {
        anim.SetTrigger("Click");
        gameManager.uiManager.Shop[1].SetBool("IsShow", false);
    }

    public void ActiveFalseCard()
    {
        gameObject.SetActive(false);
    }

    void InstantiateMonsterUI()
    {
        for (int i = 0; i < gameManager.uiManager.slotsUI.inventorys.Count; i++)
        {
            if (!gameManager.uiManager.slotsUI.inventorys[i].GetComponent<ItemSlot>().isDropped)
            {
                ItemSlot itemSlot = gameManager.uiManager.slotsUI.inventorys[i].GetComponent<ItemSlot>();
                itemSlot.isDropped = true;
                itemSlot.stat = monsterStat;
                MonsterUI monsterUI = gameManager.poolingManager.MakeObj("MonsterUI").GetComponent<MonsterUI>();
                DragDrop dragDrop = monsterUI.GetComponent<DragDrop>();
                dragDrop.curSlot = itemSlot;
                monsterUI.SetUI(monsterStat);
                monsterUI.transform.SetParent(gameManager.uiManager.slotsUI.inventorys[i].transform);
                monsterUI.transform.localScale = new Vector3(1, 1, 1);
                dragDrop.rect.position = gameManager.uiManager.slotsUI.inventorys[i].rect.position;
                gameManager.uiManager.slotsUI.FullSlot();
                gameManager.uiManager.slotsUI.Evolution();
                return;
            }
        }
    }
}

[System.Serializable]
public struct CardUI
{
    public Text name;
    public Image charImg;
    public Image star;
}