using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectButton : MonoBehaviour
{
    [SerializeField, Header("シーン遷移先")]
    private SceneObject NextScene;

    //- スクリプト用の変数
    BGMManager bgmManager;

    void Start()
    {
        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
    }
    
    public void MoveScene()
    {
        //- クリック音再生
        SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Click,1.0f,false);
        //- シーンを変える前にBGMを消す
        bgmManager.DestroyBGMManager();
        SceneManager.LoadScene(NextScene);
    }
}
