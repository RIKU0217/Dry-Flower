using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Matsuri_heaert : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ObjDestory();
    }

    [SerializeField]
    public Image image;
    public void ObjDestory()
    {
        if (Input.GetKeyDown("p"))
        {
            Destroy(image);
            Debug.Log(image.sprite);
        }
    }
}
