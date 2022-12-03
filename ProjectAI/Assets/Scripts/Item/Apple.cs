using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Item
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collision.CompareTag("King"))
        {
            gameManager.soundManager.SFXPlayer(SoundManager.SFX.ItemPick);
            gameManager.uiManager.UIPopUpApple(1);
            gameObject.SetActive(false);
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(DestroyAnim(10f));
    }
}
