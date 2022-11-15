using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (gameManager.isGameOver)
            return;
        if(Input.GetKeyDown(KeyCode.P))
        {
            ShopClk();
        }
    }

    #endregion

    #region Click

    public void ShopClk()
    {
        if (gameManager.uiManager.Shop[0].activeSelf)
            gameManager.uiManager.Shop[0].SetActive(false);
        else if (!gameManager.uiManager.Shop[0].activeSelf)
            gameManager.uiManager.Shop[0].SetActive(true);
    }

    public void DeckExit()
    {
        gameManager.uiManager.Shop[2].SetActive(false);
    }

    public void CardBoxClk()
    {
        if (gameManager.uiManager.slotsUI.isInventroyFull)
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

    public void TestStart()
    {
        gameManager.networkManager.network.networkPannel.SetActive(false);
        gameManager.isGameOver = false;
        gameManager.soundManager.bgm.Play();
        StartCoroutine(gameManager.SecPerMoney(gameManager.secMoneySize, gameManager.secMoney));
        Time.timeScale = 2f;
    }

    public void WeaponTestBtnUp(int id)
    {
        gameManager.playerAI.stat.ammo[id].level++;
    }

    public void WeaponTestBtnDown(int id)
    {
        gameManager.playerAI.stat.ammo[id].level--;
    }
    #endregion
}
