using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireworksModule : MonoBehaviour
{
    // �ԉ΂̎�ޗp�̗񋓌^
    public enum FireworksType
    {
        Normal,
        Cracker,
        Hard,
        MultiBlast,
        ResurrectionBox
    }

    //- ���ʂ̍���
    //-- �C���X�y�N�^�[�ɕ\��
    [SerializeField, Header("�ԉ΂̎��")]
    private FireworksType _type = FireworksType.Normal;
    [SerializeField, Header("�Ήԗp�̃I�u�W�F�N�g")]
    private GameObject _particleObject;
    [SerializeField, Header("�Ή�SE")]
    private AudioClip _sound;
    [SerializeField, Header("SE�̉���")]
    private float _seVolume = 1.0f;
    [SerializeField, HideInInspector]
    public GameObject _collisionObject; // �����蔻��p�I�u�W�F�N�g   �ʏ�A�n�[�h�A�}���`�u���X�g
    [SerializeField, Header("�j���̕\��I�u�W�F�N�g")]
    public GameObject _eyeObject; // �j���\��p�I�u�W�F�N�g
    //-- �C���X�y�N�^�[�����\��
    private VibrationManager vibration; // �R���g���[���[�̐U���p
    private bool _isExploded; // �����t���O
    private bool _isOnce; // ��񂾂��t���O
    //-- �O������̒l�擾�p
    public FireworksType Type => _type;
    public GameObject ParticleObject => _particleObject;
    public AudioClip Sound => _sound;
    public float SEVolume => _seVolume;
    public bool IsExploded => _isExploded;
    public GameObject CollisionObject => _collisionObject;
    public GameObject EyeObject => _eyeObject;


    //- �N���b�J�[�̍���
    //-- �C���X�y�N�^�[�ɕ\��
    [SerializeField, HideInInspector]
    public int _circleComplementNum; // �~�̕�����
    [SerializeField, HideInInspector]
    public float _blastAngle; // ����͈̔͊p�x(0�`180�x)
    [SerializeField, HideInInspector]
    public float _blastDis; // �˒�
    [SerializeField, HideInInspector]
    public float _modelDeleteTime; // ���f���̎c������
    [SerializeField, Header("�j��ヂ�f��")]
    public GameObject _afterModel; // �j���̕\�����f��
    [SerializeField, HideInInspector]
    public bool _isDrawArea = true; // ����͈͂̕`��t���O
    //-- �C���X�y�N�^�[�����\��
    private float _destroyTime = 3.0f;    // ���S�ɃI�u�W�F�N�g���������鎞��
    private LineRenderer _linerend;       // �����蔻��\���p�̐�
    private ParticleSystem _particleSystem;     // �p�[�e�B�N���V�X�e��
    private bool _isOldExploded = false;        // ���j���̒e���A�����̏������s�������ǂ���
    //-- �O������̒l�擾�p
    public int CircleComplementNum => _circleComplementNum;
    public float BlastAngle => _blastAngle;
    public float BlastDis => _blastDis;
    public float ModelDeleteTime => _modelDeleteTime;
    public GameObject AfterModel => _afterModel;
    public bool IsDrawArea => _isDrawArea;


    //- �n�[�h�A�}���`�u���X�g�̍���
    //-- �C���X�y�N�^�[�ɕ\��
    [SerializeField, HideInInspector]
    public float _blastInvSeconds = 3.0f; // �����㖳�G����
    [SerializeField, HideInInspector]
    public Color _invColor; // ���G���Ԓ��̐F(RGB)
    [SerializeField, HideInInspector]
    public int _blastNum = 2;  // ����ڂŔ������邩
    //-- �C���X�y�N�^�[�����\��
    private int _invFrameCount = 0; // ���G���ԗp�̃t���[���J�E���^
    private int _blastCount = 0; // ���񔚔�������
    private Color _initColor; // �}�e���A���̏����̐F
    private bool _isInvinsible = false; // ���������ǂ���
    //-- �O������̒l�擾�p
    public float BlastInvSeconds => _blastInvSeconds;
    public Color InvColor => _invColor;
    public int BlastNum => _blastNum;

    //- �������p�̍���
    //-- �C���X�y�N�^�[�ɕ\��
    [SerializeField, HideInInspector]
    public GameObject _playerPrefab; // ��������I�u�W�F�N�g
    [SerializeField, HideInInspector]
    public float _delayTime = 0.1f; // �����܂ł̑҂�����(�b)
    [SerializeField, HideInInspector]
    public float _animationTime = 0.1f; // �A�j���[�V��������(�b)
    [SerializeField, HideInInspector]
    public float _animationDelayTime = 0.1f; // �A�j���[�V�����̒x������(�b)
    [SerializeField, HideInInspector]
    public float _boxDisTime = 0.1f; // ���̏��Ŏ���(�b)
    [SerializeField, HideInInspector]
    public AudioClip _generatedSound; // ������SE
    [SerializeField, HideInInspector]
    public AudioClip _disSound; // ���ŉ�SE
    //-- �C���X�y�N�^�[�����\��
    SceneChange sceneChange;
    //-- �O������̒l�擾�p
    public GameObject PlayerPrefab => _playerPrefab;
    public float DelayTime => _delayTime;
    public float AnimationTime => _animationTime;
    public float AnimationDelayTime => _animationDelayTime;
    public float BoxDisTime => _boxDisTime;
    public AudioClip GeneratedSound => _generatedSound;
    public AudioClip DisSound => _disSound;


    // Start is called before the first frame update
    void Start()
    {
        //- ���ʍ���
        vibration = GameObject.Find("VibrationManager").GetComponent<VibrationManager>();
        _isExploded = false;
        _isOnce = false;

        //- �N���b�J�[�̍���
        _linerend = gameObject.AddComponent<LineRenderer>(); // ���̒ǉ�
        vibration = GameObject.Find("VibrationManager").GetComponent<VibrationManager>(); // �U���R���|�[�l���g�̎擾
        _particleSystem = ParticleObject.transform.GetChild(0).GetComponent<ParticleSystem>(); // �p�[�e�B�N���̎擾

        //- �������̍���
        sceneChange = GameObject.FindWithTag("MainCamera").GetComponent<SceneChange>();
    }

    // Update is called once per frame
    void Update()
    {

        if (IsExploded) { // ����������
            switch (Type) {
            case FireworksType.Normal:
                NormalFire();
                break;
            case FireworksType.Cracker:
                CrackerFire();
                break;
            case FireworksType.Hard:
                HardFire();
                break;
            case FireworksType.MultiBlast:
                MultiBlastFire();
                break;
            case FireworksType.ResurrectionBox:
                ResurrectionBoxFire();
                break;
            default:
                break;
            }
        }
    }

    // �������Ɏq�I�u�W�F�N�g�܂ߕ`�����߂鏈��
    void StopRenderer(GameObject gameObject)
    {
        var renderer = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < renderer.Length; i++) {
            renderer[i].enabled = false;
        }
    }

    // �����t���O�𗧂Ă鏈��
    public void Ignition()
    {
        _isExploded = true;
    }

    private void NormalFire()
    {
        if (!_isOnce) { // ����������̂�
            _isOnce = true;
            ShakeByPerlinNoise shakeByPerlinNoise;
            shakeByPerlinNoise = GameObject.FindWithTag("MainCamera").GetComponent<ShakeByPerlinNoise>();
            var duration = 0.2f;
            var strength = 0.1f;
            var vibrato = 1.0f;
            shakeByPerlinNoise.StartShake(duration, strength, vibrato);
            //- �w�肵���ʒu�ɐ���
            GameObject fire = Instantiate(
                ParticleObject,                     // ����(�R�s�[)����Ώ�
                transform.position,           // ���������ʒu
                Quaternion.Euler(0.0f, 0.0f, 0.0f)  // �ŏ��ɂǂꂾ����]���邩
                );

            if (_eyeObject)
            {
                //- �w�肵���ʒu�ɕ\���
                GameObject eye = Instantiate(
                    _eyeObject,                     // ����(�R�s�[)����Ώ�
                    transform.position,           // ���������ʒu
                    Quaternion.Euler(0.0f, 0.0f, 0.0f)  // �ŏ��ɂǂꂾ����]���邩
                    );
            }
            
            //- �R���g���[���[�̐U���̐ݒ�
            vibration.SetVibration(60, 1.0f);

            //- �����蔻���L��������
            // ���������I�u�W�F�N�g��Collider��L���ɂ���
            CollisionObject.gameObject.GetComponent<Collider>().enabled = true;
            // �����蔻��̊g��p�R���|�[�l���g��L���ɂ���
            CollisionObject.gameObject.GetComponent<DetonationCollision>().enabled = true;

            //- �������ɕ`�����߂�
            StopRenderer(gameObject);

            //- �Ήԉ��̍Đ�
            SEManager.Instance.SetPlaySE(Sound, SEVolume);
        }
    }

    private void CrackerFire()
    {
        //- �j�􎞂̓����蔻��̕\��
        if (IsDrawArea) {
            //- �_�̔z��𐶐�
            Vector3[] positions = new Vector3[_circleComplementNum + 2];
            //- �n�_�̐���
            positions[0] = this.transform.position;

            //- �~�����̓_�̐���
            for (int i = 0; i < _circleComplementNum + 1; i++) {
                //- ���S����~�����ւ̃��C�𐶐�(��������]������)
                var CircleRay = Quaternion.Euler(0, 0, (-BlastAngle / 2) + (BlastAngle / _circleComplementNum * i)) * transform.up.normalized;
                //- ���S���W�����C�����֐i�߂�
                var LineTransform = this.transform.position + (CircleRay * BlastDis);
                //- �_��ǉ�
                positions[i + 1] = LineTransform;
            }

            // �_�̐����w�肷��
            _linerend.positionCount = positions.Length;
            // ���������ꏊ���w�肷��
            _linerend.SetPositions(positions);
            //- ���ƐF�̌���
            _linerend.startWidth = 0.1f;
            _linerend.endWidth = 0.1f;
            //- �n�_�ƏI�_���Ȃ�
            _linerend.loop = true;
        }
        //- 1�t���[���O�́A���j�˗��ϐ����X�V
        _isOldExploded = _isExploded;

        //- �e����^�C�~���O�ɂȂ�܂ł́A�ȉ��̔��j�������s��Ȃ�
        if (!_isExploded) return;

        if (!_isOnce) { // ���j������̂�
            _isOnce = true;

            //- �N���b�J�[�̃G�t�F�N�g����
            GameObject fire = Instantiate(
                _particleObject,                     // ����(�R�s�[)����Ώ�
                transform.position,           // ���������ʒu
                Quaternion.Euler(0.0f, 0.0f, transform.localEulerAngles.z)  // �ŏ��ɂǂꂾ����]���邩
                );

            //- �U���̐ݒ�
            vibration.SetVibration(60, 1.0f);
            //- �Ήԉ��̍Đ�
            SEManager.Instance.SetPlaySE(Sound, SEVolume);
            //- �^�O���ԉ΂̃I�u�W�F�N�g��S�Ď擾
            GameObject[] Fireworks = GameObject.FindGameObjectsWithTag("Fireworks");
            // ���_����N���b�J�[�ւ̃x�N�g��
            Vector3 origin = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            //- �ԉ΂̃I�u�W�F�N�g��������s
            foreach (var obj in Fireworks) {
                //- ���_����ԉ΂ւ̃x�N�g��
                Vector3 direction = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
                //- �N���b�J�[����ԉ΂ւ̃x�N�g��
                Vector3 FireworkDir = direction - origin;
                //- �ԉ΂Ƃ̋������擾
                float dis = Vector3.Distance(origin, direction);
                //- �ԉ΂Ƃ̋������˒�������Ȃ������珈�����Ȃ�
                if (dis > BlastDis) continue;

                // ���g����ԉ΂Ɍ��������C���쐬
                Ray ray = new Ray(transform.position, FireworkDir);
                {
                    // ���C�����������I�u�W�F�N�g�̏�������ϐ�
                    RaycastHit hit;
                    //- ���C���΂�
                    if (Physics.Raycast(ray, out hit)) {
                        //- �X�e�[�W�ɓ��������ꍇ�������Ȃ�
                        if (hit.collider.gameObject.tag == "Stage") continue;
                    }
                }
                //- �u�ԉ΂ւ̃x�N�g���v�Ɓu�N���b�J�[�̌����x�N�g���v�̊p�x�����߂�
                var angle = Vector3.Angle((transform.up).normalized, (FireworkDir).normalized);
                if (angle != 0 && (angle < BlastAngle / 2)) {
                    float DisDelayRatio = (dis) / (BlastDis * _particleSystem.main.startSpeed.constantMin / 25) / 1.8f;
                    float DelayTime = (10 / _particleSystem.main.startSpeed.constantMin / 25) + DisDelayRatio;
                    //- �x���������Ĕ��j
                    StartCoroutine(DelayDestroy(obj, DelayTime));
                    continue;
                }

                //- �����ڕύX
                this.transform.GetChild(0).gameObject.SetActive(false);
                AfterModel.SetActive(true);
                //- �x���������Č����ڂ̃��f��������
                StartCoroutine(DelayDeleteModel(AfterModel, ModelDeleteTime));

                //- �^�O�̕ύX
                this.tag = "Untagged";
            }

            //- ���g��j�󂷂�
            Destroy(this.gameObject, _destroyTime);
        }
    }

    private void HardFire()
    {
        //- ���G���ԂłȂ����ɔ��j���L���ɂȂ����ꍇ�A��������
        if (IsExploded && !_isInvinsible) {
            //- �F�̕ύX
            this.gameObject.GetComponent<Renderer>().material.color = _invColor;
            //- �Ήԉ��̍Đ�
            SEManager.Instance.SetPlaySE(Sound, SEVolume);
            //- ���G�t���O��ݒ�
            _isInvinsible = true;
            //- ����ڂ̔��j�����X�V     
            _blastCount++;
            if (_blastCount >= _blastNum) {
                //- ���G���Ԃ̃��Z�b�g
                _invFrameCount = 0;
                // ���������I�u�W�F�N�g��SphereCollider��L���ɂ���
                this.transform.GetChild(0).gameObject.GetComponent<SphereCollider>().enabled = true;
                // ���������I�u�W�F�N�g��SphereCollider��L���ɂ���
                this.transform.GetChild(0).gameObject.GetComponent<DetonationCollision>().enabled = true;
                // �w�肵���ʒu�ɐ���
                GameObject fire = Instantiate(
                    _particleObject,                     // ����(�R�s�[)����Ώ�
                    transform.position,           // ���������ʒu
                    Quaternion.Euler(0.0f, 0.0f, 0.0f)  // �ŏ��ɂǂꂾ����]���邩
                    );

                //- �R���g���[���[�̐U���̐ݒ�
                vibration.SetVibration(60, 1.0f);

                // �������ɓ����蔻��𖳌���
                GetComponent<SphereCollider>().isTrigger = true;
                GetComponent<MeshRenderer>().enabled = false;
            }
        }

        if (_isInvinsible) {
            //- ���G���Ԃ̃J�E���g
            _invFrameCount++;
            //- ���G���ԏI�����̏���
            if (_invFrameCount >= _blastInvSeconds * 60) {
                //- �F�̕ύX
                this.gameObject.GetComponent<Renderer>().material.color = _initColor;
                //- �܂��������Ă��Ȃ��񐔂Ȃ珈��
                if (_blastNum > _blastCount) {
                    _isInvinsible = false;
                    _isExploded = false;
                }
            }
        }
    }

    private void MultiBlastFire()
    {
        //- ���G���ԂłȂ����ɔ��j���L���ɂȂ����ꍇ�A��������
        if (IsExploded && !_isInvinsible) {
            //- �F�̕ύX
            this.gameObject.GetComponent<Renderer>().material.color = _invColor;
            //- �Ήԉ��̍Đ�
            SEManager.Instance.SetPlaySE(Sound, SEVolume);
            //- �����񐔂��X�V
            _blastCount++;
            //- ���G�t���O��ݒ�
            _isInvinsible = true;
            //- ���G���Ԃ̃��Z�b�g
            _invFrameCount = 0;
            // ���������I�u�W�F�N�g��SphereCollider��L���ɂ���
            _collisionObject.GetComponent<SphereCollider>().enabled = true;
            _collisionObject.GetComponent<DetonationCollision>().enabled = true;
            // �w�肵���ʒu�ɐ���
            GameObject fire = Instantiate(
                _particleObject,                     // ����(�R�s�[)����Ώ�
                transform.position,           // ���������ʒu
                Quaternion.Euler(0.0f, 0.0f, 0.0f)  // �ŏ��ɂǂꂾ����]���邩
                );

            //- �R���g���[���[�̐U���̐ݒ�
            vibration.SetVibration(60, 1.0f);

            // �������ɓ����蔻��𖳌���
            GetComponent<SphereCollider>().isTrigger = true;
            //- ���ȏ㔚����������s���鏈��
            if (_blastCount >= _blastNum) {
                GetComponent<MeshRenderer>().enabled = false;
            }
        }
        if (_isInvinsible) {
            //- ���G���Ԃ̃J�E���g
            _invFrameCount++;
            //- ���G���ԏI�����̏���
            if (_invFrameCount >= _blastInvSeconds * 60) {
                //- �������̔����ݒ�
                _isInvinsible = false;
                //- �F�̕ύX
                this.gameObject.GetComponent<Renderer>().material.color = _initColor;
                // ���������I�u�W�F�N�g��SphereCollider�𖳌��ɂ���
                this.transform.GetChild(0).gameObject.GetComponent<SphereCollider>().enabled = false;
                // ���������I�u�W�F�N�g��SphereCollider�𖳌��ɂ���
                this.transform.GetChild(0).gameObject.GetComponent<DetonationCollision>().enabled = false;
                // �������ɓ����蔻���L����
                GetComponent<SphereCollider>().isTrigger = false;
                //- ���������������
                _isExploded = false;
            }
        }
    }

    private void ResurrectionBoxFire()
    {
        if (!_isOnce) { //- ��������
            _isOnce = true;
            //- SpawnPlayer���\�b�h��delayTime�b��ɌĂяo��
            StartCoroutine(SpawnPlayer(_delayTime));
        }
    }

    //- �x��ċN������֐�
    private IEnumerator DelayDestroy(GameObject obj, float delayTime)
    {
        //- delayTime�b�ҋ@����
        yield return new WaitForSeconds(delayTime);
        //- �N�������L���ɂ���
        if (obj) obj.GetComponent<FireworksModule>().Ignition();
    }

    //- �x��Č����ڂ������֐�
    private IEnumerator DelayDeleteModel(GameObject obj, float delayTime)
    {
        //- delayTime�b�ҋ@����
        yield return new WaitForSeconds(delayTime);
        //- �����ڂ�����
        obj.SetActive(false);
    }

    //- �v���C���[���Đ�������֐�
    private IEnumerator SpawnPlayer(float delayTime)
    {
        //- delayTime�b�ҋ@����
        yield return new WaitForSeconds(delayTime);

        //- �A�j���[�V�����p�̕ϐ�
        float elapsed = 0;

        //- ���X�ɐ�������v���C���[�̐�
        int numPlayers = 1;

        //- �v���C���[�����X�ɐ�������
        for (int i = 0; i < numPlayers; i++) 
        {
            //- �v���C���[�𐶐�����
            Vector3 spawnPosition = new Vector3(
                transform.position.x, transform.position.y, transform.position.z);
            GameObject player = Instantiate(
                _playerPrefab, spawnPosition, Quaternion.identity);

            //- �������̍Đ�
            SEManager.Instance.SetPlaySE(GeneratedSound, SEVolume);

            //- SceneChange�X�N���v�g�̃v���C���[�����t���O��true�ɂ���
            sceneChange.bIsLife = true;

            //- ���X�ɐ�������A�j���[�V����
            while (elapsed < _animationTime) 
            {
                float t = elapsed / _animationTime;
                player.transform.localScale = 
                    Vector3.Lerp(Vector3.zero, Vector3.one, t);
                elapsed += Time.deltaTime;
                yield return null;
            }

            //- �A�j���[�V�����̒x��
            yield return new WaitForSeconds(_animationDelayTime);
        }

        //- �v���C���[�𐶐���A�����������X�ɏ���
        float startTime = Time.time;
        Vector3 initialScale = transform.localScale;

        //- ���ŉ��̍Đ�
        SEManager.Instance.SetPlaySE(DisSound, SEVolume);

        //- �����������X�ɏ��ł�����
        while (Time.time < startTime + _boxDisTime) 
        {
            float t = (Time.time - startTime) / _boxDisTime;
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);
            yield return null;
        }
        Destroy(gameObject);
    }
}
