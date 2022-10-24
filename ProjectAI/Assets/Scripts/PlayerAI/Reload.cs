using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    public Vector2 startPos, endPos;
    Transform gauge;
    GameManager gameManager;

    public float curReload;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        gauge = transform.GetChild(0);
    }

    private void OnEnable()
    {
        StartCoroutine(Reloading());
    }

    IEnumerator Reloading()
    {
        curReload = 0;
        gauge.localPosition = startPos;
        gameManager.soundManager.SFXPlayer(SoundManager.SFX.Reload);

        while (curReload < gameManager.playerAI.stat.reloadSpeed)
        {
            curReload += Time.deltaTime;
            if (curReload >= gameManager.playerAI.stat.reloadSpeed)
                curReload = gameManager.playerAI.stat.reloadSpeed;
            gauge.localPosition = Vector2.Lerp(startPos, endPos, curReload / gameManager.playerAI.stat.reloadSpeed);
            yield return new WaitForFixedUpdate();
        }
        gameManager.playerAI.curBulletNum = gameManager.playerAI.stat.bulletNum;
        gameManager.playerAI.isReload = false;
        gameObject.SetActive(false);
    }

}
