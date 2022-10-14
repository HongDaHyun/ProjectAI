using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterUI : MonoBehaviour
{
    public MonsterStat stat;

    GameManager gameManager;
    Image img;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        img = GetComponent<Image>();
    }

    private void Start()
    {
        if (stat.name != null)
            SetUI();
    }

    void SetUI()
    {
        SpriteStat sprite = gameManager.csvReader.spriteExportData(stat.name);
        img.sprite = sprite.sprite;
    }
}
