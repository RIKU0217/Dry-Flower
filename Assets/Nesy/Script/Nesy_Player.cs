using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nesy_Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Vector2 playerdirection;//自分の向きを取得する

    public Vector2 thistransform;

    [SerializeField, Tooltip("移動スピード")]
    private int speed;//自分の移動スピードを取得する


    public Rigidbody2D rb2d;//自分のリジッドボディを取得する

    [SerializeField, Tooltip("Player-animation")]
    private Animator anim;

    [SerializeField, Tooltip("キーボード入力のオンオフ")]
    public bool onoff;


    void Start()
    {
        playerdirection.x = 0;
        playerdirection.y = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (onoff == true)
        {
            Move();
        }

        Directer();
        Animation();

    }

    
    public void Animation()
    {
        if (playerdirection.x == 0 && playerdirection.y == -1)
        {
            anim.SetFloat("X", 0);
            anim.SetFloat("Y", -1f);
        }
        if (playerdirection.x == 0 && playerdirection.y == 1)
        {
            anim.SetFloat("X", 0);
            anim.SetFloat("Y", 1f);//back
        }
        if (playerdirection.x == -1 && playerdirection.y == 0)
        {
            anim.SetFloat("X", -1f);
            anim.SetFloat("Y", 0);//left
        }
        if (playerdirection.x == 1 && playerdirection.y == 0)
        {
            anim.SetFloat("X", 1f);
            anim.SetFloat("Y", 0);//right
        }
    }

    public void FrontAnim()
    {
        spriteRenderer.sprite = sprites[0];
    }
    public void BackAnim()
    {
        spriteRenderer.sprite = sprites[1];//back
    }
    public void RightAnim()
    {
        spriteRenderer.sprite = sprites[2];//right
    }
    public void LeftAnim()
    {
        spriteRenderer.sprite = sprites[3];//left
    }

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] sprites;

    public void FSprite()
    {
        spriteRenderer.sprite = sprites[0];
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
