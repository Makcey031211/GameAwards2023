using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStartNewGame : MonoBehaviour
{
    //- �X�N���v�g�p�̕ϐ�
    SaveManager saveManager;

    void Start()
    {
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
    }

    public void StartNewGame()
    {
        //- �͂��߂����I�����A�Q�[���f�[�^�����Z�b�g
        saveManager.ResetSaveData();
    }
}
