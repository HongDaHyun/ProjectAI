using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Idle, Walk }

public abstract class PlayerAI : MonoBehaviour
{
    protected State state;
    protected Rigidbody2D rigid;
    protected GameManager gameManager;
    protected CSVReader csvReader;
    protected UIManager uiManager;

    public string _name;
    public int maxHP, curHP;
    public int damage;
    public bool isHit;
    public float walkSpeed, attackSpeed, bulletSpeed;

    protected float curShotTime;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
        csvReader = CSVReader.Instance;
    }

    private void Start()
    {
        state = State.Idle;
        SetStat();
    }

    public virtual void Update()
    {
        gameManager.uiManager.hpBarUI.UpdateHP(curHP, maxHP);

        TestWalk();
    }

    #region Walk
    void R_Walk()
    {
        rigid.MovePosition(rigid.position + Vector2.right * walkSpeed * Time.deltaTime);
    }
    void L_Walk()
    {
        rigid.MovePosition(rigid.position + Vector2.right * walkSpeed * Time.deltaTime);
    }
    void T_Walk()
    {
        rigid.MovePosition(rigid.position + Vector2.right * walkSpeed * Time.deltaTime);
    }
    void D_Walk()
    {
        rigid.MovePosition(rigid.position + Vector2.right * walkSpeed * Time.deltaTime);
    }

    void TestWalk()
    {
        Vector2 vector;
        vector.x = Input.GetAxisRaw("Horizontal");
        vector.y = Input.GetAxisRaw("Vertical");

        rigid.MovePosition(rigid.position + vector.normalized * walkSpeed * Time.deltaTime);
    }    
    #endregion

    #region Attack
    protected void Detecting(float radius)
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach(Collider2D collider2D in colliderArray)
        {
            if (collider2D.gameObject.CompareTag("Enemy"))
                Attack(collider2D.gameObject);
        }

    }

    protected void Attack(GameObject target)
    {
        if(curShotTime >= attackSpeed)
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
        for(int i = 0; i < csvReader.list.playerStats.Length; i ++)
        {
            if(csvReader.list.playerStats[i].name == _name)
            {
                maxHP = csvReader.list.playerStats[i].hp;
                walkSpeed = csvReader.list.playerStats[i].walkSpeed;
                attackSpeed = csvReader.list.playerStats[i].attackSpeed;
                bulletSpeed = csvReader.list.playerStats[i].bulletSpeed;
            }
        }
        uiManager = gameManager.uiManager;
        uiManager.hpBarUI.SetMaxHP();
    }
}
