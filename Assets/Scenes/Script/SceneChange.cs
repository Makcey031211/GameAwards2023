using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField, Header("シーン遷移先")]
    private SceneObject NextScene;

    [SerializeField, Header("敵をカウントしているUI")]
    private GameObject UIObject;

    [SerializeField, Header("クリア時のシーン遷移を遅らす時間(秒)")]
    private float ClearDelayTime = 2.0f;

    [SerializeField, Header("リトライ時のシーン遷移を遅らす時間(秒)")]
    private float RetryDelayTime = 2.0f;

    CountEnemy countEnemy;          // CountEnemyスクリプトを入れる変数

    private int EnemyNum;           // 敵の数
    private float CurrentTime;      // 現在の時間(敵が全滅してからカウント開始)
    private float CurrentParticleTime = 0.0f;   // パーティクルの現在の時間
    private float TotalParticleTime = 999.0f;   // パーティクルの総時間

    public static bool bIsChange;   // 次のシーンに移動するかのフラグ
    public static bool bIsRetry;    // リトライするかのフラグ
    public bool bIsLife;     // プレイヤーが生存しているか

    // Start is called before the first frame update
    void Start()
    {
        bIsChange = false;
        bIsRetry = false;
        bIsLife = true;
        countEnemy = UIObject.GetComponent<CountEnemy>();
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
            // 現在の時間が遅延時間を超えたらシーン遷移フラグをtrueに変える
            if (CurrentTime >= ClearDelayTime)
            { bIsChange = true; }
        }

        // パーティクルの再生が終わる + 敵が残っている
        if (CurrentParticleTime == TotalParticleTime && EnemyNum > 0 && !bIsLife)
        {
            // 現在の時間を更新
            CurrentTime += Time.deltaTime;
            // 現在の時間が遅延時間を超えたらリトライフラグをtrueに変える
            if (CurrentTime >= RetryDelayTime)
            { bIsRetry = true; }
        }

        // シーン遷移フラグがtrueなら次のシーンに移動
        if (bIsChange)
        { 
        //    SceneManager.LoadScene(NextScene); 
        }

        // リトライフラグがtrueなら現在のシーンを再読み込み
        if (bIsRetry)
        { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    }

    public void SetParticleTime(float currentTime, float totalTime)
    {
        // パーティクルの現在の時間を代入
        CurrentParticleTime = currentTime;
        // パーティクルの総時間を代入
        TotalParticleTime = totalTime;
    }
}
