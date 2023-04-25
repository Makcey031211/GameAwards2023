using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;

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

    //- ���Z�b�g���n�܂������̃t���O
    bool bIsStartReset = false;

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
            nPushFrameCount++; //- �J�E���g��i�߂�
            image.fillAmount = ((float)nPushFrameCount) / (RetryTime * 60); //- UI�̃��Z�b�g�Q�[�W�̑���
        }
        else
        {
            nPushFrameCount = 0;  // �J�E���g��߂�
            image.fillAmount = 0; // UI�̃��Z�b�g�Q�[�W��߂�

        }
        //- ��莞�Ԓ��������ꂽ�珈������
        if (nPushFrameCount >= RetryTime * 60)
        {
            if (bIsStartReset == true) return; //- ���Z�b�g�J�n�t���O�������Ă���΃��^�[��

            bIsStartReset = true; //�V�[���J�n�t���O�����Ă�
            GameObject.Find("FadeImage").GetComponent<ObjectFade>().SetFade(TweenColorFade.FadeState.In, 0.3f); // �t�F�[�h�J�n
            DOVirtual.DelayedCall(0.3f, () => SceneManager.LoadScene(SceneManager.GetActiveScene().name)); // �V�[���̃��[�h(�x������)
        }
    }

    public void OnReset(InputAction.CallbackContext context)
    {
        //- �{�^����������Ă���ԁA�ϐ���ݒ�
        if (context.started) { bIsPushButton = true; }
        if (context.canceled) { bIsPushButton = false; }
    }
}
