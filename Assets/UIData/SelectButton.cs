using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SelectButton : MonoBehaviour
{
    [SerializeField, Header("シーン遷移先")]
    private SceneObject NextScene;

    //- スクリプト用の変数
    BGMManager bgmManager;
    SaveManager saveManager;

    void Start()
    {
        bgmManager  = GameObject.Find("BGMManager").GetComponent<BGMManager>();
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
    }
    
    public void MoveScene()
    {
        //- クリック音再生
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);
        GameObject.Find("FadeImage").GetComponent<ObjectFade>().SetFade(TweenColorFade.FadeState.In, 0.5f);
        //- シーンを変える前にBGMを消す
        DOVirtual.DelayedCall (0.5f, ()=> bgmManager.DestroyBGMManager()); 
        DOVirtual.DelayedCall (0.5f, ()=> SceneManager.LoadScene(NextScene));
    }

    public void StartNewGame()
    {
        //- セーブデータをリセット
        saveManager.ResetSaveData();
    }
}
