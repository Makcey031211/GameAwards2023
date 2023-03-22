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
    
    public void MoveScene()
    {
        //- シーンを変える前にBGMを消す
        soundManager.DestroyBGMManager();
        SceneManager.LoadScene(NextScene);
    }
}
