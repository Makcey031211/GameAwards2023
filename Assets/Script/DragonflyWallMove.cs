using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonflyWallMove : MonoBehaviour
{
    [Header("�g���{�ԉ΃X�N���v�g"), SerializeField]
    FireworksModule module;

    void Start()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        //--- ��X�������܂� by�q��
        ////- �V�����x�N�g������
        //Vector2 newdir = new Vector2(0,0);
        //Vector3 direction = new Vector3(module.movedir.x,module.movedir.y,0.0f);
        ////- �u�v���C���[�u���b�J�[�̕����v�Ɓu�g���{�̌����v�̊p�x�����߂�
        //Debug.Log(angle);
    }
}