using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_HideCheck : MonoBehaviour
{
    private string hideTag = "Bush";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == hideTag)
        {
            C_GManager.instance.isHide = true;
            C_StageManager.instance.InHide();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == hideTag)
        {
            C_GManager.instance.isHide = false;
            C_StageManager.instance.OutHide();
        }
    }

}
