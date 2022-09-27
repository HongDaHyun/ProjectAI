using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    public PlayerAI playerAI;

    public int damage;

    public void Start()
    {
        playerAI = GameManager.Instance.playerAI;
        SetStat();
    }

    protected abstract IEnumerator TouchAttack();

    private void SetStat()
    {
        damage = GameManager.Instance.stat.monsterStat.damage;
    }
}
