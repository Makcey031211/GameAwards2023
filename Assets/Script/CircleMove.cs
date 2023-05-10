using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �~�^��
/// </summary>
public class CircleMove : MonoBehaviour
{
    private enum Direction
    {
        Clockwise,         // �����v����
        CounterClockwise,  // ���v����
    }

    [SerializeField, Header("��]����")]
    private Direction direction = Direction.Clockwise;

    [SerializeField, Header("���S�_")]
    private Vector3 Center = Vector3.zero;

    [SerializeField, Header("��]��")]
    private Vector3 Axis = Vector3.forward;

    [SerializeField, Header("���a�̑傫��")]
    private float Radius = 1.0f;

    [SerializeField, Header("�J�n���ɂ��炷����(�b)")]
    private float StartTime = 0.0f;

    [SerializeField, Header("������̂ɂ����鎞��(�b)")]
    private float PeriodTime = 2.0f;

    [SerializeField, Header("�������X�V���邩�ǂ���")]
    private bool updateRotation = false;

    //- ���݂̎���
    private float currentTime;

    //- �p�x
    float angle = 360f;

    //- ���݂̉�]�p�x
    float currentAngle;

    //- �ԉΓ_�΃X�N���v�g
    FireworksModule fireworks;

    private void Start()
    {
        fireworks = this.gameObject.GetComponent<FireworksModule>();
        //- �J�n���Ɏ��Ԃ����炷
        currentTime += StartTime;
    }

    private void Update()
    {
        if (!fireworks.IsExploded)
        {
            //- ��]�����ɉ����ď����𕪊�
            switch (direction)
            {
                case Direction.Clockwise:
                    currentAngle = (currentTime % PeriodTime) / PeriodTime * angle;
                    break;
                case Direction.CounterClockwise:
                    currentAngle = angle - ((currentTime % PeriodTime) / PeriodTime * angle);
                    break;
            }

            var trans = transform;

            //- ��]�̃N�H�[�^�j�I���쐬
            var angleAxis = Quaternion.AngleAxis(currentAngle, Axis);

            //- ���a�ɑΉ�����x�N�g�����쐬���A��]���ɉ����ĉ�]������
            var radiusVec = angleAxis * (Vector3.down * Radius);

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
            currentTime += Time.deltaTime;
        }
    }
}