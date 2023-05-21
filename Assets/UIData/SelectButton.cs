/*
 ===================
 基盤制作：大川
 追記者：井上・牧野・辻・��橋・寺前
 ボタンを選択した際に動作するスクリプト
 ===================
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

//- ボタン選択時に動作するクラス
public class SelectButton : MonoBehaviour
{
    [SerializeField, Header("シーン遷移先")] private SceneObject NextScene;          //シーン遷移先
    [SerializeField, Header("フェード開始遅延時間")] private float DelayTime;         //フェードが呼び出されるまでの遅延時間
    [SerializeField, Header("フェードオブジェクト")] private GameObject fadeObject;   //フェード用オブジェクト
    [SerializeField, Header("フェード完了までの時間")] private float FadeTime;        //フェード完了時間
    
    private BGMManager bgmManager;
    private Button button;
    private ButtonAnime buttonAnime;
    private SelectMovePlayer SelectPlayer;
    private bool Load = false;                      //多重ロード抑止

    void Awake()
    {
        buttonAnime = GetComponent<ButtonAnime>();
        button = GetComponent<Button>();
        bgmManager  = GameObject.Find("BGMManager").GetComponent<BGMManager>();
    }
    
    /// <summary>
    /// シーン遷移を行う処理
    /// タイトル・里選択・インゲーム
    /// </summary>
    public void MoveScene()
    {
        //- 呼び出し済なら処理しない
        if (Load)
        { return; }

        //- 呼び出し済にする
        Load = true;

        //- 呼び出されたら上下左右選択を無効化
        Navigation NoneNavigation = button.navigation;
        NoneNavigation.selectOnUp = null;
        NoneNavigation.selectOnDown = null;
        NoneNavigation.selectOnLeft = null;
        NoneNavigation.selectOnRight = null;
        button.navigation = NoneNavigation;
        
        //- インスタンスを変更
        button.interactable = false;

        //- クリック音再生
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);

        //- 演出の描画フラグをリセット
        if (CutIn.MoveCompleat) { CutIn.ResetMoveComplete(); }
        if (BoardMove.MoveComplete) { BoardMove.ResetMoveComplete(); }
        if (OpeningAnime.MoveCompleat) { OpeningAnime.ResetMoveComplete(); }

        //- ボタンアニメが設定されていたら行う
        buttonAnime.PushButtonAnime();

        //- 遅延後の処理
        DOVirtual.DelayedCall(DelayTime, ()=> fadeObject.GetComponent<ObjectFade>().SetFade(ObjectFade.FadeState.In, FadeTime));
        DOVirtual.DelayedCall (FadeTime, ()=> bgmManager.DestroyBGMManager()).SetDelay(DelayTime); 
        DOVirtual.DelayedCall (FadeTime, ()=> SceneManager.LoadScene(NextScene)).SetDelay(DelayTime);
    }

    /// <summary>
    /// シーン遷移を行う処理
    /// 会場選択
    /// </summary>
    public void MoveSelectScene()
    {
        //- 呼び出し済なら行わない
        if (Load)
        { return; }

        //- 呼び出し済にする
        Load = true;

        //- 呼び出されたら上下左右選択を無効化
        Navigation NoneNavigation = button.navigation;
        NoneNavigation.selectOnUp = null;
        NoneNavigation.selectOnDown = null;
        NoneNavigation.selectOnLeft = null;
        NoneNavigation.selectOnRight = null;
        button.navigation = NoneNavigation;

        //- インスタンスを変更
        button.interactable = false;

        //- 演出の描画フラグをリセット
        if (CutIn.MoveCompleat) { CutIn.ResetMoveComplete(); }
        if (BoardMove.MoveComplete) { BoardMove.ResetMoveComplete(); }
        if (OpeningAnime.MoveCompleat) { OpeningAnime.ResetMoveComplete(); }
        
        //- クリック音再生
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);
        
        //- シーン遷移用アニメーションを再生する
        SelectPlayer = GetComponent<SelectMovePlayer>();
        SelectPlayer.InStageMove();
        
        //- 遅延後の処理
        DOVirtual.DelayedCall(DelayTime, () => fadeObject.GetComponent<ObjectFade>().SetFade(ObjectFade.FadeState.In, FadeTime));
        DOVirtual.DelayedCall(FadeTime, () => bgmManager.DestroyBGMManager()).SetDelay(DelayTime);
        DOVirtual.DelayedCall(FadeTime, () => SceneManager.LoadScene(NextScene)).SetDelay(DelayTime);

    }
}