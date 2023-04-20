using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonAnime : MonoBehaviour,
    ISelectHandler,
    IDeselectHandler,
    ISubmitHandler
{
    [SerializeField, Header("選択中ボタンの大きさ倍率")]
    private Vector2 SelectSize;
    [SerializeField, Header("挙動完了までの時間")]
    private float MoveTime = 0.1f;

    private Button button;
    private Vector2 Size;
    void Awake()
    {
        button = GetComponent<Button>();
        Size =  button.transform.localScale;
        transform.DOScale(new Vector3(Size.x,Size.y),0.0f)
            .SetLink(button.gameObject,LinkBehaviour.PauseOnDisable);
    }

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        transform.DOScale(new Vector3(Size.x * SelectSize.x, Size.y * SelectSize.y),MoveTime)
            .SetEase(Ease.OutSine);
        //- 選択音再生
        //if (SEManager.Instance != null)
            //SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Click,1.0f,1.0f,false);
    }
    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        transform.DOScale(new Vector3( Size.x,Size.y),MoveTime)
            .SetEase(Ease.OutSine);
    }

    void ISubmitHandler.OnSubmit(BaseEventData eventData)
    {
        var submit = DOTween.Sequence();
        submit.Append(transform.DOScale(new Vector3(Size.x, Size.y), MoveTime).SetEase(Ease.OutSine))
            .OnComplete(()=>
            {
                transform.DOScale(new Vector3(Size.x + 10.0f, Size.y + 10.0f), MoveTime)
                .SetEase(Ease.OutSine);
                //- 選択音再生
                //if (SEManager.Instance != null)
                   // SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Click, 1.0f, 1.0f, false);
            });
    }
}
