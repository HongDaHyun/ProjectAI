using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : Atk
{
    CapsuleCollider2D col;

    public override void Awake()
    {
        base.Awake();
        col = GetComponent<CapsuleCollider2D>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        col.enabled = false;
    }

    public void ActiveCol()
    {
        col.enabled = true;
    }
}
