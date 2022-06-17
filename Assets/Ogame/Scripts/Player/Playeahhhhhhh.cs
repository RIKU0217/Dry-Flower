using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeahhhhhhh : MonoBehaviour
{
    public Vector2 playerdirection;
    private byte dir;

    public GameObject attackPoint;

    void Start()
    {

    }

    void Update()
    {
        Move();
        MoveClamp();
        Directer();
        Attack();
    }

    void Attack()
    {
        
    }

    void Move()
    {
        float x = Mathf.Round(Input.GetAxisRaw("Horizontal"));
        float y = Mathf.Round(Input.GetAxisRaw("Vertical"));

        if (x != 0 && y != 0)
        {
            transform.position += new Vector3(4f * x, 4f * y, 0) * Time.deltaTime / Mathf.Sqrt(2);
        }
        else
        {
            transform.position += new Vector3(4f * x, 4f * y, 0) * Time.deltaTime;
        }
    }

    void MoveClamp()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.5f, 3.5f), Mathf.Clamp(transform.position.y, -3.5f, 3.5f), 0);
    }

    void Directer()
    {
        //キーボードからの入力を格納
        playerdirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (playerdirection.x == 0 && playerdirection.y == -1)
        {
            dir = 0; //front
        }
        if (playerdirection.x == 0 && playerdirection.y == 1)
        {
            dir = 1; //back
        }
        if (playerdirection.x == -1 && playerdirection.y == 0)
        {
            dir = 2;//left
        }
        if (playerdirection.x == 1 && playerdirection.y == 0)
        {
            dir = 3; //right
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}