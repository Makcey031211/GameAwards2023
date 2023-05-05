using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStartNewGame : MonoBehaviour
{
    //- スクリプト用の変数
    SaveManager saveManager;

    void Start()
    {
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
    }

    public void StartNewGame()
    {
        //- はじめからを選択時、ゲームデータをリセット
        saveManager.ResetSaveData();
    }
}
