using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnManager : MonoBehaviour
{
    private static BtnManager instance = null;
    GameManager gameManager;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        ShopBtn();
    }

    #region KeyBoard

    public void ShopBtn()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            gameManager.uiManager.Shopping();
        }
    }

    #endregion

    #region Click

    public void CardBoxClk()
    {
        if (gameManager.uiManager.isInventroyFull)
            return;

        Price price = gameManager.csvReader.PriceExportData("MonsterBox");
        if (gameManager.money < price.price)
            return;

        gameManager.soundManager.SFXPlayer(SoundManager.SFX.CardOpen);
        gameManager.uiManager.Shop[2].SetActive(true);
        gameManager.money -= price.price;

        for (int i = 0; i < gameManager.uiManager.deckList.Count; i++)
        {
            gameManager.uiManager.deckList[i].SetActive(true);
            gameManager.uiManager.deckList[i].GetComponent<Card>().SetCard();
        }
    }

    #endregion
}
