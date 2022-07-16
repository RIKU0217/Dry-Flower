using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Matsuri_OgamePlayer : MonoBehaviour
{
    public byte life;

    [SerializeField] private Vector2 playerdirection;
    private byte dir;
    private bool playPermission = true;

    [SerializeField] private GameObject attackPoint;
    [SerializeField] private float speed;

    private bool isCalledOnce = false;
    private GameObject at;

    [SerializeField, Tooltip("Player-animation")]
    private Animator anim;

    [SerializeField] private Rigidbody2D rb2d;//自分のリジッドボディを取得する



    //UI関係
    //TextMeshProUGUI hPScore;

    void Start()
    {
        life = 6;
        //hPScore = GameObject.Find("HPScore").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (playPermission == true)
        {
            Move();
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
        //MoveClamp();
        Directer();
        Attack();
        //HPScore();
        Animation();
    }

    void Attack()
    {
        float[] attackX = { 0, 0, -0.8f, 0.8f };
        float[] attackY = { -0.8f, 0.8f, 0, 0 };
        float[] attackR = { 0, 0, 90f, 90f };

        if (Input.GetKey(KeyCode.Space) & isCalledOnce == false && playPermission)
        {
            at = Instantiate(attackPoint, new Vector3(transform.position.x + attackX[dir], transform.position.y + attackY[dir], 0),
                                                                Quaternion.Euler(0, 0, attackR[dir]), this.transform);
            isCalledOnce = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            playPermission = false;
            Invoke("DestroyAttackPoint", 0.5f);
            Invoke("CoolTimeReset", 1f);
        }
    }
    void DestroyAttackPoint()
    {
        Destroy(at);
        if(!Matsuri_GManager.instance.isGameClear && !Matsuri_GManager.instance.isGameOver)
            playPermission = true;
    }
    void CoolTimeReset()
    {
        isCalledOnce = false;
    }

    void Move()
    {
        rb2d.velocity = playerdirection * speed;
    }

    void MoveClamp()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10f, 6.7f), Mathf.Clamp(transform.position.y, -4.5f, 3.75f), 0);
    }

    void Directer()
    {
        if(playPermission)
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
    public void Animation()
    {
        if (playerdirection.x == 0 && playerdirection.y == -1)
        {
            anim.SetFloat("X", 0);
            anim.SetFloat("Y", -1f);//front
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

    private void HPScore()
    {
        //hPScore.SetText("HP : {0}", life / 2);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyAttackPoint"))
        {
            Matsuri_GManager.instance.isGameOver = true;
            //if (life > 2)
            //{
            //    life -= 2;
            //}
            //else
            //{
            //    life = 0;
            //    C_GManager.instance.isGameOver = true;
            //}
            Destroy(other.gameObject);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            playPermission = false;
        }
        else if (other.gameObject.CompareTag("EnemyBullet"))
        {
            Matsuri_GManager.instance.isGameOver = true;
            //if (life > 1)
            //{
            //    life -= 1;
            //}
            //else
            //{
            //    life = 0;
            //    C_GManager.instance.isGameOver = true;
            //}
            Destroy(other.gameObject);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            playPermission = false;
        }
        else if (other.gameObject.CompareTag("Goal"))
        {
            Matsuri_GManager.instance.isGameClear = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            playPermission = false;
        }
    }
}