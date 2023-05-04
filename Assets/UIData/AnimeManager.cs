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

    private static bool InMoveCompleat = false;
    private static bool OutMoveCompleat = false;
    
    private void Start()
    {
        InMoveCompleat = false;
        OutMoveCompleat = false;
        //- �{�XTips���Ȃ��ꍇ
        if (!DrawCutIn)
        {
            DrawSelect.StartMove();
            DrawReset.StartMove();
            PlayerFireBelt.MoveLocation();
        }
        else
        {   DrawCutIn.MoveCutIn();  }
    }

    void Update()
    {
        if( DrawSelect == null || 
            DrawReset  == null || 
            DrawTips == null)
        {   return; }
        //- �o��
        if (CutIn.MoveCompleat && !InMoveCompleat)
        {
            DrawTips.StartMove();
            DrawSelect.StartMove();
            DrawReset.StartMove();
            DOTween.Sequence().AppendInterval(0.5f).OnComplete(()=> {
                PlayerFireBelt.MoveLocation();
            });
            InMoveCompleat = true;
        }

        //- �N���A�����A�P�ދ��������Ă��Ȃ�
        if (SceneChange.bIsChange && !OutMoveCompleat)
        {
            OutMoveCompleat = true;
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
        }
    }
}
