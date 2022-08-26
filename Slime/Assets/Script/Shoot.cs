using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    Rigidbody2D r;
    private float speed;

    public void Init(Vector3 position, Vector3 rotate, float speed, float lifetime)
    {
        r = gameObject.GetComponent<Rigidbody2D>();
        transform.position = position;
        transform.eulerAngles = rotate;
        this.speed = speed;
        r.velocity = transform.up * speed;
        Invoke(nameof(DestroyBullet), lifetime);   
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (gameObject.tag != "E1")
        {
            if (col.gameObject.layer != 8 && col.gameObject.name != "PlatformGround" && col.gameObject.layer != 14)
            {
                DestroyBullet();
            }
        }
        else
        {
            if (col.gameObject.layer != 7 && col.gameObject.name != "PlatformGround" && col.gameObject.layer != 12)
            {
                DestroyBullet();
            }
        }
    }

}
