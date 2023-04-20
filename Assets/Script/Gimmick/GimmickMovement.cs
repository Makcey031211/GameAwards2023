using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //- 経過時間
    private float timeElapsed;

    //- 開始位置
    private Vector3 startPosition;

    //- 終了位置
    private Vector3 endPosition;

    //- 移動の方向転換用
    private bool reverse = false;

    private void Start()
    {
        startPosition = transform.position;

        endPosition = startPosition + Vector3.right * moveDistance;
    }

    private void Update()
    {
        //- 経過時間を計算する
        timeElapsed += Time.deltaTime;

        //- 移動の割合を計算する（0から1までの値）
        float t = Mathf.Clamp01(timeElapsed / travelTime);

        //- 移動方向に合わせて位置を変更する
        if (!reverse)
        {
            switch (moveDirection)
            {
                case MoveDirection.Horizontal:
                    transform.position = Vector3.Lerp(startPosition, endPosition, t);
                    break;
                case MoveDirection.Vertical:
                    transform.position = Vector3.Lerp(
                        startPosition, startPosition + Vector3.up * moveDistance, t);
                    break;
                case MoveDirection.Diagonal:
                    transform.position = Vector3.Lerp(
                        startPosition, startPosition + new Vector3(moveDistance, moveDistance, 0), t);
                    break;
                }
            }
        else
        {
            switch (moveDirection)
            {
                case MoveDirection.Horizontal:
                    transform.position = Vector3.Lerp(endPosition, startPosition, t);
                    break;
                case MoveDirection.Vertical:
                    transform.position = Vector3.Lerp(
                        startPosition + Vector3.up * moveDistance, startPosition, t);
                    break;
                case MoveDirection.Diagonal:
                    transform.position = Vector3.Lerp(
                        startPosition + new Vector3(moveDistance, moveDistance, 0), startPosition, t);
                    break;
            }
        }

        //- 移動が完了したら経過時間をリセットする
        if (t == 1.0f)
        {
            timeElapsed = 0.0f;
            reverse = !reverse; // 移動方向を反転
        }
    }
}