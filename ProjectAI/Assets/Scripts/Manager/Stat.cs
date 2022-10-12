using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct PlayerStat
{
    public string name;

    public int hp;
    public int damage;

    public float walkSpeed;
    public float attackSpeed;
    public float bulletSpeed;
}

[Serializable]
public struct MonsterStat
{
    public string name;

    public int hp;
    public int damage;

    public float moveSpeed;
    public float range;
    public float atkSpeed;

    public MonsterType type;
}

[Serializable]
public struct CardStat
{
    public string name;

    public int dmg;
    public int hp;

    public MonsterType atkType;
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
    public MonsterStat[] monsterStats;
    public CardStat[] cardStats;
    public SpriteStat[] spriteStats;
}