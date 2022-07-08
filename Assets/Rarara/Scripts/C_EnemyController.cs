using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f; //移動速度
    [SerializeField] private float moveTime = 2f; //移動時間
    [SerializeField] private float waitTime = 2f; //待機時間
    [SerializeField] private float beforeChaseTime = 0.3f; //発見後の待機時間
    [SerializeField] private float afterChaseTime = 0.3f; //見失い後の待機時間
    [SerializeField, Range(0f, 360f)] private float sightAngle = 160f;
    [SerializeField] private float sightMaxDistance = 10f;
    [SerializeField] private float chaseSpeed = 3f;
    [SerializeField] private bool notMoveAround;


    private float colXHalf;
    private float colYHalf;
    private float timeCounter = 0f;
    private bool isWait = true;
    private bool isMove = false;
    private bool isChase = false;
    private Vector2 moveDir; //進行方向
    private Vector2 areaMin;
    private Vector2 areaMax;
    private string playerTag = "Player";

    private Rigidbody2D rb;
    private Rigidbody2D target;
    private BoxCollider2D col;
    private Animator anim;
    [SerializeField] private GameObject findedText;
    [SerializeField] private GameObject missText;

    private enum InitDir
    {
        up,
        down,
        left,
        right,
    }

    [SerializeField] private InitDir initDir;

    Dictionary<string, Vector2> udlr = new Dictionary<string, Vector2>() 
    {
        {"up", Vector2.up},
        {"down", Vector2.down},
        {"left", Vector2.left},
        {"right", Vector2.right}
    };

    private void Start()
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

    private void FixedUpdate()
    {
        if (isWait) Wait();
        else if (isMove) MoveAround();
        else if (isChase) Chase();

        LimitMove();
    }

    /// <summary>
    /// 待機処理
    /// </summary>
    private void Wait()
    {
        if (!notMoveAround)
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
    /// アニメーション推移
    /// </summary>
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

    /// <summary>
    /// ランダムに上下左右の方向を生成
    /// </summary>
    /// <returns>上下左右の単位ベクトル</returns>
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

    /// <summary>
    /// 巡回処理
    /// </summary>
    private void MoveAround()
    {
        if ((rb.position.x < areaMin.x + colXHalf) || (rb.position.x > areaMax.x - colXHalf) 
            || (rb.position.y < areaMin.y + colYHalf) || (rb.position.y > areaMax.y - colYHalf))
            Turn();

        if (moveTime > timeCounter)
        {
            Move(moveDir, moveSpeed);
            if (IsVisible())
            {
                isMove = false;
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
            isMove = false;
            isWait = true;
            timeCounter = 0f;
        }

        
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    /// <param name="dir">移動方向</param>
    /// <param name="speed">移動速度</param>
    private void Move(Vector2 dir, float speed)
    {
        rb.velocity = dir * speed;
        Animate();
    }

    /// <summary>
    /// ターン処理
    /// </summary>
    private void Turn()
    {
        //if (moveDir == Vector2.up)
        //    rb.position = new Vector2(rb.position.x, areaMax.y - colYHalf);
        //else if (moveDir == Vector2.down)
        //    rb.position = new Vector2(rb.position.x, areaMin.y + colYHalf);
        //else if (moveDir == Vector2.left)
        //    rb.position = new Vector2(areaMin.x + colXHalf, rb.position.y);
        //else if (moveDir == Vector2.right)
        //    rb.position = new Vector2(areaMax.x - colXHalf, rb.position.y);

        moveDir = -moveDir;
    }

    /// <summary>
    /// 視野判定
    /// </summary>
    /// <returns></returns>
    private bool IsVisible()
    {
        Vector2 selfPos = rb.position;
        Vector2 targetPos = target.position;

        Vector2 targetDir = targetPos - selfPos; //ターゲットへの向き
        float targetDistance = targetDir.magnitude; //ターゲットまでの距離
        float thetaHalf = sightAngle / 2 * Mathf.Deg2Rad;
        float cosHalf = Mathf.Cos(thetaHalf); //cos(θ/2)

        float innerProduct = Vector2.Dot(moveDir, targetDir.normalized); //自身の向きとターゲットへの向きの内積計算

        return innerProduct > cosHalf && targetDistance < sightMaxDistance && !C_GManager.instance.isHide && !C_GManager.instance.isGameClear && !C_GManager.instance.isGameOver;
    }

    /// <summary>
    /// 追跡処理
    /// </summary>
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
            rb.velocity = Vector2.zero;
            StartCoroutine(Miss());
        }
    }

    /// <summary>
    /// 移動範囲制限
    /// </summary>
    private void LimitMove()
    {
        Vector2 currentPos = rb.position;

        currentPos.x = Mathf.Clamp(currentPos.x, areaMin.x + colXHalf, areaMax.x - colXHalf);
        currentPos.y = Mathf.Clamp(currentPos.y, areaMin.y + colYHalf, areaMax.y - colYHalf);

        rb.position = currentPos;
    }


    /// <summary>
    /// 衝突処理
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
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
    /// 発見後に一定時間待機
    /// </summary>
    /// <returns></returns>
    IEnumerator Finded()
    {
        findedText.SetActive(true);
        yield return new WaitForSeconds(beforeChaseTime);
        findedText.SetActive(false);
        isChase = true;
    }

    /// <summary>
    /// 見失った後に一定時間待機
    /// </summary>
    /// <returns></returns>
    IEnumerator Miss()
    {
        missText.SetActive(true);
        yield return new WaitForSeconds(afterChaseTime);
        missText.SetActive(false);
        isWait = true;
    }
}
