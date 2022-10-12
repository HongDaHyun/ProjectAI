using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void FixedUpdate()
    {
        transform.position = gameManager.playerAI.transform.position + new Vector3(0, 0, -10);
    }
}
