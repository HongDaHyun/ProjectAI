using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPunCallbacks, IPunObservable
{
    PhotonView pv;
    GameManager gameManager;
    public int dmg;

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
            DmgPopUp(collision);
        }
    }

    void DmgPopUp(Collider2D collision)
    {
        GameObject dmgDmgPopUp = gameManager.poolingManager.MakeObj("DmgPopUp");
        dmgDmgPopUp.GetComponent<DmgPopUp>().text.text = dmg.ToString();
        dmgDmgPopUp.transform.position = new Vector2(collision.transform.position.x, collision.transform.position.y + 0.8f);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
