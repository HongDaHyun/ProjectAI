using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Monster
{
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Follow();
    }
}
