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
        Explosion,  // 爆発
        Spark,      // 火花
        Belt,       // 打ち上げ
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
        Failure,    // 失敗
    }

    //- インスペクターに表示
    [Header("花火関連")]
    [SerializeField, Header("爆発音")]
    private AudioClip explosion;
    [SerializeField, Header("火花音")]
    private AudioClip spark;
    [SerializeField, Header("打ち上げ音")]
    private AudioClip belt;
    [Header("クラッカー関連")]
    [SerializeField, Header("破裂音")]
    private AudioClip brust;
    [SerializeField, Header("溜め音")]
    private AudioClip reservoir;
    [SerializeField, Header("着火音")]
    private AudioClip ignition;
    [Header("復活箱関連")]
    [SerializeField, Header("生成音")]
    private AudioClip generated;
    [SerializeField, Header("消滅音")]
    private AudioClip extinction;
    [Header("シーン関連")]
    [SerializeField, Header("クリック音")]
    private AudioClip click;
    [SerializeField, Header("ボタン選択音")]
    private AudioClip select;
    [SerializeField, Header("失敗音")]
    private AudioClip failure;
    //- 外部から音量を設定
    [Header("設定項目")]
    [SerializeField, Header("SEの音量")]
    public float volume = 1.0f;

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
        audioClips.Add(SoundEffect.Failure, failure);
    }

    /// <summary>
    /// SEを再生させる関数
    /// </summary>
    /// <param name="soundEffect">音</param>
    /// <param name="volume">音量</param>
    /// <param name="pitch">再生速度</param>
    /// <param name="loop">持続するかどうか</param>
    public void SetPlaySE(SoundEffect soundEffect, float pitch, bool loop)
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