using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// SEManager
/// </summary>
public class SEManager : MonoBehaviour
{
    //- �񋓌^��`
    public enum SoundEffect
    {
        //* �ԉΊ֘A */
        Firework,   // ����
        Spark,      // �Ή�
        Belt,       // �ł��グ
        Reservoir,  // �N���b�J�[����
        //* �V�[���֘A */
        Failure,    // ���s
        Click,      // �N���b�N
    }

    //- �C���X�y�N�^�[�ɕ\��
    [Header("�ԉΊ֘A")]
    [SerializeField, Header("������")]
    private AudioClip firework;
    [SerializeField, Header("�Ήԉ�")]
    private AudioClip spark;
    [SerializeField, Header("�ł��グ��")]
    private AudioClip belt;
    [SerializeField, Header("�N���b�J�[���߉�")]
    private AudioClip reservoir;
    [Header("�N���A�֘A")]
    [SerializeField, Header("���s��")]
    private AudioClip failure;
    [SerializeField, Header("�N���b�N��")]
    private AudioClip click;

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
        audioClips.Add(SoundEffect.Firework, firework);
        audioClips.Add(SoundEffect.Spark, spark);
        audioClips.Add(SoundEffect.Belt, belt);
        audioClips.Add(SoundEffect.Reservoir, reservoir);
        // �V�[���֘A
        audioClips.Add(SoundEffect.Click, click);
        audioClips.Add(SoundEffect.Failure, failure);
    }

    /// <summary>
    /// SE���Đ�������֐�
    /// </summary>
    /// <param name="soundEffect">��</param>
    /// <param name="volume">����</param>
    /// <param name="pitch">�Đ����x</param>
    /// <param name="loop">�������邩�ǂ���</param>
    public void SetPlaySE(SoundEffect soundEffect, float volume, float pitch, bool loop)
    {
        //- AudioClip�����݂��Ă��Ȃ��ꍇ
        if (!audioClips.ContainsKey(soundEffect))
        {
            Debug.LogError(soundEffect.ToString() + "AudioCilp not Sound");
            return;
        }

        //- �����œn���ꂽAudioClip���Đ�����
        _audioSource.pitch = pitch;
        _audioSource.loop  = loop;
        _audioSource.PlayOneShot(audioClips[soundEffect], volume);
    }
}