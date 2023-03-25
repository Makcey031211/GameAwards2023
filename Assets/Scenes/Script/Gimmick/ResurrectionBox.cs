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

    [SerializeField, Header("アニメーション時間(秒)")]
    private float animationTime = 0.1f;

    [SerializeField, Header("アニメーションの遅延時間(秒)")]
    private float animationDelayTime = 0.1f;

    [SerializeField, Header("箱の消滅時間(秒)")]
    private float boxDisTime = 0.1f;

    [SerializeField, Header("生成音SE")]
    private AudioClip generatedSound;

    [SerializeField, Header("消滅音SE")]
    private AudioClip disSound;

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
        {//- 爆発した後
            if (!isOnce)
            { //- 爆発直後
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

        //- アニメーション用の変数
        float elapsed = 0;

        //- 徐々に生成するプレイヤーの数
        int numPlayers = 1;

        //- 生成位置(Z)
        float playerPosZ = 1.5f;

        //- プレイヤーを徐々に生成する
        for (int i = 0; i < numPlayers; i++)
        {
            //- プレイヤーを生成する
            Vector3 spawnPosition = new Vector3(
                transform.position.x, transform.position.y, playerPosZ);
            GameObject player = Instantiate(PlayerPrefab, spawnPosition, Quaternion.identity);

            //- 音の再生
            gameObject.GetComponent<AudioSource>().PlayOneShot(generatedSound);

            //- SceneChangeスクリプトのプレイヤー生存フラグをtrueにする
            sceneChange.bIsLife = true;

            //- 徐々に生成するアニメーション
            while (elapsed < animationTime)
            {
                float t = elapsed / animationTime;
                player.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
                elapsed += Time.deltaTime;
                yield return null;
            }

            //- アニメーションの遅延
            yield return new WaitForSeconds(animationDelayTime);
        }

        //- プレイヤーを生成後、復活箱を徐々に消滅
        float startTime = Time.time;
        Vector3 initialScale = transform.localScale;

        //- 音の再生
        gameObject.GetComponent<AudioSource>().PlayOneShot(disSound);

        //- 復活箱を徐々に消滅させる
        while (Time.time < startTime + boxDisTime)
        {
            float t = (Time.time - startTime) / boxDisTime;
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);
            yield return null;
        }
        Destroy(gameObject);
    }
}