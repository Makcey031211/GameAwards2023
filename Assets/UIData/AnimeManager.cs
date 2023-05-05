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
    /*　列挙体宣言部　*/
    
    /*　変数宣言部　*/
    [SerializeField] private EntryAnime DrawSelect;
    [SerializeField] private EntryAnime DrawReset;
    [SerializeField] private FireBelt PlayerFireBelt;
    [SerializeField] private EntryAnime DrawTips;
    [SerializeField] private CutIn DrawCutIn;

    private bool NoCutIn = false;
    private bool InMoveCompleat = false;
    private bool OutMoveCompleat = false;
    
    private void Start()
    {
        //- ボスTipsがない場合
        if (!DrawCutIn)
        {
            DrawSelect.StartMove();
            DrawReset.StartMove();
            PlayerFireBelt.MoveLocation();
            NoCutIn = true;
        }
        //- カットインがあるとき
        else
        {   DrawCutIn.MoveCutIn();  }
    }

    void Update()
    {
        //- カットイン有時の登場
        if (!NoCutIn && CutIn.MoveCompleat && !InMoveCompleat)
        {
            DrawTips.StartMove();
            DrawSelect.StartMove();
            DrawReset.StartMove();
            DOTween.Sequence()
                .AppendInterval(0.5f)
                .OnComplete(()=> {  PlayerFireBelt.MoveLocation();    });
            InMoveCompleat = true;
        }

        //- クリアした、撤退挙動をしていない
        if (SceneChange.bIsChange && !OutMoveCompleat)
        {
         
            //- ボスカットインがあるか
            if (!DrawCutIn)
            {
                DrawSelect.OutMove();
                DrawReset.OutMove();
            }
            else
            {
                DrawTips.OutMove();
                DrawSelect.OutMove();
                DrawReset.OutMove();
            }
            OutMoveCompleat = true;
        }
    }
}
