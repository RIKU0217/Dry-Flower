using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgameEnemyStraightShoot : MonoBehaviour
{
    public GameObject bullet;
    private float shootTimer = 0;

    public float v;

    private float vX = 0f;
    private float vY = 0f;
    private float vT;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        vT = -v;
    }

    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        if (distance >= 4f)
        {
            float rd = this.transform.eulerAngles.z;

            vX = vT * Mathf.Sin(rd * Mathf.Deg2Rad);
            vY = vT * Mathf.Cos(rd * Mathf.Deg2Rad);

            transform.position += new Vector3(vX, vY) * Time.deltaTime;
        }
        else
        {
            if (shootTimer <= 0)
            {
                Shoot();
                shootTimer = 0.5f;
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