using UnityEngine;
using UnityEditor;

/*
 ===================
 §ìFûü´
 TvFSEðÒW·éXNvg
 ===================
 */
[CustomEditor(typeof(SEManager))]
public class SoundEditor : Editor
{
    //- SEManagerðæ¾·éÏ
    private SEManager _se;

    //- úóÔÅÍCXyN^[ÅWJ³êÄ¢éóÔ
    private bool seFoldOut = true;

    public override void OnInspectorGUI()
    {
        _se = target as SEManager;

        base.OnInspectorGUI();

        //- tH[hAEg\¦pÌGUIR|[lgðì¬
        seFoldOut = EditorGUILayout.Foldout(seFoldOut, "===== SEÖA =====");

        //- ÜèôÜêÄ¢È¢É\¦³êéGUIR|[lgðì¬·é
        if (seFoldOut)
        {
            EditorGUILayout.LabelField("--- ÔÎÖA ---");
            _se.explosion  = EditorGUILayout.ObjectField("­¹", _se.explosion, typeof(AudioClip), false) as AudioClip;
            _se.yanagifire = EditorGUILayout.ObjectField("öÔÎ¹", _se.yanagifire, typeof(AudioClip), false) as AudioClip;
            _se.tonbofire  = EditorGUILayout.ObjectField("g{ÔÎ¹", _se.tonbofire, typeof(AudioClip), false) as AudioClip;
            _se.dragonfire = EditorGUILayout.ObjectField("hSÔÎ¹", _se.dragonfire, typeof(AudioClip), false) as AudioClip;
            _se.barrierdes = EditorGUILayout.ObjectField("oAjó¹", _se.barrierdes, typeof(AudioClip), false) as AudioClip;
            _se.belt       = EditorGUILayout.ObjectField("Å¿ã°¹", _se.belt, typeof(AudioClip), false) as AudioClip;
            _se.bossbelt   = EditorGUILayout.ObjectField("{XÅ¿ã°¹", _se.bossbelt, typeof(AudioClip), false) as AudioClip;

            EditorGUILayout.LabelField("--- NbJ[ÖA ---");
            _se.brust     = EditorGUILayout.ObjectField("jô¹", _se.brust, typeof(AudioClip), false) as AudioClip;
            _se.reservoir = EditorGUILayout.ObjectField("­ß¹", _se.reservoir, typeof(AudioClip), false) as AudioClip;
            _se.ignition  = EditorGUILayout.ObjectField("Î¹", _se.ignition, typeof(AudioClip), false) as AudioClip;

            EditorGUILayout.LabelField("---  ÖA ---");
            _se.generated  = EditorGUILayout.ObjectField("¶¬¹", _se.generated, typeof(AudioClip), false) as AudioClip;
            _se.extinction = EditorGUILayout.ObjectField("ÁÅ¹", _se.extinction, typeof(AudioClip), false) as AudioClip;

            EditorGUILayout.LabelField("--- V[ÖA ---");
            _se.click   = EditorGUILayout.ObjectField("NbN¹", _se.click, typeof(AudioClip), false) as AudioClip;
            _se.select  = EditorGUILayout.ObjectField("{^Ið¹", _se.select, typeof(AudioClip), false) as AudioClip;
            _se.clear   = EditorGUILayout.ObjectField("NA¹", _se.clear, typeof(AudioClip), false) as AudioClip;
            _se.failure = EditorGUILayout.ObjectField("¸s¹", _se.failure, typeof(AudioClip), false) as AudioClip;
            _se.slide   = EditorGUILayout.ObjectField("XCh¹",_se.slide, typeof(AudioClip),false) as AudioClip;

            EditorGUILayout.LabelField("--- JoÖA ---");
            _se.opening = EditorGUILayout.ObjectField("J¹", _se.opening, typeof(AudioClip), false) as AudioClip;

            EditorGUILayout.LabelField("--- JbgCÖA ---");
            _se.letterapp = EditorGUILayout.ObjectField("¶o»¹", _se.letterapp, typeof(AudioClip), false) as AudioClip;

            EditorGUILayout.LabelField("--- ÝèÚ ---");
            _se.volume  = EditorGUILayout.Slider("Volume", _se.volume, 0f, 1f);
            _se.pitch   = EditorGUILayout.Slider("Pitch", _se.pitch, 0f, 1f);
        }
        //- CXyN^[ÌXV
        if (GUI.changed)
        { EditorUtility.SetDirty(target); }
    }
}