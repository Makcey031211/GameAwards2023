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
        //- �����l�ۑ�
        InitValues = new Dictionary<string, Dictionary<string, Vector3>>
        {{ "�{�X", new Dictionary<string, Vector3>
             {{ "�傫��", BossImg.transform.localScale },
             { "�ʒu", BossImg.transform.localPosition }}
        }};
        InitValues.Add("�o���A��",new Dictionary<string, Vector3> {
            { "�傫��", SmallCrystal.transform.localScale },
            { "�ʒu", SmallCrystal.transform.localPosition }});
        InitValues.Add("�o���A��", new Dictionary<string, Vector3> {
            { "�傫��", BiglCrystal.transform.localScale },
            { "�ʒu", BiglCrystal.transform.localPosition }});

        //- �T�C�Y�ƈʒu���A�j���[�V�����J�n���̒l�ɐݒ�
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
        //- ���߂̃e�L�X�g��90�x��]�����Ă���
        DOTweenTMPAnimator tmpAnimator = new DOTweenTMPAnimator(tmp);
        for (int i = 0; i < tmpAnimator.textInfo.characterCount; ++i)
        { tmpAnimator.DORotateChar(i, Vector3.up * 90, 0); }
        //- �{�X�摜�̃T�C�Y��0���猳�ݒu�T�C�Y��
        DoCutIn
            .AppendInterval(0.5f)
            .Append(BossImg.transform.DOScale(InitValues["�{�X"]["�傫��"], 0.1f))
            //.OnPlay(() => { SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Nushiapp); })
            .Append(SmallCrystal.transform.DOScale(InitValues["�o���A��"]["�傫��"], 0.1f))
            .Append(BiglCrystal.transform.DOScale(InitValues["�o���A��"]["�傫��"], 0.1f))
            .Append(SmallCrystal.transform.DORotate(Vector3.zero, 0.3f))
            .Append(BiglCrystal.transform.DORotate(Vector3.zero, 0.3f))
            .AppendInterval(0.25f)
            .Append(BossImg.transform.DOMove(InitValues["�{�X"]["�ʒu"], 0.5f).SetRelative(true))
            .Join(SmallCrystal.transform.DOMove(InitValues["�o���A��"]["�ʒu"], 0.5f).SetRelative(true))
            .Join(BiglCrystal.transform.DOMove(InitValues["�o���A��"]["�ʒu"], 0.5f).SetRelative(true))
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
