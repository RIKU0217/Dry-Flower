using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ogame.System;

public class OgameSummonManager : MonoBehaviour
{
    public GameObject[] enemy;

    //bool isCalledOnce = false;

    private float summonTimer0 = 0;
    private float summonTimer1 = 4f;

    void Update()
    {
        SummonUp();
        SummonSide();
    }

    private void SummonUp()
    {
        if (summonTimer0 <= 0)
        {
            Vector3 t = new Vector2(Random.Range(-8f, 8f), 6f);
            byte random = (byte)Random.Range(0, 5);

            switch (random)
            {
                case 0:
                    SummonOneStraight(0, 2f, t, Test.FindToTargetDeg("FlagArea", t));
                    break;
                case 1:
                    SummonOneStraight(0, 2f, t, Test.FindToTargetDeg("FlagArea", t));
                    break;
                case 2:
                    SummonOneChaseAttack(2, 2f, t, 0);
                    break;
                case 3:
                    SummonOneStraightShoot(3, 2f, t, Test.FindToTargetDeg("FlagArea", t));
                    break;
                case 4:
                    SummonOneStraightShootNonStop(4, 2f, t, Test.FindToTargetDeg("FlagArea", t));
                    break;
            }

            summonTimer0 = 8f;
        }
        else
        {
            summonTimer0 -= Time.deltaTime;
        }
    }
    private void SummonSide()
    {
        if (summonTimer1 <= 0)
        {
            Vector3 t = new Vector2(9.5f, Random.Range(-5f, 5f));
            byte random = (byte)Random.Range(0, 5);

            switch (random)
            {
                case 0:
                    SummonOneStraight(0, 2f, t, Test.FindToTargetDeg("FlagArea", t));
                    break;
                case 1:
                    SummonOneStraight(0, 2f, t, Test.FindToTargetDeg("FlagArea", t));
                    break;
                case 2:
                    SummonOneChaseAttack(2, 2f, t, 0);
                    break;
                case 3:
                    SummonOneStraightShoot(3, 2f, t, Test.FindToTargetDeg("FlagArea", t));
                    break;
                case 4:
                    SummonOneStraightShootNonStop(4, 2f, t, Test.FindToTargetDeg("FlagArea", t));
                    break;
            }

            summonTimer1 = 8f;
        }
        else
        {
            summonTimer1 -= Time.deltaTime;
        }
    }

    //Target‚Ö‚ÌŠp“x‚ðŒvŽZ
    /*public float FindToTargetDeg(string s, Vector2 t)
    {
        Vector2 p1 = t;
        Vector2 p2 = GameObject.Find(s).transform.position;
        Vector2 dt = p2 - p1;
        float deg = Mathf.Atan2(dt.y, dt.x) * Mathf.Rad2Deg * (-1) - 90f;

        return deg;
    }*/

    //“G‚ð¢Š«‚·‚éŠÖ”
    public void SummonOneStraight(int b,float v, Vector2 t, float mrd)
    {
        GameObject ene = Instantiate(enemy[b], t, Quaternion.Euler(0, 0, mrd));
        ene.GetComponent<OgameEnemyStraight>().v = v;
    }
    public void SummonOneStraightAttack(int b, float v, Vector2 t, float mrd)
    {
        GameObject ene = Instantiate(enemy[b], t, Quaternion.Euler(0, 0, mrd));
        ene.GetComponent<OgameEnemyStraightAttack>().v = v;
        ene.GetComponent<OgameEnemyStraightAttack>().mrd = mrd;
    }
    public void SummonOneChaseAttack(int b, float v, Vector2 t, float mrd)
    {
        GameObject ene = Instantiate(enemy[b], t, Quaternion.Euler(0, 0, mrd));
        ene.GetComponent<OgameEnemyChaseAttack>().v = v;
    }
    public void SummonOneStraightShoot(int b, float v, Vector2 t, float mrd)
    {
        GameObject ene = Instantiate(enemy[b], t, Quaternion.Euler(0, 0, mrd));
        ene.GetComponent<OgameEnemyStraightShoot>().v = v;
    }
    public void SummonOneStraightShootNonStop(int b, float v, Vector2 t, float mrd)
    {
        GameObject ene = Instantiate(enemy[b], t, Quaternion.Euler(0, 0, mrd));
        ene.GetComponent<OgameEnemyStraightShootNoStop>().v = v;
    }
}
