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

    [Header("画面外離脱時、消去するオブジェクト"), SerializeField]
    private GameObject DestroyObject;

    private Vector2[] RayStartPos;  //- レイの発生座標を格納する配列
    private Vector2[] RayDirection; //- レイの発生方向を格納する配列
    private Vector2 SaveMoveDir;    //- 元々の移動方向を保存しておくための変数

    private int HitDirPlayerBlock = -1; 　    //- 侵入不可テープが当たった方向
    private bool IsHitPlayerBlock = false;    //- 侵入不可テープの接触判定フラグ
    private bool CheckHitPlayerBlock = false; //- 侵入不可テープから離れた瞬間を調べるためのフラグ

    void Start()
    {
        //- 花火スクリプトの取得
        module = moduleObj.GetComponent<FireworksModule>();

        //- レイの発生座標配列を生成
        RayStartPos = new Vector2[5];
        RayStartPos[0] = new Vector2(-1, 1);
        RayStartPos[1] = new Vector2(1, 1);
        RayStartPos[2] = new Vector2(1, -1);
        RayStartPos[3] = new Vector2(-1, -1);
        RayStartPos[4] = RayStartPos[0]; //- 最後と最初の座標は同じなので、同じ座標を用意しておく

        //- レイの発生方向配列を生成
        RayDirection = new Vector2[4];
        RayDirection[0] = new Vector2(0, 1);
        RayDirection[1] = new Vector2(1, 0);
        RayDirection[2] = new Vector2(0, -1);
        RayDirection[3] = new Vector2(-1, 0);
    }

    void Update()
    {
        //- 侵入不可テープと離れるタイミングを調べるための変数
        CheckHitPlayerBlock = false;

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
            if (Input.GetKey(KeyCode.Alpha1))
            {
                Debug.DrawRay(NowPos, NowDir * 2, Color.red);     //赤色で５秒間可視化
                Debug.DrawRay(NowPos, NowDir * HitRayDistance, Color.blue); //青色で５秒間可視化
            }

            //- 貫通するレイを飛ばし、当たったオブジェクトを全て調べる
            foreach (RaycastHit hit in Physics.RaycastAll(ray, HitRayDistance))
            {
                //- レイが当たった距離よりヒット判定距離が長ければ、コンテニュー
                if (hit.distance >= HitRayDistance) continue;
                //- タグによって実行する処理を変える
                switch (hit.collider.gameObject.tag)
                {
                    case "Stage":
                        HitCheckStage(hit,DirNum);
                        break;
                    case "PlayerBlock":
                        HitCheckPlayerBlock(hit, DirNum);
                        break;
                    default:
                        break;
                }//- タグのswitch文終了
            }//- 一本のレイが終了
        }//- 全てのレイが終了

        // === 侵入不可テープと離れたかどうかチェック ===
        //- 侵入不可テープと接触している &　レイが侵入不可テープに当たらなかった　
        if (IsHitPlayerBlock && !CheckHitPlayerBlock)
        {
            //- フラグの変更
            IsHitPlayerBlock = false;
            //- 記憶してた移動方向を復活させる
            module.movedir = SaveMoveDir;
            //- 方向のリセット
            HitDirPlayerBlock = -1;
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        //- 外側の壁に当たったら処理
        if (collider.gameObject.tag == "OutsideWall")
        {
            //- 検索回避のためにタグの変更
            collider.gameObject.tag = "Untagged";
            //- 時間経過で消滅
            Destroy(DestroyObject, 0.5f);
        }
    }
    void HitCheckStage(RaycastHit hit, int dirnum)
    {
        // === レイの方向を調べて、移動を反転させる ===

        //- 上方向 & トンボ花火が上に移動中なら　上下に反転
        if (dirnum == 0 && module.movedir.y > 0) module.movedir.y *= -1;

        //- 右方向 & トンボ花火が右に移動中なら　左右に反転
        if (dirnum == 1 && module.movedir.x > 0) module.movedir.x *= -1;

        //- 下方向 & トンボ花火が下に移動中なら　上下に反転
        if (dirnum == 2 && module.movedir.y < 0) module.movedir.y *= -1;

        //- 左方向 & トンボ花火が左に移動中なら　左右に反転
        if (dirnum == 3 && module.movedir.x < 0) module.movedir.x *= -1;
    }
    void HitCheckPlayerBlock(RaycastHit hit, int dirnum)
    {
        //- 侵入不可テープと接触しているときのみ実行
        if (IsHitPlayerBlock)
        {
            //- 侵入不可テープがある方向、とレイが当たった方向が違うならリターン
            if (HitDirPlayerBlock != dirnum) return;
            //- 侵入不可テープとの接触チェックフラグを変更
            CheckHitPlayerBlock = true;
        }

        //- 侵入不可テープに接触していればリターン
        if (HitDirPlayerBlock != -1) return;

        // === レイの方向を調べて、移動方向を決定する ===

        //- 移動方向を記憶しておく
        SaveMoveDir = module.movedir;

        //- 侵入不可テープとの接触フラグを変更
        IsHitPlayerBlock = true;

        //- 上方向 & トンボ花火が上に移動中なら実行
        if (dirnum == 0 && module.movedir.y > 0)
        {
            //- 縦方向の移動を消す
            module.movedir.y = 0;
            //- 横方向の移動速度を伸ばす
            if (module.movedir.x >  0) module.movedir.x = 1;
            if (module.movedir.x <= 0) module.movedir.x = -1;
            //- 接触方向の保存
            HitDirPlayerBlock = 0;
        }       
        //- 右方向 & トンボ花火が右に移動中なら実行
        if (dirnum == 1 && module.movedir.x > 0)
        {
            //- 横方向の移動を消す
            module.movedir.x = 0;
            //- 縦方向の移動速度を伸ばす
            if (module.movedir.y >  0) module.movedir.y = 1;
            if (module.movedir.y <= 0) module.movedir.y = -1;
            //- 接触方向の保存
            HitDirPlayerBlock = 1;
        }
        //- 下方向 & トンボ花火が下に移動中なら実行
        if (dirnum == 2 && module.movedir.y < 0)
        {
            //- 縦方向の移動を消す
            module.movedir.y = 0;
            //- 横方向の移動速度を伸ばす
            if (module.movedir.x >  0) module.movedir.x = 1;
            if (module.movedir.x <= 0) module.movedir.x = -1;
            //- 接触方向の保存
            HitDirPlayerBlock = 2;
        }
        //- 左方向 & トンボ花火が左に移動中なら実行
        if (dirnum == 3 && module.movedir.x < 0)
        {
            //- 横方向の移動を消す
            module.movedir.x = 0;
            //- 縦方向の移動速度を伸ばす
            if (module.movedir.y >  0) module.movedir.y = 1;
            if (module.movedir.y <= 0) module.movedir.y = -1;
            //- 接触方向の保存
            HitDirPlayerBlock = 3;
        }
    }
}

