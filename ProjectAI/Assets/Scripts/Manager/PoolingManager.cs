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

    public GameObject bananaPrefab;
    public GameObject bitePrefab;

    GameObject[] targetPool;

    GameObject[] bullet;
    GameObject[] tomahawk;
    GameObject[] flameThrower;
    GameObject[] falcon;

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
    GameObject[] desertTilemap;

    GameObject[] dmgPopUp;
    GameObject[] uiPopUp;
    GameObject[] monsterUI;

    GameObject[] banana;
    GameObject[] bite;

    private void Awake()
    {
        bullet = Generate(projectilePrefab[0], 100);
        tomahawk = Generate(projectilePrefab[1], 100);
        flameThrower = Generate(projectilePrefab[2], 10);
        falcon = Generate(projectilePrefab[3], 30);
        forestTilemap = Generate(tileMaps[0], 9);
        desertTilemap = Generate(tileMaps[1], 9);
        dmgPopUp = Generate(dmgPopUpPrefab, 50);
        uiPopUp = Generate(uiPopUpPrefab, 50);
        monsterUI = Generate(monsterUIPrefab, 30);
        rabbit = Generate(enemyPrefab[0], 200);
        chicken = Generate(enemyPrefab[1], 200);
        mouse = Generate(enemyPrefab[2], 200);
        pig = Generate(enemyPrefab[3], 200);
        deer = Generate(enemyPrefab[4], 200);
        fox = Generate(enemyPrefab[5], 200);
        monkey = Generate(enemyPrefab[6], 200);
        bear = Generate(enemyPrefab[7], 200);
        lion = Generate(enemyPrefab[8], 200);
        elephant = Generate(enemyPrefab[9], 200);
        banana = Generate(bananaPrefab, 30);
        bite = Generate(bitePrefab, 200);
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
            case "FlameThrower":
                targetPool = flameThrower;
                break;
            case "Falcon":
                targetPool = falcon;
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
            case "Desert":
                targetPool = desertTilemap;
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
            case "Banana":
                targetPool = banana;
                break;
            case "Bite":
                targetPool = bite;
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
