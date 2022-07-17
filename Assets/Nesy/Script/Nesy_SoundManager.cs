using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nesy_SoundManager : MonoBehaviour
{
    public static Nesy_SoundManager instance;

    public AudioSource[] se;

    //private void Awake()
    //{

      //  if (instance == null)
       // {
         //   instance = this;
       // }
       // else if (instance != this)
        //{
         //   Destroy(gameObject);
        //}

        //DontDestroyOnLoad(gameObject);
    //}
    /// <summary>
    /// SEðÂç·(0:Q[I[o[ 1:ñ 2:íe 3:U 4:UI 5:RC)
    /// </summary>
    /// <param name="x"></param>
    public void PlaySE(int x)
    {
        se[x].Stop();

        se[x].Play();
    }

    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioSource Stopsource;
    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private float Volume;

    public void Clip()
    {
        source.PlayOneShot(clip);
        Debug.Log(source);
    }

    public void SEvolume()
    {
        source.volume = Volume;
    }

    public void StopBGM()
    {
        Stopsource.enabled = false;
    }



}
