// =============================================
// StoryFlip.cs
//
// 作成：井上
//
// ストーリーシーンの演出用スクリプト
// =============================================

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryFlip : MonoBehaviour
{
    [SerializeField]
    private GameObject[] flips;     // ストーリーのイラスト

    private GameObject enterButton; // 里セレクト画面へ遷移するボタン表示のUI
    private Image fade;             // フェード用

    private int NowFlipNum = 0;     // 現在のイラストの番号
    private bool isFlip = false;    // イラストが動いてるかどうか


    // Start is called before the first frame update
    void Start()
    {
        // ----- オブジェクト・コンポーネントの取得、初期設定
        // シーン遷移ボタン用
        enterButton = GameObject.Find("EnterButton");
        enterButton.SetActive(false);
        // フェード
        fade = GameObject.Find("FadeImage").GetComponent<Image>();
        fade.DOFade(0.0f, 1.0f);        // シーン開始時にフェードインする

        // ----- ストーリー周りの初期設定処理
        gameObject.transform.DetachChildren(); // 最初に親子関係を解除する
        // 1枚目のイラスト
        flips[0].transform.parent = gameObject.transform;               // 親子関係の設定
        RectTransform trans0 = flips[0].GetComponent<RectTransform>();  // UI用トランスフォームの取得
        trans0.localPosition = Vector3.zero;                            // イラストの初期位置設定
        // ２枚目以降のイラスト
        for (int i = 1; i < flips.Length; i++) {
            flips[i].transform.parent = gameObject.transform;
            RectTransform trans = flips[i].GetComponent<RectTransform>();
            trans.localPosition = flips[i - 1].GetComponent<RectTransform>().localPosition + new Vector3(trans.sizeDelta.x, 0.0f, 0.0f);
        }
    }

    private void Update()
    {
        // ----- シーン遷移ボタンイラストの有効化
        if (NowFlipNum >= flips.Length - 1 && !isFlip && !enterButton.activeInHierarchy)
        { // 最後のイラストに移り変わった上で、イラストが有効になっていないとき
            enterButton.SetActive(true);
        }
    }

    // ===================================================
    // シーン遷移キー押下時処理
    //
    // 里セレクト画面に遷移する
    // ===================================================
    public void OnEnter(InputAction.CallbackContext context)
    {
        if (NowFlipNum >= flips.Length - 1 && !isFlip)
        { // 最後のイラストに移り変わった時
            fade.DOFade(1.0f, 1.5f).OnComplete(() => { SceneManager.LoadScene("1_Village"); });
        }
    }

    // ===================================================
    // 次のイラストキー押下時処理
    //
    // 次のイラストに移り変わる
    // ===================================================
    public void OnNext(InputAction.CallbackContext context)
    {
        if (NowFlipNum < flips.Length - 1 && !isFlip)
        { // イラストが最後ではなく、移動中ではないとき
            isFlip = true;
            for (int i = 0; i < flips.Length; i++) {
                RectTransform trans = flips[i].GetComponent<RectTransform>();
                trans.DOMoveX(trans.position.x - 15.0f, 1.5f).SetEase(Ease.InOutCubic).OnComplete(() => { isFlip = false; });
            }
            NowFlipNum++;
        }
    }

    // ===================================================
    // 前のイラストキー押下時処理
    //
    // 前のイラストに移り変わる
    // ===================================================
    public void OnPrev(InputAction.CallbackContext context)
    {
        if (NowFlipNum > 0 && !isFlip)
        { // イラストが最初ではなく、移動中ではないとき
            isFlip = true;
            for (int i = 0; i < flips.Length; i++) {
                RectTransform trans = flips[i].GetComponent<RectTransform>();
                trans.DOMoveX(trans.position.x + 15.0f, 1.5f).SetEase(Ease.InOutCubic).OnComplete(() => { isFlip = false; });
            }
            NowFlipNum--;
        }
    }
}
