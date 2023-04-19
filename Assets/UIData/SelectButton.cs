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
    SoundManager soundManager;

    void Start()
    {
        soundManager = GameObject.Find("BGMManager").GetComponent<SoundManager>();
    }
    
    private void MoveScene()
    {
        //- �N���b�N���Đ�
        SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Click,1.0f,1.0f,false);
        //- �V�[����ς���O��BGM������
        soundManager.DestroyBGMManager();
        SceneManager.LoadScene(NextScene);
    }
}
