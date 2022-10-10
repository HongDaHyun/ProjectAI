using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Camera UICam;
    public HpBarUI hpBarUI;
    public GameObject GameOver;
    public GameObject[] Shop;

    private static UIManager instance = null;

    GameManager gameManager;

    private void Awake()
    {
        if(instance)
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
        if (gameManager.playerAI.isDead)
        {
            gameManager.playerAI.isDead = false;
            GameOver.SetActive(true);
        } //게임오버 조건 반대임(수정해야함)
    }

    public void Shopping()
    {
        if (Shop[0].activeSelf)
            Shop[0].SetActive(false);
        else if (!Shop[0].activeSelf)
            Shop[0].SetActive(true);
    }
}

[System.Serializable]
public struct HpBarUI
{
    public Slider hpBar;
    public Gradient gradient;
    public Image fill;

    float imsi;

    public void SetMaxHP()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.playerAI.curHP = gameManager.playerAI.maxHP;
        hpBar.value = 1f;
        fill.color = gradient.Evaluate(1f);
    }

    public void UpdateHP(int curHP, int maxHP)
    {
        imsi = (float)curHP / (float)maxHP;

        hpBar.value = Mathf.Lerp(hpBar.value, imsi, Time.deltaTime * 10);

        fill.color = gradient.Evaluate(hpBar.normalizedValue);
    }
}