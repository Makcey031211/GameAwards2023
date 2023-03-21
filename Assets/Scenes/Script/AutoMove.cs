using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    [SerializeField, Header("�ړ���")]
    private float MoveSpeed;
    [SerializeField]
    private Transform Left;
    [SerializeField]
    private Transform Right;

    private Vector2 StartPosition;
    private int direction = 1;
    FireFlower FireflowerScript; //- �ԉΓ_�΃X�N���v�g

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
        FireflowerScript = this.gameObject.GetComponent<FireFlower>();
    }

    // Update is called once per frame
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
