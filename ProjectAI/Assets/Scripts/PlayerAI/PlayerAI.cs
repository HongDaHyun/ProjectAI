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
    protected SpriteRenderer spriteRenderer;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        state = State.Idle;
        SetStat();
    }

    public virtual void Update()
    {
        gameManager.uiManager.hpBarUI.UpdateHP(curHP, maxHP);
    }

    private void FixedUpdate()
    {
        TestWalk();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster") && !isHit)
        {
            int dmg = collision.gameObject.GetComponent<Monster>().damage;
            StartCoroutine(Damaged(dmg));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MonsterWeapon") && !isHit)
        {
            int dmg = collision.gameObject.GetComponent<Atk>().damage;
            StartCoroutine(Damaged(dmg));
        }
    }

    protected IEnumerator Damaged(int dmg)
    {
        isHit = true;
        curHP -= dmg;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        isHit = false;
        spriteRenderer.color = Color.white;
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

        transform.Translate(new Vector2(vector.x, vector.y).normalized * Time.deltaTime * walkSpeed);
    }    
    #endregion

    #region Attack
    protected void Detecting(float radius)
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach(Collider2D collider2D in colliderArray)
        {
            if (collider2D.gameObject.CompareTag("Monster"))
            {
                Attack(collider2D.gameObject);
            }
            else if(collider2D.gameObject.CompareTag("MonsterWeapon"))
            {

            }
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
