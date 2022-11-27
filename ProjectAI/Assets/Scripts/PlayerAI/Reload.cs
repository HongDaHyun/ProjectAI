using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    public Vector2 startPos, endPos;

    Transform gauge;
    GameManager gameManager;


    private void Awake()
    {
        gameManager = GameManager.Instance;
        gauge = transform.GetChild(0);
    }

    private void OnEnable()
    {
        switch(name)
        {
            case "LongGunReload":
                StartCoroutine(LongGunReloading());
                break;
            case "ShotGunReload":
                StartCoroutine(ShotGunReloading());
                break;
            case "TomahawkReload":
                StartCoroutine(TomahawkReloading());
                break;
            case "FlameThrowerReload":
                StartCoroutine(FlameThrowerReloading());
                break;
            case "FalconReload":
                StartCoroutine(FalconReloading());
                break;
            case "BombReload":
                StartCoroutine(BombReloading());
                break;
        }
    }

    IEnumerator LongGunReloading()
    {
        float curReload = 0;
        float reloadTime = gameManager.playerAI.stat.reloadSpeed + gameManager.playerAI.stat.ammo[0].reloadSpeed;
        gauge.localPosition = startPos;
        gameManager.soundManager.SFXPlayer(SoundManager.SFX.Reload);

        while (curReload < reloadTime)
        {
            curReload += Time.deltaTime;
            if (curReload >= reloadTime)
                curReload = reloadTime;
            gauge.localPosition = Vector2.Lerp(startPos, endPos, curReload / reloadTime);
            yield return new WaitForFixedUpdate();
        }
        gameManager.playerAI.stat.ammo[0].curAmmo = gameManager.playerAI.stat.ammo[0].ammo;
        gameManager.playerAI.stat.ammo[0].isReload = false;
        gameObject.SetActive(false);
    }

    IEnumerator ShotGunReloading()
    {
        float curReload = 0;
        float reloadTime = gameManager.playerAI.stat.reloadSpeed + gameManager.playerAI.stat.ammo[1].reloadSpeed;
        gauge.localPosition = startPos;
        gameManager.soundManager.SFXPlayer(SoundManager.SFX.Reload);

        while (curReload < reloadTime)
        {
            curReload += Time.deltaTime;
            if (curReload >= reloadTime)
                curReload = reloadTime;
            gauge.localPosition = Vector2.Lerp(startPos, endPos, curReload / reloadTime);
            yield return new WaitForFixedUpdate();
        }
        gameManager.playerAI.stat.ammo[1].curAmmo = gameManager.playerAI.stat.ammo[1].ammo;
        gameManager.playerAI.stat.ammo[1].isReload = false;
        gameObject.SetActive(false);
    }

    IEnumerator TomahawkReloading()
    {
        float curReload = 0;
        float reloadTime = gameManager.playerAI.stat.reloadSpeed + gameManager.playerAI.stat.ammo[2].reloadSpeed;
        while (curReload < reloadTime)
        {
            curReload += Time.deltaTime;
            if (curReload >= reloadTime)
                curReload = reloadTime;
            yield return new WaitForFixedUpdate();
        }
        gameManager.playerAI.stat.ammo[2].curAmmo = gameManager.playerAI.stat.ammo[2].ammo;
        gameManager.playerAI.stat.ammo[2].isReload = false;
        gameObject.SetActive(false);
    }

    IEnumerator FlameThrowerReloading()
    {
        float curReload = 0;
        float reloadTime = gameManager.playerAI.stat.reloadSpeed + gameManager.playerAI.stat.ammo[4].reloadSpeed;
        gauge.localPosition = startPos;
        gameManager.soundManager.SFXPlayer(SoundManager.SFX.Reload);

        while (curReload < reloadTime)
        {
            curReload += Time.deltaTime;
            if (curReload >= reloadTime)
                curReload = reloadTime;
            gauge.localPosition = Vector2.Lerp(startPos, endPos, curReload / reloadTime);
            yield return new WaitForFixedUpdate();
        }
        gameManager.playerAI.stat.ammo[4].curAmmo = gameManager.playerAI.stat.ammo[4].ammo;
        gameManager.playerAI.stat.ammo[4].isReload = false;
        gameObject.SetActive(false);
    }

    IEnumerator FalconReloading()
    {
        float curReload = 0;
        float reloadTime = gameManager.playerAI.stat.reloadSpeed + gameManager.playerAI.stat.ammo[5].reloadSpeed;
        while (curReload < reloadTime)
        {
            curReload += Time.deltaTime;
            if (curReload >= reloadTime)
                curReload = reloadTime;
            yield return new WaitForFixedUpdate();
        }
        gameManager.playerAI.stat.ammo[5].curAmmo = gameManager.playerAI.stat.ammo[5].ammo;
        gameManager.playerAI.stat.ammo[5].isReload = false;
        gameObject.SetActive(false);
    }

    IEnumerator BombReloading()
    {
        float curReload = 0;
        float reloadTime = gameManager.playerAI.stat.reloadSpeed + gameManager.playerAI.stat.ammo[6].reloadSpeed;
        while (curReload < reloadTime)
        {
            curReload += Time.deltaTime;
            if (curReload >= reloadTime)
                curReload = reloadTime;
            yield return new WaitForFixedUpdate();
        }
        gameManager.playerAI.stat.ammo[6].curAmmo = gameManager.playerAI.stat.ammo[6].ammo;
        gameManager.playerAI.stat.ammo[6].isReload = false;
        gameObject.SetActive(false);
    }
}
