using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickLMoveWait : MonoBehaviour
{
    [SerializeField, Header("�n�_")]
    private List<Vector3> startPoint;

    [SerializeField, Header("���ԓ_")]
    private List<Vector3> halfwayPoint;

    [SerializeField, Header("�I�_")]
    private List<Vector3> endPoint;

    [SerializeField, Header("�ړ����x")]
    private float moveSpeed = 5.0f;

    [SerializeField, Header("���݂̈ʒu")]
    private int currentPoint = 0;

    [SerializeField, Header("����")]
    private int direction = 1;

    [SerializeField, Header("���ԓ_���B���̑ҋ@����")]
    private float waitTime = 2.0f;

    private List<Vector3> points = new List<Vector3>();

    //- ���ԓ_�ɓ��B�������ǂ����̃t���O
    private bool isReachedHalfwayPoint = false;

    //- ���ԓ_�ɓ��B��������
    private float reachedHalfwayPointTime = 0;

    private bool isMovingBackward = false;

    private void Start()
    {
        //--- 3�̃|�C���g��ݒ� ---
        points.AddRange(startPoint);
        points.AddRange(halfwayPoint);
        points.AddRange(endPoint);
    }

    private void Update()
    {
        //- ���̈ʒu�Ɉړ����邽�߂̕����x�N�g�����v�Z����
        Vector3 directionVector = (points[currentPoint] - transform.position).normalized;

        //- ���̈ʒu�Ɉړ����邽�߂̋������v�Z����
        float distanceToMove = moveSpeed * Time.deltaTime;

        //- ���̈ʒu�Ɉړ�����
        transform.position += directionVector * distanceToMove;

        //- ���̃|�C���g�ɓ��B������������t�ɂ���
        if (Vector3.Distance(transform.position, points[currentPoint]) < 0.01f)
        {
            if (currentPoint >= halfwayPoint.Count && !isReachedHalfwayPoint)
            {
                //- ���ԓ_�ɓ��B�������̏���
                isReachedHalfwayPoint = true;
                reachedHalfwayPointTime = Time.time;
            }

            if (isReachedHalfwayPoint && (Time.time - reachedHalfwayPointTime) < waitTime)
            {
                //- ���ԓ_�ɓ��B��A�ҋ@���鎞�Ԃ��܂��c���Ă���ꍇ
                return;
            }

            currentPoint += direction;

            if (isMovingBackward && currentPoint == 0)
            {
                //- �����ړ����I������ꍇ�� isMovingBackward �� false �ɂ���
                isMovingBackward = false;
            }
            else if (currentPoint >= points.Count || currentPoint < 0)
            {
                //- �[�_�ɓ��B�����ꍇ�͕������t�ɂ���
                direction *= -1;
                currentPoint += direction;
                isMovingBackward = true;
            }

            if (currentPoint == halfwayPoint.Count - 1)
            {
                //- ���ԓ_�ŕ������t�ɂ���
                direction *= -1;
                isMovingBackward = true;
            }

            //- ���ԓ_�ɖ߂�ꍇ�͑ҋ@�t���O���I�t�ɂ���
            if (currentPoint < halfwayPoint.Count)
            {
                isReachedHalfwayPoint = false;
            }
        }
    }
}