using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EntryAnime : MonoBehaviour
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

    private const float LEFT = -300.0f;
    private const float RIGHT = 2500.0f;
    private const float TOP = 1200.0f;
    private const float DOWN = -1200.0f;

    [SerializeField, Header("�o�ꎞ�̕���")]
    private E_OUTDIRECTION Sdirection = E_OUTDIRECTION.UP;
    [SerializeField,Header("�o�ꎞ�̈ړ��J�n�܂ł̎���")]
    private float DelayTime;
    [SerializeField, Header("�o�ꎞ�̈ړ�����")]
    private float MoveTime;
    [SerializeField, Header("�ޏꎞ�̕���")]
    private E_OUTDIRECTION direction = E_OUTDIRECTION.LEFT;
    [SerializeField, Header("�ޏꎞ�̈ړ�����")]
    private float EndMoveTime;

    private Vector3 pos;
    private bool first = false;

    
    private void Awake()
    {
        //- �z�u�ʒu���L��
        pos = transform.position;
        //- �����ʒu���w������Ɉړ�������
        switch (Sdirection)
        {
            case E_OUTDIRECTION.LEFT:
                transform.position = new Vector3(LEFT, pos.y, pos.z);
                break;
            case E_OUTDIRECTION.RIGHT:
                transform.position = new Vector3(RIGHT, pos.y, pos.z);
                break;
            case E_OUTDIRECTION.UP:
                transform.position = new Vector3(pos.x, TOP, pos.z);
                break;
            case E_OUTDIRECTION.DOWN:
                transform.position = new Vector3(pos.x, DOWN, pos.z);
                break;
        }

    }

    void Start()
    {
        //- ��ʓ��ɓo�ꂷ��
        switch (Sdirection)
        {   
            case E_OUTDIRECTION.LEFT:
                transform.DOMoveX(pos.x, MoveTime).SetDelay(DelayTime);
                break;
            case E_OUTDIRECTION.RIGHT:
                transform.DOMoveX(pos.x, MoveTime).SetDelay(DelayTime);
                break;
            case E_OUTDIRECTION.UP:
                transform.DOMoveY(pos.y, MoveTime).SetDelay(DelayTime);
                break;
            case E_OUTDIRECTION.DOWN:
                transform.DOMoveY(pos.y, MoveTime).SetDelay(DelayTime);
                break;
        }
    }

    
    void Update()
    {
        //- �N���A�t���O�������Ă���A���߂ēǂݍ���
        if(SceneChange.bIsChange && !first)
        {   OutMove();  }
    }

    /// <summary>
    /// �Ăяo���ꂽ��P�ދ������s��
    /// </summary>
    public void OutMove()
    {
        //- �w������ɓP��
        switch (direction)
        {
            case E_OUTDIRECTION.LEFT:
                transform.DOMoveX(LEFT, EndMoveTime).OnComplete(() =>
                {
                    //- �ړ�����������폜
                    Destroy(gameObject);
                });
                first = true;
                break;
            case E_OUTDIRECTION.RIGHT:
                transform.DOMoveX(RIGHT, EndMoveTime).OnComplete(() =>
                {
                    //- �ړ�����������폜
                    Destroy(gameObject);
                });
                first = true;
                break;
        }
    }
}
