using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireflower : MonoBehaviour
{
    [Header("�����ɕK�v�ȉ�"), SerializeField]
    private int nIgnitionMax = 3;

    private int nIgnitionCount = 0; // ���΂�����

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("HITBOSS");
        //===�@�^�O�𒲂ׂ�@===
        // ���������I�u�W�F�N�g�̃^�O���u�ԉ΁v�u�ł����ԉ΁v�ȊO�Ȃ烊�^�[��
        if (other.gameObject.tag != "Fireworks" &&
            other.gameObject.tag != "ShotFireworks") return;

        //===�@�u���b�N�ɎՂ��ĂȂ����`�F�b�N�@===
        // ���g����ԉ΂Ɍ������������v�Z
        Vector3 direction = other.gameObject.transform.position - transform.position;
        // ���g����ԉ΂Ɍ��������C���쐬
        Ray ray = new Ray(transform.position, direction);
        // ���C�����������I�u�W�F�N�g�̏�������ϐ�
        RaycastHit hit;
        // ���C���X�e�[�W�ɓ����������t���O
        bool StageHit = false;
        if (Physics.Raycast(ray, out hit))
        {
            // ���C�����������I�u�W�F�N�g�̃^�O���uStage�v�Ȃ烊�^�[��
            if (hit.collider.gameObject.tag == "Stage") return;
        }

    }
}