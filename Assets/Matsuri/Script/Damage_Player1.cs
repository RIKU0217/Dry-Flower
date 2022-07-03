using UnityEngine;

public class Damage_Player1 : MonoBehaviour
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
                _State.setPlayerStatus(_State.getPlayerStatus(0) - 1, 0);
                Debug.Log("HP残り");
                Debug.Log(_State.getPlayerStatus(0));
                break;
        }
    }
}
