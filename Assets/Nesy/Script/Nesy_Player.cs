using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nesy_Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Vector2 playerdirection;//�����̌������擾����

    public Vector2 thistransform;

    [SerializeField, Tooltip("�ړ��X�s�[�h")]
    private int speed;//�����̈ړ��X�s�[�h���擾����


    public Rigidbody2D rb2d;//�����̃��W�b�h�{�f�B���擾����

    [SerializeField, Tooltip("Player-animation")]
    private Animator anim;

    [SerializeField, Tooltip("�L�[�{�[�h���͂̃I���I�t")]
    public bool onoff;


    void Start()
    {
        //playerdirection.x = 0;
        //playerdirection.y = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (onoff == true)
        {
            Move();
            Animation();
        }

        Directer();
        

    }

    
    public void Animation()
    {
        if (playerdirection.x == 0 && playerdirection.y == -1)
        {
            anim.SetFloat("X", 0);
            anim.SetFloat("Y", -1f);
        }
        if (playerdirection.x == 0 && playerdirection.y == 1)
        {
            anim.SetFloat("X", 0);
            anim.SetFloat("Y", 1f);//back
        }
        if (playerdirection.x == -1 && playerdirection.y == 0)
        {
            anim.SetFloat("X", -1f);
            anim.SetFloat("Y", 0);//left
        }
        if (playerdirection.x == 1 && playerdirection.y == 0)
        {
            anim.SetFloat("X", 1f);
            anim.SetFloat("Y", 0);//right
        }
    }

    public void HokouFront()
    {
        anim.SetFloat("X", 0);
        anim.SetFloat("Y", -1f);
    }
    public void HokouBack()
    {
        anim.SetFloat("X", 0);
        anim.SetFloat("Y", 1f);
        Debug.Log("jdej");
    }
    public void HokouLeft()
    {
        anim.SetFloat("X", -1f);
        anim.SetFloat("Y", 0);
    }
    public void HokouRight()
    {
        anim.SetFloat("X", 1f);
        anim.SetFloat("Y", 0);
    }

    [SerializeField]
    private float animspeed;
    public void StartAnim()
    {
        anim.enabled = true;
        Debug.Log(anim);
    }
    public void Stopanim()
    {
        anim.enabled = false;
    }
    public void FrontAnim()
    {
        spriteRenderer.sprite = sprites[0];
    }
    public void BackAnim()
    {
        this.spriteRenderer.sprite = sprites[1];//back
        Debug.Log(sprites);
    }
    public void RightAnim()
    {
        spriteRenderer.sprite = sprites[2];//right
    }
    public void LeftAnim()
    {
        spriteRenderer.sprite = sprites[3];//left
    }

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    private Vector2 transposi;

    public void Trans()
    {
        this.transform.position = transposi;
    }
        
    




    public void Directer()
    {
        //�L�[�{�[�h����̓��͂��i�[
        playerdirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
    public void Move()
    {
        //���W�b�h�{�f�B�ɗ͉����邱�ƂŃL�����𓮂���
        rb2d.velocity = playerdirection * speed;
    }

    




}
