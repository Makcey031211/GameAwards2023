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
    [SerializeField, Header("�t�F�[�h�p�I�u�W�F�N�g")]
    private GameObject fadeObject;
    [SerializeField, Header("�t�F�[�h�b��")]
    private float FadeTime;


    //- �X�N���v�g�p�̕ϐ�
    BGMManager bgmManager;
    private ButtonAnime button;
    private SelectMovePlayer SelectPlayer;
    private bool Load = false;
    void Start()
    {
        button = GetComponent<ButtonAnime>();
        bgmManager  = GameObject.Find("BGMManager").GetComponent<BGMManager>();
    }
    
    public void MoveScene()
    {
        if(Load)
        { return; }

        Load = true;
        //- �N���b�N���Đ�
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);
        //- ���o�̕`��t���O�����Z�b�g
        if (CutIn.MoveCompleat) { CutIn.ResetMoveComplete(); }
        if (BoardMove.MoveComplete) { BoardMove.ResetMoveComplete(); }
        if (OpeningAnime.MoveCompleat) { OpeningAnime.ResetMoveComplete(); }

        DOVirtual.DelayedCall(DelayTime, () => fadeObject.GetComponent<ObjectFade>().SetFade(ObjectFade.FadeState.In, FadeTime));
        button.PushButtonAnime();
        //- �V�[����ς���O��BGM������
        DOVirtual.DelayedCall (FadeTime, ()=> bgmManager.DestroyBGMManager()).SetDelay(DelayTime); 
        DOVirtual.DelayedCall (FadeTime, ()=> SceneManager.LoadScene(NextScene)).SetDelay(DelayTime);
    }

    public void MoveSelectScene()
    {

        if (Load)
        { return; }

        Load = true;

        //- ���o�̕`��t���O�����Z�b�g
        if (CutIn.MoveCompleat) { CutIn.ResetMoveComplete(); }
        if (BoardMove.MoveComplete) { BoardMove.ResetMoveComplete(); }
        if (OpeningAnime.MoveCompleat) { OpeningAnime.ResetMoveComplete(); }

        SelectPlayer = GetComponent<SelectMovePlayer>();
        //- �N���b�N���Đ�
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);
        DOVirtual.DelayedCall(DelayTime, () => fadeObject.GetComponent<ObjectFade>().SetFade(ObjectFade.FadeState.In, FadeTime));
        SelectPlayer.InStageMove();
        //- �V�[����ς���O��BGM������
        DOVirtual.DelayedCall(FadeTime, () => bgmManager.DestroyBGMManager()).SetDelay(DelayTime);
        DOVirtual.DelayedCall(FadeTime, () => SceneManager.LoadScene(NextScene)).SetDelay(DelayTime);
    }

    public void EndGimmick()
    {
        //- �N���b�N���Đ�
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);
        //- �M�~�b�N�̓P�ނ��s��
        board.OutMove();
    }


}