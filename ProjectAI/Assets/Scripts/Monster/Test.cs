using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Monster
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !playerAI.isHit)
        {
            StartCoroutine(TouchAttack());
        }
    }

    private void Update()
    {
        Follow(playerAI.gameObject);
    }
}
