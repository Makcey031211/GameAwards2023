using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FireBelt : MonoBehaviour
{
    [SerializeField,Header("���ݒn����ǂ��܂ňړ����邩")]
    private float AddPosY;
    [SerializeField, Header("�ړ�����")]
    private float MoveTime;
    [SerializeField, Header("�O�Վ��k����")]
    private float DeleteTIme;
    [SerializeField, Header("�t�F�[�h����")]
    private float FadeTIme;
    [SerializeField, Header("�ʃI�u�W�F�N�g�̈ʒu�ɐ�������")]
    private bool ExceptionObj = false;
    [SerializeField, Header("�����������ʒu�ɂ���I�u�W�F�N�g")]
    private GameObject PentObj;
    [SerializeField, Header("��SE")]
    private AudioClip beltSE;
    [SerializeField, Header("SE�̉���")]
    private float seVolume = 1.0f;

    private Image img;
    private Slider sli;
    private bool MoveComplete = false;
    private float DiffPosY = 7.0f;
    void Start()
    {
        img = GetComponent<Image>();
        sli = GetComponent<Slider>();
        img.transform.localPosition 
            = new Vector3( PentObj.transform.localPosition.x, img.transform.localPosition.y, img.transform.localPosition.z);
        MoveToTarget();
    }

    /// <summary>
    /// �w��ʒu�Ɉړ�����
    /// </summary>
    private void MoveToTarget()
    {
        float pos = img.transform.localPosition.y;
        float TargetPos;

        if (ExceptionObj)
        {   TargetPos = PentObj.transform.localPosition.y;  }
        else
        { TargetPos = pos + AddPosY; }

        img.DOFillAmount(0, DeleteTIme)
            .SetEase(Ease.InOutQuad)//Inexpo
            .SetLink(this.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable);
        transform
            .DOLocalMoveY(TargetPos - DiffPosY, MoveTime)
            .SetEase(Ease.OutCubic)
            .SetLink(this.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable)
            .OnPlay(() => { SEManager.Instance.SetPlaySE(beltSE,seVolume); }) // �щ��Đ�
            .OnComplete(() =>
            { MoveComplete = true; });
        img.DOFade(0, FadeTIme)
            .SetEase(Ease.OutSine)
            .SetLink(this.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable);
    }

    public bool GetMoveComplete()
    {
        return MoveComplete;
    }
}
