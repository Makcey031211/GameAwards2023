using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 三方向挙動
/// </summary>
public class GimmickMovement : MonoBehaviour
{
    //- 列挙型定義
    private enum MoveDirection
    {
        Horizontal, // 横移動
        Vertical,   // 縦移動
        Diagonal    // 斜め移動
    }

    [SerializeField, Header("移動方向")]
    private MoveDirection moveDirection = MoveDirection.Horizontal;

    [SerializeField, Header("移動距離")]
    private float moveDistance = 5.0f;

    [SerializeField, Header("移動時間")]
    private float travelTime = 1.0f;

    private Vector3 startPosition; // 開始位置
    private Vector3 endPosition;   // 終了位置

    private void Start()
    {
        startPosition = transform.position;

        //- 移動の状態遷移
        switch (moveDirection)
        {
            case MoveDirection.Horizontal:
                endPosition = startPosition + Vector3.right * moveDistance;
                break;
            case MoveDirection.Vertical:
                endPosition = startPosition + Vector3.up * moveDistance;
                break;
            case MoveDirection.Diagonal:
                endPosition = startPosition + new Vector3(moveDistance, moveDistance, 0);
                break;
        }
    }

    private void Update()
    {
        //- 線形補間を使ってオブジェクトを移動
        float t = Mathf.PingPong(Time.time / travelTime, 1.0f);
        transform.position = Vector3.Lerp(startPosition, endPosition, t);
    }
}