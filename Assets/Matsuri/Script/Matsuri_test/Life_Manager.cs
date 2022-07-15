using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life_Manager : MonoBehaviour
{
    // �v���C���[�̃X�e�[�^�X���Q��
   // private State_Player _State;

    // ���g�̎q�I�u�W�F�N�g(�n�[�g)�̔z��
    private Life_Children[] _Hearts;

    // ���C�t��r�p�ɕێ����邽�߂̕ϐ�
    private int _beforeLife;

    // Start is called before the first frame update
    void Start()
    {
        // �v���C���[�X�e�[�g�}�l�[�W���[�X�N���v�g���擾
     //   _State = GameObject.FindGameObjectsWithTag("State_Manager")[0].GetComponent<State_Player>();

        // ��r�p�Ɍ��݃��C�t��ێ����Ă���
     //   _beforeLife = _State.getPlayerStatus(0);

        // ���C�t���̔z�񏉊���
        _Hearts = new Life_Children[5];

        // �q�I�u�W�F�N�g�̃X�N���v�g���擾
        _getChildObjScripts();
    }
    // �q�I�u�W�F�N�g���擾���Ċi�[
    private void _getChildObjScripts()
    {
        // ���[�v�p�ϐ�
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
