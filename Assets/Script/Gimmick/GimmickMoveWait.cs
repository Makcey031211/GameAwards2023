using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickMoveWait : MonoBehaviour
{
    [SerializeField, Header("始点")]
    private List<Vector3> startPoint;

    [SerializeField, Header("中間点")]
    private List<Vector3> halfwayPoint;

    [SerializeField, Header("終点")]
    private List<Vector3> endPoint;

    [SerializeField, Header("移動速度")]
    private float moveSpeed = 2.0f;

    [SerializeField, Header("現在の位置")]
    private int currentPoint = 0;

    [SerializeField, Header("方向")]
    private int direction = 1;

    [SerializeField, Header("中間点到達時の待機時間")]
    private float waitTime = 1.0f;

    private List<Vector3> points = new List<Vector3>();

    //- 待機時間
    private float waitingTimer = 0.0f;

    //- 中間点で待機しているかどうか
    private bool isWaiting     = false;

    private void Start()
    {
        //--- 3つのポイントを設定 ---
        points.AddRange(startPoint);
        points.AddRange(halfwayPoint);
        points.AddRange(endPoint);
    }

    private void Update()
    {
        if (isWaiting) // 中間点待機時
        {
            waitingTimer -= Time.deltaTime;
            if (waitingTimer <= 0.0f)
            {
                isWaiting = false;
                currentPoint += direction;
                if (currentPoint >= points.Count || currentPoint < 0)
                {
                    direction *= -1;
                    currentPoint += direction;
                }
            }
        }
        else // 中間点で待機していない時
        {
            //- 次の位置に移動するための方向ベクトルを計算する
            Vector3 directionVector = (points[currentPoint] - transform.position).normalized;

            //- 次の位置に移動するための距離を計算する
            float distanceToMove = moveSpeed * Time.deltaTime;

            //- 次の位置に移動する
            transform.position += directionVector * distanceToMove;

            //- 次のポイントに到達したら方向を逆にする
            if (Vector3.Distance(transform.position, points[currentPoint]) < 0.01f)
            {
                if (currentPoint == endPoint.Count)
                {
                    isWaiting    = true;
                    waitingTimer = waitTime;
                }
                else
                {
                    currentPoint += direction;
                    if (currentPoint >= points.Count || currentPoint < 0)
                    {
                        direction *= -1;
                        currentPoint += direction;
                    }
                }
            }
        }
    }
}
