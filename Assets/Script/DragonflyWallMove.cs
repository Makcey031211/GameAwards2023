using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonflyWallMove : MonoBehaviour
{
    [Header("トンボ花火スクリプト"), SerializeField]
    FireworksModule module;

    void Start()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        //--- 後々実装します by牧野
        ////- 新しいベクトル方向
        //Vector2 newdir = new Vector2(0,0);
        //Vector3 direction = new Vector3(module.movedir.x,module.movedir.y,0.0f);
        ////- 「プレイヤーブロッカーの方向」と「トンボの向き」の角度を求める
        //Debug.Log(angle);
    }
}