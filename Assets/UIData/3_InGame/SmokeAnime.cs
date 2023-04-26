using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SmokeAnime : MonoBehaviour
{
    [SerializeField, Header("一周に掛かる時間の振れ幅上限数値")]
    private int RandomRoteUPTime = 0;
    [SerializeField, Header("一周に掛かる時間の振れ幅下限数値")]
    private int RandomRoteDOWNTime = 0;
    [SerializeField, Header("プレイヤー出現までの遅延時間")]
    private float DelayTime = 0;
    [SerializeField, Header("フェード完了タイム")]
    private float FadeTime = 0;

    private Image img;
    private Vector2 InitSise;
    private Vector2 InitPos;
    private float RoteTime;
    private bool SmokeMove = false;
    private void Awake()
    {
        img = GetComponent<Image>();
        RoteTime = Random.Range((float)RandomRoteDOWNTime, (float)RandomRoteUPTime);
        //- 初期サイズを保存
        InitSise = img.transform.localScale;
        //- サイズを0にする
        img.transform.localScale = new Vector3(0, 0, 0);
        
    }

    void Start()
    {
        ////- 移動処理
        //transform
        //    .DOMove(new Vector3(InitPos.x,InitPos.y, 0), RoteTime)
        //    .SetEase(Ease.Linear)
        //    .SetLink(this.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable);
        //- 拡大処理
        transform.DOScale(InitSise, 0.5f);

        //- 回転処理
        transform
            .DORotate(new Vector3(0, 0, 360.0f), RoteTime, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1)   //永続ループ
            .SetLink(this.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable);
        //- 遅延
        DOTween.Sequence()
            .SetDelay(DelayTime)
            .OnComplete(() => 
            { SmokeMove = true; });

        //- フェード
        img.DOFade(0, FadeTime)
            .SetEase(Ease.OutSine)
            .SetLink(this.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable)
            .OnComplete(()=> 
            {
                Destroy(gameObject);
            });
    }

    public bool GetSmokeMove()
    {   return SmokeMove;    }
}
