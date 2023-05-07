/*
 ===================
 ����F���
 �M�~�b�N�K�C�h�A�j���[�V�������Ǘ�����X�N���v�g
 ===================
 */

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
#if UNITY_EDITOR
//- �f�v���C����Editor�X�N���v�g������ƃG���[�BUNITY_EDITOR�Ŋ���
using UnityEditor;
#endif

public class BoarMove : MonoBehaviour
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

    [SerializeField] private Image image;
    [SerializeField] private GameObject movie;
    [SerializeField] private TextMeshProUGUI tmp;

    public void Start()
    {
        //- ������^��
        DOTween.Sequence()
            .Append(image.transform.DOMoveX(0, 0.3f));
        //- �{�^�������ꂽ��^�񒆂���E��

    }

 
    void Update()
    {
        
    }
}
