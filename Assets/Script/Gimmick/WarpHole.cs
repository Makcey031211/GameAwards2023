using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpHole : MonoBehaviour
{
    [SerializeField, Header("���[�v����ʒu")]
    private Transform warpPoint;

    private void OnTriggerEnter(Collider other)
    {
        //- ���[�v�z�[���ɐڐG������w�肵���ʒu�Ƀ��[�v����
        if (other.CompareTag("Untagged"))
        {
            other.transform.position = warpPoint.position;
        }
    }
}
