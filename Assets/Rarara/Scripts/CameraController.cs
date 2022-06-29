using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;


    void Update()
    {
        this.transform.position = player.transform.position;
    }
}
