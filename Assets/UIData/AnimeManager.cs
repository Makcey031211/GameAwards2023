/*
 ===================
 制作：大川
 UIアニメーションの作動タイミングを管理するスクリプト
 ===================
 */

using System.Collections.Generic;
using UnityEngine;
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

    private Dictionary<string, bool> ControlFlag;
    private bool BoardMoveCompleat = false;
    private bool InMoveCompleat = false;
    private bool OutMoveCompleat = false;

    private void Awake()
    {
        //- アニメ管理するオブジェクトフラグ初期化
        ControlFlag = new Dictionary<string, bool>
                    {
                        { "セレクト", false },
                        { "リセット", false },
                        { "チップス", false },
                        { "通常開幕", false },
                        { "ギミック看板", false },
                        { "ボス演出", false },
                    };
       //- オブジェクトが存在したらフラグ変更
        if (DrawSelect)         { ControlFlag["セレクト"] = true; }
        if (DrawReset)          { ControlFlag["リセット"] = true; }
        if (DrawTips)           { ControlFlag["チップス"] = true; }
        if (DrawOpening)        { ControlFlag["通常開幕"] = true; }
        if (DrawGimmickBoard)   { ControlFlag["ギミック看板"] = true; }
        if (DrawBossCutIn)      { ControlFlag["ボス演出"] = true; }
    }

    private void Start()
    {
        if (ControlFlag["通常開幕"])
        { DrawOpening.MoveDescription();  }
        else if(ControlFlag["ボス演出"])
        {   DrawBossCutIn.MoveCutIn();  }
    }

    void Update()
    {
        //- 通常演出がある、通常演出が終了している、以下の分岐を実行していない
        if(ControlFlag["通常開幕"] && DrawOpening.GetMoveComplete() && !BoardMoveCompleat) 
        {
            if (ControlFlag["セレクト"]) { DrawSelect.StartMove(); }
            if (ControlFlag["リセット"]) { DrawReset.StartMove(); }
            if (ControlFlag["ギミック看板"]) { DrawGimmickBoard.StartMove(); }
            BoardMoveCompleat = true;
        }
        //- ボス演出がある、通常演出が終了している、以下の分岐を実行していない
        else if (ControlFlag["ボス演出"] && DrawBossCutIn.GetMoveComplete() && !InMoveCompleat)
        {
            if (ControlFlag["チップス"]) { DrawTips.StartMove(); }
            if (ControlFlag["セレクト"]) { DrawSelect.StartMove(); }
            if (ControlFlag["リセット"]) { DrawReset.StartMove(); }
            if (ControlFlag["ギミック看板"]) { DrawGimmickBoard.StartMove(); }
            InMoveCompleat = true;
        }
        
        //- クリアした、撤退挙動をしていない
        if (SceneChange.bIsChange && !OutMoveCompleat)
        {
            if (ControlFlag["チップス"]) { DrawTips.OutMove(); }
            if (ControlFlag["セレクト"]) { DrawSelect.OutMove(); }
            if (ControlFlag["リセット"]) { DrawReset.OutMove(); }
            OutMoveCompleat = true;
        }
    }
}
