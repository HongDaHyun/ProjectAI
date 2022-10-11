using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void Start()
    {
    }

    private void SpawnMonster(string name)
    {
        GameObject monster = gameManager.poolingManager.MakeObj(name);
        monster.transform.position = new Vector3(6, 0, 0);
    }
}
