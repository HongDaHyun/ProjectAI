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

    public string _name;

    public int maxHP, curHP;
    public int damage;

    public float moveSpeed;
    public float range;
    public float atkSpeed;

    public MonsterType type;

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
        curHP = maxHP;
    }

    protected virtual void Update()
    {
        rigid.velocity = Vector3.zero;
        LookAt();
        AttackPattern(type.ToString());
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
            transform.localScale = new Vector2(1, 1);
        else
            transform.localScale = new Vector2(-1, 1);
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

    protected IEnumerator Bite()
    {
        GameObject bite = transform.GetChild(0).gameObject;
        bite.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        bite.SetActive(false);
        yield return new WaitForSeconds(atkSpeed);
        isAttack = false;
    }

    protected void AttackPattern(string name)
    {
        if (!isAttack && Vector2.Distance(playerAI.transform.position, transform.position) < range)
        {
            isAttack = true;
            StartCoroutine(name);
        }
    }

    void SetStat()
    {
        for (int i = 0; i < csvReader.list.monsterStats.Length; i++)
        {
            if(csvReader.list.monsterStats[i].name == _name)
            {
                maxHP = csvReader.list.monsterStats[i].hp;
                damage = csvReader.list.monsterStats[i].damage;
                moveSpeed = csvReader.list.monsterStats[i].moveSpeed;
                agent.speed = moveSpeed;
                range = csvReader.list.monsterStats[i].range;
                agent.stoppingDistance = range;
                atkSpeed = csvReader.list.monsterStats[i].atkSpeed;
                type = csvReader.list.monsterStats[i].type;
            }
        }
    }
}