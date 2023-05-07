/*
 ===================
 ����F���
 UI�A�j���[�V�����̍쓮�^�C�~���O���Ǘ�����X�N���v�g
 ===================
 */

using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
#if UNITY_EDITOR
using UnityEditor;
#endif

//- UI�A�j���[�V�����쓮���Ǘ�����N���X
public class AnimeManager : MonoBehaviour
{
    /*�@�ϐ��錾���@*/
    [SerializeField] private EntryAnime DrawSelect;
    [SerializeField] private EntryAnime DrawReset;
    [SerializeField] private EntryAnime DrawTips;
    [SerializeField] private TargetDescription DrawDescription;
    [SerializeField] private CutIn DrawBossCutIn;
    [SerializeField] private BoardMove board;

    private bool BoardMoveVompleat = false;
    private bool InMoveCompleat = false;
    private bool OutMoveCompleat = false;
    
    private void Start()
    {
        //- �ڕW�J�n���o������
        if (DrawDescription)
        {   DrawDescription.MoveDescription();  }
        //- �{�X�J�b�g�C��������
        else
        {   DrawBossCutIn.MoveCutIn();  }
    }

    void Update()
    {
        //- �M�~�b�N�����̃A�j������
        if(TargetDescription.MoveCompleat && !BoardMoveVompleat)
        {
            DrawSelect.StartMove();
            DrawReset.StartMove();
            if(board)
            { board.StartMove(); }
            BoardMoveVompleat = true;
        }
        else if(CutIn.MoveCompleat && !InMoveCompleat)
        {
            DrawTips.StartMove();
            DrawSelect.StartMove();
            DrawReset.StartMove();
            if (board)
            { board.StartMove(); }
            //DOTween.Sequence().AppendInterval(0.5f);
            InMoveCompleat = true;
        }

        
        //- �N���A�����A�P�ދ��������Ă��Ȃ�
        if (SceneChange.bIsChange && !OutMoveCompleat)
        {
            if (DrawDescription)
            {
                DrawSelect.OutMove();
                DrawReset.OutMove();
            }
            else if(DrawBossCutIn)
            {
                DrawTips.OutMove();
                DrawSelect.OutMove();
                DrawReset.OutMove();
            }
            OutMoveCompleat = true;
        }
    }
}
