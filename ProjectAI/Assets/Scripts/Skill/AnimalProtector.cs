using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalProtector : Skill
{
    GameObject[] people;

    private void OnEnable()
    {
        SpawnMousePos();
        AnimalProtector(ref people);
        StartCoroutine(ActiveTime(3f));
    }
    public override IEnumerator ActiveTime(float time)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < people.Length; i++)
        {
            people[i].transform.SetParent(null);
            people[i].SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
