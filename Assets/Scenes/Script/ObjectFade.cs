using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectFade : MonoBehaviour
{
    enum FadeType
    {
        Color
    }

    [SerializeField, Header("�t�F�[�h�̎��")]
    private FadeType fadeType;

    // �t�F�[�h�̏��
    TweenColorFade.FadeState fadeState;

    // �A���t�@�l�̕ύX�p
    CanvasGroup canvasGroup;

    // �t�F�[�h�ɂ����鎞��
    float duration;

    // �t�F�[�h����̏��
    Tweener tweener;

    // �t�F�[�h�����ǂ���
    public bool isFade { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        // �t�F�[�h��Ԃ̏�����
        fadeState = TweenColorFade.FadeState.None;

        //----- �R���|�[�l���g�̎擾
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        duration = 0;

        tweener = null;

        isFade = false;

        SetFade(TweenColorFade.FadeState.Out, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (fadeType) {
        case FadeType.Color:
            ColorFade();
            break;
        }

        tweener.OnComplete(() => {
            isFade = false;
            //Debug.Log(isFade.ToString());
        });
    }

    public void SetFade(TweenColorFade.FadeState fadeState ,float duration)
    {
        this.fadeState = fadeState;
        this.duration = duration;
        isFade = true;
    }

    private void ColorFade()
    {
        switch (fadeState) {
        case TweenColorFade.FadeState.None:
            break;

        case TweenColorFade.FadeState.Out:
            tweener = TweenColorFade.FadeOut(canvasGroup, duration);
            fadeState = TweenColorFade.FadeState.None;
            break;

        case TweenColorFade.FadeState.In:
            tweener = TweenColorFade.FadeIn(canvasGroup, duration);
            fadeState = TweenColorFade.FadeState.None;
            break;

        default:
            break;
        }
    }
}
