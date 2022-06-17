using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nesy_SoundManager : MonoBehaviour
{
    public static Nesy_SoundManager instance;

    public AudioSource[] se;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    /// <summary>
    /// SEを鳴らす(0:ゲームオーバー 1:回復 2:被弾 3:攻撃 4:UI 5:コイン)
    /// </summary>
    /// <param name="x"></param>
    public void PlaySE(int x)
    {
        se[x].Stop();

        se[x].Play();
    }



}
