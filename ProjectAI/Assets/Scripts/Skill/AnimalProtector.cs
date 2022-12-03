using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalProtector : Skill
{
    GameObject[] people;

    protected override void Awake()
    {
        base.Awake();
        SetSkillStat(0);
    }

    private void OnEnable()
    {
        SpawnMousePos();
        AnimalProtector(ref people);
        StartCoroutine(ActiveTime());
    }
    public override IEnumerator ActiveTime()
    {
        gameManager.playerAI.stat.skill[0].isActive = true;
        yield return new WaitForSeconds(gameManager.playerAI.stat.skill[0].activeTime);
        for (int i = 0; i < people.Length; i++)
        {
            people[i].transform.SetParent(null);
            people[i].SetActive(false);
        }
        gameManager.playerAI.stat.skill[0].isActive = false;
        gameObject.SetActive(false);
    }
}