using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PController : MonoBehaviour
{
    [Header("�ړ��̑���"), SerializeField]
    private float speed = 3;

    [Header("�W�����v����u�Ԃ̑���"), SerializeField]
    private float jumpSpeed = 7;

    [Header("�X�[�p�[�W�����v�̑����̔{��"), SerializeField]
    private float SuperJumpRatio = 2;

    [Header("�d�͉����x"), SerializeField]
    private float gravity = 15;

    [Header("�������̑��������iInfinity�Ŗ������j"), SerializeField]
    private float fallSpeed = 10;

    [Header("�����̏���"), SerializeField]
    private float initFallSpeed = 2;

    [Header("�e�̔��ˑ��x�̍Œ�l"), SerializeField]
    private float minBulletSpeed = 200.0f;

    [Header("�e�̔��ˑ��x�̍ō��l"), SerializeField]
    private float maxBulletSpeed = 1000.0f;

    [Header("�e�̃`���[�W�b��(�ō��l�܂ŕK�v�ȕb��)"), SerializeField]
    float ChargeBulletMinute = 2.0f;

    [Header("�ő�A���W�����v��"), SerializeField]
    int nMaxJumpCount = 2;

    //- �e�̔��ˑ��x
    private float bulletSpeed;

    //- ���˃{�^����������Ă��邩�ǂ���
    private bool bPushBulletButton = false;

    //- �ߋ���Y���W
    private float oldposY;

    //- �E�ɑł��ǂ���
    private bool bIsShotRight = true;

    //- �ł��o���e
    ShotBullet shotBullet;

    //- �W�����v������(�W�����v�񐔂��񕜂����,���̕ϐ���0�ɖ߂�)
    private int nJumpCount = 0;
    

    private Transform _transform;
    private CharacterController characterController;
    private Vector2 ShotDirVector; //- ���˂������
   
    private Vector2 inputMove;
    private float verticalVelocity;
    private float turnVelocity;
    private bool isGroundedPrev;

    void Start()
    {
        _transform = transform;
        shotBullet = GetComponent<ShotBullet>();
        //- ���ˑ��x�̏�����
        bulletSpeed = minBulletSpeed;
    }

    /// <summary>
    /// �ړ�Action(PlayerInput������Ă΂��)
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        // ���͒l��ێ����Ă���
        inputMove = context.ReadValue<Vector2>();
        //- ���͒l�𔭎˕����x�N�g���Ƃ��ċL�^
        ShotDirVector = inputMove * 1000;
        //- Z���̈ړ��𖳌���
        inputMove.y = 0;
    }

    /// <summary>
    /// �W�����vAction(PlayerInput������Ă΂��)
    /// </summary>
    public void OnJump(InputAction.CallbackContext context)
    {
        //- �{�^���������ꂽ�u�Ԃ�������
        if (!context.started) return;
        //- �W�����v���܂��ł��邩���ׂ�B�����W�����v�ł��Ȃ��ꍇ�͏������Ȃ�
        if (nJumpCount >= nMaxJumpCount) return;
        //- �W�����v�J�E���g���P���₷
        nJumpCount++;
        // ����������ɑ��x��^����
        verticalVelocity = jumpSpeed;
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        //- context���Əu��(�P�t���[��)�������肪���Ȃ��̂�
        //- �{�^���������ꂽ�u�ԂƗ����ꂽ�u�Ԃ𗘗p���āAPUSH�̔����bool�ϐ��ɑ������(�{�^����������Ă��邩�ǂ����̔���)
        if (context.started)
        { bPushBulletButton = true; }
        if (context.canceled)
        { bPushBulletButton = false; }

        //- �ȉ��́A���ˏ����̓{�^���������ꂽ�u�Ԃɂ̂ݏ��������
        if (!context.canceled) return;

        //- ���˕����x�N�g������x�N�g���̊p�x(rad)�����߂�
        float rad = Mathf.Atan(ShotDirVector.y / ShotDirVector.x);
        //- ���˕������ǂ����0�̏ꍇ�A�v�Z���ł��Ȃ����߁A�蓮�Ŋp�x�����
        if (ShotDirVector.x == 0 && ShotDirVector.y == 0)
        { rad = 0.0f; }
        //- X�̃x�N�g�����}�C�i�X�������ꍇ�A�p�x�����]���邽�߁A180�x��]������
        if (ShotDirVector.x < 0)
        { rad += 180 * 3.14f / 180; }
        //- �p�x(rad)�Ƌ���(���˂̋���)���g���Ĕ��˕����x�N�g����XY�̒��������߂�
        float DisX = bulletSpeed * Mathf.Cos(rad);
        float DisY = bulletSpeed * Mathf.Sin(rad);

        //- �f�o�b�O�p�F���ˑ��x�̃f�o�b�O�\��
        Debug.Log("���ˑ��x�F" + bulletSpeed + " ���ˊp�x�F" + ((rad * 180 / 3.14f) + 90));

        //- ���˕����x�N�g�����g���Ĕ��ˊ֐������s
        shotBullet.Shot(new Vector2(DisX, DisY));
        //- �֐����s��A���ˑ��x�����Z�b�g
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
        
        //- �f�o�b�O�p�FCube�𗘗p�������ˑ��x�̕\��
        GameObject cube = GameObject.Find("BulletSpeedDebugCube");
        Vector3 CubeScale = cube.transform.localScale;
        CubeScale.x = (10.0f) * ((bulletSpeed - minBulletSpeed) / (maxBulletSpeed - minBulletSpeed));
        cube.transform.localScale = CubeScale;

        //- �{�^����������Ă���ԁA�e�̔��˂̑��x�𑝂₵������
        if (bPushBulletButton)
        {
            //- �`���[�W�ɕK�v�ȕb��(�t���[��)����A�P�t���[���ɕK�v�ȑ������x�����߂�
            float addBulletSpeed = (maxBulletSpeed - minBulletSpeed) / (ChargeBulletMinute * 60);
            bulletSpeed += addBulletSpeed;
        }     
        //- ���ˑ��x���ō����x�𒴂����ꍇ�A�ō����x�Ɠ����ɂ���
        if (bulletSpeed > maxBulletSpeed)
        { bulletSpeed = maxBulletSpeed; }

        var isGrounded = characterController.isGrounded;

        if (isGrounded && !isGroundedPrev)
        {
            // ���n����u�Ԃɗ����̏������w�肵�Ă���
            verticalVelocity = -initFallSpeed;
            //- ���n�����Ƃ��ɁA�W�����v���񕜂���(�W�����v�񐔂�0�ɂ���)
            nJumpCount = 0;
        }
        else if (!isGrounded)
        {
            // �󒆂ɂ���Ƃ��́A�������ɏd�͉����x��^���ė���������
            verticalVelocity -= gravity * Time.deltaTime;

            // �������鑬���ȏ�ɂȂ�Ȃ��悤�ɕ␳
            if (verticalVelocity < -fallSpeed)
                verticalVelocity = -fallSpeed;

            // �󒆂ŕ��ɂԂ�����Y�����~�܂�(��������)
            if (oldposY == this.transform.position.y)
            {
                verticalVelocity = -1;
            }
            oldposY = this.transform.position.y;
        }

        isGroundedPrev = isGrounded;

        // ������͂Ɖ����������x����A���ݑ��x���v�Z
        var moveVelocity = new Vector3(
            inputMove.x * speed,
            verticalVelocity,
            inputMove.y * speed
        );

        //- �X�e�B�b�N��X���̓��͒l�ŁA�ˌ������̕ϐ���ݒ�
        if (moveVelocity.x > 0)
        { bIsShotRight = true; }
        else
        { bIsShotRight = false; }

        // ���݃t���[���̈ړ��ʂ��ړ����x����v�Z
        var moveDelta = moveVelocity * Time.deltaTime;

        // CharacterController�Ɉړ��ʂ��w�肵�A�I�u�W�F�N�g�𓮂���
        characterController.Move(moveDelta);

        if (inputMove != Vector2.zero)
        {
            // �ړ����͂�����ꍇ�́A�U�����������s��

            // ������͂���y������̖ڕW�p�x[deg]���v�Z
            var targetAngleY = -Mathf.Atan2(inputMove.y, inputMove.x)
                * Mathf.Rad2Deg + 90;

            // �C�[�W���O���Ȃ��玟�̉�]�p�x[deg]���v�Z
            var angleY = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngleY,
                ref turnVelocity,
                0.1f
            );

            // �I�u�W�F�N�g�̉�]���X�V
            transform.rotation = Quaternion.Euler(0, angleY, 0);
        }
    }
}

