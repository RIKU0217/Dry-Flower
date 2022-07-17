using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgameFlagArea : MonoBehaviour
{
    public GameObject gameover;
    public GameObject dark;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameover.SetActive(true);
            dark.SetActive(true);
            Time.timeScale = 0;
        }
    }
}