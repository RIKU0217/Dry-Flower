using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nesy_NewPlayer : MonoBehaviour
{
    private Vector2 playerdirection;//自身の向きを取得
    private Rigidbody2D rb2d;//自分のリジッドボディを取得
    [SerializeField, Tooltip("移動スピード")]
    private int speed;//自分の移動スピードを取得する

    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Directer();
        Move();
    }

    public void Directer()
    {
        //キーボードからの入力を格納
        playerdirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    public void Move()
    {
        //リジッドボディに力加えることでキャラを動かす
        rb2d.velocity = playerdirection * speed;
    }
}
