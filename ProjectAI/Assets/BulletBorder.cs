using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBorder : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    void Update()
    {
        transform.position = gameManager.playerAI.transform.position;
    }
}
