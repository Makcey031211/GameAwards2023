using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BackScene : MonoBehaviour
{
    [SerializeField, Header("�I�v�V��������ɂ����鎞��(�b)")]
    private float OptionTime = 1.0f;
    [SerializeField, Header("�V�[���ړ����̃t�F�[�h����(�b)")]
    private float FadeTime   = 1.0f;
    [SerializeField, Header("�V�[���ړ�����BGM�؂�ւ�����(�b)")]
    private float disBGMTime = 1.0f;
    [SerializeField, Header("�߂��̃V�[����ݒ�")]
    private SceneObject backScene;

    //- �X�N���v�g�p�̕ϐ�
    BGMManager bgmManager;
    //- �C���[�W�̃R���|�[�l���g
    Image imageInGame;

    //- �{�^����������Ă��邩�ǂ���
    bool bIsPushBack = false;

    //- �{�^���������ꂽ����
    float bPushTimeBack = 0;

    //- �V�[���ړ����n�܂������ǂ����̃t���O
    bool bIsStartInGame = false;

    private void Start()
    {
        //- �R���|�[�l���g�̎擾
        imageInGame = GameObject.Find("InGameGage").GetComponent<Image>();
        bgmManager  = GameObject.Find("BGMManager").GetComponent<BGMManager>();
    }

    private void FixedUpdate()
    {
        //- �V�[���ړ����n�܂������ǂ����̃t���O���擾
        bool bIsMoveScene = false;

        //- �V�[���ړ��̃t���O�X�V 
        if (bIsStartInGame) bIsMoveScene = true;

        //�����������@�߂�{�^���@����������
        //- �u�V�[���ړ��{�^����������Ă�v���u�V�[���ړ����n�܂��Ă��Ȃ��v��
        if (bIsPushBack && !bIsMoveScene)
        {
            bPushTimeBack += Time.deltaTime;                     // �v�b�V�����Ԃ̍X�V
            imageInGame.fillAmount = bPushTimeBack / OptionTime; // �Q�[�W�̍X�V
        }
        else if (!bIsMoveScene)
        {
            bPushTimeBack = 0;          // �v�b�V�����Ԃ̃��Z�b�g
            imageInGame.fillAmount = 0; // �Q�[�W�̃��Z�b�g
        }
        //- ��莞�Ԓ��������ꂽ�珈������
        if (bPushTimeBack >= OptionTime)
        {
            if (bIsStartInGame == true) return; // ���Z�b�g�J�n�t���O�������Ă���΃��^�[��
            bIsStartInGame = true; // �V�[���J�n�t���O�����Ă�
            SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click); // �N���b�N���Đ�
            GameObject.Find("ColorFadeImage").GetComponent<ObjectFade>().SetFade(ObjectFade.FadeState.In, FadeTime); // �t�F�[�h�J�n
            DOVirtual.DelayedCall(disBGMTime, () => bgmManager.DestroyBGMManager());  // �V�[����ς���O��BGM������
            DOVirtual.DelayedCall(FadeTime, () => SceneManager.LoadScene(backScene)); // �V�[���̃��[�h(�x������)
        }
    }

    /// <summary>
    /// �R���g���[���[���擾����֐�
    /// </summary>
    /// <param name="context"></param>
    public void OnInBack(InputAction.CallbackContext context)
    {
        //- �{�^����������Ă���ԁA�ϐ���ݒ�
        if (context.started) { bIsPushBack = true; }
        if (context.canceled) { bIsPushBack = false; }
    }
}