using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 挙動のマネージャー
 */
public class MovementManager : MonoBehaviour
{
    //--- 列挙体定義(タイプ)
    public enum E_MovementType
    {
        ThreewayBehaviour,   // 三方向挙動
        ThreepointBehaviour, // 三点間挙動
        CicrleBehaviour,     // 円挙動
    }

    //--- 列挙体定義(回転)
    public enum E_RotaDirection
    {
        Clockwise,        // 半時計周り
        CounterClockwise, // 時計回り
    }

    //--- 列挙体定義(移動)
    public enum E_MoveDirection
    {
        Horizontal, // 横移動
        Vertical,   // 縦移動
        Diagonal,   // 斜め移動
    }


    //* 共通関連 *//
    //- インスペクターに表示
    [SerializeField, Header("挙動の種類")]
    public E_MovementType _type = E_MovementType.ThreewayBehaviour;
    //- インスペクターから非表示
    FireworksModule fireworks;
    public E_MovementType Type => _type;

    //* 三方向挙動関連 *//
    //- インスペクターに表示
    [SerializeField, HideInInspector]
    public E_MoveDirection _moveDirection = E_MoveDirection.Horizontal; // 移動方向
    [SerializeField, HideInInspector]
    public float _moveDistance = 5.0f; // 移動距離
    [SerializeField, HideInInspector]
    public float _travelTime   = 1.0f; // 移動時間
    //- インスペクターから非表示
    private Vector3 startPosition;   // 開始位置
    private Vector3 endPosition;     // 終了位置
    private float   timeElapsed;     // 経過時間
    private bool    reverse = false; // 移動の方向転換用
    //- 外部からの値取得用
    public E_MoveDirection MoveDirection => _moveDirection;
    public float MoveDistance => _moveDistance;
    public float TravelTime => _travelTime;

    //* 三点間挙動 *//
    //- インスペクターに表示
    [SerializeField, HideInInspector]
    public Vector3 _startPoint;   // 始点
    [SerializeField, HideInInspector]
    public Vector3 _halfwayPoint; // 中間点
    [SerializeField, HideInInspector]
    public Vector3 _endPoint;     // 終点
    [SerializeField, HideInInspector]
    public float _moveSpeed   = 1.0f;   // 移動速度
    [SerializeField, HideInInspector]
    public float _endWaitTime = 1.0f;   // 終点到達時の待機時間
    //- インスペクターから非表示
    private Vector3[] points = new Vector3[3];
    private int   currentPoint     = 0;     // 現在の位置
    private int   currentDirection = 1;     // 現在の方向
    private float waitingTimer     = 0.0f;  // 待機時間
    private bool  isWaiting        = false; // 待機しているかしていないか
    //- 外部からの値取得用
    public Vector3 StartPoint => _startPoint;
    public Vector3 HalfwayPoint => _halfwayPoint;
    public Vector3 EndPoint => _endPoint;
    public float MoveSpeed => _moveSpeed;
    public float EndWaitTime => _endWaitTime;

    //* 円挙動関連 *//
    //- インスペクターに表示
    [SerializeField, HideInInspector]
    public E_RotaDirection _rotaDirection = E_RotaDirection.Clockwise; // 回転方向
    [SerializeField, HideInInspector]
    public Vector3 _center = Vector3.zero;  // 中心点
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
    public E_RotaDirection RotaDirection => _rotaDirection;
    public Vector3 Center => _center;
    public Vector3 Axis => _axis;
    public float Radius => _radius;
    public float PeriodTime => _periodTime;
    public bool UpdateRotation => _updateRotation;


    void Start()
    {
        //* 共通項目 *//
        fireworks = this.gameObject.GetComponent<FireworksModule>();

        //* 三方向挙動項目 *//
        startPosition = transform.position;
        endPosition   = startPosition + Vector3.right * MoveDistance;

        //* 三点間挙動項目 *//
        points[0] = StartPoint;
        points[1] = HalfwayPoint;
        points[2] = EndPoint;
    }

