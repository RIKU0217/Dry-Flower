using UnityEngine;
using UnityEngine.SceneManagement;


public class C_GManager : MonoBehaviour
{
    public static C_GManager instance = null;

    [HideInInspector] public bool isHide;
    [HideInInspector] public bool isGameClear;
    [HideInInspector] public bool isGameOver;

    private bool callOnce;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (isGameClear)
        {
            if (!callOnce)
            {
                Debug.Log("Goal!    Press 'Enter' to retry ");
                callOnce = true;
            }

            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("Chiguri");
                callOnce=false;
                isGameClear=false;
            }
        }
    }

    private void Update()
    {
        if (isGameClear)
        {
            if (!callOnce)
            {
                Debug.Log("Goal!    Press 'Enter' to retry ");
                callOnce = true;
            }

            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("Chiguri");
                callOnce=false;
                isGameClear=false;
            }
        }
        
        if (isGameOver)
        {
            if (!callOnce)
            {
                Debug.Log("GameOver!    Press 'Enter' to retry ");
                callOnce = true;
            }

            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("Chiguri");
                callOnce = false;
                isGameOver = false;
            }
        }
    }
}
