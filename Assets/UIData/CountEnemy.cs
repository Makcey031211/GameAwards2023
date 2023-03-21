using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountEnemy : MonoBehaviour
{
    [SerializeField, Header("カウントを表示するTMPro")]
    private TextMeshProUGUI CountDrowText;

    [SerializeField, Header("カウントするタグの選択")]
    private CountTag SelectedTag;

    private int InitCountNum;
    private int CurrentCountNum;
    private int OldCountNum;

    public enum CountTag
    {
        Fireworks,  //花火
        Player,     //プレイヤー

        Untagged    //未選択タグ
    }


    
    void Start()
    {
        GameObject[] SelectObject =
            GameObject.FindGameObjectsWithTag(SelectedTag.ToString());
        InitCountNum = SelectObject.Length;
        CurrentCountNum = InitCountNum;
        OldCountNum = InitCountNum;

        string CountTextLabel = "";

        switch (SelectedTag)
        {
            case CountTag.Fireworks:
                CountTextLabel = "花火玉";
                break;
            case CountTag.Player:
                CountTextLabel = "残機";
                break;
            case CountTag.Untagged:
                CountTextLabel = "NoTag";
                break;
        }

        CountDrowText.text = string.Format("残りの{0}：{1}", CountTextLabel, CurrentCountNum);
    }

    /// <summary>
    /// SelectTagで選択されたタグを数え、TMProに描画する
    /// </summary>
    void Update()
    {
        GameObject[] SelectObject =
                GameObject.FindGameObjectsWithTag(SelectedTag.ToString());
        CurrentCountNum = SelectObject.Length;

        if (CurrentCountNum < OldCountNum)
        {
            string CountTextLabel = "";
            switch (SelectedTag)
            {
                case CountTag.Fireworks:
                    CountTextLabel = "花火玉";
                    break;
                case CountTag.Player:
                    CountTextLabel = "残機";
                    break;
            }

            CountDrowText.text = string.Format("残りの{0}：{1}", CountTextLabel, CurrentCountNum);
            OldCountNum = CurrentCountNum;
        }
    }

    public int GetCurrentCountNum()
    {
        return CurrentCountNum;
    }
}
