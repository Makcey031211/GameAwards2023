using UnityEngine;
using UnityEditor;

/*
 ===================
 §ìFûü´
 TvFIuWFNgÌ®ðÒW·éXNvg
 ===================
*/
[CustomEditor(typeof(MovementManager))]
public class MovementEditor : Editor
{
    //- MovementManagerðæ¾·éÏ
    private MovementManager _move;

    public override void OnInspectorGUI()
    {
        _move = target as MovementManager;

        base.OnInspectorGUI();

        EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);

        switch (_move.Type)
        {
            case MovementManager.E_MovementType.ThreewayBehaviour:
                _move._moveDirection = (MovementManager.E_MoveDirection)EditorGUILayout.EnumPopup("Ú®ûü", _move.MoveDirection);
                _move._moveDistance  = EditorGUILayout.FloatField("Ú®£", _move.MoveDistance);
                _move._travelTime    = EditorGUILayout.FloatField("Ú®Ô", _move.TravelTime);
                break;
            case MovementManager.E_MovementType.ThreepointBehaviour:
                _move._startPoint   = EditorGUILayout.Vector3Field("n_", _move.StartPoint);
                _move._halfwayPoint = EditorGUILayout.Vector3Field("Ô_", _move.HalfwayPoint);
                _move._endPoint     = EditorGUILayout.Vector3Field("I_", _move.EndPoint);
                _move._moveSpeed    = EditorGUILayout.FloatField("Ú®¬x", _move.MoveSpeed);
                _move._endWaitTime  = EditorGUILayout.FloatField("I_BÌÒ@Ô", _move.EndWaitTime);
                break;
            case MovementManager.E_MovementType.ThreepointWaitBehaviour:
                _move._startPoint   = EditorGUILayout.Vector3Field("n_", _move.StartPoint);
                _move._halfwayPoint = EditorGUILayout.Vector3Field("Ô_", _move.HalfwayPoint);
                _move._endPoint     = EditorGUILayout.Vector3Field("I_", _move.EndPoint);
                _move._moveSpeed    = EditorGUILayout.FloatField("Ú®¬x", _move.MoveSpeed);
                _move._waitTime     = EditorGUILayout.FloatField("e|CgBÌÒ@Ô", _move.WaitTime);
                break;
            case MovementManager.E_MovementType.CicrleBehaviour:
                _move._rotaDirection  = (MovementManager.E_RotaDirection)EditorGUILayout.EnumPopup("ñ]ûü",_move.RotaDirection);
                _move._center         = EditorGUILayout.Vector3Field("S_", _move.Center);
                _move._axis           = EditorGUILayout.Vector3Field("ñ]²", _move.Axis);
                _move._radius         = EditorGUILayout.FloatField("¼aÌå«³", _move.Radius);
                _move._periodTime     = EditorGUILayout.FloatField("êüñéÌÉ©©éÔ(b)", _move.PeriodTime);
                _move._updateRotation = EditorGUILayout.Toggle("ü«ðXV·é©µÈ¢©",_move._updateRotation);
                break;
            case MovementManager.E_MovementType.SmoothCircularBehaviour:
                _move._rotaDirection  = (MovementManager.E_RotaDirection)EditorGUILayout.EnumPopup("ñ]ûü", _move.RotaDirection);
                _move._center         = EditorGUILayout.Vector3Field("S_", _move.Center);
                _move._axis           = EditorGUILayout.Vector3Field("ñ]²", _move.Axis);
                _move._radius         = EditorGUILayout.FloatField("¼aÌå«³", _move.Radius);
                _move._startTime      = EditorGUILayout.FloatField("JnÉ¸ç·Ô(b)", _move.StartTime);
                _move._periodTime     = EditorGUILayout.FloatField("êüñéÌÉ©©éÔ(b)", _move.PeriodTime);
                _move._updateRotation = EditorGUILayout.Toggle("ü«ðXV·é©µÈ¢©", _move._updateRotation);
                break;
        }
        //- CXyN^[ÌXV
        if (GUI.changed)
        { EditorUtility.SetDirty(target); }
    }
}