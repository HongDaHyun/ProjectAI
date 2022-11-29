using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    GameManager gameManager;

    protected virtual void Awake()
    {
        gameManager = GameManager.Instance;
    }

    protected void SpawnMousePos()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
        Input.mousePosition.y, -Camera.main.transform.position.z));
    }

    public virtual IEnumerator ActiveTime(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
    }

    protected void AnimalProtector(ref GameObject[] people)
    {
        if (!gameManager.isSkill)
            return;
        people = new GameObject[Random.Range(3, 11)];
        for(int i = 0; i < people.Length; i++)
        {
            people[i] = gameManager.poolingManager.MakeObj("People");
            people[i].transform.SetParent(this.transform);
            people[i].transform.localPosition = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }
}
