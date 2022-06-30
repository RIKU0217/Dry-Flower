using UnityEngine;


public class C_StageManager : MonoBehaviour
{
    public static C_StageManager instance = null;

    [SerializeField] public BoxCollider2D area;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Update()
    {
        if (C_GManager.instance.isGoal)
        {
            Debug.Log("Goal");
            C_GManager.instance.isGoal = false;
        }
    }
}
