using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardFireworks : MonoBehaviour
{
    [SerializeField, Header("火花用のオブジェクト")]
    private GameObject particleObject;

    [SerializeField, Header("1回目の爆発後の無敵時間(秒)")]
    private float BlastInvSeconds = 3.0f;

    [SerializeField, Header("無敵時間中の色(RGB)")]
    private Color color;

    //- 無敵時間用のフレームカウンタ
    int nInvFrameCount = 0;

    //- 爆発中かどうか
    bool bIsNowBomb = false;

    //- 花火点火スクリプト
    FireFlower FireflowerScript;

    Rigidbody rb;

    //- 何回爆発したか
    int nBlastCount = 0;

    //- 何回目で爆発するか
    int nBlastNum = 2;

    //- マテリアルの初期の色
    Color initColor;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        FireflowerScript = this.gameObject.GetComponent<FireFlower>();
        //- マテリアルの初期の色の取得
        initColor = this.gameObject.GetComponent<Renderer>().material.color;
    }

    void FixedUpdate()
    {
        //- 無敵時間でない時に爆破が有効になった場合、処理する
        if (FireflowerScript.isExploded && !bIsNowBomb)
        {
            //- 色の変更
            this.gameObject.GetComponent<Renderer>().material.color = color;
            //- 爆発中の判定を設定
            bIsNowBomb = true;
            //- 何回目の爆破かを更新     
            nBlastCount++;
            if (nBlastCount >= nBlastNum)
            {
                //- 無敵時間のリセット
                nInvFrameCount = 0;
                // 当たったオブジェクトのSphereColliderを有効にする
                this.transform.GetChild(0).gameObject.GetComponent<SphereCollider>().enabled = true;
                // 当たったオブジェクトのSphereColliderを有効にする
                this.transform.GetChild(0).gameObject.GetComponent<DetonationCollision>().enabled = true;
                // 指定した位置に生成
                GameObject fire = Instantiate(
                    particleObject,                     // 生成(コピー)する対象
                    transform.position,           // 生成される位置
                    Quaternion.Euler(0.0f, 0.0f, 0.0f)  // 最初にどれだけ回転するか
                    );
                // 爆発時に当たり判定を無効化
                GetComponent<SphereCollider>().isTrigger = true;
                rb.isKinematic = true;
                GetComponent<MeshRenderer>().enabled = false;
            }
        }

        if (bIsNowBomb)
        {
            //- 無敵時間のカウント
            nInvFrameCount++;
            //- 無敵時間終了時の処理
            if (nInvFrameCount >= BlastInvSeconds * 60)
            {
                //- 色の変更
                this.gameObject.GetComponent<Renderer>().material.color = initColor;
                //- まだ爆発していない回数なら処理
                if (nBlastNum > nBlastCount)
                {
                    bIsNowBomb = false;
                    FireflowerScript.isExploded = false;
                }
            }
        }
    }
}