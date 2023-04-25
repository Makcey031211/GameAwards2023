using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    private bool IsOnce = false;

    private void OnParticleCollision(GameObject other)
    {
        // ���������I�u�W�F�N�g�̃^�O���u�X�e�[�W�v�Ȃ�
        if (other.gameObject.tag == "Stage")
        {
            if (!IsOnce)
            {
                IsOnce = true;
                //- �Ήԉ��̍Đ�
                SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Spark);
            }
        }
    }
}