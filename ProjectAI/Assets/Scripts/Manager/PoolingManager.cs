using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PoolingManager : MonoBehaviour
{
    private static PoolingManager instance;

    public GameObject[] projectilePrefab;

    public GameObject[] enemyPrefab;

    public GameObject[] effectPrefab;

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
    GameObject[] ghostRabbit;
    GameObject[] ghostDeer;
    GameObject[] ghostPig;
    GameObject[] ghostLion;
    GameObject[] ghostElephant;

    GameObject[] hit;
    GameObject[] blood;

    GameObject[] forestTilemap;
    GameObject[] desertTilemap;
    GameObject[] cityTilemap;

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
        cityTilemap = Generate(tileMaps[2], 9);
        dmgPopUp = Generate(dmgPopUpPrefab, 500);
        uiPopUp = Generate(uiPopUpPrefab, 50);
        monsterUI = Generate(monsterUIPrefab, 30);
        rabbit = Generate(enemyPrefab[0], 1000);
        chicken = Generate(enemyPrefab[1], 1000);
        mouse = Generate(enemyPrefab[2], 1000);
        pig = Generate(enemyPrefab[3], 1000);
        deer = Generate(enemyPrefab[4], 1000);
        fox = Generate(enemyPrefab[5], 1000);
        monkey = Generate(enemyPrefab[6], 1000);
        bear = Generate(enemyPrefab[7], 1000);
        lion = Generate(enemyPrefab[8], 1000);
        elephant = Generate(enemyPrefab[9], 1000);
        ghostRabbit = Generate(enemyPrefab[10], 1000);
        ghostDeer = Generate(enemyPrefab[11], 1000);
        ghostPig = Generate(enemyPrefab[12], 1000);
        ghostLion = Generate(enemyPrefab[13], 1000);
        ghostElephant = Generate(enemyPrefab[14], 1000);
        hit = Generate(effectPrefab[0], 500);
        blood = Generate(effectPrefab[1], 500);
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
            case "GhostRabbit":
                targetPool = ghostRabbit;
                break;
            case "GhostDeer":
                targetPool = ghostDeer;
                break;
            case "GhostPig":
                targetPool = ghostPig;
                break;
            case "GhostLion":
                targetPool = ghostLion;
                break;
            case "GhostElephant":
                targetPool = ghostElephant;
                break;
            case "Hit":
                targetPool = hit;
                break;
            case "Blood":
                targetPool = blood;
                break;
            case "Forest":
                targetPool = forestTilemap;
                break;
            case "Desert":
                targetPool = desertTilemap;
                break;
            case "City":
                targetPool = cityTilemap;
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
