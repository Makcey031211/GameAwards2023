using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickLMoveWait : MonoBehaviour
{
    [SerializeField, Header("始点")]
    private List<Vector3> startPoint;

    [SerializeField, Header("中間点")]
    private List<Vector3> halfwayPoint;

    [SerializeField, Header("終点")]
    private List<Vector3> endPoint;

    [SerializeField, Header("移動速度")]
    private float moveSpeed = 5.0f;

    [SerializeField, Header("現在の位置")]
    private int currentPoint = 0;

    [SerializeField, Header("方向")]
    private int direction = 1;

    [SerializeField, Header("中間点到達時の待機時間")]
    private float waitTime = 2.0f;

    private List<Vector3> points = new List<Vector3>();

    //- 中間点に到達したかどうかのフラグ
    private bool isReachedHalfwayPoint = false;

    //- 中間点に到達した時間
    private float reachedHalfwayPointTime = 0;

    private bool isMovingBackward = false;

    private void Start()
    {
        //--- 3つのポイントを設定 ---
        points.AddRange(startPoint);
        points.AddRange(halfwayPoint);
        points.AddRange(endPoint);
    }

    private void Update()
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
            if (currentPoint >= halfwayPoint.Count && !isReachedHalfwayPoint)
            {
                //- 中間点に到達した時の処理
                isReachedHalfwayPoint = true;
                reachedHalfwayPointTime = Time.time;
            }

            if (isReachedHalfwayPoint && (Time.time - reachedHalfwayPointTime) < waitTime)
            {
                //- 中間点に到達後、待機する時間がまだ残っている場合
                return;
            }

            currentPoint += direction;

            if (isMovingBackward && currentPoint == 0)
            {
                //- 往復移動が終わった場合は isMovingBackward を false にする
                isMovingBackward = false;
            }
            else if (currentPoint >= points.Count || currentPoint < 0)
            {
                //- 端点に到達した場合は方向を逆にする
                direction *= -1;
                currentPoint += direction;
                isMovingBackward = true;
            }

            if (currentPoint == halfwayPoint.Count - 1)
            {
                //- 中間点で方向を逆にする
                direction *= -1;
                isMovingBackward = true;
            }

            //- 中間点に戻る場合は待機フラグをオフにする
            if (currentPoint < halfwayPoint.Count)
            {
                isReachedHalfwayPoint = false;
            }
        }
    }
}