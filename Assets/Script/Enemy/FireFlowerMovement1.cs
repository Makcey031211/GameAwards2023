using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 三方向移動(指定した秒数分待機)
/// </summary>
public class FireFlowerMovement1 : MonoBehaviour
{
    private enum MoveDirection
    {
        Horizontal, //横移動
        Vertical,   //縦移動
        Diagonal    //斜め移動
    }

    [SerializeField, Header("移動方向")]
    private MoveDirection moveDirection = MoveDirection.Horizontal;

    [SerializeField, Header("移動距離")]
    private float moveDistance = 5.0f;

    [SerializeField, Header("移動時間")]
    private float travelTime = 1.0f;

    [SerializeField, Header("待機時間")]
    private float waitTime = 2.0f;

    //- 移動中かどうか
    private bool isMoving = true;

    private Vector3 startPosition; // 開始地点
    private Vector3 endPosition;   // 終了地点

    private FireworksModule fireworks;

    private void Start()
    {
        startPosition = transform.position;

        endPosition = GetEndPosition();

        fireworks = GetComponent<FireworksModule>();
    }

    private void Update()
    {
        if (!fireworks.IsExploded)
        {
            //- 停止していない時
            if (isMoving)
            {
                Move();
            }
        }
    }

    private Vector3 GetEndPosition()
    {
        switch (moveDirection)
        {
            case MoveDirection.Horizontal:
                return startPosition + Vector3.right * moveDistance;
            case MoveDirection.Vertical:
                return startPosition + Vector3.up * moveDistance;
            case MoveDirection.Diagonal:
                return startPosition + new Vector3(moveDistance, moveDistance, 0);
            default:
                return startPosition;
        }
    }

    private void Move()
    {
        //- 線形補間でオブジェクトを移動させる
        float t = Mathf.PingPong(Time.time / travelTime, 1.0f);
        transform.position = Vector3.Lerp(startPosition, endPosition, t);

        if (Vector3.Distance(transform.position, endPosition) < 0.01f)
        {
            if (endPosition == startPosition) // StartPositionに戻る場合
            {
                endPosition = GetEndPosition(); // EndPositionを更新
                isMoving = true;
            }
            else
            {
                StartCoroutine(WaitAndMoveBack());
                isMoving = false;
            }
        }
    }

    private IEnumerator WaitAndMoveBack()
    {
        yield return new WaitForSeconds(waitTime);

        //- 現在の始点を終点、終点を現在の始点に設定して、移動を再開する
        Vector3 temp  = endPosition;
        endPosition   = startPosition;
        startPosition = temp;

        isMoving = true;
    }
}