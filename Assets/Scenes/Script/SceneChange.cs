using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField, Header("�V�[���J�ڐ�")]
    private SceneObject NextScene;

    //[SerializeField, Header("�G���J�E���g���Ă���I�u�W�F�N�g")]
    //private GameObject CountObject;

    [SerializeField, Header("�N���A���̃V�[���J�ڂ�x�炷����(�b)")]
    private float ClearDelayTime = 2.0f;

    [SerializeField, Header("���g���C���̃T�E���h���������鎞��(�b)")]
    private float SoundDelayTime = 2.0f;

    [SerializeField, Header("���g���C���̃V�[���J�ڂ�x�炷����(�b)")]
    private float RetryDelayTime = 2.0f;

    [SerializeField, Header("�N���ASE")]
    private AudioClip ClearSound;

    [SerializeField, Header("���sSE")]
    private AudioClip FailureSound;

    CountEnemy countEnemy;          // CountEnemy�X�N���v�g������ϐ�

    private int EnemyNum;           // �G�̐�
    private float CurrentTime;      // ���݂̎���(�G���S�ł��Ă���J�E���g�J�n)
    private float CurrentParticleTime = 0.0f;   // �p�[�e�B�N���̌��݂̎���
    private float TotalParticleTime = 999.0f;   // �p�[�e�B�N���̑�����
    private bool bIsShotSound = false;          //- �����Đ����ꂽ���ǂ���

    public static bool bIsChange;   // ���̃V�[���Ɉړ����邩�̃t���O
    public static bool bIsRetry;    // ���g���C���邩�̃t���O
    public bool bIsLife;     // �v���C���[���������Ă��邩

    // Start is called before the first frame update
    void Start()
    {
        bIsChange = false;
        bIsRetry = false;
        bIsLife = true;
        countEnemy = this.gameObject.GetComponent<CountEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���݂̓G�̐����X�V
        EnemyNum = countEnemy.GetCurrentCountNum();

        // �p�[�e�B�N���̍Đ����I��� + �G��S�ł�������
        if(CurrentParticleTime == TotalParticleTime && EnemyNum <= 0)
        {
            // ���݂̎��Ԃ��X�V
            CurrentTime += Time.deltaTime;
            // ���݂̎��Ԃ��x�����Ԃ𒴂�����V�[���J�ڃt���O��true�ɕς��āA�����Đ�����
            if (CurrentTime >= ClearDelayTime)
            {
                //- 1�x�����Đ�
                if (!bIsShotSound)
                {
                    //- �ϐ��̐ݒ�
                    bIsShotSound = true;
                    //- ���̍Đ�
                    gameObject.GetComponent<AudioSource>().PlayOneShot(ClearSound);
                }
                bIsChange = true;
            }
        }

        // �p�[�e�B�N���̍Đ����I��� + �G���c���Ă���
        if (CurrentParticleTime == TotalParticleTime && EnemyNum > 0 && !bIsLife)
        {
            // ���݂̎��Ԃ��X�V
            CurrentTime += Time.deltaTime;
            // ���݂̎��Ԃ��x�����Ԃ𒴂����烊�g���C�t���O��true�ɕς���
            if (CurrentTime >= RetryDelayTime)
            {
                CurrentTime = 0;
                bIsRetry = true;
            }
        }

        // �V�[���J�ڃt���O��true�Ȃ玟�̃V�[���Ɉړ�
        if (bIsChange)
        { 
        //    SceneManager.LoadScene(NextScene); 
        }

        // ���g���C�t���O��true�Ȃ猻�݂̃V�[�����ēǂݍ���
        if (bIsRetry)
        {
            // ���݂̎��Ԃ��X�V
            CurrentTime += Time.deltaTime;
            //- 1�x�����Đ�
            if (!bIsShotSound)
            {
                //- �ϐ��̐ݒ�
                bIsShotSound = true;
                //- ���̍Đ�
                gameObject.GetComponent<AudioSource>().PlayOneShot(FailureSound);
            }
            if (CurrentTime >= RetryDelayTime)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void SetParticleTime(float currentTime, float totalTime)
    {
        // �p�[�e�B�N���̌��݂̎��Ԃ���
        CurrentParticleTime = currentTime;
        // �p�[�e�B�N���̑����Ԃ���
        TotalParticleTime = totalTime;
    }
}
