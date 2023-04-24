
using UnityEditor;
using UnityEngine;

public class TestUseSave : MonoBehaviour
{
    private SaveManager save;
    [SerializeField,Header("ステージ番号:-1でなにもしない")]
    private int StageNum = -1;
    public void Clear()
    {
        //save = new SaveManager();
        if (StageNum < 1)
        {
            StageNum = 1;
            save.SetStageClear(StageNum);              //クリアに設定する:引数はステージ番号を指定
            bool test = save.GetStageClear(StageNum);  //クリア状況を返却する:引数はステージ番号を指定
            Debug.Log(test);
            if (test)
            {   Debug.Log("クリアした！");    }
            else
            {   Debug.Log("クリアしていない！"); }
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
    /*　◇ーーーーーー拡張コードーーーーーー◇　*/
#if UNITY_EDITOR
    //- Inspector拡張クラス
    [CustomEditor(typeof(TestUseSave))]
    public class Test : Editor
    {
        private TestUseSave test;
        public override void OnInspectorGUI()
        {
            //- 基礎のInspector表示
            base.OnInspectorGUI();
            //- ボタン表示
            if (GUILayout.Button("クリアにする"))
            {
                test = new TestUseSave();
                test.Clear();
            }
        }
    }
#endif
}

