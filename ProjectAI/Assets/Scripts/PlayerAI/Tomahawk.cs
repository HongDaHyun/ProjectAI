using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomahawk : Projectile
{
    public bool isLeft;
    public Vector2 direction;

    public override void OnEnable()
    {
        base.OnEnable();
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    protected override void Update()
    {
        ShootStraightNoRotate(direction);
        if(!isLeft)
            transform.Rotate(new Vector3(0, 0, -600f * Time.deltaTime));
        else if(isLeft)
            transform.Rotate(new Vector3(0, 0, 600f * Time.deltaTime));
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        ProjectileBorder(collision);
    }
}
