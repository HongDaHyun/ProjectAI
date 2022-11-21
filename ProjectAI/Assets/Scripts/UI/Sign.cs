using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void OnPause()
    {
        gameManager.uiManager.Pause(true);
    }

    public void OffPause()
    {
        gameManager.uiManager.Pause(false);
    }
}