    void Update()
    {
        //- 選択するタイプに応じて処理を分岐
        switch (Type)
        {
            case E_MovementType.ThreewayBehaviour:
                ThreewayMove();
                break;
            case E_MovementType.ThreepointBehaviour:
                ThreePointMove();
                break;
            case E_MovementType.CicrleBehaviour:
                CicrleMove();
                break;
        }
    }

    /// <summary>
    /// 三方向挙動
    /// </summary>
    private void ThreewayMove()
    {
        //- nullチェック
        if (fireworks && fireworks.IsExploded) return;

        //- 経過時間を計算する
        timeElapsed += Time.deltaTime;

        //- 移動の割合を計算する（0から1までの値）
        float t = Mathf.Clamp01(timeElapsed / TravelTime);

        //- 移動方向に合わせて位置を変更する
        if (!reverse)
        {
            switch (MoveDirection)
            {
                case E_MoveDirection.Horizontal:
                    transform.position = Vector3.Lerp(startPosition, endPosition, t);
                    break;
                case E_MoveDirection.Vertical:
                    transform.position = Vector3.Lerp(
                        startPosition, startPosition + Vector3.up * MoveDistance, t);
                    break;
                case E_MoveDirection.Diagonal:
                    transform.position = Vector3.Lerp(
                        startPosition, startPosition + new Vector3(MoveDistance, MoveDistance, 0), t);
                    break;
            }
        }
        else
        {
            switch (MoveDirection)
            {
                case E_MoveDirection.Horizontal:
                    transform.position = Vector3.Lerp(endPosition, startPosition, t);
                    break;
                case E_MoveDirection.Vertical:
                    transform.position = Vector3.Lerp(
                        startPosition + Vector3.up * MoveDistance, startPosition, t);
                    break;
                case E_MoveDirection.Diagonal:
                    transform.position = Vector3.Lerp(
                        startPosition + new Vector3(MoveDistance, MoveDistance, 0), startPosition, t);
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

    /// <summary>
    /// 三点間挙動
    /// </summary>
    private void ThreePointMove()
    {
        //- nullチェック
        if (fireworks && fireworks.IsExploded) return;

        if (isWaiting)
        {
            waitingTimer -= Time.deltaTime;
            if (waitingTimer <= 0.0f)
            {
                isWaiting = false;
                currentPoint += currentDirection;
                if (currentPoint >= points.Length || currentPoint < 0)
                {
                    currentDirection *= -1;
                    currentPoint += currentDirection;
                }
            }
        }
        else
        {
            //- 次の位置に移動するための方向ベクトルを計算する
            Vector3 directionVector = (points[currentPoint] - transform.position).normalized;

            //- 次の位置に移動するための距離を計算する
            float distanceToMove = MoveSpeed * Time.deltaTime;

            //- 次の位置に移動する
            transform.position += directionVector * distanceToMove;

            //- 次のポイントに到達したら方向を逆にする
            if (Vector3.Distance(transform.position, points[currentPoint]) < 0.01f)
            {
                //- 最後のポイントに到達したら
                if (currentPoint == points.Length - 1)
                {
                    isWaiting = true;
                    waitingTimer = EndWaitTime;
                }
                else
                {
                    currentPoint += currentDirection;
                    if (currentPoint >= points.Length || currentPoint < 0)
                    {
                        currentDirection *= -1;
                        currentPoint += currentDirection;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 円運動
    /// </summary>
    private void CicrleMove()
    {
        //- nullチェック
        if (fireworks && fireworks.IsExploded) return;

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
        switch (RotaDirection)
        {
            case E_RotaDirection.Clockwise:
                currentAngle = (currentTime % PeriodTime) / PeriodTime * angle;
                break;
            case E_RotaDirection.CounterClockwise:
                currentAngle = angle - ((currentTime % PeriodTime) / PeriodTime * angle);
                break;
        }
    }
}