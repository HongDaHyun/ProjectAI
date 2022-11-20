using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    static int SIZE = 9;
    static int tileDistance = 26;
    static int scrollingRange = 52;

    public string mapName;

    GameManager gameManager;

    GameObject[] tilemap = new GameObject[SIZE];

    virtual protected void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void Start()
    {
        Generate();
    }

    private void Update()
    {
        MapScrolling();
    }

    protected void Generate()
    {
        int k = 0;
        for(int i = 1; i > -2; i--)
        {
            for(int j = -1; j < 2; j++)
            {
                tilemap[k] = gameManager.poolingManager.MakeObj(mapName);
                tilemap[k].transform.position = new Vector2(tileDistance * j, tileDistance * i);
                k++;
            }
        }
    }

    void MapScrolling()
    {
        Vector2 memory;
        for(int i = 0; i < SIZE; i++)
        {
            if (gameManager.playerAI.transform.position.x - tilemap[i].transform.position.x > scrollingRange)
            {
                memory = tilemap[i].transform.position;
                tilemap[i].SetActive(false);
                tilemap[i].transform.position = new Vector2(memory.x + tileDistance * 3, memory.y);
                tilemap[i].SetActive(true);
            }
            if (gameManager.playerAI.transform.position.x - tilemap[i].transform.position.x < -scrollingRange)
            {
                memory = tilemap[i].transform.position;
                tilemap[i].SetActive(false);
                tilemap[i].transform.position = new Vector2(memory.x - tileDistance * 3, memory.y);
                tilemap[i].SetActive(true);
            }
            if (gameManager.playerAI.transform.position.y - tilemap[i].transform.position.y > scrollingRange)
            {
                memory = tilemap[i].transform.position;
                tilemap[i].SetActive(false);
                tilemap[i].transform.position = new Vector2(memory.x, memory.y + tileDistance * 3);
                tilemap[i].SetActive(true);
            }
            if (gameManager.playerAI.transform.position.y - tilemap[i].transform.position.y < -scrollingRange)
            {
                memory = tilemap[i].transform.position;
                tilemap[i].SetActive(false);
                tilemap[i].transform.position = new Vector2(memory.x, memory.y - tileDistance * 3);
                tilemap[i].SetActive(true);
            }
        }
    }
}
