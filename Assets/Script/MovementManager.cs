using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �����̃}�l�[�W���[
 */
public class MovementManager : MonoBehaviour
{
    public enum MovementType
    {
        CicrleBehaviour, // �~����
    }

    public enum Direction
    {
        Clockwise,        // �����v����
        CounterClockwise, // ���v���
    }

    //* ���ʊ֘A *//
    //- �C���X�y�N�^�[�ɕ\��
    [SerializeField, Header("�����̎��")]
    public MovementType _type = MovementType.CicrleBehaviour;
    //- �C���X�y�N�^�[�����\��
    FireworksModule fireworks;
    public MovementType Type => _type;

    //* �~�����֘A *//
    //- �C���X�y�N�^�[�ɕ\��
    [SerializeField, HideInInspector]
    public Direction _direction = Direction.Clockwise; // ��]����
    [SerializeField, HideInInspector]
    public Vector3 _center = Vector3.zero; // ���S�_
    [SerializeField, HideInInspector]
    public Vector3 _axis = Vector3.forward; // ��]��
    [SerializeField, HideInInspector]
    public float _radius = 1.0f; // ���a�̑傫��
    [SerializeField, HideInInspector]
    public float _periodTime = 2.0f; // ������̂ɂ����鎞��(�b)
    [SerializeField, HideInInspector]
    public bool _updateRotation = false; // �������X�V���邩�ǂ���
    //- �C���X�y�N�^�[�ɔ�\��
    private float currentTime;  // ���݂̎���
    private float currentAngle; // ���݂̉�]�p�x
    private float angle = 360f; // ������̊p�x
    //- �O������̒l�擾�p
    public Direction Directionary => _direction;
    public Vector3 Center => _center;
    public Vector3 Axis => _axis;
    public float Radius => _radius;
    public float PeriodTime => _periodTime;
    public bool UpdateRotation => _updateRotation;

    void Start()
    {
        fireworks = this.gameObject.GetComponent<FireworksModule>();
    }

    void Update()
    {
        switch (Type)
        {
            //- �I������^�C�v�ɉ����ď����𕪊�
            case MovementType.CicrleBehaviour:
                CicrleMove();
                break;
        }
    }

    /// <summary>
    /// �~�^��
    /// </summary>
    private void CicrleMove()
    {
        if (!fireworks.IsExploded)
        {
            var trans = transform;

            //- ��]�̃N�H�[�^�j�I���쐬
            var angleAxis = Quaternion.AngleAxis(currentAngle, Axis);

            //- ���a�ɑΉ�����x�N�g�����쐬���A��]���ɉ����ĉ�]������
            var radiusVec = angleAxis * (Vector3.up * Radius);

            //- ���S�_�ɔ��a�ɑΉ�����x�N�g�������Z���Ĉʒu���v�Z����
            var pos = Center + radiusVec;

            //- �ʒu���X�V����
            trans.position = pos;

            //- �������X�V����
            if (UpdateRotation)
            {
                trans.rotation = Quaternion.LookRotation(Center - pos, Vector3.up);
            }

            //- ���݂̉�]�p�x���X�V����
            currentTime += Time.deltaTime;

            //- ��]�����ɉ����ď����𕪊�
            switch (Directionary)
            {
                case Direction.Clockwise:
                    currentAngle = (currentTime % PeriodTime) / PeriodTime * angle;
                    break;
                case Direction.CounterClockwise:
                    currentAngle = angle - ((currentTime % PeriodTime) / PeriodTime * angle);
                    break;
            }
        }
    }
}
