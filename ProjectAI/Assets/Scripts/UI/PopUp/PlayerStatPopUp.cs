using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatPopUp : PopUp
{
    [HideInInspector] public TextMeshPro text;
    Color alpha;

    public override void Awake()
    {
        base.Awake();
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
    }

    private void OnEnable()
    {
        StartCoroutine(Pop());
    }

    public override void Update()
    {
        base.Update();
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    public IEnumerator Pop()
    {
        alpha.a = 1;
        yield return new WaitForSeconds(activeTime);
        gameObject.SetActive(false);
    }
}
