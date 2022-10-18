using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEntity : MonoBehaviour
{
    static Vector2 bottomLeft = new Vector2(-45, -45);
    static Vector2 topRight = new Vector2(45, 45);

    public GameObject[] objects;

    public int count, errorRange;

    List<Vector2> savePos = new List<Vector2>();
    GameObject[] obj;

    private void Awake()
    {
        GenerateObj();
    }

    private void OnEnable()
    {
        ReTransFormObj();
    }

    private void OnDisable()
    {
        ClearAll();
    }

    void GenerateObj()
    {
        count += Random.Range(-errorRange, errorRange);
        obj = new GameObject[count];

        for(int i = 0; i < count; i++)
        {
            int ranObj = Random.Range(0, objects.Length);
            Vector2 pos = new Vector2(x: Random.Range((int)bottomLeft.x, (int)topRight.x), y: Random.Range((int)bottomLeft.y, (int)topRight.y));
            if (savePos.Contains(pos))
            {
                i--;
                continue;
            }

            savePos.Add(pos);
            obj[i] = Instantiate(objects[ranObj]);
            obj[i].transform.SetParent(transform);
            obj[i].transform.localPosition = pos;
        }
    }

    void ReTransFormObj()
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 pos = new Vector2(x: Random.Range((int)bottomLeft.x, (int)topRight.x), y: Random.Range((int)bottomLeft.y, (int)topRight.y));

            if (savePos.Contains(pos))
                if (savePos.Contains(pos))
                {
                    i--;
                    continue;
                }

            savePos.Add(pos);
            obj[i].transform.localPosition = pos;
            obj[i].SetActive(true);
        }
    }

    void ClearAll()
    {
        savePos.Clear();
        for (int i = 0; i < obj.Length; i++)
            obj[i].SetActive(false);
    }
}
