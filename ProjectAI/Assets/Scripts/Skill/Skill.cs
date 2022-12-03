using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected GameManager gameManager;
    public SkillStat skillStat;

    protected virtual void Awake()
    {
        gameManager = GameManager.Instance;
    }

    protected void SetSkillStat(int i)
    {
        skillStat = gameManager.playerAI.stat.skill[i];
    }

    protected void SpawnMousePos()
    {
        transform.position = gameManager.uiManager.mousePos;
    }

    public abstract IEnumerator ActiveTime();

    protected void AnimalProtector(ref GameObject[] people)
    {
        if (!gameManager.isSkill)
            return;
        people = new GameObject[Random.Range(3, 11)];
        for(int i = 0; i < people.Length; i++)
        {
            people[i] = gameManager.poolingManager.MakeObj("People");
            people[i].transform.SetParent(transform);
            people[i].transform.localPosition = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }
}
