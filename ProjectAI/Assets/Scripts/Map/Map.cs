using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Map : MonoBehaviour
{
    static int SIZE = 9;
    public GameObject[] tilemap = new GameObject[SIZE];

    private void Awake()
    {
        for(int i = 0; i < SIZE; i++)
        {
            tilemap[i] = transform.GetChild(i).gameObject;
        }
    }
}
