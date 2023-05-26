using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void SetAnime()
    {
        //- ScalePlayerメソッドをdelayTime秒後に呼び出す
        StartCoroutine(ScalePlayer(delayTime));
    }

    private IEnumerator ScalePlayer(float deltaTime)
    {
        //- delayTime秒待機する
        yield return new WaitForSeconds(delayTime);

        float startTime = Time.time; // 開始時間の更新
        Vector3 initialScale = transform.localScale; // 大きさの変数

        //- プレイヤーを徐々に縮めていくアニメーション処理
        while (Time.time < startTime + shrinkTime)
        {
            float t = (Time.time - startTime) / shrinkTime;
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);
            yield return null;
        }
    }
}
