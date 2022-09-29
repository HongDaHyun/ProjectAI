using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : PlayerAI
{
    public override void Update()
    {
        base.Update();
        Detecting(5f);
        curShotTime += Time.deltaTime;
    }
}
