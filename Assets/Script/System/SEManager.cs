using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// SEManager
/// </summary>
public class SEManager : MonoBehaviour
{
    //- 列挙型定義(SE)
    public enum SoundEffect
    {
        //* 花火関連 */
        Explosion,  // 爆発
        Spark,      // 火花
        Belt,       // 打ち上げ
        BossBelt,   // ボス打ち上げ
        //* クラッカー関連 */
        Brust,      // 破裂
        Reservoir,  // 溜め
        Ignition,   // 着火
        //* 復活箱関連 */
        Generated,  // 生成
        Extinction, // 消滅
        //* シーン関連 */
        Click,      // クリック
        Select,     // ボタン選択
        Clear,      // クリア
        Failure,    // 失敗
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
        FireWorksSE();
        // クラッカー関連
        CrackerSE();
        // 復活箱関連
        ResurrectionBoxSE();
        // シーン関連
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
    /// SEを再生させる関数
    /// </summary>
    /// <param name="soundEffect">音</param>
    public void SetPlaySE(SoundEffect soundEffect)
    {
        //- AudioClipが存在していない場合
        if (!audioClips.ContainsKey(soundEffect))
        {
            Debug.LogError(soundEffect.ToString() + "AudioCilp not Sound");
            return;
        }

        //- 引数で渡されたAudioClipを再生する
        _audioSource.pitch = Pitch;
        _audioSource.loop  = Loop;
        _audioSource.PlayOneShot(audioClips[soundEffect], volume);
    }
}