using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;


public class C_GManager : MonoBehaviour
{
    public static C_GManager instance = null;

    public C_PlayerController player;
    public CinemachineVirtualCamera goalCamera;
    public float initStayTime;

    [HideInInspector] public bool isHide = false;
    [HideInInspector] public bool isGameClear = false;
    [HideInInspector] public bool isGameOver = false;

    private string nextSceneName = "Chiguri"; //ŽŸ‚ÌƒV[ƒ“–¼
    private bool callOnce;
    private float timer;
    private int initPriority;
    private static bool cameraMoveEnd = false;

    private void Awake()
    {
        instance = this;
        if (!cameraMoveEnd)
        {
            player.enableKeyboard = false;
            timer = initStayTime;
            initPriority = goalCamera.Priority;
        }
        else
        {
            timer = 0f;
            initPriority = goalCamera.Priority;
        }
    }

    private void Start()
    {
        if (!cameraMoveEnd) goalCamera.Priority = 10;
    }


    private void Update()
    {

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (!cameraMoveEnd)
        {
            timer = 0f;
            player.enableKeyboard = true;
            cameraMoveEnd = true;
        }

        if(timer <= initStayTime/2 && goalCamera.Priority != initPriority)
        {
            goalCamera.Priority = initPriority;
        }

        if (isGameClear && C_BGM.instance.compVolFadeOut)
        {
            if (!callOnce)
            {
                Debug.Log("Goal!    Press 'Enter' to retry ");
                callOnce = true;
            }

            if (Input.GetKey(KeyCode.Return))
            {
                callOnce = false;
                isGameClear = false;
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
