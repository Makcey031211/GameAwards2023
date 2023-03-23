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

    [SerializeField, Header("生成音SE")]
    private AudioClip sound;

    //- 花火点火スクリプト
    FireFlower FireflowerScript;

    //- 生成までの待ち時間(秒)
    float delayTime;

    //- 処理を一回だけ行う
    bool isOnce;

    private GameObject CameraObject;
    SceneChange sceneChange;

    private void Start()
    {
        delayTime = delayTimer;
        FireflowerScript = this.gameObject.GetComponent<FireFlower>();
        CameraObject = GameObject.Find("Main Camera");
        sceneChange = CameraObject.GetComponent<SceneChange>();
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
        //- 音の再生
        gameObject.GetComponent<AudioSource>().PlayOneShot(sound);
        //- SceneChangeスクリプトのプレイヤー生存フラグをtrueにする
        sceneChange.bIsLife = true;
        //- プレイヤーを生成後、復活箱を削除
        Destroy(gameObject);
    }
}