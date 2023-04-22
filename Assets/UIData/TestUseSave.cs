
using UnityEditor;
using UnityEngine;

public class TestUseSave : MonoBehaviour
{
    private SaveManager save;
    [SerializeField,Header("�X�e�[�W�ԍ�:-1�łȂɂ����Ȃ�")]
    private int StageNum = -1;
    public void Clear()
    {
        save = new SaveManager();
        if (StageNum < 1)
        {
            StageNum = 1;
            save.SetStageClear(1);              //�N���A�ɐݒ肷��:�����̓X�e�[�W�ԍ����w��
            bool test = save.GetStageClear(1);  //�N���A�󋵂�ԋp����:�����̓X�e�[�W�ԍ����w��
            if (!test)
            {
                Debug.Log("�N���A���Ă��Ȃ��I");
            }
            else
            {
                Debug.Log("�N���A�����I");
            }
        }
    }
        /*�@���[�[�[�[�[�[�g���R�[�h�[�[�[�[�[�[���@*/
#if UNITY_EDITOR
        //- Inspector�g���N���X
    [CustomEditor(typeof(TestUseSave))]
    public class Test : Editor
    {
        private TestUseSave test;
        public override void OnInspectorGUI()
        {
            //- ��b��Inspector�\��
            base.OnInspectorGUI();
            //- �{�^���\��
            if (GUILayout.Button("�N���A�ɂ���"))
            {
                test = new TestUseSave();
                test.Clear();
            }
        }
    }
#endif
}

