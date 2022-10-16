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

    public void SetUI(MonsterStat monsterStat)
    {
        stat = monsterStat;
        img.sprite = gameManager.csvReader.spriteExportData(stat.name).sprite;

    }
}
