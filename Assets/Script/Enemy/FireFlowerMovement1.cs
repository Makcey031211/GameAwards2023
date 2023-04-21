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

    [SerializeField, Header("終了時間")]
    private float stopTime = 2.0f;

    //- 移動中かどうか
    private bool isMoving  = true;

    //- 停止中かどうか
    private bool isStopped = false;

    private Vector3 startPosition; // 開始地点
    private Vector3 endPosition;   // 終了地点

    private FireworksModule fireworks;

    private void Start()
    {
        startPosition = transform.position;

        fireworks = GetComponent<FireworksModule>();

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
        if (!fireworks.IsExploded)
        {
            //- 停止していない時
            if (!isStopped)
            {
                //- 線形補間を使ってオブジェクトを移動
                float t = Mathf.PingPong(Time.time / travelTime, 1.0f);
                transform.position = Vector3.Lerp(startPosition, endPosition, t);

                //- 終了地点が一定値以下なら
                if (Vector3.Distance(transform.position, endPosition) < 0.01f)
                {
                    isStopped = true;
                    StartCoroutine(StopAndWait());
                }
            }
            else if (isMoving) //- 移動している時
            {
                StartCoroutine(WaitAndMoveBack());
            }
        }
    }

    /// <summary>
    /// 指定した時間の間、オブジェクトを待機させる関数
    /// </summary>
    /// <returns>isStopped</returns>
    private IEnumerator StopAndWait()
    {
        //- stopTime分待機させる
        yield return new WaitForSeconds(stopTime);

        isStopped = false;
        isMoving  = true;
    }

    /// <summary>
    /// 待機が終了したら、再び移動させる関数
    /// </summary>
    private IEnumerator WaitAndMoveBack()
    {
        isMoving = false;
        yield return new WaitForSeconds(stopTime);

        //- 現在の終点を始点、始点を現在の終点に設定して、移動を再開する
        Vector3 temp  = endPosition;
        endPosition   = startPosition;
        startPosition = temp;

        isStopped = false;
        isMoving  = true;
    }
}