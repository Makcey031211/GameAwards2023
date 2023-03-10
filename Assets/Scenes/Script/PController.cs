using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PController : MonoBehaviour
{
    [Header("移動の速さ"), SerializeField]
    private float speed = 3;

    [Header("ジャンプする瞬間の速さ"), SerializeField]
    private float jumpSpeed = 7;

    [Header("スーパージャンプの速さの倍率"), SerializeField]
    private float SuperJumpRatio = 2;

    [Header("重力加速度"), SerializeField]
    private float gravity = 15;

    [Header("落下時の速さ制限（Infinityで無制限）"), SerializeField]
    private float fallSpeed = 10;

    [Header("落下の初速"), SerializeField]
    private float initFallSpeed = 2;

    [Header("弾の発射速度の最低値"), SerializeField]
    private float minBulletSpeed = 200.0f;

    [Header("弾の発射速度の最高値"), SerializeField]
    private float maxBulletSpeed = 1000.0f;

    [Header("弾のチャージ秒数(最高値まで必要な秒数)"), SerializeField]
    float ChargeBulletMinute = 2.0f;

    [Header("最大連続ジャンプ回数"), SerializeField]
    int nMaxJumpCount = 2;

    //- 弾の発射速度
    private float bulletSpeed;

    //- 発射ボタンが押されているかどうか
    private bool bPushBulletButton = false;

    //- 過去のY座標
    private float oldposY;

    //- 右に打つかどうか
    private bool bIsShotRight = true;

    //- 打ち出す弾
    ShotBullet shotBullet;

    //- ジャンプした回数(ジャンプ回数が回復すると,この変数は0に戻る)
    private int nJumpCount = 0;
    

    private Transform _transform;
    private CharacterController characterController;
    private Vector2 ShotDirVector; //- 発射する方向
   
    private Vector2 inputMove;
    private float verticalVelocity;
    private float turnVelocity;
    private bool isGroundedPrev;

    void Start()
    {
        _transform = transform;
        shotBullet = GetComponent<ShotBullet>();
        //- 発射速度の初期化
        bulletSpeed = minBulletSpeed;
    }

    /// <summary>
    /// 移動Action(PlayerInput側から呼ばれる)
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        // 入力値を保持しておく
        inputMove = context.ReadValue<Vector2>();
        //- 入力値を発射方向ベクトルとして記録
        ShotDirVector = inputMove * 1000;
        //- Z軸の移動を無効化
        inputMove.y = 0;
    }

    /// <summary>
    /// ジャンプAction(PlayerInput側から呼ばれる)
    /// </summary>
    public void OnJump(InputAction.CallbackContext context)
    {
        //- ボタンが押された瞬間だけ処理
        if (!context.started) return;
        //- ジャンプがまだできるか調べる。もうジャンプできない場合は処理しない
        if (nJumpCount >= nMaxJumpCount) return;
        //- ジャンプカウントを１増やす
        nJumpCount++;
        // 鉛直上向きに速度を与える
        verticalVelocity = jumpSpeed;
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        //- contextだと瞬間(１フレーム)しか判定が取れないので
        //- ボタンが押された瞬間と離された瞬間を利用して、PUSHの判定をbool変数に代入する(ボタンが押されているかどうかの判定)
        if (context.started)
        { bPushBulletButton = true; }
        if (context.canceled)
        { bPushBulletButton = false; }

        //- 以下の、発射処理はボタンが離された瞬間にのみ処理される
        if (!context.canceled) return;

        //- 発射方向ベクトルからベクトルの角度(rad)を求める
        float rad = Mathf.Atan(ShotDirVector.y / ShotDirVector.x);
        //- 発射方向がどちらも0の場合、計算ができないため、手動で角度を入力
        if (ShotDirVector.x == 0 && ShotDirVector.y == 0)
        { rad = 0.0f; }
        //- Xのベクトルがマイナスだった場合、角度が反転するため、180度回転させる
        if (ShotDirVector.x < 0)
        { rad += 180 * 3.14f / 180; }
        //- 角度(rad)と距離(発射の強さ)を使って発射方向ベクトルのXYの長さを求める
        float DisX = bulletSpeed * Mathf.Cos(rad);
        float DisY = bulletSpeed * Mathf.Sin(rad);

        //- デバッグ用：発射速度のデバッグ表示
        Debug.Log("発射速度：" + bulletSpeed + " 発射角度：" + ((rad * 180 / 3.14f) + 90));

        //- 発射方向ベクトルを使って発射関数を実行
        shotBullet.Shot(new Vector2(DisX, DisY));
        //- 関数実行後、発射速度をリセット
        bulletSpeed = minBulletSpeed;
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 Pos = _transform.position;
        Pos.z = 0.0f;
        
        //- デバッグ用：Cubeを利用した発射速度の表示
        GameObject cube = GameObject.Find("BulletSpeedDebugCube");
        Vector3 CubeScale = cube.transform.localScale;
        CubeScale.x = (10.0f) * ((bulletSpeed - minBulletSpeed) / (maxBulletSpeed - minBulletSpeed));
        cube.transform.localScale = CubeScale;

        //- ボタンが押されている間、弾の発射の速度を増やし続ける
        if (bPushBulletButton)
        {
            //- チャージに必要な秒数(フレーム)から、１フレームに必要な増加速度を求める
            float addBulletSpeed = (maxBulletSpeed - minBulletSpeed) / (ChargeBulletMinute * 60);
            bulletSpeed += addBulletSpeed;
        }     
        //- 発射速度が最高速度を超えた場合、最高速度と同じにする
        if (bulletSpeed > maxBulletSpeed)
        { bulletSpeed = maxBulletSpeed; }

        var isGrounded = characterController.isGrounded;

        if (isGrounded && !isGroundedPrev)
        {
            // 着地する瞬間に落下の初速を指定しておく
            verticalVelocity = -initFallSpeed;
            //- 着地したときに、ジャンプを回復する(ジャンプ回数を0にする)
            nJumpCount = 0;
        }
        else if (!isGrounded)
        {
            // 空中にいるときは、下向きに重力加速度を与えて落下させる
            verticalVelocity -= gravity * Time.deltaTime;

            // 落下する速さ以上にならないように補正
            if (verticalVelocity < -fallSpeed)
                verticalVelocity = -fallSpeed;

            // 空中で物にぶつかってY軸が止まる(頭ごっつんこ)
            if (oldposY == this.transform.position.y)
            {
                verticalVelocity = -1;
            }
            oldposY = this.transform.position.y;
        }

        isGroundedPrev = isGrounded;

        // 操作入力と鉛直方向速度から、現在速度を計算
        var moveVelocity = new Vector3(
            inputMove.x * speed,
            verticalVelocity,
            inputMove.y * speed
        );

        //- スティックのX軸の入力値で、射撃方向の変数を設定
        if (moveVelocity.x > 0)
        { bIsShotRight = true; }
        else
        { bIsShotRight = false; }

        // 現在フレームの移動量を移動速度から計算
        var moveDelta = moveVelocity * Time.deltaTime;

        // CharacterControllerに移動量を指定し、オブジェクトを動かす
        characterController.Move(moveDelta);

        if (inputMove != Vector2.zero)
        {
            // 移動入力がある場合は、振り向き動作も行う

            // 操作入力からy軸周りの目標角度[deg]を計算
            var targetAngleY = -Mathf.Atan2(inputMove.y, inputMove.x)
                * Mathf.Rad2Deg + 90;

            // イージングしながら次の回転角度[deg]を計算
            var angleY = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngleY,
                ref turnVelocity,
                0.1f
            );

            // オブジェクトの回転を更新
            transform.rotation = Quaternion.Euler(0, angleY, 0);
        }
    }
}

