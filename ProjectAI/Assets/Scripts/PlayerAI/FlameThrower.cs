using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : Projectile
{
    Animator anim;
    SpriteRenderer sprite;
    CapsuleCollider2D col;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<CapsuleCollider2D>();
    }

    protected override void Update()
    {
        ShootFlame(sprite);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }

    public void ActiveCol()
    {
        col.enabled = (col.enabled == false) ? true : false;
    }
}
