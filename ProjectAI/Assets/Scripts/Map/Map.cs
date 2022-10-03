using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Map : MonoBehaviour
{
    static int SIZE = 9;
    static int tileDistance = 90;
    static int scrollingRange = 210;

    bool isScrolling;

    GameManager gameManager;
    NavMeshSurface2d surface2D;

    GameObject[] tilemap = new GameObject[SIZE];

    virtual protected void Awake()
    {
        gameManager = GameManager.Instance;
        surface2D = GetComponent<NavMeshSurface2d>();
    }

    private void Update()
    {
        MapScrolling();
    }

    protected void Generate(string name)
    {
        int k = 0;
        for(int i = 1; i > -2; i--)
        {
            for(int j = -1; j < 2; j++)
            {
                tilemap[k] = gameManager.poolingManager.MakeObj(name);
                tilemap[k].transform.position = new Vector2(tileDistance * j, tileDistance * i);
                k++;
            }
        }

        surface2D.BuildNavMeshAsync();
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
                isScrolling = true;
            }
            if (gameManager.playerAI.transform.position.x - tilemap[i].transform.position.x < -scrollingRange)
            {
                memory = tilemap[i].transform.position;
                tilemap[i].SetActive(false);
                tilemap[i].transform.position = new Vector2(memory.x - tileDistance * 3, memory.y);
                tilemap[i].SetActive(true);
                isScrolling = true;
            }
            if (gameManager.playerAI.transform.position.y - tilemap[i].transform.position.y > scrollingRange)
            {
                memory = tilemap[i].transform.position;
                tilemap[i].SetActive(false);
                tilemap[i].transform.position = new Vector2(memory.x, memory.y + tileDistance * 3);
                tilemap[i].SetActive(true);
                isScrolling = true;
            }
            if (gameManager.playerAI.transform.position.y - tilemap[i].transform.position.y < -scrollingRange)
            {
                memory = tilemap[i].transform.position;
                tilemap[i].SetActive(false);
                tilemap[i].transform.position = new Vector2(memory.x, memory.y - tileDistance * 3);
                tilemap[i].SetActive(true);
                isScrolling = true;
            }
        }
        if (isScrolling)
            StartCoroutine(UpdateNavMesh());
    }

    IEnumerator UpdateNavMesh()
    {
        isScrolling = false;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("a");
        surface2D.UpdateNavMesh(surface2D.navMeshData);
    }
}
