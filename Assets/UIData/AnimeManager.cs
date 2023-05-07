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
    [SerializeField] private TargetDescription DrawDescription;
    [SerializeField] private CutIn DrawBossCutIn;
    [SerializeField] private BoardMove board;

    private bool BoardMoveVompleat = false;
    private bool InMoveCompleat = false;
    private bool OutMoveCompleat = false;
    
    private void Start()
    {
        //- 目標開始演出がある
        if (DrawDescription)
        {   DrawDescription.MoveDescription();  }
        //- ボスカットインがある
        else
        {   DrawBossCutIn.MoveCutIn();  }
    }

    void Update()
    {
        //- ギミック説明のアニメ動作
        if(TargetDescription.MoveCompleat && !BoardMoveVompleat)
        {
            DrawSelect.StartMove();
            DrawReset.StartMove();
            if(board)
            { board.StartMove(); }
            BoardMoveVompleat = true;
        }
        else if(CutIn.MoveCompleat && !InMoveCompleat)
        {
            DrawTips.StartMove();
            DrawSelect.StartMove();
            DrawReset.StartMove();
            if (board)
            { board.StartMove(); }
            //DOTween.Sequence().AppendInterval(0.5f);
            InMoveCompleat = true;
        }

        
        //- クリアした、撤退挙動をしていない
        if (SceneChange.bIsChange && !OutMoveCompleat)
        {
            if (DrawDescription)
            {
                DrawSelect.OutMove();
                DrawReset.OutMove();
            }
            else if(DrawBossCutIn)
            {
                DrawTips.OutMove();
                DrawSelect.OutMove();
                DrawReset.OutMove();
            }
            OutMoveCompleat = true;
        }
    }
}
