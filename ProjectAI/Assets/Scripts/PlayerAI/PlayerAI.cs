using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum State { Idle, Walk }

public abstract class PlayerAI : MonoBehaviourPunCallbacks, IPunObservable
{
    protected State state;
    protected Animator animator;
    protected Rigidbody2D rigid;
    protected GameManager gameManager;
    protected UIManager uiManager;
    [HideInInspector]public SpriteRenderer spriteRenderer;
    protected Reload[] reload;
    protected PhotonView pv;
    protected Vector3 curPos;
    protected HuntingDog[] huntingDogs;
    protected Collider2D[] colliderArray;
    public Transform nearEnemy;
    protected static float detectRadius = 8f;
    protected float inputX, inputY;
    protected GameObject shadow;

    public PlayerStat stat;

    public string _name;
    public int curHP, curExp, level;
    public bool isHit, isDead;

    [HideInInspector]public float lookCurTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        gameManager = GameManager.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
        reload = GetComponentsInChildren<Reload>(true);
        pv = GetComponent<PhotonView>();
        huntingDogs = GetComponentsInChildren<HuntingDog>(true);
        shadow = transform.GetChild(2).gameObject;
        SetStat();
    }

    private void Start()
    {
        state = State.Idle;
        gameManager.uiManager.WeaponSlotCheck();
        StartCoroutine(NearDetect(1f));
    }

    public virtual void Update()
    {
        gameManager.uiManager.hpBarUI.UpdateHP(curHP, stat.hp);
        gameManager.uiManager.expBarUI.UpdateExp(this);
        rigid.velocity = Vector3.zero;
        lookCurTime += Time.deltaTime;
        CheckAnim();
        GunLock();
        Die();
        TestWalk();
        Detecting();
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

    void CheckAnim()
    {
        if (inputX == 0 && inputY == 0)
            animator.SetInteger("State", 0);
        else
            animator.SetInteger("State", 1);
        if (isDead)
            animator.SetTrigger("IsDie");
    }

    #region Walk

    void TestWalk()
    {
        if (gameManager.isGameOver)
            return;

        if(pv.IsMine)
        {
            if(nearEnemy != null)
            {
                Vector2 direction = nearEnemy.transform.position - transform.position;
                direction.Normalize();

                transform.Translate(direction * -stat.walkSpeed * Time.deltaTime, Space.World);
                animator.SetInteger("State", 1);
            }
            else
                animator.SetInteger("State", 0);
        }
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }    
    #endregion

    #region Attack

    protected void Detecting()
    {
        colliderArray = Physics2D.OverlapCircleAll(transform.position, detectRadius);
        foreach(Collider2D collider2D in colliderArray)
        {
            if (collider2D.gameObject.CompareTag("Monster") || collider2D.gameObject.CompareTag("MonsterWeapon"))
            {
                Look(collider2D.transform.position);
                GunShot(collider2D.gameObject);
            }
        }
    }

    IEnumerator NearDetect(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            float shortDistance = Mathf.Infinity;
            foreach (Collider2D col in colliderArray)
            {
                if (col.gameObject.CompareTag("Monster") || col.gameObject.CompareTag("MonsterWeapon"))
                {
                    float distance = Vector3.SqrMagnitude(transform.position - col.transform.position);

                    if (isHit && nearEnemy != null)
                        yield return null;
                    else if (shortDistance > distance)
                    {
                        shortDistance = distance;
                        nearEnemy = col.transform;
                    }
                }
            }
            if (nearEnemy != null)
                if (Vector2.Distance(nearEnemy.position, transform.position) > detectRadius)
                    nearEnemy = null;
        }
    }

    #region Gun
    void Reload()
    {
        if (stat.ammo[0].curAmmo <= 0)
        {
            stat.ammo[0].isReload = true;
            reload[0].gameObject.SetActive(true);
        }
        if (stat.ammo[1].curAmmo <= 0)
        {
            stat.ammo[1].isReload = true;
            reload[1].gameObject.SetActive(true);
        }
        if(stat.ammo[2].curAmmo <= 0)
        {
            stat.ammo[2].isReload = true;
            reload[2].gameObject.SetActive(true);
        }
        if(stat.ammo[4].curAmmo <= 0)
        {
            stat.ammo[4].isReload = true;
            reload[3].gameObject.SetActive(true);
        }
        if(stat.ammo[5].curAmmo <= 0)
        {
            stat.ammo[5].isReload = true;
            reload[4].gameObject.SetActive(true);
        }
        if (stat.ammo[6].curAmmo <= 0)
        {
            stat.ammo[6].isReload = true;
            reload[5].gameObject.SetActive(true);
        }
    }

    protected void LongGun(GameObject target)
    {
        if (stat.ammo[0].level < 1)
            return;
        if(stat.ammo[0].curShotTime >= stat.attackSpeed + stat.ammo[0].atkSpeed && !stat.ammo[0].isReload)
        {
            Vector2 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle += Random.Range(-stat.ammo[0].spread, stat.ammo[0].spread);
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            if (PhotonNetwork.IsConnected && !pv.IsMine)
            {
                GameObject bullet = PhotonNetwork.Instantiate("Bullet", transform.position, rotation);
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.dmg = stat.ammo[0].damage + stat.damage;
                bulletScript.speed = stat.ammo[0].bulletSpeed + stat.bulletSpeed;
            }
            else if (!PhotonNetwork.IsConnected)
            {
                GameObject bullet = gameManager.poolingManager.MakeObj("Bullet");
                bullet.transform.position = transform.position;
                bullet.transform.rotation = rotation;
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                bulletScript.dmg = stat.ammo[0].damage + stat.damage;
                bulletScript.speed = stat.ammo[0].bulletSpeed + stat.bulletSpeed;
            }
            gameManager.cameraManager.ShakeCam(2f, 0.2f);
            gameManager.soundManager.SFXPlayer(SoundManager.SFX.Fire);
            animator.SetTrigger("IsAtk");
            stat.ammo[0].curAmmo -= 1;
            stat.ammo[0].curShotTime = 0;
        }
    }

    protected void ShotGun(GameObject target)
    {
        if (stat.ammo[1].level < 1)
            return;
        if (stat.ammo[1].curShotTime >= stat.attackSpeed + stat.ammo[1].atkSpeed && !stat.ammo[1].isReload)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2 direction = target.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                angle += Random.Range(-stat.ammo[1].spread, stat.ammo[1].spread);
                Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                if (PhotonNetwork.IsConnected && !pv.IsMine)
                {
                    GameObject bullet = PhotonNetwork.Instantiate("Bullet", transform.position, rotation);
                    Bullet bulletScript = bullet.GetComponent<Bullet>();
                    bulletScript.dmg = Mathf.RoundToInt((float)(stat.ammo[1].damage + stat.damage) / 5);
                    bulletScript.speed = stat.ammo[1].bulletSpeed + stat.bulletSpeed;
                }
                else if (!PhotonNetwork.IsConnected)
                {
                    GameObject bullet = gameManager.poolingManager.MakeObj("Bullet");
                    bullet.transform.position = transform.position;
                    bullet.transform.rotation = rotation;
                    Bullet bulletScript = bullet.GetComponent<Bullet>();
                    bulletScript.dmg = Mathf.RoundToInt((float)(stat.ammo[1].damage + stat.damage) / 5);
                    bulletScript.speed = stat.ammo[1].bulletSpeed + stat.bulletSpeed;
                }
            }
            gameManager.cameraManager.ShakeCam(2f, 0.2f);
            gameManager.soundManager.SFXPlayer(SoundManager.SFX.Fire);
            animator.SetTrigger("IsAtk");
            stat.ammo[1].curAmmo -= 1;
            stat.ammo[1].curShotTime = 0;
        }
    }

    protected void Tomahawk(GameObject target)
    {
        if (stat.ammo[2].level < 1)
            return;
        if (stat.ammo[2].curShotTime >= stat.attackSpeed + stat.ammo[2].atkSpeed && !stat.ammo[2].isReload)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();
            if (PhotonNetwork.IsConnected && !pv.IsMine)
            {
                GameObject tomahawk = PhotonNetwork.Instantiate("Tomahawk", transform.position, Quaternion.identity);
                Tomahawk tomahawkScript = tomahawk.GetComponent<Tomahawk>();
                if (spriteRenderer.flipX)
                    tomahawkScript.isLeft = true;
                else if (!spriteRenderer.flipX)
                    tomahawkScript.isLeft = false;
                tomahawkScript.direction = direction;
                tomahawkScript.dmg = (stat.ammo[2].damage + stat.damage);
                tomahawkScript.speed = stat.ammo[2].bulletSpeed + stat.bulletSpeed;
            }
            else if (!PhotonNetwork.IsConnected)
            {
                GameObject tomahawk = gameManager.poolingManager.MakeObj("Tomahawk");
                tomahawk.transform.position = transform.position;
                Tomahawk tomahawkScript = tomahawk.GetComponent<Tomahawk>();
                if (spriteRenderer.flipX)
                    tomahawkScript.isLeft = true;
                else if (!spriteRenderer.flipX)
                    tomahawkScript.isLeft = false;
                tomahawkScript.direction = direction;
                tomahawkScript.dmg = (stat.ammo[2].damage + stat.damage);
                tomahawkScript.speed = stat.ammo[2].bulletSpeed + stat.bulletSpeed;
            }
            //gameManager.soundManager.SFXPlayer(SoundManager.SFX.Fire);
            stat.ammo[2].curAmmo -= 1;
            stat.ammo[2].curShotTime = 0;
        }
    }

    protected void HuntingDog()
    {
        for (int i = 0; i < stat.ammo[3].level; i++)
        {
            if(!huntingDogs[i].gameObject.activeSelf)
                huntingDogs[i].gameObject.SetActive(true);
        }
        for(int i = stat.ammo[3].level; i < huntingDogs.Length; i++)
        {
            if (huntingDogs[i].gameObject.activeSelf)
                huntingDogs[i].gameObject.SetActive(false);
        }
    }

    protected void FlameThrower()
    {
        if (stat.ammo[4].level < 1)
            return;
        if (stat.ammo[4].curShotTime >= stat.attackSpeed + stat.ammo[4].atkSpeed && !stat.ammo[4].isReload)
        {
            if (PhotonNetwork.IsConnected && !pv.IsMine)
            {
                GameObject flame = PhotonNetwork.Instantiate("FlameThrower", transform.position, Quaternion.identity);
                FlameThrower flameScript = flame.GetComponent<FlameThrower>();
                flameScript.dmg = stat.ammo[4].damage + stat.damage;
            }
            else if (!PhotonNetwork.IsConnected)
            {
                GameObject flame = gameManager.poolingManager.MakeObj("FlameThrower");
                FlameThrower flameScript = flame.GetComponent<FlameThrower>();
                flameScript.dmg = stat.ammo[4].damage + stat.damage;
            }
            stat.ammo[4].curAmmo -= 1;
            stat.ammo[4].curShotTime = 0;
        }
    }

    protected void Falcon(GameObject target)
    {
        if (stat.ammo[5].level < 1)
            return;
        if (stat.ammo[5].curShotTime >= stat.attackSpeed + stat.ammo[5].atkSpeed && !stat.ammo[5].isReload)
        {
            if (PhotonNetwork.IsConnected && !pv.IsMine)
            {
                GameObject falcon = PhotonNetwork.Instantiate("Falcon", transform.position, Quaternion.identity);
                Falcon falconScript = falcon.GetComponent<Falcon>();
                falconScript.target = target;
                falconScript.dmg = (stat.ammo[5].damage + stat.damage);
                falconScript.speed = stat.ammo[5].bulletSpeed + stat.bulletSpeed;
            }
            else if (!PhotonNetwork.IsConnected)
            {
                GameObject falcon = gameManager.poolingManager.MakeObj("Falcon");
                falcon.transform.position = transform.position;
                Falcon falconScript = falcon.GetComponent<Falcon>();
                falconScript.target = target;
                falconScript.dmg = (stat.ammo[5].damage + stat.damage);
                falconScript.speed = stat.ammo[5].bulletSpeed + stat.bulletSpeed;
            }
            //gameManager.soundManager.SFXPlayer(SoundManager.SFX.Fire);
            stat.ammo[5].curAmmo -= 1;
            stat.ammo[5].curShotTime = 0;
        }
    }

    protected void Boom(GameObject target)
    {
        if (stat.ammo[6].level < 1)
            return;
        if (stat.ammo[6].curShotTime >= stat.attackSpeed + stat.ammo[6].atkSpeed && !stat.ammo[6].isReload)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();
            if (PhotonNetwork.IsConnected && !pv.IsMine)
            {
                GameObject bomb = PhotonNetwork.Instantiate("Bomb", transform.position, Quaternion.identity);
                Bomb bombScript = bomb.GetComponent<Bomb>();
                if (spriteRenderer.flipX)
                    bombScript.sprite.flipX = true;
                else if (!spriteRenderer.flipX)
                    bombScript.sprite.flipX = false;
                bombScript.direction = direction;
                bombScript.dmg = (stat.ammo[6].damage + stat.damage);
                bombScript.speed = stat.ammo[6].bulletSpeed + stat.bulletSpeed;
            }
            else if (!PhotonNetwork.IsConnected)
            {
                GameObject bomb = gameManager.poolingManager.MakeObj("Bomb");
                bomb.transform.position = transform.position;
                Bomb bombScript = bomb.GetComponent<Bomb>();
                if (spriteRenderer.flipX)
                    bombScript.sprite.flipX = true;
                else if (!spriteRenderer.flipX)
                    bombScript.sprite.flipX = false;
                bombScript.direction = direction;
                bombScript.dmg = (stat.ammo[6].damage + stat.damage);
                bombScript.speed = stat.ammo[6].bulletSpeed + stat.bulletSpeed;
            }
            //gameManager.soundManager.SFXPlayer(SoundManager.SFX.Fire);
            stat.ammo[6].curAmmo -= 1;
            stat.ammo[6].curShotTime = 0;
        }
    }

    protected void GunShot(GameObject target)
    {
        if(Vector2.Distance(target.transform.position, transform.position) <= stat.ammo[0].range)
            LongGun(target);
        if (Vector2.Distance(target.transform.position, transform.position) <= stat.ammo[1].range)
            ShotGun(target);
        if (Vector2.Distance(target.transform.position, transform.position) <= stat.ammo[2].range)
            Tomahawk(target);
        if (Vector2.Distance(target.transform.position, transform.position) <= stat.ammo[3].range)
            HuntingDog();
        if (Vector2.Distance(target.transform.position, transform.position) <= stat.ammo[4].range)
            FlameThrower();
        if (Vector2.Distance(target.transform.position, transform.position) <= stat.ammo[5].range)
            Falcon(target);
        if (Vector2.Distance(target.transform.position, transform.position) <= stat.ammo[6].range)
            Boom(target);

        Reload();
    }

    protected void GunLock()
    {
        for(int i = 0; i < stat.ammo.Length; i++)
        {
            if(stat.ammo[i].level >= 1)
            {
                stat.ammo[i].isEquip = true;
                stat.ammo[i].curShotTime += Time.deltaTime;
            }
            else if(stat.ammo[i].level == 0)
            {
                stat.ammo[i].isEquip = false;
                stat.ammo[i].curShotTime = 0;
            }
        }
    }
    #endregion

    protected void Look(Vector2 direction)
    {
        if (lookCurTime <= 2f)
            return;
        if (direction.x - transform.position.x > 0)
        {
            spriteRenderer.flipX = false;
            shadow.transform.localPosition = new Vector2(-0.05f, 0);
        }
        else if (direction.x - transform.position.x < 0)
        {
            spriteRenderer.flipX = true;
            shadow.transform.localPosition = new Vector2(0.05f, 0);
        }
        lookCurTime = 0;
    }

    #endregion

    protected virtual void SetStat()
    {
        stat = gameManager.csvReader.PlayerExportData(_name);
        level = 1;
        uiManager = gameManager.uiManager;
        uiManager.hpBarUI.SetMaxHP();
        uiManager.expBarUI.SetExp();
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
