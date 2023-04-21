using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonAnime : MonoBehaviour,
    ISelectHandler,
    IDeselectHandler,
    ISubmitHandler
{
    [SerializeField, Header("�I�𒆃{�^���̑傫���{��")]
    private Vector2 SelectSize;
    [SerializeField, Header("���������܂ł̎���")]
    private float MoveTime = 0.1f;
    [SerializeField, Header("�Q�[���J�n���ɑI����ԂɂȂ邩�ǂ���")]
    private bool bIsInitSelect = false;

    //- �I�����̍Đ���������Ă��邩
    private bool bPermissionSelectSE;
    private Button button;
    private Vector2 Size;
    private Tween currentTween;

    void Awake()
    {
        button = GetComponent<Button>();
        Size =  button.transform.localScale;
        bPermissionSelectSE = !bIsInitSelect;
    }

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        if (currentTween != null && currentTween.IsActive() && !currentTween.IsComplete())
        {   currentTween.Kill();    }

        transform.DOScale(new Vector3(Size.x * SelectSize.x, Size.y * SelectSize.y),MoveTime)
            .SetEase(Ease.OutSine)
            .SetLoops(-1,LoopType.Yoyo);
        //- �I�����Đ�
        if (bPermissionSelectSE)
            SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Select, 1.0f, false);
        else
            bPermissionSelectSE = true;
    }
    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        if (currentTween != null && currentTween.IsActive() && !currentTween.IsComplete())
        { currentTween.Kill(); }
        transform.DOKill();
        transform.localScale = Size;
    }

    void ISubmitHandler.OnSubmit(BaseEventData eventData)
    {
        if (currentTween != null && currentTween.IsActive() && !currentTween.IsComplete())
        { currentTween.Kill(); }
        var submit = DOTween.Sequence();
        submit.Append(transform.DOScale(new Vector3(Size.x, Size.y), MoveTime).SetEase(Ease.OutSine))
            .OnComplete(() =>
            {
                transform.DOScale(new Vector3(Size.x + 10.0f, Size.y + 10.0f), MoveTime)
                .SetEase(Ease.OutSine);
            });
        currentTween = submit;
    }
}
