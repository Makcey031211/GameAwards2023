using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameReset : MonoBehaviour
{
    //[SerializeField, Header("敵をカウントしているオブジェクト")]
    //private GameObject CountObject;

    [SerializeField, Header("リトライにかかる長押し時間(秒)")]
    private float RetryTime = 1.0f;

    //- ボタンが押されているかどうか
    bool bIsPushButton = false;

    //- フレームカウント
    int nPushFrameCount = 0;

    //- イメージのゲームオブジェクト
    GameObject Object;

    //- イメージのコンポーネント
    Image image;

    // CountEnemyスクリプトを入れる変数
    CountEnemy countEnemy;

    // 敵の数
    int EnemyNum;

    //- リセットが始まったかのフラグ
    bool bIsStartReset = false;

    void Start()
    {
        //- ゲームオブジェクトの検索
        Object = GameObject.Find("ResetGage");
        //- コンポーネントの取得
        image = Object.GetComponent<Image>();
        //- 敵カウントUIの取得
        countEnemy = GameObject.Find("Main Camera").GetComponent<CountEnemy>();
    }
    
    void FixedUpdate()
    {

        // 現在の敵の数を更新
        EnemyNum = countEnemy.GetCurrentCountNum();

        //- 敵が残っており、ボタンが押されている間、処理する
        if (bIsPushButton && EnemyNum > 0)
        {
            nPushFrameCount++; //- カウントを進める
            image.fillAmount = ((float)nPushFrameCount) / (RetryTime * 60); //- UIのリセットゲージの操作
        }
        else
        {
            nPushFrameCount = 0;  // カウントを戻す
            image.fillAmount = 0; // UIのリセットゲージを戻す

        }
        //- 一定時間長押しされたら処理する
        if (nPushFrameCount >= RetryTime * 60)
        {
            if (bIsStartReset == true) return; //- リセット開始フラグがたっていればリターン

            bIsStartReset = true; //シーン開始フラグをたてる
            GameObject.Find("FadeImage").GetComponent<ObjectFade>().SetFade(TweenColorFade.FadeState.In, 0.3f); // フェード開始
            DOVirtual.DelayedCall(0.3f, () => SceneManager.LoadScene(SceneManager.GetActiveScene().name)); // シーンのロード(遅延あり)
        }
    }

    public void OnReset(InputAction.CallbackContext context)
    {
        //- ボタンが押されている間、変数を設定
        if (context.started) { bIsPushButton = true; }
        if (context.canceled) { bIsPushButton = false; }
    }
}
