/*
 ===================
 制作：大川
 UIアニメーションの作動タイミングを管理するスクリプト
 ===================
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;
#if UNITY_EDITOR
using UnityEditor;
#endif

//- UIアニメーション作動を管理するクラス
public class AnimeManager : MonoBehaviour
{

    [SerializeField] private EntryAnime DrawSelect;         //会場選択UI
    [SerializeField] private EntryAnime DrawReset;          //やり直しUI
    [SerializeField] private EntryAnime DrawTips;           //Tips再表示UI
    [SerializeField] private OpeningAnime DrawOpening;      //開幕演出
    [SerializeField] private CutIn DrawBossCutIn;           //ボスカットイン
    [SerializeField] private BoardMove DrawGimmickBoard;    //Tips表示
    [SerializeField] private float MaxPushTime = 1.0f;      //入力時間

    private Dictionary<string, bool> ControlFlag;           //アニメーションオブジェクトのフラグ管理
    private bool InMoveCompleat = false;                    //登場処理完了フラグ
    private bool OutMoveCompleat = false;                   //撤退処理完了フラグ
    private bool FirstLoad = false;                         //初回読み込みフラグ
    private bool TipsInLoad = false;                        //Tips登場フラグ
    private bool TipsOutLoad = false;                       //Tips撤退フラグ
    private InputAction tipsAction;                         //Tips登場の入力処理
    private PController player;                             //プレイヤー操作用変数
    private Image TipsButtonGage;                           //Tips表示ボタンの長押しゲージ
    private float CurrentPushTime = 0.0f;                   //最大入力時間
    
    private void Awake()
    {
        //- アニメ管理するオブジェクトフラグ初期化
        ControlFlag = new Dictionary<string, bool>
                    {
                        { "セレクト", false },
                        { "リセット", false },
                        { "Tips再表示",false },
                        { "開幕", false },
                        { "Tips", false },
                        { "ボス", false },
                    };

        //- オブジェクトが存在したらフラグ変更
        if (DrawSelect)       { ControlFlag["セレクト"] = true;     }
        if (DrawReset)        { ControlFlag["リセット"] = true;     }
        if (DrawTips)         { ControlFlag["Tips再表示"] = true; }
        if (DrawOpening)      { ControlFlag["開幕"] = true;     }
        if (DrawGimmickBoard) { ControlFlag["Tips"] = true; }
        if (DrawBossCutIn)    { ControlFlag["ボス"] = true;     }

        //- プレイヤー情報取得
        player = GameObject.Find("Player").GetComponent<PController>();

        //- Tipsが存在しているなら、Tips再表示ボタン情報を取得
        if (ControlFlag["Tips再表示"]){ TipsButtonGage = GameObject.Find("DrawTipsGage").GetComponent<Image>();  }
    }

    private void Start()
    {
        //- Tipsがある・初回描画
        if(ControlFlag["Tips"] && !BoardMove.MoveComplete)
        { DrawGimmickBoard.StartMove(); }//Tipsを表示

        //- 開幕がある・初回描画
        else if(ControlFlag["開幕"] && !OpeningAnime.MoveCompleat)
        { DrawOpening.StartMove(); }//開幕を表示
    }

    void Update()
    {
        //- プレイヤーの自爆フラグを取得する
        bool PlayerBoom = player.GetIsOnce();


        /*　
         *　＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
         *　
         *　二つ目のアニメーションを行うか
         *　
         *　＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
         */

        //- ボスステージ・演出をしていない・初めて処理する・Tipsが撤退している
        if (ControlFlag["ボス"] && !CutIn.MoveCompleat && !FirstLoad && BoardMove.MoveComplete)
        {
            //- 初めて読み込んだ
            FirstLoad = true;
            //- ボス演出を行う
            DrawBossCutIn.MoveCutIn();
        }
        //- 通常ステージ・演出をしていない・初めて処理する・Tipsが撤退している
        else if(ControlFlag["開幕"] && !OpeningAnime.MoveCompleat && !FirstLoad && BoardMove.MoveComplete)
        {
            //- 初めて読み込んだ
            FirstLoad = true;
            //- 開幕演出を行う
            DrawOpening.StartMove();
        }


        /*　
         *　＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
         *　
         *　二つ目のアニメーションが終了し、
         *　ボタンアシストを表示する
         *　
         *　＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
         */
         
        //- ボスステージ・演出済である
        if (ControlFlag["ボス"] && CutIn.MoveCompleat)
        {
            //- アシスト表示後に使用しないためフラグ変更
            ControlFlag["ボス"] = false;
            //- ボタンアシストを表示する
            InGameDrawObjs();
        }
        //- 通常ステージ・演出済である
        if(ControlFlag["開幕"] && OpeningAnime.MoveCompleat)
        {
            //- アシスト後に使用しないためフラグ変更
            ControlFlag["開幕"] = false;
            //- ボタンアシストを表示
            InGameDrawObjs();
        }


        /*　
         *　＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
         *　
         *　Tipsを登場・撤退させる　
         *　
         *　＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
         */

        //＝＝＝＝＝＝＝＝＝      登場処理       ＝＝＝＝＝＝＝＝＝
        //- Tipsがある・ボス演出が終わっている・自爆していない・再登場ボタン入力がされている
        if (ControlFlag["Tips"] && CutIn.MoveCompleat && !PlayerBoom && DrawGimmickBoard.GetInDrawButtonPush())
        {
            //-　プレイヤーを動作不可能にする
            GameObject.Find("Player").GetComponent<PController>().SetWaitFlag(true);
            DrawGimmickBoard.StartMove();
        }
        //- Tipsがある・開幕が終わっている・自爆していない・再登場ボタン入力がされている
        else if (ControlFlag["Tips"] && OpeningAnime.MoveCompleat && !PlayerBoom && DrawGimmickBoard.GetInDrawButtonPush())
        {
            //-　プレイヤーを動作不可能にする
            GameObject.Find("Player").GetComponent<PController>().SetWaitFlag(true);
            DrawGimmickBoard.StartMove();
        }
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝


        //＝＝＝＝＝＝＝＝＝      撤退処理     ＝＝＝＝＝＝＝＝＝
        //- Tipsがある・現在Tipsが表示されている・自爆していない・再登場ボタン入力がされている
        if (ControlFlag["Tips"] && DrawGimmickBoard.GetLoadStart() && !PlayerBoom && DrawGimmickBoard.GetOutDrawButtonPush())
        {
            //- 入力継続時間を代入
            CurrentPushTime += Time.deltaTime;
            //- 時間分画像値を変動
            TipsButtonGage.fillAmount = CurrentPushTime / MaxPushTime;
        }
        //- ボタン入力がされていない
        else if(ControlFlag["Tips"] && !DrawGimmickBoard.GetOutDrawButtonPush())
        {
            CurrentPushTime = 0.0f;
            TipsButtonGage.fillAmount = 0.0f;
        }
        //- 入力時間が指定時間を越したら撤退処理を行う
        if (CurrentPushTime >= MaxPushTime)
        {
            DrawGimmickBoard.OutMove();
            //- 初回の撤退でなかったらプレイヤー操作管理を行う
            if(CutIn.MoveCompleat || OpeningAnime.MoveCompleat )
            {
                //-　プレイヤーを動作可能にする
                GameObject.Find("Player").GetComponent<PController>().SetWaitFlag(false);
            }
        }

        //- プレイヤーが自爆した時、Tipsが描画されていたら強制的に撤退
        if(PlayerBoom && ControlFlag["Tips"] && DrawGimmickBoard.GetLoadStart())
        { DrawGimmickBoard.OutMove(); }
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝


        /*　
         *　＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
         *　
         *　クリア時にボタンアシストを撤退する
         *　
         *　＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
         */
        if (SceneChange.bIsChange && !OutMoveCompleat)
        {
            if (ControlFlag["セレクト"]) { DrawSelect.OutMove(); }
            if (ControlFlag["リセット"]) { DrawReset.OutMove(); }
            if (ControlFlag["Tips再表示"]) { DrawTips.OutMove(); }
            OutMoveCompleat = true;
        }
    }

    /// <summary>
    /// ボタンアシストを表示する
    /// </summary>
    private void InGameDrawObjs()
    {
        if (ControlFlag["セレクト"]) { DrawSelect.StartMove(); }
        if (ControlFlag["リセット"]) { DrawReset.StartMove(); }
        if (ControlFlag["Tips再表示"]) { DrawTips.StartMove(); }
        //-　プレイヤーを動作可能にする
        GameObject.Find("Player").GetComponent<PController>().SetWaitFlag(false);
        InMoveCompleat = true;
    }
}

