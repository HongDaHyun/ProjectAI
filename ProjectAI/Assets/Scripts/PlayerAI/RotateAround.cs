using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    PlayerAI player;
    Transform target;

    private void Awake()
    {
        player = GetComponentInParent<PlayerAI>();
        target = transform.parent;
    }

    private void Update()
    {
        transform.RotateAround(target.position, Vector3.back, player.stat.ammo[3].bulletSpeed * Time.deltaTime);
    }
}
