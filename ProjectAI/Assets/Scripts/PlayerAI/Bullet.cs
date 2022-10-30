using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    protected override void Update()
    {
        ShootStraight();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        BulletCollision(collision);
        ProjectileBorder(collision);
    }
}
