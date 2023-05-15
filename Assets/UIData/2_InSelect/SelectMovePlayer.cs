using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;

public class SelectMovePlayer : MonoBehaviour,ISelectHandler
{
    [SerializeField, Header("à⁄ìÆéûä‘")]
    private float MoveTIme = 1.0f;
    [SerializeField, Header("ÉvÉåÉCÉÑÅ[")]
    private GameObject player;
    private Vector3 StagePos;
    private bool AnimeKill = false;
    private Sequence anime;
    public void OnSelect(BaseEventData eventData)
    {
        Vector3 pos = eventData.selectedObject.transform.position;
        player.transform.DOMove(pos, MoveTIme);
        anime = DOTween.Sequence();
        anime.Append(player.transform.DOMoveY(-0.25f, 0.5f).SetRelative().SetEase(Ease.OutSine))
             .Append(player.transform.DOMoveY(0.5f, 1.0f).SetRelative().SetEase(Ease.InOutSine))
             .Append(player.transform.DOMoveY(-0.25f, 0.5f).SetRelative().SetEase(Ease.InSine));
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
