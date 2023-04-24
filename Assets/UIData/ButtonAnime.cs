using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
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
    //- アニメーション事のパターン
    private enum E_ANIMATIONTYPE
    {
        [InspectorName("拡縮挙動")]
        PopMove,
        [InspectorName("フェード挙動")]
        Fade,
        [InspectorName("アニメーションを行わない")]
        None
    };

    [SerializeField] private E_ANIMATIONTYPE animetype = E_ANIMATIONTYPE.PopMove;   //挙動タイプ 
    [SerializeField] private Vector2 SelectSize = new Vector2(1.1f,1.1f);        //ポップ
    [SerializeField] private float AlphaNum = 0.0f;  //Fade
    [SerializeField] private float MoveTime = 0.1f;  //動作完了時間
    [SerializeField] private bool Loop = false;      //ループするか
    
    public bool bPermissionSelectSE = true; // 選択SEの再生が許可されているか

    private Button button;
    private Vector2 BaseSize;
    private Tween currentTween;

    void Awake()
    {
        button = GetComponent<Button>();
        //- タイプがポップであれば初期サイズ保存
        if(animetype == E_ANIMATIONTYPE.PopMove)
        {   BaseSize = button.transform.localScale; }
    }

    //- 選択した際の処理
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        //- アニメーションが動作していたらアニメーションを削除する
        if (currentTween != null && currentTween.IsActive() && !currentTween.IsComplete())
        {   currentTween.Kill();    }
        //- タイプごとに別処理を行う
        switch (animetype)
        {
            //- ポップ挙動
            case E_ANIMATIONTYPE.PopMove:
                if(Loop)
                {
                    transform.DOScale(
                        new Vector3(BaseSize.x * SelectSize.x, BaseSize.y * SelectSize.y), MoveTime)
                        .SetEase(Ease.OutSine)
                        .SetLoops(-1, LoopType.Yoyo);
                }
                else
                {
                    transform.DOScale(
                        new Vector3(BaseSize.x * SelectSize.x, BaseSize.y * SelectSize.y), MoveTime)
                        .SetEase(Ease.OutSine);
                }
                break;
           //- フェード挙動
            case E_ANIMATIONTYPE.Fade:
                if(Loop)
                {
                    button.image.DOFade(AlphaNum, MoveTime)
                       .SetEase(Ease.OutSine)
                       .SetLoops(-1, LoopType.Yoyo);
                }
                else
                {
                    button.image.DOFade(AlphaNum, MoveTime)
                       .SetEase(Ease.OutSine);
                }
                break;
            //- 挙動なし
            case E_ANIMATIONTYPE.None:
                break;
        }
        //- 選択音再生
        if (bPermissionSelectSE)
            SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Select, 1.0f, false);
        else
            bPermissionSelectSE = true;
    }

    //- 選択解除時の処理
    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        //- アニメーションが残っていたらアニメーションを削除
        if (currentTween != null && currentTween.IsActive() && !currentTween.IsComplete())
        {
            currentTween.OnComplete(() =>
            {
                //- 項目ごとに処理を行う
                switch (animetype)
                {
                    //- ポップ挙動
                    case E_ANIMATIONTYPE.PopMove:
                        transform.DOKill();
                        transform.localScale = BaseSize;
                        break;
                    //- フェード挙動
                    case E_ANIMATIONTYPE.Fade:
                        button.image.DOKill();
                        button.image.DOFade(1.0f, 0.0f);
                        break;
                    //- 挙動なし
                    case E_ANIMATIONTYPE.None:
                        break;
                }
            });
            currentTween.Kill();
        }
        else
        {
            //- 項目ごとに処理を行う
            switch (animetype)
            {
                //- ポップ挙動
                case E_ANIMATIONTYPE.PopMove:
                    transform.DOKill();
                    transform.localScale = BaseSize;
                    break;
                //- フェード挙動
                case E_ANIMATIONTYPE.Fade:
                    button.image.DOKill();
                    button.image.DOFade(1.0f, 0.0f);
                    break;
                //- 挙動なし
                case E_ANIMATIONTYPE.None:
                    break;
            }
        }
    }

    void ISubmitHandler.OnSubmit(BaseEventData eventData)
    {
        Debug.Log("o");
        if (currentTween != null && currentTween.IsActive() && !currentTween.IsComplete())
        { currentTween.Kill(); }
        var submit = DOTween.Sequence();
        //submit.Append(transform.DOScale(new Vector3(BaseSize.x, BaseSize.y), MoveTime))
        //    .OnComplete(() =>
        //    {
                //- 選択音再生
                SEManager.Instance.SetPlaySE(SEManager.SoundEffect.Click, 1.0f, false);
            //});
        currentTween = submit;
    }


    /*　◇ーーーーーー拡張コードーーーーーー◇　*/
