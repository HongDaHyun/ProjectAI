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
        GameObject test = gameManager.poolingManager.MakeObj("Test");
        test.transform.position = new Vector3(6, 0, 0);
        test = gameManager.poolingManager.MakeObj("Test");
        test.transform.position = new Vector3(-6, 0, 0);
    }
}
