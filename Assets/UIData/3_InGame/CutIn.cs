using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CutIn : MonoBehaviour
{
    [SerializeField] private Image BossImg;
    [SerializeField] private Image SmallCrystal;
    [SerializeField] private Image BiglCrystal;
    [SerializeField] private Image TextBack;
    [SerializeField] private TextMeshProUGUI tmp;

    public static bool MoveCompleat = false;
    private Dictionary<string,Dictionary<string, Vector3>> InitValues;

    private void Awake()
    {
        MoveCompleat = false;
        //- 初期値保存
        InitValues = new Dictionary<string, Dictionary<string, Vector3>>
        {{ "ボス", new Dictionary<string, Vector3>
             {{ "大きさ", BossImg.transform.localScale },
             { "位置", BossImg.transform.localPosition }}
        }};
        InitValues.Add("バリア小",new Dictionary<string, Vector3> {
            { "大きさ", SmallCrystal.transform.localScale },
            { "位置", SmallCrystal.transform.localPosition }});
        InitValues.Add("バリア大", new Dictionary<string, Vector3> {
            { "大きさ", BiglCrystal.transform.localScale },
            { "位置", BiglCrystal.transform.localPosition }});

        //- サイズと位置をアニメーション開始時の値に設定
        BossImg.rectTransform.localScale = Vector3.zero;
        BossImg.rectTransform.localPosition = Vector3.zero;
        SmallCrystal.rectTransform.localScale = Vector3.zero;
        SmallCrystal.rectTransform.localPosition = Vector3.zero;
        SmallCrystal.rectTransform.localRotation = Quaternion.Euler(0, 0, 90.0f);
        BiglCrystal.rectTransform.localScale = Vector3.zero;
        BiglCrystal.rectTransform.localRotation = Quaternion.Euler(0, 0, 90.0f);
        BiglCrystal.rectTransform.localPosition = Vector3.zero;
        
    }
    public void MoveCutIn()
    {
        
        var DoCutIn = DOTween.Sequence();
        //- 初めのテキストを90度回転させておく
        DOTweenTMPAnimator tmpAnimator = new DOTweenTMPAnimator(tmp);
        for (int i = 0; i < tmpAnimator.textInfo.characterCount; ++i)
        { tmpAnimator.DORotateChar(i, Vector3.up * 90, 0); }
        //- ボス画像のサイズが0から元設置サイズに
        DoCutIn
            .AppendInterval(0.5f)
            .Append(BossImg.transform.DOScale(InitValues["ボス"]["大きさ"], 0.1f))
            //.OnPlay(() => { SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Nushiapp); })
            .Append(SmallCrystal.transform.DOScale(InitValues["バリア小"]["大きさ"], 0.1f))
            .Append(BiglCrystal.transform.DOScale(InitValues["バリア大"]["大きさ"], 0.1f))
            .Append(SmallCrystal.transform.DORotate(Vector3.zero, 0.3f))
            .Append(BiglCrystal.transform.DORotate(Vector3.zero, 0.3f))
            .AppendInterval(0.25f)
            .Append(BossImg.transform.DOMove(InitValues["ボス"]["位置"], 0.5f).SetRelative(true))
            .Join(SmallCrystal.transform.DOMove(InitValues["バリア小"]["位置"], 0.5f).SetRelative(true))
            .Join(BiglCrystal.transform.DOMove(InitValues["バリア大"]["位置"], 0.5f).SetRelative(true))
            .OnComplete(()=> {
                //SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Letterapp);
                DOTween.Sequence()
                .Append(TextBack.DOFillAmount(1.0f, 0.25f));
                for (int i = 0; i < tmpAnimator.textInfo.characterCount; ++i)
                {
                    DOTween.Sequence()
                        .Append(tmpAnimator.DORotateChar(i, Vector3.zero, 0.55f));
                }
                DoCutIn.Kill();

                var DoOut = DOTween.Sequence();
                DoOut
                    .AppendInterval(1.5f)
                    .Append(BossImg.DOFade(0.0f, 0.2f))
                    .Join(SmallCrystal.DOFade(0.0f, 0.2f))
                    .Join(BiglCrystal.DOFade(0.0f, 0.2f))
                    .Join(tmp.DOFade(0.0f, 0.2f))
                    .Join(TextBack.DOFade(0.0f, 0.2f));
                MoveCompleat = true;
            });

    }


    void Update()
    {
        
    }
}
