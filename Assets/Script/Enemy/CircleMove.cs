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
    private Vector3 Axis = Vector3.forward;

    [SerializeField, Header("���a�̑傫��")]
    private float Radius = 1.0f;

    [SerializeField, Header("������̂ɂ����鎞��(�b)")]
    private float PeriodTime = 2.0f;

    [SerializeField, Header("�������X�V���邩�ǂ���")]
    private bool updateRotation = true;

    //- �p�x
    float angle = 360f;

    //- ���݂̉�]�p�x
    float currentAngle;

    //- �ԉΓ_�΃X�N���v�g
    FireworksModule fireworks;

    //- �����ʒu
    private Vector3 initialPosition;

    private void Start()
    {
        fireworks = this.gameObject.GetComponent<FireworksModule>();
        initialPosition = transform.position; // �����ʒu��ۑ�����
    }

    private void Update()
    {
        if (!fireworks.IsExploded)
        {
            var trans = transform;

            //- ��]�̃N�H�[�^�j�I���쐬
            var angleAxis = Quaternion.AngleAxis(currentAngle, Axis);

            //- ���a�ɑΉ�����x�N�g�����쐬���A��]���ɉ����ĉ�]������
            var radiusVec = angleAxis * (Vector3.right * Radius);

            //- ���S�_�ɔ��a�ɑΉ�����x�N�g�������Z���Ĉʒu���v�Z����
            var pos = Center + radiusVec;

            //- �ʒu���X�V����
            trans.position = pos;

            //- �������X�V����
            if (updateRotation)
            {
                trans.rotation = Quaternion.LookRotation(Center - pos, Vector3.up);
            }

            //- ���݂̉�]�p�x���X�V����
            currentAngle = (Time.time % PeriodTime) / PeriodTime * angle;
        }
    }

    //- �����ʒu�Ɖ�]�p�x�����Z�b�g����
    public void MoveRestart()
    {
        transform.position = initialPosition;
        currentAngle = 0f;
    }
}