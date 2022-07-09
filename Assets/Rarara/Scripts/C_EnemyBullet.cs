using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_EnemyBullet : MonoBehaviour
{
    public float lifeTime = 5f;

    private float timeCounter = 0f;

    private void Update()
    {
        if(timeCounter > lifeTime)
        {
            Destroy(this.gameObject);
        }
        timeCounter += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
