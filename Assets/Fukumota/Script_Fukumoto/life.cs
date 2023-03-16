using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class life : MonoBehaviour
{
    private const int V = 1;
    public GameObject[] lifeArray = new GameObject[4];
    private int lifePoint = 3;
    public static life instance;
    public gameOver gameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeArray[lifePoint].SetActive(true);
    }

    //instance‚ðŽg‚¤ŠÖ”
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    //HP‚ÌŒ¸­
    public void life_down()
    {
        if (lifePoint >= 1)
        {
            lifePoint--;
            lifeArray[lifePoint + 1].SetActive(false);
            if (lifePoint == 0)
            {
                player.instance3.disappearPlayer();
                gameOver.instance2.gameOverSystem();
            }
        }
    }
}
