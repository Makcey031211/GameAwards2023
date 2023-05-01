using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
 * �����̃G�f�B�^�[
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
                _move._direction  = (MovementManager.Direction)EditorGUILayout.EnumPopup("��]����",_move.Directionary);
                _move._center     = EditorGUILayout.Vector3Field("���S�_", _move.Center);
                _move._axis       = EditorGUILayout.Vector3Field("��]��", _move.Axis);
                _move._radius     = EditorGUILayout.FloatField("���a�̑傫��", _move.Radius);
                _move._periodTime = EditorGUILayout.FloatField("������̂ɂ����鎞��(�b)", _move.PeriodTime);
                _move._updateRotation = EditorGUILayout.Toggle("�������X�V���邩���Ȃ���",_move._updateRotation);
                break;
        }
        //- �C���X�y�N�^�[�̍X�V
        if (GUI.changed)
        { EditorUtility.SetDirty(target); }
    }
}
