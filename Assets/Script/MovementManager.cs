using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 挙動のマネージャー
 */
public class MovementManager : MonoBehaviour
{
    public enum MovementType
    {
        CicrleBehaviour, // 円挙動
    }

    public enum Direction
    {
        Clockwise,        // 半時計周り
        CounterClockwise, // 時計回り
    }

    //* 共通関連 *//
    //- インスペクターに表示
    [SerializeField, Header("挙動の種類")]
    public MovementType _type = MovementType.CicrleBehaviour;
    //- インスペクターから非表示
    FireworksModule fireworks;
    public MovementType Type => _type;

    //* 円挙動関連 *//
    //- インスペクターに表示
    [SerializeField, HideInInspector]
    public Direction _direction = Direction.Clockwise; // 回転方向
    [SerializeField, HideInInspector]
    public Vector3 _center = Vector3.zero; // 中心点
    [SerializeField, HideInInspector]
    public Vector3 _axis = Vector3.forward; // 回転軸
    [SerializeField, HideInInspector]
    public float _radius = 1.0f; // 半径の大きさ
    [SerializeField, HideInInspector]
    public float _periodTime = 2.0f; // 一周回るのにかかる時間(秒)
    [SerializeField, HideInInspector]
    public bool _updateRotation = false; // 向きを更新するかどうか
    //- インスペクターに非表示
    private float currentTime;  // 現在の時間
    private float currentAngle; // 現在の回転角度
    private float angle = 360f; // 一周分の角度
    //- 外部からの値取得用
    public Direction Directionary => _direction;
    public Vector3 Center => _center;
    public Vector3 Axis => _axis;
    public float Radius => _radius;
    public float PeriodTime => _periodTime;
    public bool UpdateRotation => _updateRotation;

    void Start()
    {
        fireworks = this.gameObject.GetComponent<FireworksModule>();
    }

    void Update()
    {
        switch (Type)
        {
            //- 選択するタイプに応じて処理を分岐
            case MovementType.CicrleBehaviour:
                CicrleMove();
                break;
        }
    }

    /// <summary>
    /// 円運動
    /// </summary>
    private void CicrleMove()
    {
        if (!fireworks.IsExploded)
        {
            var trans = transform;

            //- 回転のクォータニオン作成
            var angleAxis = Quaternion.AngleAxis(currentAngle, Axis);

            //- 半径に対応するベクトルを作成し、回転軸に沿って回転させる
            var radiusVec = angleAxis * (Vector3.up * Radius);

            //- 中心点に半径に対応するベクトルを加算して位置を計算する
            var pos = Center + radiusVec;

            //- 位置を更新する
            trans.position = pos;

            //- 向きを更新する
            if (UpdateRotation)
            {
                trans.rotation = Quaternion.LookRotation(Center - pos, Vector3.up);
            }

            //- 現在の回転角度を更新する
            currentTime += Time.deltaTime;

            //- 回転方向に応じて処理を分岐
            switch (Directionary)
            {
                case Direction.Clockwise:
                    currentAngle = (currentTime % PeriodTime) / PeriodTime * angle;
                    break;
                case Direction.CounterClockwise:
                    currentAngle = angle - ((currentTime % PeriodTime) / PeriodTime * angle);
                    break;
            }
        }
    }
}
