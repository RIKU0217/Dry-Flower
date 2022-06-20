using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgamePlayer : MonoBehaviour
{
    public Vector2 playerdirection;
    private byte dir;
    private bool playPermission = true;

    public GameObject attackPoint;
    public float speed;

    private bool isCalledOnce = false;
    private GameObject at;

    void Start()
    {

    }

    void Update()
    {
        if (playPermission == true)
        {
            Move();
        }
        MoveClamp();
        Directer();
        Attack();
    }

    void Attack()
    {
        float[] attackX = { 0, 0, -0.8f, 0.8f };
        float[] attackY = { -0.8f, 0.8f, 0, 0 };
        float[] attackR = { 0, 0, 90f, 90f };

        float count = 0;
        count = Mathf.Clamp(count, 0, 1f);

        if (Input.GetKey(KeyCode.Z) & isCalledOnce == false)
        {
            at = Instantiate(attackPoint, new Vector3(transform.position.x + attackX[dir], transform.position.y + attackY[dir], 0),
                                                                Quaternion.Euler(0, 0, attackR[dir]), this.transform);
            isCalledOnce = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            playPermission = false;
            Invoke("DestroyAttackPoint", 0.5f);
            Invoke("CoolTimeReset", 1f);
        }
    }
    void DestroyAttackPoint()
    {
        Destroy(at);
        playPermission = true;
    }
    void CoolTimeReset()
    {
        isCalledOnce = false;
    }

    void Move()
    {
        GetComponent<Rigidbody2D>().velocity = playerdirection * speed;
    }

    void MoveClamp()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8.4f, 8.4f), Mathf.Clamp(transform.position.y, -4.5f, 4.5f), 0);
    }

    void Directer()
    {
        //キーボードからの入力を格納
        playerdirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (playerdirection.x == 0 && playerdirection.y == -1)
        {
            dir = 0; //front
        }
        if (playerdirection.x == 0 && playerdirection.y == 1)
        {
            dir = 1; //back
        }
        if (playerdirection.x == -1 && playerdirection.y == 0)
        {
            dir = 2;//left
        }
        if (playerdirection.x == 1 && playerdirection.y == 0)
        {
            dir = 3; //right
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}