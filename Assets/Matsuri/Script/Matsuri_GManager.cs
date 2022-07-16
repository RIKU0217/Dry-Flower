using UnityEngine;
using UnityEngine.SceneManagement;


public class Matsuri_GManager : MonoBehaviour
{
    public static Matsuri_GManager instance = null;

    [HideInInspector] public bool isHide;
    [HideInInspector] public bool isGameClear;
    [HideInInspector] public bool isGameOver;
    public string nextScene = "Matsuri_MainScene";

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
                SceneManager.LoadScene(nextScene);
                callOnce=false;
                isGameClear=false;
            }
        }
    }
}
