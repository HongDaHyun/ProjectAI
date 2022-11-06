using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : Atk
{
    public Monster monster;

    public override void Awake()
    {
        monster = GetComponentInParent<Monster>();
        damage = monster.stat.damage;
    }
}
