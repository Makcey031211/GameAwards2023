using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �����̃}�l�[�W���[
 */
public class MovementManager : MonoBehaviour
{
    //--- �񋓑̒�`(�^�C�v)
    public enum E_MovementType
    {
        ThreewayBehaviour,   // �O��������
        ThreepointBehaviour, // �O�_�ԋ���
        CicrleBehaviour,     // �~����
    }

    //--- �񋓑̒�`(��])
    public enum E_RotaDirection
    {
        Clockwise,        // �����v����
        CounterClockwise, // ���v���
    }

    //--- �񋓑̒�`(�ړ�)
    public enum E_MoveDirection
    {
        Horizontal, // ���ړ�
        Vertical,   // �c�ړ�
        Diagonal,   // �΂߈ړ�
    }


    //* ���ʊ֘A *//
    //- �C���X�y�N�^�[�ɕ\��
    [SerializeField, Header("�����̎��")]
    public E_MovementType _type = E_MovementType.ThreewayBehaviour;
    //- �C���X�y�N�^�[�����\��
    FireworksModule fireworks;
    public E_MovementType Type => _type;

    //* �O���������֘A *//
    //- �C���X�y�N�^�[�ɕ\��
    [SerializeField, HideInInspector]
    public E_MoveDirection _moveDirection = E_MoveDirection.Horizontal; // �ړ�����
    [SerializeField, HideInInspector]
    public float _moveDistance = 5.0f; // �ړ�����
    [SerializeField, HideInInspector]
    public float _travelTime   = 1.0f; // �ړ�����
    //- �C���X�y�N�^�[�����\��
    private Vector3 startPosition;   // �J�n�ʒu
    private Vector3 endPosition;     // �I���ʒu
    private float   timeElapsed;     // �o�ߎ���
    private bool    reverse = false; // �ړ��̕����]���p
    //- �O������̒l�擾�p
    public E_MoveDirection MoveDirection => _moveDirection;
    public float MoveDistance => _moveDistance;
    public float TravelTime => _travelTime;

    //* �O�_�ԋ��� *//
    //- �C���X�y�N�^�[�ɕ\��
    [SerializeField, HideInInspector]
    public Vector3 _startPoint;   // �n�_
    [SerializeField, HideInInspector]
    public Vector3 _halfwayPoint; // ���ԓ_
    [SerializeField, HideInInspector]
    public Vector3 _endPoint;     // �I�_
    [SerializeField, HideInInspector]
    public float _moveSpeed   = 1.0f;   // �ړ����x
    [SerializeField, HideInInspector]
    public float _endWaitTime = 1.0f;   // �I�_���B���̑ҋ@����
    //- �C���X�y�N�^�[�����\��
    private Vector3[] points = new Vector3[3];
    private int   currentPoint     = 0;     // ���݂̈ʒu
    private int   currentDirection = 1;     // ���݂̕���
    private float waitingTimer     = 0.0f;  // �ҋ@����
    private bool  isWaiting        = false; // �ҋ@���Ă��邩���Ă��Ȃ���
    //- �O������̒l�擾�p
    public Vector3 StartPoint => _startPoint;
    public Vector3 HalfwayPoint => _halfwayPoint;
    public Vector3 EndPoint => _endPoint;
    public float MoveSpeed => _moveSpeed;
    public float EndWaitTime => _endWaitTime;

    //* �~�����֘A *//
    //- �C���X�y�N�^�[�ɕ\��
    [SerializeField, HideInInspector]
    public E_RotaDirection _rotaDirection = E_RotaDirection.Clockwise; // ��]����
    [SerializeField, HideInInspector]
    public Vector3 _center = Vector3.zero;  // ���S�_
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
    public E_RotaDirection RotaDirection => _rotaDirection;
    public Vector3 Center => _center;
    public Vector3 Axis => _axis;
    public float Radius => _radius;
    public float PeriodTime => _periodTime;
    public bool UpdateRotation => _updateRotation;


    void Start()
    {
        //* ���ʍ��� *//
        fireworks = this.gameObject.GetComponent<FireworksModule>();

        //* �O������������ *//
        startPosition = transform.position;
        endPosition   = startPosition + Vector3.right * MoveDistance;

        //* �O�_�ԋ������� *//
        points[0] = StartPoint;
        points[1] = HalfwayPoint;
        points[2] = EndPoint;
    }

    void Update()
    {
        //- �I������^�C�v�ɉ����ď����𕪊�
        switch (Type)
        {
            case E_MovementType.ThreewayBehaviour:
                ThreewayMove();
                break;
            case E_MovementType.ThreepointBehaviour:
                ThreePointMove();
                break;
            case E_MovementType.CicrleBehaviour:
                CicrleMove();
                break;
        }
    }

    /// <summary>
    /// �O��������
    /// </summary>
    private void ThreewayMove()
    {
        //- null�`�F�b�N
        if (fireworks && fireworks.IsExploded) return;

        //- �o�ߎ��Ԃ��v�Z����
        timeElapsed += Time.deltaTime;

        //- �ړ��̊������v�Z����i0����1�܂ł̒l�j
        float t = Mathf.Clamp01(timeElapsed / TravelTime);

        //- �ړ������ɍ��킹�Ĉʒu��ύX����
        if (!reverse)
        {
            switch (MoveDirection)
            {
                case E_MoveDirection.Horizontal:
                    transform.position = Vector3.Lerp(startPosition, endPosition, t);
                    break;
                case E_MoveDirection.Vertical:
                    transform.position = Vector3.Lerp(
                        startPosition, startPosition + Vector3.up * MoveDistance, t);
                    break;
                case E_MoveDirection.Diagonal:
                    transform.position = Vector3.Lerp(
                        startPosition, startPosition + new Vector3(MoveDistance, MoveDistance, 0), t);
                    break;
            }
        }
        else
        {
            switch (MoveDirection)
            {
                case E_MoveDirection.Horizontal:
                    transform.position = Vector3.Lerp(endPosition, startPosition, t);
                    break;
                case E_MoveDirection.Vertical:
                    transform.position = Vector3.Lerp(
                        startPosition + Vector3.up * MoveDistance, startPosition, t);
                    break;
                case E_MoveDirection.Diagonal:
                    transform.position = Vector3.Lerp(
                        startPosition + new Vector3(MoveDistance, MoveDistance, 0), startPosition, t);
                    break;
            }
        }

        //- �ړ�������������o�ߎ��Ԃ����Z�b�g����
        if (t == 1.0f)
        {
            timeElapsed = 0.0f;
            reverse = !reverse; // �ړ������𔽓]
        }
    }

    /// <summary>
    /// �O�_�ԋ���
    /// </summary>
    private void ThreePointMove()
    {
        //- null�`�F�b�N
        if (fireworks && fireworks.IsExploded) return;

        if (isWaiting)
        {
            waitingTimer -= Time.deltaTime;
            if (waitingTimer <= 0.0f)
            {
                isWaiting = false;
                currentPoint += currentDirection;
                if (currentPoint >= points.Length || currentPoint < 0)
                {
                    currentDirection *= -1;
                    currentPoint += currentDirection;
                }
            }
        }
        else
        {
            //- ���̈ʒu�Ɉړ����邽�߂̕����x�N�g�����v�Z����
            Vector3 directionVector = (points[currentPoint] - transform.position).normalized;

            //- ���̈ʒu�Ɉړ����邽�߂̋������v�Z����
            float distanceToMove = MoveSpeed * Time.deltaTime;

            //- ���̈ʒu�Ɉړ�����
            transform.position += directionVector * distanceToMove;

            //- ���̃|�C���g�ɓ��B������������t�ɂ���
            if (Vector3.Distance(transform.position, points[currentPoint]) < 0.01f)
            {
                //- �Ō�̃|�C���g�ɓ��B������
                if (currentPoint == points.Length - 1)
                {
                    isWaiting = true;
                    waitingTimer = EndWaitTime;
                }
                else
                {
                    currentPoint += currentDirection;
                    if (currentPoint >= points.Length || currentPoint < 0)
                    {
                        currentDirection *= -1;
                        currentPoint += currentDirection;
                    }
                }
            }
        }
    }

    /// <summary>
    /// �~�^��
    /// </summary>
    private void CicrleMove()
    {
        //- null�`�F�b�N
        if (fireworks && fireworks.IsExploded) return;

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
        switch (RotaDirection)
        {
            case E_RotaDirection.Clockwise:
                currentAngle = (currentTime % PeriodTime) / PeriodTime * angle;
                break;
            case E_RotaDirection.CounterClockwise:
                currentAngle = angle - ((currentTime % PeriodTime) / PeriodTime * angle);
                break;
        }
    }
}