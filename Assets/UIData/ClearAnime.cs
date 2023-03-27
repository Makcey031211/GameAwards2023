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

    [HeaderAttribute("---フェード設定---")]
    [SerializeField, Header("移動処理を実行する:チェックで実行")]
    private bool UseFade = false;
    [SerializeField, Header("開始のアルファ値")]
    private float StartAlpha = 1.0f;
    [SerializeField, Header("終了のアルファ値")]
    private float EndAlpha = 0.0f;
    [SerializeField, Header("フェード完了までの時間:float")]
    private float FadeTime = 0.0f;

    [HeaderAttribute("---移動設定--")]
    [SerializeField, Header("移動処理を実行する:チェックで実行")]
    private bool UseMove = false;
    [SerializeField, Header("どこから現在位置に向かってくるか")]
    private E_Directions StartPos;
    [SerializeField, Header("移動完了までの時間:float")]
    private float MoveTime = 0.0f;
    [SerializeField, Header("ディレイ:float")]
    private float Delay = 0.0f;
    private Vector2 InitPos;

    [HeaderAttribute("---ポップ設定---")]
    [SerializeField, Header("ポップ処理を実行する：チェックで実行")]
    private bool UsePop = false;
    [SerializeField, Header("ポップの最大倍率：float")]
    private Vector2 PopSize;
    [SerializeField, Header("ポップの最大倍率までの時間：float")]
    private float PopMaxSizeTime = 1.0f;
    [SerializeField, Header("ポップ後元サイズに戻るまでの時間：float")]
    private float PopInitSizeTime = 1.0f;

    private void Start()
    {
        if (UseFade)
        { DoFade(); }
        if (UseMove)
        {   PosMove();   }
        if(UsePop)
        { DoPop(); }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void PosMove()
    {
        RectTransform trans = GetComponent<RectTransform>();
        //- 初期位置を保存
        InitPos = trans.anchoredPosition;
        //- 状態に合わせてスタート位置を変更
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
        //- 初期位置にむかって移動
        transform.DOLocalMove(InitPos, MoveTime)
            .SetEase(Ease.OutSine)
            .SetLink(this.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable)
            .SetDelay(Delay);
        
    }
 
    /// <summary>
    /// フェード処理
    /// </summary>
    private void DoFade()
    {
        Image image = GetComponent<Image>();
        //- 指定したアルファ値で開始
        image.color = new Color(image.color.r, image.color.g, image.color.b, StartAlpha);
        //- フェードを行う
        image.DOFade(EndAlpha,FadeTime).SetLink(image.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable);
       
    }

    private void DoPop()
    {
        Image image = GetComponent<Image>();
        Vector2 Initsize = this.gameObject.transform.localScale;
        transform.DOScale(new Vector3(Initsize.x * PopSize.x, Initsize.y * PopSize.y, 0.0f), PopMaxSizeTime)
            .SetEase(Ease.OutSine)
            .SetLink(image.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable)
            .OnComplete(() => 
            {
                transform.DOScale(new Vector3(Initsize.x, Initsize.y, 0.0f), PopMaxSizeTime)
                .SetEase(Ease.OutSine)
                .SetLink(image.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable);
            });
    }
}
