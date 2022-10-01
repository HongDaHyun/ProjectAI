using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    private static PoolingManager instance;

    public GameObject bulletPrefab;
    public GameObject TestPrefab;

    public GameObject[] tileMaps;

    GameObject[] targetPool;

    GameObject[] bullet;
    GameObject[] test;

    GameObject[] forestTilemap;

    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        bullet = Generate(bulletPrefab, 200);
        test = Generate(TestPrefab, 30);
        forestTilemap = Generate(tileMaps[0], 9);
    }

    GameObject[] Generate(GameObject obj, int size)
    {
        GameObject[] objects = new GameObject[size];
        for(int i = 0; i < size; i++)
        {
            objects[i] = Instantiate(obj);
            objects[i].SetActive(false);
        }
        return objects;
    }

    public GameObject MakeObj(string type)
    {
        switch(type)
        {
            case "Bullet":
                targetPool = bullet;
                break;
            case "Test":
                targetPool = test;
                break;
            case "Forest":
                targetPool = forestTilemap;
                break;
        }

        for(int i = 0; i < targetPool.Length; i++)
        {
            if(!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }
        return null;
    }
}
