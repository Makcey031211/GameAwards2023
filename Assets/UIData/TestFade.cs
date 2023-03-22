using UnityEngine;
using DG.Tweening;

using UnityEngine.UI;
public class TestFade : MonoBehaviour
{
    private Image image;

    // フェードインする時間（秒）
    public float fadeInTime = 1f;

    // フェードアウトする時間（秒）
    public float fadeOutTime = 1f;

    void Start()
    {
        // Imageコンポーネントを取得する
        image = GetComponent<Image>();

        // イメージのアルファ値を0に初期化
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);

        // Dotweenを使用してフェードインする
        image.DOFade(1f, fadeInTime).SetLink(image.gameObject,LinkBehaviour.PauseOnDisablePlayOnEnable);
    }

    public void FadeOut()
    {
        // Dotweenを使用してフェードアウトする
        image.DOFade(0f, fadeOutTime);
    }


    //[SerializeField]
    //UnityEngine.UI.Image image;

    //void Start()
    //{
    //    //1秒でImageのアルファを0にする
    //    //this.image.DOFade(endValue: 0f, duration: 10f);
    //    CanvasGroup c = GetComponent<CanvasGroup>();
    //    c.FadeOut(10f);
        
    //    //image.DOFade(0, 5f);
        
    //}
}
