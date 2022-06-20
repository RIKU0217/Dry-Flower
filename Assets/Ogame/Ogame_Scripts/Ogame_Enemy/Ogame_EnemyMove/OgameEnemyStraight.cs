using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgameEnemyStraight : MonoBehaviour
{

    public float v;

    private float vX = 0f;
    private float vY = 0f;
    private float vT;

    void Start()
    {
        vT = -v;
    }

    void Update()
    {
        float rd = this.transform.eulerAngles.z;

        vX = vT * Mathf.Sin(rd * Mathf.Deg2Rad);
        vY = vT * Mathf.Cos(rd * Mathf.Deg2Rad);

        transform.position += new Vector3(vX, vY) * Time.deltaTime;
    }
}
