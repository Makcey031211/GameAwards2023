using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// L������
/// </summary>
public class LSharpMoveGmi : MonoBehaviour
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

    private List<Vector3> points = new List<Vector3>();

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
        if (Vector3.Distance(transform.position, points[currentPoint]) < 0.1f)
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