/*
 ===================
 制作：大川
 ギミックガイドアニメーションを管理するスクリプト
 ===================
 */

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
#if UNITY_EDITOR
//- デプロイ時にEditorスクリプトが入るとエラー。UNITY_EDITORで括る
using UnityEditor;
#endif

public class BoarMove : MonoBehaviour
{
    private enum E_OUTDIRECTION
    {
        [Header("左")]
        LEFT,
        [Header("右")]
        RIGHT,
        [Header("上")]
        UP,
        [Header("下")]
        DOWN,
    }

    private const float LEFT = -300.0f;
    private const float RIGHT = 2500.0f;
    private const float TOP = 1200.0f;
    private const float DOWN = -1200.0f;

    [SerializeField] private Image image;
    [SerializeField] private GameObject movie;
    [SerializeField] private TextMeshProUGUI tmp;

    public void Start()
    {
        //- 左から真ん中
        DOTween.Sequence()
            .Append(image.transform.DOMoveX(0, 0.3f));
        //- ボタン押されたら真ん中から右へ

    }

 
    void Update()
    {
        
    }
}
