using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MovieManager : MonoBehaviour
{
    [SerializeField, Header("フェードの長さ(秒)")]
    private float FadeTime;

    [SerializeField, Header("花火発生の遅延時間(秒)")]
    private float DelayFireflowerTime;

    [SerializeField, Header("花火の後、フェードが始まるまでの時間(秒)")]
    private float DelayFadeTime;

    [SerializeField, Header("ステージ描画オブジェクト")]
    private GameObject StageDrawObj;


    private SceneChange sceneChange; // コントローラーの振動用
    private bool bPlayMovie = false; // 演出中かどうか
    private ObjectFade fade; // フェード用のスプライト

    void Start()
    {
        //- メインカメラからシーン変更スクリプト取得
        sceneChange = GameObject.Find("Main Camera").GetComponent<SceneChange>();
        //- フェード用スクリプトの取得
        fade = GameObject.Find("FadeImage").GetComponent<ObjectFade>();
    }
    
    void Update()
    {
        if (bPlayMovie) sceneChange.ResetCurrentTime(); //- 演出時、クリア演出が出ないようにする
    }

    //- 演出フラグを変更する関数
    public void SetMovieFlag(bool bFlag)
    {
        bPlayMovie = bFlag;
    }

    //- 演出処理を開始する関数
    public void StartVillageMovie()
    {
        StartCoroutine(MovieSequence());
    }
    private IEnumerator MovieSequence()
    {
        bPlayMovie = true; //- 演出フラグ変更

        //- フェードを登場させる
        fade.SetFade(TweenColorFade.FadeState.In, FadeTime);
        yield return new WaitForSeconds(FadeTime);

        //- 演出シーンを追加ロード,フェードを退場させる
        LoadMovieScene();
        fade.SetFade(TweenColorFade.FadeState.Out, FadeTime);
        yield return new WaitForSeconds(FadeTime);

        //- 一定時間後、花火を発生させる
        yield return new WaitForSeconds(DelayFireflowerTime);
        SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Explosion, 1.0f, false);
        SetActiveFireflower(0, true);

        //- 一定時間後、フェードを登場させる
        yield return new WaitForSeconds(DelayFadeTime);
        fade.SetFade(TweenColorFade.FadeState.In, FadeTime);
        yield return new WaitForSeconds(FadeTime);

        //- 演出シーンをアンロード
        UnloadMovieScene();
        fade.SetFade(TweenColorFade.FadeState.Out, FadeTime);
        yield return new WaitForSeconds(FadeTime);

        bPlayMovie = false; //- 演出フラグ変更
    }

    //- 演出用シーンのロードを行う関数
    private void LoadMovieScene()
    {
        StageDrawObj.SetActive(false); //- ステージオブジェクトの描画をやめる
        SceneManager.LoadScene("MovieVillage", LoadSceneMode.Additive); //- 演出用シーンを追加ロード   
    }

    //- 特定のオブジェクトのフラグを変更する関数
    private void SetActiveFireflower(int childNum, bool bFlag)
    {
        GameObject obj = GameObject.Find("MovieObject"); //- オブジェクトの検索
        obj.transform.GetChild(childNum).gameObject.SetActive(bFlag); //- フラグ変更
    }

    //- 演出用シーンのアンロードを行う関数
    private void UnloadMovieScene()
    {
        StageDrawObj.SetActive(true); //- ステージオブジェクトの描画を再開
        SceneManager.UnloadScene("MovieVillage"); //- 演出用シーンのアンロード
    }
}
