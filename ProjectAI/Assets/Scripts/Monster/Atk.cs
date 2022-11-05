using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class Atk : MonoBehaviourPunCallbacks, IPunObservable
{
    public int damage;

    protected PhotonView pv;
    protected GameManager gameManager;
    protected Vector3 curPos;
    protected Quaternion curRot;

    public virtual void Awake()
    {
        gameManager = GameManager.Instance;
        pv = GetComponent<PhotonView>();
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

    protected void ShootStraightNoRotate(Vector2 direction)
    {
        if (!PhotonNetwork.IsConnected)
            transform.Translate(direction * 2f * Time.deltaTime, Space.World);
        else
        {
            if (pv.IsMine)
            {
                transform.Translate(direction * 2f * Time.deltaTime, Space.World);
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
}
