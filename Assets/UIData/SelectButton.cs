using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SelectButton : MonoBehaviour
{
    [SerializeField, Header("�V�[���J�ڐ�")]
    private SceneObject NextScene;
    [SerializeField, Header("�Ŕ�")]
    private BoardMove board;
    [SerializeField, Header("�x������(�b)")]
    private float DelayTime;
    [SerializeField, Header("�t�F�[�h�b��")]
    private float FadeTime;


    //- �X�N���v�g�p�̕ϐ�
    BGMManager bgmManager;
    private ButtonAnime button;
    private SelectMovePlayer SelectPlayer;
    void Start()
    {
        button = GetComponent<ButtonAnime>();
        bgmManager  = GameObject.Find("BGMManager").GetComponent<BGMManager>();
    }
    
    public void MoveScene()
    {
        //- �N���b�N���Đ�
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);

        DOVirtual.DelayedCall(DelayTime, () => GameObject.Find("FadeImage").GetComponent<ObjectFade>().SetFade(TweenColorFade.FadeState.In, FadeTime));
        button.PushButtonAnime();
        //- �V�[����ς���O��BGM������
        DOVirtual.DelayedCall (FadeTime, ()=> bgmManager.DestroyBGMManager()).SetDelay(DelayTime); 
        DOVirtual.DelayedCall (FadeTime, ()=> SceneManager.LoadScene(NextScene)).SetDelay(DelayTime);
    }

    public void MoveSelectScene()
    {
        SelectPlayer = GetComponent<SelectMovePlayer>();
        //- �N���b�N���Đ�
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);
        GameObject.Find("FadeImage").GetComponent<ObjectFade>().SetFade(TweenColorFade.FadeState.In, 0.5f);
        SelectPlayer.InStageMove();
        //- �V�[����ς���O��BGM������
        DOVirtual.DelayedCall(0.5f, () => bgmManager.DestroyBGMManager());
        DOVirtual.DelayedCall(0.5f, () => SceneManager.LoadScene(NextScene));
    }

    public void EndGimmick()
    {
        //- �N���b�N���Đ�
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);
        //- �M�~�b�N�̓P�ނ��s��
        board.OutMove();
    }


}