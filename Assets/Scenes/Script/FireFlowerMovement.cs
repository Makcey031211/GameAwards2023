using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �O��������
/// </summary>
public class FireFlowerMovement : MonoBehaviour
{
    //- �񋓌^��`
    private enum MoveDirection
    {
        Horizontal, // ���ړ�
        Vertical,   // �c�ړ�
        Diagonal    // �΂߈ړ�
    }

    [SerializeField, Header("�ړ�����")]
    private MoveDirection moveDirection = MoveDirection.Horizontal;

    [SerializeField, Header("�ړ�����")]
    private float moveDistance = 5.0f;

    [SerializeField, Header("�ړ�����")]
    private float travelTime = 1.0f;

    private Vector3 startPosition; // �J�n�ʒu
    private Vector3 endPosition;   // �I���ʒu

    private void Start()
    {
        startPosition = transform.position;

        //- �ړ��̏�ԑJ��
        switch (moveDirection)
        {
            case MoveDirection.Horizontal:
                endPosition = startPosition + Vector3.right * moveDistance;
                break;
            case MoveDirection.Vertical:
                endPosition = startPosition + Vector3.forward * moveDistance;
                break;
            case MoveDirection.Diagonal:
                endPosition = startPosition + new Vector3(moveDistance, 0, moveDistance);
                break;
        }
    }

    private void Update()
    {
        //- ���`��Ԃ��g���ăI�u�W�F�N�g���ړ�
        float t = Mathf.PingPong(Time.time / travelTime, 1.0f);
        transform.position = Vector3.Lerp(startPosition, endPosition, t);
    }
}
