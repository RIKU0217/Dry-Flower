using UnityEngine;

public class C_PlayerController : MonoBehaviour
{
    [SerializeField] private int speed;//�ړ��X�s�[�h
    [SerializeField, Tooltip("�L�[�{�[�h���͂̃I���I�t")] public bool enableKeyboard;

    private Vector2 playerdirection; //�ړ�����

    private string hideTag = "Bush";
    private string goalTag = "Goal";
    private string enemyTag = "Enemy";

    private BoxCollider2D area; //�ړ��\�͈�
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        area = C_StageManager.instance.area;
    }

    void FixedUpdate()
    {
        Directer();
        Move();
        if (playerdirection != Vector2.zero) Animation();
        LimitMove();
    }

    private void Directer()
    {
        if (enableKeyboard)
            //�L�[�{�[�h����̓��͂��i�[
            playerdirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        else
            playerdirection = Vector2.zero;
    }

    private void Move()
    {
        //���W�b�h�{�f�B�ɗ͉����邱�ƂŃL�����𓮂���
        rb.velocity = playerdirection * speed;
    }

    private void Animation()
    {
        if (Mathf.Abs(playerdirection.x) <= Mathf.Abs(playerdirection.y))
        {
            if (playerdirection.y >= 0)
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
            if (playerdirection.x >= 0)
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

    private void LimitMove()
    {
        Vector2 currentPos = rb.position;

        currentPos.x = Mathf.Clamp(currentPos.x, area.bounds.min.x + col.size.x / 2, area.bounds.max.x - col.size.x / 2);
        currentPos.y = Mathf.Clamp(currentPos.y, area.bounds.min.y + col.size.y / 2, area.bounds.max.y - col.size.y / 2);

        rb.position = currentPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == goalTag)
        {
            C_GManager.instance.isGameClear = true;
            enableKeyboard = false;
        }
        else if (collision.gameObject.tag == hideTag)
        {
            C_GManager.instance.isHide = true;
            C_StageManager.instance.InHide();
        }
        else if (collision.gameObject.tag == enemyTag)
        {
            if (!C_GManager.instance.isGameClear)
            {
                C_GManager.instance.isGameOver = true;
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == hideTag)
        {
            C_GManager.instance.isHide = false;
            C_StageManager.instance.OutHide();
        }
    }
}
