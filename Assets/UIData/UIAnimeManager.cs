using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class UIAnimeManager : MonoBehaviour
{
    private enum E_HowMove
    {
        Center,     //����
        Right,      //�E
        TopRight,   //�E��
        LowerRight, //�E��
        Left,       //��
        TopLeft,    //����
        LowerLeft,  //����
    }
    
    private enum E_Scaling
    {
        BigSize_4,  //4�{
        BigSize_3,  //3�{
        BigSize_2,  //2�{
        
        Default,    //1�{

        SmallSize_2,//1/2
        SmallSize_3,//1/3
        SmallSize_4 //1/4
    }


    [HeaderAttribute("---�g�k�ݒ�---")]
    [SerializeField, Header("�����T�C�Y")]
    private E_Scaling SetSize;
    [SerializeField, Header("�g�k�����܂ł̎���:float")]
    private float SizeTime = 1.0f;

    [HeaderAttribute("---�ړ��ݒ�---")]
    [SerializeField, Header("�����ʒu")]
    private E_HowMove StartPos;
    [SerializeField, Header("�����ʒu")]
    private E_HowMove EndPos;
    [SerializeField, Header("�ړ������܂ł̎���:float")]
    private float MoveTime = 1.0f;

    private Vector2 MoveEndPos;
    private Vector2 initSize;
    private Vector2 scall;

    public bool bUIMoveComplete;

    //========= ���W�̓o�^ ==============
    private readonly float CENTER = 0.0f;
    private readonly float RIGHT = Screen.width / 2.0f;
    private readonly float LEFT = -Screen.width / 2.0f;
    private readonly float TOP = Screen.height / 2.0f;
    private readonly float LOWER = -Screen.height / 2.0f;

    void Start()
    {
        //- �t���O��������
        bUIMoveComplete = false;
        //- Canvas���W���擾
        RectTransform trans = GetComponent<RectTransform>();
        //- �A�j���[�V����
        Move(trans);
    }

/// <summary>
/// �����Đ�
/// </summary>
/// <param name="trans"></param>
    private void Move(RectTransform trans)
    {
        //- �X�^�[�g�ʒu��ݒ�
        StartPosTransformation(trans);
        //- �I���ʒu��ݒ�
        EndPosTransformation(trans);
        //- �g�k�T�C�Y��ݒ�
        SizeSetting();
        //- �����T�C�Y����
        initSize = this.transform.localScale;
        //- �T�C�Y��ύX
        this.transform.localScale = scall;

        transform.DOScale(initSize, 1.0f)
            .OnComplete(() =>
            {
                //- �ړ�
                transform.DOLocalMove(MoveEndPos, MoveTime).SetEase(Ease.OutSine)
                    .OnComplete(()=>
                {
                    //- �ړ�������������true�ɂ���
                    bUIMoveComplete = true;
                });
            });


    }


    private void SizeSetting()
    {
        switch (SetSize)
        {
            case E_Scaling.BigSize_4:
                SizeSetting(4.0f);
                break;
            case E_Scaling.BigSize_3:
                SizeSetting(3.0f);
                break;
            case E_Scaling.BigSize_2:
                SizeSetting(2.0f);
                break;
            case E_Scaling.Default:
                SizeSetting(1.0f);
                break;
            case E_Scaling.SmallSize_2:
                SizeSetting(0.5f);
                break;
            case E_Scaling.SmallSize_3:
                SizeSetting(0.25f);
                break;
            case E_Scaling.SmallSize_4:
                SizeSetting(0.125f);
                break;
        }
    }

    private void SizeSetting(float size)
    {
        scall = new Vector3(
            this.gameObject.transform.localScale.x * size,
            this.gameObject.transform.localScale.y * size,
            0.0f);
    }

    /// <summary>
    /// �I���ʒu���v�Z����
    /// </summary>
    /// <param name="trans"></param>
    private void EndPosTransformation(RectTransform trans)
    {
        switch (EndPos)
        {
            case E_HowMove.Center:
                MoveEndPos = new Vector2(CENTER, CENTER);
                break;
            case E_HowMove.Right:
                MoveEndPos = new Vector2(RIGHT - trans.sizeDelta.x / 2, CENTER);
                break;
            case E_HowMove.TopRight:
                MoveEndPos = new Vector2(RIGHT - trans.sizeDelta.x / 2, TOP);
                break;
            case E_HowMove.LowerRight:
                MoveEndPos = new Vector2(RIGHT - trans.sizeDelta.x / 2, LOWER + trans.sizeDelta.y / 2);
                break;
            case E_HowMove.Left:
                MoveEndPos = new Vector2(LEFT + trans.sizeDelta.x / 2, CENTER);
                break;
            case E_HowMove.TopLeft:
                MoveEndPos = new Vector2(LEFT + trans.sizeDelta.x / 2, TOP);
                break;
            case E_HowMove.LowerLeft:
                MoveEndPos = new Vector2(LEFT + trans.sizeDelta.x / 2, LOWER + trans.sizeDelta.y / 2);
                break;
        }
    }

    /// <summary>
    /// �J�n�n�_�̍��W���v�Z����
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="pos"></param>
    private void StartPosTransformation(RectTransform trans)
    {
        switch (StartPos)
        {
            case E_HowMove.Center:
                trans.anchoredPosition = new Vector2(CENTER,CENTER);
                break;
            case E_HowMove.Right:
                trans.anchoredPosition = new Vector2(RIGHT - trans.sizeDelta.x / 2,CENTER);
                break;
            case E_HowMove.TopRight:
                trans.anchoredPosition = new Vector2(RIGHT - trans.sizeDelta.x / 2, TOP);
                break;
            case E_HowMove.LowerRight:
                trans.anchoredPosition = new Vector2(RIGHT - trans.sizeDelta.x / 2, LOWER + trans.sizeDelta.y / 2);
                break;
            case E_HowMove.Left:
                trans.anchoredPosition = new Vector2(LEFT + trans.sizeDelta.x / 2, CENTER);
                break;
            case E_HowMove.TopLeft:
                trans.anchoredPosition = new Vector2(LEFT + trans.sizeDelta.x / 2, TOP);
                break;
            case E_HowMove.LowerLeft:
                trans.anchoredPosition = new Vector2(LEFT + trans.sizeDelta.x / 2, LOWER + trans.sizeDelta.y / 2);
                break;
        }
    }

    public bool GetUIAnimeComplete()
    {
        return bUIMoveComplete;
    }

}
