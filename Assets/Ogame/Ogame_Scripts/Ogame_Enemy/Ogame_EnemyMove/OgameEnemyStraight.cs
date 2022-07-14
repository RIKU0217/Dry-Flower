using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ogame.System;

public class OgameEnemyStraight : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public float v;
    public float rd;

    private float vX = 0f;
    private float vY = 0f;
    private float vT;

    void Start()
    {
        vT = v;
    }

    void Update()
    {
        vX = vT * Mathf.Cos(rd * Mathf.Deg2Rad);
        vY = vT * Mathf.Sin(rd * Mathf.Deg2Rad);

        transform.position += new Vector3(vX, vY) * Time.deltaTime;

        Animation(rd);
    }

    private void Animation(float rd)
    {
        float[] temp = ClassA.Direction(rd);
        anim.SetFloat("X", temp[0]);
        anim.SetFloat("Y", temp[1]);
    }
}
