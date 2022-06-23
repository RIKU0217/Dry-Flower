using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgameEnemyChaseAttack : MonoBehaviour
{

    public float v;

    public GameObject attackPoint;
    private float shootTimer;

    private float vX = 0f;
    private float vY = 0f;
    private float vT;
    private byte dir;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        vT = v;
    }

    void Update()
    {
        Directer();
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        if (distance >= 1.2f)
        {
            float rd = FindToPlayerDeg(this.transform.position);

            vX = vT * Mathf.Cos(rd * Mathf.Deg2Rad);
            vY = vT * Mathf.Sin(rd * Mathf.Deg2Rad);

            transform.position += new Vector3(vX, vY) * Time.deltaTime;
        }
        else
        {
            if (shootTimer <= 0)
            {
                SwordAttack();
                shootTimer = 1f;
            }
            else if (shootTimer > 0)
            {
                shootTimer -= Time.deltaTime;
            }
        }
    }

    private void Directer()
    {
        float rd = FindToPlayerDeg(this.transform.position);
        if ((rd >= 135 & rd <= 180f) | (rd >= -135f & rd < 180f))
        {
            dir = 2; //left
        }
        if (rd >= 45f & rd < 135f)
        {
            dir = 1; //back
        }
        if (rd >= -45 & rd < 45f)
        {
            dir = 3; //right
        }
        if (rd >= -135f & rd < -45f)
        {
            dir = 0; //front
        }
    }

    private void SwordAttack()
    {
        float[] pX = { -1.2f, 1.2f, -1.2f, 1.2f };
        float[] pY = { -1.2f, 1.2f, 1.2f, -1.2f };
        GameObject atp = Instantiate(attackPoint, this.transform.position + new Vector3(pX[dir], pY[dir], 0), 
                                                                            Quaternion.identity, this.transform);
        atp.GetComponent<OgameEnemyAttackPoint>().p = dir;
        Destroy(atp, 0.3f);
    }

    private float FindToPlayerDeg(Vector2 t)
    {
        Vector2 p1 = t;
        Vector2 p2 = player.transform.position;
        Vector2 dt = p2 - p1;
        float deg = Mathf.Atan2(dt.y, dt.x) * Mathf.Rad2Deg;

        return deg;
    }
}
