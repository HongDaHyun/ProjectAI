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

    private void Start()
    {
        if (_name != null)
            SetUI();
    }

    void SetUI()
    {
        for(int i = 0; i < gameManager.csvReader.list.spriteStats.Length; i++)
        {
            if (_name == gameManager.csvReader.list.spriteStats[i].name)
                img.sprite = gameManager.csvReader.list.spriteStats[i].sprite;
        }
    }
}
