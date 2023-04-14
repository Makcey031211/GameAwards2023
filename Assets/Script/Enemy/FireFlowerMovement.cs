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

    //- �J�n�ʒu
    private Vector3 startPosition;

    //- �I���ʒu
    private Vector3 endPosition;

    //- �ԉ΃X�N���v�g
    FireworksModule fireworks;  

    private void Start()
    {
        startPosition = transform.position;

        fireworks = this.gameObject.GetComponent<FireworksModule>();

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
            //- ���`��Ԃ��g���ăI�u�W�F�N�g���ړ�
            float t = Mathf.PingPong(Time.time / travelTime, 1.0f);
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
        }
    }
}