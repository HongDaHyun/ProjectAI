using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public SkillStat stat;

    GameManager gameManager;
    Image img;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        img = GetComponent<Image>();
    }

    public void SetUI(SkillStat skillStat)
    {
        stat = skillStat;
        img.sprite = gameManager.csvReader.spriteExportData(stat.name).sprite;
    }
}
