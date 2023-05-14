using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DetonationCollision : MonoBehaviour
{
    [Header("�����蔻��̍L����"), SerializeField]
    private float power = 3;

    [Header("���ڂ̓����蔻��̍ő�l"), SerializeField]
    private float MaxFirColSize = 7.5f;

    [Header("���ڂ̓����蔻��̍ő�l"), SerializeField]
    private float MaxSecColSize = 7.5f;

    [Header("�����蔻��̏����T�C�Y"), SerializeField]
    private Vector3 ColSize = new Vector3(1.0f,1.0f,1.0f);

    //- 2�񔚔����邩�ǂ���(DoubleBlastFireworks.cs����A�N�Z�X����ĕύX�����ϐ��ł�)
    private bool _isDoubleBlast = false;
    public bool IsDoubleBlast { get { return _isDoubleBlast; } set { _isDoubleBlast = value; } }

    //- ����ڂ̔�����
    int nBlastCount = 1;

    void Start()
    {
        //- ���W�̎擾
        Vector3 pos = transform.position;
    }
    
    public void EndDetonation()
    {   
        //- 2��ȏ㔚������ԉ΂ł͂Ȃ��A�܂���       
        //- 2��ڂ̔����̏ꍇ�A��������
        if (!_isDoubleBlast || (nBlastCount >= 2))
        {
            // �e�I�u�W�F�N�g���ƍ폜
            Destroy(transform.parent.gameObject);
        }
        else
        {
            //- �����蔻��T�C�Y�����ɖ߂�
            transform.localScale = ColSize;
            //- ���g�̃X�N���v�g�𖳌���
            this.gameObject.GetComponent<DetonationCollision>().enabled = false;
        }
        nBlastCount++;
    }

    private void FixedUpdate()
    {
        float collSize = power * 0.0004f;
        //- 2��ȏ㔚������ԉ΂ł͂Ȃ��A�܂���
        //- 1��ڂ̔����̏ꍇ
        if (!_isDoubleBlast || (nBlastCount < 2)) {
            if (transform.localScale.x <= MaxFirColSize) {
                transform.localScale += new Vector3(collSize, collSize, collSize);
            }
        }
        else {
            if (transform.localScale.x <= MaxSecColSize) {
                transform.localScale += new Vector3(collSize, collSize, collSize);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Fireworks") return;              // ���������I�u�W�F�N�g�̃^�O���u�ԉ�] �ȊO�Ȃ烊�^�[��

        CheckHitRayStage(other.gameObject);
    }

    //- �ԉ΋ʂɓ����������ɃX�e�[�W�I�u�W�F�N�g�ɑj�܂�ĂȂ����ǂ������ׂ�֐�
    private void CheckHitRayStage(GameObject obj)
    {
        // ���g����ԉ΂Ɍ������������v�Z
        Vector3 direction = obj.transform.position - transform.position;
        // ���g�Ɖԉ΂̒������v�Z
        float DisLength = direction.magnitude;
        // ���g����ԉ΂Ɍ��������C���쐬
        Ray ray = new Ray(transform.position, direction);
        // ���������I�u�W�F�N�g���i�[���邽�߂̕ϐ�
        var HitList = new List<RaycastHit>(); 

        // ���C�����������I�u�W�F�N�g�����ׂď��ԂɊm�F���Ă���
        foreach (RaycastHit hit in Physics.RaycastAll(ray, DisLength))
        {
            //- �ŏ��̃I�u�W�F�N�g�Ȃ疳�����Ŋi�[
            if (HitList.Count == 0)
            {
                HitList.Add(hit);
                continue;
            }
            
            //- �i�[�t���O
            bool bAdd = false;
            //- �i�[�ϐ��Ɠ��������I�u�W�F�N�g�̔�r
            for (int i = 0; i < HitList.Count; i++)
            {
                //- �i�[�t���O�`�F�b�N
                if (bAdd) break; 
                //- �������i�[�ӏ��f�[�^�̋�����蒷����΃��^�[��
                if (HitList[i].distance < hit.distance) continue;
                //- ���̃f�[�^����ԍŌ�Ɋi�[
                HitList.Add(new RaycastHit());
                //- �Ōォ��i�[�ꏊ�܂Ńf�[�^�����炷
                for (int j = HitList.Count - 1; j > i; j--)
                {
                    //- �f�[�^����ړ�
                    HitList[j] = HitList[j - 1];
                }
                //- �i�[�ꏊ�Ɋi�[
                HitList[i] = hit;
                bAdd = true;
            }

            //- �i�[�t���O�������Ă��Ȃ���΁A��ԋ����������I�u�W�F�N�g�Ȃ̂�
            //- �z��̈�ԍŌ�Ɋi�[����
            if (!bAdd) HitList.Add(hit);
        }

        //- �������Z�����̂��璲�ׂ�
        for (int i = 0; i < HitList.Count; i++)
        {
            RaycastHit hit = HitList[i];

            //- �����蔻��̃f�o�b�O�\��
            if (Input.GetKey(KeyCode.Alpha1))
            {
                float markdis = 0.1f;
                Debug.DrawRay(transform.position, direction, Color.red, 3.0f);
                Debug.DrawRay(hit.point, new Vector3(+markdis, +markdis, 0), Color.blue, 3.0f);
                Debug.DrawRay(hit.point, new Vector3(+markdis, -markdis, 0), Color.blue, 3.0f);
                Debug.DrawRay(hit.point, new Vector3(-markdis, +markdis, 0), Color.blue, 3.0f);
                Debug.DrawRay(hit.point, new Vector3(-markdis, -markdis, 0), Color.blue, 3.0f);
            }
            if (hit.collider.gameObject.tag != "Stage") continue; //- �X�e�[�W�I�u�W�F�N�g�ȊO�Ȃ玟��
            if (hit.distance > DisLength) continue;               //- �ԉ΋ʂ��X�e�[�W�I�u�W�F�N�g�����ɂ���Ύ���

            //- ���������ԉ΋ʂ���O�ɃX�e�[�W�I�u�W�F�N�g�����݂���
            return; //- �������I��
        }
        
        //- ���������I�u�W�F�N�g��FireworksModule�̎擾
        FireworksModule module = obj.GetComponent<FireworksModule>();
        //- ���������I�u�W�F�N�g�̉ԉ΃^�C�v�ɂ���ď����𕪊�
        if (module.Type == FireworksModule.FireworksType.Boss)
            module.IgnitionBoss(obj);
        else if(module.Type != FireworksModule.FireworksType.ResurrectionPlayer)
            module.Ignition(transform.position);
        else if (module.Type == FireworksModule.FireworksType.ResurrectionPlayer)
            if(module.GetIsInv() == false)
            { module.Ignition(transform.position); }

        //- �X�e�[�W�I�u�W�F�N�g�ɓ������Ă��Ȃ�
        return;
    }
}
