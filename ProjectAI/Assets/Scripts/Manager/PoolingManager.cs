using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    private static PoolingManager instance;

    public GameObject bulletPrefab;
    public GameObject[] enemyPrefab;

    public GameObject[] tileMaps;

    public GameObject dmgPopUpPrefab;

    GameObject[] targetPool;

    GameObject[] bullet;

    GameObject[] test;
    GameObject[] rabbit;

    GameObject[] forestTilemap;

    GameObject[] dmgPopUp;

    private void Awake()
    {
        bullet = Generate(bulletPrefab, 200);
        test = Generate(enemyPrefab[0], 30);
        rabbit = Generate(enemyPrefab[1], 30);
        forestTilemap = Generate(tileMaps[0], 9);
        dmgPopUp = Generate(dmgPopUpPrefab, 200);
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
            case "Rabbit":
                targetPool = rabbit;
                break;
            case "Forest":
                targetPool = forestTilemap;
                break;
            case "DmgPopUp":
                targetPool = dmgPopUp;
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
