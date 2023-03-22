using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPingPong : MonoBehaviour
{
    [SerializeField,Header("���`��Ԃ̎n�_")]
    private Transform startPoint;

    [SerializeField,Header("���`��Ԃ̏I�_")]
    private Transform endPoint;

    [SerializeField,Header("�ړ�����")]
    private float travelTime = 1;

    // Update is called once per frame
    private void Update()
    {
        // �n�_�E�I�_�̈ʒu�擾
        var s = startPoint.position;
        var e = endPoint.position;

        // ��Ԉʒu�v�Z
        var t = Mathf.PingPong(Time.time / travelTime, 1);

        // ��Ԉʒu�𔽉f
        transform.position = Vector3.Lerp(s, e, t);
    }
}
