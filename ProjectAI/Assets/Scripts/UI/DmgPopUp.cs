using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DmgPopUp : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed;
    public float activeTime;

    GameManager gameManager;
    TextMeshPro text;
    Color alpha;


    private void Awake()
    {
        gameManager = GameManager.Instance;
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
    }

    private void OnEnable()
    {
        StartCoroutine(Pop());
    }

    private void Update()
    {
        transform.Translate(new Vector2(0, moveSpeed * Time.deltaTime));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    IEnumerator Pop()
    {
        alpha.a = 1;
        text.text = gameManager.playerAI.stat.damage.ToString();
        yield return new WaitForSeconds(activeTime);
        gameObject.SetActive(false);
    }
}
