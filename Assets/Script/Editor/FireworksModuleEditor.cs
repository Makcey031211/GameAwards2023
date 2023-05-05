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
            _target._circleComplementNum = EditorGUILayout.IntField("�~�̕�����", _target.CircleComplementNum);
            _target._blastAngle = EditorGUILayout.FloatField("�j��p�x�͈�(0�`180�x)", _target.BlastAngle);
            _target._blastDis = EditorGUILayout.FloatField("�˒�", _target.BlastDis);
            _target._modelDeleteTime = EditorGUILayout.FloatField("���f���̎c������(�b)", _target.ModelDeleteTime);
            _target._isDrawArea = EditorGUILayout.ToggleLeft("�͈͕\��", _target.IsDrawArea);
            break;
        case FireworksModule.FireworksType.Hard:
            _target._collisionObject = (GameObject)EditorGUILayout.ObjectField("Collision Object", _target.CollisionObject, typeof(GameObject), true);
            _target._blastInvSeconds = EditorGUILayout.FloatField("���j�㖳�G����(�b)", _target.BlastInvSeconds);
            _target._invColor = EditorGUILayout.ColorField("���G���Ԓ��̐F(RGB)", _target.InvColor);
            _target._blastNum = EditorGUILayout.IntField("���j��", _target.BlastNum);
            break;
        case FireworksModule.FireworksType.Double:
            _target._collisionObject = (GameObject)EditorGUILayout.ObjectField("Collision Object", _target.CollisionObject, typeof(GameObject), true);
            _target._blastInvSeconds = EditorGUILayout.FloatField("���j�㖳�G����(�b)", _target.BlastInvSeconds);
            _target._invColor = EditorGUILayout.ColorField("���G���Ԓ��̐F(RGB)", _target.InvColor);
            _target._multiBlast = (GameObject)EditorGUILayout.ObjectField("��d�ԉ΂̓��ڂ̃G�t�F�N�g", _target.MultiBlast, typeof(GameObject), true);
                break;
        case FireworksModule.FireworksType.ResurrectionBox:
            _target._playerPrefab = (GameObject)EditorGUILayout.ObjectField("��������I�u�W�F�N�g", _target.PlayerPrefab, typeof(GameObject), true);
            _target._delayTime = EditorGUILayout.FloatField("�����܂ł̑҂�����(�b)", _target.DelayTime);
            _target._animationTime = EditorGUILayout.FloatField("�A�j���[�V��������(�b)", _target.AnimationTime);
            _target._animationDelayTime = EditorGUILayout.FloatField("�A�j���[�V�����̒x������(�b)", _target.AnimationDelayTime);
            _target._boxDisTime = EditorGUILayout.FloatField("���̏��Ŏ���(�b)", _target.BoxDisTime);
            break;
        case FireworksModule.FireworksType.Boss:
            _target._ignitionMax = EditorGUILayout.IntField("�����ɕK�v�ȉ�", _target.IgnitionMax);
            _target._movieObject = (GameObject)EditorGUILayout.ObjectField("���o���Ǘ����Ă���I�u�W�F�N�g", _target.MovieObject, typeof(GameObject), true);
            _target._outsideBarrier = (GameObject)EditorGUILayout.ObjectField("�O���̃o���A", _target.OutsideBarrier, typeof(GameObject), true);
            _target._outsideBarrierColor = EditorGUILayout.ColorField("�O���̃o���A�̐F", _target.OutsideBarrierColor);
            _target._insideBarrier =  (GameObject)EditorGUILayout.ObjectField("�����̃o���A", _target.InsideBarrier,  typeof(GameObject), true);
            _target._insideBarrierColor = EditorGUILayout.ColorField("�����̃o���A�̐F", _target.InsideBarrierColor);
            break;

        }
        //- �C���X�y�N�^�[�̍X�V
        if (GUI.changed)
        { EditorUtility.SetDirty(target); }
    }
}
