using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public SoundManager soundManager;
    public CSVReader csvReader;
    public PoolingManager poolingManager;
    public SpawnManager spawnManager;
    public UIManager uiManager;
    public BtnManager btnManager;
    public NetworkManager networkManager;
    public PlayerAI playerAI;

    public int money, secMoneySize, secMoney;

    public bool isGameOver;

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

    private void Start()
    {
        if(!isGameOver)
        {
        }
    }

    public void FindPlayerAI()
    {
        playerAI = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>();
    }

    public void EarnMoney(int size)
    {
        money += size;
    }

    public IEnumerator SecPerMoney(int size, float sec)
    {
        EarnMoney(size);
        yield return new WaitForSeconds(sec);
        StartCoroutine(SecPerMoney(size, sec));
    }
}
