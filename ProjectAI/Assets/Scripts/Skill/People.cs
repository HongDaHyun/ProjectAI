using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : Skill
{
    Animator anim;
    SpriteRenderer sprite;
    public Sprite[] peopleSprite;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        sprite.sprite = peopleSprite[Random.Range(0, peopleSprite.Length)];
        int ran = Random.Range(0, 2);
        if (ran == 0)
            sprite.flipX = true;
        else if (ran == 1)
            sprite.flipX = false;
        anim.SetInteger("Stat", Random.Range(0, 4));
    }

    private void OnDisable()
    {
        anim.SetInteger("Stat", 0);
    }
}
