using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class Projectile : MonoBehaviourPunCallbacks, IPunObservable
{
    protected PhotonView pv;
    protected GameManager gameManager;
    protected Vector3 curPos;
    protected Quaternion curRot;
    public int dmg;
    public float speed;
    public bool isKnockBack;

    protected virtual void Awake()
    {
        gameManager = GameManager.Instance;
        pv = GetComponent<PhotonView>();
    }

    protected abstract void Update();

    protected abstract void OnTriggerEnter2D(Collider2D collision);

    protected void ShootStraight()
    {
        if(!PhotonNetwork.IsConnected)
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        else
        {
            if (pv.IsMine)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            else if ((transform.position - curPos).sqrMagnitude >= 100)
            {
                transform.position = curPos;
                transform.rotation = curRot;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
                transform.rotation = curRot;
            }
        }
    }

    protected void ShootStraightNoRotate(Vector2 target)
    {
        if(!PhotonNetwork.IsConnected)
            transform.Translate(target * speed * Time.deltaTime, Space.World);
        else
        {
            if (pv.IsMine)
            {
                transform.Translate(target * speed * Time.deltaTime, Space.World);
            }
            else if ((transform.position - curPos).sqrMagnitude >= 100)
            {
                transform.position = curPos;
                transform.rotation = curRot;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
                transform.rotation = curRot;
            }
        }
    }

    protected void ShootFlame(SpriteRenderer sprite)
    {
        if (gameManager.playerAI.spriteRenderer.flipX)
        {
            transform.position = gameManager.playerAI.transform.position + new Vector3(-0.5f, 0.2f, 0);
            sprite.flipY = true;
        }
        else if (!gameManager.playerAI.spriteRenderer.flipX)
        {
            transform.position = gameManager.playerAI.transform.position + new Vector3(0.5f, 0.2f, 0);
            sprite.flipY = false;
        }
    }

    protected void Falcon(Falcon falcon)
    {

        if (!PhotonNetwork.IsConnected)
        {
            transform.position = Vector3.MoveTowards(transform.position, falcon.target.transform.position, speed * Time.deltaTime);
            if (transform.position == falcon.target.transform.position)
                falcon.col.enabled = true;
            if (!falcon.target.activeSelf)
                gameObject.SetActive(false);
        }
        else
        {
            if (pv.IsMine)
            {
                transform.position = Vector3.MoveTowards(transform.position, falcon.target.transform.position, speed * Time.deltaTime);
                if (transform.position == falcon.target.transform.position)
                    falcon.col.enabled = true;
                if (falcon.target.GetComponent<Monster>().isDie)
                    PhotonNetwork.Destroy(gameObject);
            }
            else if ((transform.position - curPos).sqrMagnitude >= 100)
            {
                transform.position = curPos;
                transform.rotation = curRot;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
                transform.rotation = curRot;
            }
        }
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
        if (PhotonNetwork.IsConnected && pv.IsMine)
            PhotonNetwork.Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
