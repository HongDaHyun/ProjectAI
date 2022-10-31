using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : Projectile
{
    protected override void Update()
    {
        ShootFlame();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
