using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickMoveWait : MonoBehaviour
{
    [SerializeField, Header("�n�_")]
    private List<Vector3> startPoint;

    [SerializeField, Header("���ԓ_")]
    private List<Vector3> halfwayPoint;

    [SerializeField, Header("�I�_")]
    private List<Vector3> endPoint;

    [SerializeField, Header("�ړ����x")]
    private float moveSpeed = 2.0f;

    [SerializeField, Header("���݂̈ʒu")]
    private int currentPoint = 0;

    [SerializeField, Header("����")]
    private int direction = 1;

    [SerializeField, Header("���ԓ_���B���̑ҋ@����")]
    private float waitTime = 1.0f;

    private List<Vector3> points = new List<Vector3>();

    //- �ҋ@����
    private float waitingTimer = 0.0f;

    //- ���ԓ_�őҋ@���Ă��邩�ǂ���
    private bool isWaiting     = false;

    private void Start()
    {
        //--- 3�̃|�C���g��ݒ� ---
        points.AddRange(startPoint);
        points.AddRange(halfwayPoint);
        points.AddRange(endPoint);
    }

    private void Update()
    {
        if (isWaiting) // ���ԓ_�ҋ@��
        {
            waitingTimer -= Time.deltaTime;
            if (waitingTimer <= 0.0f)
            {
                isWaiting = false;
                currentPoint += direction;
                if (currentPoint >= points.Count || currentPoint < 0)
                {
                    direction *= -1;
                    currentPoint += direction;
                }
            }
        }
        else // ���ԓ_�őҋ@���Ă��Ȃ���
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
                if (currentPoint == endPoint.Count)
                {
                    isWaiting    = true;
                    waitingTimer = waitTime;
                }
                else
                {
                    currentPoint += direction;
                    if (currentPoint >= points.Count || currentPoint < 0)
                    {
                        direction *= -1;
                        currentPoint += direction;
                    }
                }
            }
        }
    }
}
