using UnityEngine;

/// <summary>
/// ���̃I�u�W�F�N�g�ԂŎ擾�o����SEManager
/// </summary>
public class SEManager : MonoBehaviour
{
    //- SEManager�̃C���X�^���X��ێ�����ϐ�
    private static SEManager _instance;

    //- SEManager�̃C���X�^���X���擾����ׂ̃v���p�e�B
    public static SEManager Instance { get { return _instance; } }

    //- AudioSource�R���|�[�l���g��ێ�����ϐ�
    private AudioSource _audioSource;

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

    //- SE���Đ�����֐�
    //  clip �炵������
    //  volume �ݒ艹��
    public void SetPlaySE(AudioClip clip, float volume)
    {
        //- AudioClip�����݂��Ă��Ȃ��ꍇ
        if (clip == null)
        {
            Debug.LogError("Null AudioClip");
            return;
        }

        //- �����œn���ꂽAudioClip���Đ�����
        _audioSource.PlayOneShot(clip, volume);
    }
}