using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetonationCollision : MonoBehaviour
{
    [Header("�����蔻��̍L����"), SerializeField]
    private float power = 3;

    [Header("�폜�܂ł̎���"), SerializeField]
    private float time = 3.0f;

    float currentTime; // ��������̌o�ߎ���

    void Start()
    {
        currentTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //----- �폜�܂ł̎��ԃJ�E���g
        currentTime += Time.deltaTime;
        if (currentTime >= time)
        {
            // �e�I�u�W�F�N�g���ƍ폜
            Destroy(transform.parent.gameObject);
        }
    }

    private void FixedUpdate()
    {
        float collSize = power * 0.0004f;
        if (transform.localScale.x <= 7.5f)
        {
            transform.localScale += new Vector3(collSize, collSize, collSize);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������I�u�W�F�N�g�̃^�O���u�ԉ΁v ���� �u�ł����ԉ΁v�Ȃ�
        if (other.gameObject.tag == "Fireworks" ||
            other.gameObject.tag == "ShotFireworks")
        {
            // ���������I�u�W�F�N�g��SphereCollider��L���ɂ���
            other.transform.GetChild(0).gameObject.GetComponent<SphereCollider>().enabled = true;
            // ���������I�u�W�F�N�g��SphereCollider��L���ɂ���
            other.transform.GetChild(0).gameObject.GetComponent<DetonationCollision>().enabled = true;
            // ���������I�u�W�F�N�g��FireFlower�X�N���v�g��L���ɂ���
            other.gameObject.GetComponent<FireFlower>().enabled = true;
            // ���������I�u�W�F�N�g��FireFlower�X�N���v�g����isExploded��true�ɕς���
            other.gameObject.GetComponent<FireFlower>().isExploded = true;
        }
    }
}
