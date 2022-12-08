using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynergyNotice : MonoBehaviour
{
    GameManager gameManager;

    public Image img;
    public Text title;
    public Text[] contents;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void OnDisable()
    {
        for (int i = 0; i < contents.Length; i++)
            contents[i].gameObject.SetActive(false);
    }

    public void ActiveFalse()
    {
        gameObject.SetActive(false);
    }

    public void SetStat(Synergy synergy)
    {
        img.sprite = gameManager.csvReader.spriteExportData(synergy.name).sprite;
        title.text = synergy.name;

        int i = 0;
        while(i < synergy.increase.Count)
        {
            contents[i].gameObject.SetActive(true);
            contents[i].color = Color.black;
            contents[i].text = synergy.increase[i];
            i++;
        }
        while(i < synergy.increase.Count + synergy.decrease.Count)
        {
            contents[i].gameObject.SetActive(true);
            contents[i].color = Color.red;
            contents[i].text = synergy.decrease[i - synergy.increase.Count];
            i++;
        }
    }
}
