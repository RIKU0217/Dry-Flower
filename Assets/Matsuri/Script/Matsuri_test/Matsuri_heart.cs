using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Matsuri_heart : MonoBehaviour
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
    private Image[] image;

    private int i = 0;

    public void ObjDestory()
    {
        if (Input.GetKeyDown("p") && i < 3)
        {
            image[i].enabled = false;
            Debug.Log(image[i].sprite);
            ++i;
        }
    }
}