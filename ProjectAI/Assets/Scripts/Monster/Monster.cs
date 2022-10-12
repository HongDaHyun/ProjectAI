using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Monster : MonoBehaviour
{
    protected PlayerAI playerAI;
    protected GameManager gameManager;
    protected UIManager uiManager;
    protected CSVReader csvReader;
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
        csvReader = CSVReader.Instance;
        uiManager = gameManager.uiManager;
        playerAI = gameManager.playerAI;
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
        rigid.velocity = Vector3.zero;
        LookAt();
        AttackPattern(stat.type.ToString());
    }

    protected virtual void FixedUpdate()
    {
        Stun();
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && !isHit)
            StartCoroutine(Damaged(playerAI.damage));
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
        GameObject dmgPopUp = gameManager.poolingManager.MakeObj("DmgPopUp");
        dmgPopUp.transform.position = new Vector2(transform.position.x, transform.position.y + 0.8f);
    }

    protected void LookAt()
    {
        if (transform.position.x > playerAI.transform.position.x)
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
            agent.SetDestination(playerAI.transform.position);
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
        if (Vector2.Distance(new Vector2(0, playerAI.transform.position.y), new Vector2(0, transform.position.y)) < 0.3f)
            weapons[0].gameObject.SetActive(true);
        else if(transform.position.y < playerAI.transform.position.y)
            weapons[1].gameObject.SetActive(true);
        else if(transform.position.y > playerAI.transform.position.y)
            weapons[2].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].gameObject.SetActive(false);
        yield return new WaitForSeconds(stat.atkDelay);
        isAttack = false;
    }

    protected void AttackPattern(string name)
    {
        if (!isAttack && Vector2.Distance(playerAI.transform.position, transform.position) < stat.range)
        {
            isAttack = true;
            StartCoroutine(name);
        }
    }

    void SetStat()
    {
        scale = transform.localScale;
        weapons = GetComponentsInChildren<Melee>();
        for (int i = 0; i < csvReader.list.monsterStats.Length; i++)
        {
            if(csvReader.list.monsterStats[i].name == _name)
            {
                stat.name = csvReader.list.monsterStats[i].name;
                stat.hp = csvReader.list.monsterStats[i].hp;
                stat.damage = csvReader.list.monsterStats[i].damage;
                stat.moveSpeed = csvReader.list.monsterStats[i].moveSpeed;
                agent.speed = stat.moveSpeed;
                stat.range = csvReader.list.monsterStats[i].range;
                agent.stoppingDistance = stat.range;
                stat.atkDelay = csvReader.list.monsterStats[i].atkDelay;
                stat.type = csvReader.list.monsterStats[i].type;
            }
        }
    }
}