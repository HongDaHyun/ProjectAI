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
        Price price = gameManager.csvReader.PriceExportData(_name);
        text.text = price.price.ToString();
    }
}
