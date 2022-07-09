using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nesy_Fungus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Fungus.Flowchart flowchart = null;
    [SerializeField]
    private string Message;
    public void StartFungus()
    {
        flowchart.SendFungusMessage(Message);
        Debug.Log("ee");
    }
}
