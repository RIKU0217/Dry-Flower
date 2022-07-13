using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 3f; //�ړ����x
    [SerializeField] private float moveTime = 2f; //�ړ�����
    [SerializeField] private float waitTime = 2f; //�ҋ@����
    [SerializeField] private BoxCollider2D area; //������͈�

    private float timeCounter = 0f;
    private float colXHalf;
    private bool isWait = true;
    private bool isMove = false;
    private Vector2 moveDir; //�i�s����

    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D col;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        colXHalf = col.size.x / 2;
        moveDir = RandomDirection();
    }

    private void FixedUpdate()
    {

        if ((rb.position.x < area.bounds.min.x + colXHalf) || (rb.position.x > area.bounds.max.x - colXHalf) || (rb.position.y < area.bounds.min.y + colXHalf) || (rb.position.y > area.bounds.max.y - colXHalf))
            Turn();

        if (isWait)
            Wait();
        else if (isMove)
            Move();
    }

    //�ҋ@����
    private void Wait()
    {
        if (waitTime > timeCounter)
        {
            timeCounter += Time.deltaTime;
            rb.velocity = Vector2.zero;
        }
        else
        {
            isWait = false;
            isMove = true;
            timeCounter = 0f;
        }
    }

    //�A�j���[�V��������
    private void Animate()
    {
        if (moveDir == Vector2.up)
        {
            anim.SetFloat("X", 0);
            anim.SetFloat("Y", 1f);
        }
        else if (moveDir == Vector2.down)
        {
            anim.SetFloat("X", 0);
            anim.SetFloat("Y", -1f);
        }
        else if (moveDir == Vector2.left)
        {
            anim.SetFloat("X", -1f);
            anim.SetFloat("Y", 0);
        }
        else if (moveDir == Vector2.right)
        {
            anim.SetFloat("X", 1f);
            anim.SetFloat("Y", 0);
        }
    }

    //�����_���ɏ㉺���E�̕����𐶐�
    private Vector2 RandomDirection()
    {
        switch(Random.Range(0, 4))
        {
            default:
            case 0:
                return Vector2.up;
            case 1:
                return Vector2.down;
            case 2:
                return Vector2.left;
            case 3:
                return Vector2.right;
        }
    }

    //�ړ�����
    private void Move()
    {
        if(moveTime > timeCounter)
        {
            timeCounter += Time.deltaTime;
            rb.velocity = moveDir * speed;
            Animate();
        }
        else
        {
            moveDir = RandomDirection();
            isMove = false;
            isWait = true;
            timeCounter = 0f;
        }
    }

    //�^�[�����鏈��
    private void Turn()
    {
        if (moveDir == Vector2.up)
            rb.position = new Vector2(rb.position.x, area.bounds.max.y - colXHalf);
        else if (moveDir == Vector2.down)
            rb.position = new Vector2(rb.position.x, area.bounds.min.y + colXHalf);
        else if (moveDir == Vector2.left)
            rb.position = new Vector2(area.bounds.min.x + colXHalf, rb.position.y);
        else if (moveDir == Vector2.right)
            rb.position = new Vector2(area.bounds.max.x - colXHalf, rb.position.y);

        moveDir = -moveDir;
    }
}
