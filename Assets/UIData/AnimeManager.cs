/*
 ===================
 制作：大川
 UIアニメーションの作動タイミングを管理するスクリプト
 ===================
 */

using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] private EntryAnime DrawTips;
    //[SerializeField] private GameObject DrawCutIn;
    private bool MoveCompleat = false;

    private void Start()
    {
        //- ボスTipsがない場合
        if (!DrawTips)
        {
            DrawSelect.StartMove();
            DrawReset.StartMove();
        }
        else
        {
            //DrawCutIn
            DrawTips.StartMove();
            DrawSelect.StartMove();
            DrawReset.StartMove();
        }
    }

    void Update()
    {
        //- クリアした、撤退挙動をしていない
        if(SceneChange.bIsChange && !MoveCompleat)
        {
            //- ボスTipsがない場合
            if (!DrawTips)
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
            MoveCompleat = true;
        }
    }
}
