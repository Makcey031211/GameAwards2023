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
    [SerializeField, Header("Ið{^Ìå«³{¦")]
    private Vector2 SelectSize;
    [SerializeField, Header("®®¹ÜÅÌÔ")]
    private float MoveTime = 0.1f;
    //[SerializeField, Header("IðSE")]
    //private AudioClip selectSE;
    //[SerializeField, Header("SEÌ¹Ê")]
    //private float seVolume = 1.0f;

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
        //- Ið¹Ä¶
        //SEManager.Instance.SetPlaySE(selectSE,seVolume);
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
                //- Ið¹Ä¶
                //SEManager.Instance.SetPlaySE(selectSE,seVolume);
            });
    }
}
