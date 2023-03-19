using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クォータニオンで円運動の軌道を計算
/// </summary>
public class CircleMove : MonoBehaviour
{
    [SerializeField, Header("中心点")]
    private Vector3 Center = Vector3.zero;

    [SerializeField, Header("回転軸")]
    private Vector3 Axis = Vector3.up;

    [SerializeField, Header("一周回るのにかかる時間(秒)")]
    private float PeriodTime = 2;

    [SerializeField, Header("向きを更新するかどうか")]
    private bool updateRotation = true;

    float angle = 360;

    // Update is called once per frame
    private void Update()
    {
        var Trans = transform;

        //- 回転のクォータニオン作成
        var angleAxis = Quaternion.AngleAxis(angle / PeriodTime * Time.deltaTime,Axis);

        //- 円運動の位置計算
        var pos = Trans.position;

        pos -= Center;
        pos = angleAxis * pos;
        pos += Center;

        Trans.position = pos;

        //- 向き更新
        if (updateRotation)
        {
            Trans.rotation = Trans.rotation * angleAxis;
        }
    }
}
