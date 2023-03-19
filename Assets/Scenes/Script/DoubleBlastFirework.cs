using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBlastFirework : MonoBehaviour
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
    private int nBlastCount = 0;

    //- ���񔚔��ł��邩
    int nBlastNum = 2;

    //- �}�e���A���̏����̐F
    Color initColor;

    void Start()
    {
        //- Rigidbody�̎擾
        rb = GetComponent<Rigidbody>();
        //- �����������ǂ����̃X�N���v�g���擾
        FireflowerScript = this.gameObject.GetComponent<FireFlower>();
        //- �����蔻��X�N���v�g�̂Q�񔚔���bool��true�ɕύX
        this.transform.GetChild(0).gameObject.GetComponent<DetonationCollision>().bIsDoubleBlast = true;
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
            //- �����񐔂��X�V
            nBlastCount++;
            //- �������̔����ݒ�
            bIsNowBomb = true;
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
            //- ���ȏ㔚����������s���鏈��
            if (nBlastCount >= nBlastNum)
            {
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
                //- �������̔����ݒ�
                bIsNowBomb = false;
                //- �F�̕ύX
                this.gameObject.GetComponent<Renderer>().material.color = initColor;
                // ���������I�u�W�F�N�g��SphereCollider�𖳌��ɂ���
                this.transform.GetChild(0).gameObject.GetComponent<SphereCollider>().enabled = false;
                // ���������I�u�W�F�N�g��SphereCollider�𖳌��ɂ���
                this.transform.GetChild(0).gameObject.GetComponent<DetonationCollision>().enabled = false;
                // �������ɓ����蔻���L����
                GetComponent<SphereCollider>().isTrigger = false;
                rb.isKinematic = false;
                //- ���������������
                FireflowerScript.isExploded = false;
            }
        }
    }
}
