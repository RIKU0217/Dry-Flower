using UnityEngine;

public class Damage_Player : MonoBehaviour
{
    // プレイヤーステートスクリプトの取得
    private State_Player _State;
    // Start is called before the first frame update
    void Start()
    {
        _State = GameObject.FindGameObjectsWithTag("State_Manager")[0].GetComponent<State_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "DamageTrap":
                Debug.Log("おはようダメージ！");
                break;
        }
    }
}
