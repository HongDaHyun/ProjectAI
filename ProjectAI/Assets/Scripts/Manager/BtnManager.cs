using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnManager : MonoBehaviour
{
    private static BtnManager instance = null;
    GameManager gameManager;
    public static float originTime = 1.5f;

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
        TestWeaponLevel();
        IsSkill();
        AnimalProtector();
    }

    #region KeyBoard

    public void ShopBtn()
    {
        if (gameManager.isGameOver || gameManager.isSkill)
            return;
        if(Input.GetKeyDown(KeyCode.P))
        {
            ShopClk();
        }
    }
    public void TestWeaponLevel()
    {
        KeyCode[] keys = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7 };

        for(int i = 0; i < keys.Length; i++)
        {
            if (Input.GetKeyDown(keys[i]))
            {
                if (i == 3)
                {
                    if (gameManager.playerAI.stat.ammo[3].level >= 4)
                        gameManager.playerAI.stat.ammo[3].level = 0;
                    else
                        gameManager.playerAI.stat.ammo[3].level++;
                }
                else
                {
                    if (gameManager.playerAI.stat.ammo[i].level > 0)
                        gameManager.playerAI.stat.ammo[i].level--;
                    else
                        gameManager.playerAI.stat.ammo[i].level++;
                }
            }
        }
    }
    public void IsSkill()
    {
        if(gameManager.uiManager.Shop[0].GetBool("IsShow") || gameManager.isGameOver)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(gameManager.isSkill)
            {
                gameManager.isSkill = false;
                Time.timeScale = originTime;
            }
            else if(!gameManager.isSkill)
            {
                gameManager.isSkill = true;
                Time.timeScale = originTime / 2;
            }
        }
    }
    public void AnimalProtector()
    {
        if(gameManager.isSkill)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameManager.poolingManager.MakeObj("AnimalProtector");
                gameManager.isSkill = false;
                Time.timeScale = originTime;
            }
        }
    }

    #endregion

    #region Click

    public void ShopClk()
    {
        if (gameManager.uiManager.Shop[0].GetBool("IsShow"))
        {
            gameManager.uiManager.Shop[0].SetBool("IsShow", false);
        }
        else if (!gameManager.uiManager.Shop[0].GetBool("IsShow"))
        {
            gameManager.uiManager.Shop[0].SetBool("IsShow", true);
        }
    }

    public void DeckExit()
    {
        gameManager.uiManager.Shop[1].SetBool("IsShow", false);
        gameManager.uiManager.Pause(false);
    }

    public void CardBoxClk()
    {
        if (gameManager.uiManager.slotsUI.isInventroyFull)
            return;

        Price price = gameManager.csvReader.PriceExportData("MonsterBox");
        if (gameManager.money < price.price)
            return;

        gameManager.soundManager.SFXPlayer(SoundManager.SFX.CardOpen);
        gameManager.uiManager.Shop[1].SetBool("IsShow", true);
        gameManager.uiManager.Pause(true);
        gameManager.money -= price.price;

        for (int i = 0; i < gameManager.uiManager.deckList.Count; i++)
        {
            if(!gameManager.uiManager.deckList[i].activeSelf)
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
        Time.timeScale = originTime;
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
