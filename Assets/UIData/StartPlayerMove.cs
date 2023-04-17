using UnityEngine;
using DG.Tweening;

public class StartPlayerMove : MonoBehaviour
{
    [SerializeField, Header("スタート位置まで移動する秒数")]
    private float MoveTime;

    private float DiffPos = 30;
    private static bool StartMove = false;

    void Start()
    {
        float InitPos = transform.localPosition.y;

        transform.localPosition = new Vector3(
            transform.localPosition.x,
            transform.localPosition.y - DiffPos,
            transform.localPosition.z);

        transform.DOMoveY(InitPos, MoveTime)
           .OnComplete(()=> {
               StartMove = true;
           });

        Debug.Log(StartMove);
    }

    public bool MoveComplete()
    {
        return StartMove;
    }
}