#if UNITY_EDITOR
    //- Inspector拡張クラス
    [CustomEditor(typeof(ButtonAnime))] //必須
    public class ButtonAnimeEditor : Editor //Editorの継承
    {
        bool folding = false; //折り畳みフラグ

        public override void OnInspectorGUI()
        {
            ButtonAnime btnAnm = target as ButtonAnime;

            /*　◇ーーーカスタム表示ーーー◇　*/
            //- 列挙型に合わせて表示を変更
            EditorGUI.BeginChangeCheck();
            btnAnm.animetype = (ButtonAnime.E_ANIMATIONTYPE)EditorGUILayout.EnumPopup("アニメーションの種類",btnAnm.animetype);

            //- animetype毎に表示する変数を変更する
            switch (btnAnm.animetype)
            {
                //- ポップ挙動
                case E_ANIMATIONTYPE.PopMove:
                    
                    folding = 
                        EditorGUILayout.BeginFoldoutHeaderGroup(folding, "ポップ挙動の設定項目");
                    
                    if(folding)
                    {
                        //- 拡大サイズの設定項目
                        btnAnm.SelectSize = 
                            EditorGUILayout.Vector2Field("選択時の拡大サイズ", btnAnm.SelectSize);
                        //- 移動完了時間設定項目
                        btnAnm.MoveTime =
                            EditorGUILayout.FloatField("挙動完了までの時間", btnAnm.MoveTime);
                        //- ループ設定項目
                        btnAnm.Loop =
                            EditorGUILayout.Toggle("ループさせるか", btnAnm.Loop);
                    }
                    EditorGUILayout.EndFoldoutHeaderGroup();
                    break;
                //- フェード挙動
                case E_ANIMATIONTYPE.Fade:
                    folding =
                       EditorGUILayout.BeginFoldoutHeaderGroup(folding, "フェード挙動の設定項目");

                    if (folding)
                    {
                        //- アルファ値の設定項目
                        btnAnm.AlphaNum =
                            EditorGUILayout.FloatField("最小アルファ値", btnAnm.AlphaNum);
                        //- 移動完了時間設定項目
                        btnAnm.MoveTime =
                            EditorGUILayout.FloatField("挙動完了までの時間", btnAnm.MoveTime);
                        //- ループ設定項目
                        btnAnm.Loop =
                            EditorGUILayout.Toggle("ループさせるか", btnAnm.Loop);
                    }
                    EditorGUILayout.EndFoldoutHeaderGroup();
                    break;

                //- アニメーションを行わない
                case E_ANIMATIONTYPE.None:
                    folding =
                       EditorGUILayout.BeginFoldoutHeaderGroup(folding, "項目なし");
                    if(folding)
                    {   Debug.Log("[UI]ボタンアニメーションを行っていません");    }
                    EditorGUILayout.EndFoldoutHeaderGroup();
                    break;
                
            }
            
            //- インスペクターの更新
            if(GUI.changed)
            {   EditorUtility.SetDirty(target); }
        }
    }
#endif

}
