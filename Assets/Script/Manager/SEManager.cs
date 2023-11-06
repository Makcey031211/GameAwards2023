using UnityEngine;
using System.Collections.Generic;

/*
 ===================
 ����F����
 �T�v�FSE���Ǘ�����X�N���v�g
 ===================
 */
public class SEManager : MonoBehaviour
{
    //- �񋓌^��`(SE)
    public enum E_SoundEffect
    {
        //* �ԉΊ֘A */
        Explosion,  // ����
        YanagiFire, // ���ԉ�
        TonboFire,  // �g���{�ԉ�
        DragonFire, // �h���S���ԉ�
        BarrierDes, // �o���A�j��
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
        Slide,      // �X���C�h
        //* �J�����o�֘A */
        Opening,    // �J�n
        //* �J�b�g�C���֘A */
        Letterapp,  // �����o��
    }

    //--- �C���X�y�N�^�[�ɕ\��
    [SerializeField, HideInInspector]
    public AudioClip explosion;
    [SerializeField, HideInInspector]
    public AudioClip yanagifire;
    [SerializeField, HideInInspector]
    public AudioClip tonbofire;
    [SerializeField, HideInInspector]
    public AudioClip dragonfire;
    [SerializeField, HideInInspector]
    public AudioClip barrierdes;
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
    public AudioClip slide;
    [SerializeField, HideInInspector]
    public AudioClip opening;
    [SerializeField, HideInInspector]
    public AudioClip letterapp;

    [SerializeField, HideInInspector]
    [Range(0f,1f)] public float volume;
    [SerializeField, HideInInspector]
    [Range(0f,1f)] public float pitch;
    //---------------------------------------

    //- SEManager�̃C���X�^���X��ێ�����ϐ�
    private static SEManager _instance;

    //- SEManager�̃C���X�^���X���擾����ׂ̃v���p�e�B
    public static SEManager Instance { get { return _instance; } }

    //- AudioSource�R���|�[�l���g��ێ�����ϐ�
    private AudioSource _audioSource;

    //- ���̎�ނƂ���ɑΉ�����I�[�f�B�I�N���b�v��ێ����鎫��
    private Dictionary<E_SoundEffect, AudioClip> audioClips;
    
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
        audioClips = new Dictionary<E_SoundEffect, AudioClip>();

        // �ԉΊ֘A
        FireWorksSE();
        // �N���b�J�[�֘A
        CrackerSE();
        // �������֘A
        ResurrectionBoxSE();
        // �V�[���֘A
        SceneSE();
        // �J�����o�֘A
        OpeningSE();
        // �J�b�g�C���֘A
        CutInSE();
    }

    /// <summary>
    /// �ԉ΂̌��ʉ��������ɒǉ�����
    /// </summary>
    private void FireWorksSE()
    {
        audioClips.Add(E_SoundEffect.Explosion, explosion);
        audioClips.Add(E_SoundEffect.YanagiFire, yanagifire);
        audioClips.Add(E_SoundEffect.TonboFire, tonbofire);
        audioClips.Add(E_SoundEffect.DragonFire, dragonfire);
        audioClips.Add(E_SoundEffect.BarrierDes, barrierdes);
        audioClips.Add(E_SoundEffect.Belt, belt);
        audioClips.Add(E_SoundEffect.BossBelt, bossbelt);
    }

    /// <summary>
    /// �N���b�J�[�̌��ʉ��������ɒǉ�����
    /// </summary>
    private void CrackerSE()
    {
        audioClips.Add(E_SoundEffect.Brust, brust);
        audioClips.Add(E_SoundEffect.Reservoir, reservoir);
        audioClips.Add(E_SoundEffect.Ignition, ignition);
    }

    /// <summary>
    /// �������̌��ʉ��������ɒǉ�����
    /// </summary>
    private void ResurrectionBoxSE()
    {
        audioClips.Add(E_SoundEffect.Generated, generated);
        audioClips.Add(E_SoundEffect.Extinction, extinction);
    }

    /// <summary>
    /// �V�[���̌��ʉ��������ɒǉ�����
    /// </summary>
    private void SceneSE()
    {
        audioClips.Add(E_SoundEffect.Click, click);
        audioClips.Add(E_SoundEffect.Select, select);
        audioClips.Add(E_SoundEffect.Clear, clear);
        audioClips.Add(E_SoundEffect.Failure, failure);
        audioClips.Add(E_SoundEffect.Slide, slide);
    }

    /// <summary>
    /// �J�����o�̌��ʉ��������ɒǉ�����
    /// </summary>
    private void OpeningSE()
    {
        audioClips.Add(E_SoundEffect.Opening, opening);
    }

    /// <summary>
    /// �J�b�g�C���̌��ʉ��������ɒǉ�����
    /// </summary>
    private void CutInSE()
    {
        audioClips.Add(E_SoundEffect.Letterapp, letterapp);
    }

    /// <summary>
    /// �����̃s�b�`�𐧌䂷��v���p�e�B
    /// </summary>
    public float Pitch
    {
        get { return pitch; }
        set { pitch = value; }
    }

    /// <summary>
    /// SE���Đ�������֐�
    /// </summary>
    /// <param name="E_SoundEffect">�Đ�����SE�̗񋓌^</param>
    public void SetPlaySE(E_SoundEffect E_SoundEffect)
    {
        //- AudioClip�����݂��Ă��Ȃ��ꍇ
        if (!audioClips.ContainsKey(E_SoundEffect))
        {
            Debug.LogError(E_SoundEffect.ToString() + "AudioCilp not Sound");
            return;
        }

        //- �����œn���ꂽAudioClip���Đ�����
        _audioSource.pitch = Pitch;
        _audioSource.PlayOneShot(audioClips[E_SoundEffect], volume);
    }

    /// <summary>
    /// SE���Đ�������֐�(�G�t�F�N�g��p)
    /// </summary>
    /// <param name="E_SoundEffect">�Đ�����SE�̗񋓌^</param>
    /// <param name="Volume">�ݒ艹��</param>
    public void EffectSetPlaySE(E_SoundEffect E_SoundEffect, float Volume)
    {
        //- AudioClip�����݂��Ă��Ȃ��ꍇ
        if (!audioClips.ContainsKey(E_SoundEffect))
        {
            Debug.LogError(E_SoundEffect.ToString() + "AudioCilp not Sound");
            return;
        }

        //- �����œn���ꂽAudioClip���Đ�����
        _audioSource.PlayOneShot(audioClips[E_SoundEffect], Volume);
    }
}