using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PController : MonoBehaviour
{
    [Header("移動の速さ"), SerializeField]
    private float speed = 3;

    [Header("重力加速度"), SerializeField]
    private float gravity = 15;

    [Header("落下時の速さ制限（Infinityで無制限）"), SerializeField]
    private float fallSpeed = 10;

    [Header("落下の初速"), SerializeField]
    private float initFallSpeed = 2;

    [SerializeField, Header("火花用のオブジェクト")]
    private GameObject particleObject;

    [SerializeField, Header("消滅するオブジェクト")]
    private GameObject DeleteObject;

    //- ジャンプした回数(ジャンプ回数が回復すると,この変数は0に戻る)
    private int nJumpCount = 0;

    AudioSource audioSource;

    private Transform _transform;
    private CharacterController characterController;
    private bool bIsPlaySound;

    private Vector2 inputMove;
    private float verticalVelocity;
    private float turnVelocity;
    private bool isGroundedPrev;
    bool isOnce; // 処理を一回だけ行う
    private GameObject CameraObject;

    FireworksModule fireworks;
    SceneChange sceneChange;

    void Start()
    {
        _transform = transform;
        CameraObject = GameObject.Find("Main Camera");
        sceneChange = CameraObject.GetComponent<SceneChange>();
        //audioSource = GetComponent<AudioSource>();

        fireworks = GetComponent<FireworksModule>();

        isOnce = false;
    }
    /// <summary>
    /// 移動Action(PlayerInput側から呼ばれる)
    /// </summary>
    
    
    public void OnMove(InputAction.CallbackContext context)
    {
        // 入力値を保持しておく
        inputMove = context.ReadValue<Vector2>();
        //- 音の再生
        if ((inputMove.x != 0 || inputMove.y != 0) && !bIsPlaySound)
        {
            bIsPlaySound = true;
            //audioSource.Play();
        }
        else if (bIsPlaySound && (inputMove.x == 0 && inputMove.y == 0))
        {
            bIsPlaySound = false;
            //audioSource.Stop();
        }
    }

    public void OnDestruct(InputAction.CallbackContext context)
    {
        //自爆
        if (!isOnce)
        {
            //- 爆発処理
            fireworks.Ignition();

            //- SceneChangeスクリプトのプレイヤー生存フラグをfalseにする
            sceneChange.bIsLife = false;
            isOnce = true;
        }
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 Pos = _transform.position;

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

            //// 空中で物にぶつかってY軸が止まる(頭ごっつんこ)
            //if (oldposY == this.transform.position.y)
            //{
            //    verticalVelocity = -1;
            //}
            //oldposY = this.transform.position.y;
        }

        isGroundedPrev = isGrounded;


        if(isOnce)
        { inputMove = Vector2.zero; }

        // 操作入力と鉛直方向速度から、現在速度を計算
        var moveVelocity = new Vector3(
            inputMove.x * speed,
            inputMove.y * speed
        );

        // 現在フレームの移動量を移動速度から計算
        var moveDelta = moveVelocity * Time.deltaTime;

        // CharacterControllerに移動量を指定し、オブジェクトを動かす
        characterController.Move(moveDelta);

        if (inputMove != Vector2.zero)
        {
           // 移動入力がある場合は、振り向き動作も行う

           // 操作入力からy軸周りの目標角度[deg]を計算
           //var targetAngleY = -Mathf.Atan2(inputMove.y, inputMove.x)
           //    * Mathf.Rad2Deg + 90;

           // イージングしながら次の回転角度[deg]を計算
           //var angleY = Mathf.SmoothDampAngle(
           //    transform.eulerAngles.y,
           //    targetAngleY,
           //    ref turnVelocity,
           //    0.01f
           //);

           // オブジェクトの回転を更新
           //  transform.rotation = Quaternion.Euler(0, angleY, 0);

            // スティックが右に倒されたとき
            if (inputMove.x >= 0.1f)
            {
               //イージングしながら次の回転角度[deg]を計算
               var angleY = Mathf.SmoothDampAngle(
                   transform.eulerAngles.y,
                   90.0f,
                   ref turnVelocity,
                   0.1f
               );

                transform.rotation = Quaternion.Euler(0,angleY,0);
            }
            if (inputMove.x <= -0.1f)
            {
                //イージングしながら次の回転角度[deg]を計算
                var angleY = Mathf.SmoothDampAngle(
                    transform.eulerAngles.y,
                    -140.0f,
                    ref turnVelocity,
                    0.1f
                );

                transform.rotation = Quaternion.Euler(0,angleY,0);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Stage")
        {
            float HitBlockMoveX = other.gameObject.transform.position.x - _transform.position.x;
            float HitBlockMoveY = other.gameObject.transform.position.y - _transform.position.y;
            var HitMove = new Vector3(HitBlockMoveX * 0.001f, HitBlockMoveY * 0.001f);
            characterController.Move(HitMove);
        }
    }
}

