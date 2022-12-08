using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : Monster
{
    protected override void Update()
    {
        base.Update();
        if (Vector2.Distance(transform.position, gameManager.playerAI.transform.position) <= stat.range && !isDie)
            anim.SetBool("IsMove", false);
        else if (Vector2.Distance(transform.position, gameManager.playerAI.transform.position) > stat.range)
            anim.SetBool("IsMove", true);
    }

    protected override IEnumerator Damaged(Projectile projectile, float stun)
    {
        isHit = true;
        DmgPopUp(projectile.dmg);
        curHP -= projectile.dmg;
        anim.SetTrigger("IsHit");
        sprite.color = Color.red;
        yield return new WaitForSeconds(stun);
        sprite.color = Color.white;
        isHit = false;
    }

    protected override void AttackPattern()
    {
        if (!isAttack && Vector2.Distance(transform.position, gameManager.playerAI.transform.position) <= stat.range && !isDie)
        {
            StartCoroutine(Banana());
        }
    }
}
