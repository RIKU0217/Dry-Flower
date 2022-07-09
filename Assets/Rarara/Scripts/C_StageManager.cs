using UnityEngine;


public class C_StageManager : MonoBehaviour
{
    public static C_StageManager instance = null;

    [Tooltip("ˆÚ“®‰Â”\”ÍˆÍ")] public BoxCollider2D area;

    private void Awake()
    {
        instance = this;
    }
}
