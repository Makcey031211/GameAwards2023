using UnityEngine;
using System.Collections.Generic;

/*
 ===================
 制作：髙橋
 概要：SEを管理するスクリプト
 ===================
 */
public class SEManager : MonoBehaviour
{
    //- 列挙型定義(SE)
    public enum E_SoundEffect
    {
        //* 花火関連 */
        Explosion,  // 爆発
        YanagiFire, // 柳花火
        TonboFire,  // トンボ花火
        DragonFire, // ドラゴン花火
        BarrierDes, // バリア破壊
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
        Slide,      // スライド
        //* 開幕演出関連 */
        Opening,    // 開始
        //* カットイン関連 */
        Letterapp,  // 文字出現
    }

    //--- インスペクターに表示
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

    //- SEManagerのインスタンスを保持する変数
    private static SEManager _instance;

    //- SEManagerのインスタンスを取得する為のプロパティ
    public static SEManager Instance { get { return _instance; } }

    //- AudioSourceコンポーネントを保持する変数
    private AudioSource _audioSource;

    //- 音の種類とそれに対応するオーディオクリップを保持する辞書
    private Dictionary<E_SoundEffect, AudioClip> audioClips;
    
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
        audioClips = new Dictionary<E_SoundEffect, AudioClip>();

        // 花火関連
        FireWorksSE();
        // クラッカー関連
        CrackerSE();
        // 復活箱関連
        ResurrectionBoxSE();
        // シーン関連
        SceneSE();
        // 開幕演出関連
        OpeningSE();
        // カットイン関連
        CutInSE();
    }

    /// <summary>
    /// 花火の効果音を辞書に追加する
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
    /// クラッカーの効果音を辞書に追加する
    /// </summary>
    private void CrackerSE()
    {
        audioClips.Add(E_SoundEffect.Brust, brust);
        audioClips.Add(E_SoundEffect.Reservoir, reservoir);
        audioClips.Add(E_SoundEffect.Ignition, ignition);
    }

    /// <summary>
    /// 復活箱の効果音を辞書に追加する
    /// </summary>
    private void ResurrectionBoxSE()
    {
        audioClips.Add(E_SoundEffect.Generated, generated);
        audioClips.Add(E_SoundEffect.Extinction, extinction);
    }

    /// <summary>
    /// シーンの効果音を辞書に追加する
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
    /// 開幕演出の効果音を辞書に追加する
    /// </summary>
    private void OpeningSE()
    {
        audioClips.Add(E_SoundEffect.Opening, opening);
    }

    /// <summary>
    /// カットインの効果音を辞書に追加する
    /// </summary>
    private void CutInSE()
    {
        audioClips.Add(E_SoundEffect.Letterapp, letterapp);
    }

    /// <summary>
    /// 音声のピッチを制御するプロパティ
    /// </summary>
    public float Pitch
    {
        get { return pitch; }
        set { pitch = value; }
    }

    /// <summary>
    /// SEを再生させる関数
    /// </summary>
    /// <param name="E_SoundEffect">再生するSEの列挙型</param>
    public void SetPlaySE(E_SoundEffect E_SoundEffect)
    {
        //- AudioClipが存在していない場合
        if (!audioClips.ContainsKey(E_SoundEffect))
        {
            Debug.LogError(E_SoundEffect.ToString() + "AudioCilp not Sound");
            return;
        }

        //- 引数で渡されたAudioClipを再生する
        _audioSource.pitch = Pitch;
        _audioSource.PlayOneShot(audioClips[E_SoundEffect], volume);
    }

    /// <summary>
    /// SEを再生させる関数(エフェクト専用)
    /// </summary>
    /// <param name="E_SoundEffect">再生するSEの列挙型</param>
    /// <param name="Volume">設定音量</param>
    public void EffectSetPlaySE(E_SoundEffect E_SoundEffect, float Volume)
    {
        //- AudioClipが存在していない場合
        if (!audioClips.ContainsKey(E_SoundEffect))
        {
            Debug.LogError(E_SoundEffect.ToString() + "AudioCilp not Sound");
            return;
        }

        //- 引数で渡されたAudioClipを再生する
        _audioSource.PlayOneShot(audioClips[E_SoundEffect], Volume);
    }
}