using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Atk : MonoBehaviour
{
    Monster monster;
    public int damage;

    private void Awake()
    {
        monster = GetComponentInParent<Monster>();
        damage = monster.damage;
    }
}
