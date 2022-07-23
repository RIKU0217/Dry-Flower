using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nesy_Goal : MonoBehaviour
{
    private Rigidbody2D ribo;
    void Start()
    {
        ribo = GetComponent<Rigidbody2D>();   
    }

    public Fungus.Flowchart flowchart = null;
    [SerializeField]
    private string Message;

    [SerializeField]
    public GameObject obj;
    [SerializeField]
    private Collider2D coll1;
    [SerializeField]
    private Collider2D coll2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            //coll1.enabled = false;
            //coll2.enabled = false;
            //obj.SetActive(false);
            flowchart.SendFungusMessage(Message);
            Debug.Log(Message);
        }
    }

}
