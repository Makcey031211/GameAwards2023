using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpGate : MonoBehaviour
{
    [SerializeField, Header("���[�v�Q�[�g")]
    private GameObject WarpHole;
    [SerializeField, Header("���[�v����ʒu")]
    private Transform warpPoint;
    [SerializeField, Header("���[�v�Q�[�g�͈̔�")]
    private float radius = 1.0f;
    [SerializeField, Header("�z�����܂�鎞�̑��x(�b)")]
    private float suctionSpeed = 1.0f;
    [SerializeField, Header("�f���o����鎞�̑��x(�b)")]
    private float spittingSpeed = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        //- ���[�v�z�[���ɐڐG������w�肵���ʒu�Ƀ��[�v����
        if (other.gameObject.name == "Player")
        {
            //- �v���C���[�ƃ��[�v�Q�[�g�̋������v�Z
            Vector3 playerPos = other.transform.position;
            Vector3 gatePos   = transform.position;
            float distance = Vector3.Distance(playerPos, gatePos);
            //���[�v����o�Ă�������ƈʒu�����̂��߂ɓ��ˊp���擾
            Vector3 warpdistance = WarpHole.transform.position - other.transform.position;

            //- �v���C���[�����[�v���鏈��
            if (distance < radius)
            {
                var cc = other.gameObject.GetComponent<CharacterController>();
                cc.enabled = false;
                //- �v���C���[���w�肵���ʒu�Ƀ��[�v������
                other.transform.position = warpPoint.position + warpdistance * 1.2f;
                Vector3 pos = other.transform.position;
                pos.z = 0.0f;
                other.transform.position = pos;
                cc.enabled = true;
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    //- ���[�v�z�[���ɐG�ꂽ��w�肵���ʒu�Ƀ��[�v����
    //    if (other.gameObject.tag == "Player")
    //    {
    //        //- �v���C���[�ƃ��[�v�Q�[�g�̋������v�Z
    //        Vector3 playerPos = other.transform.position;
    //        Vector3 gatePos   = transform.position;
    //        float distance    = Vector3.Distance(playerPos, gatePos);

    //        //- �v���C���[�����[�v���鏈��
    //        if (distance < radius)
    //        {
    //            //- �v���C���[�����[�v�Q�[�g�Ɍ������ċz���񂹂�
    //            Vector3 surtionDir = (gatePos - playerPos).normalized;
    //            other.transform.position += surtionDir * suctionSpeed * Time.deltaTime;
    //        }
    //    }
    //}
    ///// <summary>
    ///// ���[�v��̓f���o�����ꏊ�����߂�֐�
    ///// </summary>
    //private void SpitOutPlayer()
    //{
    //    //- �f���o��������������
    //    Vector3 spitDir = (warpPoint.position - transform.position).normalized;

    //    //- �v���C���[�ɑ��x�������ĉ��o����
    //    Rigidbody playerRb = GetComponent<Rigidbody>();
    //    if (playerRb != null)
    //    {
    //        playerRb.velocity = spitDir * spittingSpeed;
    //    }
    //}

}
