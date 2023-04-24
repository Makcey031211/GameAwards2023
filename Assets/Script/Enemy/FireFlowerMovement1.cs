using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �O�����ړ�(�w�肵���b�����ҋ@)
/// </summary>
public class FireFlowerMovement1 : MonoBehaviour
{
    private enum MoveDirection
    {
        Horizontal, //���ړ�
        Vertical,   //�c�ړ�
        Diagonal    //�΂߈ړ�
    }

    [SerializeField, Header("�ړ�����")]
    private MoveDirection moveDirection = MoveDirection.Horizontal;

    [SerializeField, Header("�ړ�����")]
    private float moveDistance = 5.0f;

    [SerializeField, Header("�ړ�����")]
    private float travelTime = 1.0f;

    [SerializeField, Header("�ҋ@����")]
    private float waitTime = 2.0f;

    //- �ړ������ǂ���
    private bool isMoving = true;

    private Vector3 startPosition; // �J�n�n�_
    private Vector3 endPosition;   // �I���n�_

    private FireworksModule fireworks;

    private void Start()
    {
        startPosition = transform.position;

        endPosition = GetEndPosition();

        fireworks = GetComponent<FireworksModule>();
    }

    private void Update()
    {
        if (!fireworks.IsExploded)
        {
            //- ��~���Ă��Ȃ���
            if (isMoving)
            {
                Move();
            }
        }
    }

    private Vector3 GetEndPosition()
    {
        switch (moveDirection)
        {
            case MoveDirection.Horizontal:
                return startPosition + Vector3.right * moveDistance;
            case MoveDirection.Vertical:
                return startPosition + Vector3.up * moveDistance;
            case MoveDirection.Diagonal:
                return startPosition + new Vector3(moveDistance, moveDistance, 0);
            default:
                return startPosition;
        }
    }

    private void Move()
    {
        //- ���`��ԂŃI�u�W�F�N�g���ړ�������
        float t = Mathf.PingPong(Time.time / travelTime, 1.0f);
        transform.position = Vector3.Lerp(startPosition, endPosition, t);

        if (Vector3.Distance(transform.position, endPosition) < 0.01f)
        {
            if (endPosition == startPosition) // StartPosition�ɖ߂�ꍇ
            {
                endPosition = GetEndPosition(); // EndPosition���X�V
                isMoving = true;
            }
            else
            {
                StartCoroutine(WaitAndMoveBack());
                isMoving = false;
            }
        }
    }

    private IEnumerator WaitAndMoveBack()
    {
        yield return new WaitForSeconds(waitTime);

        //- ���݂̎n�_���I�_�A�I�_�����݂̎n�_�ɐݒ肵�āA�ړ����ĊJ����
        Vector3 temp  = endPosition;
        endPosition   = startPosition;
        startPosition = temp;

        isMoving = true;
    }
}