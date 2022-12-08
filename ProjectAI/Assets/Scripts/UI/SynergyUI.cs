using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SynergyUI : MonoBehaviour
{
    public Text title;
    public GameObject[] imgPannel;
    public Image[] imgSquare;
    public Image[] img;
    CanvasGroup canvas;

    GameManager gameManager;
    [HideInInspector] public RectTransform rect;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        rect = GetComponent<RectTransform>();
        canvas = GetComponent<CanvasGroup>();
    }

    private void OnDisable()
    {
        for(int i = 0; i < imgPannel.Length; i++)
        {
            for (int k = 0; k < imgSquare.Length; k++)
            {
                imgSquare[k].color = Color.white;
                img[k].color = Color.black;
            }
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
            if (gameManager.csvReader.monsterExportData(synergy.contents[i]).num > 0)
            {
                imgSquare[i].color = Color.yellow;
                img[i].color = Color.white;
            }
            if (synergy.isUnlock)
                canvas.alpha = 1f;
            else
                canvas.alpha = 0.7f;
        }
    }
}
