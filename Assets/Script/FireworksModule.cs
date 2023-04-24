using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class FireworksModule : MonoBehaviour
{
    // 花火の種類用の列挙型
    public enum FireworksType
    {
        Normal,
        Cracker,
        Hard,
        Double,
        ResurrectionBox,
        Boss,
    }

    //- 共通の項目
    //-- インスペクターに表示
    [SerializeField, Header("花火の種類")]
    private FireworksType _type = FireworksType.Normal;
    [SerializeField, Header("火花用のオブジェクト")]
    private GameObject _particleObject;
    [SerializeField, HideInInspector]
    public GameObject _collisionObject; // 当たり判定用オブジェクト   通常、ハード、マルチブラスト
    [SerializeField, Header("破裂後の表情オブジェクト")]
    public GameObject _eyeObject; // 破裂後表情用オブジェクト
    //-- インスペクターから非表示
    private VibrationManager vibration; // コントローラーの振動用
    private bool _isExploded; // 爆発フラグ
    private bool _isOnce; // 一回だけフラグ
    private float pitch  = 1.0f; // SEの再生速度
    //-- 外部からの値取得用
    public FireworksType Type => _type;
    public GameObject ParticleObject => _particleObject;
    public bool IsExploded => _isExploded;
    public GameObject CollisionObject => _collisionObject;
    public GameObject EyeObject => _eyeObject;


    //- クラッカーの項目
    //-- インスペクターに表示
    [SerializeField, HideInInspector]
    public int _circleComplementNum; // 円の分割数
    [SerializeField, HideInInspector]
    public float _blastAngle; // 判定の範囲角度(0～180度)
    [SerializeField, HideInInspector]
    public float _blastDis; // 射程
    [SerializeField, HideInInspector]
    public float _modelDeleteTime; // 破裂後モデルの残留時間
    [SerializeField, HideInInspector]
    public bool _isDrawArea = true; // 判定範囲の描画フラグ
    //-- インスペクターから非表示
    private float _destroyTime = 3.0f;    // 完全にオブジェクトを消去する時間
    private LineRenderer _linerend;       // 当たり判定表示用の線
    private ParticleSystem _particleSystem;     // パーティクルシステム
    //-- 外部からの値取得用
    public int CircleComplementNum => _circleComplementNum;
    public float BlastAngle => _blastAngle;
    public float BlastDis => _blastDis;
    public float ModelDeleteTime => _modelDeleteTime;
    public bool IsDrawArea => _isDrawArea;


    //- ハード、二重花火の項目
    //-- インスペクターに表示
    [SerializeField, HideInInspector]
    public float _blastInvSeconds = 3.0f; // 爆発後無敵時間
    [SerializeField, HideInInspector]
    public Color _invColor; // 無敵時間中の色(RGB)
    //-- インスペクターから非表示
    private int _invFrameCount = 0; // 無敵時間用のフレームカウンタ
    private int _blastCount = 0; // 何回爆発したか
    private Color _initColor; // マテリアルの初期の色
    private bool _isInvinsible = false; // 爆発中かどうか
    //-- 外部からの値取得用
    public float BlastInvSeconds => _blastInvSeconds;
    public Color InvColor => _invColor;


    //- ハード専用の項目
    [SerializeField, HideInInspector]
    public int _blastNum = 2;  // 何回目で爆発するか
    public int BlastNum => _blastNum;

    //- 復活箱用の項目
    //-- インスペクターに表示
    [SerializeField, HideInInspector]
    public GameObject _playerPrefab; // 生成するオブジェクト
    [SerializeField, HideInInspector]
    public float _delayTime = 0.1f; // 生成までの待ち時間(秒)
    [SerializeField, HideInInspector]
    public float _animationTime = 0.1f; // アニメーション時間(秒)
    [SerializeField, HideInInspector]
    public float _animationDelayTime = 0.1f; // アニメーションの遅延時間(秒)
    [SerializeField, HideInInspector]
    public float _boxDisTime = 0.1f; // 箱の消滅時間(秒)
    //-- インスペクターから非表示
    SceneChange sceneChange;
    //-- 外部からの値取得用
    public GameObject PlayerPrefab => _playerPrefab;
    public float DelayTime => _delayTime;
    public float AnimationTime => _animationTime;
    public float AnimationDelayTime => _animationDelayTime;
    public float BoxDisTime => _boxDisTime;

    //- ぬし花火用の項目
    //-- インスペクターに表示
    [SerializeField, HideInInspector]
    public int _ignitionMax = 3;  // 何回目で爆発するか
    [SerializeField, HideInInspector]
    public GameObject _movieObject; // 演出を管理しているオブジェクト
    //-- インスペクターから非表示
    private int ignitionCount = 0; // 何回引火したか
    private float moveTimeCount = 0; // ぬし花火用の挙動用の変数
    //-- 外部からの値取得用
    public int IgnitionMax => _ignitionMax;
    public GameObject MovieObject => _movieObject;

    // Start is called before the first frame update
    void Start()
    {
        //- 共通項目
        vibration = GameObject.Find("VibrationManager").GetComponent<VibrationManager>();
        _isExploded = false;
        _isOnce = false;

        //- クラッカーの項目
        _linerend = gameObject.AddComponent<LineRenderer>(); // 線の追加
        vibration = GameObject.Find("VibrationManager").GetComponent<VibrationManager>(); // 振動コンポーネントの取得
        _particleSystem = ParticleObject.transform.GetChild(0).GetComponent<ParticleSystem>(); // パーティクルの取得

        //- 二重花火の項目
        if (_type == FireworksType.Double ) {
            _collisionObject.GetComponent<DetonationCollision>().IsDoubleBlast = true;
        }

        //- 復活箱の項目
        sceneChange = GameObject.FindWithTag("MainCamera").GetComponent<SceneChange>();
    }

    // Update is called once per frame
    void Update()
    {

        if (IsExploded) { // 爆発した後
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
            case FireworksType.Double:
                DoubleFire();
                break;
            case FireworksType.ResurrectionBox:
                ResurrectionBoxFire();
                break;
            default:
                break;
            }
        }
    }

    // 爆発時に子オブジェクト含め描画をやめる処理
    void StopRenderer(GameObject gameObject)
    {
        var renderer = GetComponentsInChildren<Renderer>();

        for (int i = 0; i < renderer.Length; i++) {
            renderer[i].enabled = false;
        }
    }

    // 爆発フラグを立てる処理
    public void Ignition()
    {
        _isExploded = true;
    }

    // ぬし花火用の引火処理
    public void IgnitionBoss(GameObject obj)
    {   
        //- 引火回数を増やす
        ignitionCount++;

        if (ignitionCount < _ignitionMax) return; // 引火回数が必要回数に満たなければリターン
        _isExploded = true; //- 爆発フラグ

        //- アニメーション処理
        transform.DOMoveY(-15, 1.5f).SetEase(Ease.OutSine).SetLink(gameObject);
        transform.DOMoveY(20, 0.7f).SetEase(Ease.OutSine).SetDelay(1.5f).SetLink(gameObject);
        //- 演出用スクリプトの取得
        MovieManager movie = MovieObject.GetComponent<MovieManager>();
        //- 演出フラグ変更
        movie.SetMovieFlag(true);
        //- 演出開始
        DOVirtual.DelayedCall(2.1f, () => movie.StartVillageMovie(), false);
        //- 破壊処理
        Destroy(gameObject, 2.2f);
    }

    private void NormalFire()
    {
        if (!_isOnce) { // 爆発直後一回のみ
            _isOnce = true;
            ShakeByPerlinNoise shakeByPerlinNoise;
            shakeByPerlinNoise = GameObject.FindWithTag("MainCamera").GetComponent<ShakeByPerlinNoise>();
            var duration = 0.2f;
            var strength = 0.1f;
            var vibrato = 1.0f;
            shakeByPerlinNoise.StartShake(duration, strength, vibrato);
            //- 指定した位置に生成
            GameObject fire = Instantiate(
                ParticleObject,                     // 生成(コピー)する対象
                transform.position,           // 生成される位置
                Quaternion.Euler(0.0f, 0.0f, 0.0f)  // 最初にどれだけ回転するか
                );

            if (_eyeObject)
            {
                //- 指定した位置に表情生成
                GameObject eye = Instantiate(
                    _eyeObject,                     // 生成(コピー)する対象
                    transform.position,           // 生成される位置
                    Quaternion.Euler(0.0f, 0.0f, 0.0f)  // 最初にどれだけ回転するか
                    );
            }
            
            //- コントローラーの振動の設定
            vibration.SetVibration(60, 1.0f);

            //- 当たり判定を有効化する
            // 当たったオブジェクトのColliderを有効にする
            CollisionObject.gameObject.GetComponent<Collider>().enabled = true;
            // 当たり判定の拡大用コンポーネントを有効にする
            CollisionObject.gameObject.GetComponent<DetonationCollision>().enabled = true;

            //- 爆発時に描画をやめる
            StopRenderer(gameObject);

            //- 爆発音の再生
            SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Explosion,pitch,false);
        }
    }

    private void CrackerFire()
    {
        //- 弾けるタイミングになるまでは、以下の爆破処理を行わない
        //if (!_isExploded) return;

        if (!_isOnce) { // 爆破直後一回のみ

            //- タグの変更(残り花火数のタグ検索を回避するため)
            this.tag = "Untagged";
            //- 爆発処理フラグを変更
            _isOnce = true;
            //- 引火前のモデルを非アクティブ化
            transform.GetChild(0).gameObject.SetActive(false);
            //- アニメーション用のモデルをアクティブ化
            transform.GetChild(1).gameObject.SetActive(true);
            //- 一定時間後に発火する
            StartCoroutine(DelayCracker(0.7f));
            //- 着火音再生
            SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Ignition, 1.0f, false);
            //- クラッカー溜め音再生
            SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Reservoir, 1.0f, false);
            //- 一定時間後にアニメーション用を非アクティブ化
            StartCoroutine(DelaySetActive(transform.GetChild(1).gameObject, false, 0.8f));
            //- 破裂後モデルをアクティブ化
            StartCoroutine(DelaySetActive(transform.GetChild(2).gameObject, true, 0.8f));
            //- 一定時間後に破裂後モデルを非アクティブ化
            StartCoroutine(DelaySetActive(transform.GetChild(2).gameObject, false, 0.8f + ModelDeleteTime));
        }
    }

    private IEnumerator DelayCracker(float delayTime)
    {
        //- delayTime秒待機する
        yield return new WaitForSeconds(delayTime);
        //- クラッカーのエフェクト生成
        GameObject fire = Instantiate(
            _particleObject,                     // 生成(コピー)する対象
            transform.position,           // 生成される位置
            Quaternion.Euler(0.0f, 0.0f, transform.localEulerAngles.z)  // 最初にどれだけ回転するか
            );

        //- 振動の設定
        vibration.SetVibration(60, 1.0f);
        //- 破裂音の再生
        SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Brust, pitch, false);
        //- タグが花火のオブジェクトを全て取得
        GameObject[] Fireworks = GameObject.FindGameObjectsWithTag("Fireworks");
        // 原点からクラッカーへのベクトル
        Vector3 origin = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        //- 花火のオブジェクトを一つずつ実行
        foreach (var obj in Fireworks)
        {
            //- 原点から花火へのベクトル
            Vector3 direction = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
            //- クラッカーから花火へのベクトル
            Vector3 FireworkDir = direction - origin;
            //- 花火との距離を取得
            float dis = Vector3.Distance(origin, direction);
            //- 花火との距離が射程内じゃなかったら処理しない
            if (dis > BlastDis) continue;

            // 自身から花火に向かうレイを作成
            Ray ray = new Ray(transform.position, FireworkDir);
            {
                // レイが当たったオブジェクトの情報を入れる変数
                RaycastHit hit;
                //- レイを飛ばす
                if (Physics.Raycast(ray, out hit))
                {
                    //- ステージに当たった場合処理しない
                    if (hit.collider.gameObject.tag == "Stage") continue;
                }
            }
            //- 「花火へのベクトル」と「クラッカーの向きベクトル」の角度を求める
            var angle = Vector3.Angle((transform.up).normalized, (FireworkDir).normalized);
            if (/*angle != 0 && */(angle <= BlastAngle / 2))
            {
                float DisDelayRatio = (dis) / (BlastDis * _particleSystem.main.startSpeed.constantMin / 25) / 1.8f;
                float DelayTime = (10 / _particleSystem.main.startSpeed.constantMin / 25) + DisDelayRatio;
                //- 遅延をかけて爆破
                StartCoroutine(DelayDestroyCracker(obj, DelayTime));
                continue;
            }
        }

        //- 自身を破壊する
        Destroy(this.gameObject, _destroyTime);
    }

    private void HardFire()
    {
        //- 無敵時間でない時に爆破が有効になった場合、処理する
        if (IsExploded && !_isInvinsible) {
            //- 色の変更
            this.gameObject.GetComponent<Renderer>().material.color = _invColor;
            //- 爆発音の再生
            SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Explosion, pitch, false);
            //- 無敵フラグを設定
            _isInvinsible = true;
            //- 何回目の爆破かを更新     
            _blastCount++;
            if (_blastCount >= _blastNum) {
                //- 無敵時間のリセット
                _invFrameCount = 0;
                // 当たったオブジェクトのSphereColliderを有効にする
                this.transform.GetChild(0).gameObject.GetComponent<SphereCollider>().enabled = true;
                // 当たったオブジェクトのSphereColliderを有効にする
                this.transform.GetChild(0).gameObject.GetComponent<DetonationCollision>().enabled = true;
                // 指定した位置に生成
                GameObject fire = Instantiate(
                    _particleObject,                     // 生成(コピー)する対象
                    transform.position,           // 生成される位置
                    Quaternion.Euler(0.0f, 0.0f, 0.0f)  // 最初にどれだけ回転するか
                    );

                //- コントローラーの振動の設定
                vibration.SetVibration(60, 1.0f);

                // 爆発時に当たり判定を無効化
                GetComponent<SphereCollider>().isTrigger = true;
                GetComponent<MeshRenderer>().enabled = false;
            }
        }

        if (_isInvinsible) {
            //- 無敵時間のカウント
            _invFrameCount++;
            //- 無敵時間終了時の処理
            if (_invFrameCount >= _blastInvSeconds * 60) {
                //- 色の変更
                this.gameObject.GetComponent<Renderer>().material.color = _initColor;
                //- まだ爆発していない回数なら処理
                if (_blastNum > _blastCount) {
                    _isInvinsible = false;
                    _isExploded = false;
                }
            }
        }
    }

    private void DoubleFire()
    {
        //- 無敵時間でない時に爆破が有効になった場合、処理する
        if (!_isInvinsible && _isExploded) {
            //- 色の変更
            this.gameObject.GetComponent<Renderer>().material.color = _invColor;
            //- 爆発音の再生
            SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Explosion, pitch, false);
            //- 爆発回数を更新
            _blastCount++;
            //- 無敵フラグを設定
            _isInvinsible = true;
            //- 無敵時間のリセット
            _invFrameCount = 0;
            // 当たったオブジェクトのSphereColliderを有効にする
            _collisionObject.GetComponent<SphereCollider>().enabled = true;
            _collisionObject.GetComponent<DetonationCollision>().enabled = true;
            // 指定した位置に生成
            GameObject fire = Instantiate(
                _particleObject,                     // 生成(コピー)する対象
                transform.position,           // 生成される位置
                Quaternion.Euler(0.0f, 0.0f, 0.0f)  // 最初にどれだけ回転するか
                );

            //- コントローラーの振動の設定
            vibration.SetVibration(60, 1.0f);

            // 爆発時に当たり判定を無効化
            GetComponent<SphereCollider>().isTrigger = true;
            //- 一定以上爆発したら実行する処理
            if (_blastCount >= _blastNum) {
                GetComponent<MeshRenderer>().enabled = false;
            }
        }
        if (_isInvinsible) {
            //- 無敵時間のカウント
            _invFrameCount++;
            //- 無敵時間終了時の処理
            if (_invFrameCount >= _blastInvSeconds * 60) {
                //- 爆発中の判定を設定
                _isInvinsible = false;
                //- 色の変更
                this.gameObject.GetComponent<Renderer>().material.color = _initColor;
                // 当たったオブジェクトのSphereColliderを無効にする
                this.transform.GetChild(0).gameObject.GetComponent<SphereCollider>().enabled = false;
                // 当たったオブジェクトのSphereColliderを無効にする
                this.transform.GetChild(0).gameObject.GetComponent<DetonationCollision>().enabled = false;
                // 爆発時に当たり判定を有効化
                GetComponent<SphereCollider>().isTrigger = false;
                //- 爆発判定を初期化
                _isExploded = false;
            }
        }
    }
    
    private void ResurrectionBoxFire()
    {
        if (!_isOnce) { //- 爆発直後
            _isOnce = true;
            //- SpawnPlayerメソッドをdelayTime秒後に呼び出す
            StartCoroutine(SpawnPlayer(_delayTime));
        }
    }

    //- 遅れて起爆するクラッカーの関数
    private IEnumerator DelayDestroyCracker(GameObject obj, float delayTime)
    {
        //- delayTime秒待機する
        yield return new WaitForSeconds(delayTime);
        //- 既に花火玉が存在しなければ処理しない
        if (!obj) yield break;
        //- FireworksModuleの取得
        FireworksModule module = obj.gameObject.GetComponent<FireworksModule>();
        //- 花火タイプによって処理を分岐
        if (module.Type == FireworksModule.FireworksType.Boss)
            obj.GetComponent<FireworksModule>().IgnitionBoss(obj.gameObject);
        else
            obj.GetComponent<FireworksModule>().Ignition();
    }

    //- オブジェクトのアクティブ判定を変更する関数
    private IEnumerator DelaySetActive(GameObject obj,bool bIsActive ,float delayTime)
    {
        //- delayTime秒待機する
        yield return new WaitForSeconds(delayTime);
        //- アクティブ判定の変更
        obj.SetActive(bIsActive);
    }

    //- プレイヤーを再生成する関数
    private IEnumerator SpawnPlayer(float delayTime)
    {
        //- delayTime秒待機する
        yield return new WaitForSeconds(delayTime);

        //- アニメーション用の変数
        float elapsed = 0;

        //- 徐々に生成するプレイヤーの数
        int numPlayers = 1;

        //- プレイヤーを徐々に生成する
        for (int i = 0; i < numPlayers; i++) 
        {
            //- プレイヤーを生成する
            Vector3 spawnPosition = new Vector3(
                transform.position.x, transform.position.y, transform.position.z);
            GameObject player = Instantiate(
                _playerPrefab, spawnPosition, Quaternion.identity);

            //- 生成音の再生
            //SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Generated, pitch, false);

            //- SceneChangeスクリプトのプレイヤー生存フラグをtrueにする
            sceneChange.bIsLife = true;

            //- 徐々に生成するアニメーション
            while (elapsed < _animationTime) 
            {
                float t = elapsed / _animationTime;
                player.transform.localScale = 
                    Vector3.Lerp(Vector3.zero, Vector3.one, t);
                elapsed += Time.deltaTime;
                yield return null;
            }

            //- アニメーションの遅延
            yield return new WaitForSeconds(_animationDelayTime);
        }

        //- プレイヤーを生成後、復活箱を徐々に消滅
        float startTime = Time.time;
        Vector3 initialScale = transform.localScale;

        //- 消滅音の再生
        //SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Extinction, pitch, false);

        //- 復活箱を徐々に消滅させる
        while (Time.time < startTime + _boxDisTime) 
        {
            float t = (Time.time - startTime) / _boxDisTime;
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);
            yield return null;
        }
        Destroy(gameObject);
    }
}