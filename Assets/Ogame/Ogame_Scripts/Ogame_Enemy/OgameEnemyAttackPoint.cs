using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgameEnemyAttackPoint : MonoBehaviour
{
    public byte p;
    private float[] vX = { 1f, -1f, 0, 0 };
    private float[] vY = { 0, 0, -1f, 1f };

    void Start()
    {

    }

    void Update()
    {
        transform.position += new Vector3(vX[p], vY[p], 0) * 8f * Time.deltaTime;
    }
}
