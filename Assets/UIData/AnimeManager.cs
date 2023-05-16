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
    /*　変数宣言部　*/
    [SerializeField] private EntryAnime DrawSelect;
    [SerializeField] private EntryAnime DrawReset;
    [SerializeField] private EntryAnime DrawTips;
    [SerializeField] private OpeningAnime DrawOpening;
    [SerializeField] private CutIn DrawBossCutIn;
    [SerializeField] private BoardMove DrawGimmickBoard;

    [SerializeField] private float OptionTime = 0.0f;

    private Dictionary<string, bool> ControlFlag;
    private bool InMoveCompleat = false;
    private bool OutMoveCompleat = false;
    private bool Load = false;
    private bool TipsInLoad = false;
    private bool TipsOutLoad = false;
    private InputAction tipsAction;
    private PController player;
    private Image imageTips;
    private float PushButton = 0.0f;
    private void Awake()
    {
        //- アニメ管理するオブジェクトフラグ初期化
        ControlFlag = new Dictionary<string, bool>
                    {
                        { "セレクト", false },
                        { "リセット", false },
                        { "ギミック表示",false },
                        { "通常開幕", false },
                        { "ギミック看板", false },
                        { "ボス演出", false },
                    };
        //- オブジェクトが存在したらフラグ変更
        if (DrawSelect) { ControlFlag["セレクト"] = true; }
        if (DrawReset) { ControlFlag["リセット"] = true; }
        if (DrawTips) { ControlFlag["ギミック表示"] = true; }
        if (DrawOpening) { ControlFlag["通常開幕"] = true; }
        if (DrawGimmickBoard) { ControlFlag["ギミック看板"] = true; }
        if (DrawBossCutIn) { ControlFlag["ボス演出"] = true; }

        player = GameObject.Find("Player").GetComponent<PController>();
        if (ControlFlag["ギミック表示"])
        {   imageTips = GameObject.Find("DrawTipsGage").GetComponent<Image>();  }
    }

    private void Start()
    {
        //- ギミック演出がある、初めて描画するか
        if (ControlFlag["ギミック看板"] && !BoardMove.MoveComplete)
        {
            DrawGimmickBoard.StartMove();
        }
        //- 通常開幕がある、初めて描画するか
        else if (ControlFlag["通常開幕"] && !OpeningAnime.MoveCompleat)
        { DrawOpening.StartMove(); }
    }

    void Update()
    {
        //- 自爆フラグを取得する
        bool PlayerBoom = player.GetIsOnce();

        /*　　開始演出の判定　　*/
        //- ボス演出が終わっている、ボタンアシストを表示していない
        if (CutIn.MoveCompleat && !InMoveCompleat)
        {
            InGameDrawObjs();
            InMoveCompleat = true;
        }

        //- ボス演出が存在する
        if (ControlFlag["ボス演出"] && !InMoveCompleat)
        {
            //- ギミック看板の処理が完了していたら処理
            if (BoardMove.MoveComplete)
            {
                //- ボス演出を行っていないなら処理
                if (!CutIn.MoveCompleat && !Load)
                {
                    DrawBossCutIn.MoveCutIn();
                    Load = true;
                }
                //- ボス演出を行っていたらボタンアシストを表示する
                else if (CutIn.MoveCompleat && Load)
                { InGameDrawObjs(); }
            }
        }
        //- 通常開幕を表示していない
        else if (ControlFlag["ギミック看板"] && !InMoveCompleat)
        {
            //- ギミック処理を行ったら処理
            if (BoardMove.MoveComplete && !OpeningAnime.MoveCompleat)
            { DrawOpening.StartMove(); }
            //- 通常開幕が終了したらボタンアシストを表示
            if (OpeningAnime.MoveCompleat)
            { InGameDrawObjs(); }
        }
        //- ボス演出もギミック看板もない場合
        else if (!ControlFlag["ボス演出"] && !ControlFlag["ギミック看板"] && !InMoveCompleat)
        {
            //- 通常開幕を行う
            if (ControlFlag["通常開幕"] && !OpeningAnime.MoveCompleat)
            { DrawOpening.StartMove(); }
            //- ボタンアシストを表示
            else if (OpeningAnime.MoveCompleat)
            { InGameDrawObjs(); }
        }

        /*　　Tips途中描画の判定　　*/
        //- 再登場
        //Tipsがある、開幕演出が終わっている、自爆していない、再登場フラグが立っている 最初のTips描画が終わっている
        if (ControlFlag["ギミック看板"] && OpeningAnime.MoveCompleat && !PlayerBoom && DrawGimmickBoard.GetInMove())
        {
            if (!TipsInLoad && DrawGimmickBoard.GetOutMoveComplet())
            {
                TipsInLoad = true;
                DrawGimmickBoard.StartMove();
                TipsOutLoad = false;
            }
        }
        else if(ControlFlag["ギミック看板"] && CutIn.MoveCompleat && !PlayerBoom && DrawGimmickBoard.GetInMove())
        {
            if (!TipsInLoad && DrawGimmickBoard.GetOutMoveComplet())
            {
                TipsInLoad = true;
                DrawGimmickBoard.StartMove();
                TipsOutLoad = false;
            }
        }
        //- 再撤退
        //Tipsがある、自爆していない、通常開幕かボス演出を行っている
        if(ControlFlag["ギミック看板"] && !PlayerBoom && !TipsOutLoad )
        {
            if (DrawGimmickBoard.GetInMoveComplet() && DrawGimmickBoard.GetOutMove())
            {
                PushButton += Time.deltaTime;
                imageTips.fillAmount = PushButton / OptionTime;
            }
            else if(!DrawGimmickBoard.GetOutMove())
            {
                PushButton = 0.0f;
                imageTips.fillAmount = 0.0f;
            }
            if(PushButton >= OptionTime)
            {
                TipsOutLoad = true;
                DrawGimmickBoard.OutMove();
                TipsInLoad = false;
            }
        }
        //- プレイヤーが自爆したら強制的に撤退させる
        if(ControlFlag["ギミック看板"] && PlayerBoom && DrawGimmickBoard.GetInMove())
        { DrawGimmickBoard.OutMove(); }


        /*　　撤退挙動の判定　　*/
        //- クリアした、撤退挙動をしていない
        if (SceneChange.bIsChange && !OutMoveCompleat)
        {
            if (ControlFlag["セレクト"]) { DrawSelect.OutMove(); }
            if (ControlFlag["リセット"]) { DrawReset.OutMove(); }
            if (ControlFlag["ギミック表示"]) { DrawTips.OutMove(); }
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
        if (ControlFlag["ギミック表示"]) { DrawTips.StartMove(); }
        InMoveCompleat = true;
    }
}

