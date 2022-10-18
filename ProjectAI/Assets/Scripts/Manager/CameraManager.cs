using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        transform.position = gameManager.playerAI.transform.position;
    }
}
