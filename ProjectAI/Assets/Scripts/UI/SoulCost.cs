using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulCost : MonoBehaviour
{
    public string _name;
    Text text;
    GameManager gameManager;

    private void Awake()
    {
        text = GetComponent<Text>();
        gameManager = GameManager.Instance;
        for(int i = 0; i < gameManager.csvReader.list.prices.Length; i++)
        {
            if (_name == gameManager.csvReader.list.prices[i].name)
                text.text = gameManager.csvReader.list.prices[i].price.ToString();
        }
    }
}
