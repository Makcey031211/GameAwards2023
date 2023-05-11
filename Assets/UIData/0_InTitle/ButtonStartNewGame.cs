using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ButtonStartNewGame : MonoBehaviour
{
    [SerializeField, Header("�V�[���J�ڐ�")]
    private SceneObject NextScene;
    [SerializeField, Header("�x������(�b)")]
    private float DelayTime;
    [SerializeField, Header("�t�F�[�h�b��")]
    private float FadeTime;

    //- �X�N���v�g�p�̕ϐ�
    BGMManager bgmManager;
    SaveManager saveManager;
    private ButtonAnime button;
    private bool isClick = true;
    private bool isSound = true;

    void Start()
    {
        button      = GetComponent<ButtonAnime>();
        bgmManager  = GameObject.Find("BGMManager").GetComponent<BGMManager>(); 
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
    }

    public void StartNewGame()
    {
        if (!isClick && !isSound) return; // false�̎��̓��^�[������

        //- �N���b�N�������t���O��ݒ�
        isClick = false;

        //- �͂��߂����I�����A�Q�[���f�[�^�����Z�b�g
        saveManager.ResetSaveData();

        //- �N���b�N���Đ�
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);
        isSound = false;

        DOVirtual.DelayedCall(DelayTime, () => GameObject.Find("FadeImage").GetComponent<ObjectFade>().SetFade(TweenColorFade.FadeState.In, FadeTime));
        button.PushButtonAnime();
        //- �V�[����ς���O��BGM������
        DOVirtual.DelayedCall(FadeTime, () => bgmManager.DestroyBGMManager()).SetDelay(DelayTime);
        DOVirtual.DelayedCall(FadeTime, () => SceneManager.LoadScene(NextScene)).SetDelay(DelayTime);
    }
}
