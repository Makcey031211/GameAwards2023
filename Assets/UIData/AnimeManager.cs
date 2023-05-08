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
    [SerializeField] private OpeningAnime DrawOpening;
    [SerializeField] private CutIn DrawBossCutIn;
    [SerializeField] private BoardMove DrawGimmickBoard;

    private Dictionary<string, bool> ControlFlag;
    private bool BoardMoveCompleat = false;
    private bool InMoveCompleat = false;
    private bool OutMoveCompleat = false;

    private void Awake()
    {
        //- �A�j���Ǘ�����I�u�W�F�N�g�t���O������
        ControlFlag = new Dictionary<string, bool>
                    {
                        { "�Z���N�g", false },
                        { "���Z�b�g", false },
                        { "�`�b�v�X", false },
                        { "�ʏ�J��", false },
                        { "�M�~�b�N�Ŕ�", false },
                        { "�{�X���o", false },
                    };
       //- �I�u�W�F�N�g�����݂�����t���O�ύX
        if (DrawSelect)         { ControlFlag["�Z���N�g"] = true; }
        if (DrawReset)          { ControlFlag["���Z�b�g"] = true; }
        if (DrawTips)           { ControlFlag["�`�b�v�X"] = true; }
        if (DrawOpening)        { ControlFlag["�ʏ�J��"] = true; }
        if (DrawGimmickBoard)   { ControlFlag["�M�~�b�N�Ŕ�"] = true; }
        if (DrawBossCutIn)      { ControlFlag["�{�X���o"] = true; }
    }

    private void Start()
    {
        if (ControlFlag["�ʏ�J��"])
        { DrawOpening.MoveDescription();  }
        else if(ControlFlag["�{�X���o"])
        {   DrawBossCutIn.MoveCutIn();  }
    }

    void Update()
    {
        //- �ʏ퉉�o������A�ʏ퉉�o���I�����Ă���A�ȉ��̕�������s���Ă��Ȃ�
        if(ControlFlag["�ʏ�J��"] && DrawOpening.GetMoveComplete() && !BoardMoveCompleat) 
        {
            if (ControlFlag["�Z���N�g"]) { DrawSelect.StartMove(); }
            if (ControlFlag["���Z�b�g"]) { DrawReset.StartMove(); }
            if (ControlFlag["�M�~�b�N�Ŕ�"]) { DrawGimmickBoard.StartMove(); }
            BoardMoveCompleat = true;
        }
        //- �{�X���o������A�ʏ퉉�o���I�����Ă���A�ȉ��̕�������s���Ă��Ȃ�
        else if (ControlFlag["�{�X���o"] && DrawBossCutIn.GetMoveComplete() && !InMoveCompleat)
        {
            if (ControlFlag["�`�b�v�X"]) { DrawTips.StartMove(); }
            if (ControlFlag["�Z���N�g"]) { DrawSelect.StartMove(); }
            if (ControlFlag["���Z�b�g"]) { DrawReset.StartMove(); }
            if (ControlFlag["�M�~�b�N�Ŕ�"]) { DrawGimmickBoard.StartMove(); }
            InMoveCompleat = true;
        }
        
        //- �N���A�����A�P�ދ��������Ă��Ȃ�
        if (SceneChange.bIsChange && !OutMoveCompleat)
        {
            if (ControlFlag["�`�b�v�X"]) { DrawTips.OutMove(); }
            if (ControlFlag["�Z���N�g"]) { DrawSelect.OutMove(); }
            if (ControlFlag["���Z�b�g"]) { DrawReset.OutMove(); }
            OutMoveCompleat = true;
        }
    }
}
