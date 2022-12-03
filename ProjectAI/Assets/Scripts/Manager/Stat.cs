using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct PlayerStat
{
    public string name;
    public PlayerAmmo[] ammo;
    public SkillStat[] skill;

    public int hp;
    public int exp;
    public int damage;

    public float walkSpeed;
    public float attackSpeed;
    public float bulletSpeed;
    public float reloadSpeed;
}

[Serializable]
public struct PlayerAmmo
{
    public string name;
    public int ammo;
    public int curAmmo;
    public int damage;
    public int range;
    public float spread;
    public float atkSpeed;
    public float bulletSpeed;
    public float reloadSpeed;
    public int level;
    public float curShotTime;
    public bool isReload, isEquip;
}

[Serializable]
public struct MonsterStat
{
    public string name;

    public int hp;
    public int damage;
    public int exp;
    public int count;

    public float moveSpeed;
    public float range;
    public float atkDelay;
    public float coolTime;

    public MonsterType type;
    public Tier tier;
}

[Serializable]
public struct Percentage
{
    public Tier tier;
    public int weight;
}

[Serializable]
public struct Price
{
    public string name;
    public int price;
}

[Serializable]
public struct SkillStat
{
    public string name;
    public int level;
    public float coolTime;
    public float activeTime;
    public bool isActive, isEquip;
}

[Serializable]
public struct SpriteStat
{
    public string name;
    public Sprite sprite;
}

[Serializable]
public class List
{
    public PlayerStat[] playerStats;
    public PlayerAmmo[] playerAmmos;
    public MonsterStat[] monsterStats;
    public Percentage[] percentages;
    public Price[] prices;
    public SkillStat[] skills;
    public SpriteStat[] spriteStats;
}