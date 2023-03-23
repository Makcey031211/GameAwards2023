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
    [SerializeField, Header("‘I‘ð’†ƒ{ƒ^ƒ“‚Ì‘å‚«‚³”{—¦")]
    private Vector2 SelectSize;
    [SerializeField, Header("‹““®Š®—¹‚Ü‚Å‚ÌŽžŠÔ")]
    private float MoveTime = 0.1f;
     
    private Button button;
    private Vector2 Size;
    void Start()
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
        Debug.Log("yUIz‘I‘ð");
    }
    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        transform.DOScale(new Vector3( Size.x,Size.y),MoveTime)
            .SetEase(Ease.OutSine);
        Debug.Log("yUIzNo‘I‘ð");
    }

    void ISubmitHandler.OnSubmit(BaseEventData eventData)
    {
        var submit = DOTween.Sequence();
        submit.Append(transform.DOScale(new Vector3(Size.x, Size.y), MoveTime).SetEase(Ease.OutSine))
            .OnComplete(()=>
            {
                transform.DOScale(new Vector3(Size.x + 10.0f, Size.y + 10.0f), MoveTime)
                .SetEase(Ease.OutSine);
            });

    }
}
