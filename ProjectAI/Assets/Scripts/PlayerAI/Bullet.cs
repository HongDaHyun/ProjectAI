using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector2.up * GameManager.Instance.stat.playerStat.bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            gameObject.SetActive(false);
    }
}
