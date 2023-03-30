using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FireworksModule))]
public class FireworksModuleEditor : Editor {
    private FireworksModule _target;

    public override void OnInspectorGUI()
    {
        _target = target as FireworksModule;

        base.OnInspectorGUI();

        EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);

        switch (_target.Type) {
        case FireworksModule.FireworksType.Normal:
            _target._collisionObject = (GameObject)EditorGUILayout.ObjectField("Collision Object", _target.CollisionObject, typeof(GameObject), true);
            break;
        case FireworksModule.FireworksType.Cracker:
            _target._circleComplementNum = EditorGUILayout.IntField("~Ìª", _target.CircleComplementNum);
            _target._blastAngle = EditorGUILayout.FloatField("jôpxÍÍ(0`180x)", _target.BlastAngle);
            _target._blastDis = EditorGUILayout.FloatField("Ëö", _target.BlastDis);
            _target._modelDeleteTime = EditorGUILayout.FloatField("fÌc¯Ô(b)", _target.ModelDeleteTime);
            _target._isDrawArea = EditorGUILayout.ToggleLeft("ÍÍ\¦", _target.IsDrawArea);
            break;
        case FireworksModule.FireworksType.Hard:
        case FireworksModule.FireworksType.MultiBlast:
            _target._collisionObject = (GameObject)EditorGUILayout.ObjectField("Collision Object", _target.CollisionObject, typeof(GameObject), true);
            _target._blastInvSeconds = EditorGUILayout.FloatField("jã³GÔ(b)", _target._blastInvSeconds);
            _target._invColor = EditorGUILayout.ColorField("³GÔÌF(RGB)", _target._invColor);
            _target._blastNum = EditorGUILayout.IntField("jñ", _target._blastNum);
            break;
        case FireworksModule.FireworksType.ResurrectionBox:
            _target._playerPrefab = (GameObject)EditorGUILayout.ObjectField("¶¬·éIuWFNg", _target.PlayerPrefab, typeof(GameObject), true);
            _target._delayTime = EditorGUILayout.FloatField("¶¬ÜÅÌÒ¿Ô(b)", _target.DelayTime);
            _target._animationTime = EditorGUILayout.FloatField("Aj[VÔ(b)", _target.AnimationTime);
            _target._animationDelayTime = EditorGUILayout.FloatField("Aj[VÌxÔ(b)", _target.AnimationDelayTime);
            _target._boxDisTime = EditorGUILayout.FloatField(" ÌÁÅÔ(b)", _target.BoxDisTime);
            _target._generatedSound = (AudioClip)EditorGUILayout.ObjectField("¶¬¹SE", _target.GeneratedSound, typeof(AudioClip), true);
            _target._disSound = (AudioClip)EditorGUILayout.ObjectField("ÁÅ¹SE", _target.DisSound, typeof(AudioClip), true);
            break;
        }
    }
}
