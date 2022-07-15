using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nesy_ActiveState : MonoBehaviour
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
    public GameObject Obj;

    public void SetFalse()
    {
        Obj.SetActive(false);
    }
}
