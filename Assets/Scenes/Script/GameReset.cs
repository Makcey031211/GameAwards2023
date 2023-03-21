using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
            //- カウントを進める
            nPushFrameCount++;
            //- UIの操作
            image.fillAmount = ((float)nPushFrameCount) / (RetryTime * 60);
        }
        else
        {
            //- 数値の初期化
            image.fillAmount = 0;
            nPushFrameCount = 0;

        }
        //- 長押しでシーン遷移
        if (nPushFrameCount >= RetryTime * 60)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void OnReset(InputAction.CallbackContext context)
    {
        //- ボタンが押されている間、変数を設定
        if (context.started) { bIsPushButton = true; }
        if (context.canceled) { bIsPushButton = false; }
    }
}
