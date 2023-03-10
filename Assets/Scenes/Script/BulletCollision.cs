using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // ���������I�u�W�F�N�g�̃^�O���u�ԉ΁v ���� �u�ł����ԉ΁v�Ȃ�
        if (collision.gameObject.tag == "Fireworks" ||
            collision.gameObject.tag == "ShotFireworks")
        {
            // ���������I�u�W�F�N�g��FireFlower�X�N���v�g��L���ɂ���
            collision.gameObject.GetComponent<FireFlower>().enabled = true;
            // ���������I�u�W�F�N�g��FireFlower�X�N���v�g����isExploded��true�ɕς���
            collision.gameObject.GetComponent<FireFlower>().isExploded = true;
        }
    }
}
