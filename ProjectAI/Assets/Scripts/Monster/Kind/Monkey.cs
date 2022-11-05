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
}
