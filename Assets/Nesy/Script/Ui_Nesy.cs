using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_Nesy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject nextGO;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("ç∂É{É^ÉìÇ™âüÇ≥ÇÍÇ‹ÇµÇΩ");
            nextGO.SetActive(true);
        }
    }
}
