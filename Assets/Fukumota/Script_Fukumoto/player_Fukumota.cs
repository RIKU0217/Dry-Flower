using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class player_Fukumota : MonoBehaviour
{
    public Rigidbody2D g;
    private Vector2 dir;
    private float speed = 3f;
    public GameObject j;
    public static player_Fukumota instance3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        director();
        Move();
    }
    private void Awake()
    {
        if (instance3 == null)
        {
            instance3 = this;
        }
    }
    public void disappearPlayer()
    {
        j.SetActive(false);
    }
    private void director()
    {
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void Move()
    {
        g.velocity = dir * speed;
    }

    //è’ìÀÇÃîªíË
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            life_Fukumota.instance.life_down();
            Debug.Log("è’ìÀ");
        }
    }
}