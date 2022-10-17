using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float monsterRadius;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void Spawn(EquipSlot equip)
    {
        if (equip.isDropped && !equip.isSpawn)
        {
            equip.isSpawn = true;
            equip.img.fillAmount = 1;
            GameObject obj = gameManager.poolingManager.MakeObj(equip.stat.name);
            obj.transform.position = RanPos();
        }
        if(equip.isDropped && equip.isSpawn)
        {
            equip.img.fillAmount -= 1 / equip.stat.coolTime * Time.deltaTime;

            if(equip.img.fillAmount <= 0)
            {
                equip.img.fillAmount = 0;
                equip.isSpawn = false;
            }
        }
    }

    Vector2 RanPos()
    {
        Vector2 playerPos = gameManager.playerAI.transform.position;
        float a = playerPos.x;
        float b = playerPos.y;

        float x = Random.Range(-monsterRadius + a, monsterRadius + a);
        float y_b = Mathf.Sqrt(Mathf.Pow(monsterRadius, 2) - Mathf.Pow(x - a, 2));
        y_b *= Random.Range(0, 2) == 0 ? -1 : 1;
        float y = y_b + b;

        Vector2 randomPosition = new Vector2(x, y);

        return randomPosition;
    }
}
