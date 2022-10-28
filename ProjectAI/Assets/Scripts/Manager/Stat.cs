using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct PlayerStat
{
    public string name;
    public PlayerAmmo[] ammo;

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
    public PlayerWeapon name;
    public int ammo;
    public int curAmmo;
    public int damage;
}

[Serializable]
public struct MonsterStat
{
    public string name;

    public int hp;
    public int damage;
    public int exp;

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
    public SpriteStat[] spriteStats;
}