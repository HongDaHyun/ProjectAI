using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : Map
{
    protected override void Awake()
    {
        base.Awake();
        Generate(name);
    }
}
