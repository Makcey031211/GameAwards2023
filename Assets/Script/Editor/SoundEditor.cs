using UnityEngine;
using UnityEditor;

/// <summary>
/// SoundEditor
/// </summary>
[CustomEditor(typeof(SEManager))]
public class SoundEditor : Editor
{
    private SEManager _se;

    //- ������Ԃł̓C���X�y�N�^�[�œW�J����Ă��Ȃ����
    private bool seFoldOut = false;

    public override void OnInspectorGUI()
    {
        _se = target as SEManager;

        serializedObject.Update();

        //- �t�H�[���h�A�E�g�\���p��GUI�R���|�[�l���g���쐬
        seFoldOut = EditorGUILayout.Foldout(seFoldOut, "===== SE�֘A =====");

        //- �܂��܂�Ă��Ȃ����ɕ\�������GUI�R���|�[�l���g���쐬����
        if (seFoldOut)
        {
            EditorGUILayout.LabelField("--- �ԉΊ֘A ---");
            _se.explosion = EditorGUILayout.ObjectField("������", _se.explosion, typeof(AudioClip), false) as AudioClip;
            _se.spark     = EditorGUILayout.ObjectField("�Ήԉ�", _se.spark, typeof(AudioClip), false) as AudioClip;
            _se.belt      = EditorGUILayout.ObjectField("�ł��グ��", _se.belt, typeof(AudioClip), false) as AudioClip;

            EditorGUILayout.LabelField("--- �N���b�J�[�֘A ---");
            _se.brust     = EditorGUILayout.ObjectField("�j��", _se.brust, typeof(AudioClip), false) as AudioClip;
            _se.reservoir = EditorGUILayout.ObjectField("���߉�", _se.reservoir, typeof(AudioClip), false) as AudioClip;
            _se.ignition  = EditorGUILayout.ObjectField("���Ή�", _se.ignition, typeof(AudioClip), false) as AudioClip;

            EditorGUILayout.LabelField("--- �������֘A ---");
            _se.generated  = EditorGUILayout.ObjectField("������", _se.generated, typeof(AudioClip), false) as AudioClip;
            _se.extinction = EditorGUILayout.ObjectField("���ŉ�", _se.extinction, typeof(AudioClip), false) as AudioClip;

            EditorGUILayout.LabelField("--- �V�[���֘A ---");
            _se.click   = EditorGUILayout.ObjectField("�N���b�N��", _se.click, typeof(AudioClip), false) as AudioClip;
            _se.select  = EditorGUILayout.ObjectField("�{�^���I����", _se.select, typeof(AudioClip), false) as AudioClip;
            _se.clear   = EditorGUILayout.ObjectField("�N���A��", _se.clear, typeof(AudioClip), false) as AudioClip;
            _se.failure = EditorGUILayout.ObjectField("���s��", _se.failure, typeof(AudioClip), false) as AudioClip;

            EditorGUILayout.LabelField("--- �ݒ荀�� ---");
            _se.volume  = EditorGUILayout.Slider("SE����", _se.volume, 0f, 1f);
        }
        //- �C���X�y�N�^�[�̍X�V
        if (GUI.changed)
        { EditorUtility.SetDirty(target); }
    }
}