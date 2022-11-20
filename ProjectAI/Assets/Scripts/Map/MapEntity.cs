using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEntity : MonoBehaviour
{
    static Vector2 bottomLeft = new Vector2(-12, -12);
    static Vector2 topRight = new Vector2(12, 12);

    [Header("Obj")]
    public GameObject[] objects;
    public int objCount, objErrorRange;

    [Header("Gro")]
    public GameObject[] grounds;
    public int groCount, groErrorRange;

    List<Vector2> savePos = new List<Vector2>();
    List<Vector2> savePos2 = new List<Vector2>();
    GameObject[] obj, gro;

    private void Awake()
    {
        GenerateObj();
        GenerateGround();
    }

    private void OnEnable()
    {
        ReTransFormObj();
        ReTransFormGro();
    }

    private void OnDisable()
    {
        ClearAll();
    }

    void GenerateObj()
    {
        objCount += Random.Range(-objErrorRange, objErrorRange);
        obj = new GameObject[objCount];

        for(int i = 0; i < objCount; i++)
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

    void GenerateGround()
    {
        groCount += Random.Range(-groErrorRange, groErrorRange);
        gro = new GameObject[groCount];

        for (int i = 0; i < groCount; i++)
        {
            int ranObj = Random.Range(0, grounds.Length);
            Vector2 pos = new Vector2(x: Random.Range((int)bottomLeft.x, (int)topRight.x), y: Random.Range((int)bottomLeft.y, (int)topRight.y));
            if (savePos2.Contains(pos))
            {
                i--;
                continue;
            }

            savePos2.Add(pos);
            gro[i] = Instantiate(grounds[ranObj]);
            gro[i].transform.SetParent(transform);
            gro[i].transform.localPosition = pos;
        }
    }

    void ReTransFormObj()
    {
        for (int i = 0; i < objCount; i++)
        {
            Vector2 pos = new Vector2(x: Random.Range((int)bottomLeft.x, (int)topRight.x), y: Random.Range((int)bottomLeft.y, (int)topRight.y));

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

    void ReTransFormGro()
    {
        for (int i = 0; i < groCount; i++)
        {
            Vector2 pos = new Vector2(x: Random.Range((int)bottomLeft.x, (int)topRight.x), y: Random.Range((int)bottomLeft.y, (int)topRight.y));

            if (savePos2.Contains(pos))
            {
                i--;
                continue;
            }

            savePos2.Add(pos);
            gro[i].transform.localPosition = pos;
            gro[i].SetActive(true);
        }
    }

    void ClearAll()
    {
        savePos.Clear();
        for (int i = 0; i < obj.Length; i++)
            obj[i].SetActive(false);
        savePos2.Clear();
        for (int i = 0; i < gro.Length; i++)
            gro[i].SetActive(false);
    }
}
