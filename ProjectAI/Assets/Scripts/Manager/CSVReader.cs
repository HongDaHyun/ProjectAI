using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class CSVReader : MonoBehaviour
{
    private static CSVReader _instance;
    public TextAsset[] textAssets;
    [HideInInspector]public int percentTotal;

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
        for (int i = 0; i < list.percentages.Length; i++)
            percentTotal += list.percentages[i].weight;
    }

    #region ReadCSV
    public void AmmoReadCSV()
    {
        int order = 0; int size = 8;
        string[] data = textAssets[order].text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / size - 1;
        list.playerAmmos = new PlayerAmmo[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            list.playerAmmos[i] = new PlayerAmmo();
            list.playerAmmos[i].name = data[size * (i + 1)];
            list.playerAmmos[i].ammo = int.Parse(data[size * (i + 1) + 1]);
            list.playerAmmos[i].curAmmo = list.playerAmmos[i].ammo;
            list.playerAmmos[i].damage = int.Parse(data[size * (i + 1) + 2]);
            list.playerAmmos[i].range = int.Parse(data[size * (i + 1) + 3]);
            list.playerAmmos[i].spread = float.Parse(data[size * (i + 1) + 4]);
            list.playerAmmos[i].atkSpeed = float.Parse(data[size * (i + 1) + 5]);
            list.playerAmmos[i].bulletSpeed = float.Parse(data[size * (i + 1) + 6]);
            list.playerAmmos[i].reloadSpeed = float.Parse(data[size * (i + 1) + 7]);
        }
    }

    public void PlayerReadCSV()
    {
        int order = 1; int size = 9;
        string[] data = textAssets[order].text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / size - 1;
        list.playerStats = new PlayerStat[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            list.playerStats[i] = new PlayerStat();
            list.playerStats[i].name = data[size * (i + 1)];
            list.playerStats[i].ammo = list.playerAmmos;
            list.playerStats[i].skill = list.skills;
            list.playerStats[i].hp = int.Parse(data[size * (i + 1) + 2]);
            list.playerStats[i].exp = int.Parse(data[size * (i + 1) + 3]);
            list.playerStats[i].damage = int.Parse(data[size * (i + 1) + 4]);
            list.playerStats[i].walkSpeed = float.Parse(data[size * (i + 1) + 5]);
            list.playerStats[i].attackSpeed = float.Parse(data[size * (i + 1) + 6]);
            list.playerStats[i].bulletSpeed = float.Parse(data[size * (i + 1) + 7]);
            list.playerStats[i].reloadSpeed = float.Parse(data[size * (i + 1) + 8]);
        }
    }

    public void MonsterReadCSV()
    {
        int order = 2;  int size = 12;
        string[] data = textAssets[order].text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / size - 1;
        list.monsterStats = new MonsterStat[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            list.monsterStats[i] = new MonsterStat();
            list.monsterStats[i].name = data[size * (i + 1)];
            list.monsterStats[i].hp = int.Parse(data[size * (i + 1) + 1]);
            list.monsterStats[i].damage = int.Parse(data[size * (i + 1) + 2]);
            list.monsterStats[i].exp = int.Parse(data[size * (i + 1) + 3]);
            list.monsterStats[i].count = int.Parse(data[size * (i + 1) + 4]);
            list.monsterStats[i].moveSpeed = float.Parse(data[size * (i + 1) + 5]);
            list.monsterStats[i].range = float.Parse(data[size * (i + 1) + 6]);
            list.monsterStats[i].atkDelay = float.Parse(data[size * (i + 1) + 7]);
            list.monsterStats[i].coolTime = float.Parse(data[size * (i + 1) + 8]);
            list.monsterStats[i].type = (MonsterType)Enum.Parse(typeof(MonsterType), (data[size * (i + 1) + 9]));
            list.monsterStats[i].tier = (Tier)Enum.Parse(typeof(Tier), (data[size * (i + 1) + 10]));
            list.monsterStats[i].description = data[size * (i + 1) + 11];
            list.monsterStats[i].synergy = new List<Synergy>();
            for(int k = 0; k < list.synergies.Length; k++)
            {
                if(list.synergies[k].contents.Contains(list.monsterStats[i].name))
                {
                    list.monsterStats[i].synergy.Add(list.synergies[k]);
                }
            }
        }
    }

    public void PriceReadCSV()
    {
        int order = 3; int size = 2;
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

    #endregion

    #region Name 
    public ref PlayerAmmo PlayerAmmoExportData(string name)
    {
        for (int i = 0; i < list.playerAmmos.Length; i++)
        {
            if (name == list.playerAmmos[i].name.ToString())
            {
                return ref list.playerAmmos[i];
            }
        }
        return ref list.playerAmmos[0];
    }

    public ref PlayerStat PlayerExportData(string name)
    {
        for(int i = 0; i < list.playerStats.Length; i++)
        {
            if (name == list.playerStats[i].name)
                return ref list.playerStats[i];
        }
        return ref list.playerStats[0];
    }

    public ref MonsterStat monsterExportData(string name)
    {
        for (int i = 0; i < list.monsterStats.Length; i++)
        {
            if (name == list.monsterStats[i].name)
                return ref list.monsterStats[i];
        }
        return ref list.monsterStats[0];
    }

    public Price PriceExportData(string name)
    {
        for (int i = 0; i < list.prices.Length; i++)
        {
            if (name == list.prices[i].name)
                return list.prices[i];
        }
        return new Price();
    }

    public SkillStat SkillExportData(string name)
    {
        for (int i = 0; i < list.skills.Length; i++)
        {
            if (name == list.skills[i].name)
                return list.skills[i];
        }
        return new SkillStat();
    }

    public SpriteStat spriteExportData(string name)
    {
        for (int i = 0; i < list.spriteStats.Length; i++)
        {
            if (name == list.spriteStats[i].name)
                return list.spriteStats[i];
        }
        return new SpriteStat();
    }

    public ref Synergy SynergyExportData(string name)
    {
        for (int i = 0; i < list.synergies.Length; i++)
        {
            if (name == list.playerStats[i].name)
                return ref list.synergies[i];
        }
        return ref list.synergies[0];
    }
    #endregion
}