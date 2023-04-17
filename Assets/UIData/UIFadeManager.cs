using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadeManager : MonoBehaviour
{
    [SerializeField, Header("フェードに掛かる時間:float")]
    private float FadeTime = 1.0f;

    void Start()
    {

    }

    private void OnEnable()
    {
        CanvasGroup canvas = GetComponent<CanvasGroup>();
        canvas.alpha = 0;
        canvas.FadeIn(FadeTime);
    }
}
