using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingDog : Projectile
{
    Transform target;
    Vector3 offset;

    private void Start()
    {
        target = gameManager.playerAI.transform;
    }

    protected override void Update()
    {

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    { }
}
