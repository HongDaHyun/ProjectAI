using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    private void Start()
    {
        StartCoroutine(SpawnItem(1.7f, 2));
    }

    public void Spawn(EquipSlot equip)
    {
        MonsterStat mon = gameManager.csvReader.monsterExportData(equip._name);
        if (equip.isDropped && equip.isSpawn)
        {
            equip.isSpawn = false;
            equip.img.fillAmount = 1;
            for (int i = 0; i < mon.count; i++)
            {
                GameObject obj = gameManager.poolingManager.MakeObj(mon.name);
                obj.transform.position = RanPosCircle(12f);
            }
        }
        if (equip.isDropped && !equip.isSpawn)
        {
            equip.img.fillAmount -= 1 / mon.coolTime * Time.deltaTime;

            if (equip.img.fillAmount <= 0)
            {
                equip.img.fillAmount = 0;
                equip.isSpawn = true;
            }
        }
    }

    IEnumerator SpawnItem(float delay, int amount)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            if(!gameManager.isGameOver)
            {
                for (int i = 0; i < amount; i++)
                {
                    int ran = Random.Range(0, gameManager.poolingManager.itemPrefab.Length);
                    switch (ran)
                    {
                        case 0:
                            GameObject item = gameManager.poolingManager.MakeObj("Apple");
                            item.transform.position = RanPosArea(8f);
                            break;
                    }
                }
            }
        }
    }

    Vector2 RanPosCircle(float radius)
    {
        Vector2 playerPos = gameManager.playerAI.transform.position;
        float a = playerPos.x;
        float b = playerPos.y;

        float x = Random.Range(-radius + a, radius + a);
        float y_b = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - a, 2));
        y_b *= Random.Range(0, 2) == 0 ? -1 : 1;
        float y = y_b + b;

        Vector2 randomPosition = new Vector2(x, y);

        return randomPosition;
    }

    public Vector2 RanPosArea(float side)
    {
        Vector2 playerPos = gameManager.playerAI.transform.position;
        float a = playerPos.x; float b = playerPos.y;

        float x = Random.Range(-side + a, side + a);
        float y = Random.Range(-side + b, side + b);

        Vector2 ranPos = new Vector2(x, y);
        return ranPos;
    }
}
