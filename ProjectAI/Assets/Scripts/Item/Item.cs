using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected Animator anim;
    protected GameManager gameManager;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        gameManager = GameManager.Instance;
    }
    protected virtual void OnEnable()
    {
        anim.SetTrigger("Instantiate");
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
            transform.position = gameManager.spawnManager.RanPosArea(8f);
    }

    protected IEnumerator DestroyAnim(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetTrigger("Destroy");
    }

    public void DestroyObj()
    {
        gameObject.SetActive(false);
    }
}
