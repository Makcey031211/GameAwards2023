using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickMovement : MonoBehaviour
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

    //- �o�ߎ���
    private float timeElapsed;

    //- �J�n�ʒu
    private Vector3 startPosition;

    //- �I���ʒu
    private Vector3 endPosition;

    //- �ړ��̕����]���p
    private bool reverse = false;

    private void Start()
    {
        startPosition = transform.position;

        endPosition = startPosition + Vector3.right * moveDistance;
    }

    private void Update()
    {
        //- �o�ߎ��Ԃ��v�Z����
        timeElapsed += Time.deltaTime;

        //- �ړ��̊������v�Z����i0����1�܂ł̒l�j
        float t = Mathf.Clamp01(timeElapsed / travelTime);

        //- �ړ������ɍ��킹�Ĉʒu��ύX����
        if (!reverse)
        {
            switch (moveDirection)
            {
                case MoveDirection.Horizontal:
                    transform.position = Vector3.Lerp(startPosition, endPosition, t);
                    break;
                case MoveDirection.Vertical:
                    transform.position = Vector3.Lerp(
                        startPosition, startPosition + Vector3.up * moveDistance, t);
                    break;
                case MoveDirection.Diagonal:
                    transform.position = Vector3.Lerp(
                        startPosition, startPosition + new Vector3(moveDistance, moveDistance, 0), t);
                    break;
                }
            }
        else
        {
            switch (moveDirection)
            {
                case MoveDirection.Horizontal:
                    transform.position = Vector3.Lerp(endPosition, startPosition, t);
                    break;
                case MoveDirection.Vertical:
                    transform.position = Vector3.Lerp(
                        startPosition + Vector3.up * moveDistance, startPosition, t);
                    break;
                case MoveDirection.Diagonal:
                    transform.position = Vector3.Lerp(
                        startPosition + new Vector3(moveDistance, moveDistance, 0), startPosition, t);
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
}