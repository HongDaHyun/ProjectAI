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

    public string _name;

    public int damage;

    public float moveSpeed;
    public float range;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        csvReader = CSVReader.Instance;
        uiManager = gameManager.uiManager;
        playerAI = gameManager.playerAI;
        SetAgent();
        SetStat();
    }

    protected IEnumerator Attack()
    {
        playerAI.isHit = true;
        playerAI.curHP -= damage;
        yield return new WaitForSeconds(0.5f);
        playerAI.isHit = false;
    }

    protected void LookAt()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (transform.position.x > playerAI.transform.position.x)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }

    protected void SetAgent()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    protected void Follow(float distance)
    {
        agent.SetDestination(playerAI.transform.position);
    }

    void SetStat()
    {
        for (int i = 0; i < csvReader.list.monsterStats.Length; i++)
        {
            if(csvReader.list.monsterStats[i].name == _name)
            {
                damage = csvReader.list.monsterStats[i].damage;
                moveSpeed = csvReader.list.monsterStats[i].moveSpeed;
                agent.speed = moveSpeed;
                range = csvReader.list.monsterStats[i].range;
                agent.stoppingDistance = range;
            }
        }
    }
}
