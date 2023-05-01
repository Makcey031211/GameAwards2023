using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField, Header("シーン遷移先")]
    private SceneObject NextScene;

    //[SerializeField, Header("敵をカウントしているオブジェクト")]
    //private GameObject CountObject;

    [SerializeField, Header("クリア時のシーン遷移を遅らす時間(秒)")]
    private float ClearDelayTime = 2.0f;

    [SerializeField, Header("リトライ時のサウンドが発生する時間(秒)")]
    private float SoundDelayTime = 2.0f;

    [SerializeField, Header("リトライ時のシーン遷移を遅らす時間(秒)")]
    private float RetryDelayTime = 2.0f;

    CountEnemy countEnemy;          // CountEnemyスクリプトを入れる変数

    private int EnemyNum;           // 敵の数
    private float CurrentTime;      // 現在の時間(敵が全滅してからカウント開始)
    private float CurrentParticleTime = 0.0f;   // パーティクルの現在の時間
    private float TotalParticleTime = 999.0f;   // パーティクルの総時間
    private bool bIsShotSound = false;          // 音が再生されたかどうか
    private ObjectFade fade;              // フェード用のスプライト
    private ClearManager clear;

    public static bool bIsChange;   // 次のシーンに移動するかのフラグ
    public static bool bIsRetry;    // リトライするかのフラグ
    public bool bIsLife;     // プレイヤーが生存しているか

    // Start is called before the first frame update
    void Start()
    {
        bIsChange = false;
        bIsRetry = false;
        bIsLife = true;
        countEnemy = this.gameObject.GetComponent<CountEnemy>();
        fade = GameObject.Find("FadeImage").GetComponent<ObjectFade>();
        clear = GameObject.Find("ClearManager").GetComponent<ClearManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // 現在の敵の数を更新
        EnemyNum = countEnemy.GetCurrentCountNum();

        // パーティクルの再生が終わる + 敵を全滅させたら
        if(CurrentParticleTime == TotalParticleTime && EnemyNum <= 0)
        {
            // 現在の時間を更新
            CurrentTime += Time.deltaTime;
            // 現在の時間が遅延時間を超えたらシーン遷移フラグをtrueに変えて、音を再生する
            if (CurrentTime >= ClearDelayTime)
            {
                bIsChange = true;
            }
        }

        // パーティクルの再生が終わる + 敵が残っている
        if (CurrentParticleTime == TotalParticleTime && EnemyNum > 0 && !bIsLife)
        {
            // 現在の時間を更新
            CurrentTime += Time.deltaTime;
            // 現在の時間が遅延時間を超えたらリトライフラグをtrueに変える
            if (CurrentTime >= RetryDelayTime)
            {
                CurrentTime = 0;
                bIsRetry = true;
            }
        }

        // パーティクルが再生中なら現在時間をリセット
        if (CurrentParticleTime != TotalParticleTime)
        {
            CurrentTime = 0;
        }

        // シーン遷移フラグがtrueならクリア情報を書き込む
        if (bIsChange)
        {
            clear.WriteClear();
        }

        // リトライフラグがtrueなら現在のシーンを再読み込み
        if (bIsRetry)
        {
            // 現在の時間を更新
            CurrentTime += Time.deltaTime;
            //- 1度だけ再生
            if (!bIsShotSound)
            {
                //- 変数の設定
                bIsShotSound = true;
                //- 失敗音の再生
                SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Failure);
                // フェードの設定
                fade.SetFade(TweenColorFade.FadeState.In, 1.0f);
            }
            if (!fade.isFade) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void SetParticleTime(float currentTime, float totalTime)
    {
        // パーティクルの現在の時間を代入
        CurrentParticleTime = currentTime;
        // パーティクルの総時間を代入
        TotalParticleTime = totalTime;
    }

    public void ResetCurrentTime()
    {
        CurrentTime = 0;
    }
}