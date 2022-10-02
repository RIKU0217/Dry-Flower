using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class C_BGM : MonoBehaviour
{
    public static C_BGM instance;

    [SerializeField, Range(0f, 1f), Tooltip("最大音量")] private float max = 1f;
    [SerializeField, Range(0f, 10f), Tooltip("フェード時間")] private float fadeTime = 1f;
    [SerializeField, Tooltip("発見時のSE")] private AudioClip findedSE;

    //[HideInInspector] public bool compVolFadeIn = false; //フェードインの終了判定
    [HideInInspector] public bool compVolFadeOut = false; //フェードアウトの終了判定

    private AudioSource audioSource;
    private bool fadeIn = false;
    private bool fadeOut = false;
    private float timer = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(this.gameObject);
            VolumeFadeStart("in");
        }
        else
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            VolumeFadeInUpdate();
        }
        else if (fadeOut)
        {
            VolumeFadeOutUpdate();
        }

        if (C_GManager.instance.isGameClear && !compVolFadeOut)
        {
            SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());
            VolumeFadeStart("out");
        }
    }

    private void VolumeFadeStart(string str)
    {
        if (fadeIn || fadeOut)
        {
            return;
        }

        switch (str)
        {
            default:
                return;
            case "in":
                audioSource.volume = 0f;
                fadeIn = true;
                break;
            case "out":
                audioSource.volume = max;
                fadeOut = true;
                break;
        }
    }

    private void VolumeFadeOutUpdate()
    {
        if(timer < fadeTime)
        {
            audioSource.volume = max - timer / fadeTime * max;
            timer += Time.deltaTime / fadeTime;
        }
        else
        {
            audioSource.volume = 0f;
            timer = 0f;
            fadeOut = false;
            compVolFadeOut = true;
        }
    }

    private void VolumeFadeInUpdate()
    {
        if (timer < fadeTime)
        {
            audioSource.volume = timer / fadeTime * max;
            timer += Time.deltaTime / fadeTime;
        }
        else
        {
            audioSource.volume = max;
            timer = 0f;
            fadeIn = false;
        }
    }

    public void OnFindedSE()
    {
        audioSource.PlayOneShot(findedSE);
    }
}
