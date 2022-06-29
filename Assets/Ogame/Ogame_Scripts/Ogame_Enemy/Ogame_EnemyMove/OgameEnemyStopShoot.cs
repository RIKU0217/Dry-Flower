using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgameEnemyStopShoot : MonoBehaviour
{
    public GameObject bullet;
    private float shootTimer;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        if (distance <= 6f)
        {
            if (shootTimer <= 0)
            {
                Shoot();
                shootTimer = 1f;
            }
            else if (shootTimer > 0)
            {
                shootTimer -= Time.deltaTime;
            }
        }
    }

    void Shoot()
    {
        GameObject bul = Instantiate(bullet, this.transform.position, Quaternion.Euler(0, 0, FindToTargetDeg("Player", transform.position)));
        bul.GetComponent<OgameEnemyBullet>().v = 3f;
    }

    public float FindToTargetDeg(string s, Vector2 t)
    {
        Vector2 p1 = t;
        Vector2 p2 = GameObject.Find(s).transform.position;
        Vector2 dt = p2 - p1;
        float deg = Mathf.Atan2(dt.y, dt.x) * Mathf.Rad2Deg + 90f;

        return deg;
    }
}