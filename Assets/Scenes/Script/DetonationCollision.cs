using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetonationCollision : MonoBehaviour
{
    [Header("�����蔻��̍L����"), SerializeField]
    private float power = 3;

    [Header("�폜�܂ł̎���"), SerializeField]
    private float time = 3.0f;

    [Header("�����蔻��̏����T�C�Y"), SerializeField]
    private Vector3 ColSize = new Vector3(1.0f,1.0f,1.0f);

    //- ��������̌o�ߎ���
    float currentTime;

    //- 2�񔚔����邩�ǂ���(DoubleBlastFireworks.cs����A�N�Z�X����ĕύX�����ϐ��ł�)
    public bool bIsDoubleBlast = false;

    //- ����ڂ̔�����
    int nBlastCount = 0;

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
            nBlastCount++;
            //- 2��ȏ㔚������ԉ΂ł͂Ȃ��A�܂���
            //- 2��ڂ̔����̏ꍇ�A��������
            if (!bIsDoubleBlast || (nBlastCount >= 2))
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
            // ���������I�u�W�F�N�g��FireFlower�X�N���v�g����isExploded��true�ɕς���
            other.gameObject.GetComponent<FireFlower>().isExploded = true;
        }
    }
}
