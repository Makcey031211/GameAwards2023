using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;

public class SelectMovePlayer : MonoBehaviour,ISelectHandler
{
    [SerializeField, Header("ˆÚ“®ŽžŠÔ")]
    private float MoveTIme = 1.0f;
    [SerializeField, Header("ƒvƒŒƒCƒ„[")]
    private GameObject player;
    private Vector3 StagePos;
    private bool AnimeKill = false;
    private Sequence anime;
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(eventData.selectedObject.transform.position);
        Vector3 pos = eventData.selectedObject.transform.position;
        var Move = DOTween.Sequence();
        Move.Append(player.transform.DOMove(pos,MoveTIme))
            .OnComplete(() =>
            {   Move.Kill();});

        anime = DOTween.Sequence();
        anime.AppendInterval(0.5f)
            .Append(player.transform.DOMoveY(pos.y + -0.25f, 0.5f).SetEase(Ease.OutSine))
            .Append(player.transform.DOMoveY(pos.y + 0.5f, 1.0f).SetEase(Ease.InOutSine))
            .Append(player.transform.DOMoveY(pos.y + -0.25f, 0.5f).SetEase(Ease.InSine));
        anime.SetLoops(-1);
    }

    

    public void InStageMove()
    {
        anime.Kill();
        DOTween.Sequence()
            .Append(player.transform.DOMoveY(-2.0f, 0.5f).SetRelative(true))
            .AppendInterval(0.25f)
            .Append(player.transform.DOMoveY(20.0f, 1.5f).SetRecyclable(true));
    }
}
