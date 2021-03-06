using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Matsuri_SceneManager : MonoBehaviour
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
        if (Matsuri_GManager.instance.isGameOver)
        {
            retryObj.SetActive(true);
        }

        if (pressRetry)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            pressRetry = false;
        }
    }
}
