using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_EnemyBase : MonoBehaviour
{
    [SerializeField, Tooltip("巡回時の速度")]              private float moveSpeed = 3f;
    [SerializeField, Tooltip("追跡速度")]                  protected float chaseSpeed = 4f;
    [SerializeField, Tooltip("移動時間")]                  private float moveTime = 2f;
    [SerializeField, Tooltip("待機時間")]                  private float waitTime = 2f;
    [SerializeField, Tooltip("発見後の待機時間")]          private float beforeChaseTime = 0.3f;
    [SerializeField, Tooltip("見失った後の待機時間")]      private float afterChaseTime = 0.3f;
    [SerializeField, Range(0f, 360f), Tooltip("視野角")]   private float sightAngle = 160f;
    [SerializeField, Tooltip("視野の最大距離")]            private float sightMaxDistance = 10f;
    [SerializeField, Tooltip("巡回しないならtrue")]        private bool notPatrol;


    private float colXHalf; //collisionの幅の半分
    private float colYHalf;
    private float timeCounter = 0f; 
    protected bool isWait = true; //待機中ならtrue
    protected bool isPatrol = false; //巡回中ならtrue
    protected bool isChase = false; //追跡中ならtrue
    protected Vector2 moveDir; //進行方向
    private Vector2 areaMin; //移動可能範囲
    private Vector2 areaMax;
    private string playerTag = "Player"; //プレイヤータグ
    protected float toTargetDistance;

    protected Rigidbody2D rb;
    protected Rigidbody2D target;
    private BoxCollider2D col;
    private Animator anim;
    [SerializeField, Tooltip("発見時に表示するテキスト")] private GameObject findedText;
    [SerializeField, Tooltip("見失った時に表示するテキスト")] private GameObject missedText;

    private enum InitDir
    {
        up,
        down,
        left,
        right,
    }

    [SerializeField, Tooltip("最初に向く方向")] private InitDir initDir;

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
    /// 待機処理
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
    /// アニメーション推移
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
    /// ランダムに上下左右の方向を生成
    /// </summary>
    /// <returns>上下左右の単位ベクトル</returns>
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
    /// 巡回処理
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
    /// 移動処理
    /// </summary>
    /// <param name="dir">移動方向</param>
    /// <param name="speed">移動速度</param>
    protected void Move(Vector2 dir, float speed)
    {
        rb.velocity = dir * speed;
        Animate();
    }

    /// <summary>
    /// ターン処理
    /// </summary>
    protected void Turn()
    {
        moveDir = -moveDir;
    }

    /// <summary>
    /// 視野判定
    /// </summary>
    /// <returns></returns>
    protected bool IsVisible()
    {
        Vector2 selfPos = rb.position;
        Vector2 targetPos = target.position;

        Vector2 targetDir = targetPos - selfPos; //ターゲットへの向き
        toTargetDistance = targetDir.magnitude; //ターゲットまでの距離
        float thetaHalf = sightAngle / 2 * Mathf.Deg2Rad;
        float cosHalf = Mathf.Cos(thetaHalf); //cos(θ/2)

        float innerProduct = Vector2.Dot(moveDir, targetDir.normalized); //自身の向きとターゲットへの向きの内積計算(cos)
        //Debug.Log($"{cosHalf} {innerProduct}");

        return innerProduct > cosHalf && toTargetDistance < sightMaxDistance && !C_GManager.instance.isHide && !C_GManager.instance.isGameClear && !C_GManager.instance.isGameOver;
    }

    /// <summary>
    /// 追跡処理
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
    /// 移動範囲制限
    /// </summary>
    protected void LimitMove()
    {
        Vector2 currentPos = rb.position;

        currentPos.x = Mathf.Clamp(currentPos.x, areaMin.x + colXHalf, areaMax.x - colXHalf);
        currentPos.y = Mathf.Clamp(currentPos.y, areaMin.y + colYHalf, areaMax.y - colYHalf);

        rb.position = currentPos;
    }


    /// <summary>
    /// 衝突処理
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
    /// 発見後に一定時間待機
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
    /// 見失った後に一定時間待機
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
