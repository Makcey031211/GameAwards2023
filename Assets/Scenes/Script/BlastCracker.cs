using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastCracker : MonoBehaviour
{
    [SerializeField, Header("火花用のオブジェクト")]
    private GameObject particleObject;

    [SerializeField, Header("クラッカーの範囲表示")]
    private bool bIsDrawArea = true;

    [SerializeField, Header("クラッカーの破裂角度範囲(0度～180度)")]
    private float BrustAngle = 1.0f;

    [SerializeField, Header("クラッカーの破裂射程")]
    private float BrustDis = 10.0f;

    [SerializeField, Header("破裂の引火の遅延(秒)")]
    private float ShotHitSecond = 1.0f;

    [SerializeField, Header("破裂後クラッカーの見た目が残り続ける時間(秒)")]
    private float DestroyTime = 1.0f;

    [SerializeField, Header("破裂SE")]
    private AudioClip sound;

    //- クラッカーが破裂する方向
    Transform CrackerTransform;

    //- 花火点火スクリプト
    FireFlower FireflowerScript;

    //- 1フレーム前の爆破確認変数
    bool bIsOldExploded = false;

    //- 弾けるタイミングの遅延(フレーム)
    private float ShotHitFrame;

    //- 引火時フレームカウント
    int HitFrameCount = 0;

    //- 爆破時の弾け、生成の処理を行ったかどうか
    bool bIsBomb = false;

    // Start is called before the first frame update
    void Start()
    {
        CrackerTransform = this.gameObject.GetComponent<Transform>();
        FireflowerScript = this.gameObject.GetComponent<FireFlower>();
        ShotHitFrame = ShotHitSecond * 60;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //- 破裂時の当たり判定の表示
        if (bIsDrawArea)
        {
            //- 左右それぞれのレイの生成
            var RightRay = Quaternion.Euler(0, BrustAngle / 2, 0) * CrackerTransform.forward.normalized;
            var LeftRay = Quaternion.Euler(0, -BrustAngle / 2, 0) * CrackerTransform.forward.normalized;
            //- それぞれのレイの表示
            Debug.DrawRay(CrackerTransform.transform.position, RightRay * BrustDis, Color.red);
            Debug.DrawRay(CrackerTransform.transform.position, LeftRay * BrustDis, Color.red);
        }
        //- 1フレーム前の、爆破依頼変数を更新
        bIsOldExploded = FireflowerScript.isExploded;


        //- 弾けるタイミングになるまでは、以下の爆破処理を行わない
        if (!FireflowerScript.isExploded) return;

        //- 爆破依頼を受けた後、弾ける時の遅延処理
        if (HitFrameCount < ShotHitFrame)
        {
            HitFrameCount++;
            return;
        }

        //- このif文の中身は一度だけ呼ばれる。
        if (!bIsBomb)
        {
            //- 音の再生
            gameObject.GetComponent<AudioSource>().PlayOneShot(sound);
            //- 弾けるときの処理判定変数を設定
            bIsBomb = true;
            //- タグが花火のオブジェクトを全て取得
            GameObject[] Fireworks = GameObject.FindGameObjectsWithTag("Fireworks");
            // 原点からクラッカーへのベクトル
            Vector3 origin = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            //- 花火のオブジェクトを一つずつ実行
            foreach (var obj in Fireworks)
            {
                //- 原点から花火へのベクトル
                Vector3 direction = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
                //- クラッカーから花火へのベクトル
                Vector3 FireworkDir = direction - origin;
                //- 花火との距離を取得
                float dis = Vector3.Distance(origin, direction);
                //- 花火との距離が射程内じゃなかったら処理しない
                if (dis > BrustDis) continue;

                //- 「花火へのベクトル」と「クラッカーの向きベクトル」の角度を求める
                var angle = Vector3.Angle((CrackerTransform.forward).normalized, (FireworkDir).normalized);
                if (angle != 0 && (angle < BrustAngle / 2))
                {
                    obj.gameObject.GetComponent<FireFlower>().isExploded = true;
                    continue;
                }
            }

            for (int i = 0; i < 30; i++)
            {
                //- 吹っ飛ぶ力を生成
                Vector3 ForceRay = Quaternion.Euler(0, Random.Range(-20, 21), 0) * CrackerTransform.forward * Random.Range(200,1500);
                // 指定した位置に生成
                GameObject fire = Instantiate(
                    particleObject,                     // 生成(コピー)する対象
                    transform.position,           // 生成される位置
                    Quaternion.Euler(0.0f, 0.0f, 0.0f)  // 最初にどれだけ回転するか
                    );
                //- 紙吹雪に吹っ飛ぶ力を与える
                fire.GetComponent<Rigidbody>().AddForce(ForceRay);
            }
            
            //- 自身を破壊する
            Destroy(this.gameObject, DestroyTime);
        }
    }
}
