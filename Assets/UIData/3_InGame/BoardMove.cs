/*
 ===================
 ����F���
 �M�~�b�N�K�C�h�A�j���[�V�������Ǘ�����X�N���v�g
 ===================
 */

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Video;
#if UNITY_EDITOR
//- �f�v���C����Editor�X�N���v�g������ƃG���[�BUNITY_EDITOR�Ŋ���
using UnityEditor;
#endif

//- �M�~�b�N�����Ŕ̃A�j���[�V����
public class BoardMove : MonoBehaviour
{
    private enum E_OUTDIRECTION
    {
        [Header("��")]
        LEFT,
        [Header("�E")]
        RIGHT,
        [Header("��")]
        UP,
        [Header("��")]
        DOWN,
    }

    private const float LEFT = -2500.0f;
    private const float RIGHT = 3500.0f;
    private const float TOP = 1200.0f;
    private const float DOWN = -1200.0f;

    [SerializeField] private Image img;
    [SerializeField] private VideoPlayer movie;
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private float IntervalTime = 0.0f;
    private Dictionary<string, Dictionary<string, Vector3>> InitValues;
    public static bool MoveComplete = false;

    //- ��Ό��炷
    private bool First = true;
    private bool fReInMove = false;     //�ēo��t���O
    private bool fReOutMove = false;    //�ēP�ރt���O
    private bool Inloaded = false;      //�ǂݍ��ݍς�
    private bool Outloaded = false;
    private bool InComplete = false;    //�J�n���������t���O
    private bool OutComplete = false;   //�P�ޏ��������t���O


    private void Awake()
    {
        //- �����l�o�^
        InitValues = new Dictionary<string, Dictionary<string, Vector3>>
        {{"����",new Dictionary<string, Vector3>{{"�ʒu",movie.transform.position},}}};
        InitValues.Add("����", new Dictionary<string, Vector3> { { "�ʒu", tmp.transform.position } });
        InitValues.Add("�w�i", new Dictionary<string, Vector3> { { "�ʒu", img.transform.position } });
        //- �����ʒu�X�V
        img.transform.localPosition = new Vector3(LEFT + img.transform.localPosition.x, img.transform.localPosition.y);
        movie.transform.localPosition = new Vector3(LEFT + img.transform.localPosition.x, movie.transform.localPosition.y);
        tmp.transform.localPosition = new Vector3(LEFT + img.transform.localPosition.x, tmp.transform.localPosition.y);
        //- �����~
        movie.Pause();

        if(MoveComplete)
        { OutComplete = true; }
    }
    
    /// <summary>
    /// �o�ꋓ�����s��
    /// </summary>
    public void StartMove()
    {
        // �����Ăяo���@�C��
        if(!Inloaded)
        {
            GameObject.Find("Player").GetComponent<PController>().SetWaitFlag(true);
            //- ��Ō��炷
            Inloaded = true;
            Outloaded = false;
            fReInMove = true;
            fReOutMove = false;
            OutComplete = false;

            movie.Play();
            //- ������^��
            var InAnime = DOTween.Sequence();
            InAnime.AppendInterval(IntervalTime)
            .Append(img.transform.DOMove(InitValues["�w�i"]["�ʒu"], 0.5f))
            .Join(movie.transform.DOMove(InitValues["����"]["�ʒu"], 0.525f))
            .Join(tmp.transform.DOMove(InitValues["����"]["�ʒu"], 0.5f))
            .OnComplete(() => {
                InComplete = true;
                InAnime.Kill();
            });
        }
    }

    /// <summary>
    /// �P�ދ������s��
    /// </summary>
    public void OutMove()
    {
        if (!Outloaded)
        {
            if(!First)
            {   GameObject.Find("Player").GetComponent<PController>().SetWaitFlag(false);   }
            
            //- ��΂���ȕK�v�Ȃ�
            fReInMove = false;
            Outloaded = true;
            Inloaded = false;
            fReOutMove = true;
            InComplete = false;
            movie.Stop();

            var OutAnime = DOTween.Sequence();
                OutAnime.Append(movie.transform.DOMoveX(RIGHT, 0.3f))
                .Join(img.transform.DOMoveX(RIGHT, 0.3f))
                .Join(tmp.transform.DOMoveX(RIGHT, 0.3f))
                .OnComplete(() =>
                {
                    OutComplete = true;
                    //- �����ʒu�X�V
                    img.transform.localPosition = new Vector3(LEFT, img.transform.localPosition.y);
                    movie.transform.localPosition = new Vector3(LEFT, movie.transform.localPosition.y);
                    tmp.transform.localPosition = new Vector3(LEFT, tmp.transform.localPosition.y);
                    MoveComplete = true;
                    OutAnime.Kill();
                });
            First = false;
        }
    }

    /// <summary>
    ///  �`��t���O�����Z�b�g����
    /// </summary>
    public static void ResetMoveComplete()
    {   MoveComplete = false;    }


    /// <summary>
    /// �ēo��t���O�ԋp
    /// </summary>
    public bool GetInMove()
    {   return fReInMove;     }

    /// <summary>
    /// �ēP�ރt���O�ԋp
    /// </summary>
    public bool GetOutMove()
    { return fReOutMove;      }  

    public bool GetInMoveComplet()
    { return InComplete; }

    public bool GetOutMoveComplet()
    { return OutComplete; }

    /// <summary>
    /// �ēo�����
    /// </summary>
    /// <param name="context"></param>
    public void OnInTips(InputAction.CallbackContext context)
    {
        //- Tips�ĕ`��t���O���I���ɂ���
        if (context.started && !SceneChange.bIsChange)
        {
            IntervalTime = 0.0f;
            fReInMove = true;
        }
    }

    /// <summary>
    /// �ēP�ޓ���
    /// </summary>
    /// <param name="context"></param>
    public void OnOutTips(InputAction.CallbackContext context)
    {
        //- Tips�ēP�ރt���O���I���ɂ���
        if (context.started && !SceneChange.bIsChange)
        {   fReOutMove = true;  }
        if(context.canceled && !SceneChange.bIsChange)
        {   fReOutMove = false; }
        
    }

    
}

