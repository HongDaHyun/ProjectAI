using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected GameManager gameManager;
    protected Vector3 curPos;
    protected Quaternion curRot;
    public int dmg;
    public float speed;
    public bool isKnockBack;

    protected virtual void Awake()
    {
        gameManager = GameManager.Instance;
    }

    protected abstract void Update();

    protected abstract void OnTriggerEnter2D(Collider2D collision);

    protected void ShootStraight()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    protected void ShootStraightNoRotate(Vector2 target)
    {
        transform.Translate(target * speed * Time.deltaTime, Space.World);
    }

    protected void ShootFlame()
    {
        if (gameManager.playerAI.spriteRenderer.flipX)
        {
            transform.position = gameManager.playerAI.transform.position + new Vector3(-0.5f, 0.2f, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (!gameManager.playerAI.spriteRenderer.flipX)
        {
            transform.position = gameManager.playerAI.transform.position + new Vector3(0.5f, 0.2f, 0);
            transform.localScale = new Vector3(1, -1, 1);
        }
    }

    protected void Falcon(Falcon falcon)
    {
        transform.position = Vector3.MoveTowards(transform.position, falcon.target.transform.position + new Vector3(0, 0.5f), speed * Time.deltaTime);
        if (transform.position == falcon.target.transform.position + new Vector3(0, 0.5f))
            falcon.col.enabled = true;
        if (!falcon.target.activeSelf)
            gameObject.SetActive(false);
    }

    protected void BulletCollision(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            DestroyObj();
        }
    }

    protected void ProjectileBorder(Collider2D collision)
    {
        if (collision.CompareTag("ProjectileBorder"))
        {
            DestroyObj();
        }
    }

    public void DestroyObj()
    {
        gameObject.SetActive(false);
    }
}
