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
    /// SE��炷(0:�Q�[���I�[�o�[ 1:�� 2:��e 3:�U�� 4:UI 5:�R�C��)
    /// </summary>
    /// <param name="x"></param>
    public void PlaySE(int x)
    {
        se[x].Stop();

        se[x].Play();
    }



}
