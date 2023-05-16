/*
 ===================
 制作：大川
 ギミックガイドアニメーションを管理するスクリプト
 ===================
 */

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Video;
#if UNITY_EDITOR
//- デプロイ時にEditorスクリプトが入るとエラー。UNITY_EDITORで括る
using UnityEditor;
#endif

//- ギミック説明看板のアニメーション
public class BoardMove : MonoBehaviour
{
    private enum E_OUTDIRECTION
    {
        [Header("左")]
        LEFT,
        [Header("右")]
        RIGHT,
        [Header("上")]
        UP,
        [Header("下")]
        DOWN,
    }

    private const float LEFT = -2500.0f;
    private const float RIGHT = 3500.0f;
    private const float TOP = 1200.0f;
    private const float DOWN = -1200.0f;

    [SerializeField] private Image img;
    [SerializeField] private VideoPlayer movie;
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private float IntervalTime = 0.0f;
    private Dictionary<string, Dictionary<string, Vector3>> InitValues;
    public static bool MoveComplete = false;

    //- 絶対減らす
    private bool First = true;
    private bool fReInMove = false;     //再登場フラグ
    private bool fReOutMove = false;    //再撤退フラグ
    private bool Inloaded = false;      //読み込み済か
    private bool Outloaded = false;
    private bool InComplete = false;    //開始処理完了フラグ
    private bool OutComplete = false;   //撤退処理完了フラグ


    private void Awake()
    {
        //- 初期値登録
        InitValues = new Dictionary<string, Dictionary<string, Vector3>>
        {{"動画",new Dictionary<string, Vector3>{{"位置",movie.transform.position},}}};
        InitValues.Add("文字", new Dictionary<string, Vector3> { { "位置", tmp.transform.position } });
        InitValues.Add("背景", new Dictionary<string, Vector3> { { "位置", img.transform.position } });
        //- 初期位置更新
        img.transform.localPosition = new Vector3(LEFT + img.transform.localPosition.x, img.transform.localPosition.y);
        movie.transform.localPosition = new Vector3(LEFT + img.transform.localPosition.x, movie.transform.localPosition.y);
        tmp.transform.localPosition = new Vector3(LEFT + img.transform.localPosition.x, tmp.transform.localPosition.y);
        //- 動画停止
        movie.Pause();

        if(MoveComplete)
        { OutComplete = true; }
    }
    
    /// <summary>
    /// 登場挙動を行う
    /// </summary>
    public void StartMove()
    {
        // 無限呼び出し　修正
        if(!Inloaded)
        {
            GameObject.Find("Player").GetComponent<PController>().SetWaitFlag(true);
            //- 後で減らす
            Inloaded = true;
            Outloaded = false;
            fReInMove = true;
            fReOutMove = false;
            OutComplete = false;

            movie.Play();
            //- 左から真ん中
            var InAnime = DOTween.Sequence();
            InAnime.AppendInterval(IntervalTime)
            .Append(img.transform.DOMove(InitValues["背景"]["位置"], 0.5f))
            .Join(movie.transform.DOMove(InitValues["動画"]["位置"], 0.525f))
            .Join(tmp.transform.DOMove(InitValues["文字"]["位置"], 0.5f))
            .OnComplete(() => {
                InComplete = true;
                InAnime.Kill();
            });
        }
    }

    /// <summary>
    /// 撤退挙動を行う
    /// </summary>
    public void OutMove()
    {
        if (!Outloaded)
        {
            if(!First)
            {   GameObject.Find("Player").GetComponent<PController>().SetWaitFlag(false);   }
            
            //- 絶対こんな必要ない
            fReInMove = false;
            Outloaded = true;
            Inloaded = false;
            fReOutMove = true;
            InComplete = false;
            movie.Stop();

            var OutAnime = DOTween.Sequence();
                OutAnime.Append(movie.transform.DOMoveX(RIGHT, 0.3f))
                .Join(img.transform.DOMoveX(RIGHT, 0.3f))
                .Join(tmp.transform.DOMoveX(RIGHT, 0.3f))
                .OnComplete(() =>
                {
                    OutComplete = true;
                    //- 初期位置更新
                    img.transform.localPosition = new Vector3(LEFT, img.transform.localPosition.y);
                    movie.transform.localPosition = new Vector3(LEFT, movie.transform.localPosition.y);
                    tmp.transform.localPosition = new Vector3(LEFT, tmp.transform.localPosition.y);
                    MoveComplete = true;
                    OutAnime.Kill();
                });
            First = false;
        }
    }

    /// <summary>
    ///  描画フラグをリセットする
    /// </summary>
    public static void ResetMoveComplete()
    {   MoveComplete = false;    }


    /// <summary>
    /// 再登場フラグ返却
    /// </summary>
    public bool GetInMove()
    {   return fReInMove;     }

    /// <summary>
    /// 再撤退フラグ返却
    /// </summary>
    public bool GetOutMove()
    { return fReOutMove;      }  

    public bool GetInMoveComplet()
    { return InComplete; }

    public bool GetOutMoveComplet()
    { return OutComplete; }

    /// <summary>
    /// 再登場入力
    /// </summary>
    /// <param name="context"></param>
    public void OnInTips(InputAction.CallbackContext context)
    {
        //- Tips再描画フラグをオンにする
        if (context.started && !SceneChange.bIsChange)
        {
            IntervalTime = 0.0f;
            fReInMove = true;
        }
    }

    /// <summary>
    /// 再撤退入力
    /// </summary>
    /// <param name="context"></param>
    public void OnOutTips(InputAction.CallbackContext context)
    {
        //- Tips再撤退フラグをオンにする
        if (context.started && !SceneChange.bIsChange)
        {   fReOutMove = true;  }
        if(context.canceled && !SceneChange.bIsChange)
        {   fReOutMove = false; }
        
    }

    
}

