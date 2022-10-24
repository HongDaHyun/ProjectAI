using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPunCallbacks, IPunObservable
{
    PhotonView pv;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        pv = GetComponent<PhotonView>();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * GameManager.Instance.playerAI.stat.bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (PhotonNetwork.IsConnected && pv.IsMine)
                PhotonNetwork.Destroy(gameObject);
            else
                gameObject.SetActive(false);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
