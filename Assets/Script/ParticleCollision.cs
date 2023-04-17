using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    [SerializeField, Header("�Ή�SE")]
    private AudioClip sound;
    [SerializeField, Header("SE�̉���")]
    private float volume;

    private bool IsOnse = false;

    private void OnParticleCollision(GameObject other)
    {
        // ���������I�u�W�F�N�g�̃^�O���u�X�e�[�W�v�Ȃ�
        if (other.gameObject.tag == "Stage")
        {
            if (!IsOnse)
            {
                IsOnse = true;
                GameObject.Find("SEManager").GetComponent<SEManager>().SetPlaySE(sound,volume);
            }
        }
    }
}