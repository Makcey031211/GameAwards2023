using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastCracker : MonoBehaviour
{
    [SerializeField, Header("�Ήԗp�̃I�u�W�F�N�g")]
    private GameObject particleObject;

    [SerializeField, Header("�N���b�J�[�͈͕̔\��")]
    private bool bIsDrawArea = true;

    [SerializeField, Header("�N���b�J�[�̉~�����̐��̐�")]
    private int nCircleComplementNum = 10;

    [SerializeField, Header("�N���b�J�[�̔j��p�x�͈�(0�x�`180�x)")]
    private float BrustAngle = 1.0f;

    [SerializeField, Header("�N���b�J�[�̔j��˒�")]
    private float BrustDis = 10.0f;

    [SerializeField, Header("�j��SE")]
    private AudioClip sound;

    [SerializeField, Header("�N���b�J�[�̌����ڂ��c�鎞��(�b)")]
    private float ModelDeleteTime = 1.0f;

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

    //- �����蔻��\���p�̐�
    LineRenderer linerend;

    //- �U���p�̃R���|�[�l���g
    VibrationManager vibration;

    //- �p�[�e�B�N���V�X�e��
    ParticleSystem particle;

    //- ���S�ɃI�u�W�F�N�g���������鎞��
    float DestroyTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        CrackerTransform = this.gameObject.GetComponent<Transform>();
        FireflowerScript = this.gameObject.GetComponent<FireFlower>();
        //- ���̒ǉ�
        linerend = gameObject.AddComponent<LineRenderer>();
        //- �U���R���|�[�l���g�̎擾
        vibration = GameObject.Find("VibrationManager").GetComponent<VibrationManager>();
        //- �p�[�e�B�N���̎擾
        particle = particleObject.transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    void FixedUpdate()
    {
        //- �j�􎞂̓����蔻��̕\��
        if (bIsDrawArea)
        {
            //- �_�̔z��𐶐�
            Vector3[] positions = new Vector3[nCircleComplementNum + 2];
            //- �n�_�̐���
            positions[0] = this.transform.position;

            //- �~�����̓_�̐���
            for (int i = 0; i < nCircleComplementNum + 1; i++)
            {
                //- ���S����~�����ւ̃��C�𐶐�(��������]������)
                var CircleRay = Quaternion.Euler(0, 0, (-BrustAngle / 2) + (BrustAngle / nCircleComplementNum * i)) * CrackerTransform.up.normalized;
                //- ���S���W�����C�����֐i�߂�
                var LineTransform = this.transform.position + (CircleRay * BrustDis);
                //- �_��ǉ�
                positions[i + 1] = LineTransform;
            }

            // �_�̐����w�肷��
            linerend.positionCount = positions.Length;
            // ���������ꏊ���w�肷��
            linerend.SetPositions(positions);
            //- ���ƐF�̌���
            linerend.startWidth = 0.1f;
            linerend.endWidth = 0.1f;
            //- �n�_�ƏI�_���Ȃ�
            linerend.loop = true;
        }
        //- 1�t���[���O�́A���j�˗��ϐ����X�V
        bIsOldExploded = FireflowerScript.isExploded;
        
        //- �e����^�C�~���O�ɂȂ�܂ł́A�ȉ��̔��j�������s��Ȃ�
        if (!FireflowerScript.isExploded) return;
        
        //- ����if���̒��g�͈�x�����Ă΂��B
        if (!bIsBomb)
        {
            //- �N���b�J�[�̃G�t�F�N�g����
            GameObject fire = Instantiate(
                particleObject,                     // ����(�R�s�[)����Ώ�
                transform.position,           // ���������ʒu
                Quaternion.Euler(0.0f, 0.0f, transform.localEulerAngles.z)  // �ŏ��ɂǂꂾ����]���邩
                );

            //- �U���̐ݒ�
            vibration.SetVibration(60, 1.0f);
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

                // ���g����ԉ΂Ɍ��������C���쐬
                Ray ray = new Ray(transform.position, FireworkDir);
                {
                    // ���C�����������I�u�W�F�N�g�̏�������ϐ�
                    RaycastHit hit;
                    // ���C���X�e�[�W�ɓ����������t���O
                    bool StageHit = false;
                    //- ���C���΂�
                    if (Physics.Raycast(ray, out hit))
                    {
                        //- �X�e�[�W�ɓ��������ꍇ�������Ȃ�
                        if (hit.collider.gameObject.tag == "Stage") continue;
                    }
                }
                //- �u�ԉ΂ւ̃x�N�g���v�Ɓu�N���b�J�[�̌����x�N�g���v�̊p�x�����߂�
                var angle = Vector3.Angle((CrackerTransform.up).normalized, (FireworkDir).normalized);
                if (angle != 0 && (angle < BrustAngle / 2))
                {
                    float DisDelayRatio = (dis) / (BrustDis *  particle.main.startSpeed.constantMin / 25) / 1.8f;
                    float DelayTime = (10 / particle.main.startSpeed.constantMin / 25) + DisDelayRatio;
                    //- �x���������Ĕ��j
                    StartCoroutine(DelayDestroy(obj, DelayTime));
                    continue;
                }

                //- �x���������Č����ڂ̃��f��������
                StartCoroutine(DelayDeleteModel(this.transform.GetChild(0).gameObject, ModelDeleteTime));
            }
            
            //- ���g��j�󂷂�
            Destroy(this.gameObject, DestroyTime);
        }
    }

    //- �x��ċN������֐�
    private IEnumerator DelayDestroy(GameObject Obj,float delayTime)
    {
        //- delayTime�b�ҋ@����
        yield return new WaitForSeconds(delayTime);
        //- �N�������L���ɂ���
        Obj.gameObject.GetComponent<FireFlower>().isExploded = true;
    }

    //- �x��Č����ڂ������֐�
    private IEnumerator DelayDeleteModel(GameObject Obj, float delayTime)
    {
        //- delayTime�b�ҋ@����
        yield return new WaitForSeconds(delayTime);
        //- �����ڂ�����
        Obj.SetActive(false);
    }
}
