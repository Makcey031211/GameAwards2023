using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TweenColorFade{

    public enum FadeState
    {
        None,
        Out,
        In
    }

    public static Tweener FadeOut(this CanvasGroup canvasGroup, float duration)
    {
        return canvasGroup.DOFade(0.0F, duration);
    }

    public static Tweener FadeIn(this CanvasGroup canvasGroup, float duration)
    {
        return canvasGroup.DOFade(1.0F, duration);
    }
}
