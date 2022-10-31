using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falcon : Projectile
{
    public GameObject target;
    [HideInInspector]public CapsuleCollider2D col;

    protected override void Awake()
    {
        base.Awake();
        col = GetComponent<CapsuleCollider2D>();
    }

    protected override void Update()
    {
        Falcon(this);
        Debug.Log(target);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
