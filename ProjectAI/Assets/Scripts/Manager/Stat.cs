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
public class List
{
    public PlayerStat[] playerStats;
    public MonsterStat[] monsterStats;
}