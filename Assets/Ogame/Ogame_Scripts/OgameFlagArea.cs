using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgameFlagArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            C_GManager.instance.isGameOver = true;
        }
    }
}