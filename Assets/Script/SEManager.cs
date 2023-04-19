using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// SEManager
/// </summary>
public class SEManager : MonoBehaviour
{
    //- 列挙型定義
    public enum SoundEffect
    {
        //* 花火関連 */
        Firework,   // 爆発
        Spark,      // 火花
        Belt,       // 打ち上げ
        Reservoir,  // クラッカー溜め
        //* シーン関連 */
        Failure,    // 失敗
        Click,      // クリック
    }

    //- インスペクターに表示
    [Header("花火関連")]
    [SerializeField, Header("爆発音")]
    private AudioClip firework;
    [SerializeField, Header("火花音")]
    private AudioClip spark;
    [SerializeField, Header("打ち上げ音")]
    private AudioClip belt;
    [SerializeField, Header("クラッカー溜め音")]
    private AudioClip reservoir;
    [Header("クリア関連")]
    [SerializeField, Header("失敗音")]
    private AudioClip failure;
    [SerializeField, Header("クリック音")]
    private AudioClip click;

    //- SEManagerのインスタンスを保持する変数
    private static SEManager _instance;

    //- SEManagerのインスタンスを取得する為のプロパティ
    public static SEManager Instance { get { return _instance; } }

    //- AudioSourceコンポーネントを保持する変数
    private AudioSource _audioSource;

    //- enumの型と、AudioClipのマッピングを格納する
    private Dictionary<SoundEffect, AudioClip> audioClips;

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

    private void Start()
    {
        audioClips = new Dictionary<SoundEffect, AudioClip>();

        //- enumとAudioClipを関連付けさせる為の初期化
        // 花火関連
        audioClips.Add(SoundEffect.Firework, firework);
        audioClips.Add(SoundEffect.Spark, spark);
        audioClips.Add(SoundEffect.Belt, belt);
        audioClips.Add(SoundEffect.Reservoir, reservoir);
        // シーン関連
        audioClips.Add(SoundEffect.Click, click);
        audioClips.Add(SoundEffect.Failure, failure);
    }

    /// <summary>
    /// SEを再生させる関数
    /// </summary>
    /// <param name="soundEffect">音</param>
    /// <param name="volume">音量</param>
    /// <param name="pitch">再生速度</param>
    /// <param name="loop">持続するかどうか</param>
    public void SetPlaySE(SoundEffect soundEffect, float volume, float pitch, bool loop)
    {
        //- AudioClipが存在していない場合
        if (!audioClips.ContainsKey(soundEffect))
        {
            Debug.LogError(soundEffect.ToString() + "AudioCilp not Sound");
            return;
        }

        //- 引数で渡されたAudioClipを再生する
        _audioSource.pitch = pitch;
        _audioSource.loop  = loop;
        _audioSource.PlayOneShot(audioClips[soundEffect], volume);
    }
}