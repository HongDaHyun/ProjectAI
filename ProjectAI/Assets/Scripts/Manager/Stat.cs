using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public enum Type { Touch, Shoot }

[System.Serializable]
public struct MonsterStat
{
    public Type type;

    public int damage;

    public float moveSpeed;
}