using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class UIAnimeManager : MonoBehaviour
{
    private enum E_HowMove
    {
        Center,     //中央
        Right,      //右
        TopRight,   //右上
        LowerRight, //右下
        Left,       //左
        TopLeft,    //左上
        LowerLeft,  //左下
    }
    
    private enum E_Scaling
    {
        BigSize_4,  //4倍
        BigSize_3,  //3倍
        BigSize_2,  //2倍
        
        Default,    //1倍

        SmallSize_2,//1/2
        SmallSize_3,//1/3
        SmallSize_4 //1/4
    }


    [HeaderAttribute("---拡縮設定---")]
    [SerializeField, Header("初期サイズ")]
    private E_Scaling SetSize;
    [SerializeField, Header("拡縮完了までの時間:float")]
    private float SizeTime = 1.0f;

    [HeaderAttribute("---移動設定---")]
    [SerializeField, Header("初期位置")]
    private E_HowMove StartPos;
    [SerializeField, Header("到着位置")]
    private E_HowMove EndPos;
    [SerializeField, Header("移動完了までの時間:float")]
    private float MoveTime = 1.0f;

    private Vector2 MoveEndPos;
    private Vector2 initSize;
    private Vector2 scall;

    public bool bUIMoveComplete;

    //========= 座標の登録 ==============
    private readonly float CENTER = 0.0f;
    private readonly float RIGHT = Screen.width / 2.0f;
    private readonly float LEFT = -Screen.width / 2.0f;
    private readonly float TOP = Screen.height / 2.0f;
    private readonly float LOWER = -Screen.height / 2.0f;

    void Start()
    {
        //- フラグを初期化
        bUIMoveComplete = false;
        //- Canvas座標を取得
        RectTransform trans = GetComponent<RectTransform>();
        //- アニメーション
        Move(trans);
    }

/// <summary>
/// 挙動再生
/// </summary>
/// <param name="trans"></param>
    private void Move(RectTransform trans)
    {
        //- スタート位置を設定
        StartPosTransformation(trans);
        //- 終了位置を設定
        EndPosTransformation(trans);
        //- 拡縮サイズを設定
        SizeSetting();
        //- 初期サイズを代入
        initSize = this.transform.localScale;
        //- サイズを変更
        this.transform.localScale = scall;

        transform.DOScale(initSize, 1.0f)
            .OnComplete(() =>
            {
                //- 移動
                transform.DOLocalMove(MoveEndPos, MoveTime).SetEase(Ease.OutSine)
                    .OnComplete(()=>
                {
                    //- 移動が完了したらtrueにする
                    bUIMoveComplete = true;
                });
            });


    }


    private void SizeSetting()
    {
        switch (SetSize)
        {
            case E_Scaling.BigSize_4:
                SizeSetting(4.0f);
                break;
            case E_Scaling.BigSize_3:
                SizeSetting(3.0f);
                break;
            case E_Scaling.BigSize_2:
                SizeSetting(2.0f);
                break;
            case E_Scaling.Default:
                SizeSetting(1.0f);
                break;
            case E_Scaling.SmallSize_2:
                SizeSetting(0.5f);
                break;
            case E_Scaling.SmallSize_3:
                SizeSetting(0.25f);
                break;
            case E_Scaling.SmallSize_4:
                SizeSetting(0.125f);
                break;
        }
    }

    private void SizeSetting(float size)
    {
        scall = new Vector3(
            this.gameObject.transform.localScale.x * size,
            this.gameObject.transform.localScale.y * size,
            0.0f);
    }

    /// <summary>
    /// 終了位置を計算する
    /// </summary>
    /// <param name="trans"></param>
    private void EndPosTransformation(RectTransform trans)
    {
        switch (EndPos)
        {
            case E_HowMove.Center:
                MoveEndPos = new Vector2(CENTER, CENTER);
                break;
            case E_HowMove.Right:
                MoveEndPos = new Vector2(RIGHT - trans.sizeDelta.x / 2, CENTER);
                break;
            case E_HowMove.TopRight:
                MoveEndPos = new Vector2(RIGHT - trans.sizeDelta.x / 2, TOP);
                break;
            case E_HowMove.LowerRight:
                MoveEndPos = new Vector2(RIGHT - trans.sizeDelta.x / 2, LOWER + trans.sizeDelta.y / 2);
                break;
            case E_HowMove.Left:
                MoveEndPos = new Vector2(LEFT + trans.sizeDelta.x / 2, CENTER);
                break;
            case E_HowMove.TopLeft:
                MoveEndPos = new Vector2(LEFT + trans.sizeDelta.x / 2, TOP);
                break;
            case E_HowMove.LowerLeft:
                MoveEndPos = new Vector2(LEFT + trans.sizeDelta.x / 2, LOWER + trans.sizeDelta.y / 2);
                break;
        }
    }

    /// <summary>
    /// 開始地点の座標を計算する
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="pos"></param>
    private void StartPosTransformation(RectTransform trans)
    {
        switch (StartPos)
        {
            case E_HowMove.Center:
                trans.anchoredPosition = new Vector2(CENTER,CENTER);
                break;
            case E_HowMove.Right:
                trans.anchoredPosition = new Vector2(RIGHT - trans.sizeDelta.x / 2,CENTER);
                break;
            case E_HowMove.TopRight:
                trans.anchoredPosition = new Vector2(RIGHT - trans.sizeDelta.x / 2, TOP);
                break;
            case E_HowMove.LowerRight:
                trans.anchoredPosition = new Vector2(RIGHT - trans.sizeDelta.x / 2, LOWER + trans.sizeDelta.y / 2);
                break;
            case E_HowMove.Left:
                trans.anchoredPosition = new Vector2(LEFT + trans.sizeDelta.x / 2, CENTER);
                break;
            case E_HowMove.TopLeft:
                trans.anchoredPosition = new Vector2(LEFT + trans.sizeDelta.x / 2, TOP);
                break;
            case E_HowMove.LowerLeft:
                trans.anchoredPosition = new Vector2(LEFT + trans.sizeDelta.x / 2, LOWER + trans.sizeDelta.y / 2);
                break;
        }
    }

    public bool GetUIAnimeComplete()
    {
        return bUIMoveComplete;
    }

}
