using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BackScene : MonoBehaviour
{
    [SerializeField, Header("オプション操作にかかる時間(秒)")]
    private float OptionTime = 1.0f;
    [SerializeField, Header("シーン移動時のフェード時間(秒)")]
    private float FadeTime   = 1.0f;
    [SerializeField, Header("シーン移動時のBGM切り替え時間(秒)")]
    private float disBGMTime = 1.0f;
    [SerializeField, Header("戻る先のシーンを設定")]
    private SceneObject backScene;

    //- スクリプト用の変数
    BGMManager bgmManager;
    //- イメージのコンポーネント
    Image imageInGame;

    //- ボタンが押されているかどうか
    bool bIsPushBack = false;

    //- ボタンが押された時間
    float bPushTimeBack = 0;

    //- シーン移動が始まったかどうかのフラグ
    bool bIsStartInGame = false;

    private void Start()
    {
        //- コンポーネントの取得
        imageInGame = GameObject.Find("InGameGage").GetComponent<Image>();
        bgmManager  = GameObject.Find("BGMManager").GetComponent<BGMManager>();
    }

    private void FixedUpdate()
    {
        //- シーン移動が始まったかどうかのフラグを取得
        bool bIsMoveScene = false;

        //- シーン移動のフラグ更新 
        if (bIsStartInGame) bIsMoveScene = true;

        //＝＝＝＝＝　戻るボタン　＝＝＝＝＝
        //- 「シーン移動ボタンが押されてる」かつ「シーン移動が始まっていない」時
        if (bIsPushBack && !bIsMoveScene)
        {
            bPushTimeBack += Time.deltaTime;                     // プッシュ時間の更新
            imageInGame.fillAmount = bPushTimeBack / OptionTime; // ゲージの更新
        }
        else if (!bIsMoveScene)
        {
            bPushTimeBack = 0;          // プッシュ時間のリセット
            imageInGame.fillAmount = 0; // ゲージのリセット
        }
        //- 一定時間長押しされたら処理する
        if (bPushTimeBack >= OptionTime)
        {
            if (bIsStartInGame == true) return; // リセット開始フラグがたっていればリターン
            bIsStartInGame = true; // シーン開始フラグをたてる
            SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click); // クリック音再生
            GameObject.Find("ColorFadeImage").GetComponent<ObjectFade>().SetFade(ObjectFade.FadeState.In, FadeTime); // フェード開始
            DOVirtual.DelayedCall(disBGMTime, () => bgmManager.DestroyBGMManager());  // シーンを変える前にBGMを消す
            DOVirtual.DelayedCall(FadeTime, () => SceneManager.LoadScene(backScene)); // シーンのロード(遅延あり)
        }
    }

    /// <summary>
    /// コントローラーを取得する関数
    /// </summary>
    /// <param name="context"></param>
    public void OnInBack(InputAction.CallbackContext context)
    {
        //- ボタンが押されている間、変数を設定
        if (context.started) { bIsPushBack = true; }
        if (context.canceled) { bIsPushBack = false; }
    }
}