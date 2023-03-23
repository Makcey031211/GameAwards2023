using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �w�肵����Ԃ������ړ�
/// </summary>
public class AutoMove : MonoBehaviour
{
    [SerializeField, Header("�ړ���")]
    private float MoveSpeed;
    [SerializeField]
    private Transform Left;
    [SerializeField]
    private Transform Right;

    //- �J�n�ʒu
    private Vector2 StartPosition;

    //- ����
    private int direction = 1;

    //- �ԉΓ_�΃X�N���v�g
    FireFlower FireflowerScript; 

    void Start()
    {
        StartPosition = transform.position;
        FireflowerScript = this.gameObject.GetComponent<FireFlower>();
    }

    void Update()
    {
        if (!FireflowerScript.isExploded)
        {
            //- �E�����ֈ��n�_���ǂ蒅�����獶������
            if (transform.position.x >= Right.position.x)
            {
                direction = -1;
            }
            //- �������ֈ��n�_���ǂ蒅������E������
            if (transform.position.x <= Left.position.x)
            {
                direction = 1;
            }
            //- ���˕Ԃ菈��
            transform.position = new Vector2(Mathf.Sin(Time.time) * MoveSpeed +
                StartPosition.x, StartPosition.y);
        }
    }
}
