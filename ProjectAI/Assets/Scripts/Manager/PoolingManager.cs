using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PoolingManager : MonoBehaviour
{
    private static PoolingManager instance;

    public GameObject[] projectilePrefab;

    public GameObject[] enemyPrefab;

    public GameObject[] tileMaps;

    public GameObject dmgPopUpPrefab;
    public GameObject uiPopUpPrefab;
    public GameObject monsterUIPrefab;

    GameObject[] targetPool;

    GameObject[] bullet;
    GameObject[] tomahawk;

    GameObject[] test;
    GameObject[] rabbit;
    GameObject[] chicken;
    GameObject[] mouse;
    GameObject[] pig;
    GameObject[] deer;
    GameObject[] fox;
    GameObject[] monkey;
    GameObject[] bear;
    GameObject[] lion;
    GameObject[] elephant;

    GameObject[] forestTilemap;

    GameObject[] dmgPopUp;
    GameObject[] uiPopUp;
    GameObject[] monsterUI;

    private void Awake()
    {
        bullet = Generate(projectilePrefab[0], 100);
        tomahawk = Generate(projectilePrefab[1], 100);
        forestTilemap = Generate(tileMaps[0], 9);
        dmgPopUp = Generate(dmgPopUpPrefab, 50);
        uiPopUp = Generate(uiPopUpPrefab, 50);
        monsterUI = Generate(monsterUIPrefab, 30);
        test = Generate(enemyPrefab[0], 30);
        rabbit = Generate(enemyPrefab[1], 30);
        chicken = Generate(enemyPrefab[2], 30);
        mouse = Generate(enemyPrefab[3], 30);
        pig = Generate(enemyPrefab[4], 30);
        deer = Generate(enemyPrefab[5], 30);
        fox = Generate(enemyPrefab[6], 30);
        monkey = Generate(enemyPrefab[7], 30);
        bear = Generate(enemyPrefab[8], 30);
        lion = Generate(enemyPrefab[9], 30);
        elephant = Generate(enemyPrefab[10], 30);
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
            case "Tomahawk":
                targetPool = tomahawk;
                break;
            case "Test":
                targetPool = test;
                break;
            case "Rabbit":
                targetPool = rabbit;
                break;
            case "Chicken":
                targetPool = chicken;
                break;
            case "Mouse":
                targetPool = mouse;
                break;
            case "Pig":
                targetPool = pig;
                break;
            case "Deer":
                targetPool = deer;
                break;
            case "Fox":
                targetPool = fox;
                break;
            case "Monkey":
                targetPool = monkey;
                break;
            case "Bear":
                targetPool = bear;
                break;
            case "Lion":
                targetPool = lion;
                break;
            case "Elephant":
                targetPool = elephant;
                break;
            case "Forest":
                targetPool = forestTilemap;
                break;
            case "DmgPopUp":
                targetPool = dmgPopUp;
                break;
            case "UIPopUp":
                targetPool = uiPopUp;
                break;
            case "MonsterUI":
                targetPool = monsterUI;
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
