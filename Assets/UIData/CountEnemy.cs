using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountEnemy : MonoBehaviour
{
    [SerializeField, Header("�J�E���g��\������TMPro")]
    private TextMeshProUGUI CountDrowText;

    [SerializeField, Header("�J�E���g����^�O�̑I��")]
    private CountTag SelectedTag;

    private int InitCountNum;
    private int CurrentCountNum;
    private int OldCountNum;

    public enum CountTag
    {
        Fireworks,  //�ԉ�
        Player,     //�v���C���[

        Untagged    //���I���^�O
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
                CountTextLabel = "�ԉ΋�";
                break;
            case CountTag.Player:
                CountTextLabel = "�c�@";
                break;
            case CountTag.Untagged:
                CountTextLabel = "NoTag";
                break;
        }

        CountDrowText.text = string.Format("�c���{0}�F{1}", CountTextLabel, CurrentCountNum);
    }

    /// <summary>
    /// SelectTag�őI�����ꂽ�^�O�𐔂��ATMPro�ɕ`�悷��
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
                    CountTextLabel = "�ԉ΋�";
                    break;
                case CountTag.Player:
                    CountTextLabel = "�c�@";
                    break;
            }

            CountDrowText.text = string.Format("�c���{0}�F{1}", CountTextLabel, CurrentCountNum);
            OldCountNum = CurrentCountNum;
        }
    }

    public int GetCurrentCountNum()
    {
        return CurrentCountNum;
    }
}
