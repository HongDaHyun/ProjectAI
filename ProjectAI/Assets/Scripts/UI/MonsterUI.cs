using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUI : MonoBehaviour
{
    public string _name;

    GameManager gameManager;
    Image img;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        img = GetComponent<Image>();
    }

    public void SetUI(string name)
    {
        _name = name;
        img.sprite = gameManager.csvReader.spriteExportData(_name).sprite;
    }
}
