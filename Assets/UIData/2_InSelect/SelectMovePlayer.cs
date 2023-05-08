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
    public void OnSelect(BaseEventData eventData)
    {
        Vector3 pos = eventData.selectedObject.transform.position;
        StagePos = pos;
        player.transform.DOMove(pos, MoveTIme);
    }

    public void InStageMove()
    {
        DOTween.Sequence()
            .Append(player.transform.DOMoveY(5.0f, 0.2f).SetRelative(true))
            .Join(player.transform.DOScale(new Vector3(2.0f,2.0f), 0.2f))
            .Append(player.transform.DOMoveY(StagePos.y, 0.2f))
            .Join(player.transform.DOScale(new Vector3(0.0f, 0.0f), 0.2f));
    }
}
