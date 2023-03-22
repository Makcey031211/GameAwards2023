using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class ClearAnime : MonoBehaviour
{
    private enum E_Directions
    {
        CENTER,
        TOP,
        LOWER,
        RIGHT,
        LEFT,
    }

    [HeaderAttribute("---�ړ��ݒ�--")]
    [SerializeField, Header("�ړ����������s����:�`�F�b�N�Ŏ��s")]
    private bool UseMove = false;
    [SerializeField, Header("�ǂ����猻�݈ʒu�Ɍ������Ă��邩")]
    private E_Directions StartPos;
    [SerializeField, Header("�ړ������܂ł̎���:float")]
    private float MoveTime = 0.0f;
    [SerializeField, Header("�f�B���C:float")]
    private float Delay = 0.0f;
    private Vector2 InitPos;


    [HeaderAttribute("---�t�F�[�h�ݒ�---")]
    [SerializeField, Header("�ړ����������s����:�`�F�b�N�Ŏ��s")]
    private bool UseFade = false;
    [SerializeField, Header("�J�n�̃A���t�@�l")]
    private float StartAlpha = 1.0f;
    [SerializeField, Header("�I���̃A���t�@�l")]
    private float EndAlpha = 0.0f;
    [SerializeField, Header("�t�F�[�h�����܂ł̎���:float")]
    private float FadeTime = 0.0f;

    private void Start()
    {
        
        if (UseMove)
        {   PosMove();   }
        
        if(UseFade)
        {   DoFade();   }
    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    private void PosMove()
    {
        RectTransform trans = GetComponent<RectTransform>();
        //- �����ʒu��ۑ�
        InitPos = trans.anchoredPosition;
        //- ��Ԃɍ��킹�ăX�^�[�g�ʒu��ύX
        switch (StartPos)
        {
            case E_Directions.CENTER:
                trans.anchoredPosition = new Vector2(0.0f, 0.0f);
                break;
            case E_Directions.TOP:
                trans.anchoredPosition = new Vector2(InitPos.x, Screen.height);
                break;
            case E_Directions.LOWER:
                trans.anchoredPosition = new Vector2(InitPos.x, -Screen.height);
                break;
            case E_Directions.RIGHT:
                trans.anchoredPosition = new Vector2(Screen.width, InitPos.y);
                break;
            case E_Directions.LEFT:
                trans.anchoredPosition = new Vector2(-Screen.width, InitPos.y);
                break;
        }
        //- �����ʒu�ɂނ����Ĉړ�
        transform.DOLocalMove(InitPos, MoveTime)
            .SetEase(Ease.OutSine)
            .SetLink(this.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable)
            .SetDelay(Delay);
        
    }
 
    /// <summary>
    /// �t�F�[�h����
    /// </summary>
    private void DoFade()
    {
        Image image = GetComponent<Image>();
        //- �w�肵���A���t�@�l�ŊJ�n
        image.color = new Color(image.color.r, image.color.g, image.color.b, StartAlpha);
        //- �t�F�[�h���s��
        image.DOFade(EndAlpha,FadeTime).SetLink(image.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable);
       
    }
}
