using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float waitCounter = 0f;
    private float moveCounter = 0f;
    private bool isWait = true;
    private bool isMove = false;
    private Vector2 moveDir;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkTime;
    [SerializeField] private float waitTime;
    [SerializeField] private BoxCollider2D moveArea;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveDir = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f));
        moveDir.Normalize();
        rb.velocity = moveDir * moveSpeed;
        Anim(moveDir);
    }

    void Wait()
    {
        if (waitTime > waitCounter)
        {
            waitCounter += Time.deltaTime;
            rb.velocity = Vector2.zero;
        }
        else
        {
            isWait = false;
            waitCounter = 0f;
        }
    }

    public void Anim(Vector2 dir)
    {
        if (dir.x == 0 && dir.y == -1)
        {
            anim.SetFloat("X", 0);
            anim.SetFloat("Y", -1f);
        }
        if (dir.x == 0 && dir.y == 1)
        {
            anim.SetFloat("X", 0);
            anim.SetFloat("Y", 1f);
        }
        if (dir.x == -1 && dir.y == 0)
        {
            anim.SetFloat("X", -1f);
            anim.SetFloat("Y", 0);
        }
        if (dir.x == 1 && dir.y == 0)
        {
            anim.SetFloat("X", 1f);
            anim.SetFloat("Y", 0);
        }
    }
}
