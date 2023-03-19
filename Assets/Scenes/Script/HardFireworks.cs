using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardFireworks : MonoBehaviour
{
    [SerializeField, Header("�Ήԗp�̃I�u�W�F�N�g")]
    private GameObject particleObject;

    [SerializeField, Header("1��ڂ̔�����̖��G����(�b)")]
    private float BlastInvSeconds = 3.0f;

    [SerializeField, Header("���G���Ԓ��̐F(RGB)")]
    private Color color;

    //- ���G���ԗp�̃t���[���J�E���^
    int nInvFrameCount = 0;

    //- ���������ǂ���
    bool bIsNowBomb = false;

    //- �ԉΓ_�΃X�N���v�g
    FireFlower FireflowerScript;

    Rigidbody rb;

    //- ���񔚔�������
    int nBlastCount = 0;

    //- ����ڂŔ������邩
    int nBlastNum = 2;

    //- �}�e���A���̏����̐F
    Color initColor;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        FireflowerScript = this.gameObject.GetComponent<FireFlower>();
        //- �}�e���A���̏����̐F�̎擾
        initColor = this.gameObject.GetComponent<Renderer>().material.color;
    }

    void FixedUpdate()
    {
        //- ���G���ԂłȂ����ɔ��j���L���ɂȂ����ꍇ�A��������
        if (FireflowerScript.isExploded && !bIsNowBomb)
        {
            //- �F�̕ύX
            this.gameObject.GetComponent<Renderer>().material.color = color;
            //- �������̔����ݒ�
            bIsNowBomb = true;
            //- ����ڂ̔��j�����X�V     
            nBlastCount++;
            if (nBlastCount >= nBlastNum)
            {
                //- ���G���Ԃ̃��Z�b�g
                nInvFrameCount = 0;
                // ���������I�u�W�F�N�g��SphereCollider��L���ɂ���
                this.transform.GetChild(0).gameObject.GetComponent<SphereCollider>().enabled = true;
                // ���������I�u�W�F�N�g��SphereCollider��L���ɂ���
                this.transform.GetChild(0).gameObject.GetComponent<DetonationCollision>().enabled = true;
                // �w�肵���ʒu�ɐ���
                GameObject fire = Instantiate(
                    particleObject,                     // ����(�R�s�[)����Ώ�
                    transform.position,           // ���������ʒu
                    Quaternion.Euler(0.0f, 0.0f, 0.0f)  // �ŏ��ɂǂꂾ����]���邩
                    );
                // �������ɓ����蔻��𖳌���
                GetComponent<SphereCollider>().isTrigger = true;
                rb.isKinematic = true;
                GetComponent<MeshRenderer>().enabled = false;
            }
        }

        if (bIsNowBomb)
        {
            //- ���G���Ԃ̃J�E���g
            nInvFrameCount++;
            //- ���G���ԏI�����̏���
            if (nInvFrameCount >= BlastInvSeconds * 60)
            {
                //- �F�̕ύX
                this.gameObject.GetComponent<Renderer>().material.color = initColor;
                //- �܂��������Ă��Ȃ��񐔂Ȃ珈��
                if (nBlastNum > nBlastCount)
                {
                    bIsNowBomb = false;
                    FireflowerScript.isExploded = false;
                }
            }
        }
    }
}