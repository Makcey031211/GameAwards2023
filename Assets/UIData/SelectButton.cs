using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectButton : MonoBehaviour
{
    [SerializeField, Header("�V�[���J�ڐ�")]
    private SceneObject NextScene;

    //- �X�N���v�g�p�̕ϐ�
    BGMManager bgmManager;

    void Start()
    {
        bgmManager = GameObject.Find("BGMManager").GetComponent<BGMManager>();
    }
    
    public void MoveScene()
    {
        //- �N���b�N���Đ�
        SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Click,1.0f,false);
        //- �V�[����ς���O��BGM������
        bgmManager.DestroyBGMManager();
        SceneManager.LoadScene(NextScene);
    }
}
