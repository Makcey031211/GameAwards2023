using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    [SerializeField, Header("�Ή�SE")]
    private AudioClip sound;

    private void OnParticleCollision(GameObject other)
    {
        // ���������I�u�W�F�N�g�̃^�O���u�X�e�[�W�v�Ȃ�
        if (other.gameObject.tag == "Stage")
        {
            transform.parent.gameObject.GetComponent<AudioSource>().PlayOneShot(sound);
        }
    }
}