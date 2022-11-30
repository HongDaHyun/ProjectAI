using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class King : MonoBehaviour
{
    public Vector3 target;

    GameManager gameManager;
    Animator anim;
    SpriteRenderer sprite;

    bool isTeleport;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        anim = GetComponent<Animator>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
        CheckAnim();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, gameManager.playerAI.stat.walkSpeed * Time.deltaTime);
    }

    private void CheckAnim()
    {
        if (Vector2.Distance(transform.position, gameManager.playerAI.transform.position) > 20f && !isTeleport)
        {
            isTeleport = true;
            anim.SetTrigger("IsTeleport");
        }

        if (transform.position == target)
            anim.SetInteger("State", 0);
        else
            anim.SetInteger("State", 1);

        if (transform.position.x > target.x)
            sprite.flipX = false;
        else if (transform.position.x < target.x)
            sprite.flipX = true;
    }

    public void TelePort()
    {
        transform.position = new Vector2(gameManager.playerAI.transform.position.x + Random.Range(-5f, 5f), gameManager.playerAI.transform.position.y + Random.Range(-5f, 5f));
        target = transform.position;
        isTeleport = false;
    }
}
