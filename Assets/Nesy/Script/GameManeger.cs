using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManeger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Swith();
    }

    [SerializeField]
    public GameObject gameObject;
    public void Swith()
    {
        if(gameObject == null)
        {
            SceneLoad();
        }
       
    }
    [SerializeField]
    private string sceneName;
    public void SceneLoad()
    {
        SceneManager.LoadScene(sceneName);
    }
}
