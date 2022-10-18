using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Monster
{
    protected override void Update()
    {
        base.Update();
        Follow();
    }
}
