using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Monster : MonoBehaviour
{
    protected GameManager gameManager;
    protected UIManager uiManager;
    protected NavMeshAgent agent;
    protected Rigidbody2D rigid;
    protected SpriteRenderer spriteRenderer;

    protected Vector2 scale;
    public Atk[] weapons;

    public string _name;

    public int curHP;

    public MonsterStat stat;

    public bool isAttack, isHit, isStun;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        uiManager = gameManager.uiManager;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetAgent();
        SetStat();
    }

    private void OnEnable()
    {
        spriteRenderer.color = Color.white;
        isStun = false;
        isAttack = false;
        isHit = false;
        curHP = stat.hp;
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        AttackPattern(stat.type.ToString());
    }

    protected virtual void FixedUpdate()
    {
        LookAt();
        Stun();
        Follow();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && !isHit)
            StartCoroutine(Damaged(gameManager.playerAI.damage));
    }

    protected IEnumerator Damaged(int dmg)
    {
        isHit = true;
        isStun = true;
        curHP -= dmg;
        DmgPopUp();
        Die();
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        isHit = false;
        isStun = false;
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

    protected void SetAgent()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    protected void Follow()
    {
        if(!isStun)
            agent.SetDestination(gameManager.playerAI.transform.position);
        rigid.velocity = Vector3.zero;
    }

    protected void Stun()
    {
        if (isStun)
            agent.enabled = false;
        else if (!isStun)
            agent.enabled = true;
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
        isStun = false;
        isAttack = false;
    }

    protected void AttackPattern(string name)
    {
        if (!isAttack && Vector2.Distance(transform.position, gameManager.playerAI.transform.position) < stat.range)
        {
            isStun = true;
            isAttack = true;
            StartCoroutine(name);
        }
    }

    void SetStat()
    {
        scale = transform.localScale;
        weapons = GetComponentsInChildren<Melee>();
        stat = gameManager.csvReader.monsterExportData(_name);
        agent.speed = stat.moveSpeed;
        agent.stoppingDistance = stat.range;
    }
}