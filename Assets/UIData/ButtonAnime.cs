/*
 ===================
 制作：大川
 ボタンアニメーションを管理するスクリプト
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

//- ボタンアニメーションクラス
public class ButtonAnime : MonoBehaviour,
    ISelectHandler,
    IDeselectHandler,
    ISubmitHandler
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private Color OverTextColor;
    private Color BaseTextColor;

    public bool bPermissionSelectSE = true; // 選択SEの再生が許可されているか

    private Button button;
    private Vector2 BaseSize;
    private Tween currentTween;

    void Awake()
    {
        if (image == null)
        { return; }
        button = GetComponent<Button>();
        image.fillAmount = 0;
        BaseTextColor = tmp.color;
    }

    //- 選択した際の処理
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        if (image == null)
        { return; }
        image.DOFillAmount(1.0f, 0.25f).SetEase(Ease.OutCubic).Play();
        tmp.DOColor(OverTextColor, 0.25f).Play();
        //- 選択音再生
        if (bPermissionSelectSE)
            SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Select);
        else
            bPermissionSelectSE = true;
    }

    /// <summary>
    /// 選択が外れた際の処理
    /// </summary>
    /// <param name="eventData"></param>
    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        if (image == null)
        { return; }
        image.DOFillAmount(0.0f, 0.25f).SetEase(Ease.OutCubic).Play();
        tmp.DOColor(BaseTextColor, 0.25f).Play();
    }

    /// <summary>
    /// ボタンが押された際に行う処理
    /// </summary>
    /// <param name="eventData"></param>
    void ISubmitHandler.OnSubmit(BaseEventData eventData)
    {
        //- 選択音再生
        SEManager.Instance.SetPlaySE(SEManager.E_SoundEffect.Click);
    }

    public void PushButtonAnime()
    {
        image.DOColor(new Color(1.0f,0.5f,0.5f), 0.25f);
    }

    /*　◇ーーーーーー拡張コードーーーーーー◇　*/
#if UNITY_EDITOR
    //- Inspector拡張クラス
    [CustomEditor(typeof(ButtonAnime))] //必須
    public class ButtonAnimeEditor : Editor //Editorの継承
    {
        public override void OnInspectorGUI()
        {
            ButtonAnime btnAnm = target as ButtonAnime;
            EditorGUI.BeginChangeCheck();
            btnAnm.image 
                = (Image)EditorGUILayout.ObjectField("動作する画像",btnAnm.image,typeof(Image),true);
            btnAnm.tmp
                = (TextMeshProUGUI)EditorGUILayout.ObjectField("テキスト", btnAnm.tmp, typeof(TextMeshProUGUI), true);
            btnAnm.OverTextColor
                = EditorGUILayout.ColorField("カラー", btnAnm.OverTextColor);
            
            //- インスペクターの更新
            if(GUI.changed)
            {   EditorUtility.SetDirty(target); }
        }
    }
#endif

}
