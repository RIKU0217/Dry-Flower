using UnityEngine;
using System.Collections;

public class C_EnemyShootController : C_EnemyBase
{
    [SerializeField, Tooltip("弾")]                         private GameObject bulletOb;
    [SerializeField, Tooltip("弾の速度")]                   private float bulletSpeed = 3f;
    [SerializeField, Tooltip("撃ち始める距離")]             private float startShootDis = 4f;
    [SerializeField, Tooltip("撃つまでの待機時間")]         private float beforeShootTime = 1f;
    [SerializeField, Tooltip("撃ち終わった後の待機時間")]   private float afterShootTime = 1f;
    [SerializeField, Tooltip("弾の数")]                     private int bulletNumbers = 1;
    [SerializeField, Tooltip("弾の寿命")]                   private float bulletLifeTime = 5f;
    [SerializeField, Tooltip("弾の間隔")]                   private float bulletDuration = 0.2f;

    private bool isShoot = false;
    private float shootTimer = 0f;
    private float bulletTimer = 0f;
    private int bulNum;

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isShoot)Shoot();
    }

    protected override void Chase()
    {
        if (IsVisible())
        {
            if(toTargetDistance >= startShootDis)
            {
                moveDir = (target.position - rb.position).normalized;
                Move(moveDir, chaseSpeed);
            }
            else
            {
                moveDir = (target.position - rb.position).normalized;
                Animate();
                bulNum = bulletNumbers;
                isChase = false;
                isShoot = true;
            }
        }
        else
        {
            isChase = false;
            rb.velocity = Vector2.zero;
            StartCoroutine(Missed());
        }
    }

    private void Shoot()
    {
        rb.velocity = Vector2.zero;
        moveDir = (target.position - rb.position).normalized;
        Animate();
        shootTimer += Time.deltaTime;
        if(shootTimer > beforeShootTime + afterShootTime || !IsVisible())
        {
            isShoot = false;
            isChase = true;
            shootTimer = 0f;
            bulletTimer = 0f;
        }
        else if(shootTimer > beforeShootTime )
        {
            if(bulNum != 0 && bulletTimer > bulletDuration)
            {
                GameObject bul = Instantiate(bulletOb, this.transform.position + new Vector3(moveDir.x,moveDir.y,0), Quaternion.Euler(0, 0, Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg + 90f));
                bul.GetComponent<C_EnemyBullet>().lifeTime = bulletLifeTime;
                bul.GetComponent<Rigidbody2D>().velocity = moveDir * bulletSpeed;
                bulNum--;
                bulletTimer = 0f;
            }
            bulletTimer += Time.deltaTime;
        }
    }
}
