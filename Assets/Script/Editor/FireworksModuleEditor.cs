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
            _target._circleComplementNum = EditorGUILayout.IntField("円の分割数", _target.CircleComplementNum);
            _target._blastAngle = EditorGUILayout.FloatField("破裂角度範囲(0～180度)", _target.BlastAngle);
            _target._blastDis = EditorGUILayout.FloatField("射程", _target.BlastDis);
            _target._modelDeleteTime = EditorGUILayout.FloatField("モデルの残留時間(秒)", _target.ModelDeleteTime);
            _target._isDrawArea = EditorGUILayout.ToggleLeft("範囲表示", _target.IsDrawArea);
            break;
        case FireworksModule.FireworksType.Hard:
            _target._collisionObject = (GameObject)EditorGUILayout.ObjectField("Collision Object", _target.CollisionObject, typeof(GameObject), true);
            _target._blastInvSeconds = EditorGUILayout.FloatField("爆破後無敵時間(秒)", _target.BlastInvSeconds);
            _target._invColor = EditorGUILayout.ColorField("無敵時間中の色(RGB)", _target.InvColor);
            _target._blastNum = EditorGUILayout.IntField("爆破回数", _target.BlastNum);
            break;
        case FireworksModule.FireworksType.Double:
            _target._collisionObject = (GameObject)EditorGUILayout.ObjectField("Collision Object", _target.CollisionObject, typeof(GameObject), true);
            _target._blastInvSeconds = EditorGUILayout.FloatField("爆破後無敵時間(秒)", _target.BlastInvSeconds);
            _target._invColor = EditorGUILayout.ColorField("無敵時間中の色(RGB)", _target.InvColor);
            _target._multiBlast = (GameObject)EditorGUILayout.ObjectField("二重花火の二回目のエフェクト", _target.MultiBlast, typeof(GameObject), true);
                break;
        case FireworksModule.FireworksType.ResurrectionBox:
            _target._playerPrefab = (GameObject)EditorGUILayout.ObjectField("生成するオブジェクト", _target.PlayerPrefab, typeof(GameObject), true);
            _target._delayTime = EditorGUILayout.FloatField("生成までの待ち時間(秒)", _target.DelayTime);
            _target._animationTime = EditorGUILayout.FloatField("アニメーション時間(秒)", _target.AnimationTime);
            _target._animationDelayTime = EditorGUILayout.FloatField("アニメーションの遅延時間(秒)", _target.AnimationDelayTime);
            _target._boxDisTime = EditorGUILayout.FloatField("箱の消滅時間(秒)", _target.BoxDisTime);
            break;
        case FireworksModule.FireworksType.Boss:
            _target._ignitionMax = EditorGUILayout.IntField("爆発に必要な回数", _target.IgnitionMax);
            _target._movieObject = (GameObject)EditorGUILayout.ObjectField("演出を管理しているオブジェクト", _target.MovieObject, typeof(GameObject), true);
            _target._outsideBarrier = (GameObject)EditorGUILayout.ObjectField("外側のバリア", _target.OutsideBarrier, typeof(GameObject), true);
            _target._outsideBarrierColor = EditorGUILayout.ColorField("外側のバリアの色", _target.OutsideBarrierColor);
            _target._insideBarrier =  (GameObject)EditorGUILayout.ObjectField("内側のバリア", _target.InsideBarrier,  typeof(GameObject), true);
            _target._insideBarrierColor = EditorGUILayout.ColorField("内側のバリアの色", _target.InsideBarrierColor);
            break;

        }
        //- インスペクターの更新
        if (GUI.changed)
        { EditorUtility.SetDirty(target); }
    }
}
