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

    [SerializeField, Header("�I������")]
    private float stopTime = 2.0f;

    //- �ړ������ǂ���
    private bool isMoving  = true;

    //- ��~�����ǂ���
    private bool isStopped = false;

    private Vector3 startPosition; // �J�n�n�_
    private Vector3 endPosition;   // �I���n�_

    private FireworksModule fireworks;

    private void Start()
    {
        startPosition = transform.position;

        fireworks = GetComponent<FireworksModule>();

        //- �ړ��̏�ԑJ��
        switch (moveDirection)
        {
            case MoveDirection.Horizontal:
                endPosition = startPosition + Vector3.right * moveDistance;
                break;
            case MoveDirection.Vertical:
                endPosition = startPosition + Vector3.up * moveDistance;
                break;
            case MoveDirection.Diagonal:
                endPosition = startPosition + new Vector3(moveDistance, moveDistance, 0);
                break;
        }
    }

    private void Update()
    {
        if (!fireworks.IsExploded)
        {
            //- ��~���Ă��Ȃ���
            if (!isStopped)
            {
                //- ���`��Ԃ��g���ăI�u�W�F�N�g���ړ�
                float t = Mathf.PingPong(Time.time / travelTime, 1.0f);
                transform.position = Vector3.Lerp(startPosition, endPosition, t);

                //- �I���n�_�����l�ȉ��Ȃ�
                if (Vector3.Distance(transform.position, endPosition) < 0.01f)
                {
                    isStopped = true;
                    StartCoroutine(StopAndWait());
                }
            }
            else if (isMoving) //- �ړ����Ă��鎞
            {
                StartCoroutine(WaitAndMoveBack());
            }
        }
    }

    /// <summary>
    /// �w�肵�����Ԃ̊ԁA�I�u�W�F�N�g��ҋ@������֐�
    /// </summary>
    /// <returns>isStopped</returns>
    private IEnumerator StopAndWait()
    {
        //- stopTime���ҋ@������
        yield return new WaitForSeconds(stopTime);

        isStopped = false;
        isMoving  = true;
    }

    /// <summary>
    /// �ҋ@���I��������A�Ăшړ�������֐�
    /// </summary>
    private IEnumerator WaitAndMoveBack()
    {
        isMoving = false;
        yield return new WaitForSeconds(stopTime);

        //- ���݂̏I�_���n�_�A�n�_�����݂̏I�_�ɐݒ肵�āA�ړ����ĊJ����
        Vector3 temp  = endPosition;
        endPosition   = startPosition;
        startPosition = temp;

        isStopped = false;
        isMoving  = true;
    }
}