using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nesy_NewPlayer : MonoBehaviour
{
    private Vector2 playerdirection;//���g�̌������擾
    private Rigidbody2D rb2d;//�����̃��W�b�h�{�f�B���擾
    [SerializeField, Tooltip("�ړ��X�s�[�h")]
    private int speed;//�����̈ړ��X�s�[�h���擾����

    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Directer();
        Move();
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
