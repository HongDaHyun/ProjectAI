using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public CSVReader csvReader;
    public PoolingManager poolingManager;
    public UIManager uiManager;
    public BtnManager btnManager;
    public PlayerAI playerAI;

    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        FindPlayerAI();
    }

    public void FindPlayerAI()
    {
        playerAI = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>();
    }
}
