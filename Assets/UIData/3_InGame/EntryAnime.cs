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
    }

    private const float LEFT = -300.0f;
    private const float RIGHT = 2500.0f;

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
        //- �����ʒu��^���Ɉړ�������
        transform.position = new Vector3(pos.x, -150.0f, pos.z);
    }

    void Start()
    {        
        //- ��ʓ��ɓo�ꂷ��
        transform.DOMoveY(pos.y, MoveTime).SetDelay(DelayTime);
    }

    
    void Update()
    {
        if(SceneChange.bIsChange && !first)
        {
            switch (direction)
            {
                case E_OUTDIRECTION.LEFT:
                    transform.DOMoveX(LEFT, EndMoveTime).OnComplete(()=> {
                        Destroy(gameObject);
                    });
                    first = true;
                    break;
                case E_OUTDIRECTION.RIGHT:
                    transform.DOMoveX(RIGHT,EndMoveTime).OnComplete(() => {
                        Destroy(gameObject);
                    });
                    first = true;
                    break;
            }
         
        }
    }
}
