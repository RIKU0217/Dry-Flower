using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgameSummonManager : MonoBehaviour
{
    public GameObject[] enemy;

    bool isCalledOnce = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.C) && isCalledOnce == false)
        {
            Vector3 t = new Vector2(Random.Range(-8f, 8f), 6f);
            //SummonOneStraight(0, 3f, t, FindToTargetDeg("FlagArea", t));
            //SummonOneStraightAttack(1, 3f, t, FindToTargetDeg("Player", t));
            //SummonOneChaseAttack(2, 3f, t, 0);
            SummonOneStraightShoot(3, 3f, t, FindToTargetDeg("FlagArea", t));
            //SummonOneStraightShootNoStop(4, 3f, t, FindToTargetDeg("Player", t));
            isCalledOnce = true;
        }
    }

    //TargetÇ÷ÇÃäpìxÇåvéZ
    public float FindToTargetDeg(string s, Vector2 t)
    {
        Vector2 p1 = t;
        Vector2 p2 = GameObject.Find(s).transform.position;
        Vector2 dt = p2 - p1;
        float deg = Mathf.Atan2(dt.y, dt.x) * Mathf.Rad2Deg * (-1) - 90f;

        return deg;
    }

    //ìGÇè¢ä´Ç∑ÇÈä÷êî
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
    public void SummonOneStraightShootNoStop(int b, float v, Vector2 t, float mrd)
    {
        GameObject ene = Instantiate(enemy[b], t, Quaternion.Euler(0, 0, mrd));
        ene.GetComponent<OgameEnemyStraightShootNoStop>().v = v;
    }
}
