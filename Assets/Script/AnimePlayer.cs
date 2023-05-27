using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 *  制作：髙橋
 *  概要：里内のプレイヤー挙動
 */
public class AnimePlayer : MonoBehaviour
{
    [SerializeField, Header("スケール変更オブジェクト")]
    private GameObject scaleObj;
    [SerializeField, Header("遅延時間(秒)")]
    private float delayTime;
    [SerializeField, Header("縮んでいく時間(秒)")]
    private float shrinkTime;

    //- ボタンが押されているかどうか
    bool bIsPushAnime = false;

    public void SetAnime()
    {
        //- ScalePlayerメソッドをdelayTime秒後に呼び出す
        StartCoroutine(ScalePlayer(delayTime));
    }

    private IEnumerator ScalePlayer(float delayTime)
    {
        //- delayTime秒待機する
        yield return new WaitForSeconds(delayTime);

        //- アニメーションの開始時刻を設定
        float startTime      = Time.time;
        //- 復活箱の初期スケールを設定
        Vector3 initialScale = transform.localScale;

        //=== プレイヤーを徐々に縮めていくアニメーション ===
        //- 指定した秒数分、アニメーションさせる
        while (Time.time < startTime + shrinkTime)
        {
            //- アニメーションの進行度を計算
            float t = (Time.time - startTime) / shrinkTime;
            //- スケールを徐々に変化させる
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);
            //- 次のフレームまで待機
            yield return null;
        }

        //- 縮小アニメーションが終了したらオブジェクトを非表示にする
        scaleObj.SetActive(false);
    }

    /// <summary>
    /// コントローラー取得関数
    /// </summary>
    /// <param name="context"></param>
    public void OnAnime(InputAction.CallbackContext context)
    {
        //- ボタンが押されている間、変数を設定
        if (context.started)  { bIsPushAnime = true; }
        if (context.canceled) { bIsPushAnime = false; }
    }
}