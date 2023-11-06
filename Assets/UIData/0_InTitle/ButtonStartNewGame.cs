using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

/*
 ===================
 制作：髙橋
 追記：井上
 概要：スタートボタン選択時の処理
 ===================
 */
public class ButtonStartNewGame : MonoBehaviour
{
    [SerializeField, Header("シーン遷移先")] private SceneObject NextScene;
    [SerializeField, Header("遅延時間(秒)")] private float DelayTime;
    [SerializeField, Header("フェード秒数")] private float FadeTime;
    [SerializeField, Header("ポップアップ")] private NewGamePopUp popUp;

    //- スクリプト用の変数
    private BGMManager bgmManager;
    private SaveManager saveManager;
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
        if (!isClick && !isSound) return; // falseの時はリターンする

        if (saveManager.GetStageClear(1)) { // ステージ1がクリアされていたら
            // ポップアップ表示
            popUp.PopUpOpen();
        }
        else {
            // ニューゲーム処理
            NewGameSetup();
        }
    }

    public void NewGameSetup()
    {
        //- クリック無効化フラグを設定
        isClick = false;

        //- はじめからを選択時、ゲームデータをリセット
        saveManager.ResetSaveData();

        //- クリック音再生
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);
        isSound = false;

        DOVirtual.DelayedCall(DelayTime, () => GameObject.Find("FadeImage").GetComponent<ObjectFade>().SetFade(ObjectFade.FadeState.In, FadeTime));
        button.PushButtonAnime();
        //- シーンを変える前にBGMを消す
        DOVirtual.DelayedCall(FadeTime, () => bgmManager.DestroyBGMManager()).SetDelay(DelayTime);
        DOVirtual.DelayedCall(FadeTime, () => SceneManager.LoadScene(NextScene)).SetDelay(DelayTime);
    }
}
