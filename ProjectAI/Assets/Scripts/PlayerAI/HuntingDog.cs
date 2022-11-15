using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingDog : Projectile
{
    SpriteRenderer sprite;

    protected override void Awake()
    {
        base.Awake();
        sprite = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        dmg = gameManager.playerAI.stat.ammo[3].damage + gameManager.playerAI.stat.damage;
        if (transform.position.y > gameManager.playerAI.transform.position.y)
            sprite.flipX = true;
        else if (transform.position.y < gameManager.playerAI.transform.position.y)
            sprite.flipX = false;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
