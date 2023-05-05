using UnityEngine;
using UnityEditor;

// 制作：髙橋
/*
 * 音の編集クラス
 */
[CustomEditor(typeof(SEManager))]
public class SoundEditor : Editor
{
    private SEManager _se;

    //- 初期状態ではインスペクターで展開されていない状態
    private bool seFoldOut = false;

    public override void OnInspectorGUI()
    {
        _se = target as SEManager;

        serializedObject.Update();

        //- フォールドアウト表示用のGUIコンポーネントを作成
        seFoldOut = EditorGUILayout.Foldout(seFoldOut, "===== SE関連 =====");

        //- 折り畳まれていない時に表示されるGUIコンポーネントを作成する
        if (seFoldOut)
        {
            EditorGUILayout.LabelField("--- 花火関連 ---");
            _se.explosion = EditorGUILayout.ObjectField("爆発音", _se.explosion, typeof(AudioClip), false) as AudioClip;
            _se.spark     = EditorGUILayout.ObjectField("火花音", _se.spark, typeof(AudioClip), false) as AudioClip;
            _se.belt      = EditorGUILayout.ObjectField("打ち上げ音", _se.belt, typeof(AudioClip), false) as AudioClip;
            _se.bossbelt  = EditorGUILayout.ObjectField("ボス打ち上げ音", _se.bossbelt, typeof(AudioClip), false) as AudioClip;

            EditorGUILayout.LabelField("--- クラッカー関連 ---");
            _se.brust     = EditorGUILayout.ObjectField("破裂音", _se.brust, typeof(AudioClip), false) as AudioClip;
            _se.reservoir = EditorGUILayout.ObjectField("溜め音", _se.reservoir, typeof(AudioClip), false) as AudioClip;
            _se.ignition  = EditorGUILayout.ObjectField("着火音", _se.ignition, typeof(AudioClip), false) as AudioClip;

            EditorGUILayout.LabelField("--- 復活箱関連 ---");
            _se.generated  = EditorGUILayout.ObjectField("生成音", _se.generated, typeof(AudioClip), false) as AudioClip;
            _se.extinction = EditorGUILayout.ObjectField("消滅音", _se.extinction, typeof(AudioClip), false) as AudioClip;

            EditorGUILayout.LabelField("--- シーン関連 ---");
            _se.click   = EditorGUILayout.ObjectField("クリック音", _se.click, typeof(AudioClip), false) as AudioClip;
            _se.select  = EditorGUILayout.ObjectField("ボタン選択音", _se.select, typeof(AudioClip), false) as AudioClip;
            _se.clear   = EditorGUILayout.ObjectField("クリア音", _se.clear, typeof(AudioClip), false) as AudioClip;
            _se.failure = EditorGUILayout.ObjectField("失敗音", _se.failure, typeof(AudioClip), false) as AudioClip;

            EditorGUILayout.LabelField("--- カットイン関連 ---");
            _se.nushiapp  = EditorGUILayout.ObjectField("ヌシ出現音", _se.nushiapp, typeof(AudioClip), false) as AudioClip;
            _se.letterapp = EditorGUILayout.ObjectField("文字出現音", _se.letterapp, typeof(AudioClip), false) as AudioClip;

            EditorGUILayout.LabelField("--- 設定項目 ---");
            _se.volume  = EditorGUILayout.Slider("SE_Volume", _se.volume, 0f, 1f);
            _se.pitch   = EditorGUILayout.Slider("SE_Pitch", _se.pitch, 0f, 1f);
            _se.loop    = EditorGUILayout.Toggle("SE_Loop", _se.loop);
        }
        //- インスペクターの更新
        if (GUI.changed)
        { EditorUtility.SetDirty(target); }
    }
}