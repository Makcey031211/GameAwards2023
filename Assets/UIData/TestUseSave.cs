
using UnityEditor;
using UnityEngine;

public class TestUseSave : MonoBehaviour
{
    private SaveManager save;
    [SerializeField,Header("�X�e�[�W�ԍ�:-1�łȂɂ����Ȃ�")]
    private int StageNum = -1;
    public void Clear()
    {
        //save = new SaveManager();
        if (StageNum < 1)
        {
            StageNum = 1;
            save.SetStageClear(StageNum);              //�N���A�ɐݒ肷��:�����̓X�e�[�W�ԍ����w��
            bool test = save.GetStageClear(StageNum);  //�N���A�󋵂�ԋp����:�����̓X�e�[�W�ԍ����w��
            Debug.Log(test);
            if (test)
            {   Debug.Log("�N���A�����I");    }
            else
            {   Debug.Log("�N���A���Ă��Ȃ��I"); }
        }
    }

    private void Update()
    {
        save = GetComponent<SaveManager>();
        if(save.GetStageClear(StageNum))
        {
            Debug.Log(save.GetStageClear(StageNum));
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

