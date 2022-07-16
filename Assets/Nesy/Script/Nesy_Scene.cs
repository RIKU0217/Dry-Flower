using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nesy_Scene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [SerializeField]
    private string sceneName;
    public void SceneLoad()
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log(sceneName);
    }
}
