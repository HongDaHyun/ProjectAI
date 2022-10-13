using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class CSVReader : MonoBehaviour
{
    private static CSVReader _instance;
    public TextAsset[] textAssets;

    public List list = new List();

    public static CSVReader Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(CSVReader)) as CSVReader;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    #region ReadCSV
    public void PlayerReadCSV()
    {
        int order = 0; int size = 6;
        string[] data = textAssets[order].text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / size - 1;
        list.playerStats = new PlayerStat[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            list.playerStats[i] = new PlayerStat();
            list.playerStats[i].name = data[size * (i + 1)];
            list.playerStats[i].hp = int.Parse(data[size * (i + 1) + 1]);
            list.playerStats[i].damage = int.Parse(data[size * (i + 1) + 2]);
            list.playerStats[i].walkSpeed = float.Parse(data[size * (i + 1) + 3]);
            list.playerStats[i].attackSpeed = float.Parse(data[size * (i + 1) + 4]);
            list.playerStats[i].bulletSpeed = float.Parse(data[size * (i + 1) + 5]);
        }
    }

    public void MonsterReadCSV()
    {
        int order = 1;  int size = 7;
        string[] data = textAssets[order].text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / size - 1;
        list.monsterStats = new MonsterStat[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            list.monsterStats[i] = new MonsterStat();
            list.monsterStats[i].name = data[size * (i + 1)];
            list.monsterStats[i].hp = int.Parse(data[size * (i + 1) + 1]);
            list.monsterStats[i].damage = int.Parse(data[size * (i + 1) + 2]);
            list.monsterStats[i].moveSpeed = float.Parse(data[size * (i + 1) + 3]);
            list.monsterStats[i].range = float.Parse(data[size * (i + 1) + 4]);
            list.monsterStats[i].atkDelay = float.Parse(data[size * (i + 1) + 5]);
            list.monsterStats[i].type = (MonsterType)Enum.Parse(typeof(MonsterType), (data[size * (i + 1) + 6]));
        }
    }

    public void PriceReadCSV()
    {
        int order = 2; int size = 2;
        string[] data = textAssets[order].text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / size - 1;
        list.prices = new Price[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            list.prices[i] = new Price();
            list.prices[i].name = data[size * (i + 1)];
            list.prices[i].price = int.Parse(data[size * (i + 1) + 1]);
        }
    }

    public void CardDeck()
    {
        int size = list.monsterStats.Length;
        list.cardStats = new CardStat[size];

        for (int i = 0; i < size; i++)
        {
            list.cardStats[i] = new CardStat();

            list.cardStats[i].name = list.monsterStats[i].name;
            list.cardStats[i].dmg = list.monsterStats[i].damage;
            list.cardStats[i].hp = list.monsterStats[i].hp;
            list.cardStats[i].atkType = list.monsterStats[i].type;
        }
    }
    #endregion
}