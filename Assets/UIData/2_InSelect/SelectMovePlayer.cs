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
            .Append(player.transform.DOMoveY(-2.0f, 0.5f).SetRelative(true))
            .AppendInterval(0.25f)
            .Append(player.transform.DOMoveY(20.0f, 1.5f).SetRecyclable(true));
    }
}
