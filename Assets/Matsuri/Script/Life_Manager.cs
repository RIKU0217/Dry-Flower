using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life_Manager : MonoBehaviour
{
    // プレイヤーのステータスを参照
   // private State_Player _State;

    // 自身の子オブジェクト(ハート)の配列
    private Life_Children[] _Hearts;

    // ライフ比較用に保持するための変数
    private int _beforeLife;

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーステートマネージャースクリプトを取得
     //   _State = GameObject.FindGameObjectsWithTag("State_Manager")[0].GetComponent<State_Player>();

        // 比較用に現在ライフを保持しておく
     //   _beforeLife = _State.getPlayerStatus(0);

        // ライフ分の配列初期化
        _Hearts = new Life_Children[5];

        // 子オブジェクトのスクリプトを取得
        _getChildObjScripts();
    }
    // 子オブジェクトを取得して格納
    private void _getChildObjScripts()
    {
        // ループ用変数
        int i = 0;
        foreach (Transform child in transform)
        {
            _Hearts[i] = child.GetComponent<Life_Children>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
