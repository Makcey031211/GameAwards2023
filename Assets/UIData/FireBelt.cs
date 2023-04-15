using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FireBelt : MonoBehaviour
{
    [SerializeField,Header("現在地からどこまで移動するか")]
    private float AddPosY;
    [SerializeField, Header("移動時間")]
    private float MoveTime;
    [SerializeField, Header("軌跡収縮時間")]
    private float DeleteTIme;
    [SerializeField, Header("フェード時間")]
    private float FadeTIme;
    [SerializeField, Header("別オブジェクトの位置に生成する")]
    private bool ExceptionObj = false;
    [SerializeField, Header("生成したい位置にあるオブジェクト")]
    private GameObject PentObj;
    [SerializeField, Header("帯SE")]
    private AudioClip beltSE;
    [SerializeField, Header("SEの音量")]
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
    /// 指定位置に移動する
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
            .OnPlay(() => { SEManager.Instance.SetPlaySE(beltSE,seVolume); }) // 帯音再生
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
