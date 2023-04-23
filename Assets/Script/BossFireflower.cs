using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireflower : MonoBehaviour
{
    [Header("爆発に必要な回数"), SerializeField]
    private int nIgnitionMax = 3;

    private int nIgnitionCount = 0; // 引火した回数

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("HITBOSS");
        //===　タグを調べる　===
        // 当たったオブジェクトのタグが「花火」「打った花火」以外ならリターン
        if (other.gameObject.tag != "Fireworks" &&
            other.gameObject.tag != "ShotFireworks") return;

        //===　ブロックに遮られてないかチェック　===
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
        }

    }
}