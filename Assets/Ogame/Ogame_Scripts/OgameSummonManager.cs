using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Ogame.System;

public class OgameSummonManager : MonoBehaviour
{
    public GameObject[] enemy;

    private bool summonTurn;

    //bool isCalledOnce = false;

    private float summonTimer = 0;
    private float summonInterval = 3f;
    private int randomRange = 2;

    private float gameTimer = 0;
    private float clearTime = 60f;

    void Start()
    {
        summonTurn = true;
        gameTimer = clearTime;
    }

    void Update()
    {
        Summon();
        GameTimer();
        Clear();
        /*if (Input.GetKeyDown(KeyCode.Z))
        {
            Vector3 t = new Vector2(Random.Range(-8f, 8f), 6f);
            //SummonOneStraight(0, 2f, t, ClassA.FindToTargetDeg("FlagArea", t));
            //SummonOneStraightAttack(1, 2f, t, ClassA.FindToTargetDeg("FlagArea", t));
            //SummonOneStraightShoot(3, 2f, t, ClassA.FindToTargetDeg("FlagArea", t));
            //SummonOneStraightShootNonStop(4, 2f, t, ClassA.FindToTargetDeg("FlagArea", t));
        }*/
    }

    private void Summon()
    {
        if (summonTimer <= 0)
        {
            Vector3 t = new Vector3(0, 0, 0);
            byte random = 0;
            Debug.Log((int)gameTimer);

            if (summonTurn)
            {
                t = new Vector2(Random.Range(-9f, 7f), 5.5f);
                random = (byte)Random.Range(0, randomRange);
                summonTurn = false;
            }
            else if(!summonTurn)
            {
                t = new Vector2(7.5f, Random.Range(-4.5f, 4f));
                random = (byte)Random.Range(0, randomRange);
                summonTurn = true;
            }

            switch (random)
            {
                case 0:
                    SummonOneStraight(0, 1.5f, t, ClassA.FindToTargetDeg("FlagArea", t));
                    break;
                case 1:
                    SummonOneStraightAttack(1, 1.5f, t, ClassA.FindToTargetDeg("FlagArea", t));
                    break;
                case 2:
                    SummonOneChaseAttack(2, 1.5f, t, 0);
                    break;
                case 3:
                    SummonOneStraightShoot(3, 1.5f, t, ClassA.FindToTargetDeg("FlagArea", t));
                    break;
                case 4:
                    SummonOneStraightShootNonStop(4, 1.5f, t, ClassA.FindToTargetDeg("FlagArea", t));
                    break;
            }

            summonTimer = summonInterval;
        }
        else
        {
            summonTimer -= Time.deltaTime;
        }
    }

    private void GameTimer()
    {
        if (!C_GManager.instance.isGameOver)
        {
            gameTimer -= Time.deltaTime;
        }
        if (gameTimer < clearTime * 2 / 3 & gameTimer >= clearTime / 3)
        {
            randomRange = 4;
            summonInterval = 2f;
        }
        else if (gameTimer < clearTime / 3)
        {
            summonInterval = 1.5f;
        }
    }

    private void Clear()
    {
        if (gameTimer <= 0)
        {
            SceneManager.LoadScene("10");
        }
    }

    //“G‚ð¢Š«‚·‚éŠÖ”
    public void SummonOneStraight(int b,float v, Vector2 t, float mrd)
    {
        GameObject ene = Instantiate(enemy[b], t, Quaternion.identity);
        ene.GetComponent<OgameEnemyStraight>().v = v;
        ene.GetComponent<OgameEnemyStraight>().rd = mrd;
    }
    public void SummonOneStraightAttack(int b, float v, Vector2 t, float mrd)
    {
        GameObject ene = Instantiate(enemy[b], t, Quaternion.identity);
        ene.GetComponent<OgameEnemyStraightAttack>().v = v;
        ene.GetComponent<OgameEnemyStraightAttack>().mrd = mrd;
    }
    public void SummonOneChaseAttack(int b, float v, Vector2 t, float mrd)
    {
        GameObject ene = Instantiate(enemy[b], t, Quaternion.identity);
        ene.GetComponent<OgameEnemyChaseAttack>().v = v;
    }
    public void SummonOneStraightShoot(int b, float v, Vector2 t, float mrd)
    {
        GameObject ene = Instantiate(enemy[b], t, Quaternion.identity);
        ene.GetComponent<OgameEnemyStraightShoot>().v = v;
        ene.GetComponent<OgameEnemyStraightShoot>().rd = mrd;
    }
    public void SummonOneStraightShootNonStop(int b, float v, Vector2 t, float mrd)
    {
        GameObject ene = Instantiate(enemy[b], t, Quaternion.identity);
        ene.GetComponent<OgameEnemyStraightShootNoStop>().v = v;
        ene.GetComponent<OgameEnemyStraightShootNoStop>().rd = mrd;
    }
}
