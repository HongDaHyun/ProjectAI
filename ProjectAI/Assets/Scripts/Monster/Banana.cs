using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : Atk
{
    public bool isLeft;
    public Vector2 direction;

    public override void OnEnable()
    {
        base.OnEnable();
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        ShootStraightNoRotate(direction);
        if (!isLeft)
            transform.Rotate(new Vector3(0, 0, -600f * Time.deltaTime));
        else if (isLeft)
            transform.Rotate(new Vector3(0, 0, 600f * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
