using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ogame.System;

public class OgameEnemyStraightShoot : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public GameObject bullet;
    private float shootTimer = 0;

    public float v;
    public float rd;

    private float vX = 0f;
    private float vY = 0f;
    private float vT;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        vT = v;
    }

    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        if (distance >= 4f)
        {
            Animation(rd);
            vX = vT * Mathf.Cos(rd * Mathf.Deg2Rad);
            vY = vT * Mathf.Sin(rd * Mathf.Deg2Rad);

            transform.position += new Vector3(vX, vY) * Time.deltaTime;
        }
        else
        {
            if (shootTimer <= 0)
            {
                Animation(FindToTargetDeg("Player", this.transform.position));
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
        GameObject bul = Instantiate(bullet, this.transform.position, Quaternion.Euler(0, 0, FindToTargetDeg("Player", transform.position) + -90f));
        bul.GetComponent<OgameEnemyBullet>().v = 3f;
    }

    public float FindToTargetDeg(string s, Vector2 t)
    {
        Vector2 p1 = t;
        Vector2 p2 = GameObject.Find(s).transform.position;
        Vector2 dt = p2 - p1;
        float deg = Mathf.Atan2(dt.y, dt.x) * Mathf.Rad2Deg;

        return deg;
    }
    private void Animation(float rd)
    {
        float[] temp = ClassA.Direction(rd);
        anim.SetFloat("X", temp[0]);
        anim.SetFloat("Y", temp[1]);
    }
}