using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CutIn : MonoBehaviour
{
    private enum E_BOSS_CUTIN
    {
        [Header("第一ヌシ")]
        StageNo10_Boss,
        [Header("第二ヌシ")]
        StageNo20_Boss,
        [Header("第三ヌシ")]
        StageNo30_Boss,
        [Header("第四ヌシ")]
        StageNo40_Boss,
    }

    [SerializeField] private E_BOSS_CUTIN Boss = E_BOSS_CUTIN.StageNo10_Boss;
    [SerializeField] private Image BossImg;
    [SerializeField] private Image SmallCrystal;
    [SerializeField] private Image BigCrystal;
    [SerializeField] private Image TextBack;
    [SerializeField] private TextMeshProUGUI tmp;

    public static bool MoveCompleat = false;

    private Vector3 InitPos = new Vector3(9999.0f, 9999.0f);
    private Vector3 InitTextPos;
    private Dictionary<string,Dictionary<string, Vector3>> InitValues;

    private void Awake()
    {
        //- 初期値保存
        InitValues = new Dictionary<string, Dictionary<string, Vector3>>
        {{ "ボス", new Dictionary<string, Vector3>
             {{ "開始位置", Vector3.zero },
             { "終点位置", BossImg.transform.localPosition },
             { "大きさ", BossImg.transform.localScale },}
        }};
        if (SmallCrystal)
        {
            InitValues.Add("バリア小", new Dictionary<string, Vector3> {
            { "開始位置", Vector3.zero },
            { "終点位置", SmallCrystal.transform.localPosition },
            { "大きさ", SmallCrystal.transform.localScale },});
        }
        if (BigCrystal)
        {
            InitValues.Add("バリア大", new Dictionary<string, Vector3> {
            { "開始位置", Vector3.zero },
            { "終点位置", BigCrystal.transform.localPosition },
            { "大きさ", BigCrystal.transform.localScale },});
        }

        //- サイズと位置をアニメーション開始時の値に設定
        BossImg.rectTransform.localScale = Vector3.zero;
        BossImg.rectTransform.localPosition = InitPos;
        if (SmallCrystal)
        {
            SmallCrystal.rectTransform.localScale = Vector3.zero;
            SmallCrystal.rectTransform.localPosition = InitPos;
            SmallCrystal.rectTransform.localRotation = Quaternion.Euler(0, 0, 90.0f);
        }
        if (BigCrystal)
        {
            BigCrystal.rectTransform.localScale = Vector3.zero;
            BigCrystal.rectTransform.localRotation = Quaternion.Euler(0, 0, 90.0f);
            BigCrystal.rectTransform.localPosition = InitPos;
        }
        InitTextPos = tmp.rectTransform.localPosition;
        tmp.rectTransform.localPosition = InitPos;
    }

    /// <summary>
    /// カットイン挙動を行う
    /// </summary>
    public void MoveCutIn()
    {
        switch (Boss)
        {
            case E_BOSS_CUTIN.StageNo10_Boss:
                BossNo10();
                break;
            case E_BOSS_CUTIN.StageNo20_Boss:
                BossNo20();
                break;
            case E_BOSS_CUTIN.StageNo30_Boss:
                break;
            case E_BOSS_CUTIN.StageNo40_Boss:
                break;
        }

    }

    private void BossNo10()
    {
        var DoCutIn = DOTween.Sequence();
        //- 初めのテキストを90度回転させておく
        DOTweenTMPAnimator tmpAnimator = new DOTweenTMPAnimator(tmp);
        for (int i = 0; i < tmpAnimator.textInfo.characterCount; ++i)
        { tmpAnimator.DORotateChar(i, Vector3.up * 90, 0); }
        //- ボス画像のサイズが0から元設置サイズに
        DoCutIn
            .OnPlay(() =>
            {
                BossImg.transform.localPosition = InitValues["ボス"]["開始位置"];
                SmallCrystal.transform.localPosition = InitValues["バリア小"]["開始位置"];
                BigCrystal.transform.localPosition = InitValues["バリア大"]["開始位置"];
            })
            .AppendInterval(0.5f)                                                            //遅延
            .Append(BossImg.transform.DOScale(InitValues["ボス"]["大きさ"], 0.1f))            //アニメーション開始
            .Append(SmallCrystal.transform.DOScale(InitValues["バリア小"]["大きさ"], 0.1f))
            .Append(BigCrystal.transform.DOScale(InitValues["バリア大"]["大きさ"], 0.1f))
            .Append(SmallCrystal.transform.DORotate(Vector3.zero, 0.3f))
            .Append(BigCrystal.transform.DORotate(Vector3.zero, 0.3f))
            .AppendInterval(0.25f)
            .Append(BossImg.transform.DOMove(InitValues["ボス"]["終点位置"], 0.5f).SetRelative(true))
            .Join(SmallCrystal.transform.DOMove(InitValues["バリア小"]["終点位置"], 0.5f).SetRelative(true))
            .Join(BigCrystal.transform.DOMove(InitValues["バリア大"]["終点位置"], 0.5f).SetRelative(true))
            .OnComplete(() =>
            {
                DOTween.Sequence()
                .Append(TextBack.DOFillAmount(1.0f, 0.25f))
                .OnPlay(() => { tmp.transform.localPosition = InitTextPos; });

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
                    .Join(BigCrystal.DOFade(0.0f, 0.2f))
                    .Join(tmp.DOFade(0.0f, 0.2f))
                    .Join(TextBack.DOFade(0.0f, 0.2f))
                    .OnComplete(() => { MoveCompleat = true; });

            });
    }

    private void BossNo20()
    {
        var DoCutIn = DOTween.Sequence();
        //- 初めのテキストを90度回転させておく
        DOTweenTMPAnimator tmpAnimator = new DOTweenTMPAnimator(tmp);
        for (int i = 0; i < tmpAnimator.textInfo.characterCount; ++i)
        { tmpAnimator.DORotateChar(i, Vector3.up * 90, 0); }
        //- ボス画像のサイズが0から元設置サイズに
        DoCutIn
            .OnPlay(() =>
            {BossImg.transform.localPosition = InitValues["ボス"]["開始位置"];
             BigCrystal.transform.localPosition = InitValues["バリア大"]["開始位置"];})
            .AppendInterval(0.5f)
            .Append(BossImg.transform.DOScale(InitValues["ボス"]["大きさ"], 0.1f))
            .Append(BigCrystal.transform.DOScale(InitValues["バリア大"]["大きさ"], 0.1f))
            .Append(BigCrystal.transform.DORotate(Vector3.zero, 0.3f))
            .AppendInterval(0.25f)
            .Append(BossImg.transform.DOMove(InitValues["ボス"]["終点位置"], 0.5f).SetRelative(true))
            .Join(BigCrystal.transform.DOMove(InitValues["バリア大"]["終点位置"], 0.5f).SetRelative(true))
            .OnComplete(() =>
            {
                DOTween.Sequence()
                .Append(TextBack.DOFillAmount(1.0f, 0.25f))
                .OnPlay(() => { tmp.transform.localPosition = InitTextPos; });
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
                    .Join(BigCrystal.DOFade(0.0f, 0.2f))
                    .Join(tmp.DOFade(0.0f, 0.2f))
                    .Join(TextBack.DOFade(0.0f, 0.2f))
                    .OnComplete(() => { MoveCompleat = true; });

            });
    }

    /// <summary>
    /// 動作が完了したかのフラグを返却
    /// </summary>
    /// <returns> 動作完了フラグ </returns>
    public static void ResetMoveComplete()
    { MoveCompleat = false; }
}
