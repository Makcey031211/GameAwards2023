using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FireBelt : MonoBehaviour
{
    private enum E_MOVELOCATION
    {
        [InspectorName("配置位置→指定位置")]
        FromCurrentToDestination,
        [InspectorName("配置位置→目標オブジェクト地点")]
        FromCurrentToTarget,
        [InspectorName("真下→配置位置")]
        FromDownToCurrent,
        [InspectorName("移動なし")]
        NoMove
    };

    [Header("挙動設定")]
    [SerializeField, Header("移動種類")]
    private E_MOVELOCATION movelocation = E_MOVELOCATION.FromCurrentToDestination;

    [Header("配置位置から目的地に向かう際の設定")]
    [SerializeField,Header("現在地からどこまで移動するか")]
    private float AddPosY;

    [Header("配置位置から目標オブジェクト地点に向かう際の設定")]
    [SerializeField, Header("生成したい位置にあるオブジェクト")]
    private GameObject PentObj = null;
    [SerializeField, Header("目的地から数値分だけ目的地をずらす")]
    private float DiffY = 7.0f;

    [Header("共通設定")]
    [SerializeField, Header("移動時間")]
    private float MoveTime;
    [SerializeField, Header("軌跡収縮時間")]
    private float DeleteTIme;
    [SerializeField, Header("フェード時間")]
    private float FadeTIme;
    [SerializeField, Header("帯SE")]
    private AudioClip beltSE;
    [SerializeField, Header("SEの音量")]
    private float seVolume = 1.0f;


    private Image img;
    private Slider sli;
    private bool MoveComplete = false;

    void Start()
    {
        img = GetComponent<Image>();
        sli = GetComponent<Slider>();
        
        //- 移動別処理
        MoveLocation();
    }

    /// <summary>
    /// 指定位置に移動する
    /// </summary>
    private void MoveLocation()
    {
        //- Y座標を保存しておく
        float pos = img.transform.localPosition.y;
        float TargetPos;

        //- 移動種類別処理
        switch (movelocation)
        {
            //- 配置位置から指定位置まで
            case E_MOVELOCATION.FromCurrentToDestination:
                //- 指定位置を保存
                TargetPos = pos + AddPosY;
                Animetion(TargetPos);
                break;

            //- 配置位置から目標オブジェクトまで
            case E_MOVELOCATION.FromCurrentToTarget:
                //- nullチェック
                if(PentObj == null)
                {   Debug.Log("生成したいオブジェクトが設定されていません:FireBelt");    }
                //- 現在地を生成したい位置のオブジェクト位置にする
                img.transform.localPosition = new Vector3(
                    PentObj.transform.localPosition.x, 
                    img.transform.localPosition.y,
                    img.transform.localPosition.z);
                //- 目標位置を設定する
                TargetPos = PentObj.transform.localPosition.y;
                //- 移動処理
                Animetion(TargetPos);
                break;
                
            //- 真下から配置位置まで
            case E_MOVELOCATION.FromDownToCurrent:
                //- 現在地を保存
                TargetPos = pos;
                img.transform.localPosition
                        = new Vector3(img.transform.localPosition.x, -50, img.transform.localPosition.z);
                Animetion(TargetPos);
                break;
            case E_MOVELOCATION.NoMove:
                break;
        }
        
        
    }

    private void Animetion(float TargetPos)
    {
        //- 移動
        transform
            .DOLocalMoveY(TargetPos - DiffY, MoveTime)
            .SetEase(Ease.OutCubic)
            .SetLink(this.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable)
            .OnPlay(() => { SEManager.Instance.SetPlaySE(beltSE, seVolume); }) // 帯音再生
            .OnComplete(() =>
            { MoveComplete = true; });
        //- 徐々に画像が消える
        img.DOFillAmount(0, DeleteTIme)
            .SetEase(Ease.InOutQuad)
            .SetLink(this.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable);
        //- フェード
        img.DOFade(0, FadeTIme)
            .SetEase(Ease.OutSine)
            .SetLink(this.gameObject, LinkBehaviour.PauseOnDisablePlayOnEnable);
    }

    public bool GetMoveComplete()
    {
        return MoveComplete;
    }
}
