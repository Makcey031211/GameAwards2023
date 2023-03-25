using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastCracker : MonoBehaviour
{
    [SerializeField, Header("火花用のオブジェクト")]
    private GameObject particleObject;

    [SerializeField, Header("クラッカーの範囲表示")]
    private bool bIsDrawArea = true;

    [SerializeField, Header("クラッカーの円部分の線の数")]
    private int nCircleComplementNum = 10;

    [SerializeField, Header("クラッカーの破裂角度範囲(0度〜180度)")]
    private float BrustAngle = 1.0f;

    [SerializeField, Header("クラッカーの破裂射程")]
    private float BrustDis = 10.0f;

    [SerializeField, Header("破裂SE")]
    private AudioClip sound;

    [SerializeField, Header("クラッカーの見た目が残る時間(秒)")]
    private float ModelDeleteTime = 1.0f;

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

    //- 当たり判定表示用の線
    LineRenderer linerend;

    //- 振動用のコンポーネント
    VibrationManager vibration;

    //- パーティクルシステム
    ParticleSystem particle;

    //- 完全にオブジェクトを消去する時間
    float DestroyTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        CrackerTransform = this.gameObject.GetComponent<Transform>();
        FireflowerScript = this.gameObject.GetComponent<FireFlower>();
        //- 線の追加
        linerend = gameObject.AddComponent<LineRenderer>();
        //- 振動コンポーネントの取得
        vibration = GameObject.Find("VibrationManager").GetComponent<VibrationManager>();
        //- パーティクルの取得
        particle = particleObject.transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    void FixedUpdate()
    {
        //- 破裂時の当たり判定の表示
        if (bIsDrawArea)
        {
            //- 点の配列を生成
            Vector3[] positions = new Vector3[nCircleComplementNum + 2];
            //- 始点の生成
            positions[0] = this.transform.position;

            //- 円部分の点の生成
            for (int i = 0; i < nCircleComplementNum + 1; i++)
            {
                //- 中心から円部分へのレイを生成(少しずつ回転させる)
                var CircleRay = Quaternion.Euler(0, 0, (-BrustAngle / 2) + (BrustAngle / nCircleComplementNum * i)) * CrackerTransform.up.normalized;
                //- 中心座標をレイ方向へ進める
                var LineTransform = this.transform.position + (CircleRay * BrustDis);
                //- 点を追加
                positions[i + 1] = LineTransform;
            }

            // 点の数を指定する
            linerend.positionCount = positions.Length;
            // 線を引く場所を指定する
            linerend.SetPositions(positions);
            //- 幅と色の決定
            linerend.startWidth = 0.1f;
            linerend.endWidth = 0.1f;
            //- 始点と終点をつなぐ
            linerend.loop = true;
        }
        //- 1フレーム前の、爆破依頼変数を更新
        bIsOldExploded = FireflowerScript.isExploded;
        
        //- 弾けるタイミングになるまでは、以下の爆破処理を行わない
        if (!FireflowerScript.isExploded) return;
        
        //- このif文の中身は一度だけ呼ばれる。
        if (!bIsBomb)
        {
            //- クラッカーのエフェクト生成
            GameObject fire = Instantiate(
                particleObject,                     // 生成(コピー)する対象
                transform.position,           // 生成される位置
                Quaternion.Euler(0.0f, 0.0f, transform.localEulerAngles.z)  // 最初にどれだけ回転するか
                );

            //- 振動の設定
            vibration.SetVibration(60, 1.0f);
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

                // 自身から花火に向かうレイを作成
                Ray ray = new Ray(transform.position, FireworkDir);
                {
                    // レイが当たったオブジェクトの情報を入れる変数
                    RaycastHit hit;
                    // レイがステージに当たったかフラグ
                    bool StageHit = false;
                    //- レイを飛ばす
                    if (Physics.Raycast(ray, out hit))
                    {
                        //- ステージに当たった場合処理しない
                        if (hit.collider.gameObject.tag == "Stage") continue;
                    }
                }
                //- 「花火へのベクトル」と「クラッカーの向きベクトル」の角度を求める
                var angle = Vector3.Angle((CrackerTransform.up).normalized, (FireworkDir).normalized);
                if (angle != 0 && (angle < BrustAngle / 2))
                {
                    float DisDelayRatio = (dis) / (BrustDis *  particle.main.startSpeed.constantMin / 25) / 1.8f;
                    float DelayTime = (10 / particle.main.startSpeed.constantMin / 25) + DisDelayRatio;
                    //- 遅延をかけて爆破
                    StartCoroutine(DelayDestroy(obj, DelayTime));
                    continue;
                }

                //- 遅延をかけて見た目のモデルを消す
                StartCoroutine(DelayDeleteModel(this.transform.GetChild(0).gameObject, ModelDeleteTime));
            }
            
            //- 自身を破壊する
            Destroy(this.gameObject, DestroyTime);
        }
    }

    //- 遅れて起爆する関数
    private IEnumerator DelayDestroy(GameObject Obj,float delayTime)
    {
        //- delayTime秒待機する
        yield return new WaitForSeconds(delayTime);
        //- 起爆判定を有効にする
        Obj.gameObject.GetComponent<FireFlower>().isExploded = true;
    }

    //- 遅れて見た目を消す関数
    private IEnumerator DelayDeleteModel(GameObject Obj, float delayTime)
    {
        //- delayTime秒待機する
        yield return new WaitForSeconds(delayTime);
        //- 見た目を消す
        Obj.SetActive(false);
    }
}
