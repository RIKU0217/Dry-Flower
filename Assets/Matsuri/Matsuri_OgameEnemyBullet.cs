using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matsuri_OgameEnemyBullet : MonoBehaviour
{

    public float v;

    private float vX = 0f;
    private float vY = 0f;
    private float vT;

    void Start()
    {
        vT = v;
    }

    void Update()
    {
        Transform myTransform = this.transform;
        Vector3 angle = myTransform.eulerAngles;
        float rd = angle.z;

        vX = vT * -Mathf.Sin(rd * Mathf.Deg2Rad);
        vY = vT * Mathf.Cos(rd * Mathf.Deg2Rad);

        transform.position += new Vector3(vX, vY) * Time.deltaTime;

        //Á‹Ž
    //    if (transform.position.y > 6f | transform.position.y < -6f | transform.position.x > 10f | transform.position.x < -10f)
    //    {
    //        Destroy(this.gameObject);
    //    }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
        Debug.Log("nessi");
    }
}
