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
            _target._circleComplementNum = EditorGUILayout.IntField("‰~‚Ì•ªŠ„”", _target.CircleComplementNum);
            _target._blastAngle = EditorGUILayout.FloatField("”j—ôŠp“x”ÍˆÍ(0`180“x)", _target.BlastAngle);
            _target._blastDis = EditorGUILayout.FloatField("Ë’ö", _target.BlastDis);
            _target._modelDeleteTime = EditorGUILayout.FloatField("ƒ‚ƒfƒ‹‚Ìc—¯ŠÔ(•b)", _target.ModelDeleteTime);
            _target._isDrawArea = EditorGUILayout.ToggleLeft("”ÍˆÍ•\¦", _target.IsDrawArea);
            break;
        case FireworksModule.FireworksType.Hard:
        case FireworksModule.FireworksType.MultiBlast:
            _target._collisionObject = (GameObject)EditorGUILayout.ObjectField("Collision Object", _target.CollisionObject, typeof(GameObject), true);
            _target._blastInvSeconds = EditorGUILayout.FloatField("”š”jŒã–³“GŠÔ(•b)", _target.BlastInvSeconds);
            _target._invColor = EditorGUILayout.ColorField("–³“GŠÔ’†‚ÌF(RGB)", _target.InvColor);
            _target._blastNum = EditorGUILayout.IntField("”š”j‰ñ”", _target.BlastNum);
            break;
        case FireworksModule.FireworksType.ResurrectionBox:
            _target._playerPrefab = (GameObject)EditorGUILayout.ObjectField("¶¬‚·‚éƒIƒuƒWƒFƒNƒg", _target.PlayerPrefab, typeof(GameObject), true);
            _target._delayTime = EditorGUILayout.FloatField("¶¬‚Ü‚Å‚Ì‘Ò‚¿ŠÔ(•b)", _target.DelayTime);
            _target._animationTime = EditorGUILayout.FloatField("ƒAƒjƒ[ƒVƒ‡ƒ“ŠÔ(•b)", _target.AnimationTime);
            _target._animationDelayTime = EditorGUILayout.FloatField("ƒAƒjƒ[ƒVƒ‡ƒ“‚Ì’x‰„ŠÔ(•b)", _target.AnimationDelayTime);
            _target._boxDisTime = EditorGUILayout.FloatField("” ‚ÌÁ–ÅŠÔ(•b)", _target.BoxDisTime);
            break;
        }
    }
}
