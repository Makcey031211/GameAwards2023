using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自爆後、復活箱からプレイヤーを再び生成する
/// </summary>
public class ResurrectionBox : MonoBehaviour
{
    [SerializeField, Header("生成するオブジェクト")]
    private GameObject PlayerPrefab;

    [SerializeField, Header("生成までの待ち時間(秒)")]
    private float delayTimer = 0.1f;

    FireFlower FireflowerScript; //- 花火点火スクリプト

    float delayTime; //- 生成までの待ち時間(秒)

    bool isOnce; //- 処理を一回だけ行う

    private void Start()
    {
        delayTime = delayTimer;
        FireflowerScript = this.gameObject.GetComponent<FireFlower>();
    }

    private void Update()
    {
        if (FireflowerScript.isExploded)
        {//爆発した後
            if (!isOnce)
            { // 爆発直後
                isOnce = true;
                //- SpawnPlayerメソッドをdelayTime秒後に呼び出す
                StartCoroutine(SpawnPlayer(delayTime));
            }
        }
    }

    private IEnumerator SpawnPlayer(float delayTime)
    {
        //- delayTime秒待機する
        yield return new WaitForSeconds(delayTime);
        //- プレイヤーを生成する
        Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
        //- プレイヤーを生成後、復活箱を削除
        Destroy(gameObject);
    }
}