using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DetonationCollision : MonoBehaviour
{
    [Header("当たり判定の広がり具合"), SerializeField]
    private float power = 3;

    [Header("一回目の当たり判定の最大値"), SerializeField]
    private float MaxFirColSize = 7.5f;

    [Header("二回目の当たり判定の最大値"), SerializeField]
    private float MaxSecColSize = 7.5f;

    [Header("当たり判定の初期サイズ"), SerializeField]
    private Vector3 ColSize = new Vector3(1.0f,1.0f,1.0f);

    //- 2回爆発するかどうか(DoubleBlastFireworks.csからアクセスされて変更される変数です)
    private bool _isDoubleBlast = false;
    public bool IsDoubleBlast { get { return _isDoubleBlast; } set { _isDoubleBlast = value; } }

    //- 何回目の爆発か
    int nBlastCount = 1;

    void Start()
    {
        //- 座標の取得
        Vector3 pos = transform.position;
    }
    
    public void EndDetonation()
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
            //- 自身のスクリプトを無効化
            this.gameObject.GetComponent<DetonationCollision>().enabled = false;
        }
        nBlastCount++;
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
        if (other.gameObject.tag != "Fireworks") return;              // 当たったオブジェクトのタグが「花火] 以外ならリターン

        CheckHitRayStage(other.gameObject);
    }

    //- 花火玉に当たった時にステージオブジェクトに阻まれてないかどうか調べる関数
    private void CheckHitRayStage(GameObject obj)
    {
        // 自身から花火に向かう方向を計算
        Vector3 direction = obj.transform.position - transform.position;
        // 自身と花火の長さを計算
        float DisLength = direction.magnitude;
        // 自身から花火に向かうレイを作成
        Ray ray = new Ray(transform.position, direction);
        // 当たったオブジェクトを格納するための変数
        var HitList = new List<RaycastHit>(); 

        // レイが当たったオブジェクトをすべて順番に確認していく
        foreach (RaycastHit hit in Physics.RaycastAll(ray, DisLength))
        {
            //- 最初のオブジェクトなら無条件で格納
            if (HitList.Count == 0)
            {
                HitList.Add(hit);
                continue;
            }
            
            //- 格納フラグ
            bool bAdd = false;
            //- 格納変数と当たったオブジェクトの比較
            for (int i = 0; i < HitList.Count; i++)
            {
                //- 格納フラグチェック
                if (bAdd) break; 
                //- 距離が格納箇所データの距離より長ければリターン
                if (HitList[i].distance < hit.distance) continue;
                //- 仮のデータを一番最後に格納
                HitList.Add(new RaycastHit());
                //- 最後から格納場所までデータをずらす
                for (int j = HitList.Count - 1; j > i; j--)
                {
                    //- データを一つ移動
                    HitList[j] = HitList[j - 1];
                }
                //- 格納場所に格納
                HitList[i] = hit;
                bAdd = true;
            }

            //- 格納フラグが立っていなければ、一番距離が長いオブジェクトなので
            //- 配列の一番最後に格納する
            if (!bAdd) HitList.Add(hit);
        }

        //- 距離が短いものから調べる
        for (int i = 0; i < HitList.Count; i++)
        {
            RaycastHit hit = HitList[i];

            //- 当たり判定のデバッグ表示
            if (Input.GetKey(KeyCode.Alpha1))
            {
                float markdis = 0.1f;
                Debug.DrawRay(transform.position, direction, Color.red, 3.0f);
                Debug.DrawRay(hit.point, new Vector3(+markdis, +markdis, 0), Color.blue, 3.0f);
                Debug.DrawRay(hit.point, new Vector3(+markdis, -markdis, 0), Color.blue, 3.0f);
                Debug.DrawRay(hit.point, new Vector3(-markdis, +markdis, 0), Color.blue, 3.0f);
                Debug.DrawRay(hit.point, new Vector3(-markdis, -markdis, 0), Color.blue, 3.0f);
            }
            if (hit.collider.gameObject.tag != "Stage") continue; //- ステージオブジェクト以外なら次へ
            if (hit.distance > DisLength) continue;               //- 花火玉よりステージオブジェクトが奥にあれば次へ

            //- 当たった花火玉より手前にステージオブジェクトが存在する
            return; //- 処理を終了
        }
        
        //- 当たったオブジェクトのFireworksModuleの取得
        FireworksModule module = obj.GetComponent<FireworksModule>();
        //- 当たったオブジェクトの花火タイプによって処理を分岐
        if (module.Type == FireworksModule.FireworksType.Boss)
            module.IgnitionBoss(obj);
        else if(module.Type != FireworksModule.FireworksType.ResurrectionPlayer)
            module.Ignition(transform.position);
        else if (module.Type == FireworksModule.FireworksType.ResurrectionPlayer)
            if(module.GetIsInv() == false)
            { module.Ignition(transform.position); }

        //- ステージオブジェクトに当たっていない
        return;
    }
}
