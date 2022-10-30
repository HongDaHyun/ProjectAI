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

    protected virtual void Awake()
    {
        gameManager = GameManager.Instance;
        pv = GetComponent<PhotonView>();
    }

    protected abstract void Update();

    protected abstract void OnTriggerEnter2D(Collider2D collision);

    protected void ShootStraight()
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

    protected void ShootStraightNoRotate(Vector2 target)
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

    protected void BulletCollision(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (PhotonNetwork.IsConnected && pv.IsMine)
                PhotonNetwork.Destroy(gameObject);
            else
                gameObject.SetActive(false);
            DmgPopUp(collision);
        }
    }

    protected void ProjectileBorder(Collider2D collision)
    {
        if (collision.CompareTag("ProjectileBorder"))
        {
            if (PhotonNetwork.IsConnected && pv.IsMine)
                PhotonNetwork.Destroy(gameObject);
            else
                gameObject.SetActive(false);
        }
    }

    protected void DmgPopUp(Collider2D collision)
    {
        GameObject dmgPopUp = gameManager.poolingManager.MakeObj("DmgPopUp");
        dmgPopUp.GetComponent<DmgPopUp>().text.text = dmg.ToString();
        dmgPopUp.transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y + 0.8f);
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
