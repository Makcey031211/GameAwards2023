using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;

public class SelectMovePlayer : MonoBehaviour,ISelectHandler
{
    [SerializeField, Header("移動時間")]
    private float MoveTIme = 1.0f;

    public void OnSelect(BaseEventData eventData)
    {
        print(name);
        Debug.Log("せれくと");
        transform.DOMove(eventData.selectedObject.transform.position, MoveTIme);
    }
}
