using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;

public class SelectMovePlayer : MonoBehaviour,ISelectHandler
{
    [SerializeField, Header("�ړ�����")]
    private float MoveTIme = 1.0f;
    [SerializeField, Header("�v���C���[")]
    private GameObject player;
    
    public void OnSelect(BaseEventData eventData)
    {
        Vector3 pos = eventData.selectedObject.transform.position;
        player.transform.DOMove(pos, MoveTIme);
    }
}
