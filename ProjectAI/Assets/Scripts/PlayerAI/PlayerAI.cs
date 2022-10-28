using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum State { Idle, Walk }

public abstract class PlayerAI : MonoBehaviour, IPunObservable
{
    protected State state;
    protected Rigidbody2D rigid;
    protected GameManager gameManager;
    protected UIManager uiManager;
    protected SpriteRenderer spriteRenderer;
    protected GameObject reload;
    protected PhotonView pv;
    protected Vector3 curPos;

    public PlayerStat stat;
    public string _name;
    public int curHP, longGunAmmo;
    public bool isHit, isDead, isReload;

    protected float curShotTime;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        reload = transform.GetChild(0).gameObject;
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        state = State.Idle;
        SetStat();
    }

    public virtual void Update()
    {
        gameManager.uiManager.hpBarUI.UpdateHP(curHP, stat.hp);
        curShotTime += Time.deltaTime;
        rigid.velocity = Vector3.zero;
        Die();
        TestWalk();
        Detecting(5f);
        Reload();
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
        rigid.MovePosition(rigid.position + Vector2.right * stat.walkSpeed * Time.deltaTime);
    }
    void L_Walk()
    {
        rigid.MovePosition(rigid.position + Vector2.right * stat.walkSpeed * Time.deltaTime);
    }
    void T_Walk()
    {
        rigid.MovePosition(rigid.position + Vector2.right * stat.walkSpeed * Time.deltaTime);
    }
    void D_Walk()
    {
        rigid.MovePosition(rigid.position + Vector2.right * stat.walkSpeed * Time.deltaTime);
    }

    void TestWalk()
    {
        if (gameManager.isGameOver)
            return;

        if(pv.IsMine)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");

            transform.Translate(new Vector2(inputX, inputY).normalized * Time.deltaTime * stat.walkSpeed);
        }
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
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
                LongGun(collider2D.gameObject);
            }
            else if(collider2D.gameObject.CompareTag("MonsterWeapon"))
            {

            }
        }

    }

    protected void Reload()
    {
        if(!isReload)
            StartCoroutine(DelayReload());
    }

    IEnumerator DelayReload()
    {
        if (longGunAmmo <= 0)
        {
            isReload = true;
            yield return new WaitForSeconds(stat.attackSpeed);
            reload.SetActive(true);
        }
    }

    protected void LongGun(GameObject target)
    {
        if(curShotTime >= stat.attackSpeed && !isReload && !target.GetComponent<Monster>().isDie)
        {
            Vector2 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            if(PhotonNetwork.IsConnected && !pv.IsMine)
                PhotonNetwork.Instantiate("Bullet", transform.position, rotation);
            else if(!PhotonNetwork.IsConnected)
            {
                GameObject bullet = gameManager.poolingManager.MakeObj("Bullet");
                bullet.transform.position = transform.position;
                bullet.transform.rotation = rotation;
            }
            gameManager.cameraManager.ShakeCam(2f, 0.2f);
            gameManager.soundManager.SFXPlayer(SoundManager.SFX.Fire);

            longGunAmmo -= 1;

            Look(direction);

            curShotTime = 0;
        }
    }

    protected void Look(Vector2 direction)
    {
        if (direction.x > 0)
            spriteRenderer.flipX = false;
        else if (direction.x < 0)
            spriteRenderer.flipX = true;
    }

    #endregion

    private void SetStat()
    {
        stat = gameManager.csvReader.PlayerExportData(_name);

        uiManager = gameManager.uiManager;
        uiManager.hpBarUI.SetMaxHP();
        longGunAmmo = stat.ammo.ammo;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
        }
    }
}
