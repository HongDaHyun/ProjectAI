using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Idle, Walk, Run }

public class PlayerAI : MonoBehaviour
{
    State state;
    Rigidbody2D rigid;
    GameManager gameManager;
    UIManager uiManager;

    public int maxHP, curHP;
    public bool isHit;

    float curShotTime;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
    }

    private void Start()
    {
        state = State.Idle;
        SetStat();
        curHP = maxHP;
    }

    private void Update()
    {
        Detecting(5f);
        curShotTime += Time.deltaTime;
    }

    #region Walk
    void R_Walk()
    {
        rigid.velocity = new Vector2(GameManager.Instance.stat.playerStat.walkSpeed, 0);
    }
    void L_Walk()
    {
        rigid.velocity = new Vector2(-GameManager.Instance.stat.playerStat.walkSpeed, 0);
    }
    void T_Walk()
    {
        rigid.velocity = new Vector2(0, GameManager.Instance.stat.playerStat.walkSpeed);
    }
    void D_Walk()
    {
        rigid.velocity = new Vector2(0, -GameManager.Instance.stat.playerStat.walkSpeed);
    }
    #endregion

    #region Attack
    void Detecting(float radius)
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach(Collider2D collider2D in colliderArray)
        {
            if (collider2D.gameObject.CompareTag("Enemy"))
                Attack(collider2D.gameObject);
        }

    }

    void Attack(GameObject target)
    {
        if(curShotTime >= GameManager.Instance.stat.playerStat.attackSpeed)
        {
            GameObject bullet = GameManager.Instance.poolingManager.MakeObj("Bullet");
            bullet.transform.position = transform.position;

            Vector2 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            bullet.transform.rotation = rotation;

            curShotTime = 0;
        }
    }

    #endregion

    private void SetStat()
    {
        maxHP = gameManager.stat.playerStat.hp;
        uiManager = gameManager.uiManager;
        uiManager.hpBarUI.SetMaxHealth(maxHP);
    }
}
