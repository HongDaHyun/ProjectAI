using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Idle, Walk }

public abstract class PlayerAI : MonoBehaviour
{
    protected State state;
    protected Rigidbody2D rigid;
    protected GameManager gameManager;
    protected UIManager uiManager;
    protected SpriteRenderer spriteRenderer;

    public string _name;
    public int maxHP, curHP;
    public int damage;
    public bool isHit, isDead;
    public float walkSpeed, attackSpeed, bulletSpeed;

    protected float curShotTime;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
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
        Die();
        rigid.velocity = Vector3.zero;
        TestWalk();
    }

    private void FixedUpdate()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster") && !isHit)
        {
            int dmg = collision.gameObject.GetComponent<Monster>().stat.damage;
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

    protected void Die()
    {
        if (curHP <= 0)
            isDead = true;
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
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector2(inputX, inputY).normalized * Time.deltaTime * walkSpeed);
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

            Look(direction);

            curShotTime = 0;
        }
    }

    protected void Look(Vector2 direction)
    {
        if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    #endregion

    private void SetStat()
    {
        PlayerStat playerStat = gameManager.csvReader.PlayerExportData(_name);
        maxHP = playerStat.hp;
        damage = playerStat.damage;
        walkSpeed = playerStat.walkSpeed;
        attackSpeed = playerStat.attackSpeed;
        bulletSpeed = playerStat.bulletSpeed;

        uiManager = gameManager.uiManager;
        uiManager.hpBarUI.SetMaxHP();
    }
}
