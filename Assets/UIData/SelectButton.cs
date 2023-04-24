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

    void Start()
    {
        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
    }
    
    public void MoveScene()
    {
        //- クリック音再生
        SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Click,1.0f,false);
        GameObject.Find("FadeImage").GetComponent<ObjectFade>().SetFade(TweenColorFade.FadeState.In, 0.3f);
        //- シーンを変える前にBGMを消す
        DOVirtual.DelayedCall (0.3f, ()=> bgmManager.DestroyBGMManager()); 
        DOVirtual.DelayedCall (0.3f, ()=> SceneManager.LoadScene(NextScene));
    }
}
