using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadeManager : MonoBehaviour
{
    [SerializeField, Header("�t�F�[�h�Ɋ|���鎞��:float")]
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
