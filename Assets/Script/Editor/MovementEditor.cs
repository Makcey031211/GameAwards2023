using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
 * 挙動のエディター
 */
[CustomEditor(typeof(MovementManager))]
public class MovementEditor : Editor
{
    private MovementManager _move;

    public override void OnInspectorGUI()
    {
        _move = target as MovementManager;

        base.OnInspectorGUI();

        EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);

        switch (_move.Type)
        {
            case MovementManager.MovementType.CicrleBehaviour:
                _move._direction  = (MovementManager.Direction)EditorGUILayout.EnumPopup("回転方向",_move.Directionary);
                _move._center     = EditorGUILayout.Vector3Field("中心点", _move.Center);
                _move._axis       = EditorGUILayout.Vector3Field("回転軸", _move.Axis);
                _move._radius     = EditorGUILayout.FloatField("半径の大きさ", _move.Radius);
                _move._periodTime = EditorGUILayout.FloatField("一周回るのにかかる時間(秒)", _move.PeriodTime);
                _move._updateRotation = EditorGUILayout.Toggle("向きを更新するかしないか",_move._updateRotation);
                break;
        }
        //- インスペクターの更新
        if (GUI.changed)
        { EditorUtility.SetDirty(target); }
    }
}
