/*
 ===================
 ����F���
 UI�A�j���[�V�����̍쓮�^�C�~���O���Ǘ�����X�N���v�g
 ===================
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
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

    [SerializeField] private float OptionTime = 0.0f;

    private Dictionary<string, bool> ControlFlag;
    private bool InMoveCompleat = false;
    private bool OutMoveCompleat = false;
    private bool Load = false;
    private bool TipsInLoad = false;
    private bool TipsOutLoad = false;
    private InputAction tipsAction;
    private PController player;
    private Image imageTips;
    private float PushButton = 0.0f;
    private void Awake()
    {
        //- �A�j���Ǘ�����I�u�W�F�N�g�t���O������
        ControlFlag = new Dictionary<string, bool>
                    {
                        { "�Z���N�g", false },
                        { "���Z�b�g", false },
                        { "�M�~�b�N�\��",false },
                        { "�ʏ�J��", false },
                        { "�M�~�b�N�Ŕ�", false },
                        { "�{�X���o", false },
                    };
        //- �I�u�W�F�N�g�����݂�����t���O�ύX
        if (DrawSelect) { ControlFlag["�Z���N�g"] = true; }
        if (DrawReset) { ControlFlag["���Z�b�g"] = true; }
        if (DrawTips) { ControlFlag["�M�~�b�N�\��"] = true; }
        if (DrawOpening) { ControlFlag["�ʏ�J��"] = true; }
        if (DrawGimmickBoard) { ControlFlag["�M�~�b�N�Ŕ�"] = true; }
        if (DrawBossCutIn) { ControlFlag["�{�X���o"] = true; }

        player = GameObject.Find("Player").GetComponent<PController>();
        if (ControlFlag["�M�~�b�N�\��"])
        {   imageTips = GameObject.Find("DrawTipsGage").GetComponent<Image>();  }
    }

    private void Start()
    {
        //- �M�~�b�N���o������A���߂ĕ`�悷�邩
        if (ControlFlag["�M�~�b�N�Ŕ�"] && !BoardMove.MoveComplete)
        {
            DrawGimmickBoard.StartMove();
        }
        //- �ʏ�J��������A���߂ĕ`�悷�邩
        else if (ControlFlag["�ʏ�J��"] && !OpeningAnime.MoveCompleat)
        { DrawOpening.StartMove(); }
    }

    void Update()
    {
        //- �����t���O���擾����
        bool PlayerBoom = player.GetIsOnce();

        /*�@�@�J�n���o�̔���@�@*/
        //- �{�X���o���I����Ă���A�{�^���A�V�X�g��\�����Ă��Ȃ�
        if (CutIn.MoveCompleat && !InMoveCompleat)
        {
            InGameDrawObjs();
            InMoveCompleat = true;
        }

        //- �{�X���o�����݂���
        if (ControlFlag["�{�X���o"] && !InMoveCompleat)
        {
            //- �M�~�b�N�Ŕ̏������������Ă����珈��
            if (BoardMove.MoveComplete)
            {
                //- �{�X���o���s���Ă��Ȃ��Ȃ珈��
                if (!CutIn.MoveCompleat && !Load)
                {
                    DrawBossCutIn.MoveCutIn();
                    Load = true;
                }
                //- �{�X���o���s���Ă�����{�^���A�V�X�g��\������
                else if (CutIn.MoveCompleat && Load)
                { InGameDrawObjs(); }
            }
        }
        //- �ʏ�J����\�����Ă��Ȃ�
        else if (ControlFlag["�M�~�b�N�Ŕ�"] && !InMoveCompleat)
        {
            //- �M�~�b�N�������s�����珈��
            if (BoardMove.MoveComplete && !OpeningAnime.MoveCompleat)
            { DrawOpening.StartMove(); }
            //- �ʏ�J�����I��������{�^���A�V�X�g��\��
            if (OpeningAnime.MoveCompleat)
            { InGameDrawObjs(); }
        }
        //- �{�X���o���M�~�b�N�Ŕ��Ȃ��ꍇ
        else if (!ControlFlag["�{�X���o"] && !ControlFlag["�M�~�b�N�Ŕ�"] && !InMoveCompleat)
        {
            //- �ʏ�J�����s��
            if (ControlFlag["�ʏ�J��"] && !OpeningAnime.MoveCompleat)
            { DrawOpening.StartMove(); }
            //- �{�^���A�V�X�g��\��
            else if (OpeningAnime.MoveCompleat)
            { InGameDrawObjs(); }
        }

        /*�@�@Tips�r���`��̔���@�@*/
        //- �ēo��
        //Tips������A�J�����o���I����Ă���A�������Ă��Ȃ��A�ēo��t���O�������Ă��� �ŏ���Tips�`�悪�I����Ă���
        if (ControlFlag["�M�~�b�N�Ŕ�"] && OpeningAnime.MoveCompleat && !PlayerBoom && DrawGimmickBoard.GetInMove())
        {
            if (!TipsInLoad && DrawGimmickBoard.GetOutMoveComplet())
            {
                TipsInLoad = true;
                DrawGimmickBoard.StartMove();
                TipsOutLoad = false;
            }
        }
        else if(ControlFlag["�M�~�b�N�Ŕ�"] && CutIn.MoveCompleat && !PlayerBoom && DrawGimmickBoard.GetInMove())
        {
            if (!TipsInLoad && DrawGimmickBoard.GetOutMoveComplet())
            {
                TipsInLoad = true;
                DrawGimmickBoard.StartMove();
                TipsOutLoad = false;
            }
        }
        //- �ēP��
        //Tips������A�������Ă��Ȃ��A�ʏ�J�����{�X���o���s���Ă���
        if(ControlFlag["�M�~�b�N�Ŕ�"] && !PlayerBoom && !TipsOutLoad )
        {
            if (DrawGimmickBoard.GetInMoveComplet() && DrawGimmickBoard.GetOutMove())
            {
                PushButton += Time.deltaTime;
                imageTips.fillAmount = PushButton / OptionTime;
            }
            else if(!DrawGimmickBoard.GetOutMove())
            {
                PushButton = 0.0f;
                imageTips.fillAmount = 0.0f;
            }
            if(PushButton >= OptionTime)
            {
                TipsOutLoad = true;
                DrawGimmickBoard.OutMove();
                TipsInLoad = false;
            }
        }
        //- �v���C���[�����������狭���I�ɓP�ނ�����
        if(ControlFlag["�M�~�b�N�Ŕ�"] && PlayerBoom && DrawGimmickBoard.GetInMove())
        { DrawGimmickBoard.OutMove(); }


        /*�@�@�P�ދ����̔���@�@*/
        //- �N���A�����A�P�ދ��������Ă��Ȃ�
        if (SceneChange.bIsChange && !OutMoveCompleat)
        {
            if (ControlFlag["�Z���N�g"]) { DrawSelect.OutMove(); }
            if (ControlFlag["���Z�b�g"]) { DrawReset.OutMove(); }
            if (ControlFlag["�M�~�b�N�\��"]) { DrawTips.OutMove(); }
            OutMoveCompleat = true;
        }
    }

    /// <summary>
    /// �{�^���A�V�X�g��\������
    /// </summary>
    private void InGameDrawObjs()
    {
        if (ControlFlag["�Z���N�g"]) { DrawSelect.StartMove(); }
        if (ControlFlag["���Z�b�g"]) { DrawReset.StartMove(); }
        if (ControlFlag["�M�~�b�N�\��"]) { DrawTips.StartMove(); }
        InMoveCompleat = true;
    }
}

