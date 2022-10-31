using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingDog : Projectile
{
    protected override void Update()
    {
        dmg = gameManager.playerAI.stat.ammo[3].damage + gameManager.playerAI.stat.damage;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
