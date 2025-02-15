using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/*
 ===================
 §ìFûü´
 TvFMovieV[Ì¢ACRÌ®XNvg
 ===================
 */
public class FadeIcon : MonoBehaviour
{
    [SerializeField, Header("xÔ(b)")] private float DelayTime = 2.0f;
    [SerializeField, Header("tF[hÔ(b)")] private float FadeTime = 2.0f;
    [SerializeField, Header("tF[hãÌAt@l")] private float FadeAlpha = 0.0f;
    [SerializeField, Header("tF[hOÌACR")] private Image preFadeIcon;
    [SerializeField, Header("tF[hãÌACR")] private Image postFadeIcon;

    private void Start()
    {
        //- tF[hOÌACRð\¦
        preFadeIcon.gameObject.SetActive(true);
        //- tF[hãÌACRðñ\¦É·é
        postFadeIcon.gameObject.SetActive(false);
        //- tF[hOÌACRðúóÔÉ·é
        preFadeIcon.canvasRenderer.SetAlpha(1.0f);

        //=== tF[hOÌACRðXÉtF[hAEg³¹é ===
        preFadeIcon.DOFade(FadeAlpha, FadeTime) // wèµ½AlphaÌlÉÈéÜÅÌtF[hÔ
        .SetDelay(DelayTime) // tF[hªnÜéÜÅÌxÔ
        .OnComplete(() =>
        {
            // === tF[hª®¹µ½çAtF[hãÌACRð\¦µÄtF[hC³¹é ===
            //- tF[hª®¹µ½_ÅAtF[hOÌACRÍtF[hAEgªIíÁÄ¢é½ßAñ\¦É
            preFadeIcon.gameObject.SetActive(false);
            //- tF[hãÌACRÍtF[hC³¹éOÈÌÅA\¦
            postFadeIcon.gameObject.SetActive(true);
            //- tF[hãÌACRÌAt@lð0ÉÝè
            postFadeIcon.canvasRenderer.SetAlpha(0.0f);
            //- tF[hãÌACRðwè³ê½ÔÅtF[hC³¹é
            postFadeIcon.CrossFadeAlpha(1.0f, FadeTime, false);
        });
    }
}
