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

    [SerializeField] private float OptionTime = 1.0f;

    private Dictionary<string, bool> ControlFlag;
    private bool InMoveCompleat = false;
    private bool OutMoveCompleat = false;
    private bool FirstLoad = false;
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
                        { "Tips�ĕ\��",false },
                        { "�J��", false },
                        { "Tips", false },
                        { "�{�X", false },
                    };
        //- �I�u�W�F�N�g�����݂�����t���O�ύX
        if (DrawSelect)       { ControlFlag["�Z���N�g"] = true;     }
        if (DrawReset)        { ControlFlag["���Z�b�g"] = true;     }
        if (DrawTips)         { ControlFlag["Tips�ĕ\��"] = true; }
        if (DrawOpening)      { ControlFlag["�J��"] = true;     }
        if (DrawGimmickBoard) { ControlFlag["Tips"] = true; }
        if (DrawBossCutIn)    { ControlFlag["�{�X"] = true;     }
        //- �v���C���[���擾
        player = GameObject.Find("Player").GetComponent<PController>();
        //- Tips��\������Ȃ�ATips�ĕ\���{�^�������擾
        if (ControlFlag["Tips�ĕ\��"]){   imageTips = GameObject.Find("DrawTipsGage").GetComponent<Image>();  }
    }

    private void Start()
    {
        //- Tips������E����`��
        if(ControlFlag["Tips"] && !BoardMove.MoveComplete)
        { DrawGimmickBoard.StartMove(); }//Tips��\��
        //- �J��������E����`��
        else if(ControlFlag["�J��"] && !OpeningAnime.MoveCompleat)
        { DrawOpening.StartMove(); }//�J����\��

        Debug.Log(OptionTime);
    }

    void Update()
    {
        //- �v���C���[�̎����t���O���擾����
        bool PlayerBoom = player.GetIsOnce();

        /*�@�����������@��ڂ̃A�j���[�V�������s�����@�����������@*/
        //- �{�X�X�e�[�W�E���o�����Ă��Ȃ��E���߂ď�������ETips���P�ނ��Ă���
        if(ControlFlag["�{�X"] && !CutIn.MoveCompleat && !FirstLoad && BoardMove.MoveComplete)
        {
            //- ���߂ēǂݍ���
            FirstLoad = true;
            //- �{�X���o���s��
            DrawBossCutIn.MoveCutIn();
        }
        //- �ʏ�X�e�[�W�E���o�����Ă��Ȃ��E���߂ď�������ETips���P�ނ��Ă���
        else if(ControlFlag["�J��"] && !OpeningAnime.MoveCompleat && !FirstLoad && BoardMove.MoveComplete)
        {
            //- ���߂ēǂݍ���
            FirstLoad = true;
            //- �J�����o���s��
            DrawOpening.StartMove();
        }

        /*�@�����������@��ڂ̃A�j���[�V�������I�����A�{�^���A�V�X�g��\������@�����������@*/
        //- �{�X�X�e�[�W�E���o�ςł���
        if(ControlFlag["�{�X"] && CutIn.MoveCompleat)
        {
            //- �A�V�X�g�\����Ɏg�p���Ȃ����߃t���O�ύX
            ControlFlag["�{�X"] = false;
            //- �{�^���A�V�X�g��\������
            InGameDrawObjs();
        }
        //- �ʏ�X�e�[�W�E���o�ςł���
        if(ControlFlag["�J��"] && OpeningAnime.MoveCompleat)
        {
            //- �A�V�X�g��Ɏg�p���Ȃ����߃t���O�ύX
            ControlFlag["�J��"] = false;
            //- �{�^���A�V�X�g��\��
            InGameDrawObjs();
        }

        /*�@�����������@Tips���Q�[�����ɕ\���E�P�ނ�����@�����������@*/
        //--- �o�ꏈ��
        //- Tips������E�{�X���o���I����Ă���E�������Ă��Ȃ��E�ēo��{�^�����͂�����Ă���
        if(ControlFlag["Tips"] && CutIn.MoveCompleat && !PlayerBoom && DrawGimmickBoard.GetInDrawButtonPush())
        {
            //-�@�v���C���[�𓮍�s�\�ɂ���
            GameObject.Find("Player").GetComponent<PController>().SetWaitFlag(true);
            DrawGimmickBoard.StartMove();
        }
        //- Tips������E�J�����I����Ă���E�������Ă��Ȃ��E�ēo��{�^�����͂�����Ă���
        else if (ControlFlag["Tips"] && OpeningAnime.MoveCompleat && !PlayerBoom && DrawGimmickBoard.GetInDrawButtonPush())
        {
            //-�@�v���C���[�𓮍�s�\�ɂ���
            GameObject.Find("Player").GetComponent<PController>().SetWaitFlag(true);
            DrawGimmickBoard.StartMove();
        }
        //--- �P�ޏ���
        //- Tips������E����Tips���\������Ă���E�������Ă��Ȃ��E�ēo��{�^�����͂�����Ă���
        if (ControlFlag["Tips"] && DrawGimmickBoard.GetLoadStart() && !PlayerBoom && DrawGimmickBoard.GetOutDrawButtonPush())
        {
            //- ���͌p�����Ԃ���
            PushButton += Time.deltaTime;
            //- ���ԕ��摜�l��ϓ�
            imageTips.fillAmount = PushButton / OptionTime;
        }
        //- �{�^�����͂�����Ă��Ȃ�
        else if(ControlFlag["Tips"] && !DrawGimmickBoard.GetOutDrawButtonPush())
        {
            PushButton = 0.0f;
            imageTips.fillAmount = 0.0f;
        }
        //- ���͎��Ԃ��w�莞�Ԃ��z������P�ޏ������s��
        if (PushButton >= OptionTime)
        {
            DrawGimmickBoard.OutMove();
            //- ����̓P�ނłȂ�������v���C���[����Ǘ����s��
            if(CutIn.MoveCompleat || OpeningAnime.MoveCompleat )
            {
                //-�@�v���C���[�𓮍�\�ɂ���
                GameObject.Find("Player").GetComponent<PController>().SetWaitFlag(false);
            }
        }

        //- �v���C���[�������������ATips���`�悳��Ă����狭���I�ɓP��
        if(PlayerBoom && ControlFlag["Tips"] && DrawGimmickBoard.GetLoadStart())
        { DrawGimmickBoard.OutMove(); }

        /*�@�����������@�N���A���ɓP�ދ������s���@�����������@*/
        if (SceneChange.bIsChange && !OutMoveCompleat)
        {
            if (ControlFlag["�Z���N�g"]) { DrawSelect.OutMove(); }
            if (ControlFlag["���Z�b�g"]) { DrawReset.OutMove(); }
            if (ControlFlag["Tips�ĕ\��"]) { DrawTips.OutMove(); }
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
        if (ControlFlag["Tips�ĕ\��"]) { DrawTips.StartMove(); }
        //-�@�v���C���[�𓮍�\�ɂ���
        GameObject.Find("Player").GetComponent<PController>().SetWaitFlag(false);
        InMoveCompleat = true;
    }
}

