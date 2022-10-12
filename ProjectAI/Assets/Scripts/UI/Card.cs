using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    public int id;

    public CardUI cardUI;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        SetCardUI();
    }

    private void OnEnable()
    {
        SetCard();
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
        ActiveFalseCard();
        for (int i = 0; i < gameManager.uiManager.slotsUI.inventorys.Count; i++)
        {
            if(!gameManager.uiManager.slotsUI.inventorys[i].GetComponent<ItemSlot>().isDropped)
            {
                gameManager.uiManager.slotsUI.inventorys[i].GetComponent<ItemSlot>().isDropped = true;
                GameObject monsterUI = gameManager.poolingManager.MakeObj("MonsterUI");
                monsterUI.GetComponent<MonsterUI>()._name = cardUI.name.text.ToString();
                monsterUI.transform.parent = gameManager.uiManager.slotsUI.inventorys[i].transform;
                monsterUI.transform.localScale = new Vector3(1, 1, 1);
                monsterUI.transform.position = gameManager.uiManager.slotsUI.inventorys[i].transform.position;
                return;
            }
        }
    }

    void SetCard()
    {
        id = Random.Range(0, gameManager.csvReader.list.monsterStats.Length);
        cardUI.name.text = gameManager.csvReader.list.cardStats[id].name;
        cardUI.hp.text = gameManager.csvReader.list.cardStats[id].hp.ToString();
        cardUI.atk.text = gameManager.csvReader.list.cardStats[id].dmg.ToString();
        for (int i = 0; i < gameManager.csvReader.list.cardStats.Length; i++)
        {
            if (gameManager.csvReader.list.cardStats[id].name == gameManager.csvReader.list.spriteStats[i].name)
                cardUI.img.sprite = gameManager.csvReader.list.spriteStats[i].sprite;
            if (gameManager.csvReader.list.cardStats[id].atkType.ToString() == gameManager.csvReader.list.spriteStats[i].name)
                cardUI.atkType.sprite = gameManager.csvReader.list.spriteStats[i].sprite;
                
        }
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