using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nesy_Sneak1: MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private string Tag;

    void Start()
    {
        gamemaneger = GameObject.Find("GameManeger");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject gamemaneger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag))
        {
            Destroy(Player);
            gamemaneger.GetComponent<GameManeger>().SceneLoad();
            Debug.Log(collision.name);
        }
    }
}
