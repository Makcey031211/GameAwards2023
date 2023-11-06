using UnityEngine;
using UnityEngine.SceneManagement;

/*
 ===================
 制作：髙橋
 追記：寺前
 概要：BGMを管理するスクリプト
 ===================
 */
public class BGMManager : MonoBehaviour
{
    void Start()
    {
        int numMusicPlayers = FindObjectsOfType<BGMManager>().Length;
        if (numMusicPlayers > 1)
        { Destroy(gameObject); }// オブジェクトを破棄する
        else
        { DontDestroyOnLoad(gameObject); }// シーン遷移しても、オブジェクトを破棄しない
    }

    /// <summary>
    /// BGMを削除する
    /// </summary>
    public void DestroyBGMManager()
    { Destroy(gameObject); }

    /// <summary>
    /// BGMを削除可能状態にする
    /// </summary>
    public void DestroyPossible()
    {
        //- DontDestroyOnLoadに避難させたオブジェクトを削除可能にする
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
    }
}