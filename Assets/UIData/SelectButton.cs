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
    SoundManager soundManager;

    void Start()
    {
        soundManager = GameObject.Find("BGMManager").GetComponent<SoundManager>();
    }
    
    private void MoveScene()
    {
        //- クリック音再生
        SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Click,1.0f,1.0f,false);
        //- シーンを変える前にBGMを消す
        soundManager.DestroyBGMManager();
        SceneManager.LoadScene(NextScene);
    }
}
