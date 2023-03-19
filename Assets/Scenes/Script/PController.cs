using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PController : MonoBehaviour
{
    [Header("�ړ��̑���"), SerializeField]
    private float speed = 3;

    //[Header("�W�����v����u�Ԃ̑���"), SerializeField]
    //private float jumpSpeed = 7;

    //[Header("�X�[�p�[�W�����v�̑����̔{��"), SerializeField]
    //private float SuperJumpRatio = 2;

    [Header("�d�͉����x"), SerializeField]
    private float gravity = 15;

    [Header("�������̑��������iInfinity�Ŗ������j"), SerializeField]
    private float fallSpeed = 10;

    [Header("�����̏���"), SerializeField]
    private float initFallSpeed = 2;

    [SerializeField, Header("�Ήԗp�̃I�u�W�F�N�g")]
    private GameObject particleObject;

    //- �W�����v������(�W�����v�񐔂��񕜂����,���̕ϐ���0�ɖ߂�)
    private int nJumpCount = 0;


    private Transform _transform;
    private CharacterController characterController;

    private Vector2 inputMove;
    private float verticalVelocity;
    private float turnVelocity;
    private bool isGroundedPrev;
    bool isOnce; // ��������񂾂��s��
    Rigidbody playerRB;

    void Start()
    {
        _transform = transform;
        playerRB = GetComponent<Rigidbody>();

        isOnce = false;
    }

    /// <summary>
    /// �ړ�Action(PlayerInput������Ă΂��)
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!isOnce)
        {
            // ���͒l��ێ����Ă���
            inputMove = context.ReadValue<Vector2>();
        }
    }

    public void OnDestruct(InputAction.CallbackContext context)
    {
        //����
        if (!isOnce)
        { // ��������
            isOnce = true;
            // �w�肵���ʒu�ɐ���
            GameObject fire = Instantiate(
                particleObject,                     // ����(�R�s�[)����Ώ�
                transform.position,                 // ���������ʒu
                Quaternion.Euler(0.0f, 0.0f, 0.0f)  // �ŏ��ɂǂꂾ����]���邩
                );

            // �q�I�u�W�F�N�g1��
            transform.GetChild(0).gameObject.GetComponent<DetonationCollision>().enabled = true;
            transform.GetChild(0).gameObject.GetComponent<SphereCollider>().enabled = true;

            // �q�I�u�W�F�N�g2��
            transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;

            //Destroy(gameObject);
        }
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 Pos = _transform.position;
        Pos.z = 0.0f;

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

            //// �󒆂ŕ��ɂԂ�����Y�����~�܂�(��������)
            //if (oldposY == this.transform.position.y)
            //{
            //    verticalVelocity = -1;
            //}
            //oldposY = this.transform.position.y;
        }

        isGroundedPrev = isGrounded;

        // ������͂Ɖ����������x����A���ݑ��x���v�Z
        var moveVelocity = new Vector3(
            inputMove.x * speed,
            verticalVelocity,
            inputMove.y * speed
        );

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

