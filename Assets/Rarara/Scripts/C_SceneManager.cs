using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class C_SceneManager : MonoBehaviour
{
    [SerializeField, Tooltip("Retry画面のGameObject")] GameObject retryObj;
    private bool firstPush = false;
    private bool pressRetry = false;

    private void Awake()
    {
        retryObj.SetActive(false);
    }

    /// <summary>
    /// retryボタンを押したときの処理
    /// </summary>
    public void PressRetry()
    {
        if (!firstPush)
        {
            firstPush = true;
            pressRetry = true;
        }
    }

    private void Update()
    {
        if (C_GManager.instance.isGameOver)
        {
            retryObj.SetActive(true);
        }

        if (pressRetry)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            C_GManager.instance.isGameOver = false;
            C_GManager.instance.isHide = false;
            pressRetry = false;
        }
    }
}
