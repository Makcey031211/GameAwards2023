using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetonationCollision : MonoBehaviour
{
    [Header("�����蔻��̍L����"), SerializeField]
    private float power = 3;

    [Header("���ڂ̓����蔻��̍ő�l"), SerializeField]
    private float MaxFirColSize = 7.5f;

    [Header("���ڂ̓����蔻��̍ő�l"), SerializeField]
    private float MaxSecColSize = 7.5f;

    [Header("�폜�܂ł̎���"), SerializeField]
    private float time = 3.0f;

    [Header("�����蔻��̏����T�C�Y"), SerializeField]
    private Vector3 ColSize = new Vector3(1.0f,1.0f,1.0f);

    //- ��������̌o�ߎ���
    float currentTime;

    //- 2�񔚔����邩�ǂ���(DoubleBlastFireworks.cs����A�N�Z�X����ĕύX�����ϐ��ł�)
    private bool _isDoubleBlast = false;
    public bool IsDoubleBlast { get { return _isDoubleBlast; } set { _isDoubleBlast = value; } }

    //- ����ڂ̔�����
    int nBlastCount = 1;

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
                //- ���Ԃ̃��Z�b�g
                currentTime = 0.0f;
                //- ���g�̃X�N���v�g�𖳌���
                this.gameObject.GetComponent<DetonationCollision>().enabled = false;
            }
            nBlastCount++;
        }
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
        // ���������I�u�W�F�N�g�̃^�O���u�ԉ�] �Ȃ�
        if (other.gameObject.tag == "Fireworks")
        {
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

                //- ���������I�u�W�F�N�g��FireworksModule�̎擾
                FireworksModule otherModule = other.gameObject.GetComponent<FireworksModule>();
                //- ���������I�u�W�F�N�g�̉ԉ΃^�C�v�ɂ���ď����𕪊�
                if (otherModule.Type == FireworksModule.FireworksType.Boss)
                    other.gameObject.GetComponent<FireworksModule>().IgnitionBoss(other.gameObject);
                else
                    other.gameObject.GetComponent<FireworksModule>().Ignition();
            }
        }
    }
}
