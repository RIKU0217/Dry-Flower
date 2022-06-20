using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgameEnemyChaseAttack : MonoBehaviour
{

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

        transform.rotation = Quaternion.Euler(0, 0, FindToPlayerDeg(this.transform.position));
        if (distance >= 2f)
        {
            float rd = this.transform.eulerAngles.z;

            vX = vT * Mathf.Sin(rd * Mathf.Deg2Rad);
            vY = vT * Mathf.Cos(rd * Mathf.Deg2Rad);

            transform.position += new Vector3(vX, vY) * Time.deltaTime;
        }
    }

    public float FindToPlayerDeg(Vector2 t)
    {
        Vector2 p1 = t;
        Vector2 p2 = player.transform.position;
        Vector2 dt = p2 - p1;
        float deg = Mathf.Atan2(dt.y, dt.x) * Mathf.Rad2Deg * (-1) - 90f;

        return deg;
    }
}
