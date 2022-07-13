using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed;//�����̈ړ��X�s�[�h���擾����

    private Vector2 playerdirection;//�����̌������擾����


    private Rigidbody2D rb;
    private Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Directer();
        Move();
        Animation();
    }

    private void Directer()
    {
        //�L�[�{�[�h����̓��͂��i�[
        playerdirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void Move()
    {
        //���W�b�h�{�f�B�ɗ͉����邱�ƂŃL�����𓮂���
        rb.velocity = playerdirection * speed;
    }

    private void Animation()
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
}
