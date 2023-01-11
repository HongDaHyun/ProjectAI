using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Atk : MonoBehaviour
{
    public int damage;

    protected GameManager gameManager;
    protected Vector3 curPos;
    protected Quaternion curRot;

    public virtual void Awake()
    {
        gameManager = GameManager.Instance;
    }

    protected void ShootStraightNoRotate(Vector2 direction)
    {
        transform.Translate(direction * 2f * Time.deltaTime, Space.World);
    }

    public void ActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
