using UnityEngine;

public class C_CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;


    void Update()
    {
        this.transform.position = player.transform.position;
    }
}
