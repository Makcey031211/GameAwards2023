using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetonationCollision : MonoBehaviour
{
    [Header("当たり判定の広がり具合"), SerializeField]
    private float power = 3;

    [Header("一回目の当たり判定の最大値"), SerializeField]
    private float MaxFirColSize = 7.5f;

    [Header("二回目の当たり判定の最大値"), SerializeField]
    private float MaxSecColSize = 7.5f;

    [Header("削除までの時間"), SerializeField]
    private float time = 3.0f;

    [Header("当たり判定の初期サイズ"), SerializeField]
    private Vector3 ColSize = new Vector3(1.0f,1.0f,1.0f);

    //- 生成からの経過時間
    float currentTime;

    //- 2回爆発するかどうか(DoubleBlastFireworks.csからアクセスされて変更される変数です)
    private bool _isDoubleBlast = false;
    public bool IsDoubleBlast { get { return _isDoubleBlast; } set { _isDoubleBlast = value; } }

    //- 何回目の爆発か
    int nBlastCount = 1;

    void Start()
    {
        currentTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //----- 削除までの時間カウント
        currentTime += Time.deltaTime;
        if (currentTime >= time)
        {
            //- 2回以上爆発する花火ではない、または
            //- 2回目の爆発の場合、処理する
            if (!_isDoubleBlast || (nBlastCount >= 2))
            {
                // 親オブジェクトごと削除
                Destroy(transform.parent.gameObject);
            }
            else
            {
                //- 当たり判定サイズを元に戻す
                transform.localScale = ColSize;
                //- 時間のリセット
                currentTime = 0.0f;
                //- 自身のスクリプトを無効化
                this.gameObject.GetComponent<DetonationCollision>().enabled = false;
            }
            nBlastCount++;
        }
    }

    private void FixedUpdate()
    {
        float collSize = power * 0.0004f;
        //- 2回以上爆発する花火ではない、または
        //- 1回目の爆発の場合
        if (!_isDoubleBlast || (nBlastCount < 2)) {
            if (transform.localScale.x <= MaxFirColSize) {
                transform.localScale += new Vector3(collSize, collSize, collSize);
            }
        }
        else {
            if (transform.localScale.x <= MaxSecColSize) {
                transform.localScale += new Vector3(collSize, collSize, collSize);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 当たったオブジェクトのタグが「花火] なら
        if (other.gameObject.tag == "Fireworks")
        {
            // 自身から花火に向かう方向を計算
            Vector3 direction = other.gameObject.transform.position - transform.position;
            // 自身から花火に向かうレイを作成
            Ray ray = new Ray(transform.position, direction);
            // レイが当たったオブジェクトの情報を入れる変数
            RaycastHit hit;
            // レイがステージに当たったかフラグ
            bool StageHit = false;
            if (Physics.Raycast(ray, out hit))
            {
                // レイが当たったオブジェクトのタグが「Stage」ならリターン
                if (hit.collider.gameObject.tag == "Stage") return;

                //- 当たったオブジェクトのFireworksModuleの取得
                FireworksModule otherModule = other.gameObject.GetComponent<FireworksModule>();
                //- 当たったオブジェクトの花火タイプによって処理を分岐
                if (otherModule.Type == FireworksModule.FireworksType.Boss)
                    other.gameObject.GetComponent<FireworksModule>().IgnitionBoss(other.gameObject);
                else
                    other.gameObject.GetComponent<FireworksModule>().Ignition();
            }
        }
    }
}
