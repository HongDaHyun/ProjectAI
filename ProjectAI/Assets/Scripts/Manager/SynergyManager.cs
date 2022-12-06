using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyManager : MonoBehaviour
{
    GameManager gameManager;
    Synergy[] synergy;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        synergy = gameManager.csvReader.list.synergies;
    }
}
