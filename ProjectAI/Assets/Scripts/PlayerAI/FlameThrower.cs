using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : Projectile
{
    BoxCollider2D col;

    protected override void Awake()
    {
        base.Awake();
        col = GetComponent<BoxCollider2D>();
    }

    protected override void Update()
    {
        ShootFlame();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }

    public void ActiveCol()
    {
        col.enabled = (col.enabled == false) ? true : false;
    }
}
