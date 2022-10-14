using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlot : ItemSlot
{
    public bool isSpawn;

    private void Update()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        if (isDropped && !isSpawn)
        {
            isSpawn = true;
            GameObject obj = gameManager.poolingManager.MakeObj(stat.name);
            obj.transform.position = RanPos(5f);
            yield return new WaitForSeconds(stat.coolTime);
            isSpawn = false;
        }
    }

    Vector2 RanPos(float radius)
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
}
