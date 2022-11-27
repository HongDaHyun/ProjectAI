using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    public Vector2 direction;
    public SpriteRenderer sprite;

    bool isBomb;
    GameObject boom;
    GameObject fire;
    CapsuleCollider2D col;

    protected override void Awake()
    {
        base.Awake();
        col = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        boom = transform.GetChild(0).gameObject;
        fire = transform.GetChild(1).gameObject;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        col.enabled = false;
        isBomb = false;
        boom.SetActive(false);
        fire.SetActive(false);
    }

    protected override void Update()
    {
        if(!isBomb)
            ShootStraightNoRotate(direction);

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }

    public void Boom()
    {
        boom.SetActive(true);
        col.enabled = true;
        gameObject.tag = "Projectile";
        isBomb = true;
        StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.2f);
        fire.SetActive(true);
        gameObject.tag = "Projectile2";
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
