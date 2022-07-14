using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FootCheck : MonoBehaviour
{
    private string hideTag = "Bush";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == hideTag)
        {
            transform.root.gameObject.GetComponent<C_PlayerController>().footCheck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == hideTag)
        {
            transform.root.gameObject.GetComponent<C_PlayerController>().footCheck = false;
        }
    }

}
