using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOver : MonoBehaviour
{
    public GameObject k;
    public static gameOver instance2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Awake()
    {
        if (instance2 == null)
        {
            instance2 = this;
        }
    }
    public void gameOverSystem()
    {
        Invoke("gameOverDisplay", 1);
    }
    public void gameOverDisplay()
    {
        k.SetActive(true);
        Debug.Log("GameOver");
    }
}
