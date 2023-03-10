using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        // ���������I�u�W�F�N�g�̃^�O���u�ԉ΁v ���� �u�ł����ԉ΁v�Ȃ�
        if (other.gameObject.tag == "Fireworks" ||
            other.gameObject.tag == "ShotFireworks")
        {
            // ���������I�u�W�F�N�g��FireFlower�X�N���v�g��L���ɂ���
            other.gameObject.GetComponent<FireFlower>().enabled = true;
            // ���������I�u�W�F�N�g��FireFlower�X�N���v�g����isExploded��true�ɕς���
            other.gameObject.GetComponent<FireFlower>().isExploded = true;
        }
    }
}