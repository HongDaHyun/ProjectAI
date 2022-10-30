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
            case "TomahawkReleoad":
                StartCoroutine(TomahawkReloading());
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
        gameManager.playerAI.isLongGunReload = false;
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
        gameManager.playerAI.isShotGunReload = false;
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
        gameManager.playerAI.isTomahawkReload = false;
        gameObject.SetActive(false);
    }
}
