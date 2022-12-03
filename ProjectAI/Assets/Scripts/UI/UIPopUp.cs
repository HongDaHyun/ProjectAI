using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopUp : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed;
    public float activeTime;

    GameManager gameManager;
    CanvasGroup canvasGroup;

    Text text;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        canvasGroup = GetComponent<CanvasGroup>();
        text = transform.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        transform.Translate(new Vector2(0, - moveSpeed * Time.deltaTime));
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
