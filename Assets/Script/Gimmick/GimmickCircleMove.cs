using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クォータニオンで円運動の軌道を計算
/// </summary>
public class GimmickCircleMove : MonoBehaviour
{
    [SerializeField, Header("中心点")]
    private Vector3 Center = Vector3.zero;

    [SerializeField, Header("回転軸")]
    private Vector3 Axis = Vector3.forward;

    [SerializeField, Header("半径の大きさ")]
    private float Radius = 1.0f;

    [SerializeField, Header("一周回るのにかかる時間(秒)")]
    private float PeriodTime = 2.0f;

    [SerializeField, Header("向きを更新するかどうか")]
    private bool updateRotation = false;

    //- 現在の時間
    private float currentTime;

    //- 角度
    float angle = 360f;

    //- 現在の回転角度
    float currentAngle;

    private void Update()
    {
        var trans = transform;

        //- 回転のクォータニオン作成
        var angleAxis = Quaternion.AngleAxis(currentAngle, Axis);

        //- 半径に対応するベクトルを作成し、回転軸に沿って回転させる
        var radiusVec = angleAxis * (Vector3.right * Radius);

        //- 中心点に半径に対応するベクトルを加算して位置を計算する
        var pos = Center + radiusVec;

        //- 位置を更新する
        trans.position = pos;

        //- 向きを更新する
        if (updateRotation)
        {
            trans.rotation = Quaternion.LookRotation(Center - pos, Vector3.up);
        }

        //- 現在の回転角度を更新する
        currentTime += Time.deltaTime;
        currentAngle = (currentTime % PeriodTime) / PeriodTime * angle;
    }
}