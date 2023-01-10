using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopUp : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed;
    public float activeTime;

    GameManager gameManager;

    public virtual void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public virtual void Update()
    {
        transform.Translate(new Vector2(0, moveSpeed * Time.deltaTime));
    }
}
