/*
 ===================
 制作：大川
 ギミックガイドアニメーションを管理するスクリプト
 ===================
 */

using UnityEngine;
using System.Collections.Generic;
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
    [SerializeField] private float DeleyTime = 0.0f;
    private Dictionary<string, Dictionary<string, Vector3>> InitValues;
    public static bool MoveComplete = false;
    //private PController pCnt;
    private void Awake()
    {
        //- 初期値登録
        InitValues = new Dictionary<string, Dictionary<string, Vector3>>
        {{"動画",new Dictionary<string, Vector3>{{"位置",movie.transform.position},}}};
        InitValues.Add("文字", new Dictionary<string, Vector3> { { "位置", tmp.transform.position } });
        InitValues.Add("背景", new Dictionary<string, Vector3> { { "位置", img.transform.position } });
        //- 初期位置更新
        img.transform.localPosition = new Vector3(LEFT, img.transform.localPosition.y);
        movie.transform.localPosition = new Vector3(LEFT, movie.transform.localPosition.y);
        tmp.transform.localPosition = new Vector3(LEFT, tmp.transform.localPosition.y);
        //- 動画停止
        movie.Pause();
    }

    /// <summary>
    /// 登場挙動を行う
    /// </summary>
    public void StartMove()
    {
        //- 左から真ん中
        DOTween.Sequence()
            .AppendInterval(1.0f)
            .Append(img.transform.DOMove(InitValues["背景"]["位置"], 0.5f))
            .Join(movie.transform.DOMove(InitValues["動画"]["位置"], 0.5f))
            .Join(tmp.transform.DOMove(InitValues["文字"]["位置"], 0.5f))
            .OnPlay(() => { movie.Play(); })
            .AppendInterval(DeleyTime)
            .OnComplete(() => { OutMove(); });
    }

    /// <summary>
    /// 撤退挙動を行う
    /// </summary>
    public void OutMove()
    {
        DOTween.Sequence()
            .Append(movie.transform.DOMoveX(RIGHT, 0.3f))
            .Join(img.transform.DOMoveX(RIGHT, 0.3f))
            .Join(tmp.transform.DOMoveX(RIGHT, 0.3f))
            .OnComplete(() =>
            {
                MoveComplete = true;
                Destroy(gameObject);
            });
        //- プレイヤーを操作可能に変更
        GameObject.Find("Player").GetComponent<PController>().SetWaitFlag(false);
    }

    public static bool GetFirstDraw()
    {
        Debug.Log("Get" + MoveComplete);
        return MoveComplete; }

    public static void SetFirstDraw(bool flag)
    {
        MoveComplete = flag;
        Debug.Log("Set" + MoveComplete);
    }
}

