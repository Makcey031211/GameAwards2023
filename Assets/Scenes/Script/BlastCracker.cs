using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastCracker : MonoBehaviour
{
    [SerializeField, Header("�Ήԗp�̃I�u�W�F�N�g")]
    private GameObject particleObject;

    [SerializeField, Header("�N���b�J�[�͈͕̔\��")]
    private bool bIsDrawArea = true;

    [SerializeField, Header("�N���b�J�[�̔j��p�x�͈�(0�x�`180�x)")]
    private float BrustAngle = 1.0f;

    [SerializeField, Header("�N���b�J�[�̔j��˒�")]
    private float BrustDis = 10.0f;

    [SerializeField, Header("�j��̈��΂̒x��(�b)")]
    private float ShotHitSecond = 1.0f;

    [SerializeField, Header("�j���N���b�J�[�̌����ڂ��c�葱���鎞��(�b)")]
    private float DestroyTime = 1.0f;

    [SerializeField, Header("�j��SE")]
    private AudioClip sound;

    //- �N���b�J�[���j�􂷂����
    Transform CrackerTransform;

    //- �ԉΓ_�΃X�N���v�g
    FireFlower FireflowerScript;

    //- 1�t���[���O�̔��j�m�F�ϐ�
    bool bIsOldExploded = false;

    //- �e����^�C�~���O�̒x��(�t���[��)
    private float ShotHitFrame;

    //- ���Ύ��t���[���J�E���g
    int HitFrameCount = 0;

    //- ���j���̒e���A�����̏������s�������ǂ���
    bool bIsBomb = false;

    // Start is called before the first frame update
    void Start()
    {
        CrackerTransform = this.gameObject.GetComponent<Transform>();
        FireflowerScript = this.gameObject.GetComponent<FireFlower>();
        ShotHitFrame = ShotHitSecond * 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //- �j�􎞂̓����蔻��̕\��
        if (bIsDrawArea)
        {
            //- ���E���ꂼ��̃��C�̐���
            var RightRay = Quaternion.Euler(0, BrustAngle / 2, 0) * CrackerTransform.forward.normalized;
            var LeftRay = Quaternion.Euler(0, -BrustAngle / 2, 0) * CrackerTransform.forward.normalized;
            //- ���ꂼ��̃��C�̕\��
            Debug.DrawRay(CrackerTransform.transform.position, RightRay * BrustDis, Color.red);
            Debug.DrawRay(CrackerTransform.transform.position, LeftRay * BrustDis, Color.red);
        }
        //- 1�t���[���O�́A���j�˗��ϐ����X�V
        bIsOldExploded = FireflowerScript.isExploded;


        //- �e����^�C�~���O�ɂȂ�܂ł́A�ȉ��̔��j�������s��Ȃ�
        if (!FireflowerScript.isExploded) return;

        //- ���j�˗����󂯂���A�e���鎞�̒x������
        if (HitFrameCount < ShotHitFrame)
        {
            HitFrameCount++;
            return;
        }

        //- ����if���̒��g�͈�x�����Ă΂��B
        if (!bIsBomb)
        {
            //- ���̍Đ�
            gameObject.GetComponent<AudioSource>().PlayOneShot(sound);
            //- �e����Ƃ��̏�������ϐ���ݒ�
            bIsBomb = true;
            //- �^�O���ԉ΂̃I�u�W�F�N�g��S�Ď擾
            GameObject[] Fireworks = GameObject.FindGameObjectsWithTag("Fireworks");
            // ���_����N���b�J�[�ւ̃x�N�g��
            Vector3 origin = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            //- �ԉ΂̃I�u�W�F�N�g��������s
            foreach (var obj in Fireworks)
            {
                //- ���_����ԉ΂ւ̃x�N�g��
                Vector3 direction = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
                //- �N���b�J�[����ԉ΂ւ̃x�N�g��
                Vector3 FireworkDir = direction - origin;
                //- �ԉ΂Ƃ̋������擾
                float dis = Vector3.Distance(origin, direction);
                //- �ԉ΂Ƃ̋������˒�������Ȃ������珈�����Ȃ�
                if (dis > BrustDis) continue;

                //- �u�ԉ΂ւ̃x�N�g���v�Ɓu�N���b�J�[�̌����x�N�g���v�̊p�x�����߂�
                var angle = Vector3.Angle((CrackerTransform.forward).normalized, (FireworkDir).normalized);
                if (angle != 0 && (angle < BrustAngle / 2))
                {
                    obj.gameObject.GetComponent<FireFlower>().isExploded = true;
                    continue;
                }
            }

            for (int i = 0; i < 30; i++)
            {
                //- ������ԗ͂𐶐�
                Vector3 ForceRay = Quaternion.Euler(0, Random.Range(-20, 21), 0) * CrackerTransform.forward * Random.Range(200,1500);
                // �w�肵���ʒu�ɐ���
                GameObject fire = Instantiate(
                    particleObject,                     // ����(�R�s�[)����Ώ�
                    transform.position,           // ���������ʒu
                    Quaternion.Euler(0.0f, 0.0f, 0.0f)  // �ŏ��ɂǂꂾ����]���邩
                    );
                //- ������ɐ�����ԗ͂�^����
                fire.GetComponent<Rigidbody>().AddForce(ForceRay);
            }
            
            //- ���g��j�󂷂�
            Destroy(this.gameObject, DestroyTime);
        }
    }
}
