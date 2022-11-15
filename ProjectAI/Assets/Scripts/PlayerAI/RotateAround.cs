using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RotateAround : MonoBehaviourPunCallbacks
{
    PlayerAI player;
    Transform target;
    PhotonView pv;

    Vector3 curPos;
    Quaternion curRot;

    private void Awake()
    {
        player = GetComponentInParent<PlayerAI>();
        pv = GetComponentInChildren<PhotonView>(true);
        target = transform.parent;
    }

    private void Update()
    {
        if(!PhotonNetwork.IsConnected)
        {
            transform.RotateAround(target.position, Vector3.back, player.stat.ammo[3].bulletSpeed * Time.deltaTime);
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            if (pv.IsMine)
            {
                transform.RotateAround(target.position, Vector3.back, player.stat.ammo[3].bulletSpeed * Time.deltaTime);
                transform.rotation = new Quaternion(0, 0, 0, 0);
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
