/*
 ===================
 ����F���
 UI�A�j���[�V�����̍쓮�^�C�~���O���Ǘ�����X�N���v�g
 ===================
 */

using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//- UI�A�j���[�V�����쓮���Ǘ�����N���X
public class AnimeManager : MonoBehaviour
{
    /*�@�񋓑̐錾���@*/
    
    /*�@�ϐ��錾���@*/
    [SerializeField] private EntryAnime DrawSelect;
    [SerializeField] private EntryAnime DrawReset;
    [SerializeField] private EntryAnime DrawTips;
    //[SerializeField] private GameObject DrawCutIn;
    private bool MoveCompleat = false;

    private void Start()
    {
        //- �{�XTips���Ȃ��ꍇ
        if (!DrawTips)
        {
            DrawSelect.StartMove();
            DrawReset.StartMove();
        }
        else
        {
            //DrawCutIn
            DrawTips.StartMove();
            DrawSelect.StartMove();
            DrawReset.StartMove();
        }
    }

    void Update()
    {
        //- �N���A�����A�P�ދ��������Ă��Ȃ�
        if(SceneChange.bIsChange && !MoveCompleat)
        {
            //- �{�XTips���Ȃ��ꍇ
            if (!DrawTips)
            {
                DrawSelect.OutMove();
                DrawReset.OutMove();
            }
            else
            {
                DrawTips.OutMove();
                DrawSelect.OutMove();
                DrawReset.OutMove();
            }
            MoveCompleat = true;
        }
    }
}
