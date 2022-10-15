using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlot : ItemSlot
{
    public bool isSpawn;

    private void Update()
    {
        StartCoroutine(gameManager.spawnManager.Spawn(this));
    }
}
