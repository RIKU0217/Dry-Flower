using UnityEngine;
using System.Collections;

public class C_EnemyShootController : C_EnemyBase
{
    [SerializeField, Tooltip("’e")]                         private GameObject bulletOb;
    [SerializeField, Tooltip("’e‚Ì‘¬“x")]                   private float bulletSpeed = 3f;
    [SerializeField, Tooltip("Œ‚‚¿Žn‚ß‚é‹——£")]             private float startShootDis = 4f;
    [SerializeField, Tooltip("Œ‚‚Â‚Ü‚Å‚Ì‘Ò‹@ŽžŠÔ")]         private float beforeShootTime = 1f;
    [SerializeField, Tooltip("Œ‚‚¿I‚í‚Á‚½Œã‚Ì‘Ò‹@ŽžŠÔ")]   private float afterShootTime = 1f;
    [SerializeField, Tooltip("’e‚Ì”")]                     private int bulletNumbers = 1;
    [SerializeField, Tooltip("’e‚ÌŽõ–½")]                   private float bulletLifeTime = 5f;
    [SerializeField, Tooltip("’e‚ÌŠÔŠu")]                   private float bulletDuration = 0.2f;

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
        if(shootTimer > beforeShootTime + afterShootTime)
        {
            isShoot = false;
            isChase = true;
            shootTimer = 0f;
            bulletTimer = 0f;
        }
        else if(shootTimer > beforeShootTime)
        {
            if(bulNum != 0 && bulletTimer > bulletDuration)
            {
                GameObject bul = Instantiate(bulletOb, this.transform.position, Quaternion.Euler(0, 0, Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg + 90f));
                bul.GetComponent<C_EnemyBullet>().lifeTime = bulletLifeTime;
                bul.GetComponent<Rigidbody2D>().velocity = moveDir * bulletSpeed;
                bulNum--;
                bulletTimer = 0f;
            }
            bulletTimer += Time.deltaTime;
        }
    }
}
