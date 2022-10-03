using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Monster
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !playerAI.isHit)
        {
            StartCoroutine(Attack());
        }
    }

    private void Update()
    {
        LookAt();
        Follow(1f);
    }
}