using UnityEngine;


public class C_StageManager : MonoBehaviour
{
    public static C_StageManager instance = null;

    [Tooltip("�ړ��\�͈�")] public BoxCollider2D area;

    private void Awake()
    {
        instance = this;
    }
}
