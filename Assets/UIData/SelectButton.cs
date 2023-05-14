using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SelectButton : MonoBehaviour
{
    [SerializeField, Header("シーン遷移先")]
    private SceneObject NextScene;
    [SerializeField, Header("看板")]
    private BoardMove board;
    [SerializeField, Header("遅延時間(秒)")]
    private float DelayTime;
    [SerializeField, Header("フェード秒数")]
    private float FadeTime;


    //- スクリプト用の変数
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
        //- クリック音再生
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);

        //- 演出の描画フラグをリセット
        CutIn.ResetMoveComplete();
        BoardMove.ResetMoveComplete();
        OpeningAnime.ResetMoveComplete();
        
        DOVirtual.DelayedCall(DelayTime, () => GameObject.Find("FadeImage").GetComponent<ObjectFade>().SetFade(TweenColorFade.FadeState.In, FadeTime));
        button.PushButtonAnime();
        //- シーンを変える前にBGMを消す
        DOVirtual.DelayedCall (FadeTime, ()=> bgmManager.DestroyBGMManager()).SetDelay(DelayTime); 
        DOVirtual.DelayedCall (FadeTime, ()=> SceneManager.LoadScene(NextScene)).SetDelay(DelayTime);
    }

    public void MoveSelectScene()
    {
        //- 演出の描画フラグをリセット
        CutIn.ResetMoveComplete();
        BoardMove.ResetMoveComplete();
        OpeningAnime.ResetMoveComplete();

        SelectPlayer = GetComponent<SelectMovePlayer>();
        //- クリック音再生
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);
        DOVirtual.DelayedCall(DelayTime, () => GameObject.Find("FadeImage").GetComponent<ObjectFade>().SetFade(TweenColorFade.FadeState.In, FadeTime));
        SelectPlayer.InStageMove();
        //- シーンを変える前にBGMを消す
        DOVirtual.DelayedCall(FadeTime, () => bgmManager.DestroyBGMManager()).SetDelay(DelayTime);
        DOVirtual.DelayedCall(FadeTime, () => SceneManager.LoadScene(NextScene)).SetDelay(DelayTime);
    }

    public void EndGimmick()
    {
        //- クリック音再生
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);
        //- ギミックの撤退を行う
        board.OutMove();
    }


}