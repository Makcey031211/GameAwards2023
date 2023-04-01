using UnityEngine;

/// <summary>
/// 他のオブジェクト間で取得出来るSEManager
/// </summary>
public class SEManager : MonoBehaviour
{
    //- SEManagerのインスタンスを保持する変数
    private static SEManager _instance;

    //- SEManagerのインスタンスを取得する為のプロパティ
    public static SEManager Instance { get { return _instance; } }

    //- AudioSourceコンポーネントを保持する変数
    private AudioSource _audioSource;

    private void Awake()
    {
        //- 既にインスタンスがある場合は、自身を破棄する
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        //- インスタンスを保持する
        _instance = this;

        //- シーンを遷移してもオブジェクトを破棄しない
        DontDestroyOnLoad(this.gameObject);

        //- AudioSourceコンポーネントを取得する
        _audioSource = GetComponent<AudioSource>();
    }

    //- SEを再生する関数
    //  clip 鳴らしたい音
    //  volume 設定音量
    public void SetPlaySE(AudioClip clip, float volume)
    {
        //- AudioClipが存在していない場合
        if (clip == null)
        {
            Debug.LogError("Null AudioClip");
            return;
        }

        //- 引数で渡されたAudioClipを再生する
        _audioSource.PlayOneShot(clip, volume);
    }
}