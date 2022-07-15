using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HwitFrash : MonoBehaviour
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
    GameObject Howit;
    public void Howitfrashtrue()
    {
        Howit.SetActive(true);
    }

    public void Howitfrashfale()
    {
        Howit.SetActive(false);
    }
}
