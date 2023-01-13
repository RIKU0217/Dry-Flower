using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D g;
    private Vector2 dir;
    private float speed = 3f;
    private int HP = 3;
    private Image HP3_SR;
    private GameObject HP1_SR;
    private GameObject HP2_SR;
    private GameObject Image_HP;
    

    //プレファブをスクリプトで生成
    void Start()
    {
        //対象プレファブのパス取得
        string adress1 = "Assets/Prefab/HP1_k.prefab";
        string adress2 = "Assets/Prefab/HP2_k.prefab";
        //対象プレファブをプレファブとして取得
        HP1_SR = AssetDatabase.LoadAssetAtPath<GameObject>(adress1);
        HP2_SR = AssetDatabase.LoadAssetAtPath<GameObject>(adress2);
        //Image-HPを取得
        Image_HP = GameObject.Find("Image-HP");
        //オブジェクトのImageを取得
        HP3_SR = Image_HP.GetComponent<Image>();
        Debug.Log(HP3_SR == null);
        Debug.Log(HP2_SR == null);
        Debug.Log(HP1_SR == null);
    }


    void Update()
    {
        director();
        Move();
        if (HP == 2)
        {
            HP3_SR.sprite = HP2_SR.GetComponent<SpriteRenderer>().sprite;
        } else if (HP == 1)
        {
            HP3_SR.sprite = HP1_SR.GetComponent<SpriteRenderer>().sprite;
        } else if (HP == 0) {
            Destroy(this.gameObject);
        }
    }

    //敵と当たったときの判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("衝突");
        if (collision.gameObject.CompareTag("Enemy") && HP > 0)
        {
            HP--;
            Debug.Log(HP);
        }
    }

    private void director()
    {
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
    private void Move()
    {
        g.velocity = dir * speed;
    }
}
