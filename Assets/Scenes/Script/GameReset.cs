using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameReset : MonoBehaviour
{
    //[SerializeField, Header("�G���J�E���g���Ă���I�u�W�F�N�g")]
    //private GameObject CountObject;

    [SerializeField, Header("���g���C�ɂ����钷��������(�b)")]
    private float RetryTime = 1.0f;

    //- �{�^����������Ă��邩�ǂ���
    bool bIsPushButton = false;

    //- �t���[���J�E���g
    int nPushFrameCount = 0;

    //- �C���[�W�̃Q�[���I�u�W�F�N�g
    GameObject Object;

    //- �C���[�W�̃R���|�[�l���g
    Image image;

    // CountEnemy�X�N���v�g������ϐ�
    CountEnemy countEnemy;

    // �G�̐�
    int EnemyNum;

    void Start()
    {
        //- �Q�[���I�u�W�F�N�g�̌���
        Object = GameObject.Find("ResetGage");
        //- �R���|�[�l���g�̎擾
        image = Object.GetComponent<Image>();
        //- �G�J�E���gUI�̎擾
        countEnemy = GameObject.Find("Main Camera").GetComponent<CountEnemy>();
    }
    
    void FixedUpdate()
    {
        // ���݂̓G�̐����X�V
        EnemyNum = countEnemy.GetCurrentCountNum();

        //- �G���c���Ă���A�{�^����������Ă���ԁA��������
        if (bIsPushButton && EnemyNum > 0)
        {
            //- �J�E���g��i�߂�
            nPushFrameCount++;
            //- UI�̑���
            image.fillAmount = ((float)nPushFrameCount) / (RetryTime * 60);
        }
        else
        {
            //- ���l�̏�����
            image.fillAmount = 0;
            nPushFrameCount = 0;

        }
        //- �������ŃV�[���J��
        if (nPushFrameCount >= RetryTime * 60)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void OnReset(InputAction.CallbackContext context)
    {
        //- �{�^����������Ă���ԁA�ϐ���ݒ�
        if (context.started) { bIsPushButton = true; }
        if (context.canceled) { bIsPushButton = false; }
    }
}
