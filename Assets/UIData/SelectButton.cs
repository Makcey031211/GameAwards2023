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
        GameObject.Find("FadeImage").GetComponent<ObjectFade>().SetFade(TweenColorFade.FadeState.In, 0.5f);
        button.PushButtonAnime();
        //- シーンを変える前にBGMを消す
        DOVirtual.DelayedCall (0.5f, ()=> bgmManager.DestroyBGMManager()); 
        DOVirtual.DelayedCall (0.5f, ()=> SceneManager.LoadScene(NextScene));
    }

    public void MoveSelectScene()
    {
        SelectPlayer = GetComponent<SelectMovePlayer>();
        //- クリック音再生
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);
        GameObject.Find("FadeImage").GetComponent<ObjectFade>().SetFade(TweenColorFade.FadeState.In, 0.5f);
        SelectPlayer.InStageMove();
        //- シーンを変える前にBGMを消す
        DOVirtual.DelayedCall(0.5f, () => bgmManager.DestroyBGMManager());
        DOVirtual.DelayedCall(0.5f, () => SceneManager.LoadScene(NextScene));
    }

    public void EndGimmick()
    {
        //- クリック音再生
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);
        //- ギミックの撤退を行う
        board.OutMove();
    }


}