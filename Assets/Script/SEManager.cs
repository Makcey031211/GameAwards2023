using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// SEManager
/// </summary>
public class SEManager : MonoBehaviour
{
    //- �񋓌^��`(SE)
    public enum SoundEffect
    {
        //* �ԉΊ֘A */
        Explosion,  // ����
        Spark,      // �Ή�
        Belt,       // �ł��グ
        BossBelt,   // �{�X�ł��グ
        //* �N���b�J�[�֘A */
        Brust,      // �j��
        Reservoir,  // ����
        Ignition,   // ����
        //* �������֘A */
        Generated,  // ����
        Extinction, // ����
        //* �V�[���֘A */
        Click,      // �N���b�N
        Select,     // �{�^���I��
        Clear,      // �N���A
        Failure,    // ���s
    }

    //---------------------------------------
    [SerializeField, HideInInspector]
    public AudioClip explosion;
    [SerializeField, HideInInspector]
    public AudioClip spark;
    [SerializeField, HideInInspector]
    public AudioClip belt;
    [SerializeField, HideInInspector]
    public AudioClip bossbelt;
    [SerializeField, HideInInspector]
    public AudioClip brust;
    [SerializeField, HideInInspector]
    public AudioClip reservoir;
    [SerializeField, HideInInspector]
    public AudioClip ignition;
    [SerializeField, HideInInspector]
    public AudioClip generated;
    [SerializeField, HideInInspector]
    public AudioClip extinction;
    [SerializeField, HideInInspector]
    public AudioClip click;
    [SerializeField, HideInInspector]
    public AudioClip select;
    [SerializeField, HideInInspector]
    public AudioClip clear;
    [SerializeField, HideInInspector]
    public AudioClip failure;
    [SerializeField, HideInInspector]
    [Range(0f,1f)] public float volume;
    [SerializeField, HideInInspector]
    [Range(0f, 1f)] public float pitch;
    [SerializeField, HideInInspector]
    public bool loop;
    //---------------------------------------

    //- SEManager�̃C���X�^���X��ێ�����ϐ�
    private static SEManager _instance;

    //- SEManager�̃C���X�^���X���擾����ׂ̃v���p�e�B
    public static SEManager Instance { get { return _instance; } }

    //- AudioSource�R���|�[�l���g��ێ�����ϐ�
    private AudioSource _audioSource;

    //- enum�̌^�ƁAAudioClip�̃}�b�s���O���i�[����
    private Dictionary<SoundEffect, AudioClip> audioClips;
    
    private void Awake()
    {
        //- ���ɃC���X�^���X������ꍇ�́A���g��j������
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        //- �C���X�^���X��ێ�����
        _instance = this;

        //- �V�[����J�ڂ��Ă��I�u�W�F�N�g��j�����Ȃ�
        DontDestroyOnLoad(this.gameObject);

        //- AudioSource�R���|�[�l���g���擾����
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioClips = new Dictionary<SoundEffect, AudioClip>();

        //- enum��AudioClip���֘A�t��������ׂ̏�����
        // �ԉΊ֘A
        FireWorksSE();
        // �N���b�J�[�֘A
        CrackerSE();
        // �������֘A
        ResurrectionBoxSE();
        // �V�[���֘A
        SceneSE();
    }

    private void FireWorksSE()
    {
        audioClips.Add(SoundEffect.Explosion, explosion);
        audioClips.Add(SoundEffect.Spark, spark);
        audioClips.Add(SoundEffect.Belt, belt);
        audioClips.Add(SoundEffect.BossBelt, bossbelt);
    }

    private void CrackerSE()
    {
        audioClips.Add(SoundEffect.Brust, brust);
        audioClips.Add(SoundEffect.Reservoir, reservoir);
        audioClips.Add(SoundEffect.Ignition, ignition);
    }

    private void ResurrectionBoxSE()
    {
        audioClips.Add(SoundEffect.Generated, generated);
        audioClips.Add(SoundEffect.Extinction, extinction);
    }

    private void SceneSE()
    {
        audioClips.Add(SoundEffect.Click, click);
        audioClips.Add(SoundEffect.Select, select);
        audioClips.Add(SoundEffect.Clear, clear);
        audioClips.Add(SoundEffect.Failure, failure);
    }

    public float Pitch
    {
        get { return pitch; }
        set { pitch = value; }
    }

    public bool Loop
    {
        get { return loop; }
        set { loop = value; }
    }

    /// <summary>
    /// SE���Đ�������֐�
    /// </summary>
    /// <param name="soundEffect">��</param>
    public void SetPlaySE(SoundEffect soundEffect)
    {
        //- AudioClip�����݂��Ă��Ȃ��ꍇ
        if (!audioClips.ContainsKey(soundEffect))
        {
            Debug.LogError(soundEffect.ToString() + "AudioCilp not Sound");
            return;
        }

        //- �����œn���ꂽAudioClip���Đ�����
        _audioSource.pitch = Pitch;
        _audioSource.loop  = Loop;
        _audioSource.PlayOneShot(audioClips[soundEffect], volume);
    }
}