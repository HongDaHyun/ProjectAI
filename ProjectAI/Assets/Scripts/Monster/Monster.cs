using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    protected PlayerAI playerAI;
    protected GameManager gameManager;
    protected UIManager uiManager;

    public int damage;
    public float moveSpeed;

    public void Start()
    {
        gameManager = GameManager.Instance;
        uiManager = gameManager.uiManager;
        playerAI = gameManager.playerAI;
        SetStat();
    }

    protected IEnumerator TouchAttack()
    {
        playerAI.isHit = true;
        playerAI.curHP -= damage;
        uiManager.hpBarUI.SetHealth(playerAI.curHP);
        yield return new WaitForSeconds(0.5f);
        playerAI.isHit = false;
    }

    protected void Follow(GameObject target)
    {
        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();

        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    private void SetStat()
    {
        damage = gameManager.stat.monsterStat.damage;
        moveSpeed = gameManager.stat.monsterStat.moveSpeed;
    }
}
