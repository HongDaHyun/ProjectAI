using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class Monster : MonoBehaviourPunCallbacks
{
    protected GameManager gameManager;
    protected UIManager uiManager;
    protected CircleCollider2D col;
    protected Rigidbody2D rigid;
    protected PhotonView pv;
    protected Animator anim;
    protected SpriteRenderer sprite;

    protected Vector2 scale;
    public Atk[] weapons;

    public string _name;

    public int curHP;

    public MonsterStat stat;

    public bool isAttack, isHit, isStun, isDie;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        uiManager = gameManager.uiManager;
        col = GetComponent<CircleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        SetStat();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        sprite.color = new Color(1, 1, 1, 1);
        col.enabled = true;
        isAttack = false;
        isHit = false;
        isStun = false;
        isDie = false;
        curHP = stat.hp;
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        AttackPattern(stat.type.ToString());
        Follow();
        DieAnim();
    }

    protected virtual void FixedUpdate()
    {
        LookAt();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile") && !isDie)
        {
            StartCoroutine(Damaged(collision.GetComponent<Projectile>(), 0.2f));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Projectile2") &&!isHit && !isDie)
        {
            StartCoroutine(Damaged(collision.GetComponent<Projectile>(), 1f));
        }
    }

    protected void DieAnim()
    {
        if (curHP <= 0 && !isDie)
        {
            anim.SetTrigger("IsDie");
            col.enabled = false;
            isDie = true;
            StartCoroutine(FadeOut());
            gameManager.playerAI.curExp += stat.exp;
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = 0;
        }
    }

    protected IEnumerator Damaged(Projectile projectile, float stun)
    {
        isHit = true;
        DmgPopUp(projectile.dmg);
        curHP -= projectile.dmg;
        anim.SetTrigger("IsHit");
        if(projectile.isKnockBack)
        {
            isStun = true;
            Vector2 direction = (transform.position - gameManager.playerAI.transform.position).normalized;
            rigid.AddForce(direction * 0.0002f, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(stun);
        isHit = false;
        isStun = false;
    }

    public void DmgPopUp(int dmg)
    {
        GameObject dmgPopUp = gameManager.poolingManager.MakeObj("DmgPopUp");
        dmgPopUp.GetComponent<DmgPopUp>().text.text = dmg.ToString();
        dmgPopUp.transform.position = new Vector2(transform.position.x, transform.position.y + 0.8f);
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
        if (isDie || isStun || Vector2.Distance(transform.position, gameManager.playerAI.transform.position) < stat.range)
            return;
        transform.position = Vector2.MoveTowards(transform.position, gameManager.playerAI.transform.position, stat.moveSpeed * Time.deltaTime);
        if (!isHit)
            rigid.velocity = Vector2.zero;
    }

    public void Die()
    {
        if (PhotonNetwork.IsConnected && pv.IsMine)
            PhotonNetwork.Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }

    IEnumerator FadeOut()
    {
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            Color c = sprite.color;
            c.a = f;
            sprite.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }

    protected IEnumerator Melee()
    {
        isAttack = true;
        anim.SetTrigger("IsAtk");
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
        if (!isAttack && Vector2.Distance(transform.position, gameManager.playerAI.transform.position) <= stat.range && !isDie)
        {
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