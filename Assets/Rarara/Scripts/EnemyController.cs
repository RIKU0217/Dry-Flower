using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f; //�ړ����x
    [SerializeField] private float moveTime = 2f; //�ړ�����
    [SerializeField] private float waitTime = 2f; //�ҋ@����
    [SerializeField] private BoxCollider2D area; //������͈�
    [SerializeField] private Rigidbody2D target;
    [SerializeField] private float sightAngle = 160f;
    [SerializeField] private float sightMaxDistance = 10f;
    [SerializeField] private float chaseSpeed = 5f;


    private float timeCounter = 0f;
    private float colXHalf;
    private bool isWait = true;
    private bool isMove = false;
    private bool isChase = false;
    private Vector2 moveDir; //�i�s����
    private string playerTag = "Player";
    private Vector2 areaMin;
    private Vector2 areaMax;

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
        areaMin = new Vector2(area.bounds.min.x, area.bounds.min.y);
        areaMax = new Vector2(area.bounds.max.x, area.bounds.max.y);
    }

    private void FixedUpdate()
    {

        
        if (isWait) Wait();
        else if (isMove) MoveAround();
        else if (isChase) Chase();

        //Debug.Log(IsVisible());
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
            moveDir = RandomDirection();
            isWait = false;
            isMove = true;
            timeCounter = 0f;
        }

        if (IsVisible())
        {
            isChase = true;
            isWait = false;
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
        switch (Random.Range(0, 4))
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

    //���񏈗�
    private void MoveAround()
    {
        if ((rb.position.x < areaMin.x + colXHalf) || (rb.position.x > areaMax.x - colXHalf) || (rb.position.y < areaMin.y + colXHalf) || (rb.position.y > areaMax.y - colXHalf))
            Turn();

        if (moveTime > timeCounter)
        {
            timeCounter += Time.deltaTime;
            Move(moveDir, moveSpeed);
        }
        else
        {
            isMove = false;
            isWait = true;
            timeCounter = 0f;
        }

        if (IsVisible())
        {
            isChase = true;
            isMove = false;
            timeCounter = 0f;
        }
    }

    //�ړ�����
    private void Move(Vector2 dir, float speed)
    {
        rb.velocity = dir * speed;
        Animate();
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

    private bool IsVisible()
    {
        Vector2 selfPos = rb.position;
        Vector2 targetPos = target.position;

        Vector2 targetDir = targetPos - selfPos; //�^�[�Q�b�g�ւ̌���
        float targetDistance = targetDir.magnitude; //�^�[�Q�b�g�܂ł̋���
        float thetaHalf = sightAngle / 2 * Mathf.Deg2Rad;
        float cosHalf = Mathf.Cos(thetaHalf); //cos(��/2)

        float innerProduct = Vector2.Dot(moveDir, targetDir.normalized); //���g�̌����ƃ^�[�Q�b�g�ւ̌����̓��όv�Z

        return innerProduct > cosHalf && targetDistance < sightMaxDistance;
    }

    private void Chase()
    {
        if (IsVisible())
        {
            Move((target.position - rb.position).normalized, chaseSpeed);
        }
        else
        {
            isChase = false;
            isWait = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == playerTag)
        {
            collision.gameObject.SetActive(false);
            isChase = false;
            rb.velocity = Vector2.zero;
        }
    }
}
