using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopUp : PopUp
{
    CanvasGroup canvasGroup;
    Text text;

    public override void Awake()
    {
        base.Awake();
        canvasGroup = GetComponent<CanvasGroup>();
        text = transform.GetComponentInChildren<Text>();
    }

    public override void Update()
    {
        base.Update();
        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, Time.deltaTime * alphaSpeed);
    }

    public IEnumerator Pop(int money)
    {
        canvasGroup.alpha = 1;
        text.text = "+" + money;
        yield return new WaitForSeconds(activeTime);
        gameObject.SetActive(false);
    }
}
