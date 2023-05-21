using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGamePopUp : MonoBehaviour
{
    private Button StartButton;
    private Button NextButton;
    private Button EndButton;

    // Start is called before the first frame update
    void Start()
    {
        StartButton = GameObject.Find("ButtonStartGame").GetComponent<Button>();
        NextButton = GameObject.Find("ButtonNextGame").GetComponent<Button>();
        EndButton = GameObject.Find("ButtonEndGame").GetComponent<Button>();

        gameObject.SetActive(false);
    }

    // ポップアップを表示する
    public void PopUpOpen()
    {
        gameObject.SetActive(true); // ポップアップのオブジェクトを有効化
        // イージング設定
        gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        gameObject.transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutBack);

        // ポップアップ内のボタン以外を無効化
        StartButton.enabled = false;
        NextButton.enabled = false;
        EndButton.enabled = false;

        // ボタンの初期選択
        gameObject.transform.Find("ButtonEnter").GetComponent<Button>().Select();
        return;
    }

    public void PopUpClose()
    {
        // イージング設定
        gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        gameObject.transform.DOScale(0.0f, 0.5f).SetEase(Ease.InCubic).OnComplete(() => { gameObject.SetActive(false); }); // 終了時オブジェクト無効化

        // ポップアップ内のボタン以外を有効化
        StartButton.enabled = true;
        NextButton.enabled = true;
        EndButton.enabled = true;

        // ニューゲームボタン選択
        StartButton.GetComponent<Button>().Select();
        return;
    }
}
