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
    /*�@�񋓑̐錾���@*/
    
    /*�@�ϐ��錾���@*/
    [SerializeField] private EntryAnime DrawSelect;
    [SerializeField] private EntryAnime DrawReset;
    [SerializeField] private FireBelt PlayerFireBelt;
    [SerializeField] private EntryAnime DrawTips;
    [SerializeField] private CutIn DrawCutIn;

    private bool NoCutIn = false;
    private bool InMoveCompleat = false;
    private bool OutMoveCompleat = false;
    
    private void Start()
    {
        //- �{�XTips���Ȃ��ꍇ
        if (!DrawCutIn)
        {
            DrawSelect.StartMove();
            DrawReset.StartMove();
            PlayerFireBelt.MoveLocation();
            NoCutIn = true;
        }
        //- �J�b�g�C��������Ƃ�
        else
        {   DrawCutIn.MoveCutIn();  }
    }

    void Update()
    {
        //- �J�b�g�C���L���̓o��
        if (!NoCutIn && CutIn.MoveCompleat && !InMoveCompleat)
        {
            DrawTips.StartMove();
            DrawSelect.StartMove();
            DrawReset.StartMove();
            DOTween.Sequence()
                .AppendInterval(0.5f)
                .OnComplete(()=> {  PlayerFireBelt.MoveLocation();    });
            InMoveCompleat = true;
        }

        //- �N���A�����A�P�ދ��������Ă��Ȃ�
        if (SceneChange.bIsChange && !OutMoveCompleat)
        {
         
            //- �{�X�J�b�g�C�������邩
            if (!DrawCutIn)
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
            OutMoveCompleat = true;
        }
    }
}
