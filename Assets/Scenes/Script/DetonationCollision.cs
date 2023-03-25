using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetonationCollision : MonoBehaviour
{
    [Header("当たり判定の広がり具合"), SerializeField]
    private float power = 3;

    [Header("当たり判定の最大値"), SerializeField]
    private float MaxColSize = 7.5f;

    [Header("削除までの時間"), SerializeField]
    private float time = 3.0f;

    [Header("当たり判定の初期サイズ"), SerializeField]
    private Vector3 ColSize = new Vector3(1.0f,1.0f,1.0f);

    //- 生成からの経過時間
    float currentTime;

    //- 2回爆発するかどうか(DoubleBlastFireworks.csからアクセスされて変更される変数です)
    public bool bIsDoubleBlast = false;

    //- 何回目の爆発か
    int nBlastCount = 0;

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
            nBlastCount++;
            //- 2回以上爆発する花火ではない、または
            //- 2回目の爆発の場合、処理する
            if (!bIsDoubleBlast || (nBlastCount >= 2))
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
        }
    }

    private void FixedUpdate()
    {
        float collSize = power * 0.0004f;
        if (transform.localScale.x <= MaxColSize)
        {
            transform.localScale += new Vector3(collSize, collSize, collSize);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 当たったオブジェクトのタグが「花火」 又は 「打った花火」なら
        if (other.gameObject.tag == "Fireworks" ||
            other.gameObject.tag == "ShotFireworks")
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
                // レイが当たったオブジェクトのタグが「Stage」なら、フラグをtrueに変える
                if (hit.collider.gameObject.tag == "Stage")
                { StageHit = true; }

                // レイがステージと当たっていなかったら
                if (StageHit == false)
                {
                    // 当たったオブジェクトのFireFlowerスクリプト内のisExplodedをtrueに変える
                    other.gameObject.GetComponent<FireFlower>().isExploded = true;
                }
            }
        }
    }
}
