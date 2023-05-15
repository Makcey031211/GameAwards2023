using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeSmoke : MonoBehaviour
{
    [SerializeField, Header("遅延時間(秒)")]
    private float DelayTime = 2.0f;
    [SerializeField, Header("フェード時間(秒)")]
    private float FadeTime = 2.0f;
    [SerializeField, Header("フェード後のアルファ値")]
    private float FadeAlpha = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //- イメージの取得
        Image image = GetComponent<Image>();
        //- フェード
        image.DOFade(FadeAlpha, FadeTime).SetDelay(DelayTime);
    }
}
