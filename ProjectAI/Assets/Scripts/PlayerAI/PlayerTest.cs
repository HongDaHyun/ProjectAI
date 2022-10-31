using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : PlayerAI
{
    protected override void SetStat()
    {
        base.SetStat();
        stat.ammo[5].level = 1;
    }
}
