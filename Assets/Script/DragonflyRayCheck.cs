using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragonflyRayCheck : MonoBehaviour
{
    [Header("花火スクリプトオブジェクト"), SerializeField]
    private GameObject moduleObj;
    private FireworksModule module; //- 花火オブジェクトにアタッチされているスクリプト

    [Header("レイBOXの発生距離(レイBOXの一辺の長さ)"), SerializeField]
    private float RayBoxRadius;

    [Header("当たった判定になるレイの長さ"), SerializeField]
    private float HitRayDistance;

    private Vector2[] RayStartPos;  //- レイの発生座標を格納する配列
    private Vector2[] RayDirection; //- レイの発生方向を格納する配列

    void Start()
    {
        //- 花火スクリプトの取得
        module = moduleObj.GetComponent<FireworksModule>();

        //- レイの発生座標配列を生成
        RayStartPos = new Vector2[5];
        RayStartPos[0] = new Vector2(-1, 1);
        RayStartPos[1] = new Vector2( 1, 1);
        RayStartPos[2] = new Vector2( 1,-1);
        RayStartPos[3] = new Vector2(-1,-1);
        RayStartPos[4] = RayStartPos[0]; //- 最後と最初の座標は同じなので、同じ座標を用意しておく

        //- レイの発生方向配列を生成
        RayDirection = new Vector2[4];
        RayDirection[0] = new Vector2( 0,  1);
        RayDirection[1] = new Vector2( 1,  0);
        RayDirection[2] = new Vector2( 0, -1);
        RayDirection[3] = new Vector2(-1,  0);
    }
    
    void Update()
    {
        //- 自身の座標を取得
        Vector3 MyPos = this.transform.position;

        // === ４つの角から計８本のレイを飛ばす処理 ===
        for (int i = 0; i < 8; i++)
        {
            // === レイ用変数用意部分 === 
            //- レイの発生座標をレイBOXサイズ分だけずらす
            Vector2 PosRadius = new Vector2(RayBoxRadius / 2 * RayStartPos[(i + 1) / 2].x, RayBoxRadius / 2 * RayStartPos[(i + 1) / 2].y);
            //- レイの発生座標を求める
            Vector3 NowPos = new Vector3(MyPos.x + PosRadius.x, MyPos.y + PosRadius.y, 0);
            //- レイの方向
            int DirNum = i / 2; //- レイ方向用の配列番号変数
            Vector3 NowDir = RayDirection[DirNum];

            //- レイを生成
            Ray ray = new Ray(NowPos, NowDir);

            //- 当たり判定のデバッグ表示
            if (Input.GetKey(KeyCode.Alpha1) || true)
            {
                Debug.DrawRay(NowPos, NowDir * 5, Color.red);     //赤色で５秒間可視化
                Debug.DrawRay(NowPos, NowDir * HitRayDistance, Color.blue); //青色で５秒間可視化
            }

            //// もしRayを投射して何らかのコライダーに衝突したら
            //if (Physics.Raycast(ray, out hit))
            //{
            
            //- 貫通するレイを飛ばし、当たったオブジェクトを全て調べる
            foreach (RaycastHit hit in Physics.RaycastAll(ray, HitRayDistance))
            {
                //- レイが当たった距離よりヒット判定距離が長ければ、コンテニュー
                if (hit.distance >= HitRayDistance)          continue;
                //- 当たったオブジェクトのタグが"Stage"でなければ、コンテニュー
                if (hit.collider.gameObject.tag != "Stage")  continue;
                
                // === レイの方向を調べる ===
                //- 上方向 & トンボ花火が上に移動中
                if (DirNum == 0 && module.movedir.y > 0)
                { module.movedir.y *= -1;
                }

                //- 右方向 & トンボ花火が右に移動中
                if (DirNum == 1 && module.movedir.x > 0)
                { module.movedir.x *= -1;
                }

                //- 下方向 & トンボ花火が下に移動中
                if (DirNum == 2 && module.movedir.y < 0)
                { module.movedir.y *= -1;
                }

                //- 左方向 & トンボ花火が左に移動中
                if (DirNum == 3 && module.movedir.x < 0)
                { module.movedir.x *= -1;
                }
            }
        }

        // === 画面外破壊処理 ===

    }
}
