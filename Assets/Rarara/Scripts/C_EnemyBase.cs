using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_EnemyBase : MonoBehaviour
{
    [SerializeField, Tooltip("���񎞂̑��x")]              private float moveSpeed = 3f;
    [SerializeField, Tooltip("�ǐՑ��x")]                  protected float chaseSpeed = 4f;
    [SerializeField, Tooltip("�ړ�����")]                  private float moveTime = 2f;
    [SerializeField, Tooltip("�ҋ@����")]                  private float waitTime = 2f;
    [SerializeField, Tooltip("������̑ҋ@����")]          private float beforeChaseTime = 0.3f;
    [SerializeField, Tooltip("����������̑ҋ@����")]      private float afterChaseTime = 0.3f;
    [SerializeField, Range(0f, 360f), Tooltip("����p")]   private float sightAngle = 160f;
    [SerializeField, Tooltip("����̍ő勗��")]            private float sightMaxDistance = 10f;
    [SerializeField, Tooltip("���񂵂Ȃ��Ȃ�true")]        private bool notPatrol;


    private float colXHalf; //collision�̕��̔���
    private float colYHalf;
    private float timeCounter = 0f; 
    protected bool isWait = true; //�ҋ@���Ȃ�true
    protected bool isPatrol = false; //���񒆂Ȃ�true
    protected bool isChase = false; //�ǐՒ��Ȃ�true
    protected Vector2 moveDir; //�i�s����
    private Vector2 areaMin; //�ړ��\�͈�
    private Vector2 areaMax;
    private string playerTag = "Player"; //�v���C���[�^�O
    protected float toTargetDistance;

    protected Rigidbody2D rb;
    protected Rigidbody2D target;
    private BoxCollider2D col;
    private Animator anim;
    [SerializeField, Tooltip("�������ɕ\������e�L�X�g")] private GameObject findedText;
    [SerializeField, Tooltip("�����������ɕ\������e�L�X�g")] private GameObject missedText;

    private enum InitDir
    {
        up,
        down,
        left,
        right,
    }

    [SerializeField, Tooltip("�ŏ��Ɍ�������")] private InitDir initDir;

    Dictionary<string, Vector2> udlr = new Dictionary<string, Vector2>() 
    {
        {"up", Vector2.up},
        {"down", Vector2.down},
        {"left", Vector2.left},
        {"right", Vector2.right}
    };

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag(playerTag).GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        moveDir = udlr[initDir.ToString()];
        Animate();
        colXHalf = col.size.x / 2;
        colYHalf = col.size.y / 2;
        areaMin = new Vector2(C_StageManager.instance.area.bounds.min.x, C_StageManager.instance.area.bounds.min.y);
        areaMax = new Vector2(C_StageManager.instance.area.bounds.max.x, C_StageManager.instance.area.bounds.max.y);
    }

    protected virtual void FixedUpdate()
    {
        if (isWait) Wait();
        else if (isPatrol) Patrol();
        else if (isChase) Chase();

        LimitMove();
    }

    /// <summary>
    /// �ҋ@����
    /// </summary>
    protected void Wait()
    {
        if (!notPatrol)
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
                isPatrol = true;
                timeCounter = 0f;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (IsVisible())
        {
            isWait = false;
            timeCounter = 0f;
            StartCoroutine(Finded());
        }
    }

    /// <summary>
    /// �A�j���[�V��������
    /// </summary>
    protected void Animate()
    {
        if (Mathf.Abs(moveDir.x) <= Mathf.Abs(moveDir.y))
        {
            if(moveDir.y >= 0)
            {
                anim.SetFloat("X", 0);
                anim.SetFloat("Y", 1f);
            }
            else
            {
                anim.SetFloat("X", 0);
                anim.SetFloat("Y", -1f);
            }
        }
        else
        {
            if (moveDir.x >= 0)
            {
                anim.SetFloat("X", 1f);
                anim.SetFloat("Y", 0);
            }
            else
            {
                anim.SetFloat("X", -1f);
                anim.SetFloat("Y", 0);
            }
        }
    }

    /// <summary>
    /// �����_���ɏ㉺���E�̕����𐶐�
    /// </summary>
    /// <returns>�㉺���E�̒P�ʃx�N�g��</returns>
    protected Vector2 RandomDirection()
    {
        //return new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)).normalized;
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

    /// <summary>
    /// ���񏈗�
    /// </summary>
    protected void Patrol()
    {
        if ((rb.position.x < areaMin.x + colXHalf) || (rb.position.x > areaMax.x - colXHalf) 
            || (rb.position.y < areaMin.y + colYHalf) || (rb.position.y > areaMax.y - colYHalf))
            Turn();

        if (moveTime > timeCounter)
        {
            Move(moveDir, moveSpeed);
            if (IsVisible())
            {
                isPatrol = false;
                timeCounter = 0f;
                rb.velocity = Vector2.zero;
                StartCoroutine(Finded());
            }
            else
            {
                timeCounter += Time.deltaTime;
            }
        }
        else
        {
            isPatrol = false;
            isWait = true;
            timeCounter = 0f;
        }

        
    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    /// <param name="dir">�ړ�����</param>
    /// <param name="speed">�ړ����x</param>
    protected void Move(Vector2 dir, float speed)
    {
        rb.velocity = dir * speed;
        Animate();
    }

    /// <summary>
    /// �^�[������
    /// </summary>
    protected void Turn()
    {
        moveDir = -moveDir;
    }

    /// <summary>
    /// ���씻��
    /// </summary>
    /// <returns></returns>
    protected bool IsVisible()
    {
        Vector2 selfPos = rb.position;
        Vector2 targetPos = target.position;

        Vector2 targetDir = targetPos - selfPos; //�^�[�Q�b�g�ւ̌���
        toTargetDistance = targetDir.magnitude; //�^�[�Q�b�g�܂ł̋���
        float thetaHalf = sightAngle / 2 * Mathf.Deg2Rad;
        float cosHalf = Mathf.Cos(thetaHalf); //cos(��/2)

        float innerProduct = Vector2.Dot(moveDir, targetDir.normalized); //���g�̌����ƃ^�[�Q�b�g�ւ̌����̓��όv�Z(cos)
        //Debug.Log($"{cosHalf} {innerProduct}");

        return innerProduct > cosHalf && toTargetDistance < sightMaxDistance && !C_GManager.instance.isHide && !C_GManager.instance.isGameClear && !C_GManager.instance.isGameOver;
    }

    /// <summary>
    /// �ǐՏ���
    /// </summary>
    protected virtual void Chase()
    {
        if (IsVisible())
        {
            moveDir = (target.position - rb.position).normalized;
            Move(moveDir, chaseSpeed);
        }
        else
        {
            isChase = false;
            rb.velocity = Vector2.zero;
            StartCoroutine(Missed());
        }
    }

    /// <summary>
    /// �ړ��͈͐���
    /// </summary>
    protected void LimitMove()
    {
        Vector2 currentPos = rb.position;

        currentPos.x = Mathf.Clamp(currentPos.x, areaMin.x + colXHalf, areaMax.x - colXHalf);
        currentPos.y = Mathf.Clamp(currentPos.y, areaMin.y + colYHalf, areaMax.y - colYHalf);

        rb.position = currentPos;
    }


    /// <summary>
    /// �Փˏ���
    /// </summary>
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == playerTag)
        {
            if (!C_GManager.instance.isGameClear)
            {
                isChase = false;
                isWait = true;
            }
            else
                Turn();
        }
    }

    /// <summary>
    /// ������Ɉ�莞�ԑҋ@
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Finded()
    {
        findedText.SetActive(true);
        yield return new WaitForSeconds(beforeChaseTime);
        findedText.SetActive(false);
        isChase = true;
    }

    /// <summary>
    /// ����������Ɉ�莞�ԑҋ@
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Missed()
    {
        missedText.SetActive(true);
        yield return new WaitForSeconds(afterChaseTime);
        missedText.SetActive(false);
        isWait = true;
    }
}
