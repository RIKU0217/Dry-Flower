using UnityEngine;

public class C_EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f; //�ړ����x
    [SerializeField] private float moveTime = 2f; //�ړ�����
    [SerializeField] private float waitTime = 2f; //�ҋ@����
    [SerializeField] private Rigidbody2D target;
    [SerializeField, Range(0f, 360f)] private float sightAngle = 160f;
    [SerializeField] private float sightMaxDistance = 10f;
    [SerializeField] private float chaseSpeed = 5f;


    private float colXHalf;
    private float colYHalf;
    private float timeCounter = 0f;
    private bool isWait = true;
    private bool isMove = false;
    private bool isChase = false;
    private Vector2 moveDir; //�i�s����
    private Vector2 areaMin;
    private Vector2 areaMax;
    private string playerTag = "Player";

    [HideInInspector] public Rigidbody2D rb;
    private BoxCollider2D col;
    private Animator anim;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        moveDir = Vector2.down;
        colXHalf = col.size.x / 2;
        colYHalf = col.size.y / 2;
        areaMin = new Vector2(C_StageManager.instance.area.bounds.min.x, C_StageManager.instance.area.bounds.min.y);
        areaMax = new Vector2(C_StageManager.instance.area.bounds.max.x, C_StageManager.instance.area.bounds.max.y);
    }

    private void FixedUpdate()
    {
        if (isWait) Wait();
        else if (isMove) MoveAround();
        else if (isChase) Chase();

        LimitMove();
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

        if (IsVisible() && !C_GManager.instance.isGameOver)
        {
            isChase = true;
            isWait = false;
            timeCounter = 0f;
        }
    }

    //�A�j���[�V��������
    private void Animate()
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

    //�����_���ɏ㉺���E�̕����𐶐�
    private Vector2 RandomDirection()
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

    //���񏈗�
    private void MoveAround()
    {
        if ((rb.position.x < areaMin.x + colXHalf) || (rb.position.x > areaMax.x - colXHalf) 
            || (rb.position.y < areaMin.y + colYHalf) || (rb.position.y > areaMax.y - colYHalf))
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
            rb.position = new Vector2(rb.position.x, areaMax.y - colYHalf);
        else if (moveDir == Vector2.down)
            rb.position = new Vector2(rb.position.x, areaMin.y + colYHalf);
        else if (moveDir == Vector2.left)
            rb.position = new Vector2(areaMin.x + colXHalf, rb.position.y);
        else if (moveDir == Vector2.right)
            rb.position = new Vector2(areaMax.x - colXHalf, rb.position.y);

        moveDir = -moveDir;
    }

    //���씻��
    private bool IsVisible()
    {
        Vector2 selfPos = rb.position;
        Vector2 targetPos = target.position;

        Vector2 targetDir = targetPos - selfPos; //�^�[�Q�b�g�ւ̌���
        float targetDistance = targetDir.magnitude; //�^�[�Q�b�g�܂ł̋���
        float thetaHalf = sightAngle / 2 * Mathf.Deg2Rad;
        float cosHalf = Mathf.Cos(thetaHalf); //cos(��/2)

        float innerProduct = Vector2.Dot(moveDir, targetDir.normalized); //���g�̌����ƃ^�[�Q�b�g�ւ̌����̓��όv�Z

        return innerProduct > cosHalf && targetDistance < sightMaxDistance && !C_GManager.instance.isHide && !C_GManager.instance.isGameClear && !C_GManager.instance.isGameOver;
    }

    //�ǐՏ���
    private void Chase()
    {
        if (IsVisible())
        {
            moveDir = (target.position - rb.position).normalized;
            Move(moveDir, chaseSpeed);
        }
        else
        {
            isChase = false;
            isWait = true;
        }
    }

    private void LimitMove()
    {
        Vector2 currentPos = rb.position;

        currentPos.x = Mathf.Clamp(currentPos.x, areaMin.x + colXHalf, areaMax.x - colXHalf);
        currentPos.y = Mathf.Clamp(currentPos.y, areaMin.y + colYHalf, areaMax.y - colYHalf);

        rb.position = currentPos;
    }


    //�Փˏ���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == playerTag)
        {
            isChase = false;
            isWait = true;
        }
    }
}
