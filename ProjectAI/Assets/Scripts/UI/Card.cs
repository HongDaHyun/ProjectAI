using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    public int id;

    public MonsterStat monsterStat;
    public CardUI cardUI;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        SetCardUI();
    }

    private void OnMouseEnter()
    {
        gameObject.transform.localScale = new Vector3(8.5f, 8.5f, 1);
    }

    private void OnMouseExit()
    {
        gameObject.transform.localScale = new Vector3(8f, 8f, 1);
    }

    private void OnMouseUp()
    {
        gameManager.soundManager.SFXPlayer(SoundManager.SFX.SelectCard);
        ActiveFalseCard();
        InstantiateMonsterUI();
    }

    public void SetCard()
    {
        monsterStat = MakeRanCard(RanCard());
        SpriteStat spriteStat = gameManager.csvReader.spriteExportData(monsterStat.name);
        SpriteStat atkSpriteStat = gameManager.csvReader.spriteExportData(monsterStat.type.ToString());

        cardUI.name.text = monsterStat.name;
        cardUI.hp.text = monsterStat.hp.ToString();
        cardUI.atk.text = monsterStat.damage.ToString();
        cardUI.img.sprite = spriteStat.sprite;
        cardUI.atkType.sprite = atkSpriteStat.sprite;
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
        cardUI.name = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        cardUI.img = transform.GetChild(1).GetComponent<Image>();
        GameObject description = transform.GetChild(2).gameObject;
        cardUI.atk = description.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        cardUI.hp = description.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        cardUI.atkType = description.transform.GetChild(2).GetComponent<Image>();
    }

    void ActiveFalseCard()
    {
        for (int i = 0; i < gameManager.uiManager.deckList.Count; i++)
            gameManager.uiManager.deckList[i].SetActive(false);
        gameManager.uiManager.Shop[2].SetActive(false);
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
    public TextMeshProUGUI name;
    public Image img;
    public TextMeshProUGUI atk;
    public TextMeshProUGUI hp;
    public Image atkType;
}