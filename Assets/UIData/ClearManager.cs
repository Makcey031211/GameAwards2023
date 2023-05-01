using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearManager : MonoBehaviour
{
    [SerializeField, Header("�X�e�[�W��")]
    private int stagenum = -1;
    private SaveManager save;
    private bool write = false;
    
    /// <summary>
    /// �Z�[�u�t�@�C���ɏ������݂��s���邩�`�F�b�N
    /// </summary>
    public void WriteClear()
    {
        //- �N���A�t���O�������Ă��邩
        if(SceneChange.bIsChange && !write)
        {
            save = new SaveManager();
            write = true;
            save.SetStageClear(stagenum);
            Debug.Log("��������" + "," + stagenum + "," + save.GetStageClear(stagenum));
        }
    }
}
