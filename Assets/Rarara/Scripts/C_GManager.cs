using UnityEngine;
using UnityEngine.SceneManagement;


public class C_GManager : MonoBehaviour
{
    public static C_GManager instance = null;

    [HideInInspector] public bool isHide = false;
    [HideInInspector] public bool isGameClear = false;
    [HideInInspector] public bool isGameOver = false;

    private string nextSceneNama = "Chiguri"; //éüÇÃÉVÅ[Éìñº
    private bool callOnce;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (isGameClear && C_BGM.instance.compVolFadeOut)
        {
            if (!callOnce)
            {
                Debug.Log("Goal!    Press 'Enter' to retry ");
                callOnce = true;
            }

            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene(nextSceneNama);
                callOnce=false;
                isGameClear=false;
            }
        }
    }
}
