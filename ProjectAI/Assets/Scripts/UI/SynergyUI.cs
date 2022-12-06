using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynergyUI : MonoBehaviour
{
    public Text title;
    public GameObject[] imgPannel;
    public Image[] img;

    GameManager gameManager;
    [HideInInspector] public RectTransform rect;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        rect = GetComponent<RectTransform>();
    }

    private void OnDisable()
    {
        for(int i = 0; i < imgPannel.Length; i++)
        {
            imgPannel[i].SetActive(false);
        }
    }

    public void SetSynergy(Synergy synergy)
    {
        title.text = synergy.name;
        for(int i = 0; i < synergy.contents.Count; i++)
        {
            imgPannel[i].SetActive(true);
            img[i].sprite = gameManager.csvReader.spriteExportData(synergy.contents[i]).sprite;
        }
    }
}
