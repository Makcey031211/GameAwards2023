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
            _target._blastAfterTime = EditorGUILayout.FloatField("爆発当たり判定の存在時間(秒)", _target.BlastAfterTime);
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
            _target._firstInvTime = EditorGUILayout.FloatField("一回目の被弾後無敵時間(秒)", _target.FirstInvTime);
            _target._blastAfterTime = EditorGUILayout.FloatField("爆発当たり判定の存在時間(秒)", _target.BlastAfterTime);
            _target._blastNum = EditorGUILayout.IntField("爆破回数", _target.BlastNum);
            break;
        case FireworksModule.FireworksType.Double:
            _target._collisionObject = (GameObject)EditorGUILayout.ObjectField("Collision Object", _target.CollisionObject, typeof(GameObject), true);
            _target._firstInvTime = EditorGUILayout.FloatField("一回目の被弾後無敵時間(秒)", _target.FirstInvTime);
            _target._secondAfterTime = EditorGUILayout.FloatField("2回目の当たり判定の存在時間(秒)", _target.SecondAfterTime);
            _target._multiBlast = (GameObject)EditorGUILayout.ObjectField("二重花火の二回目のエフェクト", _target.MultiBlast, typeof(GameObject), true);
            break;
        case FireworksModule.FireworksType.ResurrectionBox:
            _target._playerPrefab = (GameObject)EditorGUILayout.ObjectField("生成するオブジェクト", _target.PlayerPrefab, typeof(GameObject), true);
            _target._delayTime = EditorGUILayout.FloatField("生成までの待ち時間(秒)", _target.DelayTime);
            _target._animationTime = EditorGUILayout.FloatField("アニメーション時間(秒)", _target.AnimationTime);
            _target._animationDelayTime = EditorGUILayout.FloatField("アニメーションの遅延時間(秒)", _target.AnimationDelayTime);
            _target._boxDisTime = EditorGUILayout.FloatField("箱の消滅時間(秒)", _target.BoxDisTime);
            break;
        case FireworksModule.FireworksType.ResurrectionPlayer:
            _target._collisionObject = (GameObject)EditorGUILayout.ObjectField("Collision Object", _target.CollisionObject, typeof(GameObject), true);
            _target._invTime = EditorGUILayout.FloatField("無敵時間(秒)", _target.InvTime);
            break;
        case FireworksModule.FireworksType.Boss:
            _target._ignitionMax = EditorGUILayout.IntField("爆発に必要な回数", _target.IgnitionMax);
            _target._movieObject = (GameObject)EditorGUILayout.ObjectField("演出を管理しているオブジェクト", _target.MovieObject, typeof(GameObject), true);
            _target._outsideBarrier = (GameObject)EditorGUILayout.ObjectField("外側のバリア", _target.OutsideBarrier, typeof(GameObject), true);
            _target._outsideBarrierColor = EditorGUILayout.ColorField("外側のバリアの色", _target.OutsideBarrierColor);
            _target._insideBarrier =  (GameObject)EditorGUILayout.ObjectField("内側のバリア", _target.InsideBarrier,  typeof(GameObject), true);
            _target._insideBarrierColor = EditorGUILayout.ColorField("内側のバリアの色", _target.InsideBarrierColor);
            break;
        case FireworksModule.FireworksType.Dragonfly:
            _target._lowestSpeed =  EditorGUILayout.FloatField("最低速度", _target.LowestSpeed);
            _target._highestSpeed = EditorGUILayout.FloatField("最高速度", _target.HighestSpeed);
            _target._accelerationTime = EditorGUILayout.FloatField("加速時間", _target.AccelerationTime);
            _target._decelerationTime = EditorGUILayout.FloatField("減速時間", _target.DecelerationTime);
            _target._accelerationEase = (Easing.EaseType)EditorGUILayout.EnumPopup("加速時の補完タイプ", _target.AccelerationEase);
            _target._decelerationEase = (Easing.EaseType)EditorGUILayout.EnumPopup("減速時の補完タイプ", _target.DecelerationEase);
            break;
        case FireworksModule.FireworksType.Yanagi:
            EditorGUILayout.LabelField("--- 色関連 ---");
            _target._yanagiobj   = (GameObject)EditorGUILayout.ObjectField("柳花火用のオブジェクト", _target.YanagiObj, typeof(GameObject),true);
            _target._yanagiColor = EditorGUILayout.ColorField("柳花火の色", _target.YanagiColor);
            _target._reafobj1    = (GameObject)EditorGUILayout.ObjectField("葉っぱ用のオブジェクト1", _target.ReafObj1, typeof(GameObject), true);
            _target._reafColor1  = EditorGUILayout.ColorField("葉っぱの色1", _target.ReafColor1);
            _target._reafobj2    = (GameObject)EditorGUILayout.ObjectField("葉っぱ用のオブジェクト2", _target.ReafObj2, typeof(GameObject), true);
            _target._reafColor2  = EditorGUILayout.ColorField("葉っぱの色2", _target.ReafColor2);
            break;
        }
        //- インスペクターの更新
        if (GUI.changed)
        { EditorUtility.SetDirty(target); }
    }
}
