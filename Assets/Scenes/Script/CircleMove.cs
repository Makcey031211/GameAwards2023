using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �N�H�[�^�j�I���ŉ~�^���̋O�����v�Z
/// </summary>
public class CircleMove : MonoBehaviour
{
    [SerializeField, Header("���S�_")]
    private Vector3 Center = Vector3.zero;

    [SerializeField, Header("��]��")]
    private Vector3 Axis = Vector3.up;

    [SerializeField, Header("������̂ɂ����鎞��(�b)")]
    private float PeriodTime = 2;

    [SerializeField, Header("�������X�V���邩�ǂ���")]
    private bool updateRotation = true;

    float angle = 360;

    // Update is called once per frame
    private void Update()
    {
        var Trans = transform;

        //- ��]�̃N�H�[�^�j�I���쐬
        var angleAxis = Quaternion.AngleAxis(angle / PeriodTime * Time.deltaTime,Axis);

        //- �~�^���̈ʒu�v�Z
        var pos = Trans.position;

        pos -= Center;
        pos = angleAxis * pos;
        pos += Center;

        Trans.position = pos;

        //- �����X�V
        if (updateRotation)
        {
            Trans.rotation = Trans.rotation * angleAxis;
        }
    }
}
