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

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag))
        {
            Destroy(Player);
            Debug.Log(collision.name);
        }
    }
}