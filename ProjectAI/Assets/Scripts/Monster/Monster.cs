using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    protected GameManager gameManager;
    protected UIManager uiManager;
    protected Rigidbody2D rigid;
    protected SpriteRenderer spriteRenderer;

    protected Vector2 scale;
    public Atk[] weapons;

    public string _name;

    public int curHP;

    public MonsterStat stat;

    public bool isAttack, isHit;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        uiManager = gameManager.uiManager;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetStat();
    }

    private void OnEnable()
    {
        spriteRenderer.color = Color.white;
        isAttack = false;
        isHit = false;
        curHP = stat.hp;
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        AttackPattern(stat.type.ToString());
        Follow();
    }

    protected virtual void FixedUpdate()
    {
        LookAt();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && !isHit)
            StartCoroutine(Damaged(gameManager.playerAI.stat.damage, collision));
    }

    protected IEnumerator Damaged(int dmg, Collider2D collision)
    {
        isHit = true;
        curHP -= dmg;
        Die();
        Vector2 direction = (transform.position - collision.transform.position).normalized;
        rigid.AddForce(direction * 0.0001f, ForceMode2D.Impulse);
        DmgPopUp();
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        isHit = false;
        spriteRenderer.color = Color.white;
    }

    void DmgPopUp()
    {
        GameObject dmgDmgPopUp = gameManager.poolingManager.MakeObj("DmgPopUp");
        dmgDmgPopUp.transform.position = new Vector2(transform.position.x, transform.position.y + 0.8f);
    }

    protected void LookAt()
    {
        if (transform.position.x > gameManager.playerAI.transform.position.x)
            transform.localScale = new Vector2(scale.x, scale.y);
        else
            transform.localScale = new Vector2(-scale.x, scale.y);
    }

    protected void Follow()
    {
        if (isHit || Vector2.Distance(transform.position, gameManager.playerAI.transform.position) < stat.range)
            return;
        transform.position = Vector2.MoveTowards(transform.position, gameManager.playerAI.transform.position, stat.moveSpeed * Time.deltaTime);
        rigid.velocity = Vector2.zero;
    }

    protected void Die()
    {
        if (curHP <= 0)
            gameObject.SetActive(false);
        return;
    }

    protected IEnumerator Melee()
    {
        if (Vector2.Distance(new Vector2(0, gameManager.playerAI.transform.position.y), new Vector2(0, transform.position.y)) < 0.3f)
            weapons[0].gameObject.SetActive(true);
        else if(transform.position.y < gameManager.playerAI.transform.position.y)
            weapons[1].gameObject.SetActive(true);
        else if(transform.position.y > gameManager.playerAI.transform.position.y)
            weapons[2].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].gameObject.SetActive(false);
        yield return new WaitForSeconds(stat.atkDelay);
        isAttack = false;
    }

    protected void AttackPattern(string name)
    {
        if (!isAttack && Vector2.Distance(transform.position, gameManager.playerAI.transform.position) <= stat.range)
        {
            isAttack = true;
            StartCoroutine(name);
        }
    }

    void SetStat()
    {
        scale = transform.localScale;
        weapons = GetComponentsInChildren<Melee>();
        stat = gameManager.csvReader.monsterExportData(_name);
    }
}