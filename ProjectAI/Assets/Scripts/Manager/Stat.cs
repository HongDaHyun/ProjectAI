using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Stat : MonoBehaviour
{
    public PlayerStat playerStat;
    public MonsterStat monsterStat;
}

[System.Serializable]
public struct PlayerStat
{
    public int hp;
    public int damage;

    public float walkSpeed;
    public float attackSpeed;
    public float bulletSpeed;
}

[System.Serializable]
public struct MonsterStat
{
    public int damage;
}