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

    protected override IEnumerator TouchAttack()
    {
        playerAI.isHit = true;
        playerAI.curHP -= damage;
        yield return new WaitForSeconds(0.5f);
        playerAI.isHit = false;
    }
}
