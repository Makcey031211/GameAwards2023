/*
 ===================
 制作：大川
 開始時の演出を行うスクリプト
 ===================
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
#if UNITY_EDITOR
//- デプロイ時にEditorスクリプトが入るとエラー。UNITY_EDITORで括る
using UnityEditor;
#endif

public class TargetDescription : MonoBehaviour
{
    [SerializeField] private Image TextBack;
    [SerializeField] private TextMeshProUGUI tmp;
    public static bool MoveCompleat = false;

    private void Awake()
    {
        MoveCompleat = false;
        TextBack.fillAmount = 0;
    }

    /// <summary>
    /// 開始演出
    /// </summary>
    public void MoveDescription()
    {
        DOTweenTMPAnimator tmpAnimator = new DOTweenTMPAnimator(tmp);
        //- テキストを90度回転させる
        for (int i = 0; i < tmpAnimator.textInfo.characterCount; ++i)
        {   tmpAnimator.DORotateChar(i, Vector3.up * 90, 0);    }

        var In = DOTween.Sequence();

        In.AppendInterval(1.0f) //開始待機時間
          .Append(TextBack.DOFillAmount(1.0f, 0.25f))   //背景出現
          .OnComplete(() => {
                //- テキスト表示
                for (int i = 0; i < tmpAnimator.textInfo.characterCount; i++)
                {DOTween.Sequence().Append(tmpAnimator.DORotateChar(i, Vector3.zero, 0.55f));}
                DOTween.To(() => tmp.characterSpacing, value => tmp.characterSpacing = value, 2.0f, 3.0f).SetEase(Ease.OutQuart);
              In.Kill();

                var Out = DOTween.Sequence();
                Out.AppendInterval(1.0f)    //撤収待機時間
                   .Append(TextBack.DOFade(0.0f, 0.2f)) //フェード
                   .Join(tmp.DOFade(0.0f, 0.2f));       //〃
            });
        
        MoveCompleat = true;
    }

    /*　◇ーーーーーー拡張コードーーーーーー◇　*/
#if UNITY_EDITOR
    //- Inspector拡張クラス
    [CustomEditor(typeof(TargetDescription))] 
    public class TargetDescriptionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            TargetDescription td = target as TargetDescription;
            EditorGUI.BeginChangeCheck();
            td.TextBack
                = (Image)EditorGUILayout.ObjectField("動作する画像", td.TextBack, typeof(Image), true);
            td.tmp
                = (TextMeshProUGUI)EditorGUILayout.ObjectField("テキスト", td.tmp, typeof(TextMeshProUGUI), true);

            //- インスペクターの更新
            if (GUI.changed)
            { EditorUtility.SetDirty(target); }
        }
    }
        
#endif
}
