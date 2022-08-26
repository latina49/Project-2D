using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyATK : Enemy
{
    public static EnemyATK instance;
    public GameObject Bullet;
    public GameObject BulletSpawn;

    public void PlayShoot()
    {
        
        GameObject g = GameObject.Instantiate(Bullet, null);
        g.transform.position = BulletSpawn.transform.position;
        g.transform.eulerAngles = BulletSpawn.transform.eulerAngles;
        Shoot b = g.GetComponent<Shoot>();
        b.Init(BulletSpawn.transform.position, BulletSpawn.transform.eulerAngles, 1.5f, 2.5f);
    }
}
