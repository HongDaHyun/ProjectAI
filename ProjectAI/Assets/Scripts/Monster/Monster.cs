using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    protected PlayerAI playerAI;
    protected GameManager gameManager;
    protected UIManager uiManager;
    protected CSVReader csvReader;

    public string _name;

    public int damage;
    public float moveSpeed;

    private void OnEnable()
    {
        gameManager = GameManager.Instance;
        csvReader = CSVReader.Instance;
        uiManager = gameManager.uiManager;
        playerAI = gameManager.playerAI;
        SetStat();
    }

    protected IEnumerator Attack()
    {
        playerAI.isHit = true;
        playerAI.curHP -= damage;
        yield return new WaitForSeconds(0.5f);
        playerAI.isHit = false;
    }

    protected void Follow(GameObject target)
    {
        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();

        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    void SetStat()
    {
        for (int i = 0; i < csvReader.list.monsterStats.Length; i++)
        {
            if(csvReader.list.monsterStats[i].name == _name)
            {
                damage = csvReader.list.monsterStats[i].damage;
                moveSpeed = csvReader.list.monsterStats[i].moveSpeed;
            }
        }
    }
}
