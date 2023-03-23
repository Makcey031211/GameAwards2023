using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 二点間挙動
/// </summary>
public class LerpPingPong : MonoBehaviour
{
    [SerializeField,Header("始点")]
    private Transform startPoint;

    [SerializeField,Header("終点")]
    private Transform endPoint;

    [SerializeField,Header("移動時間")]
    private float travelTime = 1;

    // Update is called once per frame
    private void Update()
    {
        // 始点・終点の位置取得
        var s = startPoint.position;
        var e = endPoint.position;

        // 補間位置計算
        var t = Mathf.PingPong(Time.time / travelTime, 1);

        // 補間位置を反映
        transform.position = Vector3.Lerp(s, e, t);
    }
}
